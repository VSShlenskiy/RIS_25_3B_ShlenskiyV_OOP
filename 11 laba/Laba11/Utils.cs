using System;
using PlacesLib;

namespace Laba11
{
    internal static class Utils
    {
        private static readonly Random rnd = new Random();

        public static readonly string[] TypeNames =
        {
            "Место", "Адрес", "Город", "Мегаполис", "Область"
        };

        public static Place CreateRandomPlace()
        {
            return CreateRandomPlace(rnd.Next(5));
        }

        public static Place CreateRandomPlace(int type)
        {
            Place p;
            switch (type)
            {
                case 1: p = new Address(); break;
                case 2: p = new City(); break;
                case 3: p = new Megacity(); break;
                case 4: p = new Region(); break;
                default: p = new Place(); break;
            }
            p.RandomInit();
            return p;
        }

        public static Place CreateManualPlace()
        {
            int type = ReadPlaceType("Тип создаваемого объекта: ");
            Place p;
            switch (type)
            {
                case 1: p = new Address(); break;
                case 2: p = new City(); break;
                case 3: p = new Megacity(); break;
                case 4: p = new Region(); break;
                default: p = new Place(); break;
            }
            p.Init();
            return p;
        }

        public static int ReadPlaceType(string prompt = "Вид элементов: ")
        {
            Console.WriteLine("0 - Место, 1 - Адрес, 2 - Город, 3 - Мегаполис, 4 - Область");
            return ReadInt(prompt, 0, 4);
        }

        public static bool IsExactType(Place p, int type)
        {
            switch (type)
            {
                case 0: return p.GetType() == typeof(Place);
                case 1: return p.GetType() == typeof(Address);
                case 2: return p.GetType() == typeof(City);
                case 3: return p.GetType() == typeof(Megacity);
                case 4: return p.GetType() == typeof(Region);
                default: return false;
            }
        }

        public static int GetPopulation(Place p)
        {
            if (p is Region r) return r.Population;
            if (p is Megacity m) return m.Population;
            if (p is City c) return c.Population;
            return 0;
        }

        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Введите целое число от {min} до {max}.");
            }
        }

        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            return (Console.ReadLine() ?? "").Trim();
        }

        public static void Pause()
        {
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
