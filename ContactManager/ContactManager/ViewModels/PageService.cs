using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public class PageService : IPageService
    {
        public async Task PushAsync(Page page)
        {   
            await MainPage.Navigation.PushAsync(page);
        }

        public void PushNew(Page page)
        {
            Application.Current.MainPage = new NavigationPage(page);
        }

        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        public void SetCurrentPage(Page page)
        {   
            Application.Current.MainPage = page;
        }

        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
