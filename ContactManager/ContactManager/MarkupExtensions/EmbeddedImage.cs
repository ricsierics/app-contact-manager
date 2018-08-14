using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ContactManager.MarkupExtensions
{
    //[ContentProperty("ResourceId")]
    public class EmbeddedImage : IMarkupExtension
    {
        public string ResourceId { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceId == null)
                return null;

            return ImageSource.FromResource(ResourceId);
        }
    }
}
