using ContactManager.Services;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public class DialPadPageViewModel : INotifyPropertyChanged
    {
        private IPageService _pageService;
        private string _dialDisplay;
        public string DialDisplay
        {
            get { return _dialDisplay; }
            set
            {
                if (_dialDisplay != value)
                {
                    _dialDisplay = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DialDisplay"));
                }
            }
        }

        public ICommand BackspaceCommand { get; set; }
        public ICommand DialCommand { get; set; }
        public ICommand CallCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DialPadPageViewModel(IPageService pageService)
        {
            _pageService = pageService;
            BackspaceCommand = new Command(() => Backspace());
            DialCommand = new Command<string>((n) => Dial(n));
            CallCommand = new Command(async () => await Call());
        }

        private void Backspace()
        {
            if (string.IsNullOrWhiteSpace(DialDisplay))
                return;

            DialDisplay = DialDisplay.Remove(DialDisplay.Length - 1);
        }

        private void Dial(string number)
        {
            DialDisplay += number;
        }

        private async Task Call()
        {
            if (string.IsNullOrWhiteSpace(DialDisplay))
            {
                await _pageService.DisplayAlert("Error", "No dialed number", "Ok");
                return;
            }
            DependencyService.Get<ICallService>().CallContact(DialDisplay);
        }
    }
}
