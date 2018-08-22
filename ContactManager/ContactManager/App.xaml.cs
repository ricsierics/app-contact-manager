using ContactManager.Services;
using ContactManager.ViewModels;
using ContactManager.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ContactManager
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            var pageService = new PageService();
            var contactService = new ContactService();
            MainPage = new LoaderPage(new LoaderPageViewModel(pageService, contactService));
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
