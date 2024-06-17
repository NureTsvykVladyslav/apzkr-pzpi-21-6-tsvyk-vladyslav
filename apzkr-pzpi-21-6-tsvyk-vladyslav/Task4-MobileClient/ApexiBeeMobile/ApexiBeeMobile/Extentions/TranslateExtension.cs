using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using ApexiBeeMobile.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ApexiBeeMobile.Extentions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "ApexiBeeMobile.Resources.Resource";
        private readonly CultureInfo cultureInfo;

        public TranslateExtension()
        {
            cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            ResourceManager resourceManager = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resourceManager.GetString(Text, cultureInfo);

            if (translation == null)
            {
                translation = Text;
            }

            return translation;
        }
    }
}
