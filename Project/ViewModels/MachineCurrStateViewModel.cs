using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Project.Commends;
using Project.Models;
using Project.Repository;

namespace Project.ViewModels
{
    public class MachineCurrStateViewModel
    {
        private readonly IMachineCurrStateRepository _repository;

        public ObservableCollection<MachineCurrState> MachineStates { get; set; }

        // 필터링 조건
        public string SelectedManufacturer { get; set; }
        public string SelectedDeviceType { get; set; }
        public string SelectedState { get; set; }

        // 조회 버튼 Command
        public ICommand ApplyFilterCommand { get; }

        public MachineCurrStateViewModel(IMachineCurrStateRepository repository)
        {
            _repository = repository;
            MachineStates = new ObservableCollection<MachineCurrState>();

            ApplyFilterCommand = new RelayCommand(ApplyFilter);

            InitializeMachineStates(); // 초기 데이터 로드
        }

        // 데이터를 로드하는 초기화 메서드
        private void InitializeMachineStates()
        {
            // Repository에서 데이터를 가져옴 (전체 조회)
            var machines = _repository.FindMachineCurrStateByFilters(null, null, null);

            UpdateMachineStates(machines);
        }

        // 필터 적용 메서드
        public void ApplyFilter()
        {
            // "전체선택"은 null로 변환
            string manufacturer = SelectedManufacturer == "전체선택" ? null : SelectedManufacturer;
            string deviceType = SelectedDeviceType == "전체선택" ? null : SelectedDeviceType;
            string state = SelectedState == "전체선택" ? null : SelectedState;

            // 선택된 필터 값 디버깅
            Console.WriteLine("===== ApplyFilter Debugging =====");
            Console.WriteLine($"Selected Manufacturer: {manufacturer ?? "전체선택"}");
            Console.WriteLine($"Selected DeviceType: {deviceType ?? "전체선택"}");
            Console.WriteLine($"Selected State: {state ?? "전체선택"}");

            // Repository에서 필터링된 데이터 가져오기
            var machines = _repository.FindMachineCurrStateByFilters(manufacturer, deviceType, state);

            // 필터링 결과 디버깅
            Console.WriteLine($"Filtered Results Count: {machines.Count}");
            foreach (var machine in machines)
            {
                Console.WriteLine($"MachineId: {machine.MachineId}, DeviceId: {machine.DeviceId}, Type: {machine.Type}, State: {machine.State}");
            }

            // MachineStates 업데이트
            UpdateMachineStates(machines);
            Console.WriteLine("===== End of ApplyFilter =====");
        }

        // ObservableCollection 업데이트
        private void UpdateMachineStates(IEnumerable<MachineCurrState> machines)
        {
            MachineStates.Clear();
            foreach (var machine in machines)
            {
                MachineStates.Add(machine);
            }
        }
    }
}
