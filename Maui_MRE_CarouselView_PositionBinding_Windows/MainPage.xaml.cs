using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Maui_MRE_CarouselView_PositionBinding_Windows
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private void OnCarouselPositionChanged(object sender, PositionChangedEventArgs e)
        {
            var viewModel = (MainViewModel)BindingContext;
            System.Diagnostics.Debug.WriteLine($"[Page] CarouselView Position changed to: {e.CurrentPosition}, Previous: {e.PreviousPosition}, ViewModel CurrentTabIndex: {viewModel.CurrentTabIndex}");
        }
    }

}

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<CarouselItem> TabItems { get; set; }
    private int currentTabIndex;
    public int CurrentTabIndex
    {
        get => currentTabIndex;
        set
        {
            if (currentTabIndex != value)
            {
                System.Diagnostics.Debug.WriteLine($"[ViewModel] CurrentTabIndex changed to: {value}");
                currentTabIndex = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand TabSelectedCommand { get; }

    public MainViewModel()
    {
        TabItems = new ObservableCollection<CarouselItem>
        {
            new CarouselItem { Title = "This is page 1" },
            new CarouselItem { Title = "This is page 2" },
        };

        TabSelectedCommand = new Command<string>(TabSelected);
    }

    private void TabSelected(string index)
    {
        if (int.TryParse(index, out int tabIndex))
        {
            System.Diagnostics.Debug.WriteLine($"[ViewModel] TabSelectedCommand triggered with index: {tabIndex}");
            CurrentTabIndex = tabIndex;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public class CarouselItem
{
    public string Title { get; set; }
}
