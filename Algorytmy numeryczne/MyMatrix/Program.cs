using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Numerics;
using System.IO;
using System.Diagnostics;

//Monika Barzowska i Jan Bienias
//238143 238201
//Informatyka III gr 1
//Zad 2

namespace Macierze {
    class Program {
        public static string genDir = "Generated";
        public static string valuesDir = "Values";
        public static string gaussErrDir = "Generated\\GaussErrors";
        public static int countOfVersions = 5;
        public static int countOfMatrices = 3;
        static void Main(string[] args) {
            //using (new OutToFile("log.txt")) {
             getInfo();
            //}

        }
        public static void getInfo() {
            Console.WriteLine("GETTING INFORMATION :" + DateTime.Now.ToString());
            int size = retrieveSize("size.txt");
            double[,] sumOperationTimes = new double[3, 6];
            double[,] sumOperationErrors = new double[3, 6];
            double[] sumEigenOperationTimes = new double[5];
            string[] avgEigenOperationErrors = new string[2];
            avgEigenOperationErrors[0] = retrieveError(gaussErrDir + "\\" + "GaussPartialError.txt");
            avgEigenOperationErrors[1] = retrieveError(gaussErrDir + "\\" + "GaussFullError.txt");

            for (int c = 0; c < countOfVersions; c++) {
                double[,] operationTimes = new double[3, 6];
                double[,] operationErrors = new double[3, 6];
                double[] eigenOperationTimes = new double[5];
                double[] eigenOperationErrors = new double[2];
                string filePath = genDir + "\\" + valuesDir + (c + 1) + "\\";
                Console.WriteLine("\n\n\nOpening directory : " + filePath);
                MyMatrix<double>[] matricesDouble = new MyMatrix<double>[countOfMatrices];
                MyMatrix<double> vectorDouble = new MyMatrix<double>(convertToDouble(loadMatrix(filePath + "vector.txt", size, 1)));
                MyMatrix<float>[] matricesFloat = new MyMatrix<float>[countOfMatrices];
                MyMatrix<float> vectorFloat = new MyMatrix<float>(convertToFloat(loadMatrix(filePath + "vector.txt", size, 1)));
                MyMatrix<Fraction>[] matricesFraction = new MyMatrix<Fraction>[countOfMatrices];
                MyMatrix<Fraction> vectorFraction = new MyMatrix<Fraction>(convertToFraction(loadMatrix(filePath + "vector.txt", size, 1)));
                for (int i = 0; i < countOfMatrices; i++) {
                    matricesDouble[i] = new MyMatrix<double>(convertToDouble(loadMatrix(filePath + "matrix" + (i + 1) + ".txt", size, size)));
                    matricesFloat[i] = new MyMatrix<float>(convertToFloat(loadMatrix(filePath + "matrix" + (i + 1) + ".txt", size, size)));
                    matricesFraction[i] = new MyMatrix<Fraction>(convertToFraction(loadMatrix(filePath + "matrix" + (i + 1) + ".txt", size, size)));
                }
                MyMatrix<Fraction> AXeigenFraction = new MyMatrix<Fraction>(convertToFraction(loadMatrix(filePath + "AX.txt", size, 1)));
                MyMatrix<Fraction> ABCXeigenFraction = new MyMatrix<Fraction>(convertToFraction(loadMatrix(filePath + "ABCX.txt", size, 1)));
                MyMatrix<Fraction> ABCeigenFraction = new MyMatrix<Fraction>(convertToFraction(loadMatrix(filePath + "ABC.txt", size, size)));
                eigenOperationTimes[0] = retrieveTime(filePath + "AX.txt");
                eigenOperationTimes[1] = retrieveTime(filePath + "ABCX.txt");
                eigenOperationTimes[2] = retrieveTime(filePath + "ABC.txt");
                eigenOperationTimes[3] = retrieveTime(filePath + "partial.txt");
                eigenOperationTimes[4] = retrieveTime(filePath + "full.txt");
                Console.WriteLine("\nOPERATIONS OF MATRICES\n");
                //-----------------------------------------------AX-----------------------------------------------
                Console.WriteLine("A * X\n");
                //FLOAT
                Stopwatch stopWatch = new Stopwatch();
                MyMatrix<float> AXfloat;
                stopWatch.Start();
                AXfloat = matricesFloat[0] * vectorFloat;
                stopWatch.Stop();
                operationTimes[0, 0] = stopWatch.ElapsedMilliseconds;
                Fraction AXfloatError = AXfloat.countDiffWithFrac(AXeigenFraction);
                operationErrors[0, 0] = AXfloatError.ToDouble();
                Console.WriteLine("A * X FLOAT : ");
                Console.WriteLine("err : " + AXfloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 0] + " ms");
                Console.WriteLine("----------------------------");
                //DOUBLE
                MyMatrix<double> AXdouble;
                stopWatch.Restart();
                AXdouble = matricesDouble[0] * vectorDouble;
                stopWatch.Stop();
                operationTimes[1, 0] = stopWatch.ElapsedMilliseconds;
                Fraction AXdoubleError = AXdouble.countDiffWithFrac(AXeigenFraction);
                operationErrors[1, 0] = AXdoubleError.ToDouble();
                Console.WriteLine("A * X DOUBLE : ");
                Console.WriteLine("err : " + AXdoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 0] + " ms");
                Console.WriteLine("----------------------------");
                //FRACTION
                //MyMatrix<Fraction> AXfraction;
                //stopWatch.Restart();
                //AXfraction = matricesFraction[0] * vectorFraction;
                //stopWatch.Stop();
                //operationTimes[2, 0] = stopWatch.ElapsedMilliseconds;
                //Fraction AXfractionError = AXfraction.countDiffWithFrac(AXeigenFraction)
                //operationErrors[2, 0] = AXfractionError.ToDouble();
                //Console.WriteLine("A * X FRACTION : ");
                //Console.WriteLine("err : " + AXfractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 0] + " ms");
                //Console.WriteLine("----------------------------");
                //-----------------------------------------------(A+B+C) * X-----------------------------------------------
                Console.WriteLine("(A+B+C) * X\n");
                //FLOAT
                MyMatrix<float> ABCXfloat;
                stopWatch.Restart();
                ABCXfloat = (matricesFloat[0] + matricesFloat[1] + matricesFloat[2]) * vectorFloat;
                stopWatch.Stop();
                operationTimes[0, 1] = stopWatch.ElapsedMilliseconds;
                Fraction ABCXfloatError = ABCXfloat.countDiffWithFrac(ABCXeigenFraction);
                operationErrors[0, 1] = ABCXfloatError.ToDouble();
                Console.WriteLine("(A+B+C) * X FLOAT : ");
                Console.WriteLine("err : " + ABCXfloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 1] + " ms");
                Console.WriteLine("----------------------------");
                //DOUBLE
                MyMatrix<double> ABCXdouble;
                stopWatch.Restart();
                ABCXdouble = (matricesDouble[0] + matricesDouble[1] + matricesDouble[2]) * vectorDouble;
                stopWatch.Stop();
                operationTimes[1, 1] = stopWatch.ElapsedMilliseconds;
                Fraction ABCXdoubleError = ABCXdouble.countDiffWithFrac(ABCXeigenFraction);
                operationErrors[1, 1] = ABCXdoubleError.ToDouble();
                Console.WriteLine("(A+B+C) * X DOUBLE : ");
                Console.WriteLine("err : " + ABCXdoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 1] + " ms");
                Console.WriteLine("----------------------------");
                //FRACTION
                //MyMatrix<Fraction> ABCXfraction;
                //stopWatch.Restart();
                //ABCXfraction = (matricesFraction[0] + matricesFraction[1] + matricesFraction[2]) * vectorFraction;
                //stopWatch.Stop();
                //operationTimes[2, 1] = stopWatch.ElapsedMilliseconds;
                //Fraction ABCXfractionError = ABCXfraction.countDiffWithFrac(ABCXeigenFraction);
                //operationErrors[2, 1] = ABCXfractionError.ToDouble();
                //Console.WriteLine("(A+B+C) * X FRACTION : ");
                //Console.WriteLine("err : " + ABCXfractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 1] + " ms");
                //Console.WriteLine("----------------------------");

                //-----------------------------------------------A * B * C-----------------------------------------------
                Console.WriteLine("A * B * C\n");
                //FLOAT
                MyMatrix<float> ABCfloat;
                stopWatch.Restart();
                ABCfloat = matricesFloat[0] * matricesFloat[1] * matricesFloat[2];
                stopWatch.Stop();
                operationTimes[0, 2] = stopWatch.ElapsedMilliseconds;
                Fraction ABCfloatError = ABCfloat.countDiffWithFrac(ABCeigenFraction);
                operationErrors[0, 2] = ABCfloatError.ToDouble();
                Console.WriteLine("A * B * C FLOAT : ");
                Console.WriteLine("err : " + ABCfloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 2] + " ms");
                Console.WriteLine("----------------------------");
                //DOUBLE
                MyMatrix<double> ABCdouble;
                stopWatch.Restart();
                ABCdouble = matricesDouble[0] * matricesDouble[1] * matricesDouble[2];
                stopWatch.Stop();
                operationTimes[1, 2] = stopWatch.ElapsedMilliseconds;
                Fraction ABCdoubleError = ABCdouble.countDiffWithFrac(ABCeigenFraction);
                operationErrors[1, 2] = ABCdoubleError.ToDouble();
                Console.WriteLine("A * B * C DOUBLE : ");
                Console.WriteLine("err : " + ABCdoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 2] + " ms");
                Console.WriteLine("----------------------------");
                //FRACTION
                //MyMatrix<Fraction> ABCfraction;
                //stopWatch.Restart();
                //ABCfraction = matricesFraction[0] * matricesFraction[1] * matricesFraction[2];
                //stopWatch.Stop();
                //operationTimes[2, 2] = stopWatch.ElapsedMilliseconds;
                //Fraction ABCfractionError = ABCfraction.countDiffWithFrac(ABCeigenFraction);
                //operationErrors[2, 2] = ABCfractionError.ToDouble();
                //Console.WriteLine("A * B * C FRACTION : ");
                //Console.WriteLine("err : " + ABCfractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 2] + " ms");
                //Console.WriteLine("----------------------------");


                //END OF MATRICES OPERATIONS...
                MyMatrix<float> somewhatVectorFloat;
                MyMatrix<double> somewhatVectorDouble;
                MyMatrix<Fraction> somewhatVectorFraction;

                Console.WriteLine("\nGAUSS ELIMINATIONS\n");

                //-----------------------------------------------GAUSS BASE-----------------------------------------------
                Console.WriteLine("GAUSS BASE\n");
                //FLOAT
                MyMatrix<float> gaussBaseResultFloat;
                MyMatrix<float> ABForGaussBaseFloat = MyMatrix<float>.matrixJoinVector(matricesFloat[0], vectorFloat);
                stopWatch.Restart();
                gaussBaseResultFloat = MyMatrix<float>.gauss(ABForGaussBaseFloat, 1);
                stopWatch.Stop();
                operationTimes[0, 3] = stopWatch.ElapsedMilliseconds;
                somewhatVectorFloat = matricesFloat[0] * gaussBaseResultFloat;
                Fraction gaussBaseFloatError = somewhatVectorFloat.countDiffWithSameType(vectorFloat);
                operationErrors[0, 3] = gaussBaseFloatError.ToDouble();
                Console.WriteLine("GAUSS BASE FLOAT : ");
                Console.WriteLine("err : " + gaussBaseFloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 3] + " ms");
                Console.WriteLine("----------------------------");

                //DOUBLE
                MyMatrix<double> gaussBaseResultDouble;
                MyMatrix<double> ABForGaussBaseDouble = MyMatrix<double>.matrixJoinVector(matricesDouble[0], vectorDouble);
                stopWatch.Restart();
                gaussBaseResultDouble = MyMatrix<double>.gauss(ABForGaussBaseDouble, 1);
                stopWatch.Stop();
                operationTimes[1, 3] = stopWatch.ElapsedMilliseconds;
                somewhatVectorDouble = matricesDouble[0] * gaussBaseResultDouble;
                Fraction gaussBaseDoubleError = somewhatVectorDouble.countDiffWithSameType(vectorDouble);
                operationErrors[1, 3] = gaussBaseDoubleError.ToDouble();
                Console.WriteLine("GAUSS BASE DOUBLE : ");
                Console.WriteLine("err : " + gaussBaseDoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 3] + " ms");
                Console.WriteLine("----------------------------");

                //FRACTION
                //MyMatrix<Fraction> gaussBaseResultFraction;
                //MyMatrix<Fraction> ABForGaussBaseFraction = MyMatrix<Fraction>.matrixJoinVector(matricesFraction[0], vectorFraction);
                //stopWatch.Restart();
                //gaussBaseResultFraction = MyMatrix<Fraction>.gauss(ABForGaussBaseFraction, 1);
                //stopWatch.Stop();
                //operationTimes[2, 3] = stopWatch.ElapsedMilliseconds;
                //somewhatVectorFraction = matricesFraction[0] * gaussBaseResultFraction;
                //Fraction gaussBaseFractionError = somewhatVectorFraction.countDiffWithSameType(vectorFraction);
                //operationErrors[2, 3] = gaussBaseFractionError.ToDouble();
                //Console.WriteLine("GAUSS BASE FRACTION : ");
                //Console.WriteLine("err : " + gaussBaseFractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 3] + " ms");
                //Console.WriteLine("----------------------------");

                //-----------------------------------------------GAUSS PARTIAL-----------------------------------------------
                Console.WriteLine("GAUSS PARTIAL\n");
                //FLOAT
                MyMatrix<float> gaussPartialResultFloat;
                MyMatrix<float> ABForGaussPartialFloat = MyMatrix<float>.matrixJoinVector(matricesFloat[0], vectorFloat);
                stopWatch.Restart();
                gaussPartialResultFloat = MyMatrix<float>.gauss(ABForGaussPartialFloat, 2);
                stopWatch.Stop();
                operationTimes[0, 4] = stopWatch.ElapsedMilliseconds;
                somewhatVectorFloat = matricesFloat[0] * gaussPartialResultFloat;
                Fraction gaussPartialFloatError = somewhatVectorFloat.countDiffWithSameType(vectorFloat);
                operationErrors[0, 4] = gaussPartialFloatError.ToDouble();
                Console.WriteLine("GAUSS PARTIAL FLOAT : ");
                Console.WriteLine("err : " + gaussPartialFloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 4] + " ms");
                Console.WriteLine("----------------------------");

                //DOUBLE
                MyMatrix<double> gaussPartialResultDouble;
                MyMatrix<double> ABForGaussPartialDouble = MyMatrix<double>.matrixJoinVector(matricesDouble[0], vectorDouble);
                stopWatch.Restart();
                gaussPartialResultDouble = MyMatrix<double>.gauss(ABForGaussPartialDouble, 2);
                stopWatch.Stop();
                operationTimes[1, 4] = stopWatch.ElapsedMilliseconds;
                somewhatVectorDouble = matricesDouble[0] * gaussPartialResultDouble;
                Fraction gaussPartialDoubleError = somewhatVectorDouble.countDiffWithSameType(vectorDouble);
                operationErrors[1, 4] = gaussPartialDoubleError.ToDouble();
                Console.WriteLine("GAUSS PARTIAL DOUBLE : ");
                Console.WriteLine("err : " + gaussPartialDoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 4] + " ms");
                Console.WriteLine("----------------------------");

                //FRACTION
                //MyMatrix<Fraction> gaussPartialResultFraction;
                //MyMatrix<Fraction> ABForGaussPartialFraction = MyMatrix<Fraction>.matrixJoinVector(matricesFraction[0], vectorFraction);
                //stopWatch.Restart();
                //gaussPartialResultFraction = MyMatrix<Fraction>.gauss(ABForGaussPartialFraction, 2);
                //stopWatch.Stop();
                //operationTimes[2, 4] = stopWatch.ElapsedMilliseconds;
                //somewhatVectorFraction = matricesFraction[0] * gaussPartialResultFraction;
                //Fraction gaussPartialFractionError = somewhatVectorFraction.countDiffWithSameType(vectorFraction);
                //operationErrors[2, 4] = gaussPartialFractionError.ToDouble();
                //Console.WriteLine("GAUSS PARTIAL FRACTION : ");
                //Console.WriteLine("err : " + gaussPartialFractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 4] + " ms");
                //Console.WriteLine("----------------------------");

                //-----------------------------------------------GAUSS FULL-----------------------------------------------
                Console.WriteLine("GAUSS FULL\n");
                //FLOAT
                MyMatrix<float> gaussFullResultFloat;
                MyMatrix<float> ABForGaussFullFloat = MyMatrix<float>.matrixJoinVector(matricesFloat[0], vectorFloat);
                stopWatch.Restart();
                gaussFullResultFloat = MyMatrix<float>.gauss(ABForGaussFullFloat, 3);
                stopWatch.Stop();
                operationTimes[0, 5] = stopWatch.ElapsedMilliseconds;
                somewhatVectorFloat = matricesFloat[0] * gaussFullResultFloat;
                Fraction gaussFullFloatError = somewhatVectorFloat.countDiffWithSameType(vectorFloat);
                operationErrors[0, 5] = gaussFullFloatError.ToDouble();
                Console.WriteLine("GAUSS FULL FLOAT : ");
                Console.WriteLine("err : " + gaussFullFloatError.ToDouble());
                Console.WriteLine("time : " + operationTimes[0, 5] + " ms");
                Console.WriteLine("----------------------------");

                //DOUBLE
                MyMatrix<double> gaussFullResultDouble;
                MyMatrix<double> ABForGaussFullDouble = MyMatrix<double>.matrixJoinVector(matricesDouble[0], vectorDouble);
                stopWatch.Restart();
                gaussFullResultDouble = MyMatrix<double>.gauss(ABForGaussFullDouble, 3);
                stopWatch.Stop();
                operationTimes[1, 5] = stopWatch.ElapsedMilliseconds;
                somewhatVectorDouble = matricesDouble[0] * gaussFullResultDouble;
                Fraction gaussFullDoubleError = somewhatVectorDouble.countDiffWithSameType(vectorDouble);
                operationErrors[1, 5] = gaussFullDoubleError.ToDouble();
                Console.WriteLine("GAUSS FULL DOUBLE : ");
                Console.WriteLine("err : " + gaussFullDoubleError.ToDouble());
                Console.WriteLine("time : " + operationTimes[1, 5] + " ms");
                Console.WriteLine("----------------------------");

                //FRACTION
                //MyMatrix<Fraction> gaussFullResultFraction;
                //MyMatrix<Fraction> ABForGaussFullFraction = MyMatrix<Fraction>.matrixJoinVector(matricesFraction[0], vectorFraction);
                //stopWatch.Restart();
                //gaussFullResultFraction = MyMatrix<Fraction>.gauss(ABForGaussFullFraction,3);
                //stopWatch.Stop();
                //operationTimes[2, 5] = stopWatch.ElapsedMilliseconds;
                //somewhatVectorFraction = matricesFraction[0] * gaussFullResultFraction;
                //Fraction gaussFullFractionError = somewhatVectorFraction.countDiffWithSameType(vectorFraction);
                //operationErrors[2, 5] = gaussFullFractionError.ToDouble();
                //Console.WriteLine("GAUSS FULL FRACTION : ");
                //Console.WriteLine("err : " + gaussFullFractionError.ToDouble());
                //Console.WriteLine("time : " + operationTimes[2, 5] + " ms");
                //Console.WriteLine("----------------------------");

                //-----------------------------------------------GAUSS EIGEN-----------------------------------------------
                //EIGEN PARTIAL
                Console.WriteLine("GAUSS PARTIAL EIGEN : ");
                Console.WriteLine("avg err : " + avgEigenOperationErrors[0]);
                Console.WriteLine("time : " + eigenOperationTimes[3] + " sec");
                Console.WriteLine("----------------------------");
                //EIGEN FULL
                Console.WriteLine("GAUSS FULL EIGEN : ");
                Console.WriteLine("avg err : " + avgEigenOperationErrors[1]);
                Console.WriteLine("time : " + eigenOperationTimes[4] + " sec");
                Console.WriteLine("----------------------------");
                for (int i = 0; i < 3; i++) {
                    for (int j = 0; j < 6; j++) {
                        sumOperationTimes[i, j] += operationTimes[i, j];
                        sumOperationErrors[i, j] += operationErrors[i, j];
                    }
                }
                for (int i = 0; i < 5; i++) sumEigenOperationTimes[i] += eigenOperationTimes[i];
            }
            Console.WriteLine("\n\nSUCCESS : COUNTING AVERAGE VALUES FOR ERRORS AND TIMES FOR " + countOfVersions + " VERSIONS FOR SIZE " + size);

            double[,] avgOperationTimes = new double[3, 6];
            double[,] avgOperationErrors = new double[3, 6];
            double[] avgEigenOperationTimes = new double[5];
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 6; j++) {
                    avgOperationTimes[i, j] = sumOperationTimes[i, j] / countOfVersions;
                    avgOperationErrors[i, j] = sumOperationErrors[i, j] / countOfVersions;
                }
            }
            for (int i = 0; i < 5; i++) {
                avgEigenOperationTimes[i] = sumEigenOperationTimes[i] / countOfVersions;
            }

            string opAX = operationsOnMatrixInfo(size, 0, 0, avgOperationErrors, avgOperationTimes, avgEigenOperationTimes);
            string opABCX = operationsOnMatrixInfo(size, 1, 1, avgOperationErrors, avgOperationTimes, avgEigenOperationTimes);
            string opABC = operationsOnMatrixInfo(size, 2, 2, avgOperationErrors, avgOperationTimes, avgEigenOperationTimes);
            string opGaussBase = operationsOnGaussBaseInfo(size, 3, avgOperationErrors, avgOperationTimes);
            string opGaussPartial = operationsOnGaussFullPartialInfo(size, 4, 0, 3, avgOperationErrors, avgOperationTimes, avgEigenOperationTimes, avgEigenOperationErrors);
            string opGaussFull = operationsOnGaussFullPartialInfo(size, 5, 1, 4, avgOperationErrors, avgOperationTimes, avgEigenOperationTimes, avgEigenOperationErrors);
            if (!Directory.Exists("Results" + size)) {
                Console.WriteLine("Creating directory : Results" + size);
                DirectoryInfo di = Directory.CreateDirectory("Results" + size);
            }
            File.WriteAllText("Results" + size + "\\" + "AXresult" + size + ".txt", opAX);
            File.WriteAllText("Results" + size + "\\" + "ABCXresult" + size + ".txt", opABCX);
            File.WriteAllText("Results" + size + "\\" + "ABCresult" + size + ".txt", opABC);
            File.WriteAllText("Results" + size + "\\" + "GaussBaseResult" + size + ".txt", opGaussBase);
            File.WriteAllText("Results" + size + "\\" + "GaussPartialResult" + size + ".txt", opGaussPartial);
            File.WriteAllText("Results" + size + "\\" + "GaussFullResult" + size + ".txt", opGaussFull);
        }


        public static string operationsOnMatrixInfo(int size, int operation, int eigenOperation, double[,] operationErrors, double[,] operationTimes, double[] eigenOperationsTimes) {
            string s = "size;float_err;double_err;fraction_err;float_time;double_time;fraction_time;eigen_time" + "\n";
            string tmp = size + ";" + operationErrors[0, operation] + ";" + operationErrors[1, operation] + ";" + operationErrors[2, operation] + ";" +
                operationTimes[0, operation] + ";" + operationTimes[1, operation] + ";" + operationTimes[2, operation] + ";" + eigenOperationsTimes[eigenOperation];
            return s + tmp;
        }

        public static string operationsOnGaussFullPartialInfo(int size, int operation, int eigenError, int eigenTime, double[,] operationErrors, double[,] operationTimes, double[] eigenOperationTimes, string[] eigenOperationErrors) {
            string s = "size;float_err;double_err;fraction_err;eigen_err;float_time;double_time;fraction_time;eigen_time" + "\n";
            string tmp = size + ";" + operationErrors[0, operation] + ";" + operationErrors[1, operation] + ";" + operationErrors[2, operation] + ";" + eigenOperationErrors[eigenError] + ";" +
                operationTimes[0, operation] + ";" + operationTimes[1, operation] + ";" + operationTimes[2, operation] + ";" + eigenOperationTimes[eigenTime];
            return s + tmp;
        }

        public static string operationsOnGaussBaseInfo(int size, int operation, double[,] operationErrors, double[,] operationTimes) {
            string s = "size;float_err;double_err;fraction_err;float_time;double_time;fraction_time" + "\n";
            string tmp = size + ";" + operationErrors[0, operation] + ";" + operationErrors[1, operation] + ";" + operationErrors[2, operation] + ";" +
                operationTimes[0, operation] + ";" + operationTimes[1, operation] + ";" + operationTimes[2, operation];
            return s + tmp;
        }

        public static string[,] loadMatrix(string filename, int rows, int columns) {
            if (!File.Exists(filename)) {
                throw new FileNotFoundException("Could not open file " + filename + " !");
            }
            int i = 0, j = 0;
            string[,] result = new string[rows, columns];
            string input = File.ReadAllText(filename);
            foreach (var row in input.Split('\n')) {
                j = 0;
                foreach (var col in row.Trim().Split(' ')) {
                    if (i < rows) {
                        if (j < columns) {
                            result[i, j] = col.Trim();
                            result[i, j] = result[i, j].Replace('.', ',');
                        }
                        j++;
                    }

                }
                i++;
            }
            return result;
        }
        public static float[,] convertToFloat(string[,] str) {
            float[,] result = new float[str.GetLength(0), str.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++) {
                for (int j = 0; j < result.GetLength(1); j++) {
                    double tmp = Double.Parse(str[i, j]);
                    result[i, j] = (float)tmp;
                }
            }
            return result;
        }

        public static double[,] convertToDouble(string[,] str) {
            double[,] result = new double[str.GetLength(0), str.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++) {
                for (int j = 0; j < result.GetLength(1); j++) {
                    result[i, j] = Double.Parse(str[i, j]);
                }
            }
            return result;
        }

        public static Fraction[,] convertToFraction(string[,] str) {
            Fraction[,] result = new Fraction[str.GetLength(0), str.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++) {
                for (int j = 0; j < result.GetLength(1); j++) {
                    result[i, j] = new Fraction(str[i, j]);
                }
            }
            return result;
        }

        public static void printArray(string[,] str) {
            for (int i = 0; i < str.GetLength(0); i++) {
                for (int j = 0; j < str.GetLength(1); j++) {
                    Console.Write(str[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static string retrieveError(string filename) {
            if(!File.Exists(filename)) {
                throw new FileNotFoundException("File " + filename + " not found!");
            }
            string output = File.ReadLines(filename).First();
            return output;
        }

        public static int retrieveSize(string filename) {
            if (!File.Exists(filename)) {
                throw new FileNotFoundException("File " + filename + " not found!");
            }
            string output = File.ReadLines(filename).First();
            try {
                int tmp = Convert.ToInt32(output);
                if (tmp < 0) {
                    tmp = -tmp;
                }
                return tmp;
            } catch (Exception) {
                throw new Exception("There's no integer in file " + filename + " !");
            }
        }

        public static double retrieveTime(string filename) {
            if (!File.Exists(filename)) {
                throw new FileNotFoundException("File " + filename + " not found!");
            }
            string output = File.ReadAllLines(filename).Last();
            output = output.Replace('.', ',');
            double result = Double.Parse(output);
            return result;
        }
    }
}
