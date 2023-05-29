using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matric_Prelims
{
    internal class GettingData
    {

        public string[] GetHeadings(int desVar, int consNum)
        {
            string[] headingArr = new string[consNum];
            int tempColCount = 0;
            int tempRowCount = 0;
            string temp;
            for (int i = 0; i < desVar; i++) //getting heading values
            {
                headingArr[i] = "x" + (i + 1);
            }
            for (int i = desVar; i < consNum; i++)
            {
                Console.WriteLine("Enter sign for constraint: " + i);
                temp = Console.ReadLine();
                if (temp == "<=")
                {
                    headingArr[i] = "s" + (i + 1);
                }
                else if (temp == ">=")
                {
                    headingArr[i] = "e" + (i + 1);
                }
                else if (temp == "=")
                {
                    headingArr[i] = "e" + (i + 1);
                    tempColCount++;
                    tempRowCount++;
                }
            }
            return headingArr;
        }

        public double[,] SetModel(int desVar, int consNum)
        {
            int tempColCount = 0;
            int tempRowCount = 0;
            desVar += tempColCount;
            consNum += tempRowCount;

            double[,] conicalArr = new double[desVar, consNum];

            Console.WriteLine("What are the coeffients for the objective function?");
            for (int i = 0; i <= desVar; i++)
            {
                Console.WriteLine("Enter coeffient for variable: " + i);
                conicalArr[i - 1, 0] = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("What are the coeffients for the constraints?");
            for (int i = 1; i <= desVar; i++)
            {
                for (int j = 0; j < consNum; j++)
                {
                    Console.WriteLine("Enter coeffient for constraint variable: " + i + " " + j);
                    conicalArr[i, j] = Convert.ToInt32(Console.ReadLine());
                }
            }

            return conicalArr;
        }

        public string[] getXbvHeadings()
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

    }
}
