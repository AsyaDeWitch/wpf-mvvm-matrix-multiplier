using MatrixMultiplier.MVVM.Commands;
using MatrixMultiplier.MVVM.Models;
using MatrixMultiplier.MVVM.Views;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatrixMultiplier.MVVM.ViewModels
{
    internal class MatrixViewModel
    {
        private Matrix _matrix1;
        private Matrix _matrix2;
        private readonly MainWindow _mainWindow;

        public MatrixViewModel(MainWindow mainWindow)
        {
            Matrix1 = new Matrix(0, 0, new double[0, 0]);
            Matrix2 = new Matrix(0, 0, new double[0, 0]);
            _mainWindow = mainWindow;
            OpenMatrix1FileCommand = new OpenMatrixFileCommand(this, 1);
            OpenMatrix2FileCommand = new OpenMatrixFileCommand(this, 2);
            MultiplyMatricesCommand = new MultiplyMatricesCommand(this);
        }

        public Matrix Matrix1
        {
            get
            {
                return _matrix1;
            }
            set
            {
                _matrix1 = value;
            }
        }

        public Matrix Matrix2
        {
            get
            {
                return _matrix2;
            }
            set
            {
                _matrix2 = value;
            }
        }

        public ICommand MultiplyMatricesCommand
        {
            get;
            private set;
        }

        public ICommand OpenMatrix1FileCommand
        {
            get;
            private set;
        }

        public ICommand OpenMatrix2FileCommand
        {
            get;
            private set;
        }

        public bool CanMultiplyMatrices 
        {
            get
            {
                return (Matrix1.RowsNumber != 0 && Matrix1.ColumnsNumber != 0) && (Matrix2.RowsNumber != 0 && Matrix2.ColumnsNumber != 0);
            } 
        }

        public bool CanOpenMatrixFile
        {
            get
            {
                return true;
            }
        }

        public void OpenMatrixFile(int matrixNum)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text and CSV files (*.txt)|*.txt|(*.csv)|*.csv";
            string filePath;
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                int rowsNum, columnsNum;
                double[,] matrix;
                try
                {
                    using (StreamReader reader = new StreamReader(@filePath))
                    {
                        string[] lines = reader.ReadToEnd().Split('\n');
                        rowsNum = lines.Length;

                        columnsNum = lines[0].Split(',').Select(x => double.Parse(x)).ToArray<double>().Length;

                        matrix = new double[rowsNum, columnsNum];

                        for (int i = 0; i < rowsNum; i++)
                        {
                            double[] doubles = lines[i].Split(',').Select(x => double.Parse(x, CultureInfo.CreateSpecificCulture("en-US"))).ToArray<double>();
                            for (int j = 0; j < columnsNum; j++)
                            {
                                matrix[i, j] = doubles[j];
                            }
                        }
                    }
                    if(matrixNum == 1)
                    {
                        Matrix1.UpdateMatrix(matrix, filePath);
                    }
                    else if (matrixNum == 2)
                    {
                        Matrix2.UpdateMatrix(matrix, filePath);
                    }
                }
                catch (FormatException e)
                {
                    if (matrixNum == 1)
                    {
                        Matrix1.UpdateMatrix(new double[0, 0], "");
                    }
                    else if (matrixNum == 2)
                    {
                        Matrix2.UpdateMatrix(new double[0, 0], "");
                    }
                    MessageBox.Show("File doesn't support CSV format", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public Matrix MultiplyMatrices()
        {
            MatricesMultiplier matricesMultiplier = new MatricesMultiplier();
            return matricesMultiplier.GetResultMatrix(Matrix1, Matrix2);
        }

        public void DrawResultMatrix(Matrix resultMatrix)
        {
            _mainWindow.DataGridMatrixResult.Children.Clear();

            if (resultMatrix != null)
            {
                for (int i = 0; i < resultMatrix.ColumnsNumber; i++)
                {
                    ColumnDefinition columnDefinition = new ColumnDefinition();
                    _mainWindow.DataGridMatrixResult.ColumnDefinitions.Add(columnDefinition);

                    for (int j = 0; j < resultMatrix.RowsNumber; j++)
                    {
                        RowDefinition rowDefinition = new RowDefinition();
                        _mainWindow.DataGridMatrixResult.RowDefinitions.Add(rowDefinition);
                    }
                }

                for (int i = 0; i < resultMatrix.RowsNumber; i++)
                {
                    for (int j = 0; j < resultMatrix.ColumnsNumber; j++)
                    {
                        TextBlock matrixItem = new TextBlock();
                        matrixItem.Text = resultMatrix[i, j].ToString();
                        matrixItem.FontSize = (double)new FontSizeConverter().ConvertFrom("15pt");
                        matrixItem.Foreground = System.Windows.Media.Brushes.DarkTurquoise;
                        matrixItem.FontFamily = new System.Windows.Media.FontFamily("Technical Italic, Comic Sans MS, Arial");
                        matrixItem.Margin = new Thickness(5);

                        Grid.SetRow(matrixItem, i);
                        Grid.SetColumn(matrixItem, j);
                        _mainWindow.DataGridMatrixResult.Children.Add(matrixItem);
                    }
                }
            }
        }
    }
}
