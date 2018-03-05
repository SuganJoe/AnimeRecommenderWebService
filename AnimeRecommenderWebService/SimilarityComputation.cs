using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommenderWebService
{
    class SimilarityComputation
    {
        //To find dotproduct of user array and movie array
        public static double[,] DotProduct(double[,] User, double[,] Data)
        {
            int Data_row = Data.GetUpperBound(0);
            int Data_column = Data.GetUpperBound(1);


            //Transforming the movie or data array
            double[,] transformarray = new double[Data_column + 1, Data_row + 1];

            for (int i = 0; i <= Data_row; i++)
            {
                for (int j = 0; j <= Data_column; j++)
                {
                    transformarray[j, i] = Data[i, j];

                }
            }

            int user_row = User.GetUpperBound(0) + 1;
            int tf_column = transformarray.GetUpperBound(1) + 1;

            //Initializing DParray - Dotproduct of user array and Transform array
            double[,] DParray = new double[user_row, tf_column];
            int DP_row = DParray.GetUpperBound(0) + 1;

            //Finding dot product
            for (int k = 0; k < DP_row; k++)
            {
                for (int j = 0; j < tf_column; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < (Data_column + 1); i++)
                    {
                        sum = sum + User[k, i] * transformarray[i, j];

                    }

                    DParray[k, j] = sum;
                }
            }

            return DParray;
        }

        //Storing the dotproduct output and the index in a Tuple before sorting
        public static Tuple<double, int>[] ArraySorting(double[,] DParray)
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


            //Sorting the tuple array
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

        //To find average of the user profile- converting number of user matrix rows to a single row 
        public static double[,] AveragingUserMatrix(double[,] UserMatrix)
        {
            double[,] output = new double[1, UserMatrix.GetUpperBound(1) + 1];


            for (int k = 0; k <= output.GetUpperBound(0); k++)
            {
                for (int j = 0; j <= UserMatrix.GetUpperBound(1); j++)
                {
                    double sum = 0;
                    for (int i = 0; i <= UserMatrix.GetUpperBound(0); i++)
                    {
                        sum = sum + UserMatrix[i, j];
                    }

                    double Average = sum / (UserMatrix.GetUpperBound(0) + 1);
                    output[k, j] = Average;
                }

            }
            return output;
        }


    }
}

