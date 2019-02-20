using System;
using System.Diagnostics;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Grzybobranie {
    class Program {
        public static string inputData = "input.txt";
        public static string matrixOutput = "matrix.txt";
        public static string vectorOutput = "vector.txt";
        public static string infoOutput = "resultCS.txt";
        public static int monteCarloSimulations = 1000000;
        public static double epsilon = 0.000000000000001;
        public static Random rand = new Random();

        static void Main(string[] args) {
            info();
        }

        public static void info() {
            MyMatrix.epsilon = epsilon;
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("Epsilon : " + MyMatrix.epsilon);
            Data data = FileHelper.loadData(inputData);
            Console.WriteLine("WCZYTANO PLIK " + inputData + "\n");
            Console.WriteLine(data);
            Dice dice = new Dice(data);
            GameState first = new GameState(data);
            MonteCarlo monteCarlo = new MonteCarlo(first, dice);
            watch.Start();
            GameStateGenerator generator = new GameStateGenerator(first, dice);
            watch.Stop();
            Console.WriteLine("WYGENEROWANO " + generator.allStates.Count + " STANOW GRY");
            Console.WriteLine("W CZASIE : " + watch.ElapsedMilliseconds + "\n");
            MyMatrix matrix = generator.generateMatrix();
            MyMatrix vector = generator.generateVector();
            Console.WriteLine("ZAPISUJE WYGENEROWANĄ MACIERZ I WEKTOR...");
            FileHelper.saveMatrix(matrixOutput, matrix);
            FileHelper.saveMatrix(vectorOutput, vector);
            Console.WriteLine("SKONCZYLEM ZAPISYWAC...\n");
            countInfo(matrix, vector, monteCarlo);
        }

        public static void countInfo(MyMatrix matrix, MyMatrix vector, MonteCarlo monteCarlo) {
            double[] winChance = new double[4];
            double[] executionTime = new double[4];
            int[] iterations = new int[2];
            int size = matrix.rowCount;
            Stopwatch stopWatch = new Stopwatch();
            MyMatrix result;
            MyMatrix matrixTMP;
            MyMatrix vectorTMP;
            double monteCarloResult = monteCarlo.simulate(monteCarloSimulations);
            Console.WriteLine("-------------------------MONTE CARLO-------------------------");
            Console.WriteLine("Wynik : " + monteCarloResult);
            Console.WriteLine("-------------------------GAUSS PARTIAL BEZ OPTYMALIZACJI-------------------------");
            matrixTMP = MyMatrix.matrixJoinVector(matrix, vector);
            stopWatch.Start();
            result = MyMatrix.gauss(matrixTMP, 2, false);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            winChance[0] = result[0, 0];
            executionTime[0] = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("-------------------------GAUSS PARTIAL Z OPTYMALIZACJA-------------------------");
            matrixTMP = MyMatrix.matrixJoinVector(matrix, vector);
            stopWatch.Restart();
            result = MyMatrix.gauss(matrixTMP, 2, true);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            winChance[1] = result[0, 0];
            executionTime[1] = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("-------------------------GAUSS ITERACYJNY : JACOBI-------------------------");
            matrixTMP = new MyMatrix(matrix);
            vectorTMP = new MyMatrix(vector);
            stopWatch.Restart();
            result = MyMatrix.gaussIterative(matrixTMP, vectorTMP, 1, out iterations[0]);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Ilosc iteracji : " + iterations[0]);
            winChance[2] = result[0, 0];
            executionTime[2] = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("-------------------------GAUSS ITERACYJNY :  SEIDEL-------------------------");
            matrixTMP = new MyMatrix(matrix);
            vectorTMP = new MyMatrix(vector);
            stopWatch.Restart();
            result = MyMatrix.gaussIterative(matrixTMP, vectorTMP, 2, out iterations[1]);
            stopWatch.Stop();
            Console.WriteLine("Wynik : " + result[0, 0]);
            Console.WriteLine("Czas : " + stopWatch.ElapsedMilliseconds);
            Console.WriteLine("Ilosc iteracji : " + iterations[1]);
            winChance[3] = result[0, 0];
            executionTime[3] = stopWatch.ElapsedMilliseconds;
            FileHelper.saveStats(infoOutput, size, monteCarloResult, iterations, winChance, executionTime);
        }
    }
}
