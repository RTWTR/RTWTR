using System;
using RTWTR.Infrastructure.Contracts;

namespace RTWTR.Infrastructure
{
    public class EnvironmentVariableProvider : IVariableProvider
    {
        public string GetValue(string variable)
        {
            return Environment.GetEnvironmentVariable(variable);
        }
    }
}
