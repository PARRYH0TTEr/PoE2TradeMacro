using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PoE2TradeMacro.Web.API.Errors;

namespace PoE2TradeMacro.Web.PriceCheck.Trade
{
    public class TradeClient
    {

        private readonly HttpClient httpClient;


        // TODO:
        // Pull .json databases on launch of program instead of keeping local copy
        public TradeClient()
        {
            this.httpClient = new HttpClient();

            string headerInfoFilePath = Path.Combine(AppContext.BaseDirectory, "Web", "PriceCheck", "Trade", "header.json");

            HeaderInfo headerInfo = JsonSerializer.Deserialize<HeaderInfo>(File.ReadAllText(headerInfoFilePath));

            this.httpClient.DefaultRequestHeaders.Add("User-Agent", headerInfo.UserAgent);
        }

        // Sends a POST request to the specified url(endpoint), with a given payload
        public async Task<TradeResponse> PostTradeRequest(string payload, string urlEndpoint)
        {

            string responseBody = string.Empty;

            try
            {
                var requestContent = new StringContent(payload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.httpClient.PostAsync(urlEndpoint, requestContent);
                
                responseBody = await response.Content.ReadAsStringAsync();

                //response.EnsureSuccessStatusCode(); // Succeed if the status-code is in the range 200-299


                // We consider the 202 "Accepted" response code as an error for now, since we don't yet know if polling
                //  is supported. Suppose we consider 202 as a good response. As a result, the user will either have to
                //  wait until the response comes through (i.e. tradesite prices) or will later on (perhaps in the
                //  background) receive the response, but how should that then be represented? Lots of things to consider,
                //  and in most cases, handling 202 will feel slow/sluggish for the user.
                if (response.StatusCode == System.Net.HttpStatusCode.OK) 
                {

                    return new TradeResponse
                    {
                        Successful = true,
                        Content = responseBody,
                        Error = null,
                        Httprm = new HTTPRM
                        {
                            StatusCode = (int)response.StatusCode,
                            ReasonPhrase = response.ReasonPhrase
                        }
                    };
                }

                ErrorMessage errMessage = JsonSerializer.Deserialize<ErrorMessage>(responseBody);

                return new TradeResponse
                {
                    Successful = false,
                    Content = responseBody,
                    Error = errMessage,
                    Httprm = new HTTPRM
                    {
                        StatusCode = (int)response.StatusCode,
                        ReasonPhrase = response.ReasonPhrase
                    }
                };
                

            }
            catch (Exception ex)
            {
                //TODO: Write exception to log-file

                return new TradeResponse
                {
                    Successful = false,
                    Content = responseBody,
                    Error = null,
                    Httprm = null
                };

            }
            
        }

    }
}
