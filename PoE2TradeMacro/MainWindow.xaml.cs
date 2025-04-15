using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Net.Http;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

using PoE2TradeMacro.Parsing;
using PoE2TradeMacro.Util;
using PoE2TradeMacro.Parsing.Types;
using PoE2TradeMacro.Web.PriceCheck.Trade;

namespace PoE2TradeMacro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        static readonly HttpClient httpClient = new HttpClient();

        private readonly string tradeUrl = "https://www.pathofexile.com/trade2/search/poe2/Standard";
        private readonly string tradeAPIUrl = "https://www.pathofexile.com/api/trade2/search/poe2/Standard";    

        private string testJSONPayload = "{\"query\":{\"status\":{\"option\":\"online\"},\"stats\":[{\"type\":\"weight\",\"filters\":[{\"id\":\"explicit.stat_2557965901\",\"value\":{\"weight\":1},\"disabled\":false}]}]},\"sort\":{\"statgroup.0\":\"desc\"}}";
        private string tes2JSONPayload = "{\"query\":{\"status\":{\"option\":\"online\"},\"stats\":[{\"type\":\"and\",\"filters\":[{\"id\":\"explicit.stat_1050105434\",\"value\":{\"min\":34},\"disabled\":false}]}]}}";
        private string tes3JSONPayload = "{\"query\":{\"status\":{\"option\":\"online\"},\"filters\":{\"type_filters\":{\"filters\":{\"category\":{\"option\":\"weapon.onemelee\"}}},\"misc_filters\":{\"filters\":{\"ilvl\":{\"min\":50}}}}}}";
        private string tes4JSONPayload = "{\"query\":{\"status\":{\"option\":\"online\"},\"stats\":[{\"type\":\"and\",\"filters\":[{\"id\":\"explicit.stat_3321629045\",\"value\":{\"min\":24},\"disabled\":false},{\"id\":\"explicit.stat_3299347043\",\"value\":{\"min\":55},\"disabled\":false},{\"id\":\"explicit.stat_3981240776\",\"value\":{\"min\":32},\"disabled\":false},{\"id\":\"explicit.stat_328541901\",\"value\":{\"min\":29},\"disabled\":false},{\"id\":\"explicit.stat_3372524247\",\"value\":{\"min\":21},\"disabled\":false},{\"id\":\"explicit.stat_2923486259\",\"value\":{\"min\":23},\"disabled\":false}]}]}}";

        private TradeClient tradeClient;

        //private string jsonPayload = @"
        //                                {
        //                                  ""query"": {
        //                                    ""status"": {
        //                                      ""option"": ""online""
        //                                    },
        //                                    ""stats"": [
        //                                      {
        //                                        ""type"": ""weight"",
        //                                        ""filters"": [
        //                                          {
        //                                            ""id"": ""explicit.stat_2557965901"",
        //                                            ""value"": {
        //                                              ""weight"": 1
        //                                            },
        //                                            ""disabled"": false
        //                                          }
        //                                        ]
        //                                      }
        //                                    ]
        //                                  },
        //                                  ""sort"": {
        //                                    ""statgroup.0"": ""desc""
        //                                  }
        //                                }
        //                                ";




        //\"min\":34
        public MainWindow()
        {
            InitializeComponent();
            this.tradeClient = new TradeClient();
        }

        private async void HttpTestButton_Click(object sender, RoutedEventArgs e)
        {

            //string encodedPayload = HttpUtility.UrlEncode(testJSONPayload);
            //string requestUrl = $"{tradeAPIUrl}?q={encodedPayload}";

            ParsedItemReturnContainer pIRContainer = Parser.ParseItem(Constants.TESTING_ITEM_UniqueHelmet);

            TradeRequest tradeRequestInstance = Helper.MapParsedItemToTradeRequest(pIRContainer.parsedItemCopy);

            string tradeRequestJson = Helper.TemplateToJsonString(tradeRequestInstance);

            TradeResponse tradeRequestResponse = await tradeClient.PostTradeRequest(tradeRequestJson, Constants.TRADE_API_Search);

            if (!tradeRequestResponse.Successful) //Not successful
            {
                // Do something based on ErrorMessage, such as adjust timeout period to accommodate for rate limiting
            }

            FetchRequest fetchRequest = JsonSerializer.Deserialize<FetchRequest>(tradeRequestResponse.Content);

        }

        private void ParserTestButton_Click(object sender, RoutedEventArgs e)
        {
            //List<string> itemSections = Parser.ParseItemIntoSections();
            //List<string> sectionSubSections = Parser.ParseSectionIntoSubSections(itemSections[1]);

            //List<List<string>> itemContainer = Parser.ParseItem(Constants.TESTING_ITEM_UniqueHelmet);

            //List<List<string>> itemContainer = Parser.ParseItem(Constants.TESTING_ITEM_QualitySocketsBodyArmour);

            ParsedItemReturnContainer parsedItemReturnContainer = Parser.ParseItem(Constants.TESTING_ITEM_QualitySocketsBodyArmour);


            TradeRequest tradeRequestInstance = Helper.MapParsedItemToTradeRequest(parsedItemReturnContainer.parsedItemCopy);

            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };


            string tradeRequestJson = JsonSerializer.Serialize(tradeRequestInstance, serializationOptions);


            int test1 = 0;
            int test4 = 0;




            //parsedItemReturnContainer = Parser.ParseExplicits(itemContainer[5], parsedItemReturnContainer);

            //List<List<string>> itemContainer2 = Parser.ParseItem(Constants.TESTING_ITEM_Crossbow);

        }
    }
}