using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlacesLib;

namespace Laba10.Tests
{
    [TestClass]
    public class PlaceTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaultName()
        {
            Place p = new Place();
            Assert.AreEqual("Без названия", p.Name);
        }

        [TestMethod]
        public void ParamConstructor_SetsName()
        {
            Place p = new Place("Парк");
            Assert.AreEqual("Парк", p.Name);
        }

        [TestMethod]
        public void Name_EmptyOrNull_ReplacedWithDefault()
        {
            Place p = new Place("Парк");
            p.Name = "";
            Assert.AreEqual("Без названия", p.Name);
            p.Name = null;
            Assert.AreEqual("Без названия", p.Name);
            p.Name = "   ";
            Assert.AreEqual("Без названия", p.Name);
        }

        [TestMethod]
        public void CopyConstructor_CopiesName_AndIsIndependent()
        {
            Place a = new Place("Парк");
            Place b = new Place(a);
            Assert.AreEqual("Парк", b.Name);
            a.Name = "Сквер";
            Assert.AreEqual("Парк", b.Name);
        }

        [TestMethod]
        public void Equals_SameName_True()
        {
            Assert.IsTrue(new Place("Парк").Equals(new Place("Парк")));
        }

        [TestMethod]
        public void Equals_DifferentName_False()
        {
            Assert.IsFalse(new Place("Парк").Equals(new Place("Сквер")));
        }

        [TestMethod]
        public void Equals_Null_False()
        {
            Assert.IsFalse(new Place("Парк").Equals(null));
        }

        [TestMethod]
        public void Equals_DifferentTypes_False()
        {
            Place p = new Place("Минск");
            Region r = new Region("Минск", 1, 1, 1);
            Assert.IsFalse(p.Equals(r));
        }

        [TestMethod]
        public void RandomInit_SetsNonEmptyName()
        {
            Place p = new Place();
            p.RandomInit();
            Assert.IsFalse(string.IsNullOrWhiteSpace(p.Name));
        }

        [TestMethod]
        public void Show_PrintsName()
        {
            Place p = new Place("Парк");
            string output = ConsoleHelper.CaptureOutput(() => p.Show());
            StringAssert.Contains(output, "Парк");
        }

        [TestMethod]
        public void Init_ReadsNameFromConsole()
        {
            Place p = new Place();
            ConsoleHelper.WithInput("Сквер\n", () => p.Init());
            Assert.AreEqual("Сквер", p.Name);
        }
    }
}