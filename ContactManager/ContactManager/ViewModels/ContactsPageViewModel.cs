using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using ContactManager.Views;
using System.ComponentModel;
using System.Linq;
using ContactManager.Services;
using ContactManager.Extensions;

namespace ContactManager.ViewModels
{
    public class ContactsPageViewModel : INotifyPropertyChanged
    {
        #region FIELDS/PROPERTIES

        private IPageService _pageService;
        private List<ContactViewModel> _contactsNoFilter { get; set; }

        public ObservableCollection<ContactViewModel> Contacts { get; private set; }

        public ICommand SearchContactCommand { get; private set; }
        public ICommand CallContactCommand { get; private set; }
        public ICommand ToggleIsFavoriteContactCommand { get; private set; }
        public ICommand AddContactCommand { get; private set; }
        public ICommand EditContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region CONSTRUCTORS

        public ContactsPageViewModel(IPageService pageService, List<ContactViewModel> contactsNoFilter)
        {
            _pageService = pageService;

            SearchContactCommand = new Command<string>((n) => SearchContact(n));
            CallContactCommand = new Command<string>((c) => CallContact(c));
            ToggleIsFavoriteContactCommand = new Command<ContactViewModel>((c) => ToggleIsFavoriteContact(c));
            AddContactCommand = new Command(async () => await AddContact());

            EditContactCommand = new Command<ContactViewModel>(async (c) => await EditContact(c));
            DeleteContactCommand = new Command<ContactViewModel>(async (c) => await DeleteContact(c));

            _contactsNoFilter = SanitizeList(contactsNoFilter);
            Contacts = new ObservableCollection<ContactViewModel>(_contactsNoFilter);
        }

        #endregion

        #region METHODS

        private void SearchContact(string keywordSearch)
        {
            Contacts.Clear();
            if (string.IsNullOrWhiteSpace(keywordSearch))
            {
                Contacts.AddRange(_contactsNoFilter);
            }
            else
            {
                var filteredContacts = _contactsNoFilter.Where(x => x.FullName.ToUpper().Contains(keywordSearch.Trim().ToUpper()));
                Contacts.AddRange(filteredContacts);
            }
        }

        private void CallContact(string contactNumber)
        {
            DependencyService.Get<ICallService>().CallContact(contactNumber);
        }

        private void ToggleIsFavoriteContact(ContactViewModel contactViewModel)
        {
            contactViewModel.IsFavorite = !contactViewModel.IsFavorite;
            SortList();
        }

        private async Task AddContact()
        {
            var viewModel = new ContactDetailPageViewModel(new ContactViewModel(), _pageService);

            viewModel.ContactAdded += (source, newContact) =>
            {
                _contactsNoFilter.Add(new ContactViewModel(newContact));

                SortList();
            };

            await _pageService.PushAsync(new ContactDetailPage(viewModel));
        }

        private async Task EditContact(ContactViewModel contactViewModel)
        {
            var viewModel = new ContactDetailPageViewModel(contactViewModel, _pageService);

            viewModel.ContactUpdated += (source, updatedContact) =>
            {
                var oldContact = _contactsNoFilter.Find(x => x.Equals(contactViewModel));
                oldContact.FirstName = updatedContact.FirstName;
                oldContact.LastName = updatedContact.LastName;
                oldContact.ContactNumber = updatedContact.ContactNumber;

                SortList();
            };

            await _pageService.PushAsync(new ContactDetailPage(viewModel));
        }


        private async Task DeleteContact(ContactViewModel contactViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No"))
            {
                Contacts.Remove(contactViewModel);
                _contactsNoFilter.Remove(contactViewModel);

                //Call Service or Persistence layer to delete the contact asynchonously
                //Ex: await _contactService.DeleteAsync(Map<Contact>(contactViewModel));
            }
        }

        public List<ContactViewModel> SanitizeList(IList<ContactViewModel> list)
        {
            return list.Where(x => string.IsNullOrWhiteSpace(x.FirstName) == false
                && string.IsNullOrWhiteSpace(x.LastName) == false
                && string.IsNullOrWhiteSpace(x.ContactNumber) == false)
                .ToList();
        }

        private void SortList()
        {
            _contactsNoFilter = _contactsNoFilter.OrderByDescending(x => x.IsFavorite).ThenBy(y => y.LastName).ToList();
            Contacts = new ObservableCollection<ContactViewModel>(_contactsNoFilter);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
        }

        #endregion
    }
}