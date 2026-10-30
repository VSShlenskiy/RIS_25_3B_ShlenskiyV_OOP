using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba9.Tests
{
    [TestClass]
    public class TimeArrayTests
    {

        [TestMethod]
        public void Ctor_Default_EmptyArray()
        {
            TimeArray a = new TimeArray();
            Assert.AreEqual(0, a.Length);
            Assert.AreEqual("(массив пуст)", a.View());
        }

        [TestMethod]
        public void Ctor_Random_CorrectLengthAndRanges()
        {
            TimeArray a = new TimeArray(50, 5);
            Assert.AreEqual(50, a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.IsNotNull(a[i]);
                Assert.IsTrue(a[i].Hours >= 0 && a[i].Hours <= 5, "Часы вне диапазона");
                Assert.IsTrue(a[i].Minutes >= 0 && a[i].Minutes <= 59, "Минуты вне диапазона");
            }
        }

        [TestMethod]
        public void Ctor_Random_ZeroSize_Empty()
        {
            Assert.AreEqual(0, new TimeArray(0, 10).Length);
        }

        [TestMethod]
        public void Ctor_Random_ZeroMaxHours_AllHoursZero()
        {
            TimeArray a = new TimeArray(20, 0);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.AreEqual(0, a[i].Hours);
            }
        }

        [TestMethod]
        public void Ctor_Random_NegativeSize_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new TimeArray(-1, 5));
        }

        [TestMethod]
        public void Ctor_Random_NegativeMaxHours_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new TimeArray(3, -1));
        }

        [TestMethod]
        public void Ctor_Items_CopiesValues()
        {
            Time[] items = { new Time(1, 5), new Time(2, 30) };
            TimeArray a = new TimeArray(items);
            Assert.AreEqual(2, a.Length);
            AssertEx.TimeIs(1, 5, a[0]);
            AssertEx.TimeIs(2, 30, a[1]);
        }

        [TestMethod]
        public void Ctor_Items_ArrayIsIndependentFromSource()
        {
            Time[] items = { new Time(1, 5), new Time(2, 30) };
            TimeArray a = new TimeArray(items);
            items[0] = new Time(9, 9);
            AssertEx.TimeIs(1, 5, a[0]);
        }

        [TestMethod]
        public void Ctor_Items_EmptyArray_Works()
        {
            Assert.AreEqual(0, new TimeArray(new Time[0]).Length);
        }

        [TestMethod]
        public void Ctor_Items_Null_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new TimeArray((Time[])null));
        }

        [TestMethod]
        public void Ctor_Items_NullElement_Throws()
        {
            Time[] items = { new Time(1, 1), null };
            AssertEx.Throws<System.ArgumentException>(() => new TimeArray(items));
        }


        [TestMethod]
        public void Count_IncreasesOnEveryCtor()
        {
            int before = TimeArray.Count;
            new TimeArray();
            new TimeArray(2, 3);
            new TimeArray(new Time[0]);
            Assert.AreEqual(before + 3, TimeArray.Count);
        }

        [TestMethod]
        public void TimeCount_RandomCtorCreatesOneTimePerElement()
        {
            int before = Time.Count;
            new TimeArray(7, 3);
            Assert.AreEqual(before + 7, Time.Count);
        }


        [TestMethod]
        public void Indexer_Get_ValidIndex()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 1), new Time(2, 2), new Time(3, 3) });
            AssertEx.TimeIs(1, 1, a[0]);
            AssertEx.TimeIs(3, 3, a[2]);
        }

        [TestMethod]
        public void Indexer_Set_ValidIndex()
        {
            TimeArray a = new TimeArray(2, 1);
            a[0] = new Time(7, 7);
            a[1] = new Time(8, 8);
            AssertEx.TimeIs(7, 7, a[0]);
            AssertEx.TimeIs(8, 8, a[1]);
        }

        [TestMethod]
        public void Indexer_Get_NegativeIndex_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => { Time t = a[-1]; });
        }

        [TestMethod]
        public void Indexer_Get_IndexEqualsLength_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => { Time t = a[3]; });
        }

        [TestMethod]
        public void Indexer_Get_IndexFarOutside_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => { Time t = a[100]; });
        }

        [TestMethod]
        public void Indexer_Set_NegativeIndex_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => a[-1] = new Time(1, 1));
        }

        [TestMethod]
        public void Indexer_Set_IndexEqualsLength_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => a[3] = new Time(1, 1));
        }

        [TestMethod]
        public void Indexer_Set_Null_Throws()
        {
            TimeArray a = new TimeArray(3, 1);
            AssertEx.Throws<System.ArgumentException>(() => a[0] = null);
        }

        [TestMethod]
        public void Indexer_EmptyArray_AnyIndexThrows()
        {
            TimeArray a = new TimeArray();
            AssertEx.Throws<System.ArgumentException>(() => { Time t = a[0]; });
            AssertEx.Throws<System.ArgumentException>(() => a[0] = new Time(1, 1));
        }

        [TestMethod]
        public void Indexer_FailedSet_DoesNotChangeArray()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 1) });
            AssertEx.Throws<System.ArgumentException>(() => a[0] = null);
            AssertEx.TimeIs(1, 1, a[0]);
        }


        [TestMethod]
        public void View_FormatsElements()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 5), new Time(2, 30) });
            Assert.AreEqual("[0] 01:05  [1] 02:30", a.View());
        }

        [TestMethod]
        public void View_SingleElement_NoTrailingSpaces()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(0, 0) });
            Assert.AreEqual("[0] 00:00", a.View());
        }
    }
}
