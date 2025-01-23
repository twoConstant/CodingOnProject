using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Repository;
using Project.Models;
using System.Windows;
using LiveCharts;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;

namespace Project.ViewModels
{
    public class SensorDataViewModel : INotifyPropertyChanged
    {
        private readonly ISensorDataRepository _repository;
        private Sensor_data _sensor_data;
        private List<Sensor_data> _sensor_dataList;
        
        // Chart의 y축 값 ex) ntc, ct1, ct2.......
        public SeriesCollection YaxisValues { get; set; } // NtcSeries

        // Chart의 x축 값 collection_date_time
        public ObservableCollection<string> XaxisValues { get; set; } // ChartLabels

        public Sensor_data Sensor_data
        {
            get
            {
                return _sensor_data;
            }
            set
            {
                if (_sensor_data != value)
                {
                    _sensor_data = value;
                    OnPropertyChanged("sensor_data");
                }
            }
        }

        public List<Sensor_data> Sensor_dataList
        {
            get 
            {
                return _sensor_dataList;
            }
            set
            {
                if (_sensor_dataList != value)
                {
                    _sensor_dataList = value;
                    OnPropertyChanged("sensor_data");
                }
            }
        }

        public SensorDataViewModel(ISensorDataRepository repository)
        {
            _repository = repository;
            YaxisValues = new SeriesCollection(); // NtcSeries
            XaxisValues = new ObservableCollection<string>(); // ChartLabels
        }

        public void LoadSensorDataByMachineID(int machine_id, string sensorValue, string startDate, string endDate)
        {
            Sensor_data sensor_data = _repository.FindSensordataByMachineID(machine_id, sensorValue, startDate, endDate);
            Sensor_data = sensor_data;
        }

        public void LoadSensorDataListByMachineID(int machine_id, string sensorValue, string startDate, string endDate)
        {
            List<Sensor_data> sensor_dataList = _repository.FindSensordataListByMachineID(machine_id, sensorValue, startDate, endDate);
            Sensor_dataList = sensor_dataList;

            // 초기화
            XaxisValues.Clear(); // ChartLabels
            var yaxisValues = new List<double>(); // ntcValues

            foreach (var data in Sensor_dataList)
            {
                // Safely handle null or invalid collection_data_time
                if (data.collection_data_time != null)
                {
                    XaxisValues.Add(data.collection_data_time?.ToString("yyyy-MM-dd HH:mm") ?? "No Data"); // ChartLabels
                }
                else
                {
                    XaxisValues.Add("No Date"); // ChartLabels
                }



                //// Safely handle null or invalid NTC values
                //var yaxisValue = data.sensorValue.GetValueOrDefault(0.0); // If null, default to 0.0, ntcValue
                //yaxisValues.Add(yaxisValue); // ntcValues, ntcValue

                // 특정 `sensorValue` 값이 null이 아니면 Y축에 추가
                var sensorValueProperty = data.GetType().GetProperty(sensorValue);
                if (sensorValueProperty != null)
                {
                    var sensorValues = sensorValueProperty.GetValue(data) as double?;
                    yaxisValues.Add(sensorValues ?? 0.0); // Null이면 0.0 추가
                }

                // 차트 데이터 업데이트
                YaxisValues.Clear();
                YaxisValues.Add(new LineSeries
                {
                    Title = sensorValue,
                    Values = new ChartValues<double>(yaxisValues), // ntcValues
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 5
                });
                Console.WriteLine("LoadSensorDataListByMachineID 완료");
            }
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
