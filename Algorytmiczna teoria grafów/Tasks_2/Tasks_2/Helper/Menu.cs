using System;

namespace Tasks_2.Helper
{
    public static class Menu
    {
        public static void UndirectedGraphMenu(string initPath = "", string extension = "")
        {
            bool quit = false;
            UndirectedGraph graph = null;

            do
            {
                Console.Clear();
                Console.WriteLine("Aktualny graf nieskierowany (prosty):\n");
                if (graph == null)
                    Console.WriteLine("-\n");
                else
                    Console.WriteLine(graph);
                Console.WriteLine("1.Stwórz nowy graf nieskierowany (prosty)");
                Console.WriteLine("2.Dodaj wierzchołek");
                Console.WriteLine("3.Usuń wierzchołek");
                Console.WriteLine("4.Dodaj krawędź");
                Console.WriteLine("5.Usuń krawędź");
                Console.WriteLine("6.Drzewo spinające DFS");
                Console.WriteLine("7.Spójne składowe");
                Console.WriteLine("8.Kruskal");
                Console.WriteLine("9.Wyjdź z programu");
                var result = Console.ReadLine().TrimEnd().TrimStart();
                switch (result)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Tworzenie nowego grafu przez:\n");
                        Console.WriteLine("1.Podanie początkowej liczby wierzchołków (stworzy pusty graf z n wierzchołkami).");
                        Console.WriteLine("2.Podanie ścieżki pliku z listą krawędzi.");
                        var result1 = Console.ReadLine().Trim();
                        switch (result1)
                        {
                            case "1":
                                Console.WriteLine("Podaj ilość początkową wierzchołków:");
                                var nS = Console.ReadLine().Trim();
                                int n = -1;
                                int.TryParse(nS, out n);
                                if (n <= 0)
                                {
                                    Console.WriteLine("Niepoprawna ilość początkowa wierzchołków (ilość musi być większa niż 0).");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                                else
                                    graph = new UndirectedGraph(n);
                                break;

                            case "2":
                                Console.WriteLine("Ścieżka początkowa = " + initPath);
                                Console.WriteLine("Rozszerzenie = " + extension);
                                Console.WriteLine("Podaj nazwę pliku z listą krawędzi:");
                                var filePath = Console.ReadLine().Trim();
                                UndirectedGraph gr = null;
                                try
                                {
                                    gr = new UndirectedGraph(@initPath + @filePath + @extension);
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
                            graph.AddVertex();
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
                            Console.WriteLine("Podaj indeks usuwanego wierzchołka :");
                            try
                            {
                                var result2 = Console.ReadLine().Trim();
                                var v = -1;
                                int.TryParse(result2, out v);
                                if (!graph.DeleteVertex(v))
                                {
                                    Console.WriteLine("Nie udało się usunąć wierzchołka (" + result2 + ") grafu (być może wierzchołek o takim indeksie nie istnieje?).");
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
                            Console.WriteLine("Podaj dwa indeksy wierzchołków, które zostaną połączone krawędzią (format: v u)." +
                                "\nOpcjonalnie możesz też dodać krawędź z wagą (format: v u waga).");
                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                int u = -1;
                                int v = -1;
                                int w = 0;
                                int.TryParse(result2[0], out u);
                                int.TryParse(result2[1], out v);
                                if (result2.Length > 2)
                                    int.TryParse(result2[2], out w);
                                if (!graph.AddEdge(u, v, w))
                                {
                                    Console.WriteLine("Nie udało się dodać krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") do grafu skierowanego. Możliwe przyczyny:");
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
                            Console.WriteLine("Podaj dwa indeksy wierzchołków, których krawędź między nimi zostanie usunięta (format: v u):");
                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                int u = 0;
                                int v = 0;
                                int.TryParse(result2[0], out u);
                                int.TryParse(result2[1], out v);
                                if (!graph.DeleteEdge(u, v))
                                {
                                    Console.WriteLine("Nie udało się usunąć krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") grafu nieskierowanego. Możliwe przyczyny:");
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
                            //var edges = graph.DFSSpanningTreeIterative();
                            //Console.WriteLine("Krawędzie drzewa spinającego (DFS Iteracyjny) grafu: ");
                            //Console.WriteLine(ArrayHelper.ToString(edges, true));
                            //Console.WriteLine();
                            var edges2 = graph.DFSSpanningTreeRecursive();
                            Console.WriteLine("Krawędzie drzewa spinającego (DFS Rekurencyjny) grafu: ");
                            Console.WriteLine(ArrayHelper.ToString(edges2, true));
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
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
                            var components = graph.Components();
                            if (components.Count == 1)
                                Console.WriteLine("Graf jest spójny! (posiada tylko jedną spójną składową obejmującą wszystkie wierzchołki)\n");
                            for (int i = 0; i < components.Count; i++)
                            {
                                Console.WriteLine("Grupa " + (i + 1) + ": ");
                                for (int j = 0; j < components[i].Count; j++)
                                {
                                    Console.Write(components[i][j] + " ");
                                }
                                Console.WriteLine("\n");
                            }
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
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
                            Console.WriteLine("Kruskal (krawędzie minimalnego drzewa rozpinającego):");
                            var list = graph.Kruskal();
                            Console.WriteLine(ArrayHelper.ToString(list, true));
                            int count = 0;
                            foreach (var l in list)
                                count += l.Weight;
                            Console.WriteLine("Suma wag powyższych krawędzi: " + count + ".");

                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        break;

                    case "9":
                        quit = !quit;
                        break;

                    default:
                        break;
                }

            } while (!quit);
        }

        public static void DirectedGraphMenu(string initPath = "", string extension = "")
        {
            bool quit = false;
            DirectedGraph graph = null;

            do
            {
                Console.Clear();
                Console.WriteLine("Aktualny graf skierowany (prosty):\n");
                if (graph == null)
                    Console.WriteLine("-\n");
                else
                    Console.WriteLine(graph);
                Console.WriteLine("1.Stwórz nowy graf skierowany (prosty)");
                Console.WriteLine("2.Dodaj wierzchołek");
                Console.WriteLine("3.Usuń wierzchołek");
                Console.WriteLine("4.Dodaj krawędź");
                Console.WriteLine("5.Usuń krawędź");
                Console.WriteLine("6.Kosaraju");
                Console.WriteLine("7.Przedsięwzięcie (sieć skierowana)");
                Console.WriteLine("8.Wyjdź z programu");
                var result = Console.ReadLine().TrimEnd().TrimStart();
                switch (result)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Tworzenie nowego grafu przez:\n");
                        Console.WriteLine("1.Podanie początkowej liczby wierzchołków (stworzy pusty graf z n wierzchołkami).");
                        Console.WriteLine("2.Podanie ścieżki pliku z listą krawędzi.");
                        var result1 = Console.ReadLine().Trim();
                        switch (result1)
                        {
                            case "1":
                                Console.WriteLine("Podaj ilość początkową wierzchołków:");
                                var nS = Console.ReadLine().Trim();
                                int n = -1;
                                int.TryParse(nS, out n);
                                if (n <= 0)
                                {
                                    Console.WriteLine("Niepoprawna ilość początkowa wierzchołków (ilość musi być większa niż 0).");
                                    Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                                    Console.ReadKey();
                                }
                                else
                                    graph = new DirectedGraph(n);
                                break;

                            case "2":
                                Console.WriteLine("Ścieżka początkowa = " + initPath);
                                Console.WriteLine("Rozszerzenie = " + extension);
                                Console.WriteLine("Podaj nazwę pliku z listą krawędzi:");
                                var filePath = Console.ReadLine().Trim();
                                DirectedGraph gr = null;
                                try
                                {
                                    gr = new DirectedGraph(@initPath + @filePath + @extension);
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
                            graph.AddVertex();
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
                            Console.WriteLine("Podaj indeks usuwanego wierzchołka :");
                            try
                            {
                                var result2 = Console.ReadLine().Trim();
                                var v = -1;
                                int.TryParse(result2, out v);
                                if (!graph.DeleteVertex(v))
                                {
                                    Console.WriteLine("Nie udało się usunąć wierzchołka (" + result2 + ") grafu (być może wierzchołek o takim indeksie nie istnieje?).");
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
                            Console.WriteLine("Podaj dwa indeksy wierzchołków, które zostaną połączone krawędzią (format: v u)." +
                                "\nOpcjonalnie możesz też dodać krawędź z wagą (format: v u waga).");
                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                int u = -1;
                                int v = -1;
                                int w = 0;
                                int.TryParse(result2[0], out u);
                                int.TryParse(result2[1], out v);
                                if (result2.Length > 2)
                                    int.TryParse(result2[2], out w);
                                if (!graph.AddEdge(u, v, w))
                                {
                                    Console.WriteLine("Nie udało się dodać krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") do grafu skierowanego. Możliwe przyczyny:");
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
                            Console.WriteLine("Podaj dwa indeksy wierzchołków, których krawędź między nimi zostanie usunięta (format: v u):");
                            try
                            {
                                var result2 = Console.ReadLine().Trim().Split(' ');
                                int u = 0;
                                int v = 0;
                                int.TryParse(result2[0], out u);
                                int.TryParse(result2[1], out v);
                                if (!graph.DeleteEdge(u, v))
                                {
                                    Console.WriteLine("Nie udało się usunąć krawędzi (" + result2[0][0] + ", " + result2[1][0] + ") grafu skierowanego. Możliwe przyczyny:");
                                    Console.WriteLine("- nie istnieje krawędź / nie istnieją takie krawędzie (KOLEJNOŚĆ INPUTU MA ZNACZENIE!)");
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
                            Console.WriteLine("Silnie składowe spójności:");
                            Console.WriteLine("(id składowej) -> {zbiór wierzchołków należący do składowej}");
                            var kosaraju = graph.Kosaraju();
                            Console.WriteLine(ArrayHelper.ToString(kosaraju));
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
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
                            Console.WriteLine("Informacje dot. danej sieci skierowanej / przedsięwzięcia:");
                            Console.WriteLine(graph.Jobs());
                            Console.WriteLine("Wciśnij dowolny przycisk aby powrócić do menu głównego...");
                            Console.ReadKey();
                        }
                        break;

                    case "8":
                        quit = !quit;
                        break;

                    default:
                        break;
                }

            } while (!quit);
        }
    }
}
