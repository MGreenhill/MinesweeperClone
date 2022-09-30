namespace MinesweeperWF
{
    partial class Minesweeper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Minesweeper));
            this.menuBar = new System.Windows.Forms.ToolStrip();
            this.menuConfigDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.changeDifficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intermediateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bombToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bombsToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.boardWidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardWidthToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.boardHeightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.boardHeightToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.confirmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraLargeScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuScoresButton = new System.Windows.Forms.ToolStripButton();
            this.menuBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuBar
            // 
            this.menuBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuConfigDropDown,
            this.menuScoresButton});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.Padding = new System.Windows.Forms.Padding(0);
            this.menuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuBar.Size = new System.Drawing.Size(459, 25);
            this.menuBar.TabIndex = 0;
            this.menuBar.Text = "toolStrip1";
            // 
            // menuConfigDropDown
            // 
            this.menuConfigDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuConfigDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDifficultyToolStripMenuItem,
            this.scaleToolStripMenuItem});
            this.menuConfigDropDown.Image = ((System.Drawing.Image)(resources.GetObject("menuConfigDropDown.Image")));
            this.menuConfigDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuConfigDropDown.Name = "menuConfigDropDown";
            this.menuConfigDropDown.Size = new System.Drawing.Size(56, 22);
            this.menuConfigDropDown.Text = "Config";
            // 
            // changeDifficultyToolStripMenuItem
            // 
            this.changeDifficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginnerToolStripMenuItem,
            this.intermediateToolStripMenuItem,
            this.expertToolStripMenuItem,
            this.customToolStripMenuItem});
            this.changeDifficultyToolStripMenuItem.Name = "changeDifficultyToolStripMenuItem";
            this.changeDifficultyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.changeDifficultyToolStripMenuItem.Text = "Change Difficulty";
            // 
            // beginnerToolStripMenuItem
            // 
            this.beginnerToolStripMenuItem.Name = "beginnerToolStripMenuItem";
            this.beginnerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.beginnerToolStripMenuItem.Text = "Beginner";
            this.beginnerToolStripMenuItem.Click += new System.EventHandler(this.beginnerToolStripMenuItem_Click);
            // 
            // intermediateToolStripMenuItem
            // 
            this.intermediateToolStripMenuItem.Name = "intermediateToolStripMenuItem";
            this.intermediateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.intermediateToolStripMenuItem.Text = "Intermediate";
            this.intermediateToolStripMenuItem.Click += new System.EventHandler(this.intermediateToolStripMenuItem_Click);
            // 
            // expertToolStripMenuItem
            // 
            this.expertToolStripMenuItem.Name = "expertToolStripMenuItem";
            this.expertToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.expertToolStripMenuItem.Text = "Expert";
            this.expertToolStripMenuItem.Click += new System.EventHandler(this.expertToolStripMenuItem_Click);
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bombToolStripMenuItem,
            this.bombsToolStripTextBox,
            this.toolStripSeparator1,
            this.boardWidthToolStripMenuItem,
            this.boardWidthToolStripTextBox,
            this.toolStripSeparator2,
            this.boardHeightToolStripMenuItem,
            this.boardHeightToolStripTextBox,
            this.toolStripSeparator3,
            this.confirmToolStripMenuItem});
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.customToolStripMenuItem.Text = "Custom";
            // 
            // bombToolStripMenuItem
            // 
            this.bombToolStripMenuItem.Name = "bombToolStripMenuItem";
            this.bombToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bombToolStripMenuItem.Text = "Bombs";
            // 
            // bombsToolStripTextBox
            // 
            this.bombsToolStripTextBox.Name = "bombsToolStripTextBox";
            this.bombsToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // boardWidthToolStripMenuItem
            // 
            this.boardWidthToolStripMenuItem.Name = "boardWidthToolStripMenuItem";
            this.boardWidthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.boardWidthToolStripMenuItem.Text = "Board Width";
            // 
            // boardWidthToolStripTextBox
            // 
            this.boardWidthToolStripTextBox.Name = "boardWidthToolStripTextBox";
            this.boardWidthToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // boardHeightToolStripMenuItem
            // 
            this.boardHeightToolStripMenuItem.Name = "boardHeightToolStripMenuItem";
            this.boardHeightToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.boardHeightToolStripMenuItem.Text = "Board height";
            // 
            // boardHeightToolStripTextBox
            // 
            this.boardHeightToolStripTextBox.Name = "boardHeightToolStripTextBox";
            this.boardHeightToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // confirmToolStripMenuItem
            // 
            this.confirmToolStripMenuItem.Name = "confirmToolStripMenuItem";
            this.confirmToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.confirmToolStripMenuItem.Text = "Confirm";
            this.confirmToolStripMenuItem.Click += new System.EventHandler(this.confirmToolStripMenuItem_Click);
            // 
            // scaleToolStripMenuItem
            // 
            this.scaleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalScaleToolStripMenuItem,
            this.largeScaleToolStripMenuItem,
            this.extraLargeScaleToolStripMenuItem});
            this.scaleToolStripMenuItem.Name = "scaleToolStripMenuItem";
            this.scaleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.scaleToolStripMenuItem.Text = "Scale";
            // 
            // normalScaleToolStripMenuItem
            // 
            this.normalScaleToolStripMenuItem.Name = "normalScaleToolStripMenuItem";
            this.normalScaleToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.normalScaleToolStripMenuItem.Text = "100%";
            this.normalScaleToolStripMenuItem.Click += new System.EventHandler(this.normalScaleToolStripMenuItem_Click);
            // 
            // largeScaleToolStripMenuItem
            // 
            this.largeScaleToolStripMenuItem.Name = "largeScaleToolStripMenuItem";
            this.largeScaleToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.largeScaleToolStripMenuItem.Text = "150%";
            this.largeScaleToolStripMenuItem.Click += new System.EventHandler(this.largeScaleToolStripMenuItem_Click);
            // 
            // extraLargeScaleToolStripMenuItem
            // 
            this.extraLargeScaleToolStripMenuItem.Name = "extraLargeScaleToolStripMenuItem";
            this.extraLargeScaleToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.extraLargeScaleToolStripMenuItem.Text = "200%";
            this.extraLargeScaleToolStripMenuItem.Click += new System.EventHandler(this.extraLargeScaleToolStripMenuItem_Click);
            // 
            // menuScoresButton
            // 
            this.menuScoresButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuScoresButton.Image = ((System.Drawing.Image)(resources.GetObject("menuScoresButton.Image")));
            this.menuScoresButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuScoresButton.Name = "menuScoresButton";
            this.menuScoresButton.Size = new System.Drawing.Size(77, 22);
            this.menuScoresButton.Text = "Leaderboard";
            this.menuScoresButton.Click += new System.EventHandler(this.menuScoresButton_Click);
            // 
            // Minesweeper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 461);
            this.Controls.Add(this.menuBar);
            this.Name = "Minesweeper";
            this.Text = "Minesweeper";
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip menuBar;
        private ToolStripButton menuScoresButton;
        private ToolStripDropDownButton menuConfigDropDown;
        private ToolStripMenuItem changeDifficultyToolStripMenuItem;
        private ToolStripMenuItem beginnerToolStripMenuItem;
        private ToolStripMenuItem intermediateToolStripMenuItem;
        private ToolStripMenuItem expertToolStripMenuItem;
        private ToolStripMenuItem customToolStripMenuItem;
        private ToolStripMenuItem scaleToolStripMenuItem;
        private ToolStripMenuItem normalScaleToolStripMenuItem;
        private ToolStripMenuItem largeScaleToolStripMenuItem;
        private ToolStripMenuItem extraLargeScaleToolStripMenuItem;
        private ToolStripMenuItem bombToolStripMenuItem;
        private ToolStripTextBox bombsToolStripTextBox;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem boardWidthToolStripMenuItem;
        private ToolStripTextBox boardWidthToolStripTextBox;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem boardHeightToolStripMenuItem;
        private ToolStripTextBox boardHeightToolStripTextBox;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem confirmToolStripMenuItem;
    }
}