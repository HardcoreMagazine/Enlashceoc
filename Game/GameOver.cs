using System.Text.Json;

namespace Enlashceoc.Game
{
    internal class GameOver
    {
        static void ActionRetry()
        {
            NewGame _ = new NewGame();
        }

        static void ActionScoreboard()
        {
            Scoreboard _ = new Scoreboard();
        }

        static void ActionMenu()
        {
            Menu _ = new Menu();
        }

        static void SaveScore(string playernName, int playerScore)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "gamedata";
            //write player name && score in file in local storage
            if (File.Exists(path)) //check if file exists
            {
                string jsonString = File.ReadAllText(path); //read json from file
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    SaveData[] scoresData = JsonSerializer.Deserialize<SaveData[]>(jsonString)!;
                    //deserialize json and write values into SaveData array
                    for (int i = 0; i < scoresData.Length; i++)
                    {
                        if (playerScore > scoresData[i].Score)
                        {
                            for (int k = scoresData.Length - 1; k > i; k--)
                            {
                                scoresData[k] = scoresData[k - 1];
                            }
                            scoresData[i] = new SaveData { Name = playernName, Score = playerScore };
                            //write new data to file with override
                            string newJsonString = JsonSerializer.Serialize(scoresData);
                            File.WriteAllText(path, newJsonString);
                            //and break the loop
                            break;
                        }
                    }
                }
            }
            else //file does not exist
            {
                //create "empty" scoreboard && write scoreboard data into file
                List<SaveData> scoresData = new List<SaveData>();
                //puts player on first place
                scoresData.Add(new SaveData { Name = playernName, Score = playerScore });
                //and fills other slots with "empty - 0"
                for (int i = 0; i < 4; i++)
                {
                    scoresData.Add(new SaveData { Name = "empty", Score = 0 });
                }
                string jsonString = JsonSerializer.Serialize(scoresData);
                using (StreamWriter wf = File.CreateText(path))
                {
                    wf.WriteLine(jsonString);
                }
            }
        }

        static private void GenerateGameOverUIHeader(int score, string titleText)
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
            Console.WriteLine(title);
            Console.WriteLine("\t\t\t\t\t\t     " +
                              "SCORE\n\n" +
                              "\t\t\t\t\t\t     " +
                              score +
                              "\n\n");
        }

        static private char ProcessUserInput()
        {
            char c = Console.ReadKey().KeyChar;
            //filter key input
            if (c >= '0' && c <= '9' ||
                c >= 'A' && c <= 'Z' ||
                c >= 'a' && c <= 'z')
                return c;
            else
                return '_';
        }

        static private string? GenerateGameOverUIBody(string menuActions, bool isNameReq)
        {
            string? playerName = null;
            if (isNameReq)
            {
                //capture and return a name
                Console.WriteLine("\t\t\t\t\t\t" +
                                  "ENTER YOUR NAME:\n");
                Console.Write("\t\t\t\t\t\t");
                for (int i = 0; i < 5; i++) //name size: 5
                    playerName += ProcessUserInput();
                //override playername if eq "_____" (empty)
                if (playerName == "_____")
                    playerName = "AAAAA";
            }
            Console.WriteLine("\n\n\n" + menuActions);
            return playerName;
        }

        static void GameOverSubcontroller(int score, List<string> menuActions, string titleText, bool type)
        {
            Console.Clear();
            int selection = 0;
            bool loopComplete = false;
            string playerName = "";
            while (true)
            {
                //create 'you win!' or 'game over' headers/titles
                GenerateGameOverUIHeader(score, titleText);
                //ask for player name if player won && name is empty 
                //also: *in any case* print menu actions
                if (type && string.IsNullOrWhiteSpace(playerName))
                    playerName = GenerateGameOverUIBody(menuActions[selection], true)!;
                else
                    GenerateGameOverUIBody(menuActions[selection], false);
                ConsoleKey keyPress = Console.ReadKey(true).Key;
                switch (keyPress)
                {
                    case ConsoleKey.Spacebar:
                        loopComplete = true;
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
                        break;
                }
                if (loopComplete)
                {
                    if (type) //save score only if player wins
                        SaveScore(playerName, score);
                    switch (selection)
                    {
                        case 0:
                            ActionRetry();
                            break;
                        case 1:
                            ActionScoreboard();
                            break;
                        case 2:
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
            if (type)
                GameOverSubcontroller(
                    score,
                    menuActions,
                    "\t\t\t\t__     ______  _    _  __          _______ _   _ _ \n" +
                    "\t\t\t\t\\ \\   / / __ \\| |  | | \\ \\        / /_   _| \\ | | |\n" +
                    "\t\t\t\t \\ \\_/ / |  | | |  | |  \\ \\  /\\  / /  | | |  \\| | |\n" +
                    "\t\t\t\t  \\   /| |  | | |  | |   \\ \\/  \\/ /   | | | . ` | |\n" +
                    "\t\t\t\t   | | | |__| | |__| |    \\  /\\  /   _| |_| |\\  |_|\n" +
                    "\t\t\t\t   |_|  \\____/ \\____/      \\/  \\/   |_____|_| \\_(_)\n\n",
                    type);
            else
                GameOverSubcontroller(
                    score,
                    menuActions,
                    "\t\t\t\t  _____          __  __ ______    ______      ________ _____  \n" +
                    "\t\t\t\t / ____|   /\\   |  \\/  |  ____|  / __ \\ \\    / /  ____|  __ \\ \n" +
                    "\t\t\t\t| |  __   /  \\  | \\  / | |__    | |  | \\ \\  / /| |__  | |__) |\n" +
                    "\t\t\t\t| | |_ | / /\\ \\ | |\\/| |  __|   | |  | |\\ \\/ / |  __| |  _  / \n" +
                    "\t\t\t\t| |__| |/ ____ \\| |  | | |____  | |__| | \\  /  | |____| | \\ \\ \n" +
                    "\t\t\t\t \\_____/_/    \\_\\_|  |_|______|  \\____/   \\/   |______|_|  \\_\\\n\n",
                    type);
        }

        public GameOver(int score, bool type)
        {
            GameOverController(score, type);
        }
    }
}
