using System;

namespace PlacesLib
{
    public class Megacity : City
    {
        protected int metroStations;
        protected int districts;

        public int MetroStations
        {
            get { return metroStations; }
            set { metroStations = value >= 0 ? value : 0; }
        }

        public int Districts
        {
            get { return districts; }
            set { districts = value >= 0 ? value : 0; }
        }

        public Megacity() : base()
        {
            metroStations = 0; districts = 0;
        }

        public Megacity(string name, int population, int foundationYear, string regionName,
                        int metroStations, int districts)
            : base(name, population, foundationYear, regionName)
        {
            MetroStations = metroStations;
            Districts = districts;
        }

        public Megacity(Megacity m) : base(m)
        {
            metroStations = m.metroStations;
            districts = m.districts;
        }

        public override void Show()
        {
            Console.WriteLine($"Мегаполис: {name}, население: {population}, основан в {foundationYear}, область: {regionName}, станций метро: {metroStations}, районов: {districts}");
        }

        public new void ShowNonVirtual()
        {
            Console.WriteLine($"[Megacity] {name}");
        }

        public override void Init()
        {
            base.Init();
            MetroStations = ReadInt("Станций метро: ");
            Districts = ReadInt("Количество районов: ");
        }

        public override void RandomInit()
        {
            string[] names = { "Москва", "Париж", "Берлин", "Лондон", "Токио", "Нью-Йорк" };
            Name = names[rnd.Next(names.Length)];
            Population = rnd.Next(1_000_000, 15_000_000);
            FoundationYear = rnd.Next(800, 1800);
            RegionName = "Столичный регион";
            MetroStations = rnd.Next(10, 300);
            Districts = rnd.Next(5, 30);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Megacity m = (Megacity)obj;
            return metroStations == m.metroStations && districts == m.districts;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), metroStations, districts);
        }

        public override object Clone() => new Megacity(this);
    }
}