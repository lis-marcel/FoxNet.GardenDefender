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

        public void MatchProgram(Enum selectedProgram, IList<MainPage.ProgramsEnum> allPrograms)
        {
            Option = 0;
            aTimer = new System.Timers.Timer
            {
                Interval = 5000
            };

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
                    secondsToVibrate = 2;
                    vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

                    Vibration.Default.Vibrate(vibrationLength);
                    break;

                case 1:
                    secondsToVibrate = random.Next(1, 4);
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
