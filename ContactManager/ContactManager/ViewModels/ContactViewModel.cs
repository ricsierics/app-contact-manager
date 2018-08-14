using System;
using System.Collections.Generic;
using System.Text;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    public class ContactViewModel : BaseViewModel
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
            set { _firstName = value; } //TODO: add on property changed
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FullName {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
