using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;
using System;

namespace Laba11.Tests
{
    [TestClass]
    public class UtilsTests
    {
        [TestMethod]
        public void TypeNames_ContainsFiveTypes()
        {
            Assert.AreEqual(5, Utils.TypeNames.Length);
            CollectionAssert.AreEqual(
                new[] { "Место", "Адрес", "Город", "Мегаполис", "Область" },
                Utils.TypeNames);
        }

        [TestMethod]
        public void CreateRandomPlace_CreatesExpectedTypes()
        {
            Type[] expected =
            {
                typeof(Place), typeof(Address), typeof(City), typeof(Megacity), typeof(Region)
            };

            for (int type = 0; type < expected.Length; type++)
            {
                Place p = Utils.CreateRandomPlace(type);
                Assert.AreEqual(expected[type], p.GetType(), $"Тип {type}");
                Assert.IsFalse(string.IsNullOrWhiteSpace(p.Name));
            }
        }

        [TestMethod]
        public void CreateRandomPlace_UnknownType_CreatesPlace()
        {
            Place p = Utils.CreateRandomPlace(-1);
            Assert.AreEqual(typeof(Place), p.GetType());
        }

        [TestMethod]
        public void CreateRandomPlace_WithoutArgument_ReturnsPlace()
        {
            Place p = Utils.CreateRandomPlace();
            Assert.IsNotNull(p);
            Assert.IsTrue(p is Place);
        }

        [TestMethod]
        public void IsExactType_DistinguishesMegacityAndCity()
        {
            Place city = new City();
            Place megacity = new Megacity();
            Place place = new Place();
            Place address = new Address();
            Place region = new Region();

            Assert.IsTrue(Utils.IsExactType(place, 0));
            Assert.IsTrue(Utils.IsExactType(address, 1));
            Assert.IsTrue(Utils.IsExactType(city, 2));
            Assert.IsTrue(Utils.IsExactType(megacity, 3));
            Assert.IsTrue(Utils.IsExactType(region, 4));

            Assert.IsFalse(Utils.IsExactType(megacity, 2));
            Assert.IsFalse(Utils.IsExactType(city, 3));
            Assert.IsFalse(Utils.IsExactType(place, 1));
        }

        [TestMethod]
        public void IsExactType_UnknownType_ReturnsFalse()
        {
            Assert.IsFalse(Utils.IsExactType(new Place(), 5));
            Assert.IsFalse(Utils.IsExactType(new Place(), -1));
        }

        [TestMethod]
        public void GetPopulation_ReturnsValueForPopulationTypes()
        {
            Assert.AreEqual(0, Utils.GetPopulation(new Place()));
            Assert.AreEqual(0, Utils.GetPopulation(new Address()));
            Assert.AreEqual(100, Utils.GetPopulation(new City("A", 100, 1900, "R")));
            Assert.AreEqual(200, Utils.GetPopulation(new Megacity("B", 200, 1900, "R", 1, 1)));
            Assert.AreEqual(300, Utils.GetPopulation(new Region("C", 300, 10, 2)));
        }

        [TestMethod]
        public void ReadInt_ReturnsValidValue()
        {
            int value = -1;
            ConsoleHelper.RunWithInput("7\n", () => value = Utils.ReadInt("Ввод: ", 0, 10));
            Assert.AreEqual(7, value);
        }

        [TestMethod]
        public void ReadInt_RetriesAfterNonNumber()
        {
            int value = -1;
            string output = ConsoleHelper.RunWithInput("abc\n5\n", () => value = Utils.ReadInt("Ввод: ", 0, 10));

            Assert.AreEqual(5, value);
            StringAssert.Contains(output, "Введите целое число от 0 до 10.");
        }

        [TestMethod]
        public void ReadInt_RetriesAfterOutOfRange()
        {
            int value = -1;
            string output = ConsoleHelper.RunWithInput("-1\n11\n3\n", () => value = Utils.ReadInt("Ввод: ", 0, 10));

            Assert.AreEqual(3, value);
            StringAssert.Contains(output, "Введите целое число от 0 до 10.");
        }

        [TestMethod]
        public void ReadString_TrimsResult()
        {
            string value = null;
            ConsoleHelper.RunWithInput("  abc  \n", () => value = Utils.ReadString("Имя: "));
            Assert.AreEqual("abc", value);
        }

        [TestMethod]
        public void ReadString_EmptyInput_ReturnsEmptyString()
        {
            string value = null;
            ConsoleHelper.RunWithInput("\n", () => value = Utils.ReadString("Имя: "));
            Assert.AreEqual("", value);
        }

        [TestMethod]
        public void ReadPlaceType_ReadsTypeFromZeroToFour()
        {
            int value = -1;
            string output = ConsoleHelper.RunWithInput("4\n", () => value = Utils.ReadPlaceType());

            Assert.AreEqual(4, value);
            StringAssert.Contains(output, "0 - Место, 1 - Адрес, 2 - Город, 3 - Мегаполис, 4 - Область");
        }

        [TestMethod]
        public void Pause_WaitsForEnter()
        {
            bool called = false;
            string output = ConsoleHelper.RunWithInput("\n", () => { Utils.Pause(); called = true; });

            Assert.IsTrue(called);
            StringAssert.Contains(output, "Нажмите Enter для продолжения...");
        }
    }
}
