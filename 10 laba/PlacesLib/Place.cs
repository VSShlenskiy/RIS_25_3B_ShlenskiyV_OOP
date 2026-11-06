using System;

namespace PlacesLib
{
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
}