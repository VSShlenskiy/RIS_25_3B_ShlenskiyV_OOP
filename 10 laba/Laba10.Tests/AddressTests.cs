using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;

namespace Laba10.Tests
{
    [TestClass]
    public class AddressTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaults()
        {
            Address a = new Address();
            Assert.AreEqual("Не указан", a.CityName);
            Assert.AreEqual("Не указана", a.Street);
            Assert.AreEqual(1, a.House);
            Assert.AreEqual(0, a.Flat);
        }

        [TestMethod]
        public void ParamConstructor_SetsAllFields()
        {
            Address a = new Address("Дом", "Минск", "Ленина", 10, 5);
            Assert.AreEqual("Дом", a.Name);
            Assert.AreEqual("Минск", a.CityName);
            Assert.AreEqual("Ленина", a.Street);
            Assert.AreEqual(10, a.House);
            Assert.AreEqual(5, a.Flat);
        }

        [TestMethod]
        public void House_ZeroOrNegative_BecomesOne()
        {
            Address a = new Address();
            a.House = 0;
            Assert.AreEqual(1, a.House);
            a.House = -4;
            Assert.AreEqual(1, a.House);
        }

        [TestMethod]
        public void Flat_Negative_BecomesZero()
        {
            Address a = new Address();
            a.Flat = -1;
            Assert.AreEqual(0, a.Flat);
        }

        [TestMethod]
        public void CityName_And_Street_Empty_BecomeDefault()
        {
            Address a = new Address();
            a.CityName = "";
            a.Street = null;
            Assert.AreEqual("Не указан", a.CityName);
            Assert.AreEqual("Не указана", a.Street);
        }

        [TestMethod]
        public void CopyConstructor_CopiesAllFields()
        {
            Address a = new Address("Дом", "Минск", "Ленина", 10, 5);
            Address b = new Address(a);
            Assert.IsTrue(a.Equals(b));
        }

        [TestMethod]
        public void Equals_DifferentStreet_False()
        {
            Address a = new Address("Дом", "Минск", "Ленина", 10, 5);
            Address b = new Address("Дом", "Минск", "Мира", 10, 5);
            Assert.IsFalse(a.Equals(b));
        }

        [TestMethod]
        public void RandomInit_ValuesInExpectedRanges()
        {
            for (int i = 0; i < 100; i++)
            {
                Address a = new Address();
                a.RandomInit();
                Assert.IsFalse(string.IsNullOrWhiteSpace(a.CityName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(a.Street));
                Assert.IsTrue(a.House >= 1 && a.House < 150);
                Assert.IsTrue(a.Flat >= 0 && a.Flat < 200);
            }
        }

        [TestMethod]
        public void Show_PrintsAddressInfo()
        {
            Address a = new Address("Дом", "Минск", "Ленина", 10, 5);
            string output = ConsoleHelper.CaptureOutput(() => a.Show());
            StringAssert.Contains(output, "Минск");
            StringAssert.Contains(output, "Ленина");
        }

        [TestMethod]
        public void Init_ReadsAllFieldsFromConsole()
        {
            Address a = new Address();
            ConsoleHelper.WithInput("Офис\nГомель\nСоветская\n7\n12\n", () => a.Init());
            Assert.AreEqual("Офис", a.Name);
            Assert.AreEqual("Гомель", a.CityName);
            Assert.AreEqual("Советская", a.Street);
            Assert.AreEqual(7, a.House);
            Assert.AreEqual(12, a.Flat);
        }
    }
}