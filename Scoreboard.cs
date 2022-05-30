namespace Enlashceoc
{
    internal class Scoreboard
    {
        static private void GenerateScoreboardUI()
        {
            List<string> scores = new List<string>();
            //read from file
            if (false) //if file exists
            {
                //write data in 'scores'
            }
            else
            {
                //create empty scoreboard
                for (int i = 0; i < 5; i++)
                {
                    scores.Add(i + 1 + ". ");
                }
            }
            //note that file first created on game finish
            string borderLine =
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
            string sbTitle =
                borderLine +
                "\n" +
                "\t\t\t  _____  _____ ____  _____  ______ ____   ____          _____  _____  \n" +
                "\t\t\t / ____|/ ____/ __ \\|  __ \\|  ____|  _ \\ / __ \\   /\\   |  __ \\|  __ \\ \n" +
                "\t\t\t| (___ | |   | |  | | |__) | |__  | |_) | |  | | /  \\  | |__) | |  | |\n" +
                "\t\t\t \\___ \\| |   | |  | |  _  /|  __| |  _ <| |  | |/ /\\ \\ |  _  /| |  | |\n" +
                "\t\t\t ____) | |___| |__| | | \\ \\| |____| |_) | |__| / ____ \\| | \\ \\| |__| |\n" +
                "\t\t\t|_____/ \\_____\\____/|_|  \\_\\______|____/ \\____/_/    \\_\\_|  \\_\\_____/ \n\n" +
                borderLine +
                "\n\n";
            //-----------------------------------------------//
            Console.WriteLine(sbTitle);
            for (int i = 0; i < scores.Count; i++) 
                Console.WriteLine("\t\t\t\t\t" + scores[i] + "\n");
            Console.WriteLine("\n\n" +
                              "\t\t\t\t\t\t\t" +
                              "> RETURN" +
                              "\n\n\n");
            Console.Write(borderLine.Remove(119));
            //see explanation in 'Menu.cs'
        }

        static void ScoreboardController()
        {
            Console.Clear(); //remove traces of previous UI
            while (true)
            {
                GenerateScoreboardUI();
                var consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Enter)
                {
                    Menu menu = new Menu();
                    break;
                }
                else
                    Console.Clear();
            }
        }

        public Scoreboard()
        {
            ScoreboardController();
        }
    }
}
