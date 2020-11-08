using System;

namespace Hangman
{
    /* Class that represents a player of the Hangman game
       Author: MIDN Sean Moriarty 
     */
    public class Player{
        private int score;
        public int Score
        { get{return score;}}


        public Player(int score){
            this.score = score;
        }

        //call this method when the Player wins a game
        public void UpdateScore(){
          score++;
        }

    }
}

