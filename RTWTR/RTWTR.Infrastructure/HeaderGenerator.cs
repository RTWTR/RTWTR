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
            this.variableProvider = variableProvider
                ??
                throw new ArgumentNullException(nameof(variableProvider));

            this.consumerKey = this.variableProvider.GetValue("rtwtr_consumerKey");
            this.consumerSecret = this.variableProvider.GetValue("rtwtr_consumerSecret");
            this.accessToken = this.variableProvider.GetValue("rtwtr_accessToken");
            this.accessSecret = this.variableProvider.GetValue("rtwtr_accessSecret");
        }

        public string GenerateHeader(string url, List<string> parameters)
        {
            string nonce = GetNonce();
            string timeStamp = GetTimeStamp();
            string baseString = GetBaseString(url, nonce, timeStamp, parameters);
            string signature = GetSignature(baseString, url);

            string escapedConsumerKey = Uri.EscapeDataString(this.variableProvider.GetValue("rtwtr_consumerKey"));
            string escapedNonce = Uri.EscapeDataString(nonce);
            string escapedSignature = Uri.EscapeDataString(signature);
            string escapedSignatureMethod = Uri.EscapeDataString(this.signatureMethod);
            string escapedTimeStamp = Uri.EscapeDataString(timeStamp);
            string escapedAccessToken = Uri.EscapeDataString(this.variableProvider.GetValue("rtwtr_accessToken"));
            string escapedVersion = Uri.EscapeDataString(this.version);

            string header = string.Concat(
                "OAuth ",
                $"oauth_consumer_key=\"{escapedConsumerKey}\", ",
                $"oauth_nonce=\"{escapedNonce}\", ",
                $"oauth_signature=\"{escapedSignature}\", ",
                $"oauth_signature_method=\"{escapedSignatureMethod}\", ",
                $"oauth_timestamp=\"{escapedTimeStamp}\", ",
                $"oauth_token=\"{escapedAccessToken}\", ",
                $"oauth_version=\"{escapedVersion}\""
            );

            return header;
        }

        private string GetNonce()
        {
            return Convert.ToBase64String(
                new ASCIIEncoding().GetBytes(
                    DateTime.Now.Ticks.ToString()
                )
            );
        }

        private string GetTimeStamp()
        {
            TimeSpan timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            return Convert.ToInt64(timeSpan.TotalSeconds).ToString();
        }

        private string GetBaseString(string url, string nonce, string timeStamp, List<string> parameters)
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

            return string.Concat(
                "GET&",
                Uri.EscapeDataString(url),
                "&",
                Uri.EscapeDataString(baseString)
            );
        }

        private string GetSignature(string baseString, string url)
        {
            string signInKey = string.Concat(
                Uri.EscapeDataString(this.consumerSecret),
                "&",
                Uri.EscapeDataString(this.accessSecret)
            );

            HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(signInKey));

            return Convert.ToBase64String(
                hasher.ComputeHash(
                    ASCIIEncoding.ASCII.GetBytes(baseString)
                )
            );
        }
    }
}