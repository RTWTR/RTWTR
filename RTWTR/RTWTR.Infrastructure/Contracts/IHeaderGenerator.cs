using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTWTR.Infrastructure.Contracts
{
    public interface IHeaderGenerator
    {
        Task<string> GenerateHeader(string url, List<string> parameters);
    }
}
