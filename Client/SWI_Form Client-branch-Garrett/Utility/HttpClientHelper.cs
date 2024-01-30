using System.Text;

namespace SWI_Form_Client.Utility
{
    public static class HttpClientHelper
    {
        static readonly HttpClient client = new HttpClient();

        static readonly string baseUrl = "https://localhost:5001/";

        /// <summary>
        /// Sends a get request to the API at endpoint "method" with "header"s enabled/disabled and an authorization "token"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="header"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> Get(string? data, string method, bool header, string? token)
        {
            string url = baseUrl + method;
            string output = string.Empty;
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    if (header)
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                    }

                    var response = await client.SendAsync(request).ConfigureAwait(true);

                    output = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                }

                return output;
            }
            catch (Exception exception)
            {
                Console.WriteLine("HttpClientHelper " + exception.Message);
                return output;
            }
        }

        /// <summary>
        /// Sends a post request to the API at endpoint "method" with JSON "data", "header"s enabled/disabled and an authorization "token"
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <param name="header"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> Post(string? data, string method, bool header, string? token)
        {
            string url = baseUrl + method;
            string output = string.Empty;
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    HttpContent input = new StringContent(data, Encoding.UTF8, "application/json");
                    if (header)
                    {
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                    }

                    request.Content = input;

                    var response = await client.SendAsync(request).ConfigureAwait(true);

                    output = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                }

                return output;
            }
            catch (Exception exception)
            {
                Console.WriteLine("HttpClientHelper " + exception.Message);
                return output;
            }
        }
    }
}
