using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Service.Twitter
{
    public class TwitterApiProvider : IApiProvider
    {
        private readonly IEncoder encoder;
        private readonly IJsonProvider jsonProvider;
        private string bearerToken;

        private readonly IVariableProvider variableProvider;

        public TwitterApiProvider(IEncoder encoder, IVariableProvider variableProvider, IJsonProvider jsonProvider)
        {
            this.variableProvider = variableProvider ??
                throw new ArgumentNullException(nameof(variableProvider));

            this.jsonProvider = jsonProvider ?? throw new ArgumentNullException(nameof(jsonProvider));

            this.encoder = encoder ??
                throw new ArgumentNullException(nameof(encoder));
        }

        public async Task<JArray> GetJSON(string url)
        {
            if (this.bearerToken == null)
            {
                await SetBearerToken();
            }

            WebRequest request = GetRequest(
                url, 
                "GET", 
                $"Bearer {this.bearerToken}", 
                "application/x-www-form-urlencoded;charset=UTF-8"
            );

            var json = jsonProvider.ParseToJArray(await this.GetResponse(request));

            return json;
        }

        private async Task SetBearerToken()
        {
            string consumerKey = this.variableProvider.GetValue("rtwtr_consumerKey");
            string consumerSecret = this.variableProvider.GetValue("rtwtr_consumerSecret");

            string bearerToken = this.encoder.Encode(consumerKey, consumerSecret);

            WebRequest request = this.GetRequest(
                "https://api.twitter.com/oauth2/token",
                "POST",
                $"Basic {bearerToken}",
                "application/x-www-form-urlencoded;charset=UTF-8",
                "grant_type=client_credentials"
            );

            string jsonResponse = await GetResponse(request);

            JObject parsedResponse = JObject.Parse(jsonResponse);

            this.bearerToken = parsedResponse["access_token"].ToString();
        }

        private WebRequest GetRequest(string url, string method, string authorization, string contentType, string body = null)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            request.Headers.Add("Authorization", authorization);
            request.ContentType = contentType;

            if (body != null)
            {
                byte[] requestBody = Encoding.UTF8.GetBytes(body);

                using(Stream stream = request.GetRequestStream())
                {
                    stream.Write(requestBody, 0, requestBody.Length);
                }
            }

            return request;
        }

        private async Task<string> GetResponse(WebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();

            string jsonResponse = string.Empty;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                using(StreamReader stream = new StreamReader(response.GetResponseStream()))
                {
                    jsonResponse = stream.ReadToEnd();
                }
            }

            return jsonResponse;
        }
    }
}