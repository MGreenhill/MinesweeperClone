using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWF
{
    internal class Tile : Button
    {
        public Label tileLabel;
        public Point Position { get; }
        public int NearbyBombs { get; set; }
        public bool HasBomb { get; set; }
        public bool IsRevealed { get; set; }
        public String Text { get { return tileLabel.Text; } set { tileLabel.Text = value; } }
        
        public Tile(int width, int height, Point position, bool isBomb)
        {
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
    
        public void Reveal()//Hides the button of the tile and sets a boolean that can be referenced in the future.  Bool may not be needed
        {
            Visible = false;
            IsRevealed = true;
        }

    }
    
}
