/// A short and sweet C# 3.5 / LINQ implementation of 99 Bottles of Beer
/// Jeff Dietrich, jd@discordant.org - October 26, 2007

using System;
using System.Linq;
using System.Text;

namespace NinetyNineBottles
{
  class Beer
  {
    static void Main(string[] args)
    {
        StringBuilder beerLyric = new StringBuilder();
        string nl = System.Environment.NewLine;

        var beers =
            (from n in Enumerable.Range(0, 100)
             select new { 
               Say =  n == 0 ? "No more bottles" : 
                     (n == 1 ? "1 bottle" : n.ToString() + " bottles"),
               Next = n == 1 ? "no more bottles" : 
                     (n == 0 ? "99 bottles" : 
                     (n == 2 ? "1 bottle" : n.ToString() + " bottles")),
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
