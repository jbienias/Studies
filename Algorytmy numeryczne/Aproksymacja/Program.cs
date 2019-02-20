using System;
using System.Diagnostics;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Aproksymacja {
    class Program {
        public static string inputSize = "size.txt";
        public static string inputData = "input.txt";
        public static string matrixOutput = "matrix.txt";
        public static string vectorOutput = "vector.txt";
        public static string vectorNonZeroOutput = "vectorNonZero.txt";
        public static string infoOutput = "resultCS.txt";
        public static string infoApproxOutput = "resultApprox.txt";
        public static string gaussPartial = "data/gaussPartial.txt";
        public static string gaussPartialOpt = "data/gaussPartialOpt.txt";
        public static string gaussSeidel = "data/gaussSeidel.txt";
        public static string gaussSparseEigen = "data/gaussSparseEigen.txt";
        public static string gaussSparseSeidelEigen = "data/gaussSparseEigenSeidel.txt";
        public static double epsilon = 0.0000000001;
        public static Random rand = new Random();

        static void Main(string[] args) {
            if (args.Length >= 1) {
                Console.WriteLine(args[0]);
                if (args[0] == "info") {
                    info();
                } else if (args[0] == "approx") {
                    approx();
                } else if (args[0] == "generate") {
                    generateOnly();
                } else {
                    Console.WriteLine("Argument " + args[0] + " is unknown.");
                }
            } else {
                Console.WriteLine("Program potrzebuje argumentu!");
                Console.WriteLine("Kliknij aby zakończyć program...");
                Console.ReadKey();
            }
            Console.WriteLine("\nKoniec.\n");
            return;
        }

        public static void approx() {
            int[] size = FileHelper.loadSizes(inputSize);
            MyMatrix vector;
            double[,] partial = FileHelper.loadDataForAprox(gaussPartial);
            double[,] partialOpt = FileHelper.loadDataForAprox(gaussPartialOpt);
            double[,] seidel = FileHelper.loadDataForAprox(gaussSeidel);
            double[,] sparse = FileHelper.loadDataForAprox(gaussSparseEigen);
            double[,] sparseSeidel = FileHelper.loadDataForAprox(gaussSparseSeidelEigen);
            double[] results = new double[5];
            for (int i = 0; i < size.Length; i++) {
                Console.WriteLine("\n\n Size : " + size[i] + "\n");
                Console.WriteLine("-------------------------GAUSS PARTIAL BEZ OPTYMALIZACJI-------------------------");
                vector = Approximation.CountEquation(partial, 3);     //Gauss Partial
                results[0] = Approximation.CountVariable(vector, size[i]);
                Console.WriteLine("\nWynik : " + results[0] + "\n");
                Console.WriteLine("-------------------------GAUSS PARTIAL Z OPTYMALIZACJA-------------------------");
                vector = Approximation.CountEquation(partialOpt, 2);  //Gaus Partial Opt
                results[1] = Approximation.CountVariable(vector, size[i]);
                Console.WriteLine("\nWynik : " + results[1] + "\n");
                Console.WriteLine("-------------------------GAUSS ITERACYJNY : SEIDEL-------------------------");
                vector = Approximation.CountEquation(seidel, 2);      //Gauss Seidel
                results[2] = Approximation.CountVariable(vector, size[i]);
                Console.WriteLine("\nWynik : " + results[2] + "\n");
                Console.WriteLine("-------------------------GAUSS SPARSE LU EIGEN-------------------------");
                vector = Approximation.CountEquation(sparse, 1);      //SparseLU Eigen
                results[3] = Approximation.CountVariable(vector, size[i]);
                Console.WriteLine("\nWynik : " + results[3] + "\n");
                Console.WriteLine("-------------------------GAUSS SPARSE SEIDEL EIGEN-------------------------");
                vector = Approximation.CountEquation(sparseSeidel, 1);//Sparse Seidel Eigen
                results[4] = Approximation.CountVariable(vector, size[i]);
                Console.WriteLine("\nWynik : " + results[4] + "\n");
                FileHelper.saveStatsFromApproximations(infoApproxOutput, size[i], results);
                Console.ReadKey();
            }
        }

        public static void generateOnly() {
            Data data = FileHelper.loadData(inputData);
            Console.WriteLine("WCZYTANO PLIK " + inputData + "\n");
            Console.WriteLine(data);
            Dice dice = new Dice(data);
            GameState first = new GameState(data);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("GENERUJE STANY WYGRANYCH i PRZEGRANYCH :");
            GameStateGenerator generator = new GameStateGenerator(first, dice);
            sw.Stop();
            Console.WriteLine("WYGENEROWANO " + generator.allStates.Count + " STANOW GRY");
            Console.WriteLine("W CZASIE : " + sw.ElapsedMilliseconds);
            Console.WriteLine("ZAPISUJE WYGENEROWANĄ MACIERZ I WEKTOR...");
            generator.generateMatrixToFile(matrixOutput);
            Console.WriteLine("SKONCZYLEM ZAPISYWAC MACIERZ");
            MyMatrix vector = generator.generateVector();
            FileHelper.saveMatrix(vectorOutput, vector);
            Console.WriteLine("SKOCZYLEM ZAPISYWAC WEKTOR");
            FileHelper.countAndSaveVectorOfNonZeroElements(matrixOutput, vectorNonZeroOutput);
            Console.WriteLine("SKONCZYLEM ZAPISYWAC WEKTOR NON ZERO");
        }

        public static void info() {
            MyMatrix.epsilon = epsilon;
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("EPSILON : " + MyMatrix.epsilon);
            Data data = FileHelper.loadData(inputData);
            Console.WriteLine("WCZYTANO PLIK " + inputData + "\n");
            Console.WriteLine(data);
            Dice dice = new Dice(data);
            GameState first = new GameState(data);
            watch.Start();
            GameStateGenerator generator = new GameStateGenerator(first, dice);
            MyMatrix matrix = generator.generateMatrix();
            MyMatrix vector = generator.generateVector();
            watch.Stop();
            Console.WriteLine("WYGENEROWANO " + generator.allStates.Count + " STANOW GRY");
            Console.WriteLine("W CZASIE : " + watch.ElapsedMilliseconds + "\n");
            double generationTime = watch.ElapsedMilliseconds;
            Console.WriteLine("ZAPISUJE WYGENEROWANĄ MACIERZ I WEKTOR...");
            FileHelper.saveMatrixSparse(matrixOutput, matrix);
            FileHelper.saveMatrix(vectorOutput, vector);
            FileHelper.saveVectorOfNonZeroElements(vectorNonZeroOutput, matrix);
            Console.WriteLine("SKONCZYLEM ZAPISYWAC...\n");
            countInfo(matrix, vector, generationTime);
        }

        public static void countInfo(MyMatrix matrix, MyMatrix vector, double generationTime) {
            double[] executionTime = new double[3];
            int size = matrix.rowCount;
            Stopwatch stopWatch = new Stopwatch();
            MyMatrix result;
            MyMatrix matrixTMP;
            MyMatrix vectorTMP;
            Console.WriteLine("-------------------------GAUSS PARTIAL BEZ OPTYMALIZACJI-------------------------");
            matrixTMP = MyMatrix.matrixJoinVector(matrix, vector);
            stopWatch.Start();
            result = MyMatrix.gauss(matrixTMP, false);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            executionTime[0] = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("-------------------------GAUSS PARTIAL Z OPTYMALIZACJA-------------------------");
            matrixTMP = MyMatrix.matrixJoinVector(matrix, vector);
            stopWatch.Restart();
            result = MyMatrix.gauss(matrixTMP, true);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            executionTime[1] = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("-------------------------GAUSS ITERACYJNY : SEIDEL-------------------------");
            matrixTMP = new MyMatrix(matrix);
            vectorTMP = new MyMatrix(vector);
            stopWatch.Restart();
            result = MyMatrix.gaussSeidel(matrixTMP, vectorTMP);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            executionTime[2] = stopWatch.ElapsedMilliseconds;
            FileHelper.saveStatsAprox(infoOutput, size, generationTime, executionTime);
        }
    }
}
