using System.Threading.Tasks;
using Xamarin.Forms;
using ContactManager.Services;

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
            Color violetColor = (Color)Application.Current.Resources["Violet"];
            Color whiteColor = (Color)Application.Current.Resources["White"];
            Application.Current.MainPage = new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromRgb(violetColor.R, violetColor.G, violetColor.B),
                BarTextColor = Color.FromRgb(whiteColor.R, whiteColor.G, whiteColor.B)
            };
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
