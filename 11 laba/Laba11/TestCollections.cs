using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PlacesLib;

namespace Laba11
{
    public class TestCollections
    {
        private readonly Random rnd = new Random();
        private int nextId;

        public LinkedList<Megacity> Collection1 { get; } = new LinkedList<Megacity>();
        public LinkedList<string> Collection2 { get; } = new LinkedList<string>();
        public Dictionary<City, Megacity> Collection3 { get; } = new Dictionary<City, Megacity>();
        public Dictionary<string, Megacity> Collection4 { get; } = new Dictionary<string, Megacity>();

        public TestCollections(int count = 1000)
        {
            for (int i = 0; i < count; i++)
                Add(CreateMegacity());
        }

        public Megacity CreateMegacity()
        {
            int id = ++nextId;
            return new Megacity
            {
                Name = $"Мегаполис-{id}",
                Population = rnd.Next(1_000_000, 15_000_000),
                FoundationYear = rnd.Next(800, 2000),
                RegionName = "Столичный регион",
                MetroStations = rnd.Next(10, 300),
                Districts = rnd.Next(5, 30)
            };
        }

        public void Add(Megacity m)
        {
            Collection1.AddLast(m);
            Collection2.AddLast(m.ToString());
            Collection3.Add(m.BaseCity, m);
            Collection4.Add(m.ToString(), m);
        }

        public bool Remove(Megacity m)
        {
            if (m == null) return false;

            bool removed = Collection1.Remove(m);
            Collection2.Remove(m.ToString());
            Collection3.Remove(m.BaseCity);
            Collection4.Remove(m.ToString());
            return removed;
        }

        public void AddRandom()
        {
            Add(CreateMegacity());
        }

        public bool RemoveFirst()
        {
            if (Collection1.Count == 0) return false;
            return Remove(Collection1.First.Value);
        }

        public bool RemoveLast()
        {
            if (Collection1.Count == 0) return false;
            return Remove(Collection1.Last.Value);
        }

        public void Show(int limit = 10)
        {
            Console.WriteLine($"LinkedList<Megacity>: {Collection1.Count}");
            Console.WriteLine($"LinkedList<string>: {Collection2.Count}");
            Console.WriteLine($"Dictionary<City, Megacity>: {Collection3.Count}");
            Console.WriteLine($"Dictionary<string, Megacity>: {Collection4.Count}");
            Console.WriteLine();

            int shown = 0;
            foreach (Megacity m in Collection1)
            {
                if (shown++ >= limit)
                {
                    Console.WriteLine("...");
                    break;
                }
                m.Show();
            }
        }

        public void MeasureSearch()
        {
            if (Collection1.Count == 0)
            {
                Console.WriteLine("Коллекции пусты.");
                return;
            }

            const int repeats = 1000;

            Megacity first = Collection1.First.Value;
            Megacity middle = GetMiddle();
            Megacity last = Collection1.Last.Value;
            Megacity missing = new Megacity("Отсутствующий", 0, 1900, "Нет", 0, 0);

            var rows = new List<(string Name, long First, long Middle, long Last, long Missing, bool Ok)>();

            rows.Add(BuildLinkedListRow("LinkedList<Megacity>.Contains", Collection1,
                first, middle, last, missing, repeats));
            rows.Add(BuildLinkedListRow("LinkedList<string>.Contains", Collection2,
                first.ToString(), middle.ToString(), last.ToString(), missing.ToString(), repeats));
            rows.Add(BuildDictionaryKeyRow("Dictionary<City,Megacity>.ContainsKey", Collection3,
                first.BaseCity, middle.BaseCity, last.BaseCity, missing.BaseCity, repeats));
            rows.Add(BuildDictionaryKeyRow("Dictionary<string,Megacity>.ContainsKey", Collection4,
                first.ToString(), middle.ToString(), last.ToString(), missing.ToString(), repeats));
            rows.Add(BuildDictionaryValueRow("Dictionary<City,Megacity>.ContainsValue", Collection3,
                first, middle, last, missing, repeats));

            Console.WriteLine($"Время поиска в тиках (суммарно за {repeats} повторов):");
            Console.WriteLine($"{"Метод",-42}{"Первый",10}{"Центр",10}{"Последний",12}{"Нет",10}{"Проверка",10}");
            foreach (var row in rows)
            {
                Console.WriteLine($"{row.Name,-42}{row.First,10}{row.Middle,10}{row.Last,12}{row.Missing,10}{(row.Ok ? "OK" : "ОШИБКА"),10}");
            }
        }

        private Megacity GetMiddle()
        {
            int index = Collection1.Count / 2;
            int current = 0;
            foreach (Megacity m in Collection1)
            {
                if (current++ == index) return m;
            }
            return Collection1.Last.Value;
        }

        private static (string, long, long, long, long, bool) BuildLinkedListRow<T>(
            string name, LinkedList<T> list, T first, T middle, T last, T missing, int repeats)
        {
            var a = MeasureContains(list, first, repeats);
            var b = MeasureContains(list, middle, repeats);
            var c = MeasureContains(list, last, repeats);
            var d = MeasureContains(list, missing, repeats);
            bool ok = a.Found && b.Found && c.Found && !d.Found;
            return (name, a.Ticks, b.Ticks, c.Ticks, d.Ticks, ok);
        }

        private static (string, long, long, long, long, bool) BuildDictionaryKeyRow<TKey, TValue>(
            string name, Dictionary<TKey, TValue> dict, TKey first, TKey middle, TKey last, TKey missing, int repeats)
        {
            var a = MeasureContainsKey(dict, first, repeats);
            var b = MeasureContainsKey(dict, middle, repeats);
            var c = MeasureContainsKey(dict, last, repeats);
            var d = MeasureContainsKey(dict, missing, repeats);
            bool ok = a.Found && b.Found && c.Found && !d.Found;
            return (name, a.Ticks, b.Ticks, c.Ticks, d.Ticks, ok);
        }

        private static (string, long, long, long, long, bool) BuildDictionaryValueRow<TKey, TValue>(
            string name, Dictionary<TKey, TValue> dict, TValue first, TValue middle, TValue last, TValue missing, int repeats)
        {
            var a = MeasureContainsValue(dict, first, repeats);
            var b = MeasureContainsValue(dict, middle, repeats);
            var c = MeasureContainsValue(dict, last, repeats);
            var d = MeasureContainsValue(dict, missing, repeats);
            bool ok = a.Found && b.Found && c.Found && !d.Found;
            return (name, a.Ticks, b.Ticks, c.Ticks, d.Ticks, ok);
        }

        private static (long Ticks, bool Found) MeasureContains<T>(LinkedList<T> list, T item, int repeats)
        {
            bool found = list.Contains(item);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < repeats; i++)
                found = list.Contains(item);
            sw.Stop();
            return (sw.ElapsedTicks, found);
        }

        private static (long Ticks, bool Found) MeasureContainsKey<TKey, TValue>(
            Dictionary<TKey, TValue> dict, TKey key, int repeats)
        {
            bool found = dict.ContainsKey(key);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < repeats; i++)
                found = dict.ContainsKey(key);
            sw.Stop();
            return (sw.ElapsedTicks, found);
        }

        private static (long Ticks, bool Found) MeasureContainsValue<TKey, TValue>(
            Dictionary<TKey, TValue> dict, TValue value, int repeats)
        {
            bool found = dict.ContainsValue(value);
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < repeats; i++)
                found = dict.ContainsValue(value);
            sw.Stop();
            return (sw.ElapsedTicks, found);
        }
    }
}
