using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RTWTR.Infrastructure.Contracts;

namespace RTWTR.Infrastructure
{
    public class JsonProvider : IJsonProvider
    {
        public T DeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public JObject ParseToJObject(string jsonString)
        {
            return JObject.Parse(jsonString);
        }

        public JArray ParseToJArray(string jsonString)
        {
            return JArray.Parse(jsonString);
        }

        public string SerializeObject(object classToSerialize)
        {
            return JsonConvert.SerializeObject(classToSerialize);
        }
    }
}
