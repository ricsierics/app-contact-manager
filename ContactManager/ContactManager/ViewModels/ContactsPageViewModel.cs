using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.ViewModels
{
    public class ContactsPageViewModel : BaseViewModel
    {
        public string Tab { get; private set; } = "CONTACTS";
        public string Title { get; private set; } = "Contacts Page";
    }
}
