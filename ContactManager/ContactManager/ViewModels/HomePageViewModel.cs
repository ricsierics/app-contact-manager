using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManager.ViewModels
{
    public class HomePageViewModel
    {
        public ContactsPageViewModel ContactsPageViewModel { get; set; }

        public DialPadPageViewModel DialPadPageViewModel { get; set; }
    }
}
