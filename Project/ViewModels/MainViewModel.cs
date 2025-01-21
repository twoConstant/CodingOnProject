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


        public MainViewModel(IMachineCurrStateRepository machineCurrStateRepository)
        {
            MachineCurrStateViewModel = new MachineCurrStateViewModel(machineCurrStateRepository);
            TimeViewModel = new TimeViewModel();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
