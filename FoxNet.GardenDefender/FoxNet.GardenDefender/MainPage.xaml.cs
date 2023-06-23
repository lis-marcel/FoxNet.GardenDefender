using FoxNet.GardenDefender.ProgramsOptions;
using Microsoft.Maui.Graphics.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FoxNet.GardenDefender;

public partial class MainPage : ContentPage
{
	private VibrationPrograms vibrationPrograms = new VibrationPrograms();

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

        programPicker.SelectedIndexChanged += ShowSelectedProgramOptions;

        startButton.Clicked += DisableStartButton;
        startButton.Clicked += EnableCancelButton;
    }

	public void RunSelected(object sender, EventArgs e)
	{
        if (programPicker != null)
		{
            var selectedProgram = (ProgramsEnum)programPicker.SelectedItem;
            var parametersList = GetParametersValues(selectedProgram);
			ProgramsEnum[] allPrograms = (ProgramsEnum[])Enum.GetValues(typeof(ProgramsEnum));

            vibrationPrograms.MatchProgram(selectedProgram, allPrograms, parametersList);
        }
    }

    private void ShowSelectedProgramOptions(object sender, EventArgs e)
    {
        var selected = (ProgramsEnum)programPicker.SelectedItem;

        if (selected == ProgramsEnum.Standard)
        {
            ShowStandardOptions();
            HideRandomOptions();
        }

        if (selected == ProgramsEnum.Random)
        {
            HideStandardOptions();
            ShowStandardOptions();
        }
    }

    public void Cancel(object sender, EventArgs e)
	{
        vibrationPrograms.CancelTimer();

        DisableCancelButton(sender, e);
        EnableStartButton(sender, e);
    }

    public bool ValidateParameter(string parameter)
    {
        if (int.TryParse(parameter, out int parsedParameter)) 
        {
            int min = 1;
            int max = 50;

            if (parsedParameter >= min && parsedParameter <= max)
            {
                return true;
            }
        }

        return false;
    }

    private List<int> GetParametersValues(ProgramsEnum program)
    {
        var parametersList = new List<int>();

        errorLabel.IsVisible = false;

        if (program == ProgramsEnum.Standard)
        {
            var interval = standardInterval.Text;
            var duration = vibrationDuration.Text;

            if (ValidateParameter(interval) && ValidateParameter(duration))
            {

                var parsedInterval = int.Parse(interval);
                var parsedDuration = int.Parse(duration);

                parametersList.Add(parsedInterval);
                parametersList.Add(parsedDuration);
            }
            
            else
            {
                errorLabel.IsVisible = true;
            }
        }

        if (program == ProgramsEnum.Random)
        {
            var interval = standardInterval.Text;
            var duration = maxDuration.Text;

            if (ValidateParameter(interval) && ValidateParameter(duration))
            {
                var parsedInterval = int.Parse(interval);
                var parsedDuration = int.Parse(duration);

                parametersList.Add(parsedInterval);
                parametersList.Add(parsedDuration);
            }

            else
            {
                errorLabel.IsVisible = true;
            }
        }

        return parametersList;
    }

    #region Locking and unlocking buttons functions
    private void EnableCancelButton(Object sender, EventArgs e)
    {
        cancelButton.IsEnabled = true;
        cancelButton.BackgroundColor = Color.Parse("Red");
    }

    private void DisableStartButton(Object sender, EventArgs e)
    {
        startButton.IsEnabled = false;
        startButton.BackgroundColor = Color.Parse("DarkGrey");
    }

    private void DisableCancelButton(Object sender, EventArgs e)
    {
        cancelButton.IsEnabled = false;
        cancelButton.BackgroundColor = Color.Parse("DarkGrey");
    }

    private void EnableStartButton(Object sender, EventArgs e)
    {
        startButton.IsEnabled = true;
        startButton.BackgroundColor = Color.Parse("Green");
    }
    #endregion

    #region Showing and hiding programs options
    private void ShowStandardOptions()
    {
        standardOptions.IsVisible = true;
    }

    private void ShowRandomOptions()
    {
        randomOptions.IsVisible = true;
    }

    private void HideStandardOptions()
    {
        standardOptions.IsVisible = false;
    }

    private void HideRandomOptions()
    {
        randomOptions.IsVisible = false;
    }
    #endregion
}