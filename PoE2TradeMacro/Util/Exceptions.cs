using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Util
{
    public class IncompleteParsingException : Exception
    {
        public IncompleteParsingException() : base("Parsing of itemSection was unsuccessful.") { }

        public IncompleteParsingException(string message) : base(message) { }

        public IncompleteParsingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
