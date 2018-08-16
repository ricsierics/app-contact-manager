using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using ContactManager.Views;
using System.ComponentModel;
using System.Linq;

namespace ContactManager.ViewModels
{
    public class ContactsPageViewModel : INotifyPropertyChanged
    {
        private IPageService _pageService;
        private List<ContactViewModel> _contactsFull { get; set; }
                
        public ObservableCollection<ContactViewModel> Contacts { get; private set; }

        public ICommand SearchContactCommand { get; private set; }
        public ICommand CallCommand { get; private set; }
        public ICommand AddToFavoriteCommand { get; private set; }
        public ICommand AddContactCommand { get; private set; }
        public ICommand EditContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ContactsPageViewModel(IPageService pageService, List<ContactViewModel> contactsFull)
        {
            _pageService = pageService;

            SearchContactCommand = new Command<string>((n) => SearchContact(n));
            CallCommand = new Command<string>((c) => Call(c));
            AddToFavoriteCommand = new Command<ContactViewModel>((c) => AddToFavorite(c));
            AddContactCommand = new Command(async () => await AddContact());

            EditContactCommand = new Command<ContactViewModel>(async (c) => await EditContact(c));
            DeleteContactCommand = new Command<ContactViewModel>(async (c) => await DeleteContact(c));

            _contactsFull = contactsFull;
            Contacts = new ObservableCollection<ContactViewModel>(contactsFull);
        }

        private void SearchContact(string keywordSearch)
        {
            if (string.IsNullOrWhiteSpace(keywordSearch.Trim()))
            {
                Contacts = new ObservableCollection<ContactViewModel>(_contactsFull);
            }
            else
            {   
                var filteredContacts = _contactsFull.Where(x => x.FullName.ToUpper().Contains(keywordSearch.Trim().ToUpper()));
                Contacts = new ObservableCollection<ContactViewModel>(filteredContacts);
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Contacts"));
        }

        private void Call(string contactNumber)
        {
            System.Diagnostics.Debug.WriteLine(contactNumber);
        }

        private void AddToFavorite(ContactViewModel contactViewModel)
        {
            System.Diagnostics.Debug.WriteLine("Add To Favorite");
        }

        private async Task AddContact()
        {
            var viewModel = new ContactDetailViewModel(new ContactViewModel(), _pageService);

            viewModel.ContactAdded += (source, newContact) => 
            {
                Contacts.Add(new ContactViewModel(newContact));
                _contactsFull.Add(new ContactViewModel(newContact));
            };

            await _pageService.PushAsync(new ContactDetailPage(viewModel));
        }

        private async Task EditContact(ContactViewModel contactViewModel)
        {
            var viewModel = new ContactDetailViewModel(contactViewModel, _pageService);

            viewModel.ContactUpdated += (source, updatedContact) =>
            {
                contactViewModel.FirstName = updatedContact.FirstName;
                contactViewModel.LastName = updatedContact.LastName;
                contactViewModel.ContactNumber = updatedContact.ContactNumber;
            };

            await _pageService.PushAsync(new ContactDetailPage(viewModel));
        }


        private async Task DeleteContact(ContactViewModel contactViewModel)
        {
            if (await _pageService.DisplayAlert("Warning", $"Are you sure you want to delete {contactViewModel.FullName}?", "Yes", "No"))
            {
                Contacts.Remove(contactViewModel);
                _contactsFull.Remove(contactViewModel);

                //Call Service or Persistence layer to delete the contact asynchonously
                //Ex: await _contactService.DeleteAsync(Map<Contact>(contactViewModel));
            }
        }
    }
}
