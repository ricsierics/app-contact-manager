using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactManager.Services
{
    public interface IPageService
    {
        void PushNew(Page page);

        Task PushAsync(Page page);

        Task<Page> PopAsync();

        void SetCurrentPage(Page page);

        Task DisplayAlert(string title, string message, string ok);

        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
    }
}
