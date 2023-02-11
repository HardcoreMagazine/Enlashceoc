using System.Text.Json;

namespace Enlashceoc
{
    internal class Scoreboard
    {
        private static string EncodeString(string str)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        private static string DecodeString(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }

        static private void GenerateScoreboardUI()
        {
            List<string> scores = new List<string>();
            //access save data from local drive
            string path = AppDomain.CurrentDomain.BaseDirectory + "gamedata";
            if (File.Exists(path)) //check if file exists
            {
                string encodedJsonString = File.ReadAllText(path); //read json from file
                if (!String.IsNullOrWhiteSpace(encodedJsonString))
                {
                    string decodedStr = DecodeString(encodedJsonString);
                    SaveData[] scoresData = JsonSerializer.Deserialize<SaveData[]>(decodedStr)!;
                    //deserialize json and write values into SaveData array
                    for (int i = 0; i < 5; i++) //fixed scoreboard size: 5 elements
                    {
                        //convert unix timestamp into formatted string
                        TimeSpan t = TimeSpan.FromMilliseconds(scoresData[i].Playtime);
                        string timeStr = @$"{t.Hours:D2}:{t.Minutes:D2}:{t.Seconds:D2}:{t.Milliseconds:D3}";
                        //save recieved values in memory
                        scores.Add($"{i + 1}. {scoresData[i].Name} - {timeStr}");
                    }
                }
            }
            else
            {
                //create "empty" scoreboard with 5 positions
                //and write it into file
                List<SaveData> scoresData = new List<SaveData>();
                for (int i = 0; i < 5; i++)
                {
                    scoresData.Add(new SaveData { Name = "none", Playtime = 0 });
                    scores.Add($"{i + 1}. {scoresData[i].Name} - 00:00:00:000");
                }
                string jsonString = JsonSerializer.Serialize(scoresData);
                File.WriteAllText(path, EncodeString(jsonString));
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
            Console.WriteLine(title);
            for (int i = 0; i < scores.Count; i++) 
                Console.WriteLine("\t\t\t\t\t\t" + scores[i] + "\n");
            Console.Write($"\n\n" +
                          $"\t\t\t\t\t\t\t" +
                          $"> RETURN" +
                          $"\n\n\n" +
                          $"{borderLine.Remove(119)}");
            // See explanation in 'Menu.cs'
        }

        static void ScoreboardController()
        {
            Console.Clear(); // Remove traces of previous UI
            while (true)
            {
                GenerateScoreboardUI();
                var consoleKey = Console.ReadKey(true).Key;
                if (consoleKey == ConsoleKey.Spacebar)
                {
                    _ = new Menu();
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
