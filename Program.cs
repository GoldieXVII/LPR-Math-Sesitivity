﻿using System.Security.Cryptography;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter Matrix Size");
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
        double det = GetDeterminant(matrix, n);
        Console.WriteLine("Determinant: " + det);
        
        double[,] cofactorMatrix = GetCofactorMatrix(matrix, n);
        Console.WriteLine("Cofactor Matrix:");
        DisplayMatrix(cofactorMatrix);
        
        Console.WriteLine("Transposed Matrix:");
        double[,] transposedMatrix = TransposeMatrix(cofactorMatrix);
        DisplayMatrix(transposedMatrix);

        if (det == 0)
        {
            Console.WriteLine("Inverse does not exist. Matrix is not invertible.");
        }
        else
        {
            double[,] inverseMatrix = GetInverseMatrix(transposedMatrix, det);
            Console.WriteLine("Inverse Matrix:");
            DisplayMatrix(inverseMatrix);
        }

        Console.WriteLine("How many decision variables do you have?");
        int desVar = Convert.ToInt32(Console.ReadLine());
        int [] coVarArr = new int[desVar];
        Console.WriteLine("What are the coeffients for the objective function?");
        for(int i = 1; i <= desVar; i++)
        {
            Console.WriteLine("Enter coeffient for variable: " + i);
            coVarArr[i-1] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("How many constraits are there");
        int consNum = Convert.ToInt32(Console.ReadLine());
        int[,] tempArr = new int[desVar, consNum];
        for(int i = 1;i <= desVar; i++)
        {
            for(int j = 1;j <= consNum; j++)
            {
                Console.WriteLine("Enter coeffient for constraint variable: " + i+" "+j);
                tempArr[desVar,consNum] = Convert.ToInt32(Console.ReadLine());
            }
        }
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

    private static double[,] GetInverseMatrix(double[,] matrix, double determinant)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] inverseMatrix = new double[rows, cols];
        double det = 1 / determinant;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                inverseMatrix[i, j] = det * matrix[i, j];
            }
        }

        return inverseMatrix;
    }
    
    
    private static double GetDeterminant(double[,] matrix, int k)
    {
        if (k == 1)
        {
            return matrix[0, 0];
        }
        else if (k == 2)
        {
            return (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
        }
        else
        {
            double det = 0;
            for (int i = 0; i < k; i++)
            {
                double[,] submatrix = GetSubmatrix(matrix, 0, i, k);
                double minor = GetDeterminant(submatrix, k - 1);
                double cofactor = ((i % 2) == 0) ? minor : -minor;
                det += matrix[0, i] * cofactor;
            }
            return det;
        }
    }

    private static double[,] GetSubmatrix(double[,] matrix, int rowToRemove, int colToRemove, int n)
    {
        double[,] submatrix = new double[n - 1, n - 1];
        int rowIndex = 0;
        int colIndex = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == rowToRemove)
            {
                continue;
            }
            colIndex = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == colToRemove)
                {
                    continue;
                }
                submatrix[rowIndex, colIndex] = matrix[i, j];
                colIndex++;
            }
            rowIndex++;
        }
        return submatrix;
    }

    private static double[,] GetCofactorMatrix(double[,] matrix, int n)
    {
        double[,] cofactorMatrix = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double[,] submatrix = GetSubmatrix(matrix, i, j, n);
                double minor = GetDeterminant(submatrix, n - 1);
                double cofactor = ((i + j) % 2 == 0) ? minor : -minor;
                cofactorMatrix[i, j] = cofactor;
            }
        }
        return cofactorMatrix;
    }

    private static double[,] TransposeMatrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] transposeMatrix = new double[cols, rows];
        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                transposeMatrix[i, j] = matrix[j, i];
            }
        }
        return transposeMatrix;
    }

}