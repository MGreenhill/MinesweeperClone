namespace MinesweeperWF
{
    partial class ScoreBoardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rBBeginner = new System.Windows.Forms.RadioButton();
            this.rBIntermediate = new System.Windows.Forms.RadioButton();
            this.rBExpert = new System.Windows.Forms.RadioButton();
            this.lBScoreDisplay = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // rBBeginner
            // 
            this.rBBeginner.AutoSize = true;
            this.rBBeginner.Location = new System.Drawing.Point(15, 17);
            this.rBBeginner.Name = "rBBeginner";
            this.rBBeginner.Size = new System.Drawing.Size(72, 19);
            this.rBBeginner.TabIndex = 0;
            this.rBBeginner.TabStop = true;
            this.rBBeginner.Text = "Beginner";
            this.rBBeginner.UseVisualStyleBackColor = true;
            this.rBBeginner.CheckedChanged += new System.EventHandler(this.rBBeginner_CheckedChanged);
            // 
            // rBIntermediate
            // 
            this.rBIntermediate.AutoSize = true;
            this.rBIntermediate.Location = new System.Drawing.Point(100, 17);
            this.rBIntermediate.Name = "rBIntermediate";
            this.rBIntermediate.Size = new System.Drawing.Size(92, 19);
            this.rBIntermediate.TabIndex = 1;
            this.rBIntermediate.TabStop = true;
            this.rBIntermediate.Text = "Intermediate";
            this.rBIntermediate.UseVisualStyleBackColor = true;
            this.rBIntermediate.CheckedChanged += new System.EventHandler(this.rBIntermediate_CheckedChanged);
            // 
            // rBExpert
            // 
            this.rBExpert.AutoSize = true;
            this.rBExpert.Location = new System.Drawing.Point(205, 17);
            this.rBExpert.Name = "rBExpert";
            this.rBExpert.Size = new System.Drawing.Size(58, 19);
            this.rBExpert.TabIndex = 2;
            this.rBExpert.TabStop = true;
            this.rBExpert.Text = "Expert";
            this.rBExpert.UseVisualStyleBackColor = true;
            this.rBExpert.CheckedChanged += new System.EventHandler(this.rBExpert_CheckedChanged);
            // 
            // lBScoreDisplay
            // 
            this.lBScoreDisplay.FormattingEnabled = true;
            this.lBScoreDisplay.ItemHeight = 15;
            this.lBScoreDisplay.Location = new System.Drawing.Point(15, 60);
            this.lBScoreDisplay.Name = "lBScoreDisplay";
            this.lBScoreDisplay.Size = new System.Drawing.Size(250, 364);
            this.lBScoreDisplay.TabIndex = 3;
            this.lBScoreDisplay.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.ScoreBoardLBFormat);
            // 
            // ScoreBoardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 450);
            this.Controls.Add(this.lBScoreDisplay);
            this.Controls.Add(this.rBExpert);
            this.Controls.Add(this.rBIntermediate);
            this.Controls.Add(this.rBBeginner);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "ScoreBoardForm";
            this.Text = "LeaderBoard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton rBBeginner;
        private RadioButton rBIntermediate;
        private RadioButton rBExpert;
        private ListBox lBScoreDisplay;
    }
}