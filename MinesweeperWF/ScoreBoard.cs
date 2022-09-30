using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MinesweeperWF
{
    //Record of player's score including their name, time completed, and difficulty completed on.
    public record struct Score
    {
        public string Player { get; set; }
        public int Time { get; set; }
        public string Difficulty { get; set; }
    }

    //Reads and Records player scores to a json file
    internal class ScoreBoard
    {
        private string scoresFile;
        public List<Score> scores;

        //Create list to internally hold scores and find file that holds scores
        public ScoreBoard(){
            scores = new List<Score>();
            scoresFile = Path.GetDirectoryName(Application.ExecutablePath) + @"\LeaderBoard.json";
            CheckForFile();
        }

        //Convert records in file to internal list
        //If file doesn't exist, make one in that location
        public void CheckForFile()
        {
            if (!File.Exists(scoresFile))
            {
                File.Create(scoresFile);
                return;
            }
            //pull scores from JSON file
            scores = GetScores();
        }

        //Overwrites the records in the file and then updates the internal list to match.
        public void PushScores()
        {
            string jsonString = JsonConvert.SerializeObject(scores);
            File.WriteAllText(scoresFile, jsonString);
            scores = GetScores();
        }

        //Adds a new record to the internal list and then pushes it to the file.
        public void AddScore(string name, int time, string difficulty)
        {
            scores.Add(new Score()
            {
                Player = name,
                Time = time,
                Difficulty = difficulty
            });
            PushScores();
        }

        //Converts the json text of the file returns it as a list of records.
        public List<Score> GetScores()
        {
            //Pull from JSON file
            string jsonString = File.ReadAllText(scoresFile);
            return JsonConvert.DeserializeObject<List<Score>>(jsonString);
        }

        //Displays a form with all current records
        public void ScoresPopUp(Difficulty difficulty)
        {
            Form scoresForm = new ScoreBoardForm(difficulty, GetScores());
            scoresForm.Show();
        }
    }
}
