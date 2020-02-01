using System;
using System.Collections.Generic;

namespace Tasks_1
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            bool quit = false;
            Graph graph = null;

            do
            {
                Console.Clear();
                Console.WriteLine("Aktualny graf prosty:\n");
                if (graph == null)
                    Console.WriteLine("-\n");
                else
                    Console.WriteLine(graph + "\n");
                Console.WriteLine("1.Stwórz nowy graf prosty");
                Console.WriteLine("2.Dodaj wierzchołek");
                Console.WriteLine("3.Usuń wierzchołek");
                Console.WriteLine("4.Dodaj krawędź");
                Console.WriteLine("5.Usuń krawędź");
                Console.WriteLine("6.Istnienie podgrafu izomorficznego do cyklu C3");
                Console.WriteLine("7.Pokaż szczegóły grafu");
                //Tasks 2
                Console.WriteLine("8.Istnienie cyklu C(n)");
                Console.WriteLine("9.Wyznaczanie centrum drzewa");
                Console.WriteLine("10.Wyjdź z programu");
                var result = Console.ReadLine().TrimEnd().TrimStart();
                switch (result)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Tworzenie nowego grafu przez:\n");
                        Console.WriteLine("1.Podanie początkowej liczby wierzchołków (stworzy pusty graf z n wierzchołkami).");
                        Console.WriteLine("2.Podanie ciągu graficznego (stworzy losowy graf prosty o ile ciąg będzie poprawny).");
                        Console.WriteLine("3.Podanie ścieżki pliku z macierzą sąsiedztwa.");
                        var result1 = Console.ReadLine().Trim();
                        switch (result1)
                        {
                            case "1":
                                Console.WriteLine("Podaj ilość początkową wierzchołków:");
                                var nS = Console.ReadLine().Trim();
                                int n = 0;
                                Int32.TryParse(nS, out n);
                                if (n <= 0)
                                {
                                    Console.WriteLine("Niepoprawna ilość początkowa wierzchołków (ilość musi być większa niż 0).");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                                else
                                    graph = new Graph(n);
                                break;

                            case "2":
                                Console.WriteLine("Podaj ciąg graficzny (Format: 'a b c d e f ... z', gdzie a,b,c,d ... należą do l. naturalnych):");
                                var line = Console.ReadLine();
                                var seriesS = line.Trim().Split(' ');
                                var series = new List<int>();
                                foreach (var s in seriesS)
                                {
                                    int currentS = 0;
                                    Int32.TryParse(s, out currentS);
                                    series.Add(currentS);
                                }
                                Graph g = Graph.HavelHakimi(series);
                                if (g == null)
                                {
                                    Console.WriteLine("Ciąg graficzny nie jest poprawny.");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }

                                graph = g;
                                break;

                            case "3":
                                Console.WriteLine(@"Podaj ścieżkę do pliku z macierzą sąsiedztwa (np. C:\...\...\plik.txt):");
                                var filePath = Console.ReadLine().Trim();
                                Graph gr = null;
                                try
                                {
                                    gr = new Graph(@filePath);
                                }
                                catch (Exception e) { Console.WriteLine(e.Message); }
                                if (gr == null)
                                {
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }

                                else
                                    graph = gr;

                                break;

                            default:
                                Console.WriteLine("Wybrano nieznaną opcję.");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                                break;
                        }
                        break;



                    case "2":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine(graph + "\n");
                            Console.WriteLine("Dodawanie wierzchołka:");
                            Console.WriteLine("Podaj nazwę dodawanego wierzchołka (jeden 'char'):");

                            try
                            {
                                var result2 = Console.ReadLine().Trim()[0];
                                if (!graph.AddVertex(result2))
                                {
                                    Console.WriteLine("Nie udało się dodać wierzchołka (" + result2 + ") do grafu (być może wierzchołek o takiej nazwie już istnieje?).");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Podano zły input.");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }

                        }
                        break;

                    case "3":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine(graph + "\n");
                            Console.WriteLine("Usuwanie wierzchołka:");
                            Console.WriteLine("Podaj nazwę usuwanego wierzchołka (jeden 'char'):");

                            try
                            {
                                var result2 = Console.ReadLine().Trim()[0];
                                if (!graph.DeleteVertex(result2))
                                {
                                    Console.WriteLine("Nie udało się usunąć wierzchołka (" + result2 + ") grafu (być może wierzchołek o takiej nazwie nie istnieje?).");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Podano zły input.");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "4":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine(graph + "\n");
                            Console.WriteLine("Dodawanie krawędzi:");
                            Console.WriteLine("Podaj dwie nazwy (po jeden 'char') wierzchołków, które zostaną połączone krawędzią (format: a b):");

                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                if (!graph.AddEdge(result2[0][0], result2[1][0]))
                                {
                                    Console.WriteLine("Nie udało się dodać krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") do grafu prostego. Możliwe przyczyny:");
                                    Console.WriteLine("- nie można tworzyć 'pętelek' (tzn. np. tworzenia krawędzi z 'X' do 'X')");
                                    Console.WriteLine("- nie można dodawać więcej niż jednej tej samej krawędzi");
                                    Console.WriteLine("- nie istnieją takie wierzchołki");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Podano zły input.");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }

                        }
                        break;

                    case "5":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine(graph + "\n");
                            Console.WriteLine("Usuwanie krawędzi:");
                            Console.WriteLine("Podaj dwie nazwy (po jeden 'char') wierzchołków, których krawędź między nimi zostanie usunięta (format: a b):");

                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                if (!graph.DeleteEdge(result2[0][0], result2[1][0]))
                                {
                                    Console.WriteLine("Nie udało się usunąć krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") grafu prostego. Możliwe przyczyny:");
                                    Console.WriteLine("- nie istnieje krawędź / nie istnieją takie krawędzie");
                                    Console.WriteLine("- nie istnieją takie wierzchołki");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Podano zły input.");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }

                        }
                        break;

                    case "6":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Czy graf zawiera podgraf izomorficzny do cyklu C3? : ");
                            Console.WriteLine(graph.FindC3Naive() + " (sprawdzenie naiwne)");
                            Console.WriteLine(graph.FindC3ByMatrixMul() + " (sprawdzenie poprzez mnożenie macierzy)");
                            Console.WriteLine(graph.FindC3ByMatrixMul3() + " (ilość podgrafów izomorficznych do cyklu C3)");
                            Console.WriteLine("\nWciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        break;

                    case "7":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Szczegóły aktualnego grafu prostego: \n");
                            Console.WriteLine(graph);
                            Console.WriteLine(graph.AdditionalInfo());
                            Console.WriteLine("\nWciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        break;

                    case "8":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            List<int> list = new List<int>();
                            try
                            {

                                list = graph.FindCN();
                                if (list.Count == 0)
                                    Console.WriteLine("Nie znaleziono cyklu!");
                                else
                                    Console.WriteLine("Cykl: " + ArrayHelper.ToString(list));
                                Console.WriteLine("\nWciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Graf nie posiada minimalnego stopnia 2!");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "9":
                        Console.Clear();
                        if (graph == null)
                        {
                            Console.WriteLine("Graf nie został jeszcze zainicjalizowany! (graf jest nullem)");
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (!graph.IsAcyclic())
                            {
                                Console.WriteLine("Graf jest cykliczny / Graf nie jest drzewem!");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                            else if (!graph.IsConnected())
                            {
                                Console.WriteLine("Graf nie jest spójny!");
                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                            else
                            {
                                //var listbfs = graph.JordanCenterBFS();
                                var list0 = graph.JordanCenterHKC();
                                var list1 = graph.JordanCenterNaive();
                                Console.WriteLine("Algorytm Jordana (naive) : ");
                                Console.WriteLine(ArrayHelper.ToString(list1) + "\n");
                                Console.WriteLine("Algorytm Jordana (linear HKH algorithm) : ");
                                Console.WriteLine(ArrayHelper.ToString(list0) + "\n");
                                //Console.WriteLine("Algorytm Jordana (linear BFS) : ");
                                //Console.WriteLine(ArrayHelper.ToString(listbfs) + "\n");

                                Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case "10":
                        quit = !quit;
                        break;

                    default:
                        break;
                }

            } while (!quit);
        }
    }
}
