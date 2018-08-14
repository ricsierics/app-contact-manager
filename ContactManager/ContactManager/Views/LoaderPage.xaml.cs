using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;
using ContactManager.Services;

namespace ContactManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoaderPage : ContentPage
	{
		public LoaderPage ()
		{
            var pageService = new PageService();
            var contactService = new ContactService();
            ViewModel = new LoaderPageViewModel(pageService, contactService);

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