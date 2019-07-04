namespace Ex05_OtheloUI
{
    partial class GameSettings
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
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonAgainstComputer = new System.Windows.Forms.Button();
            this.buttonAgainstFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBoardSize.Location = new System.Drawing.Point(12, 80);
            this.buttonBoardSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(776, 94);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size: 6X6 (click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonAgainstComputer
            // 
            this.buttonAgainstComputer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgainstComputer.Location = new System.Drawing.Point(12, 226);
            this.buttonAgainstComputer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAgainstComputer.Name = "buttonAgainstComputer";
            this.buttonAgainstComputer.Size = new System.Drawing.Size(365, 80);
            this.buttonAgainstComputer.TabIndex = 0;
            this.buttonAgainstComputer.Text = "Play against the computer";
            this.buttonAgainstComputer.UseVisualStyleBackColor = true;
            this.buttonAgainstComputer.Click += new System.EventHandler(this.buttonAgainstComputer_Click);
            // 
            // buttonAgainstFriend
            // 
            this.buttonAgainstFriend.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAgainstFriend.Location = new System.Drawing.Point(421, 226);
            this.buttonAgainstFriend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonAgainstFriend.Name = "buttonAgainstFriend";
            this.buttonAgainstFriend.Size = new System.Drawing.Size(365, 80);
            this.buttonAgainstFriend.TabIndex = 0;
            this.buttonAgainstFriend.Text = "Play against your friend";
            this.buttonAgainstFriend.UseVisualStyleBackColor = true;
            this.buttonAgainstFriend.Click += new System.EventHandler(this.buttonAgainstFriend_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 325);
            this.Controls.Add(this.buttonBoardSize);
            this.Controls.Add(this.buttonAgainstFriend);
            this.Controls.Add(this.buttonAgainstComputer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonAgainstComputer;
        private System.Windows.Forms.Button buttonAgainstFriend;
    }
}

