namespace MinesweeperWF
{
    public enum TileState { Unflagged, Flagged, Possible }//3 possible bomb states the player can mark the tile as.

    internal class Tile : Button
    {
        public TileState CurrentState;
        public Label tileLabel;
        public Point Position { get; }
        public int NearbyBombs { get; set; }
        public bool HasBomb { get; set; }
        public bool IsRevealed { get; set; }
        public String bText { get; set; }
        public int xLocal;
        public int yLocal;
        public List<Tile> Neighbors { get; set; }
        
        
        public Tile(int width, int height, int x, int y, bool isBomb)
        {
            //New instances
            Neighbors = new List<Tile> { };

            //Tile positioning and size
            xLocal = x;
            yLocal = y;
            Width = width - 1;
            Height = height - 1;
            Position = new Point(xLocal * width, yLocal * height);
            Location = Position;
            //Display setup
            FlatStyle = FlatStyle.Flat;
            BackColor = Color.White;

            //Bomb Setup
            CurrentState = TileState.Unflagged;
            HasBomb = false;
            HasBomb = isBomb;
            NearbyBombs = 0;
            NearbyBombs = 0;
        }
    
        //Changes the Tile's state when called
        public void ToggleState()
        {
            switch (CurrentState)
            {
                case TileState.Unflagged://If the Tile has no flag, mark it as Flagged
                    CurrentState = TileState.Flagged;
                    BackColor = Color.Red;
                    break;
                case TileState.Flagged://If the Tile is marked as Flagged, change mark to Possible flag
                    CurrentState = TileState.Possible;
                    BackColor = Color.DeepPink;
                    break;
                case TileState.Possible://If the Tile is marked as Possible, remove mark
                    CurrentState = TileState.Unflagged;
                    BackColor = Color.White;
                    break;
            }
        }

        //Adds any tiles nearby in 8 directions of the grid
        public void FindNeighbors(Tile[,] tGrid)
        {
            List<Tuple<int, int>> possibleNeighbors = new List<Tuple<int, int>>    //List of all 8 hypothetical surrounding tiles
            {
                Tuple.Create(xLocal + 1, yLocal),       //Right
                Tuple.Create(xLocal + 1, yLocal + 1),   //LowerRight
                Tuple.Create(xLocal, yLocal + 1),       //Down
                Tuple.Create(xLocal - 1, yLocal + 1),   //LowerLeft
                Tuple.Create(xLocal - 1, yLocal),       //Left
                Tuple.Create(xLocal - 1, yLocal - 1),   //UpperLeft
                Tuple.Create(xLocal, yLocal - 1),       //Up
                Tuple.Create(xLocal + 1, yLocal - 1)    //UpperRight

            };

            //Check hypothetical tiles if they're within the grid and add the corrects ones to the returned list
            foreach (Tuple<int, int> pos in possibleNeighbors)
            {
                if (pos.Item1 >= 0 && pos.Item1 < tGrid.GetLength(0) && pos.Item2 >= 0 && pos.Item2 < tGrid.GetLength(1))
                {
                    Neighbors.Add(tGrid[pos.Item1, pos.Item2]);
                }
            }
        }

        //Determines how many neighbor tiles have bombs and changes text to display the amount or if this tile is a bomb
        public void CheckBombs()
        {
            foreach (Tile tile in Neighbors)
            {
                if (tile.HasBomb) {
                    NearbyBombs++;
                }
            }
            bText = HasBomb ? "B" : NearbyBombs.ToString();
        }

        //Changes Tile's flag state.
        public bool Reveal()
        {
            if (HasBomb && CurrentState == TileState.Unflagged && !IsRevealed)
            {
                return true;
            }
            //Allows you to click tile if it hasn't already been tagged
            else if (CurrentState == TileState.Unflagged && !IsRevealed)
            {
                IsRevealed = true;
                FlatStyle = FlatStyle.Flat;
                BackColor = Color.LightGray;
                Text = bText;
                if(NearbyBombs == 0)
                {
                    foreach(Tile tile in Neighbors)
                    {
                        tile.Reveal();
                    }
                }
            }

            return false;
        }

        
    }
    
}
