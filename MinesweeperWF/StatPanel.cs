using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWF
{
    internal class StatPanel : Panel
    {
        //Declare Controls
        Label bombCounter = new Label();
        Label gameTimeDisplay = new Label();
        public Button newGame = new Button();
        public System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        private int time = 0;
        int bombAmount = 0;
        private Board gameBoard;


        public StatPanel()
        {
        }

        //Delay startup so that board class can be initialized.
        public void StartUp(Board game)
        {
            BorderStyle = BorderStyle.Fixed3D;
            gameBoard = game;

            //New game button setup
            newGame.Location = new Point((Width/2) - (gameBoard.buttonSize/2), (Height - gameBoard.buttonSize)/2);
            newGame.Width = gameBoard.buttonSize;
            newGame.Height = gameBoard.buttonSize;
            newGame.FlatStyle = FlatStyle.Flat;
            newGame.BackColor = Color.White;
            newGame.Click += new EventHandler(NewGame_Click);

            //Bomb counter setup
            bombCounter.Location = new Point(5, 5 + (Height - gameBoard.buttonSize) / 2);
            bombCounter.Width = 25;
            bombCounter.Height = 25;

            //Game timer setup
            gameTimeDisplay.Width = 50;
            gameTimeDisplay.Height = 50;
            gameTimeDisplay.Location = new Point(Width - (gameTimeDisplay.Width + 5), 5 + (Height - gameBoard.buttonSize) / 2);

            //Update timer every 1 second.
            gameTimer.Tick += new EventHandler(TimeObserver);
            gameTimer.Interval = 1000;

            //Add labels and button to controls.
            Controls.Add(newGame);
            Controls.Add(gameTimeDisplay);
            Controls.Add(bombCounter);
            
            Reset();

        }


        //Updates bombCounter by ammount and refreshes counter
        public void BombCounterUpdate(int amount)
        {
            bombAmount += amount;
            bombCounter.Text = bombAmount.ToString();
            bombCounter.Refresh();
        }

        //Stops timer and sets it to 0.  Resets bombAmount.  Refreshes panel.
        public void Reset()
        {
            if (gameTimer.Enabled)
            {
                gameTimer.Stop();
            }
            //reset values
            newGame.BackColor = Color.White;
            bombAmount = gameBoard.amountBombs;
            time = 0;
            //Update Text
            bombCounter.Text = bombAmount.ToString();
            gameTimeDisplay.Text = time.ToString();
            gameTimeDisplay.Text = time.ToString();
            //Refresh whole panel
            this.Refresh();

        }

        //When new game button is clicked, reset this panel and restart the game board.
        private void NewGame_Click(object sender, EventArgs e)
        {
            gameBoard.Restart();
            Reset();
        }

        //Every 1 second, update gameTimeDisplay.
        private void TimeObserver(object sender, EventArgs e)
        {
            time++;
            gameTimeDisplay.Text=time.ToString();
            gameTimeDisplay.Refresh();
        }

    }
}
