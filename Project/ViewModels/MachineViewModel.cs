using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Repository;
using Project.Models;

namespace Project.ViewModels
{
    public class MachineViewModel : INotifyPropertyChanged
    {
        private readonly IMachineRepository _repository;
        private readonly SensorDataViewModel _sensorDataViewModel;
        private Machine _machine;

        public Machine Machine
        {
            get => _machine;
            set
            {
                if (_machine != value)
                {
                    _machine = value;
                    OnPropertyChanged(nameof(Machine));
                }
            }
        }

        public SensorDataViewModel SensorDataViewModel => _sensorDataViewModel;

        public MachineViewModel(IMachineRepository repository, SensorDataViewModel sensorDataViewModel)
        {
            _repository = repository;
            _sensorDataViewModel = sensorDataViewModel;
        }

        public void LoadMachineById(int machineId)
        {
            Console.WriteLine($"Loading machine with ID: {machineId}");

            Machine machine = _repository.FindMachineById(machineId);
            if (machine == null)
            {
                Console.WriteLine("No machine found for the given ID.");
            }

            Machine = machine; // Machine 속성 설정
            Console.WriteLine($"Loaded Machine: {Machine?.MachineId}, {Machine?.DeviceId}");

            // 센서 데이터 로드
            _sensorDataViewModel.LoadData(machineId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            Console.WriteLine($"PropertyChanged triggered for: {propertyName}");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}