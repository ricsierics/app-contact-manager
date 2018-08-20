using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public class DialPadPageViewModel : INotifyPropertyChanged
    {   
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

        public DialPadPageViewModel()
        {
            BackspaceCommand = new Command(() => Backspace());
            DialCommand = new Command<string>((n) => Dial(n));
            CallCommand = new Command(() => Call());
        }

        private void Backspace()
        {
            if (DialDisplay.Length == 0)
                return;

            DialDisplay = DialDisplay.Remove(DialDisplay.Length - 1);
        }

        private void Dial(string number)
        {
            DialDisplay += number;
        }

        private void Call()
        {
            DependencyService.Get<ICallService>().CallContact(DialDisplay);
        }
    }
}
