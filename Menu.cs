namespace Enlashceoc
{
    // Main game menu
    internal class Menu
    {
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
            Environment.Exit(0);
        }
        static void MenuController()
        {
            List<string> menuActions = new List<string>
            {
                //new game
                "\t\t\t\t\t\t" +
                "> NEW GAME\n\n" +
                "\t\t\t\t\t\t" +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t" +
                "EXIT\n\n",
                //scoreboard
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
            int selection = 0;
            while (true)
            {
                Console.TreatControlCAsInput = false;
                bool loopComplete = false;
                GenerateUI(menuActions[selection]);
                var consoleKey = Console.ReadKey(true).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.Enter:
                        loopComplete = true;
                        switch (selection)
                        {
                            case 0:
                                ActionNewGame();
                                break;
                            case 1:
                                ActionScoreboard();
                                break;
                            case 2:
                                ActionExit();
                                break;
                            default:
                                break;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (selection - 1 < 0)
                        {
                            selection = 2;
                        }
                        else
                        {
                            selection--;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selection + 1 > 2)
                        {
                            selection = 0;
                        }
                        else
                        {
                            selection++;
                        }
                        break;
                    default:
                        continue;
                }
                Console.Clear();
                if (loopComplete)
                {
                    break;
                }
            }
        }
        static private void GenerateUI(string menuActions)
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
                          "version: 0.1 - " +
                          "https://github.com/HardcoreMagazine/Enlashceoc\n" +
                          borderLine);
        }
        // Self-initialization
        public Menu()
        {
            MenuController();
        }
    }
}
