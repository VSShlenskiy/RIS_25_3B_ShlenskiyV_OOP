using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Laba11.Tests
{
    [TestClass]
    public class Task1StackTests
    {
        private static string Run(Task1Stack task, string input)
        {
            return ConsoleHelper.RunWithInput(input, () => task.Run());
        }

        [TestMethod]
        public void AddManual_Place_AddsElementToStack()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nТестовое место\n\n4\n\n0\n");

            StringAssert.Contains(output, "Добавлен элемент: Место: Тестовое место");
            StringAssert.Contains(output, "Элементы стека");
            StringAssert.Contains(output, "1. Место: Тестовое место");
        }

        [TestMethod]
        public void AddManual_InvalidType_RepromptsAndAdds()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n9\n0\nЭлемент\n\n0\n");

            StringAssert.Contains(output, "Введите целое число от 0 до 4.");
            StringAssert.Contains(output, "Добавлен элемент: Место: Элемент");
        }

        [TestMethod]
        public void AddRandom_AddsOneElement()
        {
            var task = new Task1Stack();
            string output = Run(task, "1\n\n0\n");

            StringAssert.Contains(output, "Добавлен элемент:");
            StringAssert.Contains(output, "=== Задание 1. Stack (элементов: 1) ===");
        }

        [TestMethod]
        public void RemoveTop_RemovesLastAddedElement()
        {
            var task = new Task1Stack();
            Run(task, "2\n0\nA\n\n2\n0\nB\n\n3\n\n0\n");

            string output = Run(task, "4\n\n0\n");
            StringAssert.Contains(output, "1. Место: A");
            Assert.IsFalse(output.Contains("Место: B"));
        }

        [TestMethod]
        public void RemoveTop_ShowsRemovedElement()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nA\n\n2\n0\nB\n\n3\n\n0\n");

            StringAssert.Contains(output, "Удалён верхний элемент: Место: B");
        }

        [TestMethod]
        public void RemoveTop_Empty_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "3\n\n0\n");

            StringAssert.Contains(output, "Стек пуст.");
        }

        [TestMethod]
        public void ShowAll_Empty_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "4\n\n0\n");

            StringAssert.Contains(output, "Стек пуст.");
        }

        [TestMethod]
        public void QueryCount_CountsOnlyExactType()
        {
            var task = new Task1Stack();
            string output = Run(task,
                "2\n0\nA\n\n" +
                "2\n2\nB\n1000\n1900\nR\n\n" +
                "5\n2\n\n0\n");

            StringAssert.Contains(output, "Элементов вида «Город»: 1");
        }

        [TestMethod]
        public void QueryCount_MegacityIsNotCountedAsCity()
        {
            var task = new Task1Stack();
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
            var task = new Task1Stack();
            string output = Run(task,
                "2\n0\nA\n\n" +
                "2\n2\nB\n1000\n1900\nR\n\n" +
                "6\n2\n\n0\n");

            int index = output.IndexOf("Элементы вида «Город»:", StringComparison.Ordinal);
            Assert.IsTrue(index >= 0, "Секция печати городов не найдена");

            string section = output.Substring(index);
            StringAssert.Contains(section, "Город: B");
            Assert.IsFalse(section.Contains("Место: A"));
        }

        [TestMethod]
        public void QueryPrint_NoMatches_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "6\n4\n\n0\n");

            StringAssert.Contains(output, "Таких элементов нет.");
        }

        [TestMethod]
        public void QueryMaxPopulation_FindsObjectWithMaxPopulation()
        {
            var task = new Task1Stack();
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
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nA\n\n7\n\n0\n");

            StringAssert.Contains(output, "В коллекции нет объектов с населением.");
        }

        [TestMethod]
        public void Clone_CreatesIndependentStructure()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nA\n\n8\n\n0\n");

            StringAssert.Contains(output, "Оригинал: 1 элементов, клон: 1 элементов.");
            StringAssert.Contains(output, "Содержимое клона:");
            StringAssert.Contains(output, "После Pop из клона: оригинал = 1, клон = 0.");
        }

        [TestMethod]
        public void Clone_EmptyStack_DoesNotPop()
        {
            var task = new Task1Stack();
            string output = Run(task, "8\n\n0\n");

            StringAssert.Contains(output, "Оригинал: 0 элементов, клон: 0 элементов.");
            Assert.IsFalse(output.Contains("После Pop из клона"));
        }

        [TestMethod]
        public void Sort_SortsByNameFromTopToBottom()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nB\n\n2\n0\nA\n\n9\n\n0\n");

            int index = output.IndexOf("Стек отсортирован по названию:", StringComparison.Ordinal);
            Assert.IsTrue(index >= 0, "Сообщение о сортировке не найдено");

            string section = output.Substring(index);
            int first = section.IndexOf("1. Место: A", StringComparison.Ordinal);
            int second = section.IndexOf("2. Место: B", StringComparison.Ordinal);

            Assert.IsTrue(first >= 0, "Первый элемент после сортировки не найден");
            Assert.IsTrue(second > first, "Элементы отсортированы неверно");
        }

        [TestMethod]
        public void Sort_LessThanTwoElements_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nA\n\n9\n\n0\n");

            StringAssert.Contains(output, "Нечего сортировать.");
        }

        [TestMethod]
        public void Search_FindsElementIgnoringCase()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nTest\n\n10\ntest\n\n0\n");

            StringAssert.Contains(output, "Найден: Место: Test");
            StringAssert.Contains(output, "Поиск завершён.");
            StringAssert.Contains(output, "Contains(копия верхнего элемента) = True");
        }

        [TestMethod]
        public void Search_NotFound_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nTest\n\n10\nzzz\n\n0\n");

            StringAssert.Contains(output, "Элемент не найден.");
        }

        [TestMethod]
        public void Search_EmptyStack_ShowsMessage()
        {
            var task = new Task1Stack();
            string output = Run(task, "10\n\n0\n");

            StringAssert.Contains(output, "Стек пуст.");
        }

        [TestMethod]
        public void Clear_EmptiesCollection()
        {
            var task = new Task1Stack();
            string output = Run(task, "2\n0\nA\n\n11\n\n4\n\n0\n");

            StringAssert.Contains(output, "Коллекция очищена.");
            StringAssert.Contains(output, "Стек пуст.");
        }

        [TestMethod]
        public void Run_InvalidMenuChoice_Reprompts()
        {
            var task = new Task1Stack();
            string output = Run(task, "99\n1\n\n0\n");

            StringAssert.Contains(output, "Введите целое число от 0 до 11.");
            StringAssert.Contains(output, "Добавлен элемент:");
        }
    }
}
