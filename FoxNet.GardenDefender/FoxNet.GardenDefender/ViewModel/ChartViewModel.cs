using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;

namespace FoxNet.GardenDefender
{
    public partial class ChartViewModel : ObservableObject
    {
        private Random _random = new();
        private readonly ObservableCollection<ObservableValue> _observableValues;

        public ChartViewModel()
        {
            _observableValues = new ObservableCollection<ObservableValue> { };

            Series = new ObservableCollection<ISeries>
            {
                new LineSeries<ObservableValue>
                {
                    Values = _observableValues,
                    Fill = null,
                    LineSmoothness = 0,
                }
            };
        }

        public ObservableCollection<ISeries> Series { get; set; }

        [RelayCommand]
        public void AddItem()
        {
            var item = _random.Next(1, 5);
            _observableValues.Add(new(item));

            ControlChartSize();
        }

        [RelayCommand]
        public void RemoveItem()
        {
            if (_observableValues.Count == 0) return;
            _observableValues.RemoveAt(0);
        }

        private void ControlChartSize()
        {
            if (_observableValues.Count > 9)
            {
                RemoveItem();
            }
        }
    }
}