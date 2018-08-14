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

    public abstract class Tab1PageXaml : BaseTabbedPage<BaseViewModel> { }

    public partial class TabbedPage1 : Tab1PageXaml
    {
        public TabbedPage1 ()
        {
            BindingContext = new ContactsPageViewModel();

            Children.Add(new ContactsPage(new ContactsPageViewModel()));

            BindingContext = new DialPadPageViewModel();

            Children.Add(new DialPadPage(new DialPadPageViewModel()));

            InitializeComponent();
        }
    }
}