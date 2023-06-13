using Matric_Prelims;

internal class Program
{
    private static void Main(string[] args)
    {
        //need to do error handling when entering data

        GettingData getData = new GettingData();
        MatrixMaths matrixMaths = new MatrixMaths();
        
        Console.WriteLine("How many decision variables do you have?");
        int desVar = Convert.ToInt32(Console.ReadLine()) * 2 + 1; //row num times by two for s and e var
        Console.WriteLine("How many constraits are there");
        int consNum = Convert.ToInt32(Console.ReadLine())+1; //column num
        
        //original lp
        double[,] modelArr = getData.SetModel(desVar, consNum);
        string[] headings = getData.GetHeadings(desVar, consNum);

        //Entering XBv value headings
        getData.DisplayHeadings(headings);
        DisplayMatrix(modelArr);;
        string[] xbv = getData.GetXbvHeadings();
        double[,] bVal = getData.GetBValues(headings, xbv, modelArr, consNum);
        Console.WriteLine();
        Console.WriteLine();
        DisplayMatrix(bVal);
        
        int n = bVal.GetLength(0);

        double[,] cofactorMatrix = matrixMaths.GetCofactorMatrix(bVal, n);
        Console.WriteLine("Cofactor Matrix:");
        DisplayMatrix(cofactorMatrix);

        Console.WriteLine("Transposed Matrix:");
        double[,] transposedMatrix = matrixMaths.TransposeMatrix(cofactorMatrix);
        DisplayMatrix(transposedMatrix);

        double det = matrixMaths.GetDeterminant(bVal, n);
        double[,] inverseMatrix = null;
        Console.WriteLine("Determinant: " + det);
        if (det == 0)
        {
            Console.WriteLine("Inverse does not exist. Matrix is not invertible.");
            Environment.Exit(0);
        }
        else
        {
            inverseMatrix = matrixMaths.GetInverseMatrix(transposedMatrix, det);
            Console.WriteLine("Inverse Matrix:");
            DisplayMatrix(inverseMatrix);
        }

        double[] cbv = getData.getCBv(headings, xbv, modelArr);
        double[] cbvb1 = getData.GetCBVB1(cbv, inverseMatrix);


    }

    private static void DisplayMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}