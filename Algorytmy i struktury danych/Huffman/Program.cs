using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

/*Jan Bienias 238201
 * Task 5.5 a / Huffman Algorithm
 * https://inf.ug.edu.pl/~zylinski/dydaktyka/AiSD/ALZ_05.pdf
 * Help : 
 * http://onehourofdevelopment.blogspot.com/2012/12/implementing-huffman-coding-in-c.html
 */

namespace Huffman2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter file name / file path > ");
            string fileName = Console.ReadLine();
            var chars = new List<char>();
            var occurance = new List<int>();
            FileCharsOcc(fileName, occurance, chars);
            var counts = new Dictionary<char, int>();
            Console.WriteLine("CHAR\tOCCURANCE");
            for(int i=0;i<chars.Count; i++)
            {
                counts.Add(chars[i], occurance[i]);
                Console.WriteLine(chars[i] + "\t" + occurance[i]);
            }
            HuffmanTree tree = new HuffmanTree(counts);
            IDictionary<char, string> encodings = tree.CreateEncodings();

            var chars_new = new List<char>();
            var chars_new_size = new List<int>();
            Console.WriteLine("CHAR\tCODE");
            foreach (KeyValuePair<char, string> k in encodings)
            {
                Console.WriteLine((k.Key == '\n' ? "EOF" : k.Key.ToString()) + ":\t" + k.Value);
                chars_new.Add(k.Key);
                chars_new_size.Add(k.Value.Length);
            }
            int formattedFileSize = 0;
            for(int i = 0; i<chars.Count; i++)
            {
                for(int j = 0; j<chars_new.Count; j++)
                {
                    if(chars[i] == chars_new[j])
                    {
                        formattedFileSize += occurance[i] * chars_new_size[j];
                        break;
                    }
                }
            }
            double fileSize = FileSize(fileName);
            fileSize *= Math.Sqrt(counts.Count);
            Console.WriteLine("File "+ fileName+" without compression : " + fileSize + " Bits");
            Console.WriteLine("File " + fileName + " with compression    : " + formattedFileSize +  " Bits");
        }

        static void FileCharsOcc(string file, List<int> counter, List<char> characters)         
        {
            var counterHelper = new int[256];
            for (int i = 0; i < 256; i++)
                counterHelper[i] = 0;
            int c;
            if (!File.Exists(file))
            {
                throw new InvalidProgramException("File does not exist");
            }
            StreamReader reader;
            reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                c = reader.Read();
                //logic when to count
                counterHelper[c] += 1;
            }
            reader.Close();
            reader.Dispose();
            for (int i = 0; i < 256; i++)
            {
                if (counterHelper[i] > 0)
                {
                    counter.Add(counterHelper[i]);
                    characters.Add((char)i);
                }
            }
        }

        static int FileSize(string file)
        {
            if (!File.Exists(file))
                throw new InvalidProgramException("File does not exist");

            int c;
            int counter = 0;
            StreamReader reader;
            reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                c = reader.Read();
                counter++;
            }
            reader.Close();
            reader.Dispose();
            return counter;
        }

        internal class PriorityQueue<T>
        {
            private readonly SortedDictionary<int, Queue<T>> _sortedDictionary = new SortedDictionary<int, Queue<T>>();

            public int Count { get; private set; }

            public void Enqueue(T item, int priority)
            {
                ++Count;
                if (!_sortedDictionary.ContainsKey(priority))
                    _sortedDictionary[priority] = new Queue<T>();
                _sortedDictionary[priority].Enqueue(item);
            }

            public T Dequeue()
            {
                --Count;
                var item = _sortedDictionary.First();
                if (item.Value.Count == 1)
                    _sortedDictionary.Remove(item.Key);
                return item.Value.Dequeue();
            }
        }

        internal class HuffmanNode
        {
            public HuffmanNode Parent { get; set; }
            public HuffmanNode Left { get; set; }
            public HuffmanNode Right { get; set; }
            public char Value { get; set; }
            public int Count { get; set; }
        }

        internal class HuffmanTree
        {
            private readonly HuffmanNode _root;

            public HuffmanTree(IEnumerable<KeyValuePair<char, int>> counts)
            {
                var priorityQueue = new PriorityQueue<HuffmanNode>();

                foreach (KeyValuePair<char, int> k in counts)
                {
                    priorityQueue.Enqueue(new HuffmanNode { Value = k.Key, Count = k.Value }, k.Value);
                }

                while (priorityQueue.Count > 1)
                {
                    HuffmanNode n1 = priorityQueue.Dequeue();
                    HuffmanNode n2 = priorityQueue.Dequeue();
                    var n3 = new HuffmanNode { Left = n1, Right = n2, Count = n1.Count + n2.Count };
                    n1.Parent = n3;
                    n2.Parent = n3;
                    priorityQueue.Enqueue(n3, n3.Count);
                }

                _root = priorityQueue.Dequeue();
            }

            public IDictionary<char, string> CreateEncodings()
            {
                var encodings = new Dictionary<char, string>();
                Encode(_root, "", encodings);
                return encodings;
            }

            private void Encode(HuffmanNode node, string path, IDictionary<char, string> encodings)
            {
                if (node.Left != null)
                {
                    Encode(node.Left, path + "0", encodings);
                    Encode(node.Right, path + "1", encodings);
                }
                else
                {
                    encodings.Add(node.Value, path);
                }
            }
        }
    }
}
