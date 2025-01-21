using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using Project.Repository;
using Project.ViewModels;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Repository와 ViewModel 초기화
            IMachineCurrStateRepository machineCurrStateRepository = new MachineCurrStateRepository();
            _viewModel = new MainViewModel(machineCurrStateRepository);

            // ViewModel을 DataContext에 바인딩
            DataContext = _viewModel;
            Console.WriteLine("DataContext is set to MainViewModel");

        }
    }
}