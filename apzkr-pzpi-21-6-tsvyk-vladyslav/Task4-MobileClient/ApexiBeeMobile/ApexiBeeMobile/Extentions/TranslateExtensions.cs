using ApexiBeeMobile.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;

namespace ApexiBeeMobile.Extentions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        readonly CultureInfo cultureInfo;
        const string resourceId = "ApexiBeeMobile.Resources.Resource";

        public TranslateExtension()
        {
            cultureInfo = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return "";

            ResourceManager resourceManager = new ResourceManager(resourceId,
                        typeof(TranslateExtension).GetTypeInfo().Assembly);

            var translation = resourceManager.GetString(Text, cultureInfo);

            if (translation == null)
            {
                translation = Text;
            }
            return translation;
        }
    }
}
