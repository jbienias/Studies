using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

//Jan Bienias
//238201
//
//Scientific sources :
//https://inf.ug.edu.pl/~pmp/Z/ASDwyklad/wzorceP1.pdf
//http://www.geeksforgeeks.org/searching-for-patterns-set-1-naive-pattern-searching/
//http://www.geeksforgeeks.org/searching-for-patterns-set-2-kmp-algorithm/
//http://www.geeksforgeeks.org/searching-for-patterns-set-3-rabin-karp-algorithm/
//https://www.youtube.com/watch?v=GTJr8OvyEVQ
//https://www.youtube.com/watch?v=KG44VoDtsAA
//https://www.youtube.com/watch?v=H4VrKHVG5qI


namespace PatternSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            String txt = "AABAACAADAABAAABAA";
            String pat = "AABA";

            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Restart();
            Console.WriteLine("Naive");
            stopWatch.Start();
            Naive(pat, txt);
            stopWatch.Stop();
            Console.WriteLine("Elapsed = {0}\n", stopWatch.Elapsed);

            stopWatch.Restart();
            Console.WriteLine("KMP");
            stopWatch.Start();
            KMP(pat, txt);
            stopWatch.Stop();
            Console.WriteLine("Elapsed = {0}\n", stopWatch.Elapsed);

            stopWatch.Restart();
            Console.WriteLine("Rabin");
            stopWatch.Start();
            Rabin(pat, txt);
            stopWatch.Stop();
            Console.WriteLine("Elapsed = {0}\n", stopWatch.Elapsed);

            stopWatch.Restart();
            Console.WriteLine("Finite-automata");
            stopWatch.Start();
            FA(pat, txt);
            stopWatch.Stop();
            Console.WriteLine("Elapsed = {0}\n", stopWatch.Elapsed);

        }

        public static void Naive(String pattern, String text)
        {
            //worst case 0(m * n)
            int m = pattern.Length;
            int n = text.Length;
            for(int i = 0; i <= n-m; i++)
            {
                int j;
                for (j = 0; j < m; j++)
                    if (text[i + j] != pattern[j])
                        break;
                if(j == m)
                    Console.WriteLine("Pattern found at index " + i);
            }
        }


        public static void PrefixArray(String pattern, int[] pi)
        {
            //O(m)
            int index = 0;
            for(int i = 1; i < pattern.Length; )
            {
                if(pattern[i] == pattern[index])
                {
                    index++;
                    pi[i] = index;
                    i++;
                }
                else
                {
                    if (index != 0)
                        index = pi[index - 1];
                    else
                    {
                        pi[i] = 0;
                        i++;
                    }
                }
            }

        }

        public static void KMP(String pattern, String text)
        {
            // O(m+n)
            int m = pattern.Length;
            int n = text.Length;
            int i = 0; int j = 0;
            int[] pi = new int[m];

            PrefixArray(pattern, pi);
            while (i < n)
            {
                if (pattern[j] == text[i])
                {
                    i++; j++;
                }
                if (j == m)
                {
                    Console.WriteLine("Pattern found at index {0}", i - j);
                    j = pi[j - 1];
                }
                else if (i < n && pattern[j] != text[i])
                {
                    if (j != 0)
                        j = pi[j - 1];
                    else
                        i++;
                }
            }
        }

        public static void Rabin(String pattern, String text)
        {
            int d = 256; //0 - 255
            int q = 27077;
            int m = pattern.Length;
            int n = text.Length;
            int hp = 0; //hash val for pattern
            int ht = 0; //hash val for text
            int h = 1;

            for (int i = 0; i < m - 1; i++) // power(d, m-1) mod q, with overflow check
                h = (h * d) % q;

            for(int i = 0; i < m; i++)
            {
                hp = (d * hp + pattern[i]) % q;
                ht = (d * ht + text[i]) % q;
            }

            for(int i = 0; i <= n-m; i++)
            {
                if (hp == ht)
                {
                    int j;
                    for(j = 0; j < m; j++)
                    {
                        if (pattern[j] != text[i + j])
                            break;
                    }

                    if(j == m)
                        Console.WriteLine("Pattern found at index " + i);
                }

                if(i < n-m)
                {
                    ht = (d * (ht - text[i] * h) + text[i + m]) % q;
                    //we might get integer overflow, so we convert it to positive val
                    if (ht < 0)
                        ht = (ht + q);
                }
            }

        }

        public static int State(int[] pi, int i, String pattern, char ch)
        {
            int len = pi[i - 1];
            while(true)
            {
                if (pattern[len] == ch)
                    return len + 1;
                else
                {
                    if (len == 0)
                        return 0;
                    else
                        len = pi[len - 1];
                }
            }
        }



        public static void FA(String pattern, String text)
        {
            int m = pattern.Length;
            int n = text.Length;
            int d = 256;
            int[] pi = new int[m];
            PrefixArray(pattern, pi);
            int[,] arr = new int[m + 1, d];
            int j; int i;
            //transition function
            for(j = 0; j < m; j++)
            {
                for (i = 0; i < d; i++)
                {
                    if (j == m || i != pattern[j])
                        arr[j, i] = arr[pi[j], i];
                    else
                        arr[j, i] = j + 1;
                }
            }
            j = 0;
            for(i = 0; i < n; i++)
            {
                j = arr[j, text[i]];
                if(j == m)
                    Console.WriteLine("Pattern found at index {0}" ,i-m+1);
            }
        }
    }
   
}
