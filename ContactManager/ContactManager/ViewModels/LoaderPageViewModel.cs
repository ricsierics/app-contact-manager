using System.Collections.Generic;
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
        private IPageService _pageService;
        private IContactService _contactService;
        private ContactsPageViewModel _contactsPageViewModel;
        private DialPadPageViewModel _dialPadPageViewModel;
        private string _imageSourcePath { get { return "ContactManager.Images.call_me_maybe.png"; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public ImageSource ImageCallMeMaybe { get; private set; }
        
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

            ImageCallMeMaybe = ImageSource.FromResource(_imageSourcePath);
            BodyText = "Downloading Contacts...";
            
            LoadDataCommand = new Command(async () => await LoadData());
            GoToHomePageCommand = new Command(() => GoToHomePage());
        }

        private async Task LoadData()
        {
            //Set delay to  make the download message to error message transition noticeable
            await Task.Run(() =>
            {
                Thread.Sleep(500);
            });

            var contactsVM = new List<ContactViewModel>();
            var contacts = await _contactService.GetContactsAsync();
            if (contacts == null)
            {
                IsErrorEncountered = true;
                BodyText = "An error occurred while downloading your contacts";
                _contactsPageViewModel = new ContactsPageViewModel(_pageService, contactsVM);
                _dialPadPageViewModel = new DialPadPageViewModel(_pageService);
            }
            else
            {   
                foreach (var c in contacts)
                {
                    contactsVM.Add(new ContactViewModel(c));
                }

                _contactsPageViewModel = new ContactsPageViewModel(_pageService, contactsVM);
                _dialPadPageViewModel = new DialPadPageViewModel(_pageService);
                GoToHomePage();
            }
        }
        
        private void GoToHomePage()
        {   
            _pageService.PushNew(new HomePage(_contactsPageViewModel, _dialPadPageViewModel));
        }
    }
}
