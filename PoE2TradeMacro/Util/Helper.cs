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

        // The parameter is assumed to look something like this string literal "#-#"
        public static int GetAvgDamage(string damageInterval)
        {
            // 1. Split to string[]
            // 2. Convert to int[]
            // 3. Reduce to combined damage
            // 4. Divide for average damage
            int avgDamage = (Array.ConvertAll(damageInterval.Split("-"), int.Parse)
                                                           .Aggregate(0, (acc, x) => acc + x)) / 2;

            return avgDamage;
        }


    }
}
