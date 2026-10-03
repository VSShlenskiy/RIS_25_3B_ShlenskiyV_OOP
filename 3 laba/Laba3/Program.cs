using System;

namespace Laba2
{
    class Program
    {
        static void Main()
        {
            int K;
            Console.Write("Введите K: ");
            while (!int.TryParse(Console.ReadLine(), out K) || K <= 0)
            {
                Console.WriteLine("Неверно введено K. Попробуйте ещё раз: ");
            }

            Option option = new Option();
            option.K = K;

            Console.WriteLine("X\t\tSN\t\tSE\t\tY");

            for (int i = 0; i <= K; i++)
            {
                double x = option.GetX(i);
                Calculator calc = new Calculator(x, option.N, option.Eps);

                double SN = calc.SumN();
                double SE = calc.SumEps();
                double Y = option.Formula(x);

                Console.WriteLine($"X = {x:F4}  SN = {SN:F6}  SE = {SE:F6}  Y = {Y:F6}");
            }
        }
    }

    class Option
    {
        private double A = 0.1;
        private double B = 1.0;
        public int K;
        public int N = 35;
        public double Eps = 0.0001;

        public double GetX(int i)
        {
            return A + i * (B - A) / K;
        }

        public double Formula(double x)
        {
            return (1 - x * x / 2) * Math.Cos(x) - x / 2 * Math.Sin(x);
        }
    }

    class Calculator
    {
        private double X;
        private int N;
        private double Eps;

        public Calculator(double x, int n, double eps)
        {
            X = x;
            N = n;
            Eps = eps;
        }

        public double SumN()
        {
            double sum = 1.0;
            for (int m = 1; m <= N; m++)
            {
                sum += Element(m);
            }
            return sum;
        }

        public double SumEps()
        {
            double sum = 1.0;
            int m = 1;
            while (true)
            {
                double a = Element(m);
                sum += a;
                if (Math.Abs(a) < Eps)
                    break;
                m++;
            }
            return sum;
        }

        private double Element(int m)
        {
            if (m == 0)
                return 1.0;

            double sign = (m % 2 == 0) 
                ? 1.0 
                : -1.0;
            double power = Math.Pow(X, 2 * m);
            double fact = Factorial(2 * m);

            return (2 * m * m + 1) * sign * power / fact;
        }

        private double Factorial(int n)
        {
            double result = 1.0;
            for (int i = 2; i <= n; i++)
                result *= i;
            return result;
        }
    }
}
