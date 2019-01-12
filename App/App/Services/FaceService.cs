using System.Collections.Generic;
using System.Web;
using RestSharp;
using RestSharp.Serialization.Json;

namespace App.Services
{
    public interface IFaceService
    {
        List<Face> FaceDetect(string url);
    }

    public class FaceService : IFaceService
    {
        string apiAddress = "https://westeurope.api.cognitive.microsoft.com/face/v1.0";
        string key1 = "1c0f1a51e89f4d1db0a5bebc3bdac8d0";

        public List<Face> FaceDetect(string url)
        {
            var client = new RestClient(apiAddress);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["returnFaceId"] = "true";
            queryString["returnFaceLandmarks"] = "false";
            queryString["returnFaceAttributes"] = "smile";
            
            var request = new RestRequest("detect?" + queryString, Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader(@"Ocp-Apim-Subscription-Key", key1);

            request.AddBody(new { Url = url });

            JsonDeserializer deserializer = new JsonDeserializer();

            var response2 = client.Execute(request);
            var faces = deserializer.Deserialize<List<Face>>(response2);

            return faces;
        }
    }

    public class FaceRectangle
    {
        public int top { get; set; }
        public int left { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Emotion
    {
        public int anger { get; set; }
        public double contempt { get; set; }
        public int disgust { get; set; }
        public int fear { get; set; }
        public double happiness { get; set; }
        public double neutral { get; set; }
        public double sadness { get; set; }
        public int surprise { get; set; }
    }

    public class FaceAttributes
    {
        public double smile { get; set; }
        public string gender { get; set; }
        public int age { get; set; }
        public Emotion emotion { get; set; }
    }

    public class Face
    {
        public string faceId { get; set; }
        public FaceRectangle faceRectangle { get; set; }
        public FaceAttributes faceAttributes { get; set; }
    }
}