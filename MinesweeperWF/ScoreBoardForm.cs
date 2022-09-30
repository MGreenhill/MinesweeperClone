using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperWF
{
    //Creates a form to display saved records
    //Displays time and name of records filtered by difficulty
    public partial class ScoreBoardForm : Form
    {
        private List<Score> Scores { get; set; }
        
        //When created, assigns scores to a local variable and determines current selected difficulty
        public ScoreBoardForm(Difficulty startDifficulty, List<Score> scores)
        {
            Scores = scores;
            InitializeComponent();
            switch (startDifficulty)
            {
                case Difficulty.Beginner:
                    rBBeginner.Checked = true;
                    break;
                case Difficulty.Intermediate:
                    rBIntermediate.Checked = true;
                    break;
                case Difficulty.Expert:
                    rBExpert.Checked = true;
                    break;
                default:
                    rBBeginner.Checked = true;
                    break;
            }
            UpdateList();
        }


        //Updates the displayed list by filtering the difficulty as checked on radio buttons
        public void UpdateList()
        {
            if (rBBeginner.Checked)
            {
                lBScoreDisplay.DataSource = Scores.Where(x => x.Difficulty == Difficulty.Beginner.ToString()).OrderBy(x => x.Time).ToList(); ;
                lBScoreDisplay.DisplayMember = "Time";
                lBScoreDisplay.Refresh();
            }
            else if (rBIntermediate.Checked)
            {
                lBScoreDisplay.DataSource = Scores.Where(x => x.Difficulty == Difficulty.Intermediate.ToString()).OrderBy(x => x.Time).ToList();
                lBScoreDisplay.DisplayMember = "Time";
                lBScoreDisplay.Refresh();
            }
            else if (rBExpert.Checked)
            {
                lBScoreDisplay.DataSource = Scores.Where(x => x.Difficulty == Difficulty.Expert.ToString()).OrderBy(x => x.Time).ToList();
                lBScoreDisplay.DisplayMember = "Time";
                lBScoreDisplay.Refresh();
            }
        }

        //Update list when radio button is clicked
        private void rBBeginner_CheckedChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void rBIntermediate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void rBExpert_CheckedChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        //Format the board to display only the time and name of the records
        private void ScoreBoardLBFormat(object sender, ListControlConvertEventArgs e)
        {
            string name = ((Score)e.ListItem).Player.ToString();
            string score = ((Score)e.ListItem).Time.ToString();
            e.Value = score + "          " + name;
        }
    }
}
