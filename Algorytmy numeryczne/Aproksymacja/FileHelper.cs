using System;
using System.IO;
using System.Globalization;
using System.Threading;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Aproksymacja {
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

        public static int[] loadSizes(string filename) {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Could not open file " + filename + " !");
            string[] input = File.ReadAllLines(filename);
            int[] result = new int[input.Length];
            for (int i = 0; i < result.Length; i++) {
                result[i] = Int32.Parse(input[i]);
            }
            return result;
        }

        public static double[,] loadDataForAprox(string filename) {
            if (!File.Exists(filename))
                throw new FileNotFoundException("Could not open file " + filename + " !");
            String[] lines = File.ReadAllLines(filename);
            double[,] result = new double[lines.Length, 2];
            int i = 0, j = 0;
            foreach (var row in lines) {
                j = 0;
                foreach (var col in row.Trim().Split(';')) {
                    result[i, j] = Double.Parse(col.Trim());
                    j++;
                }
                i++;
            }
            return result;
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

        public static void saveMatrixSparse(string filename, MyMatrix matrix) {
            CultureInfo customCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            using (TextWriter tw = new StreamWriter(filename)) {
                tw.WriteLine(matrix.rowCount + " " + matrix.columnCount);
                for (int i = 0; i < matrix.rowCount; i++) {
                    for (int j = 0; j < matrix.columnCount; j++) {
                        if (matrix[i, j] != 0.0) {
                            tw.WriteLine(i + " " + j + " " + matrix[i, j]);
                        }
                    }
                }
            }
            customCulture.NumberFormat.NumberDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = customCulture;
        }

        public static void saveVectorOfNonZeroElements(string filename, MyMatrix matrix) {
            MyMatrix vector = new MyMatrix(matrix.rowCount, 1);
            int counter = 0;
            for (int j = 0; j < matrix.columnCount; j++) {
                counter = 0;
                for (int i = 0; i < matrix.rowCount; i++) {
                    if (matrix[i, j] != 0.0) counter++;
                }
                vector[j, 0] = counter;
            }
            saveMatrix(filename, vector);
        }

        public static void countAndSaveVectorOfNonZeroElements(string matrixFilename, string saveTo) {
            MyMatrix vector;
            string line = "";
            string[] splitted;
            int iter = 0;
            using (StreamReader sr = new StreamReader(matrixFilename)) {
                line = sr.ReadLine(); splitted = line.Split(); int size = Int32.Parse(splitted[0]);
                vector = new MyMatrix(size, 1);
                while (true) {
                    line = sr.ReadLine();
                    if (line == null) {
                        break;
                    } else {
                        splitted = line.Split(' ');
                        vector[Int32.Parse(splitted[1]), 0]++;
                    }
                    iter++;
                }
            }
            FileHelper.saveMatrix(saveTo, vector);
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

        public static void saveStatsAprox(string filename, int matrixSize, double generationTime, double[] time) {
            // string header = "rozmiar;czas generacji;czas gaussa partial bez optymalizacji;czas gaussa partial z optymalizacja;czas gaussa seidel\n";
            string tmp = matrixSize + ";" + generationTime + ";";
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

        public static void saveStatsFromApproximations(string filename, int matrixSize, double[] time) {
            // string header = "rozmiar;czas gaussa partial bez optymalizacji;czas gaussa partial z optymalizacja;czas gaussa seidel;czas sparseLU;czas sparse seidel\n";
            string tmp = matrixSize + ";";
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
