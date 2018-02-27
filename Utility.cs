using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRecommenderWS
{
    class Utility
    {
        //To load csv file from a folder path and converts to str array
        public string[,] LoadCsv(string moviesfilepath)
        {

            // Get the file's text.
            string whole_file = System.IO.File.ReadAllText(moviesfilepath);

            // Split into lines.
            whole_file = whole_file.Replace('\n', '\r');
            string[] lines = whole_file.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are.
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(',').Length;

            // Allocate the data array.
            string[,] values = new string[num_rows, num_cols];

            // Load the array.
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(',');
                for (int c = 0; c < num_cols; c++)
                {
                    values[r, c] = line_r[c];
                }
            }

            // Return the values.
            return values;
        }


        public double[,] SliceArray(string[,] V)
        {
            int row = V.GetUpperBound(0);
            int column = V.GetUpperBound(1);

            double[,] customarray = new double[row, column];
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= column; j++)
                {
                    //Console.WriteLine(V[i, j]);
                    customarray[i - 1, j - 1] = double.Parse(V[i, j]);
                }
            }
            //Console.WriteLine(customarray[0, 0]);
            return customarray;
        }
    }
}
