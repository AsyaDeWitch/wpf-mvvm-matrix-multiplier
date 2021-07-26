using MatrixMultiplier.MVVM.ViewModels;
using System.Windows;

namespace MatrixMultiplier.MVVM.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MatrixViewModel(this);
        }
    }
}
