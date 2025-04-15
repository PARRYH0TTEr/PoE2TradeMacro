using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoE2TradeMacro.Web.API.Schemas
{
    public class statsSchema
    {
        [JsonPropertyName("result")]
        public List<StatCategory> Result { get; set; }
    }

    public class StatCategory
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("entries")]
        public List<StatEntry> Entries { get; set; }
    }

    public class StatEntry
    {
        [JsonPropertyName("id")]
        public string ID { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

}
