using System;
using System.Text;
using System.Web;
using RTWTR.Infrastructure.Contracts;

namespace RTWTR.Infrastructure
{
    public class TokenEncoder : IEncoder
    {
        public string Encode(params string[] args)
        {
            return this.EncodeBearerToken(args[0], args[1]);
        }

        private string EncodeToBase64(string toEncode)
        {
            return Convert.ToBase64String(
                Encoding.UTF8.GetBytes(
                    toEncode
                )
            );
        }

        private string EncodeBearerToken(string consumerKey, string consumerSecret)
        {
            string bearerToken = string.Concat(
                HttpUtility.UrlEncode(consumerKey),
                ":",
                HttpUtility.UrlEncode(consumerSecret)
            );

            return this.EncodeToBase64(
                bearerToken
            );
        }
    }
}