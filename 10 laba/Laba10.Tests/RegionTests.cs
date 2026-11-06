using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;

namespace Laba10.Tests
{
    [TestClass]
    public class RegionTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaults()
        {
            Region r = new Region();
            Assert.AreEqual(0, r.Population);
            Assert.AreEqual(1, r.Area);
            Assert.AreEqual(0, r.Districts);
        }

        [TestMethod]
        public void ParamConstructor_SetsAllFields()
        {
            Region r = new Region("Минская", 1500000, 40000, 22);
            Assert.AreEqual("Минская", r.Name);
            Assert.AreEqual(1500000, r.Population);
            Assert.AreEqual(40000, r.Area);
            Assert.AreEqual(22, r.Districts);
        }

        [TestMethod]
        public void Population_Negative_BecomesZero()
        {
            Region r = new Region();
            r.Population = -5;
            Assert.AreEqual(0, r.Population);
        }

        [TestMethod]
        public void Area_ZeroOrNegative_BecomesOne()
        {
            Region r = new Region();
            r.Area = 0;
            Assert.AreEqual(1, r.Area);
            r.Area = -10;
            Assert.AreEqual(1, r.Area);
        }

        [TestMethod]
        public void Districts_Negative_BecomesZero()
        {
            Region r = new Region();
            r.Districts = -1;
            Assert.AreEqual(0, r.Districts);
        }

        [TestMethod]
        public void CopyConstructor_CopiesAllFields()
        {
            Region a = new Region("Минская", 1500000, 40000, 22);
            Region b = new Region(a);
            Assert.IsTrue(a.Equals(b));
            a.Population = 1;
            Assert.AreEqual(1500000, b.Population);
        }

        [TestMethod]
        public void Equals_SameValues_True()
        {
            Region a = new Region("Минская", 100, 200, 3);
            Region b = new Region("Минская", 100, 200, 3);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentPopulation_False()
        {
            Region a = new Region("Минская", 100, 200, 3);
            Region b = new Region("Минская", 101, 200, 3);
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void RandomInit_ValuesInExpectedRanges()
        {
            for (int i = 0; i < 100; i++)
            {
                Region r = new Region();
                r.RandomInit();
                Assert.IsTrue(r.Name.EndsWith(" область"));
                Assert.IsTrue(r.Population >= 500000 && r.Population < 3000000);
                Assert.IsTrue(r.Area >= 20000 && r.Area < 50000);
                Assert.IsTrue(r.Districts >= 8 && r.Districts < 25);
            }
        }

        [TestMethod]
        public void Show_PrintsRegionInfo()
        {
            Region r = new Region("Минская", 100, 200, 3);
            string output = ConsoleHelper.CaptureOutput(() => r.Show());
            StringAssert.Contains(output, "Область");
            StringAssert.Contains(output, "Минская");
        }

        [TestMethod]
        public void Init_ReadsAllFieldsFromConsole()
        {
            Region r = new Region();
            ConsoleHelper.WithInput("Брестская\n1000\n500\n10\n", () => r.Init());
            Assert.AreEqual("Брестская", r.Name);
            Assert.AreEqual(1000, r.Population);
            Assert.AreEqual(500, r.Area);
            Assert.AreEqual(10, r.Districts);
        }

        [TestMethod]
        public void Init_InvalidNumberThenValid_Retries()
        {
            Region r = new Region();
            // "abc" не число, должен запросить ввод повторно
            ConsoleHelper.WithInput("Брестская\nabc\n1000\n500\n10\n", () => r.Init());
            Assert.AreEqual(1000, r.Population);
        }
    }
}