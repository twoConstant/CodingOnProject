using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;
using Project.Repository;

namespace Project.ViewModels
{
    public class MachineCurrStateViewModel
    {
        private readonly IMachineCurrStateRepository _repository;

        public ObservableCollection<MachineCurrState> MachineStates { get; set; }

        public MachineCurrStateViewModel(IMachineCurrStateRepository repository)
        {
            _repository = repository;
            MachineStates = new ObservableCollection<MachineCurrState>();

            LoadMachineStates(); // 데이터 로드
        }

        private void LoadMachineStates()
        {
            // 임시로 필터 조건을 비워 모든 데이터를 가져옴
            var machines = _repository.FindMachineCurrStateByFilters(null, null, null);

            foreach (var machine in machines)
            {
                MachineStates.Add(machine);
            }
        }
    }
}
