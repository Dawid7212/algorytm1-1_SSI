namespace algorytm1_1_SSI
{
    partial class Form1
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
            PB1 = new PictureBox();
            RTB1 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)PB1).BeginInit();
            SuspendLayout();
            // 
            // PB1
            // 
            PB1.BackColor = SystemColors.ControlLightLight;
            PB1.Location = new Point(37, 30);
            PB1.Name = "PB1";
            PB1.Size = new Size(524, 484);
            PB1.TabIndex = 0;
            PB1.TabStop = false;
            // 
            // RTB1
            // 
            RTB1.Location = new Point(596, 30);
            RTB1.Name = "RTB1";
            RTB1.Size = new Size(204, 282);
            RTB1.TabIndex = 1;
            RTB1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(856, 536);
            Controls.Add(RTB1);
            Controls.Add(PB1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)PB1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox PB1;
        private RichTextBox RTB1;
    }
}
