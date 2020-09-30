/// A short and sweet C# 3.5 / LINQ implementation of 99 Bottles of Beer
/// Jeff Dietrich, jd@discordant.org - October 26, 2007

using System;
using System.Linq;
using System.Text;
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

        static void WriteBeer(int lines) {
            Random r = new Random(); //Random object to generate random numbers
      StringBuilder beerLyric = new StringBuilder(); //final output
      string nl = System.Environment.NewLine; //convienient nl char
      //list holding the numbers to be outputted
      List<int> Numbers = new List<int>();

      //for loop to generate lists of numbers to output incrementing by rand
      //num between 1 and 10
      for(int i = 1; i < 100; i += r.Next(1, lines))
        Numbers.Add(i);
            var beers =
                (from n in Numbers
                 select new { 
                 Say =  n == 0 ? "No more lines" : 
                 (n == 1 ? "1 line" : PrimeFactors(n) + " lines"),
                 Next = n == 1 ? "no more lines" : 
                 (n == 0 ? "99 lines" : 
                  (n == 2 ? "1 line" : PrimeFacots(n) + " lines")),
                 Action = n == 0 ? "Go to the store and buy some more" : 
                 "Print it out, stand up and shout"
                 });

            foreach (var beer in beers)
            {
                beerLyric.AppendFormat("{0} of text on the screen, {1} of text.{2}",
                        beer.Say, beer.Say.ToLower(), nl);
                beerLyric.AppendFormat("{0}, {1} of text on the screen.{2}", 
                        beer.Action, beer.Next, nl);
                beerLyric.AppendLine();
            }
            Console.WriteLine(beerLyric.ToString());
        }
    }
}
