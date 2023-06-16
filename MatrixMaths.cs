using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matric_Prelims
{
    internal class MatrixMaths
    {

        public double[,] GetInverseMatrix(double[,] matrix, double determinant)
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


        public double GetDeterminant(double[,] matrix, int k)
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

        public double[,] GetSubmatrix(double[,] matrix, int rowToRemove, int colToRemove, int n)
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

        public double[,] GetCofactorMatrix(double[,] matrix, int n)
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

        public double[,] TransposeMatrix(double[,] matrix)
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

        public double[] GetCBVB1(double[] CBv, double[,] Inversematrix)
        {
            int vectorSize = CBv.Length;
            int numCols = Inversematrix.GetLength(1);

            //if (vectorSize != Inversematrix.GetLength(0))
            //{
            //    throw new ArgumentException("The length of the vector must be equal to the number of rows in the matrix.");
            //}

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

            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + "\t");
            }

            return result;

        }

    }
}
