namespace ImageAlignment
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
            label1 = new Label();
            chkGuidelines = new CheckBox();
            chkDiagonals = new CheckBox();
            chkAutoCrop = new CheckBox();
            btnRotate90 = new Button();
            btnRotateMinus90 = new Button();
            btnAutoAlign = new Button();
            pictureBox1 = new PictureBox();
            tbAngle = new TrackBar();
            txtAngle = new TextBox();
            btnLoadImage = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbAngle).BeginInit();
            SuspendLayout();

            // Label
            label1.AutoSize = true;
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 1;
            label1.Text = "Настройки";

            // CheckBoxes
            chkGuidelines.AutoSize = true;
            chkGuidelines.Location = new Point(12, 67);
            chkGuidelines.Name = "chkGuidelines";
            chkGuidelines.Size = new Size(152, 19);
            chkGuidelines.TabIndex = 2;
            chkGuidelines.Text = "Направляющие линии";
            chkGuidelines.UseVisualStyleBackColor = true;

            chkDiagonals.AutoSize = true;
            chkDiagonals.Location = new Point(12, 92);
            chkDiagonals.Name = "chkDiagonals";
            chkDiagonals.Size = new Size(175, 19);
            chkDiagonals.TabIndex = 3;
            chkDiagonals.Text = "Направляющие диагонали";
            chkDiagonals.UseVisualStyleBackColor = true;

            chkAutoCrop.AutoSize = true;
            chkAutoCrop.Location = new Point(12, 117);
            chkAutoCrop.Name = "chkAutoCrop";
            chkAutoCrop.Size = new Size(132, 19);
            chkAutoCrop.TabIndex = 4;
            chkAutoCrop.Text = "Автовыравнивание";
            chkAutoCrop.UseVisualStyleBackColor = true;

            // Buttons
            btnRotate90.Location = new Point(12, 163);
            btnRotate90.Name = "btnRotate90";
            btnRotate90.Size = new Size(75, 23);
            btnRotate90.TabIndex = 5;
            btnRotate90.Text = "+90";
            btnRotate90.UseVisualStyleBackColor = true;

            btnRotateMinus90.Location = new Point(12, 192);
            btnRotateMinus90.Name = "btnRotateMinus90";
            btnRotateMinus90.Size = new Size(75, 23);
            btnRotateMinus90.TabIndex = 6;
            btnRotateMinus90.Text = "-90";
            btnRotateMinus90.UseVisualStyleBackColor = true;

            btnAutoAlign.Location = new Point(12, 221);
            btnAutoAlign.Name = "btnAutoAlign";
            btnAutoAlign.Size = new Size(75, 23);
            btnAutoAlign.TabIndex = 7;
            btnAutoAlign.Text = "авто";
            btnAutoAlign.UseVisualStyleBackColor = true;

            btnLoadImage.Location = new Point(12, 250);
            btnLoadImage.Name = "btnLoadImage";
            btnLoadImage.Size = new Size(75, 23);
            btnLoadImage.TabIndex = 11;
            btnLoadImage.Text = "Загрузить";
            btnLoadImage.UseVisualStyleBackColor = true;

            // PictureBox
            pictureBox1.Location = new Point(193, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(595, 327);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;

            // TrackBar
            tbAngle.Location = new Point(12, 393);
            tbAngle.Name = "tbAngle";
            tbAngle.Size = new Size(714, 45);
            tbAngle.TabIndex = 9;

            // TextBox
            txtAngle.Location = new Point(732, 393);
            txtAngle.Name = "txtAngle";
            txtAngle.Size = new Size(56, 23);
            txtAngle.TabIndex = 10;

            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLoadImage);
            Controls.Add(txtAngle);
            Controls.Add(tbAngle);
            Controls.Add(pictureBox1);
            Controls.Add(btnAutoAlign);
            Controls.Add(btnRotateMinus90);
            Controls.Add(btnRotate90);
            Controls.Add(chkAutoCrop);
            Controls.Add(chkDiagonals);
            Controls.Add(chkGuidelines);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Автовыравнивание изображения";

            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbAngle).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox chkGuidelines;
        private CheckBox chkDiagonals;
        private CheckBox chkAutoCrop;
        private Button btnRotate90;
        private Button btnRotateMinus90;
        private Button btnAutoAlign;
        private Button btnLoadImage;
        private PictureBox pictureBox1;
        private TrackBar tbAngle;
        private TextBox txtAngle;
    }
}
