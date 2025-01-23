using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project.Commends;
using Project.Repository;

namespace Project.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MachineCurrStateViewModel MachineCurrStateViewModel { get; }
        public TimeViewModel TimeViewModel { get; }
        public MachineViewModel MachineViewModel { get; }


        public MainViewModel(
                IMachineCurrStateRepository machineCurrStateRepository,
                IMachineRepository machineRepository,
                SensorDataViewModel sensorDataViewModel
            )
        {
            MachineCurrStateViewModel = new MachineCurrStateViewModel(machineCurrStateRepository);
            MachineViewModel = new MachineViewModel(machineRepository, sensorDataViewModel);
            TimeViewModel = new TimeViewModel();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
