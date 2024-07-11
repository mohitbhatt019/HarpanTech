using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ApiGateway.Helpers
{
    public class AzureFunctionHelper
    {


        string TargetUrl = "";

        public AzureFunctionHelper(string url)
        {
            TargetUrl = url;
        }



        public async Task<S> Post<T, S>(T request) where S : new()
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            S responseModel = new S();

            string requestObj = JsonConvert.SerializeObject(request);
            var url = new Uri(TargetUrl);
            HttpContent content = new StringContent(requestObj, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var result = await client.PostAsync(url, content);

            if (result.IsSuccessStatusCode)
            {
                responseModel = JsonConvert.DeserializeObject<S>(await result.Content.ReadAsStringAsync());
            }
            return responseModel;
        }

        // esponseModel = await result.Content.ReadAsAsync<S>();
    }
}

