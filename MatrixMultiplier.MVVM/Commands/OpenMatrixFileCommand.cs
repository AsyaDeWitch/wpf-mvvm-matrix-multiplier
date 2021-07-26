using MatrixMultiplier.MVVM.ViewModels;
using System;
using System.Windows.Input;

namespace MatrixMultiplier.MVVM.Commands
{
    internal class OpenMatrixFileCommand : ICommand
    {
        private readonly MatrixViewModel _viewModel;
        private readonly int _matrixNum;

        public OpenMatrixFileCommand(MatrixViewModel viewModel, int matrixNum)
        {
            _viewModel = viewModel;
            _matrixNum = matrixNum;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanOpenMatrixFile;
        }

        public void Execute(object parameter)
        {
            _viewModel.OpenMatrixFile(_matrixNum);
        }
    }
}
