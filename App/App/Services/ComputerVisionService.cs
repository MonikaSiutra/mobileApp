using System.Collections.Generic;
using System.Web;
using RestSharp;
using RestSharp.Serialization.Json;

namespace App.Services
{
    public interface IComputerVisionService
    {
        ComputerVision DescribeImage(string url);
    }

    public class ComputerVisionService : IComputerVisionService
    {
        private readonly string _apiAddress = @"https://westeurope.api.cognitive.microsoft.com/vision/v1.0";
        string key1 = "1afbb0266ba649598e9798bd2d64a5fa";

        public ComputerVision DescribeImage(string url)
        {
            var client = new RestClient(_apiAddress);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["maxCandidates"] = "1";

            var request = new RestRequest("describe?" + queryString, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader(@"Ocp-Apim-Subscription-Key", key1);
            request.AddBody(new { Url = url });

            var deserializer = new JsonDeserializer();

            var response2 = client.Execute(request);
            var imageDescription = deserializer.Deserialize<ComputerVision>(response2);

            return imageDescription;
        }
    }

    public class Image
    {
        public string Url { get; set; }

        public ComputerVision ComputerVision { get; set; }
    }

    public class Caption
    {
        public string text { get; set; }
        public double confidence { get; set; }
    }

    public class Description
    {
        public List<string> tags { get; set; }
        public List<Caption> captions { get; set; }
    }

    public class Metadata
    {
        public int height { get; set; }
        public int width { get; set; }
        public string format { get; set; }
    }

    public class ComputerVision
    {
        public Description description { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
    }
}