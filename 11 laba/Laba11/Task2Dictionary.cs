using System;
using System.Collections.Generic;
using System.Linq;
using PlacesLib;

namespace Laba11
{
    internal class Task2Dictionary
    {
        private Dictionary<string, Place> dict = new Dictionary<string, Place>();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"=== Задание 2. Dictionary<string, Place> (элементов: {dict.Count}) ===");
                Console.WriteLine("1. Добавить случайный объект");
                Console.WriteLine("2. Добавить объект вручную");
                Console.WriteLine("3. Удалить элемент по ключу");
                Console.WriteLine("4. Показать все элементы (foreach)");
                Console.WriteLine("5. Запрос 1: количество элементов заданного вида");
                Console.WriteLine("6. Запрос 2: печать элементов заданного вида");
                Console.WriteLine("7. Запрос 3: объект с максимальным населением");
                Console.WriteLine("8. Клонировать коллекцию");
                Console.WriteLine("9. Сортировать по ключу");
                Console.WriteLine("10. Сортировать по названию значения");
                Console.WriteLine("11. Поиск по ключу (ContainsKey)");
                Console.WriteLine("12. Поиск по значению (ContainsValue)");
                Console.WriteLine("13. Очистить коллекцию");
                Console.WriteLine("0. Назад");
                int choice = Utils.ReadInt("Выбор: ", 0, 13);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: AddRandom(); break;
                    case 2: AddManual(); break;
                    case 3: RemoveByKey(); break;
                    case 4: ShowAll(); break;
                    case 5: QueryCount(); break;
                    case 6: QueryPrint(); break;
                    case 7: QueryMaxPopulation(); break;
                    case 8: CloneCollection(); break;
                    case 9: SortByKey(); break;
                    case 10: SortByValueName(); break;
                    case 11: SearchByKey(); break;
                    case 12: SearchByValue(); break;
                    case 13: dict.Clear(); Console.WriteLine("Коллекция очищена."); break;
                    case 0: return;
                }

                Utils.Pause();
            }
        }

        private string MakeUniqueKey(string baseName)
        {
            string key = baseName;
            int number = 2;
            while (dict.ContainsKey(key))
                key = $"{baseName} #{number++}";
            return key;
        }

        private void AddRandom()
        {
            Place p = Utils.CreateRandomPlace();
            string key = MakeUniqueKey(p.Name);
            dict.Add(key, p);
            Console.Write($"Добавлен элемент с ключом «{key}»: ");
            p.Show();
        }

        private void AddManual()
        {
            Place p = Utils.CreateManualPlace();
            string key = MakeUniqueKey(p.Name);
            dict.Add(key, p);
            Console.Write($"Добавлен элемент с ключом «{key}»: ");
            p.Show();
        }

        private void RemoveByKey()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            Console.WriteLine("Ключи: " + string.Join(", ", dict.Keys));
            string key = Utils.ReadString("Ключ для удаления: ");
            if (dict.Remove(key))
                Console.WriteLine("Элемент удалён.");
            else
                Console.WriteLine("Ключ не найден.");
        }

        private void ShowAll()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            Console.WriteLine("Элементы коллекции (foreach):");
            foreach (KeyValuePair<string, Place> pair in dict)
            {
                Console.Write($"[{pair.Key}] ");
                pair.Value.Show();
            }
        }

        private void QueryCount()
        {
            int type = Utils.ReadPlaceType();
            int count = 0;
            foreach (Place p in dict.Values)
                if (Utils.IsExactType(p, type)) count++;

            Console.WriteLine($"Элементов вида «{Utils.TypeNames[type]}»: {count}");
        }

        private void QueryPrint()
        {
            int type = Utils.ReadPlaceType();
            Console.WriteLine($"Элементы вида «{Utils.TypeNames[type]}»:");
            bool found = false;
            foreach (KeyValuePair<string, Place> pair in dict)
            {
                if (Utils.IsExactType(pair.Value, type))
                {
                    Console.Write($"[{pair.Key}] ");
                    pair.Value.Show();
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Таких элементов нет.");
        }

        private void QueryMaxPopulation()
        {
            Place best = null;
            int bestPopulation = 0;
            foreach (Place p in dict.Values)
            {
                int population = Utils.GetPopulation(p);
                if (population > bestPopulation)
                {
                    best = p;
                    bestPopulation = population;
                }
            }

            if (best == null)
            {
                Console.WriteLine("В коллекции нет объектов с населением.");
                return;
            }

            Console.WriteLine($"Максимальное население: {bestPopulation}");
            best.Show();
        }

        private void CloneCollection()
        {
            var clone = new Dictionary<string, Place>(dict);
            Console.WriteLine($"Оригинал: {dict.Count} элементов, клон: {clone.Count} элементов.");

            if (clone.Count > 0)
            {
                string firstKey = clone.Keys.First();
                clone.Remove(firstKey);
                Console.WriteLine($"После удаления «{firstKey}» из клона: оригинал = {dict.Count}, клон = {clone.Count}.");
            }

            Console.WriteLine("Клонирование поверхностное: значения — те же объекты.");
        }

        private void SortByKey()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            dict = dict.OrderBy(pair => pair.Key, StringComparer.Ordinal)
                       .ToDictionary(pair => pair.Key, pair => pair.Value);
            Console.WriteLine("Коллекция упорядочена по ключу:");
            ShowAll();
        }

        private void SortByValueName()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            dict = dict.OrderBy(pair => pair.Value.Name, StringComparer.Ordinal)
                       .ToDictionary(pair => pair.Key, pair => pair.Value);
            Console.WriteLine("Коллекция упорядочена по названию значения:");
            ShowAll();
        }

        private void SearchByKey()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            string key = Utils.ReadString("Ключ для поиска: ");
            if (dict.TryGetValue(key, out Place p))
            {
                Console.Write("Найдено: ");
                p.Show();
            }
            else
            {
                Console.WriteLine("Ключ не найден.");
            }
        }

        private void SearchByValue()
        {
            if (dict.Count == 0)
            {
                Console.WriteLine("Коллекция пуста.");
                return;
            }

            string name = Utils.ReadString("Название значения для поиска: ");
            bool found = false;
            foreach (KeyValuePair<string, Place> pair in dict)
            {
                if (string.Equals(pair.Value.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write($"[{pair.Key}] ");
                    pair.Value.Show();
                    found = true;
                }
            }

            Console.WriteLine(found ? "Поиск завершён." : "Элемент не найден.");

            Place sample = dict.Values.First();
            Place copy = (Place)sample.Clone();
            Console.WriteLine($"ContainsValue(копия первого элемента) = {dict.ContainsValue(copy)}");

            Place missing = new Place("Такого элемента нет");
            Console.WriteLine($"ContainsValue(отсутствующий элемент) = {dict.ContainsValue(missing)}");
        }
    }
}
