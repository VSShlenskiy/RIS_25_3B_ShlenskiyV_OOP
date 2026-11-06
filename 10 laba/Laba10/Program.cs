using System;
using System.Linq;
using PlacesLib;

namespace Laba10
{
    class Program
    {
        static Random rnd = new Random();

        static Place CreateRandom(int type)
        {
            Place p;
            switch (type)
            {
                case 1: p = new Region(); break;
                case 2: p = new City(); break;
                case 3: p = new Megacity(); break;
                case 4: p = new Address(); break;
                default: p = new Place(); break;
            }
            p.RandomInit();
            return p;
        }

        static Place[] CreateArray(int n)
        {
            Place[] a = new Place[n];
            for (int i = 0; i < n; i++)
                a[i] = CreateRandom(i < 5 ? i : rnd.Next(5));
            return a;
        }

        static void ShowAll(Place[] a)
        {
            foreach (Place p in a) p.Show();
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int v) && v >= min && v <= max) return v;
                Console.WriteLine("Некорректное значение, повторите ввод.");
            }
        }

        static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine() ?? "";
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Place[] arr = CreateArray(10);

            while (true)
            {
                Console.WriteLine("1. виртуальные/невиртуальные методы, Equals");
                Console.WriteLine("2. запросы (is / as)");
                Console.WriteLine("3. IComparable, IComparer, бинарный поиск");
                Console.WriteLine("4. массив IInit");
                Console.WriteLine("5. клонирование и ShallowCopy");
                Console.WriteLine("6. Пересоздать массив");
                Console.WriteLine("0. Выход");
                int choice = ReadInt("Выбор: ", 0, 6);
                Console.WriteLine();
                switch (choice)
                {
                    case 1: Part1(arr); break;
                    case 2: Part2(arr); break;
                    case 3: Part3Sort(arr); break;
                    case 4: Part3IInit(); break;
                    case 5: Part3Clone(); break;
                    case 6: arr = CreateArray(10); Console.WriteLine("Массив создан заново."); break;
                    case 0: return;
                }
            }
        }

        static void Part1(Place[] arr)
        {
            Console.WriteLine("--- Виртуальный Show() ---");
            ShowAll(arr);

            Console.WriteLine("\n--- Невиртуальный ShowNonVirtual() ---");
            foreach (Place p in arr) p.ShowNonVirtual();

            Console.WriteLine("\n--- Equals ---");
            City c1 = new City("Борисов", 140000, 1102, "Минская область");
            City c2 = new City(c1);
            City c3 = new City("Борисов", 140000, 1102, "Брестская область");
            Console.WriteLine($"c1.Equals(c2) = {c1.Equals(c2)}");
            Console.WriteLine($"c1.Equals(c3) = {c1.Equals(c3)}");
            Console.WriteLine($"c1.Equals(new Place(\"Борисов\")) = {c1.Equals(new Place("Борисов"))}");

            Console.WriteLine("\n--- Ввод объекта с клавиатуры (Init) ---");
            if (ReadInt("Ввести город вручную? (1 - да, 0 - нет): ", 0, 1) == 1)
            {
                City c = new City();
                c.Init();
                c.Show();
            }
        }

        static void Part2(Place[] arr)
        {
            Console.WriteLine("Текущий массив:");
            ShowAll(arr);

            var regions = arr.OfType<City>().Select(c => c.RegionName).Distinct().ToList();
            Console.WriteLine("\nОбласти в массиве: " + string.Join(", ", regions));

            Console.WriteLine("\n1. Города заданной области");
            Console.WriteLine("2. Суммарное население всех городов области");
            Console.WriteLine("3. Количество городов в области");
            int q = ReadInt("Выбор запроса: ", 1, 3);

            string region = ReadString("Область: ");

            switch (q)
            {
                case 1:
                    {
                        bool found = false;
                        foreach (Place p in arr)
                            if (p is City c && c.RegionName == region)
                            { found = true; Console.WriteLine($"  {c.Name}"); }
                        if (!found) Console.WriteLine("  Городов не найдено.");
                        break;
                    }
                case 2:
                    {
                        long sum = 0;
                        foreach (Place p in arr)
                            if (p is City c && c.RegionName == region) sum += c.Population;
                        Console.WriteLine($"Суммарное население городов области «{region}»: {sum}");
                        break;
                    }
                case 3:
                    {
                        int count = 0;
                        foreach (Place p in arr)
                        {
                            City c = p as City;
                            if (c != null && c.RegionName == region) count++;
                        }
                        Console.WriteLine($"Городов в области «{region}»: {count}");
                        break;
                    }
            }
        }

        static void Part3Sort(Place[] source)
        {
            Place[] arr = (Place[])source.Clone();

            Console.WriteLine("--- Сортировка по названию (IComparable, Array.Sort) ---");
            Array.Sort(arr);
            ShowAll(arr);

            Console.WriteLine("\n--- Сортировка по населению (IComparer, Array.Sort) ---");
            Array.Sort(arr, new SortByPopulation());
            ShowAll(arr);

            Array.Sort(arr);

            Console.WriteLine("\nДоступные названия для поиска (массив отсортирован по Name):");
            for (int i = 0; i < arr.Length; i++)
                Console.WriteLine($"  [{i}] {arr[i].Name} ({arr[i].GetType().Name})");

            string key = ReadString("\nНазвание для бинарного поиска: ");
            int idx = BinarySearch(arr, key);
            Console.WriteLine(idx >= 0
                ? $"Свой бинарный поиск: найден элемент №{idx}:"
                : "Свой бинарный поиск: не найдено.");
            if (idx >= 0) arr[idx].Show();

            int idx2 = Array.BinarySearch(arr, new Place(key));
            Console.WriteLine(idx2 >= 0 ? $"Array.BinarySearch: индекс {idx2}" : "Array.BinarySearch: не найдено.");
        }

        static int BinarySearch(Place[] a, string name)
        {
            int lo = 0, hi = a.Length - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                int c = string.Compare(a[mid].Name, name, StringComparison.Ordinal);
                if (c == 0) return mid;
                if (c < 0) lo = mid + 1; else hi = mid - 1;
            }
            return -1;
        }

        static void Part3IInit()
        {
            IInit[] items =
            {
                new Place(), new Region(), new City(),
                new Megacity(), new Address(), new Landmark()
            };

            Console.WriteLine("--- RandomInit() для всех элементов ---");
            foreach (IInit it in items) it.RandomInit();
            ShowIInit(items);

            Console.WriteLine("\n--- Init() для выбранного элемента ---");
            for (int i = 0; i < items.Length; i++)
                Console.WriteLine($"{i}: {items[i].GetType().Name}");
            int k = ReadInt("Номер элемента: ", 0, items.Length - 1);
            items[k].Init();
            ShowIInit(items);
        }

        static void ShowIInit(IInit[] items)
        {
            foreach (IInit it in items)
            {
                if (it is Place p) p.Show();
                else if (it is Landmark l) l.Show();
            }
        }

        static void Part3Clone()
        {
            Region original = new Region("Минская область", 1_500_000, 40_000, 22);
            original.CityNames = new[] { "Борисов", "Молодечно", "Пинск" };

            Region shallow = (Region)original.ShallowCopy();
            Region clone = (Region)original.Clone();

            Console.WriteLine("До изменения:");
            Console.Write("Оригинал:     "); original.Show();
            Console.Write("ShallowCopy:  "); shallow.Show();
            Console.Write("Clone:        "); clone.Show();

            original.CityNames[0] = "ИЗМЕНЁННЫЙ";
            original.Name = "Новое имя";

            Console.WriteLine("\nПосле изменения оригинала (CityNames[0] и Name):");
            Console.Write("Оригинал:     "); original.Show();
            Console.Write("ShallowCopy:  "); shallow.Show();
            Console.Write("Clone:        "); clone.Show();

            Console.WriteLine($"\nReferenceEquals(original.CityNames, shallow.CityNames) = {ReferenceEquals(original.CityNames, shallow.CityNames)}");
            Console.WriteLine($"ReferenceEquals(original.CityNames, clone.CityNames)   = {ReferenceEquals(original.CityNames, clone.CityNames)}");
        }
    }
}