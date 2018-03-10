////////////////////////////////////////////////////////////////////////////
//
//  Anime recommender web SOAP service - SOAP service that recommends anime
//  movies based on user anime watch history.
//
//  Course : TCSS 559A - Web Services
//  Team members : Suganya Jeyaraman, Brandon Olson, Ramya Kumar
//  
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AnimeRecommenderWebService
{
    /// <summary>
    /// Summary description for Reco_Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Reco_Service : System.Web.Services.WebService
    {
        private string[,] DataMatrix;
        public Reco_Service()
        {
            //Path of anime movies dataset (.csv) and creating data matrix 
            string moviesfilepath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Data/features.csv");
            DataMatrix = Utility.LoadCsv(moviesfilepath);
        }

        //Web method exposed by this SOAP web service
        [WebMethod(MessageName ="String", Description ="This method returns recommended movies")]
        public string[] UserWatchHistory(string [] watchhistory)
        {
             //User matrix created out of user watched history             
            double[,] UserMatrix = new double[watchhistory.Length, DataMatrix.GetUpperBound(1) + 1];

            for (int r = 0; r <= UserMatrix.GetUpperBound(0); r++)
            {
                for (int i = 1; i <= DataMatrix.GetUpperBound(0); i++)
                {
                    if (watchhistory[r] == DataMatrix[i, 2])
                    {
                        for (int j = 3; j <= UserMatrix.GetUpperBound(1); j++)
                        {
                            UserMatrix[r, j - 3] = double.Parse(DataMatrix[i, j]);
                        }
                    }
                }
            }

            //To slice the movies ids, names and feature names from data matrix
            double[,] DataMatrix1 = Utility.SliceArray(DataMatrix);
          
            //Averaging user watch history matrix
            double[,] UserMatrix1 = SimilarityComputation.AveragingUserMatrix(UserMatrix);

            //To find similarity between user profile and movie database
            double[,] DParray = SimilarityComputation.DotProduct(UserMatrix1, DataMatrix1);

            //To sort the Dotproduct matrix to find the high similarity movies
            Tuple<double, int>[] Sortedarray = SimilarityComputation. ArraySorting(DParray);

            int greatestfives = 5;

            //To get the index of top fives in the sorted array of tuples
            int[] order = new int[greatestfives];

            for (int j = 0; j < greatestfives; j++)
            {
                order[j] = (Sortedarray[j].Item2);
            }

            //Use the indexes from order[] to get the movie names from Data matrix
            string[] RecommendedMovies = new string[greatestfives];

            for (int i = 0; i < greatestfives; i++)
            {
                RecommendedMovies[i]= (DataMatrix[(order[i] + 1), 2]);
            }
            return RecommendedMovies;
        }
    }
}
