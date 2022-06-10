using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public String Text { get { return tileLabel.Text; } set { tileLabel.Text = value; } }
        
        
        public Tile(int width, int height, Point position, bool isBomb)
        {
            BackColor = Color.White;
            CurrentState = TileState.Unflagged;
            Width = width;
            Height = height;
            Position = position;
            HasBomb = false;
            HasBomb = isBomb;
            NearbyBombs = 0;
            NearbyBombs = 0;
        
            
            Location = Position;
            Width = Width - 1;
            Height = Height - 1;
            FlatStyle = FlatStyle.Flat;

            tileLabel = new Label();
            tileLabel.Location = Position;
            tileLabel.Width = Width - 3;
            tileLabel.Height = Height - 3;
            tileLabel.BackColor = Color.LightGray;
            tileLabel.Margin = new Padding(3);
        
        }
    
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
                    BackColor = Color.Yellow;
                    break;
                case TileState.Possible://If the Tile is marked as Possible, remove mark
                    CurrentState = TileState.Unflagged;
                    BackColor = Color.White;
                    break;
            }
        }

    }
    
}
