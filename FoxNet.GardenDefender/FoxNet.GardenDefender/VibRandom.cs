namespace FoxNet.GardenDefender
{
    public class VibRandom : VibProg
    {
        private DateTime? lastRun;

        public VibRandom()
        {
            VibProgType = VibProgType.RandomPeriodDuration;
            Name = "Random Period/Duration";
            Description = "";
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
            Thread.Sleep(DurationMs); // TODO: Marcel
            lastRun = DateTime.Now;
        }

    }


}
