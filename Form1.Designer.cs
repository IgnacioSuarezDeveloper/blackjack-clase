
namespace BlackJack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            AskForCardButton = new Button();
            buttonStartGame = new Button();
            label1 = new Label();
            label2 = new Label();
            StayButton = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            AskForCardButton.Location = new Point(366, 435);
            AskForCardButton.Name = "button1";
            AskForCardButton.Size = new Size(94, 29);
            AskForCardButton.TabIndex = 0;
            AskForCardButton.Text = "Carta";
            AskForCardButton.UseVisualStyleBackColor = true;
            AskForCardButton.Click += AskForCard;
            // 
            // buttonPutCarta
            // 
            buttonStartGame.Location = new Point(87, 435);
            buttonStartGame.Name = "buttonPutCarta";
            buttonStartGame.Size = new Size(94, 29);
            buttonStartGame.TabIndex = 1;
            buttonStartGame.Text = "Empezar";
            buttonStartGame.UseVisualStyleBackColor = true;
            buttonStartGame.Click += StartGame;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(53, 9);
            label1.Name = "label1";
            label1.Size = new Size(73, 20);
            label1.TabIndex = 2;
            label1.Text = "Croupier :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 214);
            label2.Name = "label2";
            label2.Size = new Size(62, 20);
            label2.TabIndex = 3;
            label2.Text = "Jugador";
            // 
            // PlantarseButton
            // 
            StayButton.Location = new Point(232, 435);
            StayButton.Name = "PlantarseButton";
            StayButton.Size = new Size(94, 29);
            StayButton.TabIndex = 4;
            StayButton.Text = "plantarse";
            StayButton.UseVisualStyleBackColor = true;
            StayButton.Click += Stay;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(568, 504);
            Controls.Add(StayButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(buttonStartGame);
            Controls.Add(AskForCardButton);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private Button AskForCardButton;
        private Button buttonStartGame;
        private Label label1;
        private Label label2;
        private Button StayButton;
    }
}
