using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba9.Tests
{
    [TestClass]
    public class TimeTests
    {

        [TestMethod]
        public void Ctor_Default_IsZero()
        {
            AssertEx.TimeIs(0, 0, new Time());
        }

        [TestMethod]
        public void Ctor_WithParams_SetsValues()
        {
            AssertEx.TimeIs(10, 40, new Time(10, 40));
        }

        [TestMethod]
        public void Ctor_BoundaryValues_Allowed()
        {
            AssertEx.TimeIs(0, 0, new Time(0, 0));
            AssertEx.TimeIs(100, 59, new Time(100, 59));
        }

        [TestMethod]
        public void Ctor_NegativeHours_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new Time(-1, 0));
        }

        [TestMethod]
        public void Ctor_NegativeMinutes_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new Time(1, -1));
        }

        [TestMethod]
        public void Ctor_MinutesOver59_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => new Time(1, 60));
        }

        [TestMethod]
        public void Hours_SetNegative_Throws()
        {
            Time t = new Time(1, 1);
            AssertEx.Throws<System.ArgumentException>(() => t.Hours = -5);
            Assert.AreEqual(1, t.Hours);
        }

        [TestMethod]
        public void Minutes_SetInvalid_Throws()
        {
            Time t = new Time(1, 1);
            AssertEx.Throws<System.ArgumentException>(() => t.Minutes = 60);
            AssertEx.Throws<System.ArgumentException>(() => t.Minutes = -1);
            Assert.AreEqual(1, t.Minutes);
        }

        [TestMethod]
        public void HoursMinutes_SetValid_Works()
        {
            Time t = new Time();
            t.Hours = 7;
            t.Minutes = 59;
            AssertEx.TimeIs(7, 59, t);
        }


        [TestMethod]
        public void Count_IncreasesOnEveryCtor()
        {
            int before = Time.Count;
            new Time();
            new Time(1, 2);
            Assert.AreEqual(before + 2, Time.Count);
        }


        [TestMethod]
        public void ToString_FormatsWithLeadingZeros()
        {
            Assert.AreEqual("05:07", new Time(5, 7).ToString());
            Assert.AreEqual("00:00", new Time().ToString());
            Assert.AreEqual("123:45", new Time(123, 45).ToString());
        }


        [TestMethod]
        public void Subtract_Static_Normal()
        {
            AssertEx.TimeIs(6, 50, Time.Subtract(new Time(10, 40), new Time(3, 50)));
        }

        [TestMethod]
        public void Subtract_Method_Normal()
        {
            AssertEx.TimeIs(6, 50, new Time(10, 40).Subtract(new Time(3, 50)));
        }

        [TestMethod]
        public void Subtract_ResultNegative_ReturnsZero()
        {
            AssertEx.TimeIs(0, 0, Time.Subtract(new Time(3, 50), new Time(10, 40)));
            AssertEx.TimeIs(0, 0, new Time(0, 0).Subtract(new Time(0, 1)));
        }

        [TestMethod]
        public void Subtract_EqualTimes_ReturnsZero()
        {
            AssertEx.TimeIs(0, 0, Time.Subtract(new Time(5, 5), new Time(5, 5)));
        }

        [TestMethod]
        public void Subtract_MinutesBorrow()
        {
            AssertEx.TimeIs(0, 50, Time.Subtract(new Time(1, 10), new Time(0, 20)));
        }

        [TestMethod]
        public void Subtract_DoesNotModifyOperands()
        {
            Time a = new Time(10, 40);
            Time b = new Time(3, 50);
            Time r = a.Subtract(b);
            AssertEx.TimeIs(10, 40, a);
            AssertEx.TimeIs(3, 50, b);
            Assert.AreNotSame(a, r);
        }


        [TestMethod]
        public void Increment_AddsMinute()
        {
            Time t = new Time(10, 30);
            t++;
            AssertEx.TimeIs(10, 31, t);
        }

        [TestMethod]
        public void Increment_CarriesToHour()
        {
            Time t = new Time(10, 59);
            t++;
            AssertEx.TimeIs(11, 0, t);
        }

        [TestMethod]
        public void Increment_Postfix_ReturnsOldValue()
        {
            Time t = new Time(10, 59);
            Time old = t++;
            AssertEx.TimeIs(10, 59, old);
            AssertEx.TimeIs(11, 0, t);
        }

        [TestMethod]
        public void Increment_Prefix_ReturnsNewValue()
        {
            Time t = new Time(10, 59);
            Time res = ++t;
            AssertEx.TimeIs(11, 0, res);
            AssertEx.TimeIs(11, 0, t);
        }

        [TestMethod]
        public void Decrement_SubtractsMinute()
        {
            Time t = new Time(10, 30);
            t--;
            AssertEx.TimeIs(10, 29, t);
        }

        [TestMethod]
        public void Decrement_BorrowsFromHour()
        {
            Time t = new Time(11, 0);
            t--;
            AssertEx.TimeIs(10, 59, t);
        }

        [TestMethod]
        public void Decrement_AtZero_StaysZero()
        {
            Time t = new Time();
            t--;
            AssertEx.TimeIs(0, 0, t);
        }

        [TestMethod]
        public void Decrement_Postfix_ReturnsOldValue()
        {
            Time t = new Time(2, 0);
            Time old = t--;
            AssertEx.TimeIs(2, 0, old);
            AssertEx.TimeIs(1, 59, t);
        }


        [TestMethod]
        public void ImplicitInt_ReturnsTotalMinutes()
        {
            int m = new Time(2, 15);
            Assert.AreEqual(135, m);
        }

        [TestMethod]
        public void ImplicitInt_Zero()
        {
            int m = new Time();
            Assert.AreEqual(0, m);
        }

        [TestMethod]
        public void ExplicitBool_ZeroTime_False()
        {
            Assert.IsFalse((bool)new Time());
        }

        [TestMethod]
        public void ExplicitBool_OnlyMinutes_True()
        {
            Assert.IsTrue((bool)new Time(0, 1));
        }

        [TestMethod]
        public void ExplicitBool_OnlyHours_True()
        {
            Assert.IsTrue((bool)new Time(1, 0));
        }

        [TestMethod]
        public void ExplicitBool_HoursAndMinutes_True()
        {
            Assert.IsTrue((bool)new Time(3, 20));
        }


        [TestMethod]
        public void Less_ByHours()
        {
            Assert.IsTrue(new Time(1, 59) < new Time(2, 0));
            Assert.IsFalse(new Time(2, 0) < new Time(1, 59));
        }

        [TestMethod]
        public void Less_ByMinutes()
        {
            Assert.IsTrue(new Time(2, 15) < new Time(2, 30));
            Assert.IsFalse(new Time(2, 30) < new Time(2, 15));
        }

        [TestMethod]
        public void Greater_ByHoursAndMinutes()
        {
            Assert.IsTrue(new Time(2, 0) > new Time(1, 59));
            Assert.IsTrue(new Time(2, 30) > new Time(2, 15));
            Assert.IsFalse(new Time(2, 15) > new Time(2, 30));
        }

        [TestMethod]
        public void Compare_EqualTimes_BothFalse()
        {
            Time a = new Time(4, 20);
            Time b = new Time(4, 20);
            Assert.IsFalse(a < b);
            Assert.IsFalse(a > b);
        }
    }
}
