namespace Enlashceoc
{
    internal class NewGame
    {
        static int RandomGenerator()
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(int.MaxValue);
            //min time to generate && print 1000 numbers: 46 ms
            //max time to generate && print 1000 numbers: 394 ms
            //data based on 10 test runs
        }

        static void NewGameController()
        {
            Console.Clear();
            int h = Console.WindowHeight; //default: 30
            int w = Console.WindowWidth; //default: 120
            int plotSize = h * w;
            int score = 0;
            bool finishState = false; //false == player loss
            while (true)
            {
                for (int i = 0; i < plotSize; i++)
                {
                    int temp = RandomGenerator();
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
                    if (score % 10000 == 0)
                        finishState = true;
                    GameOver gameOver = new GameOver(score, finishState);
                    break;
                }
            }
        }

        public NewGame()
        {
            NewGameController();
        }
    }
}
