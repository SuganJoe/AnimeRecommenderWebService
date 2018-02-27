using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommenderWS
{
    class Program
    {
        //To find dotproduct of user array and movie array
        public static double[,] DotProduct(double[,] V1, double[,] V2)
        {
            int height = V2.GetUpperBound(0);
            int Length = V2.GetUpperBound(1);


            //Transforming the movie array
            double[,] transformarray = new double[Length + 1, height + 1];

            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= Length; j++)
                {

                    transformarray[j, i] = V2[i, j];

                }
            }

            int V1r = V1.GetUpperBound(0) + 1;
            int l2 = transformarray.GetUpperBound(1) + 1;
            int l3 = V1.GetUpperBound(1) + 1;

            double[,] DParray = new double[V1r, l2];
            int l1 = DParray.GetUpperBound(0) + 1;

            //Finding dot product
            for (int k = 0; k < l1; k++)
            {
                for (int j = 0; j < l2; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < l3; i++)
                    {
                        sum = sum + V1[k, i] * transformarray[i, j];

                    }

                    DParray[k, j] = sum;
                }
            }

            //Printing the dotproduct array
            //for (int i = 0; i <= (DParray.GetUpperBound(0)); i++)
            //{
            //    for (int j = 0; j <= (DParray.GetUpperBound(1)); j++)
            //    {
            //        Console.WriteLine(DParray[i, j]);
            //    }
            //}
            return DParray;
        }

            public static Tuple<double,int> [] ArraySorting(double [,] DParray)
            {


            //Tuple Creation
            Tuple<double, int>[] Tarray = new Tuple<double, int>[DParray.Length];


            for (int i = 0; i <= DParray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= DParray.GetUpperBound(1); j++)
                {
                    Tarray[j] = new Tuple<double, int>(DParray[i, j], j);
                }
            }


            //Sorting of the tuple array
            for (int i = (Tarray.Length - 1); i > 0; i--)
            {
                int max_index = i;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (Tarray[j].Item1 < Tarray[max_index].Item1)
                    {
                        max_index = j;
                    }
                    Tuple<double, int> temp = Tarray[max_index];
                    Tarray[max_index] = Tarray[i];
                    Tarray[i] = temp;

                }

            }

            return Tarray;
            }

        public static double[,] AveragingUserMatrix(double[,] UserMatrix)
        {
           // int [,] arr = new int[2,5] { { 1, 0, 1, 2, 3 }, { 1, 2, 3, 1, 1 } };
            double[,] output = new double [1,UserMatrix.GetUpperBound(1)+1];


            for (int k = 0; k <= output.GetUpperBound(0); k++)
            {
                for (int j = 0; j <= UserMatrix.GetUpperBound(1); j++)
                {
                    double sum = 0;
                    for (int i = 0; i <= UserMatrix.GetUpperBound(0); i++)
                    {
                        sum = sum + UserMatrix[i,j];
                    }

                    double Average = sum / (UserMatrix.GetUpperBound(0) + 1);
                    output[k, j] = Average;
                }

            }
            
            //for (int i = 0; i <= (output.GetUpperBound(0)); i++)
            //{
            //    for (int j = 0; j <= (output.GetUpperBound(1)); j++)
            //    {
            //        Console.WriteLine(output[i, j]);
            //    }
            //}
             return output;
        }
        


        static void Main(string[] args)
            {
                Utility handle = new Utility();

                //Path of anime movies dataset
                string moviesfilepath = @"D:\Courses\Winter2018\WebServices\Project\Updated1\anime-recommender-db-master\out\Processed\database.csv";
                string[,] BigMatrix = handle.LoadCsv(moviesfilepath);

                //Path of user profile data
                string userdatapath = @"D:\Courses\Winter2018\WebServices\Project\Updated1\anime-recommender-db-master\out\Processed\UserProfile.csv";
                string[,] UserMatrix = handle.LoadCsv(userdatapath);

            double[,] BigMatrix1 = handle.SliceArray(BigMatrix);
            double[,] UserMatrix1 = handle.SliceArray(UserMatrix);
            
            double[,] UserMatrix2 = AveragingUserMatrix(UserMatrix1);

            //To find similarity between user profile and movie database
            double[,] DParray = DotProduct(UserMatrix2, BigMatrix1);
            Tuple<double, int>[] Sortedarray = ArraySorting(DParray);

            int greatestfives = 5;
            int[] order = new int[greatestfives];


            for (int j = 0; j < greatestfives; j++)
            {
                order[j] = (Sortedarray[j].Item2);
            }

            for (int i = 0; i < greatestfives; i++)
            {
                Console.WriteLine(BigMatrix[(order[i] + 1), 0]);
            }
        }
        }
    }

