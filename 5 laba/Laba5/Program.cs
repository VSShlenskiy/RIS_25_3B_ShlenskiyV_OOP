using System;

namespace Laba5
{
    class Program
    {
        static void Main()
        {
            DoubleArr arr = new DoubleArr();
            TornArr tornArr = new TornArr();
            WorkToString enterString = new WorkToString();

            while (true)
            {
                try
                {
                    Console.WriteLine("\n--- МЕНЮ ---");
                    Console.WriteLine("1 - Двумерный массив");
                    Console.WriteLine("2 - Рваный массив");
                    Console.WriteLine("3 - Работа со строкой");
                    Console.WriteLine("0 - Закончить");

                    int q = Input.ReadInt("Выберите пункт: ", 0, 3);

                    switch (q)
                    {
                        case 0:
                            return;
                        case 1:
                            DoubleArrMenu(arr);
                            break;
                        case 2:
                            TornArrMenu(tornArr);
                            break;
                        case 3:
                            StringMenu(enterString);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
                }
            }
        }

        static void DoubleArrMenu(DoubleArr arr)
        {
            while (true)
            {
                Console.WriteLine("\n1 - Создать массив");
                Console.WriteLine("2 - Заполнить вручную");
                Console.WriteLine("3 - Заполнить рандомно (10..99)");
                Console.WriteLine("4 - Заполнить рандомно (свой диапазон)");
                Console.WriteLine("5 - Вывести массив");
                Console.WriteLine("6 - Добавить K строк, начиная со строки N");
                Console.WriteLine("7 - Удалить строки");
                Console.WriteLine("8 - Суммы строк");
                Console.WriteLine("0 - Назад");

                int q = Input.ReadInt("Выберите пункт: ", 0, 8);

                try
                {
                    switch (q)
                    {
                        case 0:
                            return;

                        case 1:
                            arr.CreateArr();
                            break;

                        case 2:
                            arr.FillManual();
                            break;

                        case 3:
                            arr.FillRandom();
                            break;

                        case 4:
                            int min = Input.ReadInt("Введите минимальное значение: ");
                            int max = Input.ReadInt("Введите максимальное значение (не включая): ");
                            arr.FillRandom(min, max);
                            break;

                        case 5:
                            arr.PrintArr();
                            break;

                        case 6:
                            int K = Input.ReadInt("Введите кол-во строк для добавления: ", 1);
                            int N = Input.ReadInt("Введите номер строки, с которой добавлять (нумерация с 0): ", 0);
                            arr.AddRows(N, K);
                            arr.PrintArr("Массив после добавления:");
                            break;

                        case 7:
                            int delN = Input.ReadInt("Введите номер строки, с которой удалять (нумерация с 0): ", 0);
                            int delK = Input.ReadInt("Введите кол-во строк для удаления: ", 1);
                            arr.DeleteRows(delN, delK);
                            arr.PrintArr("Массив после удаления:");
                            break;

                        case 8:
                            arr.PrintInfo();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Неверный формат данных.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Ошибка! Выход за границы массива.");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Ошибка! Массив не создан или не заполнен.");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Ошибка! Недостаточно памяти для такого размера массива.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static void TornArrMenu(TornArr tornArr)
        {
            while (true)
            {
                Console.WriteLine("\n1 - Создать массив");
                Console.WriteLine("2 - Заполнить вручную");
                Console.WriteLine("3 - Заполнить рандомно (10..99)");
                Console.WriteLine("4 - Заполнить рандомно (свой диапазон)");
                Console.WriteLine("5 - Вывести массив");
                Console.WriteLine("6 - Добавить строки в конец");
                Console.WriteLine("7 - Удалить строки с конца");
                Console.WriteLine("8 - Информация о массиве");
                Console.WriteLine("0 - Назад");

                int q = Input.ReadInt("Выберите пункт: ", 0, 8);

                try
                {
                    switch (q)
                    {
                        case 0:
                            return;

                        case 1:
                            tornArr.CreateArr();
                            break;

                        case 2:
                            tornArr.FillManual();
                            break;

                        case 3:
                            tornArr.FillRandom();
                            break;

                        case 4:
                            int min = Input.ReadInt("Введите минимальное значение: ");
                            int max = Input.ReadInt("Введите максимальное значение (не включая): ");
                            tornArr.FillRandom(min, max);
                            break;

                        case 5:
                            tornArr.PrintArr();
                            break;

                        case 6:
                            int countK = Input.ReadInt("Введите кол-во строк для добавления в конец: ", 1);
                            tornArr.AddRowsToEnd(countK);
                            tornArr.PrintArr("Массив после добавления:");
                            break;

                        case 7:
                            int delK = Input.ReadInt("Введите кол-во строк для удаления с конца: ", 1);
                            tornArr.DeleteRowsFromEnd(delK);
                            tornArr.PrintArr("Массив после удаления:");
                            break;

                        case 8:
                            tornArr.PrintInfo();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Неверный формат данных.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Ошибка! Выход за границы массива.");
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Ошибка! Массив не создан или не заполнен.");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Ошибка! Недостаточно памяти для такого размера массива.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        static void StringMenu(WorkToString enterString)
        {
            while (true)
            {
                Console.WriteLine("\n1 - Ввести строку с клавиатуры");
                Console.WriteLine("2 - Выбрать строку из тестового массива");
                Console.WriteLine("3 - Показать текущую строку");
                Console.WriteLine("4 - Обработать строку (удалить слова, начинающиеся с цифры)");
                Console.WriteLine("0 - Назад");

                int q = Input.ReadInt("Выберите пункт: ", 0, 4);

                try
                {
                    switch (q)
                    {
                        case 0:
                            return;

                        case 1:
                            enterString.EnterFromKeyboard();
                            break;

                        case 2:
                            enterString.EnterFromArray();
                            break;

                        case 3:
                            enterString.PrintString();
                            break;

                        case 4:
                            enterString.ProcessString();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }
    }
}