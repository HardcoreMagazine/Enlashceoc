namespace Enlashceoc.Game
{
    internal class NewGame
    {
        private static char heading = ' ';
        private static int X = 1, Y = 1; //player position
        //X - character number in row (allowed values: 1..118; total: 0..119)
        //Y - row number (allowed values: 1..28; total: 0..29)
        private static int score = 0;
        private static bool playerHasKey = false; //key is required to escape
        static int h = Console.WindowHeight; //default: 30
        static int w = Console.WindowWidth; //default: 120
        private static string space = "";

        // Reset board && player position to default values
        static void ResetGame()
        {
            X = 1; //default player X pos
            Y = 1; //default player Y pos
            space = "";
            playerHasKey = false;
        }

        // Replace single object on board by X, Y coordinates
        static void ReplaceObject(int Xpos, int Ypos, char replacement)
        {
            // Note: does not work directly ('space[index] = replacement;')
            // see error code 'CS0200'
            char[] temp = space.ToCharArray();
            temp[GetPos(Xpos, Ypos)] = replacement;
            space = new string(temp);
        }

        // Replace single object on board by index
        static void ReplaceObject(int index, char replacement)
        {
            char[] temp = space.ToCharArray();
            temp[index] = replacement;
            space = new string(temp);
        }

        // Convert X, Y coordinates into array index
        static int GetPos(int Xpos, int Ypos)
        {
            return Ypos * w + Xpos ;
            // 'space' (board) string is basically array of characters.
            // By default, 'space' size is 3599 characters;
            // for ease of access to player position
            // it's INDEX represnted by X, Y values (2D-axis).
            // In order to retrieve object position (CELL) in 'space'
            // we need to find characters ROW (range): Y * window_width
            // && add to this number COLUMN number: + X
        }

        // Generate pseudo-random number
        static int RNG(int? maxValue = null)
        {
            Random num = new Random(Guid.NewGuid().GetHashCode());
            if (maxValue == null)
                return num.Next(int.MaxValue);
            else
                return num.Next((int)maxValue);
        }

        // Check if path is blocked by an obstacle
        // Possible obstacles:
        // wall - #;
        // chest - C;
        // key - K;
        // locked door - L.
        static bool IsObstacle(int Xpos, int Ypos)
        {
            char cell = space[GetPos(Xpos, Ypos)];
            if (cell == '#' || cell == 'C' ||
                cell == 'K' || cell == 'L') //return true if obstacle found
                return true;
            else
                return false;
        }

        // Use object in front on player [handler]
        static void ActionUseObject()
        {
            int Xpos = X, Ypos = Y;
            switch (heading)
            {
                case 'u':
                    Ypos -= 1;
                    break;
                case 'd':
                    Ypos += 1;
                    break;
                case 'l':
                    Xpos -= 1;
                    break;
                case 'r':
                    Xpos += 1;
                    break;
                default:
                    return; //quit function if 'heading' is yet empty
            }
            char nextCell = space[GetPos(Xpos, Ypos)];
            switch (nextCell)
            {
                case 'C':
                    score += 1000; //apply effect
                    ReplaceObject(Xpos, Ypos, ' '); //dispose object
                    break;
                case 'K':
                    playerHasKey = true;
                    break;
                case 'L':
                    if (playerHasKey)
                        ReplaceObject(Xpos, Ypos, ' ');
                    break;
                default:
                    return;
            }
        }

        // Adds player on board, processes player input
        // Note: player is constantly moving, do not use this function
        // for object placement!
        static byte PlayerController()
        {
            byte actionResult = 2; //possible values: 2, 1, 0 - continue, win, quit/loss
            char player = '@';

            ReplaceObject(X, Y, player);
            
            // Print board
            Console.Write(space); //will be removed in future -- because map isnt yet generated

            // Player actions handling
            ConsoleKey input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.UpArrow: //move up
                    if (!IsObstacle(X, Y - 1))
                    {
                        ReplaceObject(X, Y, ' '); //remove old player obj from board
                        heading = 'u';
                        Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow: //move down
                    if (!IsObstacle(X, Y + 1))
                    {
                        ReplaceObject(X, Y, ' ');
                        heading = 'd';
                        Y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow: //move left
                    if (!IsObstacle(X - 1, Y))
                    {
                        ReplaceObject(X, Y, ' '); 
                        heading = 'l';
                        X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow: //move right
                    if (!IsObstacle(X + 1, Y))
                    {
                        ReplaceObject(X, Y, ' ');
                        heading = 'r';
                        X += 1;
                    }
                    break;
                case ConsoleKey.Spacebar: //use item
                    ActionUseObject();
                    break;
                case ConsoleKey.Escape: //quit game
                    actionResult = 0;
                    ResetGame();
                    break;
                default:
                    break;
            }
            return actionResult;
        }

        // Overrides 'space' variable, creating empty board with walls
        static void GenerateEmptyBoard()
        {
            string horizontalWall = "##############################" +
                                    "##############################" +
                                    "##############################" +
                                    "##############################";
            //create field inside walls with no objects
            space += horizontalWall;
            for (int i = 1; i < h-2; i++) //Y axis (row)
            {
                for (int j = 0; j < w; j++) //X axis (column)
                {
                    if (j % 119 == 0)
                        space += "#"; //vertical wall
                    else
                        space += " "; //space
                }
            }
            space += horizontalWall.Remove(119);
        }

        // Override 'space' variable, creating playable level
        static void GenerateLevel()
        {
            // Create level borders
            GenerateEmptyBoard();

            // Create player exit (locked door)
            // Stepping into that cell will result in player win
            List<int> possibleExits = new List<int>();
            for (int k = (w - 1) *  2; k < w * h - 2 * w; k += w) //default: w*h = 3600
            {
                possibleExits.Add(k + 1); //238+1, ...., 3479+1
            }
            int randomIndex = RNG(possibleExits.Count - 1);
            ReplaceObject(possibleExits[randomIndex], 'L');
        }

        static void GameController()
        {
            Console.Clear(); //clean console from old UI
            GenerateLevel();
            ReplaceObject(4, 1, 'C'); //chest sample -- only for testing
            while (true)
            {
                //GenerateBoard(); //'space' gets overwritten each button click
                //                 //-> no need to manually remove old elements
                switch (PlayerController())
                {
                    case 2: //continue game
                        Console.Clear();
                        //space = "";
                        break;
                    case 1: //player won
                        _ = new GameOver(score, true);
                        score = 0; //dont remove this or game will break
                        break;
                    default: //player loss/quit (also covers 'case 0')
                        _ = new GameOver(score, false);
                        score = 0; //dont remove this or game will break
                        break;
                }
            }
        }

        public NewGame()
        {
            GameController();
        }
    }
}
