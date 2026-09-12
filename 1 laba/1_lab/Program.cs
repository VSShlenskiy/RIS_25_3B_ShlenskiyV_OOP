using System;

namespace Lab1
{
    public class Program 
    {
        static void Main()
        {
            while (true) 
            {
                Console.WriteLine("\n _ __ ___   ___ _ __  _   _ \r\n| '_ ` _ \\ / _ \\ '_ \\| | | |\r\n| | | | | |  __/ | | | |_| |\r\n|_| |_| |_|\\___|_| |_|\\__,_|");
                Console.WriteLine("TaskOne - 1");
                Console.WriteLine("TaskTwo - 2");
                Console.WriteLine("TaskThree - 3");
                Console.WriteLine("Exit - 0");
                Console.Write("Enter number: ");

                int q = 0;
                while (!int.TryParse(Console.ReadLine(), out q))
                {
                    Console.Write("Invalid input! Enter number: ");
                }

                switch (q) 
                {
                    case 1:
                        FirstTask objectTaskOne = new FirstTask();
                        objectTaskOne.Run();
                        break;
                    case 2:
                        SecondTask objectTaskTwo = new SecondTask();
                        objectTaskTwo.Run();
                        break;
                    case 3:
                        ThirdTask objectTaskThree = new ThirdTask();
                        objectTaskThree.Run();
                        break;
                    case 0:
                        Console.WriteLine("The program is completed.");
                        return;
                    default:
                        Console.WriteLine("Invalid input! Please select an item from the menu.");
                        break;
                }
            }
        }
    }

    public class FirstTask
    {
        public void Run()
        {
            Console.Write("n? ");
            int firstNumber = 0;
            while (!int.TryParse(Console.ReadLine(), out firstNumber))
            {
                Console.Write("Error! Please re-enter n: ");
            }

            Console.Write("m? ");
            int secondNumber = 0;
            while (!int.TryParse(Console.ReadLine(), out secondNumber))
            {
                Console.Write("Error! Please re-enter m: ");
            }

            int firstNumberCopy = firstNumber;
            int result1 = ProductWithPostIncrement(firstNumberCopy, secondNumber);
            Console.WriteLine($"n={firstNumberCopy}  m={secondNumber}  n++*m = {result1}");

            int firstNumberCopy2 = firstNumber;
            bool result2 = LessThanWithPostIncrement(firstNumberCopy2, secondNumber);
            Console.WriteLine($"n={firstNumberCopy2}  m={secondNumber}  n++<m = {result2}");

            int secondNumberCopy = secondNumber;
            bool result3 = GreaterThanWithPreDecrement(firstNumber, secondNumberCopy);
            Console.WriteLine($"m={secondNumberCopy}  n={firstNumber}  --m>n = {result3}");

            for (int x = -10; x < 10; x++)
            {
                if (x < 0)
                {
                    Console.WriteLine($"x={x,3}: Error! This value cannot be used.");
                }
                else
                {
                    Console.WriteLine($"x={x,3}: {PowerWithRoot(x)}");
                }
            }
        }

        private int ProductWithPostIncrement(int n, int m)
        {
            return n++ * m;
        }

        private bool LessThanWithPostIncrement(int n, int m)
        {
            return n++ < m;
        }

        private bool GreaterThanWithPreDecrement(int n, int m)
        {
            return --m > n;
        }

        private double PowerWithRoot(double x)
        {
            return Math.Pow(2, -x) * Math.Sqrt(x + Math.Pow(Math.Abs(x), 1.0 / 4.0));
        }
    }

    public class SecondTask 
    {
        public void Run() 
        {
            Console.WriteLine("Enter value pointX: ");
            double pointX = 0d;
            while (!double.TryParse(Console.ReadLine(), out pointX))
            {
                Console.Write("Error! Please re-enter the number: ");
            }

            Console.WriteLine("Enter value pointY: ");
            double pointY = 0d;
            while (!double.TryParse(Console.ReadLine(), out pointY))
            {
                Console.Write("Error! Please re-enter the number: ");
            }

            bool isInArea = IsPointInArea(pointX, pointY);
            Console.WriteLine(isInArea ? "The point belongs to the shaded area." : "The point does not belong to the shaded area.");
        }

        private bool IsPointInArea(double pointX, double pointY) 
        {
            if ((pointX >= 0 && pointY <= 0 && pointY >= 5.0 / 3.0 * pointX - 5) 
                || (pointX <= 0 && pointY <= (5.0 / 7.0) * pointX + 5 && pointY >= (-5.0 / 7.0) * pointX - 5)) 
            {
                return true;
            }
            return false;
        }
    }

    public class ThirdTask
    {
        public void Run()
        {
            float a1 = 1000f;
            float b1 = 0.0001f;
            Console.WriteLine("float = " + CalculateFloat(a1, b1));

            double a2 = 1000;
            double b2 = 0.0001;
            Console.WriteLine("double = " + CalculateDouble(a2, b2));
        }

        private float CalculateFloat(float a1, float b1)
        {
            float diff = a1 - b1;
            float diffCubed = diff * diff * diff;
            float aCubed = a1 * a1 * a1;
            float numerator = diffCubed - aCubed;

            float bSquared = b1 * b1;
            float bCubed = b1 * b1 * b1;
            float aSquared = a1 * a1;
            float denominator = 3 * a1 * bSquared - bCubed - 3 * aSquared * b1;

            float result = numerator / denominator;
            return result;
        }

        private double CalculateDouble(double a2, double b2)
        {
            double diff = a2 - b2;
            double diffCubed = Math.Pow(diff, 3);
            double aCubed = Math.Pow(a2, 3);
            double numerator = diffCubed - aCubed;

            double bSquared = Math.Pow(b2, 2);
            double bCubed = Math.Pow(b2, 3);
            double aSquared = Math.Pow(a2, 2);
            double denominator = 3 * a2 * bSquared - bCubed - 3 * aSquared * b2;

            double result = numerator / denominator;
            return result;
        }
    }
}
