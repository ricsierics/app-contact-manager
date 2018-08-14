using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using ContactManager.ViewModels;

namespace ContactManager.Views
{
    public abstract class BaseTabbedPage<T> : TabbedPage where T : BaseViewModel
    {
        public T ViewModel
        {
            get { return base.BindingContext as T; }
        }

        public new T BindingContext
        {
            set
            {
                base.BindingContext = value;
                base.OnPropertyChanged("BindingContext");
            }
        }

        protected BaseTabbedPage()
        {
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = ViewModel;
        }
    }
}
