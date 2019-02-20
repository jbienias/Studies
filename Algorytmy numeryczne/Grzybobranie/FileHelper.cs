using System;
using System.IO;
using System.Globalization;
using System.Threading;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Grzybobranie {
    public class FileHelper {

        public static Data loadData(string filename) {
            Data data = new Data();
            if (!File.Exists(filename))
                throw new FileNotFoundException("Could not open file " + filename + " !");
            string[] loadedData = File.ReadAllLines(filename);
            data.mapSize = Int32.Parse(loadedData[0]) * 2 + 1;
            string[] scanner = loadedData[1].Split(' ');
            data.mushroomCount = Int32.Parse(scanner[0]);
            data.mushroomMap = new bool[data.mapSize];
            for (int i = 0; i < data.mushroomCount; i++)
                data.mushroomMap[Int32.Parse(scanner[i + 1])] = true;
            scanner = loadedData[2].Split(' ');
            data.playerOnePos = Int32.Parse(scanner[0]);
            data.playerTwoPos = Int32.Parse(scanner[1]);
            data.diceSize = Int32.Parse(loadedData[3]);
            data.diceValues = new int[data.diceSize];
            data.diceProbabilities = new int[data.diceSize];
            scanner = loadedData[4].Split(' ');
            for (int i = 0; i < data.diceSize; i++)
                data.diceValues[i] = Int32.Parse(scanner[i]);
            scanner = loadedData[5].Split(' ');
            for (int i = 0; i < data.diceSize; i++) {
                data.diceProbabilities[i] = Int32.Parse(scanner[i]);
                data.probabilitySum += data.diceProbabilities[i];
            }
            return data;
        }

        public static void saveMatrix(string filename, MyMatrix matrix) {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            using (TextWriter tw = new StreamWriter(filename)) {
                tw.WriteLine(matrix.rowCount + " " + matrix.columnCount);
                for (int i = 0; i < matrix.rowCount; i++) {
                    for (int j = 0; j < matrix.columnCount; j++) {
                        tw.Write(matrix[i, j] + " ");
                    }
                    tw.WriteLine();
                }
            }
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;
        }

        public static void saveStats(string filename, int matrixSize, double monteCarlo, int[] iterations, double[] wins, double[] time) {
            // string header = "rozmiar;wynik monte carlo;wynik gaussa partial bez optymalizacji;wynik gaussa partial z optymalizacja;wynik gaussa jacobi;wynik gaussa seidel;ilosc iteracji jacobi;ilosc iteracji seidel;czas gaussa partial bez optymalizacji;czas gaussa partial z optymalizacja;czas gaussa jacobi;czas gaussa seidel\n";
            string tmp = matrixSize + ";" + monteCarlo + ";";
            for (int i = 0; i < wins.Length; i++) {
                tmp += wins[i];
                if (i != wins.Length - 1)
                    tmp += ";";
            }
            tmp += ";";
            for (int i = 0; i < iterations.Length; i++) {
                tmp += iterations[i];
                if (i != iterations.Length - 1)
                    tmp += ";";
            }
            tmp += ";";
            for (int i = 0; i < time.Length; i++) {
                tmp += time[i];
                if (i != time.Length - 1)
                    tmp += ";";
            }
            tmp += "\n";
            using (StreamWriter w = File.AppendText(filename)) {
                w.Write(tmp);
            }
        }
    }
}
