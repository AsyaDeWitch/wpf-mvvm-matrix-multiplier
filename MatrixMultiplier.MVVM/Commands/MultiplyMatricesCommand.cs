using MatrixMultiplier.MVVM.Models;
using MatrixMultiplier.MVVM.ViewModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MatrixMultiplier.MVVM.Commands
{
    internal class MultiplyMatricesCommand : ICommand
    {
        private readonly MatrixViewModel _viewModel;

        public MultiplyMatricesCommand(MatrixViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _viewModel.CanMultiplyMatrices;
        }

        public async void Execute(object parameter)
        {
            Matrix resultMatrix = await Task.Run(() => _viewModel.MultiplyMatrices());
            _viewModel.DrawResultMatrix(resultMatrix);
        }
    }
}
