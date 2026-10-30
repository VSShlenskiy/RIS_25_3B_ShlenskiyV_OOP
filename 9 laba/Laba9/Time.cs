using System;

namespace Laba9
{
    class Time
    {
        private int hours;
        private int minutes;
        private static int count = 0;

        public int Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Часы не могут быть < 0");
                }
                hours = value;
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                if (value < 0 || value > 59)
                {
                    throw new ArgumentException("Минуты должны быть 0..59");
                }
                minutes = value;
            }
        }

        private int TotalMinutes
        {
            get
            {
                return Hours * 60 + Minutes;
            }
        }

        public static int Count
        {
            get
            {
                return count;
            }
        }

        public Time()
        {
            Hours = 0;
            Minutes = 0;
            count++;
        }

        public Time(int h, int m)
        {
            Hours = h;
            Minutes = m;
            count++;
        }

        private static Time FromMinutes(int total)
        {
            if (total < 0)
            {
                total = 0;
            }
            return new Time(total / 60, total % 60);
        }

        public override string ToString()
        {
            return $"{Hours:D2}:{Minutes:D2}";
        }


        public static Time Subtract(Time a, Time b)
        {
            return FromMinutes(a.TotalMinutes - b.TotalMinutes);
        }

        public Time Subtract(Time other)
        {
            return Subtract(this, other);
        }


        public static Time operator ++(Time t)
        {
            return FromMinutes(t.TotalMinutes + 1);
        }

        public static Time operator --(Time t)
        {
            return FromMinutes(t.TotalMinutes - 1);
        }

        public static implicit operator int(Time t)
        {
            return t.TotalMinutes;
        }

        public static explicit operator bool(Time t)
        {
            return t.Hours != 0 || t.Minutes != 0;
        }

        public static bool operator <(Time t1, Time t2)
        {
            return t1.TotalMinutes < t2.TotalMinutes;
        }

        public static bool operator >(Time t1, Time t2)
        {
            return t1.TotalMinutes > t2.TotalMinutes;
        }
    }
}