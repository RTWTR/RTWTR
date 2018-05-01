using System;

namespace RTWTR.Infrastructure.Contracts
{
    public interface IEncoder
    {
        string Encode(params string[] args);
    }
}
