namespace FoxNet.GardenDefender;

public partial class MainPage : ContentPage
{
    public IList<VibProg> VibProgList { get; set; }

    public MainPage()
    {
        InitializeComponent();

        BindPicker();

        //    programPicker.SelectedIndexChanged += ShowOptions;

        //    intervalEntry.TextChanged += ParameterChanged;
        //    durationEntry.TextChanged += ParameterChanged;

        startButton.Clicked += Start;

        cancelButton.Clicked += Cancel;
    }

    public void Start(object sender, EventArgs e)
    {
        HideOptions(sender, e);
        //RunSelected(sender, e);
        LockStartButton(sender, e);
        UnlockCancelButton(sender, e);
    }

    public void Cancel(object sender, EventArgs e)
    {
        //Program.Stop();
        LockCancelButton(sender, e);
    }

    /*public void RunSelected(object sender, EventArgs e)
    {
        var selectedProgram = (VibProgRegister)programPicker.SelectedItem;
        var parametersList = GetParametersValues();
        ProgramsEnum[] allPrograms = (ProgramsEnum[])Enum.GetValues(typeof(ProgramsEnum));
        VibExecutor.Start();
    }*/

    public void BindPicker()
    {
        VibProgList = VibProgRegister.All;

        // Set the binding context to the current page
        BindingContext = this;

        // Set the binding properties for the Picker
        programPicker.SetBinding(Picker.ItemsSourceProperty, new Binding("VibProgList"));
        programPicker.SetBinding(Picker.SelectedItemProperty, new Binding("SelectedPickerItem"));
        programPicker.ItemDisplayBinding = new Binding("Name");
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
        options.IsVisible = true;
    }

    private void HideOptions(object sender, EventArgs e)
    {
        options.IsVisible = false;
    }
    #endregion
}