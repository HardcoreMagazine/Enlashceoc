namespace Enlashceoc
{
    // Main game menu
    internal class Menu
    {
        static void ActionNewGame()
        {
            NewGame ng = new NewGame();
        }

        static void ActionScoreboard()
        {
            Scoreboard sb = new Scoreboard();
        }

        static void ActionExit()
        {
            Environment.Exit(0);
            //what would you expect?
        }

        static void MenuController()
        {
            Console.Clear(); //remove traces of previous UI
            List<string> menuActions = new List<string>
            {
                //new game
                "\t\t\t\t\t\t   " +
                "> NEW GAME\n\n" +
                "\t\t\t\t\t\t   " +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "EXIT\n\n",
                //scoreboard
                "\t\t\t\t\t\t   " +
                "NEW GAME\n\n" +
                "\t\t\t\t\t\t   " +
                "> SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "EXIT\n\n",
                //exit
                "\t\t\t\t\t\t   " +
                "NEW GAME\n\n" +
                "\t\t\t\t\t\t   " +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "> EXIT\n\n"
            };
            int selection = 0;
            while (true)
            {
                //cycle between menu options
                //each iteration has KeyPress check event:
                //if ArrowDown or ArrowUp key pressed:
                //cycle between menu activities
                //if Enter (Return) key pressed:
                //switch to selected activity, break the loop
                //else:
                //continue the loop, clear console
                bool loopComplete = false;
                GenerateMenuUI(menuActions[selection]);
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
                            selection = 2;
                        else
                            selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selection + 1 > 2)
                            selection = 0;
                        else
                            selection++;
                        break;
                    default:
                        continue;
                }
                if (loopComplete)
                    break;
                else
                    Console.Clear();
            }
        }

        static private void GenerateMenuUI(string menuActions)
        {
            string borderLine =
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
            string gameTitle =
                borderLine +
                "\n" +
                "\t\t\t\t   ______       _           _\n" +
                "\t\t\t\t  |  ____|     | |         | |\n" +
                "\t\t\t\t  | |__   _ __ | | __ _ ___| |__   ___ ___  ___   ___\n" +
                "\t\t\t\t  |  __| | '_ \\| |/ _` / __| '_ \\ / __/ _ \\/ _ \\ / __|\n" +
                "\t\t\t\t  | |____| | | | | (_| \\__ \\ | | | (_|  __/ (_) | (__\n" +
                "\t\t\t\t  |______|_| |_|_|\\__,_|___/_| |_|\\___\\___|\\___/ \\___|\n\n" +
                borderLine +
                "\n\n\n";
            //-----------------------------------------------//
            Console.WriteLine(gameTitle);
            Console.WriteLine(menuActions);
            
            Console.Write("\n\n\n\n\n\n\n" +
                          "version: 0.1 - " +
                          "https://github.com/HardcoreMagazine/Enlashceoc\n" +
                          borderLine.Remove(119));
                          //explanation:
                          //default window size is 120 characters;
                          //on Windows systems last line character
                          //is always "_" (user input)
                          //if we use default 120-char line
                          //"_" symbol will be placed on
                          //next (new) line, which will
                          //result in "format loss"
        }

        // Self-initialization
        public Menu()
        {
            MenuController();
        }
    }
}
