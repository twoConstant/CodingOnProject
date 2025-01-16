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

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChartValues<float> Values { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Values = new ChartValues<float> { 1, 3, 5, 7, 9, 6, 2, 3, 5, 9, 9, 10, 3, 2 };
            DataContext = this;
        }
    }
}