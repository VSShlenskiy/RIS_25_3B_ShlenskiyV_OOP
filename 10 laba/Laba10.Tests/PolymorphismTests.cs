using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;

namespace Laba10.Tests
{
    [TestClass]
    public class PolymorphismTests
    {
        [TestMethod]
        public void VirtualShow_UsesRealObjectType()
        {
            Place[] places =
            {
                new Place("Парк"),
                new Region("Минская", 1, 1, 1),
                new City("Борисов", 1, 1500, "Минская"),
                new Megacity("Москва", 1, 1147, "МО", 1, 1),
                new Address("Дом", "Минск", "Ленина", 1, 1)
            };
            string[] expected = { "Место", "Область", "Город", "Мегаполис", "Адрес" };

            for (int i = 0; i < places.Length; i++)
            {
                Place p = places[i];
                string output = ConsoleHelper.CaptureOutput(() => p.Show());
                StringAssert.Contains(output, expected[i]);
            }
        }

        [TestMethod]
        public void NonVirtualShow_UsesReferenceType()
        {
            Place p = new Region("Минская", 1, 1, 1);
            string output = ConsoleHelper.CaptureOutput(() => p.ShowNonVirtual());
            StringAssert.Contains(output, "[Place]");
            Assert.IsFalse(output.Contains("[Region]"));
        }

        [TestMethod]
        public void NonVirtualShow_ThroughOwnType_UsesOwnVersion()
        {
            Region r = new Region("Минская", 1, 1, 1);
            string output = ConsoleHelper.CaptureOutput(() => r.ShowNonVirtual());
            StringAssert.Contains(output, "[Region]");
        }

        [TestMethod]
        public void Megacity_IsCity_And_IsPlace()
        {
            object m = new Megacity();
            Assert.IsTrue(m is City);
            Assert.IsTrue(m is Place);
            Assert.IsFalse(m is Region);
        }

        [TestMethod]
        public void RandomInit_Virtual_WorksThroughBaseReference()
        {
            Place p = new Megacity();
            p.RandomInit();
            Megacity m = (Megacity)p;
            Assert.IsTrue(m.MetroStations >= 10);
        }

        [TestMethod]
        public void Equals_Virtual_UsesRealObjectType()
        {
            Place a = new Region("Минская", 100, 200, 3);
            Place b = new Region("Минская", 100, 200, 3);
            Place c = new Region("Минская", 999, 200, 3);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }
    }
}