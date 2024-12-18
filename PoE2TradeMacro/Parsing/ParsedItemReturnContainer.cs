using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing
{
    
    public class ParsedItemReturnContainer
    {
        public bool parseStatus;
        public ParsedItem parsedItemCopy;

        public ParsedItemReturnContainer()
        {
            parseStatus = false;
            parsedItemCopy = new ParsedItem();
        }
    }
}
