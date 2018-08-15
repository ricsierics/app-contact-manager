using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public class ContactsPageViewModel : BaseViewModel
    {
        public string TabTitle { get; private set; } = "CONTACTS";
        
        public int ContactsCount
        {
            get { return Contacts?.Count ?? 0; }
        }

        public ObservableCollection<ContactViewModel> Contacts { get; set; } //= new ObservableCollection<ContactViewModel>();

        public ICommand CallCommand { get; private set; }
        public ICommand AddToFavoriteCommand { get; private set; }
        public ICommand AddContactCommand { get; private set; }
        public ICommand EditContactCommand { get; private set; }
        public ICommand DeleteContactCommand { get; private set; }

        public ContactsPageViewModel()
        {
            CallCommand = new Command<string>((cn) => Call(cn));
            AddToFavoriteCommand = new Command<ContactViewModel>((c) => AddToFavorite(c));
            AddContactCommand = new Command(() => AddContact());
        }

        public void Call(string contactNumber)
        {
            System.Diagnostics.Debug.WriteLine(contactNumber);
        }

        public void AddToFavorite(ContactViewModel contact)
        {
            System.Diagnostics.Debug.WriteLine("Add To Favorite");
        }

        public void AddContact()
        {
            System.Diagnostics.Debug.WriteLine("Add");
        }
    }
}
