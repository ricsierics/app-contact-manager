using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    public class ContactViewModel : INotifyPropertyChanged
    {   
        public string ContactNumber { get; set; }
        public bool IsFavorite { get; set; }

        public ContactViewModel() {}

        public ContactViewModel(Contact c)
        {
            _firstName = c.FirstName;
            LastName = c.LastName;
            ContactNumber = c.ContactNumber;
            IsFavorite = c.IsFavorite;
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FullName"));
                }
                
            }
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
