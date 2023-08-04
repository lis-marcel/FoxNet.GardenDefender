namespace FoxNet.GardenDefender.VibProgs
{
    public class VibRandom : VibProg
    {
        private DateTime? lastRun;

        public VibRandom()
        {
            VibProgType = VibProgType.RandomPeriodDuration;
            Name = "Random Period/Duration";
            PeriodDescription = "Enter maximal interval in seconds between next vibrations.";
            DurationDescription = "Enter maximal duration of vibrations, duration will be random from 1s to your given max.";
            DurationMs = 2000;
            PeriodMs = 5000;
        }

        // TODO: Marcel - min/max dla Period/Duration

        public override void Init()
        {
        }

        public override DateTime? NextRun()
        {
            Randomize();

            var nextRun = lastRun == null ?
                DateTime.Now :
                DateTime.Now.AddMilliseconds(PeriodMs);

            return nextRun;
        }

        private void Randomize()
        {
            var rnd = new Random();
            DurationMs = rnd.Next(1, 20) * 1000;
            PeriodMs = rnd.Next(1, 20) * 1000;
        }

        public override void MakeNoise()
        {
            Vibration.Default.Vibrate(DurationMs);

            lastRun = DateTime.Now;
        }
    }
}