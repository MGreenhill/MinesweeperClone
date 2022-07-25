namespace MinesweeperWF
{
    public partial class Form1 : Form
    {

        //Values relating to difficulty size
        int amountBombs = 20;
        Size boardSize = new Size(10, 10);
        int buttonSize = 30;
        Board gameBoard;


        public Form1()
        {
            InitializeComponent();

            //Gameboard setup
            gameBoard = new Board(buttonSize, boardSize, amountBombs);
            gameBoard.Location = new Point(100, 75);
            gameBoard.Size = new Size(boardSize.Width * buttonSize + 5, boardSize.Height * buttonSize + 5);
            this.Controls.Add(gameBoard);

            //New game button setup
            Button newBoard = new Button();
            newBoard.Location = new Point(205, 25);
            newBoard.Width = buttonSize;
            newBoard.Height = buttonSize;
            newBoard.FlatStyle = FlatStyle.Flat;
            this.Controls.Add(newBoard);
            newBoard.Click += new EventHandler(NewBoard_Click);
        }


        private void NewBoard_Click(object sender, EventArgs e)
        {
            gameBoard.Restart();
        }

    }
}