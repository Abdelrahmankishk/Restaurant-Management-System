using Restaurant_Management_System.Helpers;
using Restaurant_Management_System.UI;

namespace Restaurant_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataSeeder.Seed();
            MainMenu.Run();
        }
    }
}
