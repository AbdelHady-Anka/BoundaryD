using BoundaryDetector.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoundaryDetector.Services
{
    public class RestService
    {
        private readonly RestClient _client;
        public RestService()
        {
            _client = new RestClient("URL_OF_YOUR_API");

        }

        public async Task<BoundaryDetails> Post(double longitude, double latitude)
        {

            _client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("apikey", "YOUR_API_KEY");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("accuracy", "3");
            request.AddParameter("latitude", latitude);
            request.AddParameter("longitude", longitude);
            request.AddParameter("fallback_boundary", "false");
            IRestResponse response = await _client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
            
            var result = JsonConvert.DeserializeObject<BoundaryDetails>(response.Content);

            return result;
        }
    }
}
