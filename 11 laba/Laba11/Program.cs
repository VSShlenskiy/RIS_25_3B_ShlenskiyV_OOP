using System;

namespace Laba11
{
    internal class Program
    {
        static void Main()
        {
            Task1Stack task1 = new Task1Stack();
            Task2Dictionary task2 = new Task2Dictionary();
            Task3TestCollections task3 = new Task3TestCollections();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Задание 1 — Stack (System.Collections)");
                Console.WriteLine("2. Задание 2 — Dictionary<string, Place> (System.Collections.Generic)");
                Console.WriteLine("3. Задание 3 — TestCollections (LinkedList<T>, Dictionary<K,T>)");
                Console.WriteLine("0. Выход");
                int choice = Utils.ReadInt("Выбор: ", 0, 3);
                Console.WriteLine();

                switch (choice)
                {
                    case 1: task1.Run(); break;
                    case 2: task2.Run(); break;
                    case 3: task3.Run(); break;
                    case 0: return;
                }
            }
        }
    }
}
