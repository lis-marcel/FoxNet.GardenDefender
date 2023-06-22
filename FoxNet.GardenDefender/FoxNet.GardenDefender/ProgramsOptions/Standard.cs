using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxNet.GardenDefender.ProgramsOptions
{
    public class Standard
    {
        public View GetContent()
        {
            var amountOfVibesLabel = new Label { Text = "Enter number of vibes per hour:" };
            var amountOfVibesInput = new Entry { Placeholder = "Enter number:" };

            var layout = new StackLayout();

            layout.Children.Add(amountOfVibesLabel);
            layout.Children.Add(amountOfVibesInput);

            return layout;
        }
    }
}
