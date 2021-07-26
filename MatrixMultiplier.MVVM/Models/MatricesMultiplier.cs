using System.Windows;

namespace MatrixMultiplier.MVVM.Models
{
    public class MatricesMultiplier
    {
        public Matrix GetResultMatrix(Matrix matrix1, Matrix matrix2)
        {
            if (matrix1.ColumnsNumber != matrix2.RowsNumber)
            {
                MessageBox.Show("1st Matrix columns number don't equal 2nd Matrix rows number. Please choose other Matrices ", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            double[,] resultMatrix = new double[matrix1.RowsNumber, matrix2.ColumnsNumber];

            for (int i = 0; i < matrix1.RowsNumber; i++)
            {
                for (int j = 0; j < matrix2.ColumnsNumber; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matrix1.ColumnsNumber; k++)
                    {
                        temp += matrix1[i, k] * matrix2[k, j];
                    }
                    resultMatrix[i, j] = temp;
                }
            }
            return new Matrix(resultMatrix);
        }
    }
}
