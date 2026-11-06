using System;

namespace PlacesLib
{
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
}