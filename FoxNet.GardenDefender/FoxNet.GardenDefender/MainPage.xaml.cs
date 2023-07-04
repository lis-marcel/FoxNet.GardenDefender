namespace FoxNet.GardenDefender;

public partial class MainPage : ContentPage
{
    public IList<VibProg> VibProgList { get; set; }
    public VibProg SelectedProgram { get; set; }
    public VibExecutor VibExecutor { get; set; }

    public MainPage()
    {
        InitializeComponent();

        BindPicker();

        periodEntry.TextChanged += ParameterChanged;
        durationEntry.TextChanged += ParameterChanged;

        programPicker.SelectedIndexChanged += ProgramPickerChangedValue;

        startButton.Clicked += Start;

        cancelButton.Clicked += Cancel;
    }

    public void Start(object sender, EventArgs e)
    {
        HideOptions(sender, e);

        VibExecutor = new();
        VibExecutor.Start(SelectedProgram);

        LockStartButton(sender, e);
        UnlockCancelButton(sender, e);
    }

    public void Cancel(object sender, EventArgs e)
    {
        VibExecutor.Stop();
        VibExecutor.Dispose();

        LockCancelButton(sender, e);
    }

    public void BindPicker()
    {
        VibProgList = VibProgRegister.All;

        // Set the binding context to the current page
        BindingContext = this;

        // Set the binding properties for the Picker
        programPicker.SetBinding(Picker.ItemsSourceProperty, new Binding("VibProgList"));
        programPicker.ItemDisplayBinding = new Binding("Name");
    }

    public void ProgramPickerChangedValue(object sender, EventArgs e)
    {
        SelectedProgram = (VibProg)programPicker.SelectedItem;

        ShowOptions(sender, e);

        int periodS = SelectedProgram.PeriodMs / 1000;
        int durationS = SelectedProgram.DurationMs / 1000;

        periodLabel.Text = SelectedProgram.PeriodDescription;
        periodEntry.Text = periodS.ToString();

        durationLabel.Text = SelectedProgram.DurationDescription;
        durationEntry.Text = durationS.ToString();
    }

    public void ParameterChanged(object sender, TextChangedEventArgs e)
    {
        var period = periodEntry.Text;
        var duration = durationEntry.Text;
        _ = int.TryParse(period, out int _period);
        _ = int.TryParse(duration, out int _duration);

        bool periodRes = ValidateParameter(_period);
        bool durationRes = ValidateParameter(_duration);

        if (periodRes && durationRes)
        {
            SelectedProgram.PeriodMs = _period * 1000;
            SelectedProgram.DurationMs = _duration * 1000;

            UnlockStartButton(sender, e);
        }

        else
        {
            LockStartButton(sender, e);
        }
    }

    private bool ValidateParameter(int param)
    {
        int min = 1;
        int max = 20;

        if (param > max || param < min) 
            return false;

        return true;
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
        options.IsVisible = true;
    }

    private void HideOptions(object sender, EventArgs e)
    {
        options.IsVisible = false;
    }
    #endregion
}