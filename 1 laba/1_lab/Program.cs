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
                Console.WriteLine("Третие Задание - 3");
                Console.WriteLine("Выход - 0");

                int menuChoice = ReadInt("Введите число: ");
                switch (menuChoice)
                {
                    case 1:
                        FirstTask firstTask = new FirstTask();
                        firstTask.Run();
                        break;
                    case 2:
                        SecondTask secondTask = new SecondTask();
                        secondTask.Run();
                        break;
                    case 3:
                        ThirdTask thirdTask = new ThirdTask();
                        thirdTask.Run();
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

        public static int ReadInt(string message)
        {
            int value;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Ошибка! Пожалуйста, введите снова.: ");
            }
            return value;
        }

        public static double ReadDouble(string message)
        {
            double value;
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Ошибка! Пожалуйста, введите снова.: ");
            }
            return value;
        }
    }

    public class FirstTask
    {
        public void Run()
        {
            int n = Program.ReadInt("n? ");

            int m = Program.ReadInt("m? ");

            int nForProduct = n;
            int productResult = GetProductWithPostIncrement(nForProduct, m);
            Console.WriteLine($"n={nForProduct}  m={m}  n++*m = {productResult}");

            int nForComparison = n;
            bool isLess = IsLessThanWithPostIncrement(nForComparison, m);
            Console.WriteLine($"n={nForComparison}  m={m}  n++<m = {isLess}");

            int mForComparison = m;
            bool isGreater = IsGreaterThanWithPreDecrement(n, mForComparison);
            Console.WriteLine($"m={mForComparison}  n={n}  --m>n = {isGreater}");

            for (int x = -10; x < 10; x++)
            {
                if (x < 0)
                {
                    Console.WriteLine($"x={x,3}: Ошибка! Это значение нельзя использовать..");
                }
                else
                {
                    Console.WriteLine($"x={x,3}: {CalculatePowerWithRoot(x)}");
                }
            }
        }

        private int GetProductWithPostIncrement(int n, int m)
        {
            return n++ * m;
        }

        private bool IsLessThanWithPostIncrement(int n, int m)
        {
            return n++ < m;
        }

        private bool IsGreaterThanWithPreDecrement(int n, int m)
        {
            return --m > n;
        }

        private double CalculatePowerWithRoot(double x)
        {
            return Math.Pow(2, -x) * Math.Sqrt(x + Math.Pow(Math.Abs(x), 1.0 / 4.0));
        }
    }

    public class SecondTask 
    {
        public void Run() 
        {
            double pointX = Program.ReadDouble("Введите значение pointX: ");

            double pointY = Program.ReadDouble("Введите значение pointY: ");

            bool isInArea = IsPointInArea(pointX, pointY);
            Console.WriteLine(isInArea
                ? "Точка принадлежит заштрихованной области."
                : "Точка не принадлежит заштрихованной области.");
        }

        private bool IsPointInArea(double pointX, double pointY)
        {
            return (pointX >= 0 && pointY <= 0 && pointY >= 5.0 / 3.0 * pointX - 5)
                || (pointX <= 0 && pointY <= 5.0 / 7.0 * pointX + 5 && pointY >= -5.0 / 7.0 * pointX - 5);
        }
    }

    public class ThirdTask
    {
        public void Run()
        {
            float aFloat = 1000f;
            float bFloat = 0.0001f;
            Console.WriteLine("float = " + CalculateExpressionFloat(aFloat, bFloat));

            double aDouble = 1000;
            double bDouble = 0.0001;
            Console.WriteLine("double = " + CalculateExpressionDouble(aDouble, bDouble));
        }

        private float CalculateExpressionFloat(float a, float b)
        {
            float difference = a - b;
            float differenceCubed = difference * difference * difference;
            float aCubed = a * a * a;
            float numerator = differenceCubed - aCubed;

            float bSquared = b * b;
            float bCubed = b * b * b;
            float aSquared = a * a;
            float denominator = 3 * a * bSquared - bCubed - 3 * aSquared * b;

            return numerator / denominator;
        }

        private double CalculateExpressionDouble(double a, double b)
        {
            double difference = a - b;
            double differenceCubed = Math.Pow(difference, 3);
            double aCubed = Math.Pow(a, 3);
            double numerator = differenceCubed - aCubed;

            double bSquared = Math.Pow(b, 2);
            double bCubed = Math.Pow(b, 3);
            double aSquared = Math.Pow(a, 2);
            double denominator = 3 * a * bSquared - bCubed - 3 * aSquared * b;

            return numerator / denominator;
        }
    }
}
