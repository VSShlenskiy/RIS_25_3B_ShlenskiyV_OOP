using System;

namespace Laba5
{
    static class Input
    {
        public static Random rand = new Random();

        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write("Ошибка! Введите корректное число: ");
            }
            return value;
        }
    }
}