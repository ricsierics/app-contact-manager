using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ContactManager.ViewModels
{
    public interface IPageService
    {
        Task PushAsync(Page page);

        void SetCurrentPage(Page page);
    }
}
