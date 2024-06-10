using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using ApexiBeeMobile.Resources;

namespace ApexiBeeMobile.Localisation
{
    public static class Localize
    {
        private static readonly ResourceManager ResourceManager = new ResourceManager(typeof(Resource));

        public static string GetString(string key)
        {
            return ResourceManager.GetString(key, CultureInfo.CurrentCulture);
        }
    }
}
