using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hangman
{
    /* Class that extends Form to makeup the GUI of a "homescreen" and
     * interface between classes
       Author: MIDN Sean Moriarty 
     */
    public class HangmanController : Form{
        private Button startGameButton; 

        public HangmanController(){
            this.StartPosition = FormStartPosition.CenterScreen;
            CreateStartGameButton();
            this.Size = new Size(700, 700);
            this.Text = "Hangman";
            this.BackColor = Color.FromName("white");
        }

        private void CreateStartGameButton(){
            startGameButton = new Button();
            startGameButton.AutoSize = true;
            startGameButton.Text = "Start Game";
            startGameButton.Font = new Font("Arial", 20);
            startGameButton.Location = new
              Point((this.Width/2)-(startGameButton.Width/2),(this.Height/2)-(startGameButton.Height/2));
            startGameButton.Click += new EventHandler(startGameButton_Click);
            this.Controls.Add(startGameButton);  
        }

        private void startGameButton_Click(object sender, EventArgs e){
            Application.Exit(); 
            Application.Run(new HangmanForm("Sean"));
        }

        //called everytime the screen is resized
        protected override void OnResize(EventArgs e){
            base.OnResize(e);
            startGameButton.Location = new
              Point((this.Width/2)-(startGameButton.Width/2),(this.Height/2)-(startGameButton.Height/2));

        }

        [STAThread]
        static void Main(){
            Application.EnableVisualStyles();
            Application.Run(new HangmanController());
        }
    }
}
