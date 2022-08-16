namespace MinesweeperWF
{
    internal class Board : Panel
    {
        //Game Difficulty
        public int amountBombs;
        public Size boardSize;
        public int buttonSize;
        bool endGame = false;
        

        //Preview Tiles
        bool tilePreview = false;
        Tile currTile;
        List<Tile> previewedTiles = new List<Tile> ();

        //Reference to panel with timer, new game, and bombcount
        StatPanel stats;


        public Board(int sizeButton, Size sizeBoard, int bombs, StatPanel stats)
        {
            this.stats = stats;
            amountBombs = bombs;
            boardSize = sizeBoard;
            buttonSize = sizeButton;

            BorderStyle = BorderStyle.Fixed3D;
            BuildBoard(boardSize, buttonSize, amountBombs);

        }



        //Create a new board based on difficulty selected
        public void BuildBoard(Size boardSize, int tileSize, int bombAmount)
        {
            //Create tiles to fill the boardSize and assign bombs to the correlating tiles
            Tile[,] grid = new Tile[boardSize.Width, boardSize.Height];
            List<int> bombList = GetBombs(boardSize, bombAmount);

            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y] = new Tile(tileSize, tileSize, x, y, bombList.Contains((y * boardSize.Width) + x));
                }
            }

            //Calculates the amount of nearby bombs for each tile
            //Adds the buttons and labels for each tile in the grid to the control board
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y].FindNeighbors(grid);
                    grid[x, y].CheckBombs();

                    Controls.Add(grid[x, y]);
                    Controls.Add(grid[x, y].tileLabel);  //Must do this last because label text doesn't update after implemented.

                    //Used MouseUp cause MouseClick only detects Left Clicks
                    grid[x, y].MouseUp += (sender, MouseEventArgs) => { TileReveal_Click(sender, MouseEventArgs); };
                    grid[x, y].MouseDown += (sender, MouseEventArgs) => { TilePreview_Click(sender, MouseEventArgs); };
                    grid[x, y].MouseHover += (sender, EventArgs) => { Tile_Hover(sender, EventArgs); };
                }
            }

        }

        //Prepare a list to randomly assisgn bombs to tiles of a predefined amount
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
        //Bomb tiles turn blue and endgame is set to true
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
            }
        }

        //When game is over prematurely, turn off timer and reveal all tiles in red.
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

        //Clear board and rebuild and reset endGame.
        public void Restart()
        {
            Controls.Clear();
            BuildBoard(boardSize, buttonSize, amountBombs);
            endGame = false;
        }



        //Runs when MouseUp is detected
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
                    //Reveals the tile and runs gameover if tile is a bomb
                    case MouseButtons.Left:
                        Reveal(clickedButton);
                        break;

                    //Changes the tile's flagged state, updates the bomb count when tile is flagged or flag is removed
                    //Only ran if game hasn't ended and the tile hasn't been revealed.
                    case MouseButtons.Right:
                        if (clickedButton.IsRevealed == false && !endGame)
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
                                Reveal(t);
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
        private void TilePreview_Click(object sender, MouseEventArgs e)
        {
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
            //Check if still in preview and if the hovered tile has updated.  If so, clear previewedTiles and add hoveredTile and neighbors to previewedTiles.
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
