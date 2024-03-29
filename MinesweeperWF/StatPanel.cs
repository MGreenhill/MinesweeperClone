﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWF
{
    //Class that presents and keeps track of game time and the ammount of discovered bombs.
    //Also contains button to start a new game.
    //Contains adjustable variables for gameboard.

    internal class StatPanel : Panel
    {
        //Declare Controls
        Label bombCounter = new Label();
        Label gameTimeDisplay = new Label();
        public Button newGame = new Button();
        public System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        //Internal Variables
        public int time = 0;
        private int bombAmount = 0;
        private float startFontSize;

        //Variables to adjust the gameboard
        public GameBoard gameBoard;
        public Difficulty newDifficulty = Difficulty.Beginner;
        public BoardScale newScale = BoardScale.Normal;
        public Size customSize = new Size(5, 5);
        public int customBombs = 3;

        //Used by parent form to determine when game is restarted
        public event EventHandler GameReset;

        //Partial setup for panel controls
        public StatPanel()
        {
            BorderStyle = BorderStyle.Fixed3D;

            newGame.FlatStyle = FlatStyle.Flat;
            newGame.Click += new EventHandler(NewGame_Click);

            //Update timer every 1 second.
            gameTimer.Tick += new EventHandler(TimeObserver);
            gameTimer.Interval = 1000;
        }

        //Delayed startup so that GameBoard class can be initialized.
        //Add controls to panel
        public void StartUp(GameBoard game)
        {
            gameBoard = game;
            startFontSize = bombCounter.Font.Size;
            SPResize();

            //Add labels and button to controls.
            Controls.Add(newGame);
            Controls.Add(gameTimeDisplay);
            Controls.Add(bombCounter);

            //Set panel to pregame status
            Reset();
        }

        //resizes the elements of this panel based on the gameboard size and scale.
        public void SPResize()
        {
            Size = new Size(gameBoard.Width, gameBoard.buttonSize + 10);
            float fHeight = Height;
            //New game button setup
            newGame.Width = gameBoard.buttonSize - 1;
            newGame.Height = gameBoard.buttonSize - 1;
            newGame.Location = new Point((Width / 2) - (newGame.Width / 2) -3, (Height - newGame.Height) / 2);
            
            //Bomb counter setup
            bombCounter.Location = new Point(5, (Height - gameBoard.buttonSize) / 2);
            bombCounter.Width = 70;
            bombCounter.Height = 50;
            bombCounter.Font = new Font("Arial", ((fHeight / 22f) * startFontSize));

            //Game timer setup
            gameTimeDisplay.Location = new Point(Width - (gameTimeDisplay.Width + 5),(Height - gameBoard.buttonSize) / 2);
            gameTimeDisplay.Width = 70;
            gameTimeDisplay.Height = 50;
            gameTimeDisplay.Font = new Font("Arial", ((fHeight / 22f) * startFontSize));
            gameTimeDisplay.TextAlign = ContentAlignment.TopRight;

            //GameBoard setup
            gameBoard.Location = new Point(10, 42 + Height);
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
            time = 0;
            newGame.BackColor = Color.White;
            bombAmount = gameBoard.amountBombs;

            //Update Text
            bombCounter.Text = bombAmount.ToString();
            gameTimeDisplay.Text = time.ToString();
            gameTimeDisplay.Text = time.ToString();

            //Resize panel to fit gameBoard panel
            SPResize();
        }

        //When new game button is clicked, restart the gameboard and reset this panel.
        //When finished, calls event for parent form to resize itself.
        private void NewGame_Click(object sender, EventArgs e)
        {
            if (Enabled)
            {
                this.Enabled = false;
                gameBoard.BoardClear();
                gameBoard.BuildBoard(newDifficulty, newScale);
                Reset();
                Invoke(GameReset);
                this.Enabled = true;
            }
        }

        //Every 1 second, update gameTimeDisplay.
        //Time limit of 999
        private void TimeObserver(object sender, EventArgs e)
        {
            if (time < 999)
            {
                time++;
                gameTimeDisplay.Text = time.ToString();
                gameTimeDisplay.Refresh();
            }
        }

    }
}
