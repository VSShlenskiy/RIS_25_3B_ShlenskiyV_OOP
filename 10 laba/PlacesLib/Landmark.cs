using System;

namespace PlacesLib
{
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
}