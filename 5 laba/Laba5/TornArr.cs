using System;

namespace Laba5
{
    class TornArr
    {
        public int rows;
        private int[][] array;

        public TornArr()
        {
            rows = 0;
            array = null;
        }

        private bool IsEmpty()
        {
            return array == null || array.Length == 0;
        }

        private bool CheckEmpty()
        {
            if (IsEmpty())
            {
                Console.WriteLine("Ошибка! Массив пуст. Сначала создайте массив.");
                return true;
            }
            return false;
        }

        public void CreateArr()
        {
            rows = Input.ReadInt("Введите количество строк: ", 1, 1000);
            array = new int[rows][];

            int cols;
            for (int i = 0; i < rows; i++)
            {
                cols = Input.ReadInt($"Введите кол-во элементов в строке {i + 1}: ", 1, 1000);
                array[i] = new int[cols];
            }
        }

        public void FillManual()
        {
            if (CheckEmpty())
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = Input.ReadInt($"Элемент[{i}][{j}] = ");
                }
            }
        }

        public void FillRandom(int min = 10, int max = 100)
        {
            if (CheckEmpty())
            {
                return;
            }

            if (min >= max)
            {
                Console.WriteLine("Ошибка! Минимум должен быть меньше максимума.");
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    array[i][j] = Input.rand.Next(min, max);
                }
            }
        }

        public void PrintArr()
        {
            PrintArr("Массив:");
        }

        public void PrintArr(string title)
        {
            if (IsEmpty())
            {
                Console.WriteLine("Массив пуст!");
                return;
            }

            Console.WriteLine(title);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    Console.WriteLine("(строка пуста)");
                    continue;
                }

                for (int j = 0; j < array[i].Length; j++)
                {
                    Console.Write(array[i][j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void AddRowsToEnd(int K)
        {
            if (CheckEmpty())
            {
                return;
            }

            if (K <= 0 || K > 1000)
            {
                Console.WriteLine("Ошибка! Некорректное кол-во добавляемых строк.");
                return;
            }

            int[][] newArray = new int[rows + K][];

            for (int i = 0; i < rows; i++)
            {
                newArray[i] = array[i];
            }

            for (int i = rows; i < rows + K; i++)
            {
                int newCols;
                newCols = Input.ReadInt($"Введите кол-во элементов для новой строки {i}: ", 1, 1000);

                newArray[i] = new int[newCols];

                for (int j = 0; j < newCols; j++)
                {
                    newArray[i][j] = Input.rand.Next(10, 100);
                }
            }

            array = newArray;
            rows = rows + K;
        }

        public void DeleteRowsFromEnd(int K)
        {
            if (CheckEmpty())
            {
                return;
            }

            if (K <= 0)
            {
                Console.WriteLine("Ошибка! Кол-во удаляемых строк должно быть больше 0.");
                return;
            }

            if (K > rows)
            {
                Console.WriteLine($"Ошибка! Нельзя удалить {K} строк: в массиве всего {rows}.");
                return;
            }

            if (K == rows)
            {
                array = null;
                rows = 0;
                Console.WriteLine("Все строки удалены, массив пуст.");
                return;
            }

            rows = rows - K;
            Array.Resize(ref array, rows);
        }

        private void GetMinMax(out int min, out int max)
        {
            min = array[0][0];
            max = array[0][0];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] < min)
                    {
                        min = array[i][j];
                    }
                    if (array[i][j] > max)
                    {
                        max = array[i][j];
                    }
                }
            }
        }

        public void PrintInfo()
        {
            if (CheckEmpty())
            {
                return;
            }

            int maxLen = 0;
            for (int i = 0; i < rows; i++)
            {
                if (array[i].Length > maxLen)
                {
                    maxLen = array[i].Length;
                }
            }

            int min, max;
            GetMinMax(out min, out max);

            Console.WriteLine($"Кол-во строк: {rows}");
            Console.WriteLine($"Длина самой длинной строки: {maxLen}");
            Console.WriteLine($"Минимальный элемент: {min}, максимальный: {max}");
        }
    }
}