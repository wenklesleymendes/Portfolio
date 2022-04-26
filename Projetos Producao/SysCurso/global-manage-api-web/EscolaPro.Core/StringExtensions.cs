using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace EscolaPro.Core
{
    public static class StringExtensions
    {
        public static String ToTitleCase(this string _value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_value);
        }
    }
}
