using System;

namespace Laba9
{
    class Program
    {
        static void Main()
        {
            Input.Demo1();
            Input.Demo2();
            Input.Demo3();
        }

        public static Time Max(TimeArray array)
        {
            if (array == null || array.Length == 0)
            {
                throw new ArgumentException("Массив пуст");
            }

            Time max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }
            return max;
        }
    }
}