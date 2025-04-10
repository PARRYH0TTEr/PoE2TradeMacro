using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoE2TradeMacro.Web.PriceCheck.Trade
{
    public class TradeClient
    {

        private readonly HttpClient httpClient;

        public TradeClient()
        {
            this.httpClient = new HttpClient();
        }



    }
}
