using System.ComponentModel;
using System.Windows.Threading;

namespace Project.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        private readonly DispatcherTimer _timer;
        private string _currentDate;
        private string _currentTime;

        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentDate
        {
            get => _currentDate;
            set
            {
                _currentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public TimeViewModel()
        {
            // DispatcherTimer 초기화
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // 1초마다 실행
            };
            _timer.Tick += UpdateDateTime;
            _timer.Start();

            // 초기 값 설정
            UpdateDateTime(null, null);
        }

        private void UpdateDateTime(object sender, EventArgs e)
        {
            // 현재 날짜와 시간 갱신
            CurrentDate = DateTime.Now.ToString("yyyy.MM.dd (ddd)");
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void DisposeTimer()
        {
            // 타이머 정지 및 리소스 해제
            _timer?.Stop();
        }
    }
}
