using System.ComponentModel;
using ContactManager.Models;

namespace ContactManager.ViewModels
{
    public class ContactViewModel : INotifyPropertyChanged
    {   
        public ContactViewModel() {}

        public ContactViewModel(Contact c)
        {
            _firstName = c.FirstName;
            LastName = c.LastName;
            ContactNumber = c.ContactNumber;
            IsFavorite = c.IsFavorite;
        }

        public string ContactNumber { get; set; }
        
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

        private bool _isFavorite;
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                if (_isFavorite != value)
                {
                    _isFavorite = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFavorite"));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsFavoriteImagePath"));
                }
            }
        }

        public string IsFavoriteImagePath
        {
            get
            {
                return IsFavorite ? "favorite_active.png" : "favorite_inactive.png";
            }
        }
        
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
