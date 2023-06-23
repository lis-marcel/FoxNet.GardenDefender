using System.Runtime.CompilerServices;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.Maui.Controls.Compatibility;

namespace FoxNet.GardenDefender
{
    public class VibrationPrograms
    {
        private static System.Timers.Timer aTimer;
        private int Option;
        private List<int> OptionList;

        public void MatchProgram(Enum selectedProgram, IList<MainPage.ProgramsEnum> allPrograms, List<int> parameters)
        {
            Option = 0;
            aTimer = new System.Timers.Timer();
            OptionList = parameters;

            foreach (var program in allPrograms)
            {
                if (program.Equals(selectedProgram))
                {
                    aTimer.Elapsed += Scenarios;
                    break;
                }

                Option++;
            }

            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void Scenarios(Object source, System.Timers.ElapsedEventArgs e)
        {
            int secondsToVibrate;
            var random = new Random();
            TimeSpan vibrationLength;

            switch (Option)
            {
                case 0:
                    aTimer.Interval = OptionList[0] * 1000;

                    secondsToVibrate = OptionList[1];
                    vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

                    Vibration.Default.Vibrate(vibrationLength);
                    break;

                case 1:
                    aTimer.Interval = OptionList[0] * 1000;
                    int maxVibrationTime = OptionList[0];

                    secondsToVibrate = random.Next(1, maxVibrationTime);
                    vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

                    Vibration.Default.Vibrate(vibrationLength);
                    break;
            }
        }

        public void CancelTimer()
        {
            aTimer.Stop();
            aTimer.Dispose();
        }
    }
}
