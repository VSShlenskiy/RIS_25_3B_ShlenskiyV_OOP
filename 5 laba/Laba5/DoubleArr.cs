using System;

namespace Laba5
{
    class DoubleArr
    {
        public int rows;
        public int cols;
        private int[,] array;

        public DoubleArr()
        {
            rows = 0;
            cols = 0;
            array = null;
        }

        private bool IsEmpty()
        {
            return array == null || rows == 0 || cols == 0;
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
            rows = Input.ReadInt("Введите кол-во строк: ", 1, 1000);
            cols = Input.ReadInt("Введите кол-во столбцов: ", 1, 1000);
            array = new int[rows, cols];
        }

        public void FillManual()
        {
            if (CheckEmpty())
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = Input.ReadInt($"Элемент[{i}][{j}] = ");
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
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = Input.rand.Next(min, max);
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
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public void AddRows(int N, int K)
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

            if (N < 0 || N > rows)
            {
                Console.WriteLine($"Ошибка! Номер строки должен быть от 0 до {rows}.");
                return;
            }

            int[,] newArray = new int[rows + K, cols];

            Array.Copy(array, 0, newArray, 0, N * cols);

            for (int i = N; i < N + K; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newArray[i, j] = Input.rand.Next(10, 100);
                }
            }

            Array.Copy(array, N * cols, newArray, (N + K) * cols, (rows - N) * cols);

            array = newArray;
            rows = rows + K;
        }

        public void DeleteRows(int N, int K = 1)
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

            if (N < 0 || N > rows - K)
            {
                Console.WriteLine($"Ошибка! С позиции {N} нельзя удалить {K} строк (допустимая позиция: от 0 до {rows - K}).");
                return;
            }

            if (K == rows)
            {
                array = null;
                rows = 0;
                cols = 0;
                Console.WriteLine("Все строки удалены, массив пуст.");
                return;
            }

            int[,] newArray = new int[rows - K, cols];
            Array.Copy(array, 0, newArray, 0, N * cols);
            Array.Copy(array, (N + K) * cols, newArray, N * cols, (rows - N - K) * cols);

            array = newArray;
            rows = rows - K;
        }

        private int SumRow(int row, int col = 0)
        {
            if (col >= cols)
            {
                return 0;
            }
            return array[row, col] + SumRow(row, col + 1);
        }

        public void PrintInfo()
        {
            if (CheckEmpty())
            {
                return;
            }

            for (int i = 0; i < rows; i++)
            {
                Console.WriteLine($"Сумма элементов строки {i}: {SumRow(i)}");
            }
        }
    }
}