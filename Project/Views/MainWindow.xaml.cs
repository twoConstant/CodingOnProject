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
using Project.Models;
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
            // SensorDataViewModel 인스턴스 생성
            SensorDataViewModel sensorDataViewModel = new SensorDataViewModel();

            // Repository와 ViewModel 초기화
            IMachineCurrStateRepository machineCurrStateRepository = new MachineCurrStateRepository();
            IMachineRepository MachineRepository = new MachineRepository();
            _viewModel = new MainViewModel(machineCurrStateRepository, MachineRepository, sensorDataViewModel);

            // ViewModel을 DataContext에 바인딩
            DataContext = _viewModel;
            Console.WriteLine("DataContext is set to MainViewModel");

        }

        private void OnDeviceIdClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is MachineCurrState selectedMachine)
            {
                // 클릭된 설비명의 MachineId 가져오기
                int machineId = selectedMachine.MachineId;
                MessageBox.Show($"Machine ID: {machineId} 클릭됨.", "Machine Info", MessageBoxButton.OK, MessageBoxImage.Information);

                // ViewModel 메서드 호출
                _viewModel.MachineViewModel.LoadMachineById(machineId);
            }
        }
    }
}