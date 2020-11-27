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
        {get{
                string t = String.Copy(progress);
                int n = (progress.Length-1) * 2;
                for (int i=1; i<n; i+=2){
                    t = t.Insert(i," ");
                }
                return t;
            }} //return as a string
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
                wrong = 6;
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
            Hangman h = new Hangman(2);
            Console.WriteLine(h.Word);
            Console.WriteLine(h.Progress);
            h.guess('e');
            Console.WriteLine(h.Progress);
            h.guess('i');
            Console.WriteLine(h.Progress);
            h.guess('o');
            Console.WriteLine(h.Progress);
            h.guess('u');
            Console.WriteLine(h.Progress);
        }*/
    }
}
