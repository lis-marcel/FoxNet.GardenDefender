using System.Timers;

namespace FoxNet.GardenDefender
{
    public class VibExecutor : IDisposable
    {
        private static System.Timers.Timer Timer { get; set; }
        public VibProg CurrentVibProg { get; private set; }

        public void Dispose()
        {
            Timer?.Dispose();
        }

        public void Start(VibProg vibProg)
        {
            if (vibProg == null)
                throw new ArgumentException("vibProg can not be a null");

            this.CurrentVibProg = vibProg;

            vibProg.Init();
            var when = vibProg.NextRun();

            if (when.HasValue)
            {
                Timer = new()
                {
                    AutoReset = false
                };

                Timer.Elapsed += TimerCallback;
                
                InitTimerForNextRun(when);
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
            InitTimerForNextRun(CurrentVibProg.NextRun());
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
