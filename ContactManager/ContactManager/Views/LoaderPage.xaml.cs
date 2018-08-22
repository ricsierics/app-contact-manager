using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoaderPage : ContentPage
	{
		public LoaderPage (LoaderPageViewModel viewModel)
		{
            ViewModel = viewModel;
            InitializeComponent ();
		}

        public LoaderPageViewModel ViewModel
        {
            get { return BindingContext as LoaderPageViewModel; }
            set { BindingContext = value; }
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);

            base.OnAppearing();
        }
    }
}