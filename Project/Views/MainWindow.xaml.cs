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
        private readonly MachineViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Repository와 ViewModel 초기화
            IMachineRepository repository = new MachineRepository();
            MachineViewModel viewModel = new MachineViewModel(repository);
            _viewModel = viewModel;

            // ViewModel을 DataContext에 바인딩
            DataContext = _viewModel;            
        }

        private void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            int machineId = 1;
            string SensorValue = "ntc";

            // 입력값 검증
            if (int.TryParse(MachineIdInput.Text, out machineId))
            {
                _viewModel.LoadMachineById(machineId);
            }
            else
            {
                MessageBox.Show("Please enter a valid Machine ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnOpenSensorDataPageClick(object sender, RoutedEventArgs e)
        {
            // Machine ID를 설정 (예: 12345)
            int machineId = 1;
            string SensorValue = "ntc";

            // SensorDataPage 생성 및 Navigation
            SensorDataPage sensorDataPage = new SensorDataPage(machineId, SensorValue);
            MainFrame.Navigate(sensorDataPage);
        }
    }
}