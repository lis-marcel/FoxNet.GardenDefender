namespace FoxNet.GardenDefender
{
    public class VibStandard : VibProg
    {
        private DateTime? lastRun;

        public VibStandard()
        {
            VibProgType = VibProgType.Standard;
            Name = "Standrd";
            Description = "";
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
            Thread.Sleep(DurationMs); // TODO: Marcel
            lastRun = DateTime.Now;
        }
    }
}