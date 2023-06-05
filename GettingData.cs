using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Enter coeffient for variable: " + i);
                conicalArr[0, i] = Convert.ToDouble(Console.ReadLine());
            }

            Console.WriteLine("What are the coeffients for the constraints?");
            for (int m = 1; m < consNum; m++)
            {
                for (int j = 0; j < desVar; j++)
                {
                    Console.WriteLine("Enter coeffient for constraint variable: " + m + " " + j);
                    conicalArr[m, j] = Convert.ToDouble(Console.ReadLine());
                }
            }

            return conicalArr;
        }

        public string[] GetHeadings(int desVar, int consNum)
        {
            string[] headingArr = new string[desVar];

            for (int i = 0; i < desVar; i++)
            {
                Console.WriteLine("Enter Variable Heading for: " + i);
                headingArr[i] = Console.ReadLine();
            }

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

        public string[] GetXbvHeadings()
        {
            Console.WriteLine("Enter how many basic variables there are");
            int numXbvValues = Convert.ToInt32(Console.ReadLine());
            string[] xbvValue = new string[numXbvValues];

            for (int i = 0; i < numXbvValues; i++)
            {
                Console.WriteLine("Enter the heading of the Basic Variable");
                xbvValue[i] = Console.ReadLine();
            }

            return xbvValue;
        }

        public double[,] GetBValues(int desVar, int consNum, string[] headings, string [] headingLook, double[,] orArr)
        {
            

            int length = headingLook.Length;
            int [] valPlace =new int[length];
            int k = 0;

            for (int i = 0; i < length; i++)
            {
                string currentVal = headingLook[i];

                for (int j = 0; j < headings.Length; j++)
                {
                    if (headings[j]==currentVal)
                    {
                        valPlace[k] = j;
                        k++;
                    }
                }
            }

            double[,] bVal = new double[consNum, length];
            k = 0;
            double temp = 0;

            while (k < length)
            {
                for (int m = 0; m < valPlace.Length; m++)
                {
                    for (int i = 0; i < consNum; i++)
                    {
                        if (valPlace[m] == i)
                        {
                            for (int j = 0; j < desVar; j++)
                            {
                                temp = orArr[i, j];
                                bVal[k, i] = temp;
                            }
                            k++;
                        }
                    }
                }
            }

            //for(int m = 0; m < valPlace.Length; m++)
            //{
            //    for (int i = 0; i < consNum; i++)
            //    {
            //        if (valPlace[m] == i)
            //        {
            //            for (int j = 0; j < desVar; j++)
            //            {
            //                temp = orArr[i, j];
            //                bVal[k, i] = temp;
            //            }
            //            k++;
            //        }
            //    }
            //}
            return bVal;
        }
    }
}
