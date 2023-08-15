namespace FoxNet.GardenDefender.VibProgs
{
    public class VibStandard : VibProg
    {
        private DateTime? lastRun;

        public VibStandard()
        {
            VibProgType = VibProgType.Standard;
            Name = "Standrd";
            PeriodDescription = "Enter interval in seconds between vibrations.";
            DurationDescription = "Enter how long to vibrate, in seconds.";
            DurationMs = 2000;
            PeriodMs = 5000;
        }

        public override void Init()
        {
        }

        public override DateTime? NextRun()
        {
            var nextRun = lastRun == null ?
                DateTime.Now :
                DateTime.Now.AddMilliseconds(PeriodMs);

            return nextRun;
        }
        public override void MakeNoise()
        {
            Vibration.Default.Vibrate(DurationMs);

            lastRun = DateTime.Now;
        }
    }
}