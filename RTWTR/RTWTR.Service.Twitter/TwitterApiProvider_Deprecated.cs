using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Service.Twitter
{
    // This class is Deprecated and is NOT to be used
    public class TwitterApiProvider_Deprecated : IApiProvider
    {
        private readonly IHeaderGenerator headerGenerator;

        public TwitterApiProvider_Deprecated(IHeaderGenerator headerGenerator)
        {
            this.headerGenerator = headerGenerator ?? throw new ArgumentNullException(nameof(headerGenerator));
        }

        public string GetJSON(string url)
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