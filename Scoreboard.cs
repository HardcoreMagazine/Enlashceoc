using System.Text.Json;

#pragma warning disable CS8600 //converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 //dereference of a possibly null reference.
namespace Enlashceoc
{
    internal class Scoreboard
    {
        static private void GenerateScoreboardUI()
        {
            List<string> scores = new List<string>();
            //access save data from local drive
            string path = AppDomain.CurrentDomain.BaseDirectory + "gamedata";
            if (File.Exists(path)) //check if file exists
            {
                string jsonString = File.ReadAllText(path); //read json from file
                SaveData[] scoresData = JsonSerializer.Deserialize<SaveData[]>(jsonString);
                //deserialize json and write values into SaveData array
                for (int i = 0; i < scoresData.Length; i++)
                {
                    //save recieved values in memory
                    scores.Add(i + 1 + ". " + scoresData[i].Name + " - " + scoresData[i].Score);
                }
            }
            else
            {
                //create "empty" scoreboard
                //and write it into file
                List<SaveData> scoresData = new List<SaveData>();
                for (int i = 0; i < 5; i++)
                {
                    scoresData.Add(new SaveData { Name = "empty", Score = 0 });
                    scores.Add(i + 1 + ". " + scoresData[i].Name + " - " + scoresData[i].Score);
                }
                string jsonString = JsonSerializer.Serialize(scoresData);
                using (StreamWriter wf = File.CreateText(path))
                {
                    wf.WriteLine(jsonString);
                }
            }
            string borderLine =
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" +
                "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
            string title =
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
            Console.WriteLine(title);
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
                if (consoleKey == ConsoleKey.Spacebar)
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
