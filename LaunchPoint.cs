namespace Enlashceoc
{
    internal class LaunchPoint
    {
        static void Main(string[] args)
        {
            // Global configuration
            Console.Title = "Enlashceoc";
            Console.TreatControlCAsInput = false;
            // Call main menu
            _ = new Menu();
        }
    }
}
