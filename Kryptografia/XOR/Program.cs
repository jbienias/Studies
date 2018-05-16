using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

//Jan Bienias
//238201
//XOR

namespace XOR {
    class Program {

        public static string fileOrig = "orig.txt";
        public static string filePlain = "plain.txt";
        public static string fileKey = "key.txt";
        public static string fileCrypto = "crypto.txt";
        public static string fileDecrypt = "decrypt.txt";
        public static int lineLength = 64; //characters per line in file, also the length of the key

        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Wywolaj program z argumentem '-p', '-e' lub '-k'!");
                return;
            }
            if (args[0].Equals("-p")) {
                if (!File.Exists(fileOrig)) {
                    Console.WriteLine("Brak pliku orig.txt!");
                    return;
                }
                if (!(new FileInfo(fileOrig).Length > 0)) {
                    Console.WriteLine("Plik orig.txt jest pusty!");
                    return;
                }
                string orig = readFile(fileOrig);
                orig = prepareStringForXor(orig);
                writeToFileBinary(filePlain, orig);
                Console.WriteLine("Plik plain.txt został przygotowany!");
                return;
            }

            if (args[0].Equals("-e")) {
                if (!File.Exists(filePlain)) {
                    Console.WriteLine("Plik plain.txt nie zostal przygotowany! Wywolaj program z komenda '-p'");
                    return;
                }
                if (!File.Exists(fileKey)) {
                    Console.WriteLine("Plik key.txt nie istnieje!");
                    return;
                }
                string plain = readFile(filePlain);
                string key = readFileLine(fileKey);
                if (!checkLenEqualInt(key, lineLength)) {
                    Console.WriteLine("Plik key.txt zawiera niewlasciwy klucz! Klucz musi miec dlugosc : " + lineLength);
                    return;
                }
                //A moze check czy jest alfanumeryczny?
                //string keyCheck = onlyAlphanumericLowercase(key);
                //if(!key.Equals(keyCheck)) {
                //    Console.WriteLine("Klucz moze zawierac jedynie male litery);
                //}
                string crypto = xor(plain, key);
                writeToFileBinary(fileCrypto, crypto);
                Console.WriteLine("Plik crypto.txt utworzony!");
                return;
            }

            if (args[0].Equals("-k")) {
                if (!File.Exists(fileCrypto)) {
                    Console.WriteLine("Plik crypto.txt nie istnieje! Wywolaj program z komenda '-e'");
                }
                string crypto = readFile(fileCrypto);
                string decryptBrutal = xorBrutal(crypto);
                writeToFileBinary(fileDecrypt, decryptBrutal);
                Console.WriteLine("Plik decrypt.txt utworzony!");
                return;
            } else {
                Console.WriteLine("Wywolaj program z argumentem '-p', '-e' lub '-k'!");
                return;
            }

        }

        public static string xor(string input, string key) {
            string output = string.Empty;
            int charInLine = 0; //current number/index of char in line
            for (int i = 0; i < input.Length; i++) {
                if (charInLine % lineLength == 0) {
                    charInLine = 0;
                }
                output += Convert.ToChar(key[charInLine] ^ input[i]);
                charInLine++;
            }
            return output;
        }

        public static string xorBrutal(string input) {

            string output = string.Empty;
            char[] guessKey = new char[lineLength];
            int charInLine = 0; //current number/index of char in line
            int spaceMask = 64; //previously typeOf char
                                //spaceMask =   0  1  0  0  0  0  0  0
                                //            128 64 32 16  8  4  2  1
            int A, B, C, space;   //previously typeOf char
            space = Convert.ToInt32(' ');
            for (int i = 0; i < input.Length - 2; i++) {
                A = Convert.ToInt32(input[i]);
                B = Convert.ToInt32(input[i + 1]);
                C = Convert.ToInt32(input[i + 2]);

                try {

                    if (((A ^ B) & spaceMask) != 0) {
                        if (((A ^ C) & spaceMask) != 0 && ((B ^ C) & spaceMask) == 0) {
                            guessKey[charInLine] = Convert.ToChar(A ^ space);
                        } else {
                            if (((A ^ C) & spaceMask) == 0 && ((B ^ C) & spaceMask) != 0) {
                                guessKey[charInLine + 1] = Convert.ToChar(B ^ space);
                            }
                        }
                    }
                } catch (Exception) { }

                try {
                    if (((A ^ C) & spaceMask) != 0) {
                        if (((A ^ B) & spaceMask) != 0 && ((B ^ C) & spaceMask) == 0) {
                            guessKey[charInLine] = Convert.ToChar(A ^ space);
                        } else {
                            if (((A ^ B) & spaceMask) == 0 && ((B ^ C) & spaceMask) != 0) {
                                guessKey[charInLine + 2] = Convert.ToChar(C ^ space);
                            }
                        }
                    }
                } catch (Exception) { }

                try {
                    if (((B ^ C) & spaceMask) != 0) {
                        if (((A ^ B) & spaceMask) != 0 && ((A ^ C) & spaceMask) == 0) {
                            guessKey[charInLine + 1] = Convert.ToChar(B ^ space);
                        } else {
                            if (((A ^ B) & spaceMask) == 0 && ((A ^ C) & spaceMask) != 0) {
                                guessKey[charInLine + 2] = Convert.ToChar(C ^ space);
                            }
                        }
                    }
                } catch (Exception) { }
                charInLine++;
                if (charInLine % (lineLength) == 0) {
                    charInLine = 0;
                }
            }
            string guessedKey = new string(guessKey);
            Console.WriteLine("Odgadniety klucz : " + guessedKey);
            output = xor(input, guessedKey);
            return output;
        }

        public static string prepareStringForXor(string input) {
            string output = string.Empty;
            input = onlyAlphanumericLowercase(input);
            int charInLine = 0; //current number/index of char in line
            for (int i = 0; i < input.Length; i++) {
                if (charInLine == lineLength) {
                    output += '\n';
                    charInLine = 0;
                }
                if (input[i] == '\n') {
                    output += ' ';
                } else {
                    output += input[i];
                }
                charInLine++;
            }
            //filling the last line with empty spaces, if the len != lineLength
            while (charInLine != lineLength) {
                output += ' ';
                charInLine++;
            }
            return output;
        }

        public static string onlyAlphanumericLowercase(string input) {
            string output = "";
            input = input.ToLower();
            for (int i = 0; i < input.Length; i++) {
                if ((input[i] > 96 && input[i] < 123) || input[i] == ' ') {
                    output += input[i];
                } else {
                    output += ' ';
                }
            }
            //replace multiple whitespaces / tabulations with one space
            output = Regex.Replace(output, @"\s+", " ");
            return output;
        }

        public static bool checkLenEqualInt(string input, int number) {
            if (input.Length == number)
                return true;
            return false;
        }

        public static string readFile(string filename) {
            string output = File.ReadAllText(filename);
            return output;
        }

        public static string readFileLine(string filename) {
            string output = File.ReadLines(filename).First();
            return output;
        }

        public static void writeToFile(string filename, string txt) {
            File.WriteAllText(filename, txt);
            return;
        }

        public static void writeToFileBinary(string filename, string txt) {
            byte[] text = Encoding.ASCII.GetBytes(txt);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(fs);
            writer.Write(text);
            writer.Close();
        }


    }
}
