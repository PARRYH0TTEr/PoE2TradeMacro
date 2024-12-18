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

using PoE2TradeMacro.Parsing;

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



        public MainWindow()
        {
            InitializeComponent();
        }

        private async void HttpTestButton_Click(object sender, RoutedEventArgs e)
        {

            //string encodedPayload = HttpUtility.UrlEncode(testJSONPayload);
            //string requestUrl = $"{tradeAPIUrl}?q={encodedPayload}";

            try
            {
                var poeSessionID = POESESSID_TextBox.Text;
                var userAgent = UserAgent_TextBox.Text;

                httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
                httpClient.DefaultRequestHeaders.Add("Cookie", $"POESESSID={poeSessionID}");

                var requestContent = new StringContent(testJSONPayload, Encoding.UTF8, "application/json");

                using HttpResponseMessage response = await httpClient.PostAsync(tradeAPIUrl, requestContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.Write(ex.Message);
            }
        }

        private void ParserTestButton_Click(object sender, RoutedEventArgs e)
        {
            //List<string> itemSections = Parser.ParseItemIntoSections();
            //List<string> sectionSubSections = Parser.ParseSectionIntoSubSections(itemSections[1]);

            //List<List<string>> itemContainer = Parser.ParseItem(Parser.testUniqueHelmetString);
            List<List<string>> itemContainer = Parser.ParseItem(Parser.testWaystoneString);



        }
    }
}