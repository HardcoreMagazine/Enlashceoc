namespace Enlashceoc
{
    internal class LaunchPoint
    {
        static void Main(string[] args)
        {
            //global configuration
            Console.Title = "Enlashceoc";
            Console.TreatControlCAsInput = false;
            //call main menu
            _ = new Menu();
        }
    }
}
