using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PoE2TradeMacro.Web.API.Errors
{
    public class ErrorMessage
    {
        [JsonPropertyName("error")]
        public ErrorMessageBody Error;
    }


    public class ErrorMessageBody
    {
        [JsonPropertyName("code")]
        public int Code;

        [JsonPropertyName("message")]
        public string Message;

    }


}
