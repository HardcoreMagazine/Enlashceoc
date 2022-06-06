namespace Enlashceoc
{
    internal class GameOver
    {
        static void ActionRetry()
        {
            NewGame ng = new NewGame();
        }

        static void ActionScoreboard()
        {
            Scoreboard sb = new Scoreboard();
        }

        static void ActionMenu()
        {
            Menu menu = new Menu();
        }

        static private void GenerateGameOverWinUI(int score, string menuActions, string titleText)
        {
            string borderLine =
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
            string title =
                borderLine +
                "\n" +
                titleText +
                borderLine +
                "\n\n\n";
            //-----------------------------------------------//
            Console.WriteLine(title);
            Console.WriteLine("\t\t\t\t\t\t\t" +
                              "SCORE\n\n" +
                              "\t\t\t\t\t\t\t" +
                              score +
                              "\n\n\n");
            Console.WriteLine(menuActions);
        }

        static void GameOverSubcontroller(int score, List<string> menuActions, string titleText)
        {
            Console.Clear();
            int selection = 0;
            bool loopComplete = false;
            while (true)
            {
                GenerateGameOverWinUI(score, menuActions[selection], titleText);
                ConsoleKey keyPress = Console.ReadKey(true).Key;
                switch(keyPress)
                {
                    case ConsoleKey.Spacebar:
                        loopComplete = true;
                        break;
                    case ConsoleKey.UpArrow:
                        if (selection - 1 < 0)
                            selection = menuActions.Count - 1;
                        else
                            selection--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (selection + 1 > menuActions.Count - 1)
                            selection = 0;
                        else
                            selection++;
                        break;
                    default:
                        break;
                }
                if (loopComplete)
                {
                    switch(selection)
                    {
                        case 0:
                            ActionRetry();
                            break;
                        case 1:
                            if (menuActions.Count == 3)
                                ActionScoreboard();
                            else
                                ActionMenu();
                            break;
                        case 2:
                            if (menuActions.Count == 3) //just in case
                                ActionMenu();
                            break;
                    }
                    break;
                }
                else
                    Console.Clear();
            }
        }

        static void GameOverController(int score, bool type)
        {
            List<string> menuActions = new List<string>
            {
                //retry
                "\t\t\t\t\t\t   " +
                "> RETRY\n\n" +
                "\t\t\t\t\t\t   " +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "MENU\n\n",
                //scoreboard
                "\t\t\t\t\t\t   " +
                "RETRY\n\n" +
                "\t\t\t\t\t\t   " +
                "> SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "MENU\n\n",
                //exit to menu
                "\t\t\t\t\t\t   " +
                "RETRY\n\n" +
                "\t\t\t\t\t\t   " +
                "SCOREBOARD\n\n" +
                "\t\t\t\t\t\t   " +
                "> MENU\n\n"
            };
            List<string> altMenuActions = new List<string>
            {
                //retry
                "\t\t\t\t\t\t   " +
                "> RETRY\n\n" +
                "\t\t\t\t\t\t   " +
                "MENU\n\n",
                //exit to menu
                "\t\t\t\t\t\t   " +
                "RETRY\n\n" +
                "\t\t\t\t\t\t   " +
                "> MENU\n\n"
            };
            string gameOverWinTitleText =
                "\t\t\t\t__     ______  _    _  __          _______ _   _ _ \n" +
                "\t\t\t\t\\ \\   / / __ \\| |  | | \\ \\        / /_   _| \\ | | |\n" +
                "\t\t\t\t \\ \\_/ / |  | | |  | |  \\ \\  /\\  / /  | | |  \\| | |\n" +
                "\t\t\t\t  \\   /| |  | | |  | |   \\ \\/  \\/ /   | | | . ` | |\n" +
                "\t\t\t\t   | | | |__| | |__| |    \\  /\\  /   _| |_| |\\  |_|\n" +
                "\t\t\t\t   |_|  \\____/ \\____/      \\/  \\/   |_____|_| \\_(_)\n\n";
            string gameOverLossTitleText =
                "\t\t\t\t  _____          __  __ ______    ______      ________ _____  \n" +
                "\t\t\t\t / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\ \n" +
                "\t\t\t\t| |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) |\n" +
                "\t\t\t\t| | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  / \n" +
                "\t\t\t\t| |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\ \n" +
                "\t\t\t\t \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_\\\n\n";
            if (type)
            {
                GameOverSubcontroller(score, menuActions, gameOverWinTitleText);
            }
            else
            {
                GameOverSubcontroller(score, altMenuActions, gameOverLossTitleText);
            }
        }

        public GameOver(int score, bool type)
        {
            GameOverController(score, type);
        }
    }
}
