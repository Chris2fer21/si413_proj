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
        private MenuItem difficulty;

        public HangmanController(){
            this.StartPosition = FormStartPosition.CenterScreen;
            CreateStartGameButton();
            CreateMyMainMenu();

            this.Size = new Size(700, 700);
            this.MinimumSize = new Size(550, 550);
            this.Text = "Hangman";
            this.BackColor = Color.FromName("white");
        }

        /*
           Method that builds a mainmenu at the top of the window allowing
           for the difficulty of the game to be chosen
        */
        private void CreateMyMainMenu(){
            // Create an empty MainMenu.
            MainMenu mainMenu = new MainMenu();

            difficulty = new MenuItem();
            MenuItem easy = new MenuItem();
            MenuItem medium = new MenuItem();
            MenuItem hard = new MenuItem();

            difficulty.Text = "Difficulty";
            easy.Text = "Easy";
            medium.Text = "Medium";
            hard.Text = "Hard";

            // Add difficulty menu to main menu and difficulties to the
            // difficulty menu
            mainMenu.MenuItems.Add(difficulty);
            difficulty.MenuItems.Add(easy);
            difficulty.MenuItems.Add(medium);
            difficulty.MenuItems.Add(hard);
           
            // Add functionality with a click method
            easy.Click += new EventHandler(this.difficulty_Click);
            medium.Click += new EventHandler(this.difficulty_Click);
            hard.Click += new EventHandler(this.difficulty_Click);
             
            //Easy difficulty by default
            easy.Checked = true;

            // Bind the MainMenu to Hangman Controller
            Menu = mainMenu;   
        }

        private void difficulty_Click(object sender, EventArgs e){
            //Clear all checked MenuItems before checking the one clicked. 
            foreach(MenuItem menu in difficulty.MenuItems)
                menu.Checked = false;
            
            //Mark clicked menuitem as checked
            ((MenuItem)sender).Checked = true;
        }

        //set parameters of the startGameButton
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

        //enter into a new Hangman game when start game button is clicked
        private void startGameButton_Click(object sender, EventArgs e){
            string difficultyLevel = "";
            //find the currently checked difficulty level in the difficulty
            //menu
            foreach(MenuItem menu in difficulty.MenuItems){
                if(menu.Checked)
                    difficultyLevel = menu.Text;
            }
            
            Application.Exit();
            //switch on given difficulty level
            switch(difficultyLevel){
                case "Easy":
                    Application.Run(new HangmanForm(0, 0, 0));
                    break;
                case "Medium":
                    Application.Run(new HangmanForm(0, 0, 1));
                    break;
                case "Hard":
                    Application.Run(new HangmanForm(0, 0, 2));
                    break;
            }
        }

        //ensure that the controller exits properly upon the closing of the window
        protected override void OnClosed(EventArgs e){
            base.OnClosed(e);
            Application.Exit();
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
