using System.ComponentModel;
using System.Timers;
using FoxNet.GardenDefender.VibProgs;

namespace FoxNet.GardenDefender.VibLogic
{
    public partial class VibExecutor : IDisposable
    {
        private static System.Timers.Timer Timer { get; set; }
        public VibProg CurrentVibProg { get; set; }
        public DateTime? NextRun { get; set; }
         
        public event PropertyChangedEventHandler PropertyChanged;
        public void Dispose()
        {
            Timer?.Dispose();
        }

        public void Start(VibProg vibProg)
        {
            if (vibProg == null)
                throw new ArgumentException("vibProg can not be a null");

            CurrentVibProg = vibProg;

            vibProg.Init();

            NextRun = vibProg.NextRun();

            if (NextRun.HasValue)
            {
                Timer = new()
                {
                    AutoReset = false
                };

                Timer.Elapsed += TimerCallback;

                InitTimerForNextRun(NextRun);
            }
        }

        private void InitTimerForNextRun(DateTime? when)
        {
            Timer.Stop();

            if (when.HasValue)
            {
                Timer.Interval = Math.Max(1, (when - DateTime.Now).Value.TotalMilliseconds);
                Timer.Start();
            }
        }

        private void TimerCallback(object source, ElapsedEventArgs e)
        {
            CurrentVibProg.MakeNoise();
            InitTimerForNextRun(CurrentVibProg.
                NextRun());
        }

        public void Stop()
        {
            InitTimerForNextRun(null);
        }

        public bool IsRunning
        {
            get
            {
                return Timer.Enabled;
            }
        }
    }
}
