using System;
using System.Collections;
using System.Linq;

namespace PlacesLib
{
    public interface IInit
    {
        void Init();
        void RandomInit();
    }

    public class Place : IInit, IComparable, ICloneable
    {
        protected static Random rnd = new Random();
        protected string name;

        public string Name
        {
            get { return name; }
            set { name = string.IsNullOrWhiteSpace(value) ? "Без названия" : value; }
        }

        public Place() { name = "Без названия"; }
        public Place(string name) { Name = name; }
        public Place(Place other) { name = other.name; }

        public virtual void Show()
        {
            Console.WriteLine($"Место: {name}");
        }

        public void ShowNonVirtual()
        {
            Console.WriteLine($"[Place] {name}");
        }

        public virtual void Init()
        {
            Console.Write("Название: ");
            Name = Console.ReadLine();
        }

        public virtual void RandomInit()
        {
            string[] names = { "Центральное место", "Старый район", "Северный край",
                               "Южная окраина", "Западный квартал", "Восточный холм" };
            Name = names[rnd.Next(names.Length)];
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return name == ((Place)obj).name;
        }

        public override int GetHashCode()
        {
            return name == null ? 0 : name.GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Place other))
                throw new ArgumentException("Объект не является Place");
            return string.Compare(name, other.name, StringComparison.Ordinal);
        }

        public virtual object Clone() => new Place(this);

        public Place ShallowCopy() => (Place)MemberwiseClone();

        protected static int ReadInt(string prompt)
        {
            Console.Write(prompt);
            int v;
            while (!int.TryParse(Console.ReadLine(), out v))
                Console.Write("Введите целое число: ");
            return v;
        }

        protected static double ReadDouble(string prompt)
        {
            Console.Write(prompt);
            double v;
            while (!double.TryParse(Console.ReadLine(), out v))
                Console.Write("Введите число: ");
            return v;
        }
    }

    public class Address : Place
    {
        protected string cityName;
        protected string street;
        protected int house;
        protected int flat;

        public string CityName
        {
            get { return cityName; }
            set { cityName = string.IsNullOrWhiteSpace(value) ? "Не указан" : value; }
        }

        public string Street
        {
            get { return street; }
            set { street = string.IsNullOrWhiteSpace(value) ? "Не указана" : value; }
        }

        public int House
        {
            get { return house; }
            set { house = value > 0 ? value : 1; }
        }

        public int Flat
        {
            get { return flat; }
            set { flat = value >= 0 ? value : 0; }
        }

        public Address() : base()
        {
            cityName = "Не указан"; street = "Не указана"; house = 1; flat = 0;
        }

        public Address(string name, string cityName, string street, int house, int flat) : base(name)
        {
            CityName = cityName;
            Street = street;
            House = house;
            Flat = flat;
        }

        public Address(Address a) : base(a)
        {
            cityName = a.cityName;
            street = a.street;
            house = a.house;
            flat = a.flat;
        }

        public override void Show()
        {
            Console.WriteLine($"Адрес ({name}): г. {cityName}, ул. {street}, д. {house}, кв. {flat}");
        }

        public new void ShowNonVirtual()
        {
            Console.WriteLine($"[Address] {name}");
        }

        public override void Init()
        {
            base.Init();
            Console.Write("Город: ");
            CityName = Console.ReadLine();
            Console.Write("Улица: ");
            Street = Console.ReadLine();
            House = ReadInt("Дом: ");
            Flat = ReadInt("Квартира: ");
        }

        public override void RandomInit()
        {
            string[] names = { "Жилой дом", "Офис", "Школа", "Магазин", "Больница" };
            string[] cities = { "Минск", "Брест", "Гродно", "Витебск", "Гомель" };
            string[] streets = { "Ленина", "Советская", "Мира", "Садовая", "Центральная", "Победы" };
            Name = names[rnd.Next(names.Length)];
            CityName = cities[rnd.Next(cities.Length)];
            Street = streets[rnd.Next(streets.Length)];
            House = rnd.Next(1, 150);
            Flat = rnd.Next(0, 200);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Address a = (Address)obj;
            return cityName == a.cityName && street == a.street && house == a.house && flat == a.flat;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), cityName, street, house, flat);
        }

        public override object Clone() => new Address(this);
    }

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

    public class Landmark : IInit
    {
        private static Random rnd = new Random();
        private string title = "";
        private int year;

        public string Title
        {
            get { return title; }
            set { title = string.IsNullOrWhiteSpace(value) ? "Без названия" : value; }
        }

        public int Year
        {
            get { return year; }
            set { year = (value > 0 && value <= DateTime.Now.Year) ? value : 1900; }
        }

        public void Init()
        {
            Console.Write("Название достопримечательности: ");
            Title = Console.ReadLine();
            Console.Write("Год создания: ");
            int v;
            while (!int.TryParse(Console.ReadLine(), out v)) Console.Write("Введите целое: ");
            Year = v;
        }

        public void RandomInit()
        {
            string[] titles = { "Мирский замок", "Софийский собор", "Брестская крепость",
                                "Нарочанский парк", "Хатынь" };
            Title = titles[rnd.Next(titles.Length)];
            Year = rnd.Next(1000, 2000);
        }

        public void Show() => Console.WriteLine($"Достопримечательность: {title}, год: {year}");
    }

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