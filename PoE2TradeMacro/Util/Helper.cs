using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Util
{
    public static class Helper
    {

        public static string RemoveAll(string originalString, string[] toBeRemovedSubStrings)
        {
            string retString = originalString;

            foreach (string subString in toBeRemovedSubStrings)
            {
                retString = retString.Replace(subString, "");
            }

            return retString;
        }
    }
}
