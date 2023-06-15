using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Matric_Prelims
{
    internal class GettingData
    {

        public double[,] SetModel(int desVar, int consNum)
        {

            double[,] conicalArr = new double[consNum, desVar];

            Console.WriteLine("What are the coeffients for the objective function?");
            for (int i = 0; i < desVar; i++)
            {
                if(i != desVar - 1)
                {
                    Console.WriteLine("Enter coeffient for variable: " + (i + 1));
                    conicalArr[0, i] = Convert.ToDouble(Console.ReadLine());
                }else
                {
                    Console.WriteLine("Enter z value: ");
                    conicalArr[0, i] = Convert.ToDouble(Console.ReadLine());
                }
                
            }

            Console.WriteLine("What are the coeffients for the constraints?");
            for (int m = 1; m < consNum; m++)
            {
                for (int j = 0; j < desVar; j++)
                {
                    if(j != desVar - 1)
                    {
                        Console.WriteLine("Enter coeffient for constraint number " + (m) + ", Decision Variable " + (j + 1));
                        conicalArr[m, j] = Convert.ToDouble(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("Enter coeffient of Z value for constraint number " + (m));
                        conicalArr[m, j] = Convert.ToDouble(Console.ReadLine());
                    }
                    
                }
            }

            return conicalArr;
        }

        public string[] GetHeadings(int desVar, int consNum)
        {
            string[] headingArr = new string[desVar];

            for (int i = 0; i < desVar-1; i++)
            {
                Console.WriteLine("Enter Variable Heading for: " + i);
                headingArr[i] = Console.ReadLine();
            }

            headingArr[headingArr.Length-1] = "z";

            return headingArr;
        }

        public double [,] GetbValues (int consNum)
        {
            double[,] bVal = new double[consNum, 1];
            
            for (int i = 0; i < consNum; i++)
            {
                bVal[i, 0] = Convert.ToDouble(Console.ReadLine());
            }

            return bVal;
        }

        public void DisplayHeadings(string[] heading)
        {
            
            int temp = heading.Length;

            for (int i = 0; i < temp; i++)
            {
                Console.Write(heading[i] + "\t");
            }
            Console.WriteLine();
        }

        public string[] GetXbvHeadings(string[] headings)
        {
            Console.WriteLine("Enter how many basic variables there are");

            string[] xbvValue = null;
            bool flagOuter = true;
            
            while (flagOuter == true)
            {
                try
                {
                    int numXbvValues = Convert.ToInt32(Console.ReadLine());
                    xbvValue = new string[numXbvValues];

                    for (int i = 0; i < numXbvValues; i++)
                    {
                        string temp;
                        bool found;

                        do
                        {
                            Console.WriteLine("Enter the heading of the Basic Variable");
                            temp = Console.ReadLine();

                            found = ArrayContainsValue(headings, temp);

                            if(!found)
                            {
                                Console.WriteLine("Enter a heading that exisits");
                            }    
                        } while (!found);
                        
                        xbvValue[i] = temp;
                        
                        if (i == numXbvValues - 1)
                        {
                            flagOuter = false;
                        }

                    }                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a proper Value");
                }
            }
            return xbvValue;
        }

        private static bool ArrayContainsValue(string[] array, string value)
        {
            foreach (string element in array)
            {
                if (element.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        private int[] gettingIndicies(string[] headings, string [] headingLook)
        {
            int length = headingLook.Length;
            int[] valPlace = new int[length];
            int k = 0;

            for (int i = 0; i < length; i++)
            {
                string currentVal = headingLook[i];

                for (int j = 0; j < headings.Length; j++)
                {
                    if (headings[j] == currentVal)
                    {
                        valPlace[k] = j;
                        k++;
                    }
                }
            }

            return valPlace;
            
        }

        public double[,] GetBValues(string[] headings, string [] headingLook, double[,] orArr, int consNum)
        {
         
            int [] indicies = gettingIndicies(headings, headingLook);
            int rows = consNum-1;
            int columns = indicies.Length;

            double[,] bVal = new double[rows, columns];

            for (int c = 0; c < columns; c++)
            {
                int columnIndex = indicies[c];
                for (int r = 0; r < rows; r++)
                {
                    bVal[r, c] = orArr[r+1, columnIndex];
                }
            }

            return bVal;

        }

        public double[] getCBv (string[] headings, string[] headingLook, double[,] orArr)
        {
            int[] indicies = gettingIndicies(headings, headingLook);
            int columns = indicies.Length;

            double[] cBv = new double[indicies.Length];

            for(int i = 0; i < columns; i++)
            {
                int columnIndex = indicies[i];
                cBv[i] = orArr[0, columnIndex];
            }

            Console.WriteLine("CBv Value:");
            for(int c = 0; c < columns; c++)
            {
                Console.Write(cBv[c] + "\t");
            }

            return cBv;
        }

        public double[] GetCBVB1 (double[] CBv, double[,] Inversematrix)
        {
            int vectorSize = CBv.Length;
            int numCols = Inversematrix.GetLength(1);

            if (vectorSize != Inversematrix.GetLength(0))
            {
                throw new ArgumentException("The length of the vector must be equal to the number of rows in the matrix.");
            }

            double[] result = new double[numCols];

            for (int j = 0; j < numCols; j++)
            {
                double sum = 0;
                for (int i = 0; i < vectorSize; i++)
                {
                    sum += CBv[i] * Inversematrix[i, j];
                }
                result[j] = sum;
            }

            for(int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + "\t");
            }

            return result;

        }
    }
}
