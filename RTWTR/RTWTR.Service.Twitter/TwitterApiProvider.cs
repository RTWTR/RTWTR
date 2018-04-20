using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Service.Twitter
{
    public class TwitterApiProvider : IApiProvider
    {
        private readonly string baseUrl;

        private readonly IHeaderGenerator headerGenerator;

        public TwitterApiProvider(IHeaderGenerator headerGenerator)
        {
            this.headerGenerator = headerGenerator ?? throw new ArgumentNullException(nameof(headerGenerator));
            this.baseUrl = "https://api.twitter.com/1.1/";
        }

        public Task<string> GetSingleTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSingleUserJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserTimelineJSON(string id)
        {
            throw new NotImplementedException();
        }

        public Task<string> SearchTweetJSON(string id)
        {
            throw new NotImplementedException();
        }

        public string SearchUserJSON(string handle)
        {
            string additional = $"users/search.json?q={handle}";

            return this.GetJSON(this.baseUrl + additional);
        }

        private string GetJSON(string url)
        {
            List<string> parameters;
            if (url.Contains("?"))
            {
                parameters = GetQueryParameters(url);
                url = url.Substring(0, url.IndexOf('?'));
            }
            else 
            {
                parameters = null;
            }

            string header = this.headerGenerator.GenerateHeader(url, parameters);

            WebResponse response = SendRequest(url, header, parameters);

            string responseData = string.Empty;

            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                responseData = stream.ReadToEnd();
            }

            return responseData;
        }

        private List<string> GetQueryParameters(string url) 
        {
            List<string> result = new List<string>();

            string queryString = url.Substring(url.IndexOf('?') + 1);
            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(queryString);

            foreach (string param in nameValueCollection)
            {
                result.Add(
                    string.Concat(
                        param, 
                        "=",
                        Uri.EscapeDataString(nameValueCollection[param])
                    )
                );
            }

            return result;
        }

        private string GetRequestBody(List<string> parameters)
        {
            return string.Join("&", parameters);
        }

        private WebResponse SendRequest(string url, string header, List<string> parameters)
        {
            ServicePointManager.Expect100Continue = false;
            string requestBody = string.Empty;

            if (parameters != null)
            {
                requestBody = GetRequestBody(parameters);
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                string.Concat(
                    url,
                    "?",
                    requestBody
                )
            );

            // Add the generated OAuth header
            request.Headers.Add("Authorization", header);

            // Set the request method
            request.Method = "GET";

            // Set the content type
            request.ContentType = "application/x-www-form-urlencoded";

            return request.GetResponse();
        }
    }
}