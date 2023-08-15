using System.Timers;
using FoxNet.GardenDefender.VibProgs;
using System.Collections.ObjectModel;
using LiveChartsCore;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;



namespace FoxNet.GardenDefender
{
    public partial class ViewModel : ObservableObject, IDisposable
    {
        private static System.Timers.Timer _timer { get; set; }
        private Random _random = new();
        private readonly ObservableCollection<ObservableValue> _observableValues;

        public VibProg CurrentVibProg { get; set; }
        public ObservableCollection<ISeries> Series { get; set; }
        public IList<VibProg> VibProgsList 
        { 
            get => VibProgRegister.All; 
        }

        public ViewModel()
        {
            _observableValues = new ObservableCollection<ObservableValue> { };

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = _observableValues,
                    Fill = null,
                    LineSmoothness = 0,
                }
            };
        }

        #region Timer
        public void Start(VibProg vibProg)
        {
            if (vibProg == null)
                throw new ArgumentException("vibProg can not be a null");

            this.CurrentVibProg = vibProg;
            var when = vibProg.NextRun();

            if (when.HasValue)
            {
                _timer = new()
                {
                    AutoReset = false
                };

                _timer.Elapsed += TimerCallback;

                InitTimerForNextRun(when);
            }
        }

        private void InitTimerForNextRun(DateTime? when)
        {
            _timer.Stop();

            if (when.HasValue)
            {
                _timer.Interval = Math.Max(1, (when - DateTime.Now).Value.TotalMilliseconds);
                _timer.Start();

                this.CurrentVibProg.NextRunTime = when;

                // Add chart element here
                AddItem();
            }
        }

        private void TimerCallback(object source, ElapsedEventArgs e)
        {
            CurrentVibProg.MakeNoise();
            InitTimerForNextRun(
                CurrentVibProg.NextRun());
        }

        public void Stop()
        {
            InitTimerForNextRun(null);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public bool IsRunning
        {
            get
            {
                return _timer.Enabled;
            }
        }
        #endregion

        #region Chart
        [RelayCommand]
        public void AddItem()
        {
            var item = _random.Next(1, 5);
            _observableValues.Add(new(item));

            ControlChartSize();
        }

        [RelayCommand]
        public void RemoveItem()
        {
            if (_observableValues.Count == 0) return;
            _observableValues.RemoveAt(0);
        }

        private void ControlChartSize()
        {
            if (_observableValues.Count > 9)
            {
                RemoveItem();
            }
        }
        #endregion
    }
}
