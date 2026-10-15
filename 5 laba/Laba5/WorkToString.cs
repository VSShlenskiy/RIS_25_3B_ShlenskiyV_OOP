using System;
using System.Collections.Generic;

namespace Laba5
{
    class WorkToString
    {
        private string enterString;

        private const string endMarks = ".!?";
        private const string middleMarks = ",;:";

        private string[] testStrings =
        {
            "static void PrintUpper string info 12346: WriteLine ToUpper info, 1234info.",
            "Hello world, 123 test! 7up is a drink.",
            "1abc 2def ghi.",
            "123 456 789"
        };

        public WorkToString()
        {
            enterString = "";
        }

        public void EnterFromKeyboard()
        {
            Console.WriteLine("Введите строку для обработки:");
            enterString = Console.ReadLine();

            if (enterString == null)
            {
                enterString = "";
            }
        }

        public void EnterFromArray()
        {
            Console.WriteLine("Тестовые строки:");
            for (int i = 0; i < testStrings.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {testStrings[i]}");
            }

            int n = Input.ReadInt("Выберите строку: ", 1, testStrings.Length);
            enterString = testStrings[n - 1];
        }

        public void PrintString()
        {
            if (string.IsNullOrWhiteSpace(enterString))
            {
                Console.WriteLine("Строка пуста!");
                return;
            }

            Console.WriteLine($"Текущая строка:\n{enterString}");
        }

        private bool IsIn(char c, string marks)
        {
            return marks.IndexOf(c) >= 0;
        }

        public string ProcessString(string str)
        {
            string[] words = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> kept = new List<string>();

            foreach (string word in words)
            {
                if (!char.IsDigit(word[0]))
                {
                    kept.Add(word);
                    continue;
                }

                char last = word[word.Length - 1];
                if (!IsIn(last, endMarks) && !IsIn(last, middleMarks))
                {
                    continue;
                }

                if (kept.Count == 0)
                {
                    continue;
                }

                string prev = kept[kept.Count - 1];
                char prevLast = prev[prev.Length - 1];

                if (IsIn(last, endMarks))
                {
                    if (IsIn(prevLast, middleMarks))
                    {
                        prev = prev.Substring(0, prev.Length - 1);
                        prevLast = prev.Length > 0 
                            ? prev[prev.Length - 1] 
                            : ' ';
                    }

                    if (!IsIn(prevLast, endMarks))
                    {
                        prev = prev + last;
                    }
                }
                else
                {
                    if (!IsIn(prevLast, endMarks) && !IsIn(prevLast, middleMarks))
                    {
                        prev = prev + last;
                    }
                }

                kept[kept.Count - 1] = prev;
            }

            return string.Join(" ", kept);
        }

        public void ProcessString()
        {
            if (string.IsNullOrWhiteSpace(enterString))
            {
                Console.WriteLine("Строка пуста!");
                return;
            }

            string resultString = ProcessString(enterString);

            if (string.IsNullOrWhiteSpace(resultString))
            {
                Console.WriteLine("После обработки строка стала пустой!");
                return;
            }

            Console.WriteLine($"Результат:\n{resultString}");
        }
    }
}