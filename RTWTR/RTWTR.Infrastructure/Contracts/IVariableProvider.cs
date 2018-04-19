using System;

namespace RTWTR.Infrastructure.Contracts
{
    public interface IVariableProvider
    {
        string GetValue(string variable);
    }
}
