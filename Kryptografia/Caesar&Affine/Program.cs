using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Jan Bienias
//238201
//Kryptografia - zadanie 1, szyfr cezara i afiniczny


//try / catch w mainie wyłapuje to ale nie przedstawia konkretnych powodow bledu, czyt. program nie jest w pełni idiotoodporny
//(np. nie ma sprawdzenia przed czytaniem czy pliki istnieja etc...)

namespace CaesarAffine
{

    class Program
    {
        public static string filePlain = "plain.txt";
        public static string fileKey = "key.txt";
        public static string fileCrypto = "crypto.txt";
        public static string fileDecrypt = "decrypt.txt";
        public static string fileExtra = "extra.txt";
        public static string fileKeyNew = "key-new.txt";
        const int ALPH = 26;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Za mało argumentów!");
                return;
            }

            try
            {
                if (args[0].Equals("-c"))
                {
                    if (args[1].Equals("-e")) //szyfrowanie
                    {
                        string plain = readFileLine(filePlain);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string cipher = caesarEncrypt(key[0], plain);
                        writeToFile(fileCrypto, cipher);

                    }
                    else if (args[1] == "-d") //odszyfrowanie
                    {
                        string cipher = readFileLine(fileCrypto);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string plain = caesarDecrypt(key[0], cipher);
                        writeToFile(fileDecrypt, plain);
                    }
                    else if (args[1] == "-j") //kryptoanaliza z tekstem jawnym
                    {
                        string cipher = readFileLine(fileCrypto);
                        string helper = readFileLine(fileExtra);
                        string[] krypto = helpCaesarDecrypt(cipher, helper);
                        writeToFile(fileKeyNew, krypto[1]);
                        writeToFile(fileDecrypt, krypto[0]);
                    }
                    else if (args[1] == "-k") //kryptoanaliza wyłącznie w oparciu o kryptogram
                    {
                        string cipher = readFileLine(fileCrypto);
                        string krypto = brutalCaesarDecrypt(cipher);
                        writeToFile(fileDecrypt, krypto);
                    }
                }
                else if (args[0] == "-a")
                {
                    if (args[1] == "-e")
                    {
                        string plain = readFileLine(filePlain);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string cipher = affineEncrypt(key[0], key[1], plain);
                        writeToFile(fileCrypto, cipher);
                    }
                    else if (args[1] == "-d")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string keyFile = readFileLine(fileKey);
                        int[] key = retrieveKeys(keyFile);
                        string plain = affineDecrypt(key[0], key[1], cipher);
                        writeToFile(fileDecrypt, plain);
                    }
                    else if (args[1] == "-j")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string helper = readFileLine(fileExtra);
                        string[] krypto = helpAffineDecrypt(cipher, helper, 0);
                        writeToFile(fileKeyNew, krypto[1] + " " + krypto[2]);
                        writeToFile(fileDecrypt, krypto[0]);
                    }
                    else if (args[1] == "-k")
                    {
                        string cipher = readFileLine(fileCrypto);
                        string krypto = brutalAffineDecrypt(cipher);
                        writeToFile(fileDecrypt, krypto);
                    }
                }
                else
                {
                    Console.WriteLine("Użyto złych argumentów! Uzyj np. nazwa.exe -a -d");
                }
            }
            catch (Exception) { Console.WriteLine("Błąd!"); }
        }



        //FUNCKJE

        public static string caesarEncrypt(int key, string input)
        {
            //E(k,x)=x+k (mod 26)
            return encrypt(1, key, input);
        }

        public static string caesarDecrypt(int key, string input)
        {
            //D(k,y)=y-k (mod 26)
            return decrypt(1, key, input);
        }

        public static string affineEncrypt(int a, int b, string input)
        {
            //E(a,b,x)=a*x+b (mod 26)
            if (gcd(a, ALPH) != 1)
            {
                Console.WriteLine("Niepoprawny klucz do enkrypcji");
                return null;
            }
            return encrypt(a, b, input);
        }

        public static string affineDecrypt(int a, int b, string input)
        {
            //D(a,b,y)=a'*(y-b) (mod 26)
            int aOpposite = oppositeModulo(a, ALPH);
            if (aOpposite == 0)
            {
                Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + ALPH + " dla " + a + "!");
                return null;
            }
            else
                return decrypt(aOpposite, b, input);
        }

        public static string brutalCaesarDecrypt(string input)
        {
            string output = "";
            for (int i = 0; i < 26; i++)
            {
                output += caesarDecrypt(i, input);
                output += '\n';
            }
            return output;
        }

        public static string brutalAffineDecrypt(string input)
        {
            string output = "";
            int a;
            var list = new List<int>();
            for (int i = 1; i < ALPH; i++)
            {
                if (checkAffineKey(i, ALPH))
                {
                    list.Add(i);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                a = list[i];
                int aOpposite = oppositeModulo(a, ALPH);
                if (aOpposite == 0)
                {
                    Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + ALPH + " dla " + a + "!");
                }
                else
                {
                    for (int b = 0; b < ALPH; b++)
                    {
                        output += affineDecrypt(aOpposite, b, input);
                        output += "\n";
                    }
                }
            }
            return output;
        }

        public static string[] helpCaesarDecrypt(string input, string inputHelper)
        {
            //returns array of strings size 2, index 0 = decrypted cipher, index 1 = guessed key
            if (inputHelper.Length < 1)
            {
                Console.WriteLine("Za mało danych!");
                return null;
            }
            string[] output = new string[2];
            int key;
            for (int i = 0; i < inputHelper.Length; i++)
            {
                if (checkCharAlphabetic(input[i]))
                {
                    key = Convert.ToInt32((input[i] - inputHelper[i]) % 26);
                    output[0] = caesarDecrypt(key, input);
                    if (key < 0)
                        key += ALPH;
                    output[1] = key.ToString();
                    break;
                }
            }
            return output;
        }

        public static string[] helpAffineDecrypt(string input, string inputHelper, int startingIndex)
        {
            //returns array of strings size 3, index 0 = decrypted cipher, index 1 = guessed a, index 2 = guessed b
            //https://www.youtube.com/watch?v=ry3g0xN8QKU
            if (inputHelper.Length < 2)
            {
                Console.WriteLine("Za mało danych aby odgadnac klucze!");
                return null;
            }
            string[] output = new string[3];
            int[,] tab = new int[2, 2];
            int firstPairIndex = 0;
            int firstPairFound = 0;
            int secondPairIndex = 0;
            int secondPairFound = 0;
            for (int i = startingIndex; i < inputHelper.Length; i++)
            {
                if (checkCharAlphabeticLower(inputHelper[i]))
                {
                    tab[0, 0] = Convert.ToInt32(inputHelper[i] - 97);
                    tab[0, 1] = Convert.ToInt32(input[i] - 97);
                    firstPairIndex = i;
                    firstPairFound = 1;
                    break;
                }
                if (checkCharAlphabeticUpper(inputHelper[i]))
                {
                    tab[0, 0] = Convert.ToInt32(inputHelper[i] - 'A');
                    tab[0, 1] = Convert.ToInt32(input[i] - 'A');
                    firstPairIndex = i;
                    firstPairFound = 1;
                    break;
                }
            }

            if (firstPairFound == 1)
            {
                //Console.WriteLine("Znalazłem pierwsza pare");
                firstPairIndex++;
                if (firstPairIndex == inputHelper.Length - 1)
                {
                    Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                    return null;
                }

                for (int i = firstPairIndex; i < inputHelper.Length; i++)
                {
                    if (checkCharAlphabeticLower(inputHelper[i]))
                    {
                        tab[1, 0] = Convert.ToInt32(inputHelper[i] - 'a');
                        tab[1, 1] = Convert.ToInt32(input[i] - 'a');
                        secondPairFound = 1;
                        secondPairIndex = i;
                        break;
                    }
                    if (checkCharAlphabeticUpper(inputHelper[i]))
                    {
                        tab[1, 0] = Convert.ToInt32(inputHelper[i] - 'A');
                        tab[1, 1] = Convert.ToInt32(input[i] - 'A');
                        secondPairFound = 1;
                        secondPairIndex = i;
                        break;
                    }
                }

                if (secondPairFound == 1)
                {
                    Console.WriteLine("Znalazłem dwie pary");
                    Console.WriteLine("extra: " + Convert.ToChar(tab[0, 0] + 97) + " " + Convert.ToChar(tab[1, 0] + 97));
                    Console.WriteLine("cipher: " + Convert.ToChar(tab[0, 1] + 97) + " " + Convert.ToChar(tab[1, 1] + 97));
                    Console.WriteLine("Sprawdzam...");
                    int a, b;
                    int x = Convert.ToInt32(tab[0, 0] - tab[1, 0]);
                    if (x < 0)
                        x += ALPH;
                    int y = Convert.ToInt32(tab[0, 1] - tab[1, 1]);
                    if (y < 0)
                        y += ALPH;
                    int xOpposite = oppositeModulo(x, ALPH);
                    if (xOpposite != 0)
                    {
                        a = (y * xOpposite) % ALPH;
                        b = (tab[0, 1] - ((a * tab[0, 0]) % ALPH) + ALPH) % ALPH;
                        if (a != 0)
                        {
                            output[1] = a.ToString();
                            output[2] = b.ToString();
                            int aOpposite = oppositeModulo(a, ALPH);
                            if (aOpposite == 0)
                            {
                                Console.WriteLine("Nie mogę znaleźć liczby przeciwnej w mod " + ALPH + " dla " + a + "!");
                                return null;
                            }
                            else
                            {
                                Console.WriteLine("Udało się! Klucz to : " + a + ", " + b);
                                output[0] = affineDecrypt(a, b, input);
                                return output;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Niepoprawne a");
                            return helpAffineDecrypt(input, inputHelper, secondPairIndex);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie mogę znaleźć przeciwności dla " + x + " w mod " + ALPH);
                        if (secondPairIndex == inputHelper.Length - 1)
                            return null;
                        return helpAffineDecrypt(input, inputHelper, secondPairIndex);

                    }
                }
                else
                {
                    Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                    return null;
                }

            }

            else
            {
                Console.WriteLine("Za mało danych w tekscie pomocniczym!");
                return null;
            }

            return null;
        }


        //FUNKCJE POMOCNICZE

        public static string encrypt(int a, int b, string input)
        {
            string output = "";
            char[] chars = input.ToCharArray();
            int x;
            for (int i = 0; i < input.Length; i++)
            {
                if (chars[i] >= 'A' && chars[i] <= 'Z')
                {
                    x = Convert.ToInt32(chars[i] - 65);
                    output += Convert.ToChar((((x * a) + b + ALPH) % ALPH) + 65);
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {
                    x = Convert.ToInt32(chars[i] - 97);
                    output += Convert.ToChar((((x * a) + b + ALPH) % ALPH) + 97);
                }
                else
                {
                    output += chars[i];
                }
            }
            return output;
        }

        public static string decrypt(int a, int b, string input)
        {
            string output = "";
            char[] chars = input.ToCharArray();
            int x;
            for (int i = 0; i < input.Length; i++)
            {
                if (chars[i] >= 'A' && chars[i] <= 'Z')
                {
                    x = Convert.ToInt32(chars[i] - 65);
                    if (x - b < 0) x = Convert.ToInt32(x) + ALPH;
                    output += Convert.ToChar(((a * (x - b)) % ALPH) + 65);
                }

                else if (chars[i] >= 'a' && chars[i] <= 'z')
                {

                    x = Convert.ToInt32(chars[i] - 97);
                    if (x - b < 0) x = Convert.ToInt32(x) + ALPH;
                    output += Convert.ToChar(((a * (x - b)) % ALPH) + 97);
                }
                else
                {
                    output += chars[i];
                }
            }
            return output;
        }

        public static bool checkAffineKey(int a, int range)
        {
            if ((gcd(a, range) == 1) && ((a * (oppositeModulo(a, range)) % range) == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool checkCharAlphabetic(char c)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                return true;
            }
            return false;
        }

        public static bool checkCharAlphabeticLower(char c)
        {
            if (c >= 'a' && c <= 'z')
            {
                return true;
            }
            return false;
        }

        public static bool checkCharAlphabeticUpper(char c)
        {
            if (c >= 'A' && c <= 'Z')
            {
                return true;
            }
            return false;
        }

        public static int oppositeModulo(int n, int m)
        //naive method
        {
            for (int i = 1; i < m; i++)
            {
                if ((n * i) % m == 1)
                {
                    return i;
                }
            }
            return 0;
        }

        public static int gcd(int a, int b)
        {
            int temp;
            while (b != 0)
            {
                temp = a % b;

                a = b;
                b = temp;
            }
            return a;
        }

        public static string readFileLine(string filename)
        {
            string output = File.ReadLines(filename).First();
            return output;
        }

        public static void writeToFile(string filename, string txt)
        {
            File.WriteAllText(filename, txt);
        }

        public static int[] retrieveKeys(string input)
        {
            string[] outputH = new string[2];
            int[] output = new int[2];
            char[] t = input.ToCharArray();
            int secondNumber = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (t[i] == 32)
                {
                    secondNumber = 1;
                }
                if (secondNumber == 0)
                {
                    outputH[0] += t[i];
                }
                else if (secondNumber == 1)
                {
                    outputH[1] += t[i];
                }
            }
            output[0] = Int32.Parse(outputH[0]);
            output[1] = Int32.Parse(outputH[1]);

            return output;
        }
    }
}
