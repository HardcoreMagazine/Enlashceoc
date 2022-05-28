namespace Enlashceoc
{
    // Main game menu
    internal class Menu
    {
        static void GenerateField()
        {
            Console.Title = "Enlashceoc";
            ConsoleKey pressedKey = Console.ReadKey().Key;
        }
        static void ActionNewGame()
        {
            //
        }
        static void ActionScoreboard()
        {
            //
        }
        static void ActionExit()
        {
            //
        }


        // Self-initialization
        public Menu()
        {
            GenerateField();
        }
    }
}
