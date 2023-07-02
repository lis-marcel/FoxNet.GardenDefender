namespace FoxNet.GardenDefender
{
    public static class VibProgRegister
    {
        private static VibProg[] all = new VibProg[]
        {
            new VibStandard(),
            new VibRandom(),
        };

        public static IList<VibProg> All
        {
            get
            {
                return all;
            }
        }
    }
}
