using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ContactManager.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> range) where T : class
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        }
    }
}
