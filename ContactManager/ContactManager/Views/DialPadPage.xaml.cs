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
	public partial class DialPadPage : ContentPage
	{
		public DialPadPage (DialPadPageViewModel viewModel)
		{
            BindingContext = viewModel;

            InitializeComponent ();
		}
	}
}