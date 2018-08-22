namespace ContactManager.Models
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public bool IsFavorite { get; set; }

        public Contact Trim()
        {
            FirstName = FirstName.Trim();
            LastName = LastName.Trim();
            ContactNumber = ContactNumber.Trim();
            return this;
        }
    }
}
