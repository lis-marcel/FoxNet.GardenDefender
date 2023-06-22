using FoxNet.GardenDefender.ProgramsOptions;

namespace FoxNet.GardenDefender;

public partial class MainPage : ContentPage
{
	VibrationPrograms vibrationPrograms = new VibrationPrograms();

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

        programPicker.SelectedIndexChanged += PickerSelectedIndexChanged;

        startButton.Clicked += LockStartAndUnlockCancelButton;
    }

    private void PickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var selected = (ProgramsEnum)programPicker.SelectedItem;

        if (selected == ProgramsEnum.Standard)
        {
            var standard = new Standard();
            var standardContent = standard.GetContent();

            AddContentToPage(standardContent);
        }
    }

    void AddContentToPage(View content)
    {
        var existingContent  = Content as View;
        var containerLayout = new StackLayout();

        containerLayout.Children.Add(existingContent);
        containerLayout.Children.Add(content);

        Content = containerLayout;
    }

	public void RunSelected(object sender, EventArgs e)
	{
        if (programPicker != null)
		{
            var selectedProgram = (ProgramsEnum)programPicker.SelectedItem;
			ProgramsEnum[] allPrograms = (ProgramsEnum[])Enum.GetValues(typeof(ProgramsEnum));

            vibrationPrograms.MatchProgram(selectedProgram, allPrograms);
        }
    }

    public void Cancel(object sender, EventArgs e)
	{
        vibrationPrograms.CancelTimer();

        UnlockStartAndLockCancelButton(sender, e);
    }

    #region Locking and unlocking buttons functions
    private void LockStartAndUnlockCancelButton(Object sender, EventArgs e)
    {
        startButton.IsEnabled = false;
        startButton.BackgroundColor = Color.Parse("DarkGrey");

        cancelButton.IsEnabled = true;
        cancelButton.BackgroundColor = Color.Parse("Red");
    }

    private void UnlockStartAndLockCancelButton(Object sender, EventArgs e)
    {
        startButton.IsEnabled = true;
        startButton.BackgroundColor = Color.Parse("Green");

        cancelButton.IsEnabled = false;
        cancelButton.BackgroundColor = Color.Parse("DarkGrey");
    }
    #endregion
}