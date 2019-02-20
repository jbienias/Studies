using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Jan Bienias
//238201
//Algorytmy numeryczne 
//Zadanie 1 - Sumowanie szeregów potęgowych (e^x)

namespace Exp
{
    class Program
    {
        static void Main(string[] args)
        {
            calculate(10, 100, 10, 10, 1000, 10);
        }


        public static void calculate(int xStart, int xFinish, int xStep, int nStart, int nFinish, int nStep)
        {
            calculateErrorMathFunc(xStart, xFinish, xStep, nStart, nFinish, nStep);
            calculateErrorForwBack(xStart, xFinish, xStep, nStart, nFinish, nStep);
        }

        public static void calculateErrorMathFunc(int xStart, int xFinish, int xStep, int nStart, int nFinish, int nStep)
        {
            for (int x = xStart; x <= xFinish; x = x + xStep)
            {
                StreamWriter writer = new StreamWriter("roznice_bezwzgl_" + x + ".csv");
                Console.SetOut(writer);
                Console.WriteLine("N;Roznica exp - taylor(przod);Roznica exp - taylor(tyl);Roznica exp - poprzednik(przod);Roznica exp - poprzednik(tyl); e^" + x);
                for (int n = nStart; n <= nFinish; n = n + nStep)
                {
                    double exp = Math.Exp(x);
                    double bladSnForw = Math.Abs(exp - expSnForward(n, x));
                    double bladSnBack = Math.Abs(exp - expSnBackward(n, x));
                    double bladTaylorForw = Math.Abs(exp - expTaylorForward(n, x));
                    double bladTaylorBack = Math.Abs(exp - expTaylorBackward(n, x));
                    Console.WriteLine(n + ";" + bladTaylorForw + ";" + bladTaylorBack + ";" + bladSnForw + ";" + bladSnBack);
                }
            }
        }

        public static void calculateErrorForwBack(int xStart, int xFinish, int xStep, int nStart, int nFinish, int nStep)
        {
            for (int x = xStart; x <= xFinish; x = x + xStep)
            {
                StreamWriter writer = new StreamWriter("roznice_przodtyl_" + x + ".csv");
                Console.SetOut(writer);
                Console.WriteLine("N;Roznica exp - taylor(przod/tyl);Roznica exp - poprzednik(przod/tyl); e^" + x);
                for (int n = nStart; n <= nFinish; n = n + nStep)
                {
                    double bladSn = Math.Abs(expSnBackward(n, x) - expSnForward(n, x));
                    double bladTaylor = Math.Abs(expTaylorBackward(n, x) - expTaylorForward(n, x));
                    Console.WriteLine(n + ";" + bladTaylor + ";" + bladSn);
                }
            }
        }

        //DOUBLE FUNCTIONS

        public static double expSnForward(int n, double x)
        {
            if (n < 1)
                return 1;
            double[] tab = new double[n + 1];
            tab[0] = 1;
            double result = 0;
            for (int i = 1; i <= n; i++)
            {
                try
                {
                    tab[i] = tab[i - 1] * x / i;
                }
                catch (OverflowException) { break; }


            }
            for (int i = 0; i <= n; i++)
            {
                result += tab[i];
            }
            return result;
        }

        public static double expSnBackward(int n, double x)
        {
            if (n < 1)
                return 1;
            double[] tab = new double[n + 1];
            tab[0] = 1;
            double result = 0;
            for (int i = 1; i <= n; i++)
            {
                try
                {
                    tab[i] = tab[i - 1] * x / i;
                }
                catch (OverflowException) { break; }

            }
            for (int i = n; i >= 0; i--)
            {
                result += tab[i];
            }
            return result;
        }

        public static double expTaylorForward(int n, double x)
        {
            if (n < 1)
                return 1;
            double result = 0;
            double[] tab = new double[n + 1];
            int i;
            for (i = 0; i <= n; i++)
            {
                try
                {
                    tab[i] = Math.Pow(x, i) / factorial(i);
                    result += tab[i];
                }
                catch (OverflowException) { break; }
            }
            return result;
        }

        public static double expTaylorBackward(int n, double x)
        {
            if (n < 1)
                return 1;
            double result = 0;
            double[] tab = new double[n + 1];
            int i;
            for (i = 0; i <= n; i++)
            {
                try
                {
                    tab[i] = Math.Pow(x, i) / factorial(i);
                }
                catch (OverflowException) { break; }
            }
            i--;
            for (int j = i; j >= 0; j--)
            {
                result += tab[j];
            }
            return result;
        }

        //DOUBLE FUNCTIONS HELPERS

        public static double factorial(int n)
        {
            if (n == 0)
                return 1;
            double factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }
            return factorial;
        }

    }
}
