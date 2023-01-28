namespace Enlashceoc.Game
{
    internal class NewGame
    {
        private static int X = 1, Y = 1; //player position
        //X - character number in row (allowed values: 1..118; total: 0..119)
        //Y - row number (allowed values: 1..28; total: 0..29)
        private static long timer1 = 0;
        //used for playtime calculation on game end
        static readonly int h = Console.WindowHeight; //default: 30
        static readonly int w = Console.WindowWidth; //default: 120
        private static string space = "";

        // Reset board && player position to default values
        static void ResetGame()
        {
            X = 1; //default player X pos
            Y = 1; //default player Y pos
            space = "";
        }

        // Replace single object on board by X, Y coordinates
        static void ReplaceObject(int Xpos, int Ypos, char replacement)
        {
            // Note: does not work directly ('space[index] = replacement;')
            // see: MS docs page error code 'CS0200'
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
        // exit - E.
        static bool IsObstacle(int Xpos, int Ypos)
        {
            char cell = space[GetPos(Xpos, Ypos)];
            if (cell == '#' || cell == 'E') //return true if obstacle found
                return true;
            else
                return false;
        }

        // Use object in front on player [handler]
        static bool ActionUseObject()
        {
            //check 4 cells around player (up, down, right, left)
            char[] cellsAround =
            {
                space[GetPos(X, Y-1)],
                space[GetPos(X, Y+1)],
                space[GetPos(X+1, Y)],
                space[GetPos(X-1, Y)]
            };
            for (int i = 0; i < cellsAround.Length; i++)
            {
                if (cellsAround[i] == 'E')
                {
                    return true;
                }
            }
            return false;
        }

        // Adds player on board, processes player input
        // Note: player is constantly moving, do not use this function
        // for object placement!
        static byte PlayerController()
        {
            byte actionResult = 2; //values: 2 - continue, 1 - win - quit/loss
            char player = '@';

            ReplaceObject(X, Y, player);
            
            //* Render playboard
            Console.Write(space);

            // Player actions handling
            ConsoleKey input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.UpArrow: //move up
                    if (!IsObstacle(X, Y - 1))
                    {
                        ReplaceObject(X, Y, ' '); //remove old player obj from board
                        Y -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow: //move down
                    if (!IsObstacle(X, Y + 1))
                    {
                        ReplaceObject(X, Y, ' ');
                        Y += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow: //move left
                    if (!IsObstacle(X - 1, Y))
                    {
                        ReplaceObject(X, Y, ' '); 
                        X -= 1;
                    }
                    break;
                case ConsoleKey.RightArrow: //move right
                    if (!IsObstacle(X + 1, Y))
                    {
                        ReplaceObject(X, Y, ' ');
                        X += 1;
                    }
                    break;
                case ConsoleKey.Spacebar: //use item
                    if (ActionUseObject())
                    {
                        actionResult = 1;
                    }
                    //ResetGame();
                    break;
                case ConsoleKey.Escape: //quit game
                    actionResult = 0;
                    //ResetGame();
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
            // Reset board before use
            // *is required because of bug occuring on new game
            // after player's win that causes console to print out
            // pile of random garbage
            ResetGame();

            // Create level borders
            GenerateEmptyBoard();

            // Create player exit (locked door)
            // Stepping into that cell will result in player win
            List<int> possibleExits = new List<int>();
            for (int k = (w - 1) *  2; k < w * h - 2 * w; k += w) //default: w*h = 3600
            {
                possibleExits.Add(k + 1); //238+1, ...., 3479+1
            }
            //randomly place maze Exit on right wall
            int randomIndex = RNG(possibleExits.Count - 1);
            ReplaceObject(possibleExits[randomIndex], 'E');
            //TODO
            //TODO
        }

        static void GameController()
        {
            Console.Clear(); //clean console from old UI
            GenerateLevel();            
            timer1 = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            while (true)
            {
                var timer2 = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                switch (PlayerController())
                {
                    case 2: //continue game
                        Console.Clear();
                        break;
                    case 1: //player won
                        _ = new GameOver(timer2 - timer1, true);
                        break;
                    default: //player loss/quit (also covers 'case 0')
                        _ = new GameOver(timer2 - timer1, false);
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
