using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class JsonParser
    {
        public JsonParser(string jsonString)
        {
            JsonString = jsonString;
        }

        public string JsonString { get; }


        public dynamic ToObject()
        {
            return JsonConvert.DeserializeObject(JsonString);
        }
    }
}
