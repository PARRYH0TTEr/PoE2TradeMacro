using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing.Types
{
    public class AffixVals
    {

        public string? AffixId { get; set; }
        public int? min { get; set; }
        public int? max { get; set; }


        public AffixVals(string? AffixId = null, int? min  = null, int? max = null)
        {
            this.AffixId = AffixId;
            this.min = min;
            this.max = max;
        }
    }
}
