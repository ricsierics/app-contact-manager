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
        public HomePage ()
        {
            Children.Add(new ContactsPage(new ContactsPageViewModel()));

            Children.Add(new DialPadPage(new DialPadPageViewModel()));

            InitializeComponent();
        }
    }
}