using System;

namespace Lab1
{
    public class Program
    {
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine("\n _ __ ___   ___ _ __  _   _ \r\n| '_ ` _ \\ / _ \\ '_ \\| | | |\r\n| | | | | |  __/ | | | |_| |\r\n|_| |_| |_|\\___|_| |_|\\__,_|");
                Console.WriteLine("Первое Задание - 1");
                Console.WriteLine("Второе Задание - 2");
                Console.WriteLine("Третье Задание - 3");
                Console.WriteLine("Выход - 0");

                int choice = ReadInt("Введите число: ");
                switch (choice)
                {
                    case 1:
                        new Task1().Run();
                        break;
                    case 2:
                        new Task2().Run();
                        break;
                    case 3:
                        new Task3().Run();
                        break;
                    case 0:
                        Console.WriteLine("Работа программы завершена.");
                        return;
                    default:
                        Console.WriteLine("Некоректный ввод! Пожалуйста, выберите пункт из меню.");
                        break;
                }
            }
        }

        public static int ReadInt(string msg)
        {
            int val;
            Console.Write(msg);
            while (!int.TryParse(Console.ReadLine(), out val))
            {
                Console.Write("Ошибка! Пожалуйста, введите снова: ");
            }
            return val;
        }

        public static double ReadDouble(string msg)
        {
            double val;
            Console.Write(msg);
            while (!double.TryParse(Console.ReadLine(), out val))
            {
                Console.Write("Ошибка! Пожалуйста, введите снова: ");
            }
            return val;
        }
    }

    public class Task1
    {
        public void Run()
        {
            int n = Program.ReadInt("n? ");
            int m = Program.ReadInt("m? ");

            int n1 = n;
            int mul = Mul(n1, m);
            Console.WriteLine($"n={n1}  m={m}  n++*m = {mul}");

            int n2 = n;
            bool less = Less(n2, m);
            Console.WriteLine($"n={n2}  m={m}  n++<m = {less}");

            int m2 = m;
            bool greater = Greater(n, m2);
            Console.WriteLine($"m={m2}  n={n}  --m>n = {greater}");

            int x = Program.ReadInt("Введите число x: ");
            if (x < 0)
            {
                Console.WriteLine($"x={x,3}: Ошибка! Это значение нельзя использовать.");
            }
            else
            {
                Console.WriteLine($"x={x,3}: {Calc(x)}");
            }
        }

        private int Mul(int n, int m)
        {
            return n++ * m;
        }

        private bool Less(int n, int m)
        {
            return n++ < m;
        }

        private bool Greater(int n, int m)
        {
            return --m > n;
        }

        private double Calc(double x)
        {
            return Math.Pow(2, -x) * Math.Sqrt(x + Math.Pow(Math.Abs(x), 1.0 / 4.0));
        }
    }

    public class Task2
    {
        public void Run()
        {
            double x = Program.ReadDouble("Введите значение pointX: ");
            double y = Program.ReadDouble("Введите значение pointY: ");

            bool inside = InArea(x, y);
            Console.WriteLine(inside
                ? "Точка принадлежит заштрихованной области."
                : "Точка не принадлежит заштрихованной области.");
        }

        private bool InArea(double x, double y)
        {
            return (x >= 0 && y <= 0 && y >= 5.0 / 3.0 * x - 5)
                || (x <= 0 && y <= 5.0 / 7.0 * x + 5 && y >= -5.0 / 7.0 * x - 5);
        }
    }

    public class Task3
    {
        public void Run()
        {
            float a1 = 1000f;
            float b1 = 0.0001f;
            Console.WriteLine("float = " + CalcF(a1, b1));

            double a2 = 1000;
            double b2 = 0.0001;
            Console.WriteLine("double = " + CalcD(a2, b2));
        }

        private float CalcF(float a, float b)
        {
            float d = a - b;
            float d3 = d * d * d;
            float a3 = a * a * a;
            float num = d3 - a3;

            float b2 = b * b;
            float b3 = b * b * b;
            float a2 = a * a;
            float den = 3 * a * b2 - b3 - 3 * a2 * b;

            return num / den;
        }

        private double CalcD(double a, double b)
        {
            double d = a - b;
            double d3 = Math.Pow(d, 3);
            double a3 = Math.Pow(a, 3);
            double num = d3 - a3;

            double b2 = Math.Pow(b, 2);
            double b3 = Math.Pow(b, 3);
            double a2 = Math.Pow(a, 2);
            double den = 3 * a * b2 - b3 - 3 * a2 * b;

            return num / den;
        }
    }
}