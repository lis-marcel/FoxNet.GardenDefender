using System.Runtime.CompilerServices;
using System.Timers;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.Maui.Controls.Compatibility;

namespace FoxNet.GardenDefender
{
    public class VibrationPrograms
    {
        private static System.Timers.Timer ATimer { get; set; }
        private int Option { get; set; }
        private int SecondsToVibrate { get; set; }

        public void Run(Enum selectedProgram, IList<MainPage.ProgramsEnum> allPrograms, List<int> parameters)
        {
            Option = 0;
            ATimer = new()
            {
                Interval = parameters[0] * 1000,
            };

            SecondsToVibrate = parameters[1];

            foreach (var program in allPrograms)
            {
                if (program.Equals(selectedProgram))
                {
                    ATimer.Elapsed += Scenarios;
                    break;
                }

                Option++;
            }

            ATimer.AutoReset = true;
            ATimer.Enabled = true;
        }

        private void Scenarios(object source, System.Timers.ElapsedEventArgs e)
        {
            int secondsToVibrate;
            var random = new Random();
            TimeSpan duration;

            switch (Option)
            {
                case 0:
                    duration = TimeSpan.FromSeconds(SecondsToVibrate);

                    Vibration.Default.Vibrate(duration);
                    break;

                case 1:
                    secondsToVibrate = random.Next(1, SecondsToVibrate);
                    duration = TimeSpan.FromSeconds(secondsToVibrate);

                    Vibration.Default.Vibrate(duration);
                    break;
            }
        }

        public void CancelTimer()
        {
            ATimer = null;
        }
    }
}
