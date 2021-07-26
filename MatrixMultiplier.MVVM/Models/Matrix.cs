using System.ComponentModel;

namespace MatrixMultiplier.MVVM.Models
{
    public class Matrix : INotifyPropertyChanged
    {
        private int _rowsNum;
        private int _columnsNum;
        private double[,] _matrix;
        private string _sourceFile;

        public Matrix(int rowsNumber, int columnNumber, double[,] matrix)
        {
            RowsNumber = rowsNumber;
            ColumnsNumber = columnNumber;
            _matrix = matrix;
        }

        public Matrix (double[,] matrix)
        {
            RowsNumber = matrix.GetLength(0);
            ColumnsNumber = matrix.GetLength(1);
            _matrix = matrix;
        }

        public Matrix(double[,] matrix, string sourceFile)
        {
            RowsNumber = matrix.GetLength(0);
            ColumnsNumber = matrix.GetLength(1);
            _matrix = matrix;
            _sourceFile = sourceFile;
        }

        public Matrix(int rowsNumber, int columnNumber, double[,] matrix, string sourceFile)
        {
            RowsNumber = rowsNumber;
            ColumnsNumber = columnNumber;
            _matrix = matrix;
            _sourceFile = sourceFile;
        }

        public int RowsNumber
        {
            get
            {
                return _rowsNum;
            }
            private set
            {
                _rowsNum = value;
                OnPropertyChanged(nameof(RowsNumber));
            }
        }

        public int ColumnsNumber
        {
            get
            {
                return _columnsNum;
            }
            private set
            {
                _columnsNum = value;
                OnPropertyChanged(nameof(ColumnsNumber));
            }
        }

        public string SourceFile
        {
            get
            {
                return _sourceFile;
            }
            private set
            {
                _sourceFile = value;
                OnPropertyChanged(nameof(SourceFile));
            }
        }

        public double this[int index1, int index2]
        {
            get
            {
                return _matrix[index1, index2];
            }
            set
            {
                _matrix[index1, index2] = value;
            }
        }

        public void UpdateMatrix(double[,] matrix)
        {
            _matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = matrix[i, j];
                }
            }
            RowsNumber = matrix.GetLength(0);
            ColumnsNumber = matrix.GetLength(1);
        }

        public void UpdateMatrix(double[,] matrix, string filePath)
        {
            _matrix = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    _matrix[i, j] = matrix[i, j];
                }
            }
            RowsNumber = matrix.GetLength(0);
            ColumnsNumber = matrix.GetLength(1);
            SourceFile = filePath;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
