using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage (ContactsPageViewModel contactsPageVM)
        {
            Children.Add(new ContactsPage(contactsPageVM));

            Children.Add(new DialPadPage(new DialPadPageViewModel()));

            InitializeComponent();
        }
    }
}