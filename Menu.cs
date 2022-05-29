namespace Enlashceoc
{
    // Main game menu
    internal class Menu
    {
        static void GenerateField(string menuActions)
        {
            string borderLine = 
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
            string gameTitle =
                borderLine +
                "\n\n" +
                "\t\t\t\t   ______       _           _\n" +
                "\t\t\t\t  |  ____|     | |         | |\n" +
                "\t\t\t\t  | |__   _ __ | | __ _ ___| |__   ___ ___  ___   ___\n" +
                "\t\t\t\t  |  __| | '_ \\| |/ _` / __| '_ \\ / __/ _ \\/ _ \\ / __|\n" +
                "\t\t\t\t  | |____| | | | | (_| \\__ \\ | | | (_|  __/ (_) | (__\n" +
                "\t\t\t\t  |______|_| |_|_|\\__,_|___/_| |_|\\___\\___|\\___/ \\___|\n\n" +
                borderLine +
                "\n\n\n";

            //-----------------------------------------------//
            Console.Title = "Enlashceoc";
            Console.WriteLine(gameTitle);
            Console.WriteLine(menuActions);
            Console.Write("\n\n\n\n\n\n\n\n" +
                          "version: 0.1\n" +
                          borderLine);
            //~arrow key handler
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
            List<string> menuActions = new List<string>
            {
                //ng
                "\t\t\t\t\t\t" +
                "> NEW GAME\n\n" +
                "\t\t\t\t\t\t" +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t" +
                "EXIT\n\n",
                //sb
                "\t\t\t\t\t\t" +
                "NEW GAME\n\n" +
                "\t\t\t\t\t\t" +
                "> SCOREBOARD\n\n" +
                "\t\t\t\t\t\t" +
                "EXIT\n\n",
                //exit
                "\t\t\t\t\t\t" +
               "NEW GAME\n\n" +
               "\t\t\t\t\t\t" +
               "SCOREBOARD\n\n" +
               "\t\t\t\t\t\t" +
               "> EXIT\n\n"
            };
            while (true)
            {
                GenerateField(menuActions[0]);
                ConsoleKey consoleKey = Console.ReadKey().Key;
                switch (consoleKey)
                {
                    case ConsoleKey.Enter:
                        //
                        Console.Clear();
                        break;
                    case ConsoleKey.UpArrow:
                        //
                        Console.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        //
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
