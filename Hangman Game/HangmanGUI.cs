using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hangman
{
    public class HangmanForm : Form
    {
        private Button resetButton;
        private Button button2;
        private PictureBox hangedMan;
        private Label currentWord;
        private readonly int PICTURE_WIDTH = 300;
        private readonly int PICTURE_HEIGHT = 300;
        private readonly string[] filenames = new string[7]{"Hangmen/Hangman.jpg",
          "Hangmen/Hangman1.jpg", "Hangmen/Hangman2.jpg", "Hangmen/Hangman3.jpg",
          "Hangmen/Hangman4.jpg", "Hangmen/Hangman5.jpg", "Hangmen/Hangman6.jpg"};

        public HangmanForm(){
            this.StartPosition = FormStartPosition.CenterScreen;
            CreateResetButton();
            create_button2();
            CreateHangedMan();
            CreateCurrentWord();
            this.Size = new Size(700, 700);
            this.Text = "Hangman";
            this.BackColor = Color.FromName("white");
        }

        //place the iniaital currentWord label
        private void CreateCurrentWord(){
            currentWord = new Label();
            currentWord.AutoSize = true;
            currentWord.Font = new Font("Arial", 16);
            currentWord.Text = "_ _ _ _ _";
            currentWord.Location = new
              Point(((this.Width/2)-(currentWord.Width/2))+this.Width/4,
                    (this.Height/2)-(currentWord.Height/2));
            this.Controls.Add(currentWord);
        }
  
        //set the PictureBox to the starting image of the gallows
        private void CreateHangedMan(){
            hangedMan = new PictureBox();
            //set location based on size of window and image
            hangedMan.Location = new Point(((this.Width/2)-(PICTURE_WIDTH/2))/2,
                (this.Height/ 2) - (PICTURE_HEIGHT/2));
            hangedMan.SizeMode = PictureBoxSizeMode.StretchImage;
            hangedMan.Size = new Size(PICTURE_WIDTH, PICTURE_HEIGHT);
            hangedMan.Image = Image.FromFile(filenames[0]);
            this.Controls.Add(hangedMan);
        }

        //initialize the reset game button
        private void CreateResetButton(){
            resetButton = new Button();
            resetButton.AutoSize = true;
            //resetButton.Size = new Size(40, 40);
            resetButton.Location = new Point(5, 5);
            resetButton.Text = "Reset Game";
            this.Controls.Add(resetButton);
            resetButton.Click += new EventHandler(button_Click);
        }
        
        //helper method to initialize the button
        private void create_button2(){
            button2 = new Button();
            button2.Size = new Size(40, 40);
            button2.Location = new Point(50, 30);
            button2.Padding = new Padding(10);
            button2.Text = "Click me";
            this.Controls.Add(button2);
            button2.Click += new EventHandler(button_Click);
        }

        //called everytime the screen is resized
        protected override void OnResize(EventArgs e){
            base.OnResize(e);
            /*update the positions based on new size
            Hangedman image: position is halfway down and a quarter of the way across from
            the right */
            hangedMan.Location = new Point(((this.Width/2)-(PICTURE_WIDTH/2))/2,
                (this.Height / 2) - (PICTURE_HEIGHT/2));
            currentWord.Location = new
              Point(((this.Width/2)-(currentWord.Width/2))+this.Width/4,
                    (this.Height/2)-(currentWord.Height/2));
        }


        private void button_Click(object sender, EventArgs e){
            MessageBox.Show("Hey ya'll");
        }

        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.Run(new HangmanForm());
        }
    }
}
