using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RTWTR.Infrastructure.Contracts;

namespace RTWTR.Infrastructure
{
    public class HeaderGenerator : IHeaderGenerator
    {
        private string consumerKey;
        private string consumerSecret;
        private string accessToken;
        private string accessSecret;
        private string version = "1.0";
        private string signatureMethod = "HMAC-SHA1";
        private readonly IVariableProvider variableProvider;

        public HeaderGenerator(IVariableProvider variableProvider)
        {
            this.variableProvider = variableProvider ?? throw new ArgumentNullException(nameof(variableProvider));

            this.consumerKey = this.variableProvider.GetValue("rtwtr_consumerKey");
            this.consumerSecret = this.variableProvider.GetValue("rtwtr_consumerSecret");
            this.accessToken = this.variableProvider.GetValue("rtwtr_accessToken");
            this.accessSecret = this.variableProvider.GetValue("rtwtr_accessSecret");
        }
        public async Task<string> GenerateHeader(string url, List<string> parameters)
        {
            Task<string> nonce = GetNonce();
            Task<string> timeStamp = GetTimeStamp();
            Task<string> baseString = GetBaseString(url, await nonce, await timeStamp, parameters);
            Task<string> signature = GetSignature(await baseString, url);

            string header = string.Concat(
                "OAuth ",
                $"oauth_consumer_key=\"{this.variableProvider.GetValue("rtwtr_consumerKey")}\"",
                $"oauth_nonce=\"{nonce}\"",
                $"oauth_signature=\"{signature}\"",
                $"oauth_signature_method=\"{this.signatureMethod}\"",
                $"oauth_timestamp=\"{timeStamp}\"",
                $"oauth_token=\"{this.accessToken}\"",
                $"oauth_version=\"{this.version}\""
            );

            return header;
        }

        private Task<string> GetNonce()
        {
            return new Task<string>(
                () => Convert.ToBase64String(
                    new ASCIIEncoding().GetBytes(
                        DateTime.Now.Ticks.ToString()
                    )
                )
            );
        }

        private Task<string> GetTimeStamp()
        {
            return new Task<string>(
                () => {
                    TimeSpan timeSpan = DateTime.Now - default(DateTime);

                    return Convert.ToInt64(timeSpan.TotalSeconds).ToString();
                }
            );
        }

        private Task<string> GetBaseString(string url, string nonce, string timeStamp, List<string> parameters)
        {
            List<string> result = new List<string>();

            result.Add($"oauth_consumer_key={this.consumerKey}");
            result.Add($"oauth_nonce={nonce}");
            result.Add($"oauth_signature_method={this.signatureMethod}");
            result.Add($"oauth_timestamp={timeStamp}");
            result.Add($"oauth_token={this.accessToken}");
            result.Add($"oauth_version={this.version}");

            if (parameters != null)
            {
                result.AddRange(parameters);
            }

            result.Sort();

            string baseString = string.Join("&", result);

            return new Task<string>(
                () => string.Concat(
                    "GET&",
                    Uri.EscapeDataString(url),
                    "&",
                    Uri.EscapeDataString(baseString)
                )
            );
        }

        private Task<string> GetSignature(string baseString, string url)
        {
            string signInKey = string.Concat(
                Uri.EscapeDataString(this.consumerSecret),
                "&",
                Uri.EscapeDataString(this.accessSecret)
            );

            HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signInKey));

            return new Task<string>(            
                () => Convert.ToBase64String(
                    hasher.ComputeHash(
                        ASCIIEncoding.ASCII.GetBytes(baseString)
                    )
                )
            );
        }
    }
}