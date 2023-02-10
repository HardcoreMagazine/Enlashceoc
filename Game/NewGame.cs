namespace Enlashceoc.Game
{
    internal class NewGame
    {
        private static int X, Y; // Player position
        // X - character number in row (0..119)
        // Y - row number (0..29)
        private static long T1;
        // Used for playtime calculation on game end
        private static readonly int h = Console.WindowHeight; // Default: 30
        private static readonly int w = Console.WindowWidth; // Default: 120
        private static char[] space = new char[h * w - 1];


        // Reset board && player position to default values
        private static void ResetGame()
        {
            Console.Clear();
            GenerateLevel();
            T1 = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        private static int GetPos(int X, int Y)
        {
            if (X == w & Y == h - 2) // last element
                return w * h - 1;
            else
                return Y * w + X;
        }

        private static int[] ReversePos(int index)
        {
            int posX = index % w;
            int posY = (index - posX) / w;
            return new int[] { posX, posY };
        }

        // Generate pseudo-random number
        private static int RNG(int? minValue = null, int? maxValue = null)
        {
            Random num = new Random(Guid.NewGuid().GetHashCode());
            if (minValue == null)
                if (maxValue == null)
                    return num.Next(0, int.MaxValue);
                else
                    return num.Next(0, (int)maxValue);
            else
                if (maxValue == null)
                return num.Next((int)minValue, int.MaxValue);
            else
                return num.Next((int)minValue, (int)maxValue);
        }

        // Check if movement is legal and path is not blocked by an obstacle
        // Obstacles:
        // wall - '#' or '■';
        // exit - 'E'.
        private static bool IsLegalMovement(int Xpos, int Ypos)
        {
            // 0 <= X <= 119 & 1 <= Y <= 27
            if ((Xpos < w & Xpos >= 0) & (Ypos < h - 2 & Ypos >= 1))
            {
                // Check for obstacles
                char cell = space[GetPos(Xpos, Ypos)];
                if (cell != '■' & cell != 'E')
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        // Use object in front on player [handler]
        private static bool ActionUseObject()
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
                if (cellsAround[i] == 'E')
                    return true;
            return false;
        }

        // Adds player on board, processes player input
        // Note: player is constantly moving, do not use this function
        // for object placement!
        private static byte PlayerController()
        {
            byte actionResult = 2;
            // values: 3 - reset game, 2 - continue, 1 - win, 0 - quit

            //* Render playboard
            Console.Write(space);

            // Player actions handling
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow: //move up
                    if (IsLegalMovement(X, Y - 1))
                    {
                        space[GetPos(X, Y)] = ' '; //remove old player obj from board
                        Y -= 1;
                        space[GetPos(X, Y)] = '@';
                    }
                    break;
                case ConsoleKey.DownArrow: //move down
                    if (IsLegalMovement(X, Y + 1))
                    {
                        space[GetPos(X, Y)] = ' ';
                        Y += 1;
                        space[GetPos(X, Y)] = '@';
                    }
                    break;
                case ConsoleKey.LeftArrow: //move left
                    if (IsLegalMovement(X - 1, Y))
                    {
                        space[GetPos(X, Y)] = ' ';
                        X -= 1;
                        space[GetPos(X, Y)] = '@';
                    }
                    break;
                case ConsoleKey.RightArrow: //move right
                    if (IsLegalMovement(X + 1, Y))
                    {
                        space[GetPos(X, Y)] = ' ';
                        X += 1;
                        space[GetPos(X, Y)] = '@';
                    }
                    break;
                case ConsoleKey.Spacebar: //use item
                    if (ActionUseObject())
                        actionResult = 1;
                    break;
                case ConsoleKey.R: //restart game & create new level
                    actionResult = 3;
                    break;
                case ConsoleKey.Escape: //quit game
                    actionResult = 0;
                    break;
                default:
                    break;
            }
            return actionResult;
        }

        // Override 'space' variable, creating playable level
        private static void GenerateLevel()
        {
            LevelGenerator lg = new LevelGenerator();
            // Unify border walls and maze field:
            for (int i = 0; i < w; i++) space[i] = '#';
            for (int i = 0; i < lg.space.Length; i++) space[i + w] = lg.space[i];
            for (int i = w + lg.space.Length; i < space.Length; i++) space[i] = '#';
            // Set player position:
            int[] revIndex = ReversePos(lg.playerIndex);
            X = revIndex[0];
            Y = revIndex[1];
        }

        private static void GameController()
        {
            Console.Clear(); //clean console from old UI
            GenerateLevel();            
            T1 = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            while (true)
            {
                var T2 = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                switch (PlayerController())
                {
                    case 3: //reset game
                        ResetGame();
                        break;
                    case 2: //continue game
                        Console.Clear();
                        break;
                    case 1: //player won
                        _ = new GameOver(T2 - T1, true);
                        break;
                    default: //player loss/quit (also covers 'case 0')
                        _ = new GameOver(T2 - T1, false);
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
