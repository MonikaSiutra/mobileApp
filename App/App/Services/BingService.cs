using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using RestSharp;
using RestSharp.Serialization.Json;

namespace App.Services
{
    public interface IBingService
    {
        string GetImage(string query);
    }


    public class BingService : IBingService
    {
        private string _apiUrl = @"https://api.cognitive.microsoft.com/bing/v7.0/images";
        private string key = "fd716cf8bf934d2fb05811b7d96b4b4d";

        public string GetImage(string query)
        {
            var restCLient = new RestClient(_apiUrl);

            //var queryString = HttpUtility.ParseQueryString(string.Empty);
            //queryString["q"] = query;

            var request = new RestRequest($"search?q={query}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Ocp-Apim-Subscription-Key", key);

            var result = restCLient.Execute(request);

            var deserializer = new JsonDeserializer();

            var imageResponse = deserializer.Deserialize<ImageResponse>(result);

            return imageResponse.value.FirstOrDefault().contentUrl;
        }
    }

    public class ImageResponse
    {
        public string _type { get; set; }
        public Instrumentation instrumentation { get; set; }
        public string readLink { get; set; }
        public string webSearchUrl { get; set; }
        public Querycontext queryContext { get; set; }
        public int totalEstimatedMatches { get; set; }
        public int nextOffset { get; set; }
        public List<Value> value { get; set; }
    }

    public class Instrumentation
    {
        public string _type { get; set; }
    }

    public class Querycontext
    {
        public string originalQuery { get; set; }
        public string alterationDisplayQuery { get; set; }
        public string alterationOverrideQuery { get; set; }
        public string alterationMethod { get; set; }
        public string alterationType { get; set; }
    }

    public class Value
    {
        public string webSearchUrl { get; set; }
        public string name { get; set; }
        public string thumbnailUrl { get; set; }
        public DateTime datePublished { get; set; }
        public string contentUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string contentSize { get; set; }
        public string encodingFormat { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string imageInsightsToken { get; set; }
        public Insightsmetadata insightsMetadata { get; set; }
        public string imageId { get; set; }
        public string accentColor { get; set; }
        public string creativeCommons { get; set; }
    }

    public class Thumbnail
    {
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Insightsmetadata
    {
        public int pagesIncludingCount { get; set; }
        public int availableSizesCount { get; set; }
    }

}
