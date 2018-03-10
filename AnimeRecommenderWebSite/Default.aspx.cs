using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        watchHistoryContainer.Visible = true;
        recommendationContainer.Visible = true;

        string myAnimeListUserName = UserName.Text;
        string URL = "https://myanimelist.net/malappinfo.php?u=" + myAnimeListUserName + "&status=all&type=anime";
        HttpWebRequest serviceRequest = (HttpWebRequest) WebRequest.Create(URL);
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

        List<Anime> watchhistory = new List<Anime>();
        foreach (XmlNode animeNode in animeNodes)
        {
            XmlNode titleNode = animeNode.SelectSingleNode("series_title");
            XmlNode imageNode = animeNode.SelectSingleNode("series_image");

            Anime anime = new Anime();
            anime.Title = titleNode.InnerText;
            anime.ImageUrl = imageNode.InnerText;

            watchhistory.Add(anime);
        }
        WatchHistoryList.DataSource = watchhistory;
        WatchHistoryList.DataBind();

        AnimeRecommenderWebService.ArrayOfString arrStrings = new AnimeRecommenderWebService.ArrayOfString();
        arrStrings.AddRange(watchhistory.Select(h => h.Title));
        AnimeRecommenderWebService.Reco_ServiceSoapClient movies = new AnimeRecommenderWebService.Reco_ServiceSoapClient();
        AnimeRecommenderWebService.ArrayOfString Recommendedmovies = movies.UserWatchHistory(arrStrings);

        Recommendations.DataSource = Recommendedmovies;
        Recommendations.DataBind();
    } 
}

public class Anime
{
    public String Title { get; set; }
    public String ImageUrl { get; set; }
}