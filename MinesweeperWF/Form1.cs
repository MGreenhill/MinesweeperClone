namespace MinesweeperWF
{
    public partial class Form1 : Form
    {

        //Values relating to difficulty size
        int amountBombs = 20;
        Size boardSize = new Size(10, 10);
        int buttonSize = 30;
        Board gameBoard;
        StatPanel stats;


        public Form1()
        {
            InitializeComponent();
            stats = new StatPanel();

            //Gameboard setup
            gameBoard = new Board(buttonSize, boardSize, amountBombs, stats);
            gameBoard.Location = new Point(100, 75);
            gameBoard.Size = new Size(boardSize.Width * buttonSize + 5, boardSize.Height * buttonSize + 5);
            this.Controls.Add(gameBoard);

            stats.Location = new Point(100, 10);
            stats.Size = new Size(boardSize.Width * buttonSize + 5, buttonSize + 10);
            stats.BackColor = Color.LightGray;
            stats.StartUp(gameBoard);
            this.Controls.Add(stats);
        }

    }
}