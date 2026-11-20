using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Laba11.Tests
{
    [TestClass]
    public class Task2DictionaryTests
    {
        private static string Run(Task2Dictionary task, string input)
        {
            return ConsoleHelper.RunWithInput(input, () => task.Run());
        }

        [TestMethod]
        public void AddManual_AddsElementWithNameAsKey()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n4\n\n0\n");

            StringAssert.Contains(output, "Добавлен элемент с ключом «Test»");
            StringAssert.Contains(output, "[Test] Место: Test");
        }

        [TestMethod]
        public void AddManual_DuplicateName_GeneratesUniqueKey()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n2\n0\nTest\n\n4\n\n0\n");

            StringAssert.Contains(output, "Добавлен элемент с ключом «Test»");
            StringAssert.Contains(output, "Добавлен элемент с ключом «Test #2»");
            StringAssert.Contains(output, "[Test #2] Место: Test");
        }

        [TestMethod]
        public void AddRandom_AddsElement()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "1\n\n0\n");

            StringAssert.Contains(output, "Добавлен элемент с ключом");
            StringAssert.Contains(output, "=== Задание 2. Dictionary<string, Place> (элементов: 1) ===");
        }

        [TestMethod]
        public void RemoveByKey_RemovesElement()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n3\nTest\n\n4\n\n0\n");

            StringAssert.Contains(output, "Элемент удалён.");
            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void RemoveByKey_UnknownKey_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n3\nНет такого\n\n0\n");

            StringAssert.Contains(output, "Ключ не найден.");
        }

        [TestMethod]
        public void RemoveByKey_Empty_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "3\n\n0\n");

            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void ShowAll_Empty_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "4\n\n0\n");

            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void QueryCount_CountsOnlyExactType()
        {
            var task = new Task2Dictionary();
            string output = Run(task,
                "2\n0\nA\n\n" +
                "2\n2\nB\n1000\n1900\nR\n\n" +
                "5\n2\n\n0\n");

            StringAssert.Contains(output, "Элементов вида «Город»: 1");
        }

        [TestMethod]
        public void QueryCount_MegacityIsNotCountedAsCity()
        {
            var task = new Task2Dictionary();
            string output = Run(task,
                "2\n3\nM\n1000\n1900\nR\n10\n5\n\n" +
                "5\n2\n\n" +
                "5\n3\n\n0\n");

            StringAssert.Contains(output, "Элементов вида «Город»: 0");
            StringAssert.Contains(output, "Элементов вида «Мегаполис»: 1");
        }

        [TestMethod]
        public void QueryPrint_PrintsOnlyRequestedType()
        {
            var task = new Task2Dictionary();
            string output = Run(task,
                "2\n0\nA\n\n" +
                "2\n2\nB\n1000\n1900\nR\n\n" +
                "6\n2\n\n0\n");

            int index = output.IndexOf("Элементы вида «Город»:", StringComparison.Ordinal);
            Assert.IsTrue(index >= 0, "Секция печати городов не найдена");

            string section = output.Substring(index);
            StringAssert.Contains(section, "[B] Город: B");
            Assert.IsFalse(section.Contains("Место: A"));
        }

        [TestMethod]
        public void QueryPrint_NoMatches_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "6\n4\n\n0\n");

            StringAssert.Contains(output, "Таких элементов нет.");
        }

        [TestMethod]
        public void QueryMaxPopulation_FindsObjectWithMaxPopulation()
        {
            var task = new Task2Dictionary();
            string output = Run(task,
                "2\n2\nC\n5000\n1900\nR\n\n" +
                "2\n4\nReg\n100\n1000\n5\nX\n\n" +
                "7\n\n0\n");

            StringAssert.Contains(output, "Максимальное население: 5000");
            StringAssert.Contains(output, "Город: C");
        }

        [TestMethod]
        public void QueryMaxPopulation_NoPopulation_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nA\n\n7\n\n0\n");

            StringAssert.Contains(output, "В коллекции нет объектов с населением.");
        }

        [TestMethod]
        public void Clone_CreatesIndependentDictionary()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n8\n\n0\n");

            StringAssert.Contains(output, "Оригинал: 1 элементов, клон: 1 элементов.");
            StringAssert.Contains(output, "После удаления «Test» из клона: оригинал = 1, клон = 0.");
        }

        [TestMethod]
        public void Clone_EmptyDictionary_DoesNotRemove()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "8\n\n0\n");

            StringAssert.Contains(output, "Оригинал: 0 элементов, клон: 0 элементов.");
            Assert.IsFalse(output.Contains("После удаления"));
        }

        [TestMethod]
        public void SortByKey_OrdersElementsByKey()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nB\n\n2\n0\nA\n\n9\n\n0\n");

            int index = output.IndexOf("Коллекция упорядочена по ключу:", StringComparison.Ordinal);
            Assert.IsTrue(index >= 0, "Сообщение о сортировке не найдено");

            string section = output.Substring(index);
            int a = section.IndexOf("[A]", StringComparison.Ordinal);
            int b = section.IndexOf("[B]", StringComparison.Ordinal);

            Assert.IsTrue(a >= 0 && b > a, "Ключи упорядочены неверно");
        }

        [TestMethod]
        public void SortByValueName_OrdersElementsByName()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nB\n\n2\n0\nA\n\n10\n\n0\n");

            int index = output.IndexOf("Коллекция упорядочена по названию значения:", StringComparison.Ordinal);
            Assert.IsTrue(index >= 0, "Сообщение о сортировке не найдено");

            string section = output.Substring(index);
            int a = section.IndexOf("[A]", StringComparison.Ordinal);
            int b = section.IndexOf("[B]", StringComparison.Ordinal);

            Assert.IsTrue(a >= 0 && b > a, "Значения упорядочены неверно");
        }

        [TestMethod]
        public void Sort_EmptyDictionary_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "9\n\n10\n\n0\n");

            Assert.AreEqual(2, CountOccurrences(output, "Коллекция пуста."));
        }

        [TestMethod]
        public void SearchByKey_FindsElement()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n11\nTest\n\n0\n");

            StringAssert.Contains(output, "Найдено: Место: Test");
        }

        [TestMethod]
        public void SearchByKey_NotFound_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n11\nzzz\n\n0\n");

            StringAssert.Contains(output, "Ключ не найден.");
        }

        [TestMethod]
        public void SearchByKey_Empty_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "11\n\n0\n");

            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void SearchByValue_FindsElementIgnoringCase()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n12\ntest\n\n0\n");

            StringAssert.Contains(output, "[Test] Место: Test");
            StringAssert.Contains(output, "Поиск завершён.");
            StringAssert.Contains(output, "ContainsValue(копия первого элемента) = True");
            StringAssert.Contains(output, "ContainsValue(отсутствующий элемент) = False");
        }

        [TestMethod]
        public void SearchByValue_NotFound_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nTest\n\n12\nzzz\n\n0\n");

            StringAssert.Contains(output, "Элемент не найден.");
            StringAssert.Contains(output, "ContainsValue(отсутствующий элемент) = False");
        }

        [TestMethod]
        public void SearchByValue_Empty_ShowsMessage()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "12\n\n0\n");

            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void Clear_EmptiesCollection()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "2\n0\nA\n\n13\n\n4\n\n0\n");

            StringAssert.Contains(output, "Коллекция очищена.");
            StringAssert.Contains(output, "Коллекция пуста.");
        }

        [TestMethod]
        public void Run_InvalidMenuChoice_Reprompts()
        {
            var task = new Task2Dictionary();
            string output = Run(task, "99\n1\n\n0\n");

            StringAssert.Contains(output, "Введите целое число от 0 до 13.");
            StringAssert.Contains(output, "Добавлен элемент с ключом");
        }

        private static int CountOccurrences(string text, string value)
        {
            int count = 0;
            int index = 0;
            while ((index = text.IndexOf(value, index, StringComparison.Ordinal)) >= 0)
            {
                count++;
                index += value.Length;
            }
            return count;
        }
    }
}
