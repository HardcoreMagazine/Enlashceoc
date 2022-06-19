using System.Text;

namespace Enlashceoc.Game
{
    internal class NewGame
    {
        private static int X, Y;
        private static int score = 0;
        static int h = Console.WindowHeight; //default: 30
        static int w = Console.WindowWidth; //default: 120
        //3600 total -> left border indexes: 0, 120, 240, ...
        //public static string space = "";
        public static String space = "";

        static int GetPos(int X, int Y)
        {
            return Y * w + X ;
        }

        static byte PlayerController()
        {
            byte end = 2; //possible values: 2, 1, 0 - continue, win, quit/loss
            char player = '@';

            //default player start:
            X = 5; //character number in line
            Y = 4; //line number

            //replace character in string
            //note: doesnt work directly ('space[coord] = player;')
            //see: error code CS0200
            char[] temp = space.ToCharArray();
            temp[GetPos(X, Y)] = player;
            space = new string(temp);
            
            //read key input until valid key pressed
            ConsoleKey input = Console.ReadKey(true).Key;
            /*while (input != ConsoleKey.UpArrow || input != ConsoleKey.DownArrow ||
                   input != ConsoleKey.RightArrow || input != ConsoleKey.LeftArrow ||
                   input != ConsoleKey.Spacebar || input != ConsoleKey.Escape)
            {
                input = Console.ReadKey(true).Key;
            }
            */
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    score += 100;
                    break;
                case ConsoleKey.DownArrow:
                    break;
                case ConsoleKey.RightArrow:
                    break;
                case ConsoleKey.LeftArrow:
                    break;
                case ConsoleKey.Spacebar:
                    break;
                case ConsoleKey.Escape: //quit game
                    end = 0;
                    break;
                default:
                    break;
            }
            return end;
        }

        static void Board()
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
                    space += " ";
                }
            }
            space += horizontalWall.Remove(119);
            Console.Write(space);
        }

        static void GameController()
        {
            Console.Clear(); //clean console from old UI
            while (true)
            {
                Board(); //'space' gets overwritten each button click
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
