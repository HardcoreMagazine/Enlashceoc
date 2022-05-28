namespace Enlashceoc
{
    // Main game menu
    internal class Menu
    {
        static void GenerateField()
        {
            Console.Title = "Enlashceoc";
            Console.WriteLine(
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n" +
            "   ______       _           _\n" +
            "  |  ____|     | |         | |\n" +
            "  | |__   _ __ | | __ _ ___| |__   ___ ___  ___   ___\n" + 
            "  |  __| | '_ \\| |/ _` / __| '_ \\ / __/ _ \\/ _ \\ / __|\n" + 
            "  | |____| | | | | (_| \\__ \\ | | | (_|  __/ (_) | (__\n" + 
            "  |______|_| |_|_|\\__,_|___/_| |_|\\___\\___|\\___/ \\___|\n" +
            "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@\n\n\n");
            Console.WriteLine("ascii-art-menu-actions");
            //arrow key handler
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
