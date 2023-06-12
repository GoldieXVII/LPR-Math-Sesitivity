using Matric_Prelims;

internal class Program
{
    private static void Main(string[] args)
    {
        //need to do error handling when entering data

        GettingData getData = new GettingData();
        MatrixMaths matrixMaths = new MatrixMaths();

        /*Console.WriteLine("Enter Matrix Size");
        int row=0;
        int col=0;
        string temp;
        try
        {
            temp = Console.ReadLine();
            string temp1 = temp.Substring(0 , temp.LastIndexOf('x'));
            string temp2 = temp.Substring(temp.LastIndexOf('x')+1);
            row = Convert.ToInt32(temp1);
            col = Convert.ToInt32(temp2);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        double [,] matrix = SetMatrix(row, col);
        DisplayMatrix(matrix);
        
        int n = matrix.GetLength(0);
        double det = matrixMaths.GetDeterminant(matrix, n);
        Console.WriteLine("Determinant: " + det);
        
        double[,] cofactorMatrix = matrixMaths.GetCofactorMatrix(matrix, n);
        Console.WriteLine("Cofactor Matrix:");
        DisplayMatrix(cofactorMatrix);
        
        Console.WriteLine("Transposed Matrix:");
        double[,] transposedMatrix = matrixMaths.TransposeMatrix(cofactorMatrix);
        DisplayMatrix(transposedMatrix);

        if (det == 0)
        {
            Console.WriteLine("Inverse does not exist. Matrix is not invertible.");
        }
        else
        {
            double[,] inverseMatrix = matrixMaths.GetInverseMatrix(transposedMatrix, det);
            Console.WriteLine("Inverse Matrix:");
            DisplayMatrix(inverseMatrix);
        }*/
        //commented out to test getting Xbv Values

        Console.WriteLine("How many decision variables do you have?");
        int desVar = Convert.ToInt32(Console.ReadLine()) * 2; //row num times by two for s and e var
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


        //wont be able to easily solve the matrix with dual phase in C#

    }

    private static double[,] SetMatrix(int row, int col)
    {
        Console.WriteLine("Values entered left to right, top to bottom");
        double[,] matrix = new double[row, col];
        for (int m = 0; m < row; m++)
        {
            for (int j = 0; j < col; j++)
            {
                Console.Write("Enter value for: " + m + 1 + " " + j + 1 + "\n");
                double value = Convert.ToDouble(Console.ReadLine());
                matrix[m, j] = value;
            }
        }
        return matrix;
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