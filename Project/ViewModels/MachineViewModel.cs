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
        private Machine _machine;

        public Machine Machine
        {
            get
            {
                return _machine;
            }
            set
            {
                if (_machine != value)
                {
                    _machine = value;
                    OnPropertyChanged("Machine");
                }
            }
        }

        public MachineViewModel(IMachineRepository repository)
        {
            _repository = repository;
        }

        public void LoadMachineById(int machineId)
        {
            Machine machine = _repository.FindMachineById(machineId);
            Machine = machine;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
