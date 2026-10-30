using System;

namespace Laba9
{
    class Input
    {
        public static void Demo1()
        {
            Console.WriteLine("=== Часть 1 ===");

            Time t1 = new Time(10, 40);
            Time t2 = new Time(3, 50);
            Time t0 = new Time();

            Console.WriteLine($"t1: Hours = {t1.Hours}, Minutes = {t1.Minutes} ({t1})");
            Console.WriteLine($"t2: Hours = {t2.Hours}, Minutes = {t2.Minutes} ({t2})");
            Console.WriteLine($"t0 (конструктор без параметров) = {t0}");

            Time r1 = Time.Subtract(t1, t2);
            Time r2 = t1.Subtract(t2);
            Console.WriteLine($"t1 - t2 (статическая функция) = {r1}");
            Console.WriteLine($"t1 - t2 (метод класса)        = {r2}");

            Time r3 = t2.Subtract(t1);
            Console.WriteLine($"t2 - t1 (результат < 0 -> 00:00) = {r3}");

            Console.WriteLine($"Создано объектов Time: {Time.Count}");
        }

        public static void Demo2()
        {
            Console.WriteLine("\n=== Часть 2 ===");

            Time t = new Time(10, 59);
            Console.WriteLine($"t = {t}");

            Time old = t++;
            Console.WriteLine($"t++ вернуло {old}, теперь t = {t}");
            old = t--;
            Console.WriteLine($"t-- вернуло {old}, теперь t = {t}");
            Console.WriteLine($"++t = {++t}");
            Console.WriteLine($"--t = {--t}");

            Time zero = new Time();
            zero--;
            Console.WriteLine($"00:00 после -- = {zero} (меньше 0 быть не может)");

            int minutes = t;
            Console.WriteLine($"int (неявно): {t} = {minutes} мин");

            Console.WriteLine($"bool (явно): (bool){t} = {(bool)t}");
            Console.WriteLine($"bool (явно): (bool){zero} = {(bool)zero}");

            Time a = new Time(2, 15);
            Time b = new Time(2, 30);
            Console.WriteLine($"{a} < {b} = {a < b}");
            Console.WriteLine($"{a} > {b} = {a > b}");
            Console.WriteLine($"{b} > {a} = {b > a}");

            Console.WriteLine($"Создано объектов Time: {Time.Count}");
        }

        public static void Demo3()
        {
            Console.WriteLine("\n=== Часть 3 ===");

            TimeArray a1 = new TimeArray();
            Console.WriteLine("1) Пустой массив:");
            Console.WriteLine("   " + a1.View());

            TimeArray a2 = new TimeArray(6, 12);
            Console.WriteLine("\n2) Случайный массив (6 элементов, часы 0..12):");
            Console.WriteLine("   " + a2.View());
            PrintMax(a2);

            Console.WriteLine("\n3) Ввод с клавиатуры:");
            TimeArray a3 = ReadTimeArray();
            Console.WriteLine("   " + a3.View());
            PrintMax(a3);

            Console.WriteLine("\nИндексатор:");
            if (a2.Length > 0)
            {
                a2[0] = new Time(1, 5);
                Console.WriteLine($"   a2[0] после записи = {a2[0]}");
            }
            try
            {
                Console.WriteLine(a2[a2.Length]);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   Ошибка: {ex.Message}");
            }
            try
            {
                a2[-1] = new Time(0, 1);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   Ошибка: {ex.Message}");
            }

            Console.WriteLine($"\nСоздано массивов TimeArray: {TimeArray.Count}");
            Console.WriteLine($"Создано объектов Time: {Time.Count}");
        }

        private static void PrintMax(TimeArray array)
        {
            if (array.Length == 0)
            {
                Console.WriteLine("   Максимум: массив пуст");
                return;
            }
            Console.WriteLine($"   Максимум: {Program.Max(array)}");
        }

        public static int ReadInt(string prompt, int min)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int value) && value >= min)
                {
                    return value;
                }
                Console.WriteLine($"   Введите целое число >= {min}");
            }
        }

        public static Time ReadTime(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                int h = ReadInt("   Часы: ", 0);
                int m = ReadInt("   Минуты (0..59): ", 0);
                try
                {
                    return new Time(h, m);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"   Ошибка: {ex.Message}");
                }
            }
        }

        public static TimeArray ReadTimeArray()
        {
            int size = ReadInt("   Размер массива: ", 0);
            Time[] items = new Time[size];
            for (int i = 0; i < size; i++)
            {
                items[i] = ReadTime($"   Элемент [{i}]:");
            }
            return new TimeArray(items);
        }
    }
}