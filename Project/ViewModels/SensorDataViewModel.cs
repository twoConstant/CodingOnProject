using System.Collections.ObjectModel;
using System.ComponentModel;
using Project.Models;
using Project.Repository;
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Project.ViewModels
{
    public class SensorDataViewModel : INotifyPropertyChanged
    {
        private readonly SensorDataRepository _repository;

        public SensorDataViewModel()
        {
            _repository = new SensorDataRepository();
            LoadDataCommand = new RelayCommand(LoadData);

            // 그래프 데이터 초기화
            NtcValues = new SeriesCollection();
            PmValues = new SeriesCollection(); // PM10, PM2.5, PM1.0를 하나의 그래프에 표시할 SeriesCollection
            CtValues = new SeriesCollection(); // CT1, CT2, CT3, CT4를 하나의 그래프에 표시할 SeriesCollection
            TimeLabels = new ObservableCollection<string>();
        }

        public ICommand LoadDataCommand { get; }
        public SeriesCollection NtcValues { get; }
        public SeriesCollection PmValues { get; }
        public SeriesCollection CtValues { get; }
        public ObservableCollection<string> TimeLabels { get; }

        public void LoadData(object parameter)
        {
            if (parameter is int machineId)
            {
                var sensorDataList = _repository.FindSensorDataByMachineId(machineId);

                if (sensorDataList != null && sensorDataList.Any())
                {
                    // 그래프 데이터 초기화
                    NtcValues.Clear();
                    PmValues.Clear();
                    CtValues.Clear();
                    TimeLabels.Clear();

                    var ntcSeries = new ChartValues<double>();

                    // PM, CT 그래프를 위한 ChartValues 준비
                    var pm10Series = new ChartValues<double>();
                    var pm25Series = new ChartValues<double>();
                    var pm1Series = new ChartValues<double>();

                    var ct1Series = new ChartValues<double>();
                    var ct2Series = new ChartValues<double>();
                    var ct3Series = new ChartValues<double>();
                    var ct4Series = new ChartValues<double>();

                    foreach (var data in sensorDataList)
                    {
                        ntcSeries.Add(data.Ntc);
                        pm10Series.Add(data.Pm10);
                        pm25Series.Add(data.Pm2_5);
                        pm1Series.Add(data.Pm1_0);
                        ct1Series.Add(data.Ct1);
                        ct2Series.Add(data.Ct2);
                        ct3Series.Add(data.Ct3);
                        ct4Series.Add(data.Ct4);
                        TimeLabels.Add(data.CollectionDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }

                    NtcValues.Add(new LineSeries { Title = "NTC", Values = ntcSeries });

                    // PM 및 CT SeriesCollection에 시리즈 추가
                    PmValues.Add(new LineSeries { Title = "PM10", Values = pm10Series });
                    PmValues.Add(new LineSeries { Title = "PM2.5", Values = pm25Series });
                    PmValues.Add(new LineSeries { Title = "PM1.0", Values = pm1Series });

                    CtValues.Add(new LineSeries { Title = "CT1", Values = ct1Series });
                    CtValues.Add(new LineSeries { Title = "CT2", Values = ct2Series });
                    CtValues.Add(new LineSeries { Title = "CT3", Values = ct3Series });
                    CtValues.Add(new LineSeries { Title = "CT4", Values = ct4Series });

                    OnPropertyChanged(nameof(NtcValues));
                    OnPropertyChanged(nameof(PmValues));
                    OnPropertyChanged(nameof(CtValues));
                    OnPropertyChanged(nameof(TimeLabels));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object parameter) => _execute(parameter);
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
