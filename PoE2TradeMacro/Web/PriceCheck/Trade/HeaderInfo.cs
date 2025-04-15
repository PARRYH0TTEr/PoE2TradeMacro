using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoE2TradeMacro.Web.PriceCheck.Trade
{
    public class HeaderInfo
    {
        [JsonPropertyName("UserAgent")]
        public string UserAgent { get; set; }

        [JsonPropertyName("PoeSessId")]
        public string PoeSessId { get; set; }
    }
}
