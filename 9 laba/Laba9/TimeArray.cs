using System;

namespace Laba9
{
    class TimeArray
    {
        private Time[] arr;
        private static int count = 0;
        private static Random rnd = new Random();

        public static int Count
        {
            get
            {
                return count;
            }
        }

        public int Length
        {
            get
            {
                return arr.Length;
            }
        }

        public TimeArray()
        {
            arr = new Time[0];
            count++;
        }

        public TimeArray(int size, int maxHours)
        {
            if (size < 0)
            {
                throw new ArgumentException("Размер массива не может быть < 0");
            }
            if (maxHours < 0)
            {
                throw new ArgumentException("Максимум часов не может быть < 0");
            }

            arr = new Time[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = new Time(rnd.Next(0, maxHours + 1), rnd.Next(0, 60));
            }
            count++;
        }

        public TimeArray(Time[] items)
        {
            if (items == null)
            {
                throw new ArgumentException("Массив элементов не задан");
            }

            arr = new Time[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                this[i] = items[i];
            }
            count++;
        }

        public Time this[int index]
        {
            get
            {
                CheckIndex(index);
                return arr[index];
            }
            set
            {
                CheckIndex(index);
                if (value == null)
                {
                    throw new ArgumentException("Элемент не может быть null");
                }
                arr[index] = value;
            }
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= arr.Length)
            {
                throw new ArgumentException($"Индекс {index} вне границ массива (0..{arr.Length - 1})");
            }
        }

        public string View()
        {
            if (arr.Length == 0)
            {
                return "(массив пуст)";
            }

            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result += $"[{i}] {arr[i]}";
                if (i < arr.Length - 1)
                {
                    result += "  ";
                }
            }
            return result;
        }
    }
}