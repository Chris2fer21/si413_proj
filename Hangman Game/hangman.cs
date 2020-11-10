using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman {
    class Hangman {
        private string used;
        public string Used
        { get{return used;} }
        private string word;
        public string Word
        { get{return word;}}
        private int wrong;
        public int Wrong
        {get{return wrong;} }
        private string progress;
        public string Progress
        {get{return progress;}} //return as a string
        private bool debug = false;

        // Contructor for a game of hangman
        // param diff is the requested difficulty of the word
        public Hangman(int diff) {
            if (diff<0 || diff>2)
                diff = 0;
            used = "";
            var rand = new Random();
            string[] lines = System.IO.File.ReadAllLines
                ("wordlist.10000");
            word = lines[rand.Next(0,lines.Length)];
            while(difficulty(word)!=diff)
                word = lines[rand.Next(0,lines.Length)];
            char[] p = new char[word.Length];
            for (int i=0;i<word.Length;i++) {
                p[i] = '_';
            }
            progress = new string(p);
        }

        // method to guess the entire word
        public bool guess(string s) {
            if (s == word) {
                return true;
            }
            else {
                wrong++;
                return false;
            }
        }

        // method to guess a letter in the word
        public bool guess(char s) {
            if(debug)
                Console.WriteLine("Word: "+word);
            if (!used.Contains(s.ToString())) {
                if (word.Contains(s.ToString())) {
                    char[] p = progress.ToCharArray();
                    for (int i=0;i<word.Length;i++) {
                        if (word[i]==s)
                            p[i] = s;
                    }
                    progress = new string(p);
                }
                else {
                    wrong++;
                }
                used = string.Concat(used,s.ToString());
            }
            //check for completion
            if (progress.Contains("_"))
                return false;
            else 
                return true;
        }

        // determines the difficulty of the word based on:
        // vowels, length, and unique letters
        private static int difficulty(string word) {
            string[] vowels = {"a","e","i","o","u"};
            string unique = new String(word.Distinct().ToArray());
            int vowels_n = 0;
            for (int i=0;i<vowels.Length;i++) {
                if (word.Contains(vowels[i]))
                    vowels_n++;
            }
            int score = word.Length * unique.Length * (6-vowels_n);
            if (score<101)
                return 0;
            if (score>169)
                return 2;
            else
                return 1;
        } 

        /*static void Main(string[] args){
            string[] lines = System.IO.File.ReadAllLines
                                ("wordlist.10000");
            List<int> scores = new List<int>();
            for(int i=0;i<lines.Length;i++)
                scores.Add(difficulty(lines[i]));

            scores.Sort();
            Console.WriteLine(scores[0]);
            Console.WriteLine(scores[(int)(scores.Count*.33)]);
            Console.WriteLine(scores[(int)(scores.Count*.67)]);
            Hangman h = new Hangman(2);
        }*/
    }
}
