using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;

namespace Laba10.Tests
{
    [TestClass]
    public class MegacityTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaults()
        {
            Megacity m = new Megacity();
            Assert.AreEqual(0, m.MetroStations);
            Assert.AreEqual(0, m.Districts);
            Assert.AreEqual(1900, m.FoundationYear);
        }

        [TestMethod]
        public void ParamConstructor_SetsAllFields()
        {
            Megacity m = new Megacity("Москва", 12000000, 1147, "Московская область", 250, 12);
            Assert.AreEqual("Москва", m.Name);
            Assert.AreEqual(12000000, m.Population);
            Assert.AreEqual(1147, m.FoundationYear);
            Assert.AreEqual(250, m.MetroStations);
            Assert.AreEqual(12, m.Districts);
        }

        [TestMethod]
        public void MetroStations_Negative_BecomesZero()
        {
            Megacity m = new Megacity();
            m.MetroStations = -3;
            Assert.AreEqual(0, m.MetroStations);
        }

        [TestMethod]
        public void Districts_Negative_BecomesZero()
        {
            Megacity m = new Megacity();
            m.Districts = -3;
            Assert.AreEqual(0, m.Districts);
        }

        [TestMethod]
        public void CopyConstructor_CopiesAllFields()
        {
            Megacity a = new Megacity("Москва", 12000000, 1147, "МО", 250, 12);
            Megacity b = new Megacity(a);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentMetroStations_False()
        {
            Megacity a = new Megacity("Москва", 1, 1147, "МО", 250, 12);
            Megacity b = new Megacity("Москва", 1, 1147, "МО", 251, 12);
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void Equals_CityVsMegacity_False()
        {
            City c = new City("Москва", 1, 1147, "МО");
            Megacity m = new Megacity("Москва", 1, 1147, "МО", 0, 0);
            Assert.IsFalse(c.Equals(m));
            Assert.IsFalse(m.Equals(c));
        }

        [TestMethod]
        public void RandomInit_ValuesInExpectedRanges()
        {
            for (int i = 0; i < 100; i++)
            {
                Megacity m = new Megacity();
                m.RandomInit();
                Assert.IsTrue(m.Population >= 1000000 && m.Population < 15000000);
                Assert.IsTrue(m.MetroStations >= 10 && m.MetroStations < 300);
                Assert.IsTrue(m.Districts >= 5 && m.Districts < 30);
            }
        }

        [TestMethod]
        public void Show_PrintsMegacityInfo()
        {
            Megacity m = new Megacity("Москва", 12000000, 1147, "МО", 250, 12);
            string output = ConsoleHelper.CaptureOutput(() => m.Show());
            StringAssert.Contains(output, "Мегаполис");
            StringAssert.Contains(output, "Москва");
            StringAssert.Contains(output, "250");
        }

        [TestMethod]
        public void Init_ReadsAllFieldsFromConsole()
        {
            Megacity m = new Megacity();
            ConsoleHelper.WithInput("Токио\n14000000\n1457\nКанто\n290\n23\n", () => m.Init());
            Assert.AreEqual("Токио", m.Name);
            Assert.AreEqual(14000000, m.Population);
            Assert.AreEqual(290, m.MetroStations);
            Assert.AreEqual(23, m.Districts);
        }
    }
}