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
        private SensorData _sensorData;

        public SensorDataViewModel()
        {
            _repository = new SensorDataRepository();
            LoadDataCommand = new RelayCommand(LoadData);
        }

        public SensorData SensorData
        {
            get => _sensorData;
            set
            {
                _sensorData = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadDataCommand { get; }

        private void LoadData(object parameter)
        {
            if (parameter is int metaInfoId)
            {
                SensorData = _repository.FindSensorDataById(metaInfoId);
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
