using System;

namespace Laba11
{
    internal class Task3TestCollections
    {
        private TestCollections collections = new TestCollections(1000);

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Задание 3. TestCollections ===");
                Console.WriteLine("1. Показать коллекции (первые 10 элементов)");
                Console.WriteLine("2. Добавить случайный элемент");
                Console.WriteLine("3. Удалить первый элемент");
                Console.WriteLine("4. Удалить последний элемент");
                Console.WriteLine("5. Измерить время поиска");
                Console.WriteLine("6. Пересоздать коллекции (1000 элементов)");
                Console.WriteLine("0. Назад");
                int choice = Utils.ReadInt("Выбор: ", 0, 6);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: collections.Show(10); break;
                    case 2: collections.AddRandom(); Console.WriteLine("Элемент добавлен во все четыре коллекции."); break;
                    case 3: Console.WriteLine(collections.RemoveFirst() ? "Первый элемент удалён." : "Коллекции пусты."); break;
                    case 4: Console.WriteLine(collections.RemoveLast() ? "Последний элемент удалён." : "Коллекции пусты."); break;
                    case 5: collections.MeasureSearch(); break;
                    case 6: collections = new TestCollections(1000); Console.WriteLine("Коллекции пересозданы."); break;
                    case 0: return;
                }

                Utils.Pause();
            }
        }
    }
}
