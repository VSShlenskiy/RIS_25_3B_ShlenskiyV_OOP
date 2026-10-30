using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba9.Tests
{
    [TestClass]
    public class ProgramTests
    {
        private static TimeArray Make(params Time[] items)
        {
            return new TimeArray(items);
        }

        [TestMethod]
        public void Max_SingleElement()
        {
            AssertEx.TimeIs(3, 20, Program.Max(Make(new Time(3, 20))));
        }

        [TestMethod]
        public void Max_AtBeginning()
        {
            TimeArray a = Make(new Time(9, 0), new Time(2, 0), new Time(5, 30));
            AssertEx.TimeIs(9, 0, Program.Max(a));
        }

        [TestMethod]
        public void Max_InMiddle()
        {
            TimeArray a = Make(new Time(1, 0), new Time(9, 59), new Time(5, 30));
            AssertEx.TimeIs(9, 59, Program.Max(a));
        }

        [TestMethod]
        public void Max_AtEnd()
        {
            TimeArray a = Make(new Time(1, 0), new Time(2, 0), new Time(10, 1));
            AssertEx.TimeIs(10, 1, Program.Max(a));
        }

        [TestMethod]
        public void Max_SameHours_ComparesMinutes()
        {
            TimeArray a = Make(new Time(4, 10), new Time(4, 50), new Time(4, 30));
            AssertEx.TimeIs(4, 50, Program.Max(a));
        }

        [TestMethod]
        public void Max_AllEqual()
        {
            TimeArray a = Make(new Time(2, 2), new Time(2, 2), new Time(2, 2));
            AssertEx.TimeIs(2, 2, Program.Max(a));
        }

        [TestMethod]
        public void Max_AllZero()
        {
            TimeArray a = Make(new Time(), new Time());
            AssertEx.TimeIs(0, 0, Program.Max(a));
        }

        [TestMethod]
        public void Max_RandomArray_NoElementGreaterThanResult()
        {
            TimeArray a = new TimeArray(30, 20);
            Time max = Program.Max(a);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.IsFalse(a[i] > max);
            }
        }

        [TestMethod]
        public void Max_EmptyArray_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => Program.Max(new TimeArray()));
        }

        [TestMethod]
        public void Max_Null_Throws()
        {
            AssertEx.Throws<System.ArgumentException>(() => Program.Max(null));
        }
    }
}
