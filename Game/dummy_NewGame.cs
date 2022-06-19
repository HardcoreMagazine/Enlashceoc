namespace Enlashceoc.Game
{
    internal class dummy_NewGame
    {
        static int RNG()
        {
            Random num = new Random(Guid.NewGuid().GetHashCode());
            return num.Next(int.MaxValue);
        }

        static void NewGameController()
        {
            Console.Clear();
            int h = Console.WindowHeight; //default: 30
            int w = Console.WindowWidth; //default: 120
            int plotSize = h * w - 1; //default: 3599
            int score = 0;
            bool finishState = false; //false == player loss
            while (true)
            {
                for (int i = 0; i < plotSize; i++)
                {
                    int temp = RNG();
                    if (i % 2 == 0)
                    {
                        if (temp % 3 == 0)
                            Console.Write("X");
                        else
                            Console.Write("_");
                    }
                    else
                    {
                        if (temp % 2 == 0)
                            Console.Write("X");
                        else
                            Console.Write("_");
                    }

                }
                ConsoleKey keyPress = Console.ReadKey(true).Key;
                if (keyPress != ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    score += 100;
                }
                else
                {
                    //win condition [TODO]
                    if (score % 3 == 0)
                        finishState = true;
                    GameOver _ = new GameOver(score, finishState);
                    break;
                }
            }
        }

        public dummy_NewGame()
        {
            NewGameController();
        }
    }
}
