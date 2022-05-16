using System.Drawing;
using System.Windows.Forms;
namespace MinesweeperWF
{
    public partial class Form1 : Form
    {


        public struct Tile
        {
            public Label tileLabel;
            public Button tileButton;

            public int Width { get; }
            public int Height { get; }
            public Point Position { get; } 
            public int NearbyBombs { get; set; }
            public bool HasBomb { get; set; }
            public String Text { get { return tileLabel.Text; } set { tileLabel.Text = value; } }

            public Tile(int width, int height, Point position, int amtBombsNear, bool isBomb)
            {
                Width = width;
                Height = height;
                Position = position;
                HasBomb = false;
                HasBomb = isBomb;
                NearbyBombs = 0;
                NearbyBombs = amtBombsNear;

                tileButton = new Button();
                tileButton.Location = Position;
                tileButton.Width = Width-1;
                tileButton.Height = Height-1;
                tileButton.FlatStyle = FlatStyle.Flat;
                

                tileLabel = new Label();
                tileLabel.Location = Position;
                tileLabel.Width = Width-3;
                tileLabel.Height = Height-3;
                tileLabel.BackColor = Color.LightGray;
                tileLabel.Margin = new Padding(3);

            }

        }


        public Form1()
        {
            InitializeComponent();

            //Values relating to difficulty size
            int amountBombs = 20;
            Size boardSize = new Size(10, 10);
            int buttonSize = 25;

            Panel gameBoard = new Panel();
            gameBoard.Location = new Point(100, 75);
            gameBoard.Size = new Size(boardSize.Width*buttonSize+5, boardSize.Height*buttonSize+5);
            gameBoard.BorderStyle = BorderStyle.Fixed3D;
            this.Controls.Add(gameBoard);

            //Objects and containers
            Random rnd = new Random();
            Tile[,] grid = new Tile[boardSize.Width, boardSize.Height];
            List<int> bombList = new List<int>();

            //Prepare a list to randomly assisgn bombs to tiles of a predefined amount
            while (bombList.Count < amountBombs)
            {
                int randNum = rnd.Next(0, boardSize.Height * boardSize.Width);
                if (!bombList.Contains(randNum)) {
                    bombList.Add(randNum);
                    grid[randNum % boardSize.Width, randNum / boardSize.Width].HasBomb = true;
                    }
            }

            //Create tiles to fill the boardSize and assign bombs to the correlating tiles
            for(int y = 0; y < boardSize.Height; y++)
            {
                for(int x = 0; x < boardSize.Width; x++)
                {
                    grid[x,y] = new Tile(buttonSize, buttonSize, new Point(x*buttonSize, y*buttonSize), grid[x, y].NearbyBombs, grid[x,y].HasBomb);
                    
                    //If current tile is a bomb, increase the nearbybombs value of surrounding tiles
                    if (grid[x, y].HasBomb)
                    {
                        foreach (Tuple<int, int> neighbor in SurroundingTiles(x, y, boardSize))
                        {
                            grid[neighbor.Item1, neighbor.Item2].NearbyBombs++;
                        }
                    }


                }
            }

            //Adds the buttons and labels for each tile in the grid to the control board
            //Must do this after because label text doesn't update after implemented.
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y].Text = grid[x, y].HasBomb ? "B" : grid[x, y].NearbyBombs.ToString();

                    gameBoard.Controls.Add(grid[x,y].tileButton);
                    gameBoard.Controls.Add(grid[x,y].tileLabel);

                    grid[x, y].tileButton.Click += new EventHandler(Tile_Click);
                }
            }
            //gameBoard.Refresh();
        }

        //Returns a list of the positions for available surrounding tiles
        //Pass in the current tile's coordinates and the grid's size
        public List<Tuple<int,int>> SurroundingTiles(int tX, int tY, Size board)
        {
            List<Tuple<int,int>> neighbors = new List<Tuple<int,int>>();
            List<Tuple<int, int>> uncheckedPositions = new List<Tuple<int, int>>    //List of all 8 hypothetical surrounding tiles
            {
                Tuple.Create(tX + 1, tY),       //Right
                Tuple.Create(tX + 1, tY + 1),   //LowerRight
                Tuple.Create(tX, tY + 1),       //Down
                Tuple.Create(tX - 1, tY + 1),   //LowerLeft
                Tuple.Create(tX - 1, tY),       //Left
                Tuple.Create(tX - 1, tY - 1),   //UpperLeft
                Tuple.Create(tX, tY - 1),       //Up
                Tuple.Create(tX + 1, tY - 1)    //UpperRight
            };

            //Check hypothetical tiles if they're within the grid and add the corrects ones to the returned list
            foreach (Tuple<int, int> pos in uncheckedPositions)
            {
                if(pos.Item1 >= 0 && pos.Item1 < board.Width && pos.Item2 >= 0 && pos.Item2 < board.Height)
                {
                    neighbors.Add(pos);
                }
            }

            return neighbors;
        }


        private void Tile_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            clickedButton.Dispose();
        }
        private void tileButton_Paint(object sender, PaintEventArgs e)
        {
            Tile paintTile = (Tile)sender;
            ControlPaint.DrawBorder(e.Graphics, paintTile.tileButton.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }

    }
}