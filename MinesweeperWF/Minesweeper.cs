namespace MinesweeperWF
{
    //Form that contains all game elements
    //Initial starting point of the program
    public partial class Minesweeper : Form
    {
        GameBoard gameBoard;
        StatPanel stats;

        public Minesweeper()
        {
            //StatPanel must be created first so GameBoard can reference custom elements in it's constructor
            //GameBoard will later be passed into StatPanel to finish it's setup
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            stats = new StatPanel();

            //Gameboard setup
            gameBoard = new GameBoard(stats);
            gameBoard.Location = new Point(10, 50 + menuBar.Height);
            this.Controls.Add(gameBoard);
            gameBoard.leaderBoard.CheckForFile();

            //StatPanel setup
            stats.StartUp(gameBoard);
            stats.Location = new Point(10, 10 + menuBar.Height);
            stats.BackColor = Color.LightGray;
            stats.GameReset += new EventHandler(Game_Reset);  //Detects if new game has occurred in StatPanel
            this.Controls.Add(stats);
            FormResize();
        }


        //Resizes the form
        public void FormResize()
        {
            this.Size = new Size(gameBoard.Width + 35, gameBoard.Height + stats.Height + 65 + menuBar.Height);
        }

        //When new game has occured, resize the form to fit the new board.
        private void Game_Reset(object sender, EventArgs e)
        {
            FormResize();
        }

        //Displays a form with saved scores when Leaderboard button is clicked.
        private void menuScoresButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<ScoreBoardForm>().Count() == 1)
            {
                Application.OpenForms.OfType<ScoreBoardForm>().First().Close();
            }
            gameBoard.leaderBoard.ScoresPopUp(gameBoard.SkillLevel);
        }


        //Change Difficulty to Beginner
        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newDifficulty = Difficulty.Beginner;
        }

        //Change Difficulty to Intermediate
        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newDifficulty = Difficulty.Intermediate;
        }

        //Change Difficulty to Expert
        private void expertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newDifficulty = Difficulty.Expert;
        }

        //Change Difficulty to Custom
        //Passes the user's desired board width, height and amount of bombs to StatPanel for new GameBoard construction.
        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uint bombs;
            uint cWidth;
            uint cHeight;

            //Parses user input for a viable board
            if (UInt32.TryParse(bombsToolStripTextBox.Text, out bombs) &&
                UInt32.TryParse(boardWidthToolStripTextBox.Text, out cWidth) &&
                UInt32.TryParse(boardHeightToolStripTextBox.Text, out cHeight) &&
                bombs != 0 && cWidth > 4 && cHeight > 4 && cWidth < 41 && cHeight < 21 &&
                bombs < cWidth * cHeight)
            {
                stats.customBombs = (int)bombs;
                stats.customSize = new Size((int)cWidth, (int)cHeight);
                stats.newDifficulty = Difficulty.Custom;
            }
            else
            {
                MessageBox.Show("Please input:\n" +
                    "Width between 5 and 40.\n" +
                    "Height between 5 and 20.\n" +
                    "Bomb Amount below the total amount of tiles in the board and more than 0.\n" +
                    "Please use positive numberals.");
            }
        }


        //Changes scale of the board upon construction
        private void normalScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newScale = BoardScale.Normal;
            gameBoard.UpdateSize(stats.newScale);
            stats.SPResize();
            FormResize();
            Refresh();
        }

        private void mediumScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newScale = BoardScale.Large;
            gameBoard.UpdateSize(stats.newScale);
            stats.SPResize();
            FormResize();
            Refresh();
        }

        private void largeScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stats.newScale = BoardScale.ExtraLarge;
            gameBoard.UpdateSize(stats.newScale);
            stats.SPResize();
            FormResize();
            Refresh();
        }
    }
}