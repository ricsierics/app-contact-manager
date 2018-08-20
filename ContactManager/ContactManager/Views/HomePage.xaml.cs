using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : TabbedPage
    {
        public HomePage (ContactsPageViewModel contactsPageVM, DialPadPageViewModel dialPadPageVM)
        {
            Children.Add(new ContactsPage(contactsPageVM));

            Children.Add(new DialPadPage(dialPadPageVM));

            InitializeComponent();
        }
    }
}