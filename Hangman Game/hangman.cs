using System;

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

        public Hangman() {
            used = "";
            var rand = new Random();
            string[] lines = System.IO.File.ReadAllLines
                ("words.txt");
            word = lines[rand.Next(0,lines.Length)];
            char[] p = new char[word.Length];
            for (int i=0;i<word.Length;i++) {
                p[i] = '_';
            }
            progress = new string(p);
        }

        public bool guess(string s) {
            if (s.Equals(word)) {
                return true;
            }
            else {
                wrong++;
                return false;
            }
        }
        public bool guess(char s) {
            //****DEBUGGING CODE DELETE LATER***
            Console.WriteLine(word);
            Console.WriteLine(progress);
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

/*        static void Main(string[] args){
            Hangman h = new Hangman();
            Console.WriteLine(h.word);
            while (!h.guess(Console.ReadKey().KeyChar)) {
                Console.WriteLine();
                Console.WriteLine(h.progress);
                Console.WriteLine(h.wrong);
                Console.WriteLine(h.used);
                Console.WriteLine();
            }*/
    }
}
