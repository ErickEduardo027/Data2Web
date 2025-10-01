using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.Utils
{
    public static class FormatHelper
    {
        
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        public static string Capitalize(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            return char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}
