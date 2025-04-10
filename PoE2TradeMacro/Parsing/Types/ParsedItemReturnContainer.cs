using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Parsing.Types
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

        public ParsedItemReturnContainer(ParsedItemReturnContainer other)
        {
            this.parseStatus = other.parseStatus;
            this.parsedItemCopy = new ParsedItem(other.parsedItemCopy);
        }
    }
}
