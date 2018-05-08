using Newtonsoft.Json.Linq;

namespace RTWTR.Infrastructure.Contracts
{
    public interface IJsonProvider
    {
        T DeserializeObject<T>(string jsonString);
        JObject ParseToJObject(string jsonString);
        JArray ParseToJArray(string jsonString);
        string SerializeObject(object classToSerialize);
    }
}
