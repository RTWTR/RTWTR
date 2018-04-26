using System;
using System.Threading.Tasks;

namespace RTWTR.Service.Twitter.Contracts
{
    public interface IApiProvider
    {
        Task<string> GetJSON(string url);
    }
}
