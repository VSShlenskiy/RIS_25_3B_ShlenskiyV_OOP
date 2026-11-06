using System;

namespace PlacesLib
{
    public class City : Place
    {
        protected int population;
        protected int foundationYear;
        protected string regionName;

        public int Population
        {
            get { return population; }
            set { population = value >= 0 ? value : 0; }
        }

        public int FoundationYear
        {
            get { return foundationYear; }
            set { foundationYear = (value > 0 && value <= DateTime.Now.Year) ? value : 1900; }
        }

        public string RegionName
        {
            get { return regionName; }
            set { regionName = string.IsNullOrWhiteSpace(value) ? "Не указана" : value; }
        }

        public City() : base()
        {
            population = 0; foundationYear = 1900; regionName = "Не указана";
        }

        public City(string name, int population, int foundationYear, string regionName) : base(name)
        {
            Population = population;
            FoundationYear = foundationYear;
            RegionName = regionName;
        }

        public City(City c) : base(c)
        {
            population = c.population;
            foundationYear = c.foundationYear;
            regionName = c.regionName;
        }

        public override void Show()
        {
            Console.WriteLine($"Город: {name}, население: {population}, основан в {foundationYear}, область: {regionName}");
        }

        public new void ShowNonVirtual()
        {
            Console.WriteLine($"[City] {name}");
        }

        public override void Init()
        {
            base.Init();
            Population = ReadInt("Население: ");
            FoundationYear = ReadInt("Год основания: ");
            Console.Write("Область: ");
            RegionName = Console.ReadLine();
        }

        public override void RandomInit()
        {
            string[] names = { "Борисов", "Молодечно", "Барановичи", "Пинск", "Лида", "Орша", "Мозырь" };
            string[] regions = { "Минская область", "Брестская область", "Гродненская область",
                                 "Витебская область", "Гомельская область" };
            Name = names[rnd.Next(names.Length)];
            Population = rnd.Next(10_000, 500_000);
            FoundationYear = rnd.Next(1000, 1950);
            RegionName = regions[rnd.Next(regions.Length)];
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            City c = (City)obj;
            return population == c.population && foundationYear == c.foundationYear && regionName == c.regionName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), population, foundationYear, regionName);
        }

        public override object Clone() => new City(this);
    }
}