using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
		public ContactsPage (ContactsPageViewModel viewModel)
		{
            ViewModel = viewModel;
			InitializeComponent ();
		}

        public ContactsPageViewModel ViewModel
        {
            get { return BindingContext as ContactsPageViewModel; }
            set { BindingContext = value; }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.SearchContactCommand.Execute(e.NewTextValue);
        }
    }
}