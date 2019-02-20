using System;
using System.Collections.Generic;

//Monika Barzowska, Jan Bienias
//238143, 238201


namespace Grzybobranie {
    public class MyMatrix {
        public static double epsilon;
        private double[,] matrix;

        public MyMatrix(int rows, int columns) {
            matrix = new double[rows, columns];
        }

        public MyMatrix(double[,] array) {
            matrix = new double[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    matrix[i, j] = array[i, j];
                }
            }
        }

        public MyMatrix(double[] array) {
            matrix = new double[array.GetLength(0), 1];
            for (int i = 0; i < array.GetLength(0); i++) {
                matrix[i, 0] = array[i];
            }
        }

        public MyMatrix(MyMatrix m) {
            matrix = new double[m.rowCount, m.columnCount];
            for (int i = 0; i < m.rowCount; i++) {
                for (int j = 0; j < m.columnCount; j++) {
                    matrix[i, j] = m[i, j];
                }
            }
        }

        public double this[int rowIndex, int columnIndex] {
            set { matrix[rowIndex, columnIndex] = value; }
            get { return matrix[rowIndex, columnIndex]; }
        }

        public int rowCount {
            get { return matrix.GetLength(0); }
        }

        public int columnCount {
            get { return matrix.GetLength(1); }
        }

        public static MyMatrix operator +(MyMatrix a, MyMatrix b) {
            if (a.rowCount != b.rowCount || a.columnCount != b.columnCount) {
                Console.WriteLine("Matrices sizes are incorrect for operation : a + b!");
                return null;
            }

            MyMatrix c = new MyMatrix(a.rowCount, a.columnCount);
            for (int i = 0; i < a.rowCount; i++) {
                for (int j = 0; j < a.columnCount; j++) {
                    c.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j];
                }
            }
            return c;
        }

        public static MyMatrix operator -(MyMatrix a, MyMatrix b) {
            if (a.rowCount != b.rowCount || a.columnCount != b.columnCount) {
                Console.WriteLine("Matrices sizes are incorrect for operation : a + b!");
                return null;
            }

            MyMatrix c = new MyMatrix(a.rowCount, a.columnCount);
            for (int i = 0; i < a.rowCount; i++) {
                for (int j = 0; j < a.columnCount; j++) {
                    c.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j];
                }
            }
            return c;
        }

        public static MyMatrix operator *(MyMatrix a, MyMatrix b) {
            if (a.columnCount != b.rowCount) {
                Console.WriteLine("Matrices sizes are incorrect for operation : a * b!");
                return null;
            }
            MyMatrix c = new MyMatrix(a.rowCount, b.columnCount);
            for (int i = 0; i < a.rowCount; i++) {
                for (int j = 0; j < b.columnCount; j++) {
                    c.matrix[i, j] = default(double);
                    for (int k = 0; k < b.rowCount; k++) {
                        c.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j];
                    }
                }
            }
            return c;
        }


        public static MyMatrix gauss(MyMatrix AB, int version, bool optimise) {
            if (AB.rowCount != AB.columnCount - 1)
                throw new Exception("Matrix N x (N+1) is required for Gaussian Eliminations!");
            int n = AB.rowCount;
            for (int i = 0; i < n; i++) {
                double max = Math.Abs(AB[i, i]);
                int maxRow = i;
                for (int k = i + 1; k < n; k++) {
                    if (Math.Abs(AB[k, i]) > max) {
                        max = Math.Abs(AB[k, i]);
                        maxRow = k;
                    }
                }
                for (int k = i; k < n + 1; k++) {
                    double tmp = AB[maxRow, k];
                    AB[maxRow, k] = AB[i, k];
                    AB[i, k] = tmp;
                }

                for (int k = i + 1; k < n; k++) {
                    double c = (default(double) - AB[k, i]) / AB[i, i];
                    if (optimise == true && c == default(double))
                        continue;
                    for (int j = i; j < n + 1; j++) {
                        if (i == j)
                            AB[k, j] = default(double);
                        else
                            AB[k, j] += c * AB[i, j];
                    }
                }
            }
            MyMatrix X = countVariables(AB);
            return X;
        }

        public static MyMatrix gaussIterative(MyMatrix A, MyMatrix B, int version, out int saver) {
            int n = A.rowCount;
            if (A.rowCount != B.rowCount || A.rowCount != A.columnCount) {
                throw new Exception("Matrix A must be n*n! A rowCount must be equal B rowCount!");
            }
            if (version == 1) { //JACOBI
                MyMatrix X_NEW = new MyMatrix(n, 1);
                MyMatrix X_OLD = new MyMatrix(n, 1);
                double sum = 0.0;
                int iter = 0;
                while(true) {
                    for (int i = 0; i < n; i++) {
                        for (int j = 0; j < n; j++) {
                            if (j != i) sum -= A[i, j] * X_OLD[j, 0];
                        }
                        if (A[i, i] == 0.0) {
                            int row = findBiggestRowInColumn(A, i);
                            swapRows(A, i, row);
                            swapRows(B, i, row);
                        }
                        X_NEW[i, 0] = (B[i, 0] + sum) / A[i, i];
                        sum = 0.0;
                    }
                    double norm1 = (B - (A * X_NEW)).countNorm();
                    double norm2 = B.countNorm();
                    if ((norm1 / norm2) < epsilon) { break; }
                    for (int i = 0; i < n; i++) {
                        X_OLD[i, 0] = X_NEW[i, 0];
                    }
                    iter++;
                }
                saver = iter;
                return X_NEW;
            } else if (version == 2) { //SEIDEL
                MyMatrix X = new MyMatrix(n, 1);
                double sum = 0.0;
                int iter = 0;
                while (true) {
                    for (int i = 0; i < n; i++) {
                        for (int j = 0; j < i; j++) {
                            sum -= A[i, j] * X[j, 0];
                        }
                        for (int j = i + 1; j < n; j++) {
                            sum -= A[i, j] * X[j, 0];
                        }
                        if (A[i, i] == 0.0) {
                            int row = findBiggestRowInColumn(A, i);
                            swapRows(A, i, row);
                            swapRows(B, i, row);
                        }
                        X[i, 0] = (B[i, 0] + sum) / A[i, i];
                        sum = 0.0;
                    }
                    double norm1 = (B - (A * X)).countNorm();
                    double norm2 = B.countNorm();
                    if (norm1 / norm2 < epsilon) { break; }
                    iter++;
                }
                saver = iter;
                return X;

            } else {
                throw new Exception("Unknown Gauss elimination version! 1 - base, 2 - partial, 3 - full");
            }
        }

        private static MyMatrix countVariables(MyMatrix AB) {
            int n = AB.rowCount;
            MyMatrix X = new MyMatrix(AB.rowCount, 1);
            for (int i = n - 1; i >= 0; i--) {
                X[i, 0] = AB[i, n] / AB[i, i];
                for (int k = i - 1; k >= 0; k--) {
                    AB[k, n] -= AB[k, i] * X[i, 0];
                }
            }
            return X;
        }

        private static void findBiggestValue(MyMatrix AB, int index, List<int> queue) {
            double max = AB[index, index];
            int rowIndex = index;
            int columnIndex = index;
            for (int i = index; i < AB.rowCount; i++) {
                for (int j = index; j < AB.columnCount - 1; j++) {
                    if (Math.Abs(AB[i, j]) > max) {
                        max = Math.Abs(AB[i, j]);
                        rowIndex = i;
                        columnIndex = j;
                    }
                }
            }
            swapRows(AB, index, rowIndex);
            swapColumns(AB, index, columnIndex, queue);
        }

        private double countNorm() {
            if (this.columnCount > 1)
                throw new Exception("countNorm() is implemented only for vectors (ATM)");
            double result = 0;
            for (int i = 0; i < this.rowCount; i++) {
                result += Math.Pow(this.matrix[i, 0], 2);
            }
            return Math.Sqrt(result);

        }

        private static int findBiggestRowInColumn(MyMatrix matrix, int column) {
            double max = 0.0;
            int row = 0;
            for (int i = 0; i < matrix.rowCount; i++) {
                if (matrix[i, column] > max) {
                    max = matrix[i, column];
                    row = i;
                }
            }
            return row;
        }

        private static void swapRows(MyMatrix matrix, int row1, int row2) {
            if (row1 == row2) {
                return;
            }
            for (int i = 0; i < matrix.columnCount; i++) {
                double tmp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = tmp;
            }
            return;
        }

        private static void swapColumns(MyMatrix matrix, int column1, int column2, List<int> queue) {
            if (column1 == column2) {
                return;
            }
            int tmp = queue[column1];
            queue[column1] = queue[column2];
            queue[column2] = tmp;
            for (int i = 0; i < matrix.rowCount; i++) {
                double Tmp = matrix[i, column1];
                matrix[i, column1] = matrix[i, column2];
                matrix[i, column2] = Tmp;
            }
            return;
        }

        private MyMatrix resultByQueue(MyMatrix vector, List<int> queue) {
            MyMatrix tmp = new MyMatrix(vector.rowCount, 1);
            for (int i = 0; i < vector.rowCount; i++) {
                for (int j = 0; j < vector.columnCount; j++) {
                    tmp[i, j] = vector[i, j];
                }
            }
            for (int i = 0; i < vector.rowCount; i++) {
                vector[queue[i], 0] = tmp[i, 0];
            }
            return vector;
        }

        public void fillDiagonal(int value) {
            if (rowCount != columnCount) {
                throw new Exception("Wrong matrix size for filling diagonally! Must be n*n!");
            }
            for (int i = 0; i < rowCount; i++) {
                matrix[i, i] = value;
            }
        }

        public static MyMatrix matrixJoinVector(MyMatrix A, MyMatrix B) {
            if (A.rowCount != B.rowCount) {
                throw new Exception("Wrong Matrixes size!");
            }
            if (B.columnCount != 1) {
                throw new Exception("Wrong Vector columnCount!");
            }
            MyMatrix result = new MyMatrix(A.rowCount, A.columnCount + 1);
            int j = 0;
            for (int i = 0; i < A.rowCount; i++) {
                for (j = 0; j < A.columnCount; j++) {
                    result[i, j] = A[i, j];
                }
            }

            for (int i = 0; i < A.rowCount; i++) {
                result[i, j] = B[i, 0];
            }
            return result;
        }

        public override string ToString() {
            string output = "";
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {

                    if (j == columnCount - 1) {
                        output += matrix[i, j];
                    } else
                        output += matrix[i, j] + " ";
                }
                if (i != rowCount - 1) {
                    output += '\n';
                }
            }
            return output;
        }
    }
}
