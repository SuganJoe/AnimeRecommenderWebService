using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Recoconsoleapp
{
    class Program
    {
        static void Main(string[] args)
        {
            string myAnimeListUserName = "";

            Console.Write("Please enter a MyAnimeList username: ");
            myAnimeListUserName = Console.ReadLine();

            string URL = "https://myanimelist.net/malappinfo.php?u=" + myAnimeListUserName + "&status=all&type=anime";
            HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(URL);
            serviceRequest.Method = "GET";
            serviceRequest.ContentLength = 0;
            serviceRequest.Accept = "application/xml";
            HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);
            string responseFromServer = readStream.ReadToEnd();

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseFromServer);
            XmlElement root = xmlDocument.DocumentElement;
            XmlNodeList animeNodes = root.SelectNodes("anime");

            List<string> watchhistory = new List<string>();

            Console.WriteLine("Based on your watch history of:");
            foreach (XmlNode animeNode in animeNodes)
            {
                XmlNode titleNode = animeNode.SelectSingleNode("series_title");
                string title = titleNode.InnerText;
                watchhistory.Add(title);
                Console.WriteLine(titleNode.InnerText);
            }

            // accessing Reco_Service class through Web service reference
            localhost.Reco_Service movies = new localhost.Reco_Service();
            string[] Recommendedmovies = movies.UserWatchHistory(watchhistory.ToArray());

            Console.WriteLine("We recommend:");
            foreach (string recommendedMovie in Recommendedmovies)
            {
                Console.WriteLine(recommendedMovie);
            }

            Console.WriteLine("Press any key to end the program...");
            Console.ReadKey();
        }
    }
}
