using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba11.Tests
{
    [TestClass]
    public class Task3TestCollectionsTests
    {
        private static string Run(Task3TestCollections task, string input)
        {
            return ConsoleHelper.RunWithInput(input, () => task.Run());
        }

        [TestMethod]
        public void Run_Show_PrintsCollections()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "1\n\n0\n");

            StringAssert.Contains(output, "LinkedList<Megacity>: 1000");
            StringAssert.Contains(output, "LinkedList<string>: 1000");
            StringAssert.Contains(output, "Dictionary<City, Megacity>: 1000");
            StringAssert.Contains(output, "Dictionary<string, Megacity>: 1000");
        }

        [TestMethod]
        public void Run_AddRandom_IncreasesAllCollections()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "2\n\n1\n\n0\n");

            StringAssert.Contains(output, "Элемент добавлен во все четыре коллекции.");
            StringAssert.Contains(output, "LinkedList<Megacity>: 1001");
            StringAssert.Contains(output, "Dictionary<string, Megacity>: 1001");
        }

        [TestMethod]
        public void Run_RemoveFirst_DecreasesAllCollections()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "3\n\n1\n\n0\n");

            StringAssert.Contains(output, "Первый элемент удалён.");
            StringAssert.Contains(output, "LinkedList<Megacity>: 999");
            StringAssert.Contains(output, "Dictionary<City, Megacity>: 999");
        }

        [TestMethod]
        public void Run_RemoveLast_DecreasesAllCollections()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "4\n\n1\n\n0\n");

            StringAssert.Contains(output, "Последний элемент удалён.");
            StringAssert.Contains(output, "LinkedList<Megacity>: 999");
            StringAssert.Contains(output, "Dictionary<string, Megacity>: 999");
        }

        [TestMethod]
        public void Run_MeasureSearch_PrintsTable()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "5\n\n0\n");

            StringAssert.Contains(output, "Время поиска в тиках");
            StringAssert.Contains(output, "LinkedList<Megacity>.Contains");
            StringAssert.Contains(output, "OK");
            Assert.IsFalse(output.Contains("ОШИБКА"));
        }

        [TestMethod]
        public void Run_Recreate_RestoresThousandElements()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "3\n\n6\n\n1\n\n0\n");

            StringAssert.Contains(output, "Коллекции пересозданы.");
            StringAssert.Contains(output, "LinkedList<Megacity>: 1000");
        }

        [TestMethod]
        public void Run_InvalidMenuChoice_Reprompts()
        {
            var task = new Task3TestCollections();
            string output = Run(task, "99\n1\n\n0\n");

            StringAssert.Contains(output, "Введите целое число от 0 до 6.");
            StringAssert.Contains(output, "LinkedList<Megacity>: 1000");
        }
    }
}
