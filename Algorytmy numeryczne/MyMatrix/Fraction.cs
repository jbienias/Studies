using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;

//Monika Barzowska i Jan Bienias
//238143 238201
//Informatyka III gr 1
//Zad 2

namespace Macierze {
    public struct Fraction {
        private BigInteger numerator;
        private BigInteger denominator;

        public Fraction(BigInteger n, BigInteger d) {
            numerator = n;
            denominator = d;
            this = reduceFraction();
        }

        public Fraction(Fraction n) {
            numerator = n.numerator;
            denominator = n.denominator;
            this = reduceFraction();
        }

        public Fraction(string n) {
            try {
                denominator = 1;
                int afterComma = digitsAfterComma(n);
                if (afterComma == 0) {
                    numerator = BigInteger.Parse(n);
                } else {
                    numerator = BigInteger.Parse(removeDotOrComma(n));
                    denominator = 1;
                    for (int i = 0; i < afterComma; i++) {
                        denominator *= 10;
                    }
                }
                this = reduceFraction();
            } catch (Exception) {
                this = default(Fraction);
            }
        }

        private Fraction reduceFraction() {
            if (numerator == 0 || denominator == 0) {
                numerator = 0; denominator = 0;
                return this;
            }
            Fraction modified = this;
            BigInteger gcd = GCD(modified.numerator, modified.denominator);
            modified.numerator /= gcd;
            modified.denominator /= gcd;
            if (modified.denominator < 0) {
                modified.numerator = -modified.numerator;
                modified.denominator = -modified.denominator;
            }
            return modified;
        }


        public override string ToString() {
            return numerator + "/" + denominator;
        }

        public double ToDouble() {
            if (this.denominator == 0) {
                return (double)numerator;
            }
            return (double)numerator / (double)denominator;
        }

        public float ToFloat() {
            if (this.denominator == 0) {
                return (float)numerator;
            }
            return (float)numerator / (float)denominator;
        }

        public static Fraction operator +(Fraction a, Fraction b) {
            try {
                if (a.denominator == 0)
                    return b;
                else if (b.denominator == 0)
                    return a;
                BigInteger n = a.numerator * b.denominator + b.numerator * a.denominator;
                BigInteger d = a.denominator * b.denominator;
                return (new Fraction(n, d).reduceFraction());
            } catch (Exception) { throw new Exception("Error in operation : ADD!"); }
        }

        public static Fraction operator -(Fraction a, Fraction b) {
            try {
                if (a.denominator == 0)
                    return new Fraction(-b.numerator, b.denominator);
                else if (b.denominator == 0)
                    return a;
                BigInteger n = a.numerator * b.denominator - b.numerator * a.denominator;
                BigInteger d = a.denominator * b.denominator;
                return (new Fraction(n, d).reduceFraction());
            } catch (Exception) { throw new Exception("Error in operation : SUBTRACT!"); }
        }

        public static Fraction operator *(Fraction a, Fraction b) {
            try {
                BigInteger n = a.numerator * b.numerator;
                BigInteger d = a.denominator * b.denominator;
                return (new Fraction(n, d).reduceFraction());

            } catch (Exception) { throw new Exception("Error in operation : MULTIPLY!"); }
        }

        public static Fraction operator /(Fraction a, Fraction b) {
            try {
                BigInteger n = a.numerator * b.denominator;
                BigInteger d = a.denominator * b.numerator;
                return (new Fraction(n, d).reduceFraction());

            } catch (Exception) { throw new Exception("Error in operation : DIVIDE!"); }
        }

        public static bool operator >(Fraction a, Fraction b) {
            if (fractionCompare(a, b) == 1)
                return true;
            return false;
        }

        public static bool operator <(Fraction a, Fraction b) {
            if (fractionCompare(a, b) == -1)
                return true;
            return false;
        }

        private static int fractionCompare(Fraction a, Fraction b) {
            if ((a.numerator == 0 && b.numerator < 0) || (b.numerator == 0 && a.numerator > 0)) return 1;
            if ((a.numerator < 0 && b.numerator == 0) || (b.numerator > 0 && a.numerator == 0)) return -1;
            if ((a.numerator == 0 && b.numerator == 0)) return 0;
            Fraction A = a; Fraction B = b;
            A.numerator *= B.denominator;
            B.numerator *= A.denominator;
            if (A.numerator > B.numerator) return 1;
            if (B.numerator > A.numerator) return -1;
            if (A.numerator == B.numerator) return 0;
            return 100;
        }

        private static BigInteger GCD(BigInteger a, BigInteger b) {
            if (a < 0) a = -a;
            if (b < 0) b = -b;

            do {
                if (a < b) {
                    BigInteger tmp = a;
                    a = b;
                    b = tmp;
                }
                a = a % b;
            } while (a != 0);
            return b;
        }

        public static BigInteger LCM(BigInteger a, BigInteger b) {
            return (a / GCD(a, b)) * b;
        }

        public static string removeDotOrComma(string n) {
            if (n.Contains(",")) {
                return n.Replace(",", "");
            } else if (n.Contains(".")) {
                return n.Replace(".", "");
            } else
                return n;
        }

        public static int digitsAfterComma(string n) {
            if (n.Contains(",")) {
                return n.Split(',')[1].Length;
            } else if (n.Contains(".")) {
                return n.Split('.')[1].Length;
            } else
                return 0;
        }

        public static Fraction fracABS(Fraction value) {
            if (value < default(Fraction)) {
                return default(Fraction) - value;
            }
            return value;
        }

        public bool Equals(Fraction a) {
            if ((object)a == null)
                return false;
            return (numerator == a.numerator) && (denominator == a.denominator);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}