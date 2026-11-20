using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;
using System;
using System.Linq;

namespace Laba11.Tests
{
    [TestClass]
    public class TestCollectionsTests
    {
        [TestMethod]
        public void Constructor_FillsAllFourCollections()
        {
            var tc = new TestCollections(25);

            Assert.AreEqual(25, tc.Collection1.Count);
            Assert.AreEqual(25, tc.Collection2.Count);
            Assert.AreEqual(25, tc.Collection3.Count);
            Assert.AreEqual(25, tc.Collection4.Count);
        }

        [TestMethod]
        public void Constructor_Zero_CreatesEmptyCollections()
        {
            var tc = new TestCollections(0);

            Assert.AreEqual(0, tc.Collection1.Count);
            Assert.AreEqual(0, tc.Collection2.Count);
            Assert.AreEqual(0, tc.Collection3.Count);
            Assert.AreEqual(0, tc.Collection4.Count);
        }

        [TestMethod]
        public void DefaultConstructor_CreatesThousandElements()
        {
            var tc = new TestCollections();
            Assert.AreEqual(1000, tc.Collection1.Count);
        }

        [TestMethod]
        public void CreateMegacity_GeneratesUniqueNames()
        {
            var tc = new TestCollections(0);
            var names = Enumerable.Range(0, 100).Select(_ => tc.CreateMegacity().Name).ToList();

            Assert.AreEqual(100, names.Distinct().Count());
            Assert.AreEqual("Мегаполис-1", names[0]);
            Assert.AreEqual("Мегаполис-100", names[99]);
        }

        [TestMethod]
        public void CreateMegacity_ValuesInExpectedRanges()
        {
            var tc = new TestCollections(0);
            for (int i = 0; i < 100; i++)
            {
                Megacity m = tc.CreateMegacity();
                Assert.IsTrue(m.Population >= 1_000_000 && m.Population < 15_000_000);
                Assert.IsTrue(m.FoundationYear >= 800 && m.FoundationYear < 2000);
                Assert.IsTrue(m.MetroStations >= 10 && m.MetroStations < 300);
                Assert.IsTrue(m.Districts >= 5 && m.Districts < 30);
                Assert.AreEqual("Столичный регион", m.RegionName);
            }
        }

        [TestMethod]
        public void Add_AddsElementToAllCollections()
        {
            var tc = new TestCollections(0);
            var m = CreateMegacity("М-1");

            tc.Add(m);

            Assert.AreEqual(1, tc.Collection1.Count);
            Assert.AreEqual(1, tc.Collection2.Count);
            Assert.AreEqual(1, tc.Collection3.Count);
            Assert.AreEqual(1, tc.Collection4.Count);
            Assert.AreSame(m, tc.Collection1.First.Value);
        }

        [TestMethod]
        public void Add_StringCollectionsContainToStringValue()
        {
            var tc = new TestCollections(0);
            var m = CreateMegacity("М-1");

            tc.Add(m);

            Assert.AreEqual(m.ToString(), tc.Collection2.First.Value);
            Assert.IsTrue(tc.Collection4.ContainsKey(m.ToString()));
            Assert.AreSame(m, tc.Collection4[m.ToString()]);
        }

        [TestMethod]
        public void Add_DictionaryKeyIsBaseCity()
        {
            var tc = new TestCollections(0);
            var m = CreateMegacity("М-1");

            tc.Add(m);

            City key = tc.Collection3.Keys.Single();
            Assert.AreEqual(typeof(City), key.GetType());
            Assert.AreEqual("М-1", key.Name);
            Assert.AreEqual(m.Population, key.Population);
            Assert.AreEqual(m.FoundationYear, key.FoundationYear);
            Assert.AreEqual(m.RegionName, key.RegionName);
            Assert.AreSame(m, tc.Collection3[key]);
        }

        [TestMethod]
        public void BaseCity_ReturnsNewCityObject()
        {
            var m = CreateMegacity("М-1");

            City a = m.BaseCity;
            City b = m.BaseCity;

            Assert.AreEqual(typeof(City), a.GetType());
            Assert.AreNotSame(a, b);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Remove_DeletesElementFromAllCollections()
        {
            var tc = new TestCollections(0);
            var m = CreateMegacity("М-1");
            tc.Add(m);

            Assert.IsTrue(tc.Remove(m));
            Assert.AreEqual(0, tc.Collection1.Count);
            Assert.AreEqual(0, tc.Collection2.Count);
            Assert.AreEqual(0, tc.Collection3.Count);
            Assert.AreEqual(0, tc.Collection4.Count);
        }

        [TestMethod]
        public void Remove_Null_ReturnsFalse()
        {
            var tc = new TestCollections(0);
            Assert.IsFalse(tc.Remove(null));
        }

        [TestMethod]
        public void Remove_UnknownElement_ReturnsFalse()
        {
            var tc = new TestCollections(0);
            tc.Add(CreateMegacity("М-1"));

            Assert.IsFalse(tc.Remove(CreateMegacity("М-2")));
            Assert.AreEqual(1, tc.Collection1.Count);
            Assert.AreEqual(1, tc.Collection3.Count);
        }

        [TestMethod]
        public void RemoveFirst_RemovesHead()
        {
            var tc = new TestCollections(0);
            tc.Add(CreateMegacity("М-1"));
            tc.Add(CreateMegacity("М-2"));

            Assert.IsTrue(tc.RemoveFirst());
            Assert.AreEqual("М-2", tc.Collection1.First.Value.Name);
            Assert.AreEqual(1, tc.Collection2.Count);
        }

        [TestMethod]
        public void RemoveLast_RemovesTail()
        {
            var tc = new TestCollections(0);
            tc.Add(CreateMegacity("М-1"));
            tc.Add(CreateMegacity("М-2"));

            Assert.IsTrue(tc.RemoveLast());
            Assert.AreEqual("М-1", tc.Collection1.Last.Value.Name);
            Assert.AreEqual(1, tc.Collection4.Count);
        }

        [TestMethod]
        public void RemoveFirstAndLast_Empty_ReturnFalse()
        {
            var tc = new TestCollections(0);

            Assert.IsFalse(tc.RemoveFirst());
            Assert.IsFalse(tc.RemoveLast());
        }

        [TestMethod]
        public void AddRandom_AddsElement()
        {
            var tc = new TestCollections(0);
            tc.AddRandom();

            Assert.AreEqual(1, tc.Collection1.Count);
            Assert.AreEqual(1, tc.Collection2.Count);
            Assert.AreEqual(1, tc.Collection3.Count);
            Assert.AreEqual(1, tc.Collection4.Count);
        }

        [TestMethod]
        public void Show_PrintsCountsAndElements()
        {
            var tc = new TestCollections(0);
            tc.Add(CreateMegacity("М-1"));

            string output = ConsoleHelper.CaptureOutput(() => tc.Show(10));

            StringAssert.Contains(output, "LinkedList<Megacity>: 1");
            StringAssert.Contains(output, "LinkedList<string>: 1");
            StringAssert.Contains(output, "Dictionary<City, Megacity>: 1");
            StringAssert.Contains(output, "Dictionary<string, Megacity>: 1");
            StringAssert.Contains(output, "Мегаполис: М-1");
        }

        [TestMethod]
        public void Show_Limit_PrintsEllipsis()
        {
            var tc = new TestCollections(0);
            for (int i = 1; i <= 5; i++)
                tc.Add(CreateMegacity($"М-{i}"));

            string output = ConsoleHelper.CaptureOutput(() => tc.Show(2));

            StringAssert.Contains(output, "Мегаполис: М-1");
            StringAssert.Contains(output, "Мегаполис: М-2");
            Assert.IsFalse(output.Contains("Мегаполис: М-3"));
            StringAssert.Contains(output, "...");
        }

        [TestMethod]
        public void Show_Empty_PrintsZeroCounts()
        {
            var tc = new TestCollections(0);
            string output = ConsoleHelper.CaptureOutput(() => tc.Show());

            StringAssert.Contains(output, "LinkedList<Megacity>: 0");
        }

        [TestMethod]
        public void MeasureSearch_AllChecksPass()
        {
            var tc = new TestCollections(1000);

            string output = ConsoleHelper.CaptureOutput(() => tc.MeasureSearch());

            StringAssert.Contains(output, "LinkedList<Megacity>.Contains");
            StringAssert.Contains(output, "LinkedList<string>.Contains");
            StringAssert.Contains(output, "Dictionary<City,Megacity>.ContainsKey");
            StringAssert.Contains(output, "Dictionary<string,Megacity>.ContainsKey");
            StringAssert.Contains(output, "Dictionary<City,Megacity>.ContainsValue");
            StringAssert.Contains(output, "OK");
            Assert.IsFalse(output.Contains("ОШИБКА"));
        }

        [TestMethod]
        public void MeasureSearch_Empty_ShowsMessage()
        {
            var tc = new TestCollections(0);
            string output = ConsoleHelper.CaptureOutput(() => tc.MeasureSearch());

            StringAssert.Contains(output, "Коллекции пусты.");
        }

        private static Megacity CreateMegacity(string name)
        {
            return new Megacity(name, 2_000_000, 1800, "Столичный регион", 50, 10);
        }
    }
}
