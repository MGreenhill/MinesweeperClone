using System.Drawing;
using System.Windows.Forms;
namespace MinesweeperWF
{
    public partial class Form1 : Form
    {

        //Values relating to difficulty size
        int amountBombs = 20;
        Size boardSize = new Size(10, 10);
        int buttonSize = 50;

        Panel gameBoard = new Panel();//gameBoard is created outside of contructor so it can be referenced easier

        public Form1()
        {
            InitializeComponent();

            //Gameboard setup
            gameBoard.Location = new Point(100, 75);
            gameBoard.Size = new Size(boardSize.Width * buttonSize + 5, boardSize.Height * buttonSize + 5);
            gameBoard.BorderStyle = BorderStyle.Fixed3D;

            //New game button setup
            Button newBoard = new Button();
            newBoard.Location = new Point(205, 25);
            newBoard.Width = buttonSize;
            newBoard.Height = buttonSize;
            newBoard.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(newBoard);
            newBoard.Click += new EventHandler(NewBoard_Click);

            //Create board and add it to the form
            BuildBoard(boardSize, buttonSize, amountBombs);
            this.Controls.Add(gameBoard);
        }




        //Create a new board based on difficulty selected
        public void BuildBoard(Size boardSize, int tileSize, int bombAmount)
        {

            Random rnd = new Random();
            List<int> bombList = new List<int>();

            //Prepare a list to randomly assisgn bombs to tiles of a predefined amount
            while (bombList.Count < bombAmount)
            {
                int randNum = rnd.Next(0, boardSize.Height * boardSize.Width);
                if (!bombList.Contains(randNum))
                {
                    bombList.Add(randNum);
                }
            }

            //Create tiles to fill the boardSize and assign bombs to the correlating tiles
            Tile[,] grid = new Tile[boardSize.Width, boardSize.Height];
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    grid[x, y] = new Tile(tileSize, tileSize, new Point(x * tileSize, y * tileSize), bombList.Contains((y * boardSize.Width) + x));
                }
            }

            //Calculates the amount of nearby bombs for each tile
            //Adds the buttons and labels for each tile in the grid to the control board
            for (int y = 0; y < boardSize.Height; y++)
            {
                for (int x = 0; x < boardSize.Width; x++)
                {
                    foreach (Tuple<int, int> neighbor in SurroundingTiles(x, y, boardSize))
                    {
                        if(grid[neighbor.Item1, neighbor.Item2].HasBomb)
                        {
                            grid[x, y].NearbyBombs++;
                        }
                    }
                    grid[x, y].Text = grid[x, y].HasBomb ? "B" : grid[x, y].NearbyBombs.ToString();

                    gameBoard.Controls.Add(grid[x, y]);
                    gameBoard.Controls.Add(grid[x, y].tileLabel);  //Must do this last because label text doesn't update after implemented.

                    //Used MouseUp cause MouseClick only detects Left Clicks
                    grid[x, y].MouseUp += (sender, MouseEventArgs) => { TileReveal_Click(sender, MouseEventArgs);  };
                }
            }
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



        private void NewBoard_Click(object sender, EventArgs e)
        {
            gameBoard.Controls.Clear();
            BuildBoard(boardSize, buttonSize, amountBombs);
        }



        private void TileReveal_Click(object sender, MouseEventArgs e)
        {
            Tile clickedButton = (Tile)sender;
            switch (e.Button)//Checks which button is pressed
            {
                case MouseButtons.Left://Reveals the tile
                    if (clickedButton.HasBomb && clickedButton.CurrentState == TileState.Unflagged)
                    {
                        foreach (Control c in gameBoard.Controls)//if tile is a bomb and not flagged, reveal all tiles/game over
                        {
                            Tile t = c as Tile;
                            if (t != null)
                            {
                                t.Visible = false;
                            }
                        }
                    }
                    else if(clickedButton.CurrentState == TileState.Unflagged)//Allows you to click tile if it hasn't already been tagged
                    {
                        clickedButton.Visible = false;
                    }
                break;

                case MouseButtons.Right://Changes the tile's flagged state
                    IAsyncResult async = clickedButton.BeginInvoke(clickedButton.ToggleState);
                    clickedButton.EndInvoke(async);
                break;
            }
            
        }



        private void tileButton_Paint(object sender, PaintEventArgs e)
        {
            Tile paintTile = (Tile)sender;
            ControlPaint.DrawBorder(e.Graphics, paintTile.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }

    }
}