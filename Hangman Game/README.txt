MIDN Donald Jones 213228
MIDN Sean Moriarty 214728

Building the project to run:

If on VM or if possible run:
sudo apt-get update
sudo apt-get upgrade
sudo apt-get install mono-complete

Compile with:
make
Run with:
mono HangmanController.exe or ./HangmanController.exe if mono
is not available


How to Play/Operate Hangman:

Start Screen:
On the start screen (made with HangmanController) you can choose between
three difficulties: easy, medium, and hard in the menu at the top left of
the window. Easy is chosen by default. After choosing that or going with
the default of easy, you hit the Start Game button which brings you to 
the main Hangman game.

Game Window:
The game window (made with HangmanGUI) has everything you would expect out of
a normal operation of Hangman. At the top right you will find a list of every
letter of the alphabet to start with. As you select one and press the "Guess
Letter" button, it will check if that letter is in the current unknown word.
If it is in the word, the screen will display everywhere in the word it is in.
If not, another part of the hanged man will be displayed on the screen. You
can guess letters until you get the whole word or the man is drawn and you
lose. When you win the score seen at the bottom of the window will update to
reflect one point added to the player. If you lose, the computer gains one
point. The next thing you have the ability to do if you think you know the
whole word is to guess it outright. The top middle of the window has a text
box to type your guess into, and after pressing the "Guess Word" button, your
guess will be checked. If you are wrong another part of the man will be drawn,
but if you are correct then you win the round. The "Reset Round" button at the
top left of the window will reset the hanged man and give you a new word if
you are not happy with your performance. The "New Game" button will take you
back to the start screen if you want to reset the score or choose a different
difficulty.

Extra Note:
There is a field in hangman.cs called debug that can be set to true in order
to display the word that is trying to be guessed after one letter has been guessed. 
This can be used to help test all the functionality (guessing words and letters) 
of the game faster. 
