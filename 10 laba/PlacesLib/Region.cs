using System;
using System.Linq;

namespace PlacesLib
{
    public class Region : Place
    {
        protected int population;
        protected double area;
        protected int districts;
        protected string[] cityNames;

        public int Population
        {
            get { return population; }
            set { population = value >= 0 ? value : 0; }
        }

        public double Area
        {
            get { return area; }
            set { area = value > 0 ? value : 1; }
        }

        public int Districts
        {
            get { return districts; }
            set { districts = value >= 0 ? value : 0; }
        }

        public string[] CityNames
        {
            get { return cityNames; }
            set { cityNames = value ?? new string[0]; }
        }

        public Region() : base()
        {
            population = 0; area = 1; districts = 0; cityNames = new string[0];
        }

        public Region(string name, int population, double area, int districts) : base(name)
        {
            Population = population;
            Area = area;
            Districts = districts;
            cityNames = new string[0];
        }

        public Region(Region r) : base(r)
        {
            population = r.population;
            area = r.area;
            districts = r.districts;
            cityNames = (string[])r.cityNames.Clone();
        }

        public override void Show()
        {
            Console.WriteLine($"Область: {name}, население: {population}, площадь: {area} км², " +
                              $"районов: {districts}, города: [{string.Join(", ", cityNames)}]");
        }

        public new void ShowNonVirtual()
        {
            Console.WriteLine($"[Region] {name}");
        }

        public override void Init()
        {
            base.Init();
            Population = ReadInt("Население: ");
            Area = ReadDouble("Площадь (км²): ");
            Districts = ReadInt("Количество районов: ");
            Console.Write("Города области (через запятую): ");
            string s = Console.ReadLine();
            CityNames = (s ?? "").Split(',', StringSplitOptions.RemoveEmptyEntries)
                                 .Select(x => x.Trim()).ToArray();
        }

        public override void RandomInit()
        {
            string[] names = { "Минская", "Брестская", "Гродненская", "Витебская", "Гомельская", "Могилёвская" };
            string[] cities = { "Борисов", "Молодечно", "Пинск", "Лида", "Орша", "Мозырь" };
            Name = names[rnd.Next(names.Length)] + " область";
            Population = rnd.Next(500_000, 3_000_000);
            Area = rnd.Next(20_000, 50_000);
            Districts = rnd.Next(8, 25);
            int n = rnd.Next(2, 5);
            CityNames = cities.OrderBy(x => rnd.Next()).Take(n).ToArray();
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Region r = (Region)obj;
            return population == r.population && area == r.area && districts == r.districts
                   && cityNames.SequenceEqual(r.cityNames);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), population, area, districts, cityNames.Length);
        }

        public override object Clone() => new Region(this);
    }
}