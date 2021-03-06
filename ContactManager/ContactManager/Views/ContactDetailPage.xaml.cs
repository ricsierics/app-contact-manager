﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
		public ContactDetailPage (ContactDetailPageViewModel viewModel)
		{
			InitializeComponent ();

            BindingContext = viewModel;
		}
	}
}