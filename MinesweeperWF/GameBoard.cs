namespace MinesweeperWF
{
    //Enumerators to handle game difficulty and board size
    public enum Difficulty { Beginner, Intermediate, Expert, Custom}
    public enum BoardScale { Normal, Large, ExtraLarge};


    //Class creates panel to hold a grid of tiles.
    //Generates grid with randomly assigned tiles as bombs, remain tiles display nearby bombs
    //Create win and loss conditions
    //Win condition creates record
    //Board can be cleared and rebuilt
    //Click and hover events to interact with board.

    internal class GameBoard : Panel
    {
        //Reference to other class objects.
        private StatPanel stats;
        public ScoreBoard leaderBoard = new ScoreBoard();

        //Holds internal game difficulty
        public Difficulty SkillLevel { get; set; }

        //Default settings for variables
        public int amountBombs = 0;
        private Size boardSize = new Size(1,1);
        public int buttonSize = 23;
        private bool endGame = false;

        //Preview Tiles
        private bool tilePreview = false;
        private Tile currTile;
        private List<Tile> previewedTiles = new List<Tile>();


        public GameBoard(StatPanel stats)
        {
            this.stats = stats;
            BorderStyle = BorderStyle.Fixed3D;
            BuildBoard(stats.newDifficulty, stats.newScale);
        }


        //Create a new board based on difficulty selected
        public void BuildBoard(Difficulty difficulty, BoardScale tileSize)
        {
            //Assign new difficulty and adjust board layout
            SkillLevel = difficulty;
            switch (difficulty)
            {
                case Difficulty.Beginner:
                    amountBombs = 10;
                    boardSize = new Size(9,9);
                    break;
                case Difficulty.Intermediate:
                    amountBombs = 40;
                    boardSize = new Size(16, 16);
                    break;
                case Difficulty.Expert:
                    amountBombs = 99;
                    boardSize = new Size(30, 16);
                    break;
                case Difficulty.Custom:
                    amountBombs = stats.customBombs;
                    boardSize = stats.customSize;
                    break;
                default:
                    difficulty = Difficulty.Beginner;
                    amountBombs = 10;
                    boardSize = new Size(9, 9);
                    break;
            }


            //Change board size and position based on button size and grid size  --May separate to adjust immediately
            switch (tileSize)
            {
                case BoardScale.Normal:
                    buttonSize = 23;
                    break;
                case BoardScale.Large:
                    buttonSize = 35;
                    break;
                case BoardScale.ExtraLarge:
                    buttonSize = 46;
                    break;

            }
            Size = new Size(boardSize.Width * buttonSize + 5, boardSize.Height * buttonSize + 5);
            Location = new Point(10, 42 + stats.Height);


            //Create tiles to fill the boardSize and assign bombs to the correlating tiles
            Tile[,] grid = new Tile[boardSize.Width, boardSize.Height];
            List<int> bombList = GetBombs(boardSize, amountBombs);
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y] = new Tile(buttonSize, buttonSize, x, y, bombList.Contains((y * boardSize.Width) + x));
                }
            }


            //Creates list of nearby tiles for each tile
            //Adds Tiles to the controls panel
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y].FindNeighbors(grid);
                    grid[x, y].CheckBombs();

                    Controls.Add(grid[x, y]);

                    //Used MouseUp cause MouseClick only detects Left Clicks
                    grid[x, y].MouseUp += (sender, MouseEventArgs) => { TileReveal_Click(sender, MouseEventArgs); };
                    grid[x, y].MouseDown += (sender, MouseEventArgs) => { TilePreview_Click(sender, MouseEventArgs); };
                    grid[x, y].MouseHover += (sender, EventArgs) => { Tile_Hover(sender, EventArgs); };
                }
            }
            endGame = false;

        }


        //Prepare a list of randomly selected tiles to be assigned as bombs
        private static List<int> GetBombs(Size boardSize, int bombAmount)
        {
            Random rnd = new Random();
            List<int> bombList = new List<int>();

            while (bombList.Count < bombAmount)
            {
                int randNum = rnd.Next(0, boardSize.Height * boardSize.Width);
                if (!bombList.Contains(randNum))
                {
                    bombList.Add(randNum);
                }
            }

            return bombList;
        }


        //Reveals tile, checks if game has ended, and checks if game has been won.
        //Runs the tile's reveal meothod that will return if it's a bomb.
        private void Reveal(Tile revealedTile)
        {
            endGame = revealedTile.Reveal();
            if (endGame) { GameOver(); }
            CheckWin();
        }


        //Win game when all Unrevealed tiles are bombs
        //Bomb tiles turn blue, timer stops, and endgame is set to true
        private void CheckWin()
        {
            if (Controls.OfType<Tile>().Count(t => t.IsRevealed == false) == amountBombs)
            {
                stats.gameTimer.Stop();
                foreach (Control c in Controls)
                {
                    Tile t = c as Tile;
                    if (t != null && t.HasBomb)
                    {
                        t.BackColor = Color.Blue;
                        stats.newGame.BackColor = Color.Blue;
                        endGame = true;
                    }
                }
                //Player can save score if not playing a custom board
                //Displays leaderboard after attempting to save score
                if(SkillLevel != Difficulty.Custom)
                {
                    SaveRecord();
                    leaderBoard.ScoresPopUp(SkillLevel);
                }
            }
        }

        //Creates popup with prompt to save score
        //Requests name to associate with score
        private void SaveRecord()
        {
            int recTime = stats.time;
            string message = $"Your time is {stats.time.ToString()}";
            string title = "Congratulations!";
            leaderBoard.AddScore(Microsoft.VisualBasic.Interaction.InputBox(message, title, "Input Your Name Here", 1, 1), recTime, SkillLevel.ToString());
        }


        //When game is over prematurely, turn off timer and reveal all remaining tiles in dark salmon.
        //Remaining bombs are revealed in red.
        private void GameOver()
        {
            stats.gameTimer.Stop();
            foreach (Control c in Controls)
            {
                Tile t = c as Tile;
                if (t != null && t.IsRevealed != true)
                {
                    t.IsRevealed = true;
                    t.Text = t.bText;
                    t.BackColor = t.HasBomb ? Color.Red : Color.DarkSalmon;
                    stats.newGame.BackColor = Color.Red;
                }
            }
        }


        //Ends game and clears controls from board, then clears memory.
        public void BoardClear()
        {
            foreach (Control c in Controls)
            {
                c.Dispose();
            }
            Controls.Clear();
            GC.Collect();
        }


        //Runs when MouseUp is detected
        //Left click - reveal tile
        //Right click - toggle tile's flagged state
        //Middle click - ends or reveals surrounding tiles preview
        private void TileReveal_Click(object sender, MouseEventArgs e)
        {
            //Only allow tiles to be clicked if the game has not ended.
            if (!endGame) { 

                //Starts timer on first click
                if (!stats.gameTimer.Enabled)
                {
                    stats.gameTimer.Start();
                }


                Tile clickedButton = (Tile)sender;
                switch (e.Button)
                {
                    //On Left Click
                    //Reveals the tile
                    case MouseButtons.Left:
                        Reveal(clickedButton); 
                        break;


                    //On Right Click
                    //Changes the tile's flagged state, updates the bomb count when tile is flagged or flag is removed
                    //Only ran if the tile hasn't been revealed.
                    case MouseButtons.Right:
                        if (clickedButton.IsRevealed == false)
                        {
                            IAsyncResult async = clickedButton.BeginInvoke(clickedButton.ToggleState);
                            clickedButton.EndInvoke(async);

                            //Changes bomb count if tile is flagged or flag is removed.
                            if (clickedButton.CurrentState == TileState.Flagged)
                            {
                                stats.BombCounterUpdate(-1);
                            }
                            else if (clickedButton.CurrentState == TileState.Possible)
                            {
                                stats.BombCounterUpdate(1);
                            }
                        }
                        break;


                    //Ends preview and tries to reveal previewed tiles.
                    case MouseButtons.Middle:
                        tilePreview = false;
                        //Checks if amount of bombs near tile is equal to amount of flagged tiles near tile.
                        if (previewedTiles.Count(flagged => flagged.CurrentState == TileState.Flagged) == previewedTiles.Count(bomb => bomb.HasBomb == true) && clickedButton.IsRevealed == true && !previewedTiles.Any(t => t.CurrentState == TileState.Possible))
                        {
                            foreach (Tile t in previewedTiles.Where(tile => tile.Visible == true))
                            {
                                if (!endGame) { Reveal(t); }
                            }
                        }
                        //Reset look of tiles
                        foreach (Tile t in previewedTiles)
                        {
                            t.FlatStyle = FlatStyle.Flat;
                        }
                        break;
                }
            }
        }

        //Runs when MouseDown is detected.
        //Left click - nothing
        //Right click - nothing
        //Middle click - previews nearby tiles
        private void TilePreview_Click(object sender, MouseEventArgs e)
        {
            //Only run when the game hasn't ended
            if (!endGame)
            {
                Tile clickedButton = (Tile)sender;
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        break;

                    case MouseButtons.Right:
                        break;

                    //Begins Preview
                    //Clears previewedTiles list and adds current tile and all nearby tiles to list.
                    case MouseButtons.Middle:
                        previewedTiles.Clear();
                        previewedTiles.Add(clickedButton);
                        foreach (Tile t in clickedButton.Neighbors)
                        {
                            tilePreview = true;
                            previewedTiles.Add(t);
                        }
                        //Change look of tiles being previewed.
                        foreach (Tile t in previewedTiles)
                        {
                            t.FlatStyle = FlatStyle.System;
                        }
                        break;
                }
            }
        }


        //Update which tiles are previewed when moving to hovering over a new tile.
        private void Tile_Hover(object sender, EventArgs e)
        {
            Tile hoveredTile = (Tile)sender;
            //Check if still in preview and if the hovered tile has updated.
            //If true, clear previewedTiles and add hoveredTile and neighbors to previewedTiles.
            if (tilePreview && hoveredTile != currTile)
            {
                foreach(Tile t in previewedTiles)
                {
                    t.FlatStyle = FlatStyle.Flat;
                }

                previewedTiles.Clear();
                previewedTiles.Add(hoveredTile);

                foreach(Tile t in hoveredTile.Neighbors)
                {
                    previewedTiles.Add(t);
                }

                foreach(Tile t in previewedTiles)
                {
                    t.FlatStyle = FlatStyle.System;
                }
            }
            currTile = hoveredTile;
        }


    }
}
