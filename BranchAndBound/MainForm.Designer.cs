namespace BranchAndBound
{
    partial class MainForm
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
            this.launchButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.matrixGridView = new System.Windows.Forms.DataGridView();
            this.sizeTextBox = new System.Windows.Forms.TextBox();
            this.createSizeButton = new System.Windows.Forms.Button();
            this.setDefaultInfoButton = new System.Windows.Forms.Button();
            this.outputPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.matrixGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // launchButton
            // 
            this.launchButton.Location = new System.Drawing.Point(527, 12);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(75, 23);
            this.launchButton.TabIndex = 0;
            this.launchButton.Text = "Запустить";
            this.launchButton.Click += new System.EventHandler(this.LaunchButton_Click);
            this.launchButton.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(304, 12);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(217, 176);
            this.outputTextBox.TabIndex = 1;
            this.outputTextBox.Text = "";
            // 
            // matrixGridView
            // 
            this.matrixGridView.AllowUserToAddRows = false;
            this.matrixGridView.AllowUserToDeleteRows = false;
            this.matrixGridView.AllowUserToResizeColumns = false;
            this.matrixGridView.AllowUserToResizeRows = false;
            this.matrixGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.matrixGridView.Location = new System.Drawing.Point(12, 12);
            this.matrixGridView.Name = "matrixGridView";
            this.matrixGridView.Size = new System.Drawing.Size(180, 180);
            this.matrixGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.matrixGridView.TabIndex = 2;
            // 
            // sizeTextBox
            // 
            this.sizeTextBox.Location = new System.Drawing.Point(198, 12);
            this.sizeTextBox.Name = "sizeTextBox";
            this.sizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.sizeTextBox.TabIndex = 3;
            // 
            // createSizeButton
            // 
            this.createSizeButton.Location = new System.Drawing.Point(198, 38);
            this.createSizeButton.Name = "createSizeButton";
            this.createSizeButton.Size = new System.Drawing.Size(100, 23);
            this.createSizeButton.TabIndex = 4;
            this.createSizeButton.Text = "Задать размер";
            this.createSizeButton.UseVisualStyleBackColor = true;
            this.createSizeButton.Click += new System.EventHandler(CreateSizeButton_Click);
            // 
            // setDefaultInfo
            // 
            this.setDefaultInfoButton.Location = new System.Drawing.Point(198, 67);
            this.setDefaultInfoButton.Name = "setDefaultInfo";
            this.setDefaultInfoButton.Size = new System.Drawing.Size(100, 49);
            this.setDefaultInfoButton.TabIndex = 5;
            this.setDefaultInfoButton.Click += new System.EventHandler(SetDefaultInfoButton_Click);
            this.setDefaultInfoButton.Text = "Задать стандартные значения";
            this.setDefaultInfoButton.UseVisualStyleBackColor = true;
                        // 
            // outputPictureBox
            // 
            this.outputPictureBox.Location = new System.Drawing.Point(12, 198);
            this.outputPictureBox.Name = "outputPictureBox";
            this.outputPictureBox.Size = new System.Drawing.Size(450, 450);
            this.outputPictureBox.TabIndex = 6;
            this.outputPictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.createSizeButton);
            this.Controls.Add(this.sizeTextBox);
            this.Controls.Add(this.matrixGridView);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.launchButton);
            this.Controls.Add(this.setDefaultInfoButton);
            this.Controls.Add(this.outputPictureBox);
            this.Name = "MainForm";
            this.Text = "Branch and bound";
            ((System.ComponentModel.ISupportInitialize)(this.matrixGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.DataGridView matrixGridView;
        private System.Windows.Forms.TextBox sizeTextBox;
        private System.Windows.Forms.Button createSizeButton;
        private System.Windows.Forms.Button setDefaultInfoButton;
        private System.Windows.Forms.PictureBox outputPictureBox;
    }
}

