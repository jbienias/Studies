using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Monika Barzowska, Jan Bienias
//238143, 238201

namespace Aproksymacja {
    class Approximation {

        public static MyMatrix CountEquation(double[,] data, int power) {
            if (data.GetLength(1) != 2 || data.GetLength(0) < 1) {
                throw new Exception("Wrong data table! Count requires 2 columns of data.");
            }
            int dataLength = data.GetLength(0);
            int sCount = (2 * power) + 1;
            int tCount = power + 1;
            int size = power + 1;
            double[,] sTab = new double[dataLength, sCount];
            double[,] tTab = new double[dataLength, tCount];
            double[] sResults = new double[sCount];
            double[] tResults = new double[tCount];

            for (int i = 0; i < sTab.GetLength(0); i++) { //GetLength(0) == dataLen == rowsy
                for (int j = 0; j < sTab.GetLength(1); j++) { //GetLength(1) == sCount == kolumny
                    sTab[i, j] = Math.Pow(data[i, 0], j);
                    sResults[j] += sTab[i, j];
                }
                for (int j = 0; j < tTab.GetLength(1); j++) {
                    tTab[i, j] = data[i, 1] * sTab[i, j];
                    tResults[j] += tTab[i, j];
                }
            }
            MyMatrix matrix = new MyMatrix(size, size);
            MyMatrix vector = new MyMatrix(size, 1);
            int helper = 0;
            for (int i = 0; i < size; i++) {
                vector[i, 0] = tResults[i];
                for (int j = 0; j < size; j++) {
                    matrix[i, j] = sResults[j + helper];
                }
                helper++;
            }
            MyMatrix resultVector = MyMatrix.gauss(MyMatrix.matrixJoinVector(matrix, vector), true);
            return resultVector;
        }

        public static double CountVariable(MyMatrix equation, double variable) {
            string tmp = "";
            double result = 0;
            for (int i = 0; i < equation.rowCount; i++) {
                result += equation[i, 0] * Math.Pow(variable, i);
                tmp += "(" + equation[i, 0] + "* x^" + i + ")";
                if (i != equation.rowCount - 1) {
                    tmp += " + ";
                }

            }
            Console.WriteLine(tmp);
            return result;
        }
    }
}
