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

        public void SetCurrentPage(Page page)
        {   
            Application.Current.MainPage = page;
        }

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
    }
}
