using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using ContactManager.Views;
using ContactManager.Services;

namespace ContactManager.ViewModels
{
    public class LoaderPageViewModel : INotifyPropertyChanged
    {
        private bool _isDataLoaded;
        private IPageService _pageService;
        private IContactService _contactService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ImageSourcePath { get; private set; } = "ContactManager.Images.call_me_maybe.png";
        public ImageSource ImageCallMeMaybe { get; private set; }
        //public string BodyText { get; private set; } = "Downloading Contacts...";
        public string FooterText { get; private set; } = "Developed by Rics";

        private string _bodyText;
        public string BodyText
        {
            get { return _bodyText; }
            private set
            {
                if (_bodyText != value)
                {
                    _bodyText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BodyText"));
                }
            }
        }

        private bool _isErrorEncountered;
        public bool IsErrorEncountered {
            get { return _isErrorEncountered; }
            private set
            {
                if (_isErrorEncountered != value)
                {
                    _isErrorEncountered = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsErrorEncountered"));
                }
            }
        }

        public ICommand LoadDataCommand { get; set; }
        public ICommand GoToHomePageCommand { get; set; }

        public LoaderPageViewModel(IPageService pageService, IContactService contactService)
        {
            _pageService = pageService;
            _contactService = contactService;
            ImageCallMeMaybe = ImageSource.FromResource(ImageSourcePath);
            BodyText = "Downloading Contacts...";
            
            LoadDataCommand = new Command(async () => await LoadData());
            GoToHomePageCommand = new Command(() => GoToHomePage());
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;

            //Set delay to  make the download message transition noticeable
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
            });

            var contacts = await _contactService.GetContactsAsync();
            if (contacts == null) { 
                IsErrorEncountered = true;
                BodyText = "An error occurred while downloading your contacts";
            }
            else
                GoToHomePage();
        }

        private void GoToHomePage()
        {
            _pageService.SetCurrentPage(new HomePage());
        }
    }
}
