using System.Collections;

namespace PlacesLib
{
    public class SortByPopulation : IComparer
    {
        public int Compare(object x, object y)
        {
            return GetPopulation(x).CompareTo(GetPopulation(y));
        }

        private int GetPopulation(object o)
        {
            if (o is Region r) return r.Population;
            if (o is Megacity m) return m.Population;
            if (o is City c) return c.Population;
            return 0;
        }
    }
}