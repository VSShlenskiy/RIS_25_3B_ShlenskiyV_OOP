using System;

namespace Laba2
{
    class Program
    {
        static void Main()
        {
            int K = 10;
            double A = 0.1;
            double B = 1.0;
            int N = 35;
            double Eps = 0.0001;

            Console.WriteLine("X\t\tSN\t\tSE\t\tY");

            for (int i = 0; i <= K; i++)
            {
                double x = A + i * (B - A) / K;
                Calculator calc = new Calculator(x, N, Eps);

                double SN = calc.SumN();
                double SE = calc.SumEps();
                double Y = (1 - x * x / 2) * Math.Cos(x) - x / 2 * Math.Sin(x);

                Console.WriteLine($"X = {x:F4}  SN = {SN:F6}  SE = {SE:F6}  Y = {Y:F6}");
            }
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
