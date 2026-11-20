using System;
using System.Collections;
using PlacesLib;

namespace Laba11
{
    internal class Task1Stack
    {
        private readonly Stack stack = new Stack();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine($"=== Задание 1. Stack (элементов: {stack.Count}) ===");
                Console.WriteLine("1. Добавить случайный объект");
                Console.WriteLine("2. Добавить объект вручную");
                Console.WriteLine("3. Удалить верхний элемент (Pop)");
                Console.WriteLine("4. Показать все элементы (foreach)");
                Console.WriteLine("5. Запрос 1: количество элементов заданного вида");
                Console.WriteLine("6. Запрос 2: печать элементов заданного вида");
                Console.WriteLine("7. Запрос 3: объект с максимальным населением");
                Console.WriteLine("8. Клонировать коллекцию");
                Console.WriteLine("9. Отсортировать коллекцию");
                Console.WriteLine("10. Найти элемент");
                Console.WriteLine("11. Очистить коллекцию");
                Console.WriteLine("0. Назад");
                int choice = Utils.ReadInt("Выбор: ", 0, 11);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: AddRandom(); break;
                    case 2: AddManual(); break;
                    case 3: RemoveTop(); break;
                    case 4: ShowAll(); break;
                    case 5: QueryCount(); break;
                    case 6: QueryPrint(); break;
                    case 7: QueryMaxPopulation(); break;
                    case 8: CloneCollection(); break;
                    case 9: SortCollection(); break;
                    case 10: Search(); break;
                    case 11: stack.Clear(); Console.WriteLine("Коллекция очищена."); break;
                    case 0: return;
                }

                Utils.Pause();
            }
        }

        private void AddRandom()
        {
            Place p = Utils.CreateRandomPlace();
            stack.Push(p);
            Console.Write("Добавлен элемент: ");
            p.Show();
        }

        private void AddManual()
        {
            Place p = Utils.CreateManualPlace();
            stack.Push(p);
            Console.Write("Добавлен элемент: ");
            p.Show();
        }

        private void RemoveTop()
        {
            if (stack.Count == 0)
            {
                Console.WriteLine("Стек пуст.");
                return;
            }

            Place p = (Place)stack.Pop();
            Console.Write("Удалён верхний элемент: ");
            p.Show();
        }

        private void ShowAll()
        {
            if (stack.Count == 0)
            {
                Console.WriteLine("Стек пуст.");
                return;
            }

            Console.WriteLine("Элементы стека (от вершины к основанию):");
            int number = 1;
            foreach (object item in stack)
            {
                Console.Write($"{number++}. ");
                ((Place)item).Show();
            }
        }

        private void QueryCount()
        {
            int type = Utils.ReadPlaceType();
            int count = 0;
            foreach (object item in stack)
                if (Utils.IsExactType((Place)item, type)) count++;

            Console.WriteLine($"Элементов вида «{Utils.TypeNames[type]}»: {count}");
        }

        private void QueryPrint()
        {
            int type = Utils.ReadPlaceType();
            Console.WriteLine($"Элементы вида «{Utils.TypeNames[type]}»:");
            bool found = false;
            foreach (object item in stack)
            {
                Place p = (Place)item;
                if (Utils.IsExactType(p, type))
                {
                    p.Show();
                    found = true;
                }
            }

            if (!found) Console.WriteLine("Таких элементов нет.");
        }

        private void QueryMaxPopulation()
        {
            Place best = null;
            int bestPopulation = 0;
            foreach (object item in stack)
            {
                Place p = (Place)item;
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
            Stack clone = (Stack)stack.Clone();
            Console.WriteLine($"Оригинал: {stack.Count} элементов, клон: {clone.Count} элементов.");
            Console.WriteLine("Содержимое клона:");
            foreach (object item in clone)
                ((Place)item).Show();

            if (clone.Count > 0)
            {
                clone.Pop();
                Console.WriteLine($"После Pop из клона: оригинал = {stack.Count}, клон = {clone.Count}.");
            }

            Console.WriteLine("Клонирование поверхностное: структура независима, объекты общие.");
        }

        private void SortCollection()
        {
            if (stack.Count < 2)
            {
                Console.WriteLine("Нечего сортировать.");
                return;
            }

            object[] items = stack.ToArray();
            Array.Sort(items);
            stack.Clear();
            for (int i = items.Length - 1; i >= 0; i--)
                stack.Push(items[i]);

            Console.WriteLine("Стек отсортирован по названию:");
            ShowAll();
        }

        private void Search()
        {
            if (stack.Count == 0)
            {
                Console.WriteLine("Стек пуст.");
                return;
            }

            string name = Utils.ReadString("Введите название для поиска: ");
            bool found = false;
            foreach (object item in stack)
            {
                Place p = (Place)item;
                if (string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write("Найден: ");
                    p.Show();
                    found = true;
                }
            }

            Console.WriteLine(found ? "Поиск завершён." : "Элемент не найден.");

            Place top = (Place)stack.Peek();
            Place copy = (Place)top.Clone();
            Console.WriteLine($"Contains(копия верхнего элемента) = {stack.Contains(copy)}");
        }
    }
}
