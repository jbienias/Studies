using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Monika Barzowska i Jan Bienias
//238143 238201
//Informatyka III gr 1
//Zad 2

namespace Macierze {
    public class MyMatrix<T> {
        private T[,] matrix;

        public MyMatrix(int rows, int columns) {
            matrix = new T[rows, columns];
        }

        public MyMatrix(T[,] array) {
            matrix = new T[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++) {
                for (int j = 0; j < array.GetLength(1); j++) {
                    matrix[i, j] = array[i, j];
                }
            }
        }

        public T this[int rowIndex, int columnIndex] {
            set { matrix[rowIndex, columnIndex] = value; }
            get { return matrix[rowIndex, columnIndex]; }
        }

        public int rowCount {
            get { return matrix.GetLength(0); }
        }

        public int columnCount {
            get { return matrix.GetLength(1); }
        }

        public static MyMatrix<T> operator +(MyMatrix<T> a, MyMatrix<T> b) {
            if (a.rowCount != b.rowCount || a.columnCount != b.columnCount) {
                Console.WriteLine("Matrices sizes are incorrect for operation : a + b!");
                return null;
            }

            MyMatrix<T> c = new MyMatrix<T>(a.rowCount, a.columnCount);
            for (int i = 0; i < a.rowCount; i++) {
                for (int j = 0; j < a.columnCount; j++) {
                    c.matrix[i, j] = (dynamic)a.matrix[i, j] + (dynamic)b.matrix[i, j];
                }
            }
            return c;
        }

        public static MyMatrix<T> operator *(MyMatrix<T> a, MyMatrix<T> b) {
            if (a.columnCount != b.rowCount) {
                Console.WriteLine("Matrices sizes are incorrect for operation : a * b!");
                return null;
            }
            MyMatrix<T> c = new MyMatrix<T>(a.rowCount, b.columnCount);
            for (int i = 0; i < a.rowCount; i++) {
                for (int j = 0; j < b.columnCount; j++) {
                    c.matrix[i, j] = default(T);
                    for (int k = 0; k < b.rowCount; k++) {
                        c.matrix[i, j] += (dynamic)a.matrix[i, k] * (dynamic)b.matrix[k, j];
                    }
                }
            }
            return c;
        }


        public static MyMatrix<T> gauss(MyMatrix<T> AB, int version) {
            if (AB.rowCount != AB.columnCount - 1)
                throw new Exception("Matrix N x (N+1) is required for Gaussian Eliminations!");
            int n = AB.rowCount;
            List<int> queue = new List<int>();
            if (version == 1) { //base
                for (int i = 0; i < n; i++) {
                    for (int k = i + 1; k < n; k++) {
                        T c = (dynamic)((dynamic)default(T) - AB[k, i]) / AB[i, i];
                        for (int j = i; j < n + 1; j++) {
                            if (i == j)
                                AB[k, j] = default(T);
                            else
                                AB[k, j] += c * (dynamic)AB[i, j];
                        }
                    }
                }
            }
            else if (version == 2) { //partional
                for (int i = 0; i < n; i++) {
                    T max = ABS(AB[i, i]);
                    int maxRow = i;
                    for (int k = i + 1; k < n; k++) {
                        if ((dynamic)ABS(AB[k, i]) > max) {
                            max = ABS(AB[k, i]);
                            maxRow = k;
                        }
                    }
                    for (int k = i; k < n + 1; k++) {
                        T tmp = (dynamic)AB[maxRow, k];
                        AB[maxRow, k] = AB[i, k];
                        AB[i, k] = tmp;
                    }

                    for (int k = i + 1; k < n; k++) {
                        T c = ((dynamic)default(T) - AB[k, i]) / AB[i, i];
                        for (int j = i; j < n + 1; j++) {
                            if (i == j)
                                AB[k, j] = default(T);
                            else
                                AB[k, j] += (dynamic)c * AB[i, j];
                        }
                    }
                }
            }
            else if (version == 3) { //full
                for (int i = 0; i < n; i++) {
                    queue.Add(i);
                }
                for (int i = 0; i < n; i++) {
                    findBiggestValue(AB, i, queue);
                    for (int k = i + 1; k < n; k++) {
                        T c = ((dynamic)default(T) - AB[k, i]) / AB[i, i];
                        for (int j = i; j < n + 1; j++) {
                            if (i == j)
                                AB[k, j] = default(T);
                            else
                                AB[k, j] += (dynamic)c * AB[i, j];
                        }
                    }
                }
            }
            else {
                throw new Exception("Unknown Gauss elimination version! 1 - base, 2 - partial, 3 - full");
            }
            MyMatrix<T> X = countVariables(AB);
            if (version == 3) X.resultByQueue(X, queue);
            return X;
        }

        private static MyMatrix<T> countVariables(MyMatrix<T> AB) {
            int n = AB.rowCount;
            MyMatrix<T> X = new MyMatrix<T>(AB.rowCount, 1);
            for (int i = n - 1; i >= 0; i--) {
                X[i, 0] = (dynamic)AB[i, n] / AB[i, i];
                for (int k = i - 1; k >= 0; k--) {
                    AB[k, n] -= (dynamic)AB[k, i] * X[i, 0];
                }
            }
            return X;
        }

        private static void findBiggestValue(MyMatrix<T> AB, int index, List<int> queue) {
            //size :  n x (n+1) / 'AB' Matrix
            T max = AB[index, index];
            int rowIndex = index;
            int columnIndex = index;
            for (int i = index; i < AB.rowCount; i++) {
                for (int j = index; j < AB.columnCount - 1; j++) { //columnCount - 1 , to only look at nxn matrix
                    if ((dynamic)ABS(AB[i, j]) > max) {
                        max = (dynamic)ABS(AB[i, j]);
                        rowIndex = i;
                        columnIndex = j;
                    }
                }
            }
            swapRows(AB, index, rowIndex);
            swapColumns(AB, index, columnIndex, queue);
        }

        private static void swapRows(MyMatrix<T> matrix, int row1, int row2) {
            if (row1 == row2) {
                return;
            }
            for (int i = 0; i < matrix.columnCount; i++) {
                T tmp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = tmp;
            }
            return;
        }

        private static void swapColumns(MyMatrix<T> matrix, int column1, int column2, List<int> queue) {
            if (column1 == column2) {
                return;
            }
            int tmp = queue[column1];
            queue[column1] = queue[column2];
            queue[column2] = tmp;
            for (int i = 0; i < matrix.rowCount; i++) {
                T Tmp = matrix[i, column1];
                matrix[i, column1] = matrix[i, column2];
                matrix[i, column2] = Tmp;
            }
            return;
        }

        private MyMatrix<T> resultByQueue(MyMatrix<T> vector, List<int> queue) {
            MyMatrix<T> tmp = new MyMatrix<T>(vector.rowCount, 1);
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

        public static MyMatrix<T> matrixJoinVector(MyMatrix<T> A, MyMatrix<T> B) {
            if (A.rowCount != B.rowCount) {
                throw new Exception("Wrong Matrixes size!");
            }
            if (B.columnCount != 1) {
                throw new Exception("Wrong Vector columnCount!");
            }
            MyMatrix<T> result = new MyMatrix<T>(A.rowCount, A.columnCount + 1);
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

        public Fraction countDiffWithFrac(MyMatrix<Fraction> matrix) {
            if (rowCount != matrix.rowCount) {
                throw new Exception("Wrong vector sizes!");
            }
            if (columnCount != matrix.columnCount) {
                throw new Exception("Wrong Matrices sizes!");
            }
            Fraction[,] thisTmp = new Fraction[rowCount, columnCount];
            Fraction[,] matrixTmp = new Fraction[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {
                    if (typeof(T) == typeof(Fraction)) {
                        thisTmp[i, j] = new Fraction((dynamic)this[i, j]);
                    }
                    else {
                        thisTmp[i, j] = new Fraction(this[i, j].ToString());
                    }
                    matrixTmp[i, j] = new Fraction((dynamic)matrix[i, j]);
                }
            }
            Fraction sum = default(Fraction);
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {
                    sum += Fraction.fracABS(thisTmp[i, j] - matrixTmp[i, j]);
                }
            }
            int count = rowCount * columnCount;
            Fraction div = new Fraction(count.ToString());
            return sum / div;
        }

        public Fraction countDiffWithSameType(MyMatrix<T> matrix) {
            if (rowCount != matrix.rowCount) {
                throw new Exception("Wrong vector sizes!");
            }
            if (columnCount != matrix.columnCount) {
                throw new Exception("Wrong Matrices sizes!");
            }
            Fraction[,] thisTmp = new Fraction[rowCount, columnCount];
            Fraction[,] matrixTmp = new Fraction[rowCount, columnCount];
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {
                    if (typeof(T) == typeof(Fraction)) {
                        thisTmp[i, j] = new Fraction((dynamic)this[i, j]);
                        matrixTmp[i, j] = new Fraction((dynamic)matrix[i, j]);
                    }
                    else {
                        thisTmp[i, j] = new Fraction(this[i, j].ToString());
                        matrixTmp[i, j] = new Fraction(matrix[i, j].ToString());
                    }

                }
            }
            Fraction sum = default(Fraction);
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {
                    sum += Fraction.fracABS(thisTmp[i, j] - matrixTmp[i, j]);
                }
            }
            int count = rowCount * columnCount;
            Fraction div = new Fraction(count.ToString());
            return sum / div;
        }

        public static T ABS(T value) {
            if ((dynamic)value < default(T)) {
                return (dynamic)default(T) - value;
            }
            return value;
        }

        public override string ToString() {
            string output = "";
            for (int i = 0; i < rowCount; i++) {
                for (int j = 0; j < columnCount; j++) {

                    if (j == columnCount - 1) {
                        output += matrix[i, j];
                    }
                    else
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
