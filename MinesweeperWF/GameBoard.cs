namespace MinesweeperWF
{
    internal class Board : Panel
    {
        //Game Difficulty
        public int amountBombs;
        public Size boardSize;
        public int buttonSize;

        //Preview Tiles
        bool tilePreview = false;
        Tile currTile;
        List<Tile> previewedTiles = new List<Tile> ();


        public Board(int sizeButton, Size sizeBoard,int bombs)
        {
            amountBombs = bombs;
            boardSize = sizeBoard;
            buttonSize = sizeButton;

            BorderStyle = BorderStyle.Fixed3D;
            BuildBoard(sizeBoard, sizeButton, bombs);
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


        private void GameOver()
        {
            //if tile is a bomb and not flagged, reveal all tiles/game over
            foreach (Control c in Controls)
            {
                Tile t = c as Tile;
                if (t != null)
                {
                    t.Visible = false;
                }
            }
        }

        //Runs when MouseUp is detected
        private void TileReveal_Click(object sender, MouseEventArgs e)
        {
            Tile clickedButton = (Tile)sender;
            switch (e.Button)//Checks which button is pressed
            {
                //Reveals the tile and runs gameover if tile is a bomb
                case MouseButtons.Left:
                    bool endGame = clickedButton.Reveal();
                    if (endGame) { GameOver();}
                    break;

                //Changes the tile's flagged state
                case MouseButtons.Right:
                    IAsyncResult async = clickedButton.BeginInvoke(clickedButton.ToggleState);
                    clickedButton.EndInvoke(async);
                    break;
                //Ends preview
                case MouseButtons.Middle:
                    tilePreview = false;
                    foreach (Tile t in previewedTiles)
                    {
                        t.FlatStyle = FlatStyle.Flat;
                    }
                    break;
            }

        }

        //Runs when MouseDown is detected.
        private void TilePreview_Click(object sender, MouseEventArgs e)
        {
            Tile clickedButton = (Tile)sender;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;

                case MouseButtons.Right:
                    break;

                //Begins Preview
                case MouseButtons.Middle:
                    previewedTiles.Clear();
                    previewedTiles.Add(clickedButton);
                    foreach (Tile t in clickedButton.Neighbors)
                    {
                        tilePreview = true;
                        previewedTiles.Add(t);
                    }
                    foreach (Tile t in previewedTiles)
                    {
                        t.FlatStyle = FlatStyle.System;
                    }
                    break;
            }
            
        }

        //Update which tiles are previewed when moving to hovering over a new tile.
        private void Tile_Hover(object sender, EventArgs e)
        {
            Tile hoveredTile = (Tile)sender;
            //Check if still in preview, then clear previewed tiles and then preview new tiles
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

        //Clear board and rebuild
        public void Restart()
        {
            Controls.Clear();
            BuildBoard(boardSize, buttonSize, amountBombs);
        }
    }
}
