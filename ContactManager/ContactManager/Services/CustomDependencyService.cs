using Xamarin.Forms;

/*
 * Source: http://www.devsdna.com/blog/ArticleID/11/Unit-Testing-with-Xamarin-Forms-DependencyService
 */

namespace ContactManager.Services
{
    public class CustomDependencyService : IDependencyService
    {
        public T Get<T>() where T : class
        {
            return DependencyService.Get<T>();
        }
    }
}
