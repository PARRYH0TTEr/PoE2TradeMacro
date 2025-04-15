using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoE2TradeMacro.Web.PriceCheck.Trade
{
    public class FetchRequest
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("complexity")]
        public int Complexity { get; set; }

        [JsonPropertyName("result")]
        public List<string> Result { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("inexact")]
        public bool Inexact { get; set; }
    }
}
