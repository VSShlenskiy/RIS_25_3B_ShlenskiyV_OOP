using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Laba9.Tests
{
    internal static class AssertEx
    {
        public static void Throws<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(T))
                {
                    return;
                }
                Assert.Fail($"Ожидалось {typeof(T).Name}, но выброшено {ex.GetType().Name}");
            }
            Assert.Fail($"Ожидалось исключение {typeof(T).Name}, но оно не выброшено");
        }

        public static void TimeIs(int hours, int minutes, Time t)
        {
            Assert.IsNotNull(t);
            Assert.AreEqual(hours, t.Hours, "Часы");
            Assert.AreEqual(minutes, t.Minutes, "Минуты");
        }
    }
}
