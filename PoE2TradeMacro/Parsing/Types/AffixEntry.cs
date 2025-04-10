using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing.Types
{
    public class AffixEntry
    {
        public string? modId = null;
        public AffixEntryValue? value = null;
        public bool? disabled = null;
    }


    public class AffixEntryValue
    {
        public int? weight = null;
        public int? min = null;
        public int? max = null;
    }
}
