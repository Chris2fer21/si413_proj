using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hangman
{
    /* Class that extends Form to makeup the main GUI of the Hangman game
       Author: MIDN Sean Moriarty 
     */
    public class HangmanForm : Form
    {
        private Button resetButton;
        private Button newGameButton;
        private Button guessLetterButton;
        private Button guessWordButton;
        private PictureBox hangedMan;
        private Label currentWord;
        private Label scoreLabel;
        private Hangman hangman;
        private Player player;
        private ListBox alphabetList;
        private MaskedTextBox guessWordBox;
        private int computerScore, difficulty;
        //dimensions of whatever Hangman picture is loaded
        private readonly int PICTURE_WIDTH = 300;
        private readonly int PICTURE_HEIGHT = 300;
        private readonly string[] filenames = new string[7]{"Hangmen/Hangman.jpg",
          "Hangmen/Hangman1.jpg", "Hangmen/Hangman2.jpg", "Hangmen/Hangman3.jpg",
          "Hangmen/Hangman4.jpg", "Hangmen/Hangman5.jpg", "Hangmen/Hangman6.jpg"};

        //Constructor that creates all the elements of the GUI and sets
        //parameters of the window
        public HangmanForm(int computerScore, int playerScore, int difficulty){
            hangman = new Hangman(difficulty);
            this.difficulty = difficulty;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            CreateResetButton();
            CreateGuessLetterButton();
            CreateHangedMan();
            CreateCurrentWord();
            CreateAlphabetList();
            CreateGuessWordBox();
            CreateGuessWordButton();
            CreateNewGameButton();
            player = new Player(playerScore);
            this.computerScore = computerScore;
            CreateScoreLabel();

            this.Size = new Size(700, 700);
            //smaller than this and everything looks weird
            this.MinimumSize = new Size(550, 550);
            this.Text = "Hangman";
            this.BackColor = Color.FromName("white");
        }

        private void CreateScoreLabel(){
            scoreLabel = new Label();
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Arial", 16);
            string text = "Current Score: Player "+player.Score+" - Computer "+
              computerScore;
            scoreLabel.Text = text;
            scoreLabel.Location = new
              Point((this.Width/2)-(scoreLabel.Width/2), this.Height -
                  scoreLabel.Height - 10);  
            this.Controls.Add(scoreLabel); 
        }

        private void CreateGuessWordBox(){
            guessWordBox = new MaskedTextBox();
            //guessWordBox.Mask = "a*"; //restricts text to only a-z A-Z
            guessWordBox.Location = new 
              Point((this.Width/2)-(guessWordBox.Width/2), 10);
            this.Controls.Add(guessWordBox);
        }

        private void CreateAlphabetList()
        {
            // Create an instance of the ListBox.
            alphabetList = new ListBox();
            // Set the size and location of the ListBox.
            alphabetList.Size = new System.Drawing.Size(50, 150);
            alphabetList.Location = new System.Drawing.Point(this.Width -
                alphabetList.Width - 5, 5);
            this.Controls.Add(alphabetList);
        
            // Shutdown the painting of the ListBox as items are added.
            alphabetList.BeginUpdate();
            // Loop through and add all the letters of the alphabet based on
            // ascii value
            for (int i = 97; i < 123; i++)
              alphabetList.Items.Add(((char)i).ToString());
            // Allow the ListBox to repaint and display the new items.
            alphabetList.EndUpdate();
        }

        //place the iniaital currentWord label
        private void CreateCurrentWord(){
            currentWord = new Label();
            currentWord.AutoSize = true;
            currentWord.Font = new Font("Arial", 16);
            
            //get the current progress on the word
            string text = hangman.Progress;
            currentWord.Text = text;
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

        //initialize the new game button
        private void CreateNewGameButton(){
            newGameButton = new Button();
            newGameButton.AutoSize = true;
            //place new game button right below reset game button
            newGameButton.Location = new 
              Point(5, resetButton.Height + 15);
            newGameButton.Text = "New Game";
            this.Controls.Add(newGameButton);
            newGameButton.Click += new EventHandler(newGameButton_Click);
        }

        //initialize the guess word button
        private void CreateGuessWordButton(){
            guessWordButton = new Button();
            guessWordButton.AutoSize = true;
            guessWordButton.Location = new
              Point((this.Width/2)-(guessWordButton.Width/2),
                  guessWordBox.Height + 20);
            guessWordButton.Text = "Guess Word";
            this.Controls.Add(guessWordButton);
            guessWordButton.Click += new EventHandler(guessWordButton_Click);
        }

        //initialize the reset game button
        private void CreateResetButton(){
            resetButton = new Button();
            resetButton.AutoSize = true;
            resetButton.Location = new Point(5, 5);
            resetButton.Text = "Reset Game";
            this.Controls.Add(resetButton);
            resetButton.Click += new EventHandler(resetButton_Click);
        }
        
        //helper method to initialize the guess letter button
        private void CreateGuessLetterButton(){
            guessLetterButton = new Button();
            guessLetterButton.AutoSize = true;
            guessLetterButton.Location = new
              Point(((this.Width/2)-(guessLetterButton.Width/2))+this.Width/4, 10);
            guessLetterButton.Text = "Guess Letter";
            this.Controls.Add(guessLetterButton);
            guessLetterButton.Click += new EventHandler(guessLetterButton_Click);
        }

        //called everytime the screen is resized
        protected override void OnResize(EventArgs e){
            base.OnResize(e);
            /*update the positions based on new size
            Hangedman image: position is halfway down and a quarter of the way across from
            the right
            currentWord: 3/4 of the way across the page and halfway down
            alphabetList: Top right of the window
            createGuessLetterButton: 3/4 across the top of the window
            guessWordBox: halfway across the top of the screen
            guessWordButton: halfway across screen below guessWordBox
            scoreLabel: halfway across and all the way down
             */
            hangedMan.Location = new Point(((this.Width/2)-(PICTURE_WIDTH/2))/2,
                (this.Height / 2) - (PICTURE_HEIGHT/2));
            currentWord.Location = new
              Point(((this.Width/2)-(currentWord.Width/2))+this.Width/4,
                    (this.Height/2)-(currentWord.Height/2));
            alphabetList.Location = new System.Drawing.Point(this.Width -
                alphabetList.Width - 10, 5);
            guessLetterButton.Location = new
              Point(((this.Width/2)-(guessLetterButton.Width/2))+this.Width/4, 10);
            guessWordBox.Location = new 
              Point((this.Width/2)-(guessWordBox.Width/2), 10);
            guessWordButton.Location = new
              Point((this.Width/2)-(guessWordButton.Width/2),
                  guessWordBox.Height + 20);
            scoreLabel.Location = new
              Point((this.Width/2)-(scoreLabel.Width/2), this.Height -
                  scoreLabel.Height - 30);
        }

        //ensure that the game exits properly upon the closing of the window
        protected override void OnClosed(EventArgs e){
            base.OnClosed(e);
            Application.Exit();
        }
        
        //resets the game from the beginning with new word
        private void ResetGame(){
            Application.Exit();
            Application.Run(new HangmanForm(computerScore, player.Score,
                  difficulty));

        }

        //When new game button pressed, return to HangmanController to start a
        //new game
        private void newGameButton_Click(object sender, EventArgs e){
            string title = "New Game";
            string text = "Are you sure you want to start a new game?\n";
            text += "You will lose your current score and\n";
            text += " get to choose difficulty again.";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //create a message box with the options yes or no to ensure user
            //wants to reset the game
            DialogResult result = MessageBox.Show(text, title, buttons);
            if(result == DialogResult.Yes){
              Application.Exit();
              Application.Run(new HangmanController());
            }
        }

        private void guessWordButton_Click(object sender, EventArgs e){
            //get text from text box
            string guess = guessWordBox.Text.ToLower();
            //***DEBUGING CODE DELETE LATER*****
            Console.WriteLine("Word: "+guess);
            if(guess == "")
                MessageBox.Show("Please type in a word to guess with!");
            else{
                bool wordGuessed = hangman.guess(guess);
                //update image to reflect number of wrong guesses
                hangedMan.Image = Image.FromFile(filenames[hangman.Wrong]);
                if(GameOver(wordGuessed))
                    ResetGame();
            }
        }

        //When reset button clicked, reset the game
        private void resetButton_Click(object sender, EventArgs e){
            string title = "Reset Game";
            string text = "Are you sure you want to reset the game?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //create a message box with the options yes or no to ensure user
            //wants to reset the game
            DialogResult result = MessageBox.Show(text, title, buttons);
            if(result == DialogResult.Yes)
                ResetGame();
        }

        /*method that checks if the game is over based on number of guesses
          made or if the word was guessed
          Returns true if the game is over, false otherwise 
         */
        private bool GameOver(bool wordGuessed){
            string messageTitle = "Game Results";
            if(wordGuessed){
                MessageBox.Show("Congratulations you won!", messageTitle);
                player.UpdateScore();
                return true;
            }
            else if(hangman.Wrong == 6){ //too many wrong guesses
                MessageBox.Show("Too many wrong guesses, you lost!\nThe word was: "
                    +hangman.Word, messageTitle);
                computerScore++;
                return true;
            }
            else
                return false;
        }

        //onClick method for guessing a new letter 
        private void guessLetterButton_Click(object sender, EventArgs e){
            //send current selected letter to Hangman 
            //that returns a bool if the word is fully guessed or not
            if(alphabetList.SelectedItem == null)
                MessageBox.Show("Please select a letter to guess with!");
            else{
                char guessedLetter = Convert.ToChar(alphabetList.SelectedItem);
                //update the GUI to reflect the change
                alphabetList.BeginUpdate();
                alphabetList.Items.Remove(alphabetList.SelectedItem);
                alphabetList.EndUpdate();

                //***DEBUGING CODE DELETE LATER*****
                Console.WriteLine("Letter: "+guessedLetter);
                bool letterGuessed = hangman.guess(guessedLetter);
                currentWord.Text = hangman.Progress;
                //update image to reflect any changes in number of wrong guesses
                hangedMan.Image = Image.FromFile(filenames[hangman.Wrong]);
                //if the game is over then reset the game
                if(GameOver(letterGuessed))
                    ResetGame();
            }
        }
    }
}
