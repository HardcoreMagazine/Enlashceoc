using System.Text;

namespace Enlashceoc.Game
{
    internal class NewGame
    {
        private static int X = 1, Y = 1;
        //player position on board
        //X - character number in line (1-118)
        //Y - line number (1-28)
        private static int score = 0;
        static int h = Console.WindowHeight; //default: 30
        static int w = Console.WindowWidth; //default: 120
        public static string space = "";


        static void ResetGame()
        {
            X = 1; //default X pos
            Y = 1; //default Y pos
            space = ""; //default board (empty)
        }

        //convert (X, Y) position into array index
        static int GetPos(int X, int Y)
        {
            return Y * w + X ;
        }

        static byte PlayerController()
        {
            byte end = 2; //possible values: 2, 1, 0 - continue, win, quit/loss
            char player = '@';

            //replace character in string
            //note: doesnt work directly ('space[coord] = player;')
            //see: error code CS0200
            char[] temp = space.ToCharArray();
            temp[GetPos(X, Y)] = player;
            space = new string(temp);
            Console.Write(space);
            
            //read key input until valid key pressed
            ConsoleKey input = Console.ReadKey(true).Key;
            switch (input)
            {
                case ConsoleKey.UpArrow: //move up
                    if (Y - 1 > 0)
                        Y -= 1;
                    break;
                case ConsoleKey.DownArrow: //move down
                    if (Y + 1 < h - 1)
                        Y += 1;
                    break;
                case ConsoleKey.RightArrow: //move right
                    if (X + 1 < w - 1)
                        X += 1;
                    break;
                case ConsoleKey.LeftArrow: //move left
                    if (X - 1 > 0)
                        X -= 1;
                    break;
                case ConsoleKey.Spacebar:
                    score += 100;
                    break;
                case ConsoleKey.Escape: //quit game
                    end = 0;
                    ResetGame();
                    break;
                default:
                    break;
            }
            return end;
        }

        //overrides 'space' variable each itaration
        //cleaning board from unused elements
        static void GenerateBoard()
        {
            string horizontalWall = "##############################" +
                                    "##############################" +
                                    "##############################" +
                                    "##############################";
            //create field inside walls with no objects
            space += horizontalWall;
            for (int i = 0; i < h-2; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (j % 119 == 0)
                        space += "#"; //vertical wall
                    else
                        space += " "; //space
                }
            }
            space += horizontalWall.Remove(119);
        }

        static void GameController()
        {
            Console.Clear(); //clean console from old UI
            while (true)
            {
                GenerateBoard(); //'space' gets overwritten each button click
                         //-> no need to manually remove old elements
                switch (PlayerController())
                {
                    case 2: //continue game
                        Console.Clear();
                        space = "";
                        break;
                    case 1: //player won
                        _ = new GameOver(score, true);
                        break;
                    default: //player loss/pressed ESC ('case 0')
                        _ = new GameOver(score, false);
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
