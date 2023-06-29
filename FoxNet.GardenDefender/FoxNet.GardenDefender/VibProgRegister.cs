namespace FoxNet.GardenDefender
{
    public static class VibProgRegister
    {
        private static VibProg[] all = new VibProg[]
        {
            new VibStandard(),
            new VibRandom(),
        };

        public static IEnumerable<VibProg> All
        {
            get
            {
                return all;
            }
        }
    }
}
