/// A short and sweet C# 3.5 / LINQ implementation of 99 Bottles of Beer
/// Jeff Dietrich, jd@discordant.org - October 26, 2007
// Revised and worked on by MIDN Sean Moriarty and MIDN Donald Jones 01 OCT 20

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading;
using Gtk;

namespace NinetyNineBottles
{
    class Beer
    {
        public VBox f;
        public Entry e;
        public Button b;
        public Beer() {
            f = new VBox();
            Label text = new Label();
            text.Text = "How many lines?";
            f.Add(text);
            e = new Entry();
            b = new Button("Enter");
            b.Clicked += new EventHandler(EnterHandler);
            f.Add(e);
            f.Add(b);
        }
        void EnterHandler(object obj, EventArgs args){
            int lines = Convert.ToInt32(e.Text);
            WriteBeer(lines);
        }

        //take a number n and return a string that is n in factored form
        static string PrimeFactors(int n){
            string factors = "("; //string that will contain the prime factors
            //return empty paranthesis if n == 0 || 1
            if(n == 0 || n == 1)
                return "()";
            else{
                for(int i = 2; n > 1; i++){
                    if(n % i == 0){
                        //while loop to check how many times i is a prime factor
                        while(n % i == 0){
                            n /= i;
                            if(factors.Length == 1)
                                factors += i.ToString(); //first one, no * between them
                            else
                                factors += "*" + i.ToString(); //seperate with *
                        }
                    }
                }
            }
            return factors + ")";
        }

        static void Main(string[] args)
        {
            Application.Init();
            Window myWin = new Window("Prompt");
            Beer B = new Beer();
            myWin.Add(B.f);
            myWin.ShowAll();
            Application.Run();

        }
       
        //Helper function to find the next factorization to display in the
        //output based on the list of random numbers created
        static string NextFactor(List<int> Numbers, int lastFactor){
            Random r = new Random(); //Random object to generate random numbers
            try{
                //return the next index of the array if it exists
                return PrimeFactors(Numbers[Numbers.IndexOf(lastFactor) + 1]);
            }catch(Exception e){
                //return a number that is between 1 and 10 more than before
                return PrimeFactors(Numbers[Numbers.IndexOf(lastFactor)] +
                    r.Next(1,10)); 
            }
        }

        //writes the lyrics to stdout
        static void WriteBeer(int lines) {
            Random r = new Random(); //Random object to generate random numbers
            StringBuilder beerLyric = new StringBuilder(); //final output
            string nl = System.Environment.NewLine; //convienient nl char
            //list holding the numbers to be outputted
            List<int> Numbers = new List<int>();

            //for loop to generate lists of numbers to output incrementing by rand
            //num between 1 and 10
            for(int i = 1; i < lines; i += r.Next(1, 10))
                Numbers.Add(i);

            //creates an Enumerable object that fills lyrics in with given numbers
            var beers =
                (from n in Numbers
                 select new { 
                 Say =  n == 0 ? "No more lines" : 
                 (n == 1 ? "() line" : PrimeFactors(n) + " lines"),
                 Next = n == 1 ? NextFactor(Numbers, n) + " lines" : 
                 (n == 0 ? "99 lines" : 
                  (n == 2 ? NextFactor(Numbers, n)+" line" :
                   NextFactor(Numbers, n)+" lines")),
                 Action = n == 0 ? "Go to the store and buy some more" : 
                 "Print it out, stand up and shout"
                 });

            //goes through Enumerable object and adds each to the StringBuilder
            foreach (var beer in beers)
            {
                beerLyric.Clear(); //clear beerLyric for next line
                beerLyric.AppendFormat("{0} of text on the screen, {1} of text.{2}",
                        beer.Say, beer.Say.ToLower(), nl);
                beerLyric.AppendFormat("{0}, {1} of text on the screen.{2}", 
                        beer.Action, beer.Next, nl);
                beerLyric.AppendLine();
                Console.WriteLine(beerLyric.ToString()); //output the next lyrics
                Thread.Sleep(1000); //sleep for one second
            }
        }
    }
}
