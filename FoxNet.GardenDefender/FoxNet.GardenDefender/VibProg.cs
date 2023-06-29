﻿namespace FoxNet.GardenDefender
{
    public enum VibProgType { Standard, RandomPeriodDuration };
    public abstract class VibProg
    {
        public VibProgType VibProgType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PeriodMs { get; set; }
        public int DurationMs { get; set; }

        public abstract void Init();
        public abstract DateTime? NextRun();
        public abstract void MakeNoise();
    }

}