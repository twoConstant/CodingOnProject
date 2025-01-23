using System.Reflection.PortableExecutable;
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
using Project.Models;
using Project.Repository;
using Project.ViewModels;

namespace Project.Views
{
    public partial class SensorDataPage : Page
    {
        private readonly SensorDataViewModel _viewModel;
        private readonly int _machineId;
        private readonly string _sensorValue;


        // 기본 생성자: Navigate나 XAML Designer를 위한 설정
        public SensorDataPage()
        {
            InitializeComponent();
            // 기본 생성자는 ViewModel 초기화나 로직을 처리하지 않음
        }

        public SensorDataPage(int machine_id, string sensorValue)
        {
            InitializeComponent();

            // Repository 및 ViewModel 초기화
            ISensorDataRepository repository = new Sensor_dataRepository();
            SensorDataViewModel viewModel = new SensorDataViewModel(repository);
            _viewModel = viewModel;
            _machineId = machine_id;
            _sensorValue = sensorValue;
            Console.WriteLine(_machineId);


            // ViewModel을 DataContext에 바인딩
            DataContext = _viewModel;

            // 차트 바인딩 설정
            SensorChart.Series = _viewModel.YaxisValues;
        }

        private void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            string startDate = "";
            string endDate = "";

            // 입력값 검증
            if (DateTime.TryParse(StartDateInput.Text, out DateTime parsedStartDate) && DateTime.TryParse(EndDateInput.Text, out DateTime parsedEndDate))
            {
                startDate = parsedStartDate.ToString();
                endDate = parsedEndDate.ToString();

                _viewModel.LoadSensorDataByMachineID(_machineId, _sensorValue, startDate, endDate);
                //MessageBox.Show(_viewModel.Sensor_data.ct4.ToString());

            }
            else
            {
                MessageBox.Show("Please enter a valid Machine ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListOnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            string startDate = "";
            string endDate = "";

            // 입력값 검증
            if (DateTime.TryParse(StartDateInput.Text, out DateTime parsedStartDate) && DateTime.TryParse(EndDateInput.Text, out DateTime parsedEndDate))
            {
                startDate = parsedStartDate.ToString();
                endDate = parsedEndDate.ToString();

                _viewModel.LoadSensorDataListByMachineID(_machineId, _sensorValue, startDate, endDate);
                //MessageBox.Show(_viewModel.Sensor_data.ct4.ToString());

            }
            else
            {
                MessageBox.Show("Please enter a valid Machine ID.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Console.WriteLine("ListOnLoadButtonClick 완료");
        }
    }
}