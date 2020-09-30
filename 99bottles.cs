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
            StringBuilder beerLyric = new StringBuilder();
            string nl = System.Environment.NewLine;
            var beers =
                (from n in Enumerable.Range(0, lines)
                 select new { 
                 Say =  n == 0 ? "No more lines" : 
                 (n == 1 ? "1 line" : n.ToString() + " lines"),
                 Next = n == 1 ? "no more lines" : 
                 (n == 0 ? "99 lines" : 
                  (n == 2 ? "1 line" : n.ToString() + " lines")),
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
            Console.ReadLine();
        }
    }
}
