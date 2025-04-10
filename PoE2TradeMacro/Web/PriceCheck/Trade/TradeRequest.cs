using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Cryptography.X509Certificates;


namespace PoE2TradeMacro.Web.PriceCheck.Trade
{
    public class TradeRequest
    {
        [JsonPropertyName("query")]
        public Query Query { get; set; }
    }

    public class Query
    {
        [JsonPropertyName("status")]
        public Status? Status { get; set; }

        [JsonPropertyName("stats")]
        public List<StatFilterGroup>? Stats { get; set; }
    }

    public class Status
    {
        [JsonPropertyName("option")]
        public string? Option { get; set; }
    }

    public class StatFilterGroup
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("filters")]
        public List<StatFilter>? Filters { get; set; }
    }

    public class StatFilter
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("value")]
        public StatFilterValue Value { get; set; }

        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }
    }

    public class StatFilterValue
    {
        [JsonPropertyName("weight")]
        public int? Weight { get; set; }

        [JsonPropertyName("min")]
        public int? Min { get; set; }

        [JsonPropertyName("max")]
        public int? Max { get; set; }
    }
}

