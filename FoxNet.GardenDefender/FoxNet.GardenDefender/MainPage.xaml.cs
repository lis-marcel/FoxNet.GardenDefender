using FoxNet.GardenDefender.ProgramsOptions;
using Microsoft.Maui.Graphics.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FoxNet.GardenDefender;

public partial class MainPage : ContentPage
{
    private VibrationPrograms Program { get; set; }

    #region Program types enum
    public enum ProgramsEnum
    {
        Standard,
        Random,
    }
    #endregion

    public MainPage()
    {
        InitializeComponent();

        programPicker.ItemsSource = (System.Collections.IList)Enum.GetValues(typeof(ProgramsEnum)).Cast<ProgramsEnum>();

        programPicker.SelectedIndexChanged += ShowOptions;

        intervalEntry.TextChanged += ParameterChanged;
        durationEntry.TextChanged += ParameterChanged;

        startButton.Clicked += Start;

        cancelButton.Clicked += Cancel;
    }

    public void Start(object sender, EventArgs e)
    {
        HideOptions(sender, e);
        RunSelected(sender, e);
        LockStartButton(sender, e);
        UnlockCancelButton(sender, e);
    }

    public void Cancel(object sender, EventArgs e)
    {
        Program.CancelTimer();
        LockCancelButton(sender, e);
    }

    public void RunSelected(object sender, EventArgs e)
    {
        if (programPicker != null)
        {
            Program = new();

            var selectedProgram = (ProgramsEnum)programPicker.SelectedItem;
            var parametersList = GetParametersValues();
            ProgramsEnum[] allPrograms = (ProgramsEnum[])Enum.GetValues(typeof(ProgramsEnum));

            VibrationPrograms.Run(selectedProgram, allPrograms, parametersList);
        }
    }

    public void ParameterChanged(object sender, TextChangedEventArgs e)
    {
        var interval = intervalEntry.Text;
        var duration = durationEntry.Text;
        _ = int.TryParse(interval, out int _interval);
        _ = int.TryParse(duration, out int _duration);

        List<int> paramsList = new()
        {
            _interval,
            _duration
        };

        var res = ValidateParameter(paramsList);

        if (res)
        {
            UnlockStartButton(sender, e);
        }

        else
        {
            LockStartButton(sender, e);
        }
    }

    private bool ValidateParameter(List<int> paramsList)
    {
        int min = 1;
        int max = 50;

        foreach (int param in paramsList)
        {
            if (param > max || param < min) return false;
        }

        return true;
    }

    private List<int> GetParametersValues()
    {
        var parametersList = new List<int>();

        errorLabel.IsVisible = false;

        var interval = intervalEntry.Text;
        var duration = durationEntry.Text;

        var parsedInterval = int.Parse(interval);
        var parsedDuration = int.Parse(duration);

        parametersList.Add(parsedInterval);
        parametersList.Add(parsedDuration);

        return parametersList;
    }

    #region Locking and unlocking buttons functions
    private void UnlockCancelButton(object sender, EventArgs e)
    {
        cancelButton.IsEnabled = true;
        cancelButton.BackgroundColor = Color.Parse("Red");
    }

    private void LockStartButton(object sender, EventArgs e)
    {
        startButton.IsEnabled = false;
        startButton.BackgroundColor = Color.Parse("DarkGrey");
    }

    private void UnlockStartButton(object sender, EventArgs e)
    {
        startButton.IsEnabled = true;
        startButton.BackgroundColor = Color.Parse("Green");
    }

    private void LockCancelButton(object sender, EventArgs e)
    {
        cancelButton.IsEnabled = false;
        cancelButton.BackgroundColor = Color.Parse("DarkGrey");
    }
    #endregion

    #region Showing and hiding programs options
    private void ShowOptions(object sender, EventArgs e)
    {
        /*switch (ProgramsEnum)
        {
            case Standard:
                intervalLabel.Text = "Enter interval between vibrations: (min 1s, max 50s)";
                durationLabel.Text = "Enter constant duration: (min 1s, max 50s)";
                break;
            case 0:
                intervalLabel.Text = "Enter interval between vibrations: (min 1s, max 50s)";
                durationLabel.Text = "Enter duration, this will be maximal duration of vibration: (min 1s, max 50s)";
                break;
        }*/

        options.IsVisible = true;
    }

    private void HideOptions(object sender, EventArgs e)
    {
        options.IsVisible = false;
    }
    #endregion
}