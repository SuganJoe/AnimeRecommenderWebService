using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoconsoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] watchhistory = new string[6] { "HÅzuki no Reitetsu",
                "Dungeon ni Deai o Motomeru no wa Machigatteiru DarÅ ka: Arrow of the Orion",
                "Is It Wrong to Try to Pick Up Girls in a Dungeon?",
                "Goblin Slayer",
                "Sword Art Online: Alicization",
                "Midnight Crazy Trail"};

            //accessing Reco_Service class through Web service reference
            localhost.Reco_Service movies = new localhost.Reco_Service();
            string[] Recommendedmovies = movies.UserWatchHistory(watchhistory);

            for (int i = 0; i < Recommendedmovies.Length; i++)
            {
                Console.WriteLine(Recommendedmovies[i]);
            }


        }
    }
}
