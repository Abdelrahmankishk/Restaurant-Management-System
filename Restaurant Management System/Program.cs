using Restaurant_Management_System.Helpers;
using Restaurant_Management_System.UI;
using System.Globalization;

namespace Restaurant_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-EG");

            culture.NumberFormat.CurrencyPositivePattern = 3;
            culture.NumberFormat.CurrencyNegativePattern = 8;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            DataSeeder.Seed();
            MainMenu.Run();
        }
    }
}
