using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;
using System;

namespace Laba10.Tests
{
    [TestClass]
    public class CityTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaults()
        {
            City c = new City();
            Assert.AreEqual(0, c.Population);
            Assert.AreEqual(1900, c.FoundationYear);
            Assert.AreEqual("Не указана", c.RegionName);
        }

        [TestMethod]
        public void ParamConstructor_SetsAllFields()
        {
            City c = new City("Борисов", 140000, 1102, "Минская область");
            Assert.AreEqual("Борисов", c.Name);
            Assert.AreEqual(140000, c.Population);
            Assert.AreEqual(1102, c.FoundationYear);
            Assert.AreEqual("Минская область", c.RegionName);
        }

        [TestMethod]
        public void Population_Negative_BecomesZero()
        {
            City c = new City();
            c.Population = -100;
            Assert.AreEqual(0, c.Population);
        }

        [TestMethod]
        public void FoundationYear_Invalid_BecomesDefault()
        {
            City c = new City();
            c.FoundationYear = -5;
            Assert.AreEqual(1900, c.FoundationYear);
            c.FoundationYear = 0;
            Assert.AreEqual(1900, c.FoundationYear);
            c.FoundationYear = DateTime.Now.Year + 10;
            Assert.AreEqual(1900, c.FoundationYear);
        }

        [TestMethod]
        public void FoundationYear_Valid_IsStored()
        {
            City c = new City();
            c.FoundationYear = 1500;
            Assert.AreEqual(1500, c.FoundationYear);
        }

        [TestMethod]
        public void RegionName_Empty_BecomesDefault()
        {
            City c = new City();
            c.RegionName = "";
            Assert.AreEqual("Не указана", c.RegionName);
        }

        [TestMethod]
        public void CopyConstructor_CopiesAllFields()
        {
            City a = new City("Борисов", 140000, 1102, "Минская область");
            City b = new City(a);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentRegion_False()
        {
            City a = new City("Борисов", 1, 1500, "Минская область");
            City b = new City("Борисов", 1, 1500, "Брестская область");
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void RandomInit_ValuesInExpectedRanges()
        {
            for (int i = 0; i < 100; i++)
            {
                City c = new City();
                c.RandomInit();
                Assert.IsFalse(string.IsNullOrWhiteSpace(c.Name));
                Assert.IsTrue(c.Population >= 10000 && c.Population < 500000);
                Assert.IsTrue(c.FoundationYear >= 1000 && c.FoundationYear < 1950);
                Assert.IsFalse(string.IsNullOrWhiteSpace(c.RegionName));
            }
        }

        [TestMethod]
        public void Show_PrintsCityInfo()
        {
            City c = new City("Борисов", 140000, 1102, "Минская область");
            string output = ConsoleHelper.CaptureOutput(() => c.Show());
            StringAssert.Contains(output, "Город");
            StringAssert.Contains(output, "Борисов");
            StringAssert.Contains(output, "Минская область");
        }

        [TestMethod]
        public void Init_ReadsAllFieldsFromConsole()
        {
            City c = new City();
            ConsoleHelper.WithInput("Пинск\n130000\n1097\nБрестская область\n", () => c.Init());
            Assert.AreEqual("Пинск", c.Name);
            Assert.AreEqual(130000, c.Population);
            Assert.AreEqual(1097, c.FoundationYear);
            Assert.AreEqual("Брестская область", c.RegionName);
        }
    }
}