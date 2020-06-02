using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public class OpenLearnHistoryDetailFetcher
    {
        public OpenLearnHistoryDetailFetcher(string jsonString)
        {
            JsonString = jsonString;
        }

        public string JsonString { get; }

        public dynamic Fetch()
        {
            var jsonParser = new JsonParser(JsonString);
            return jsonParser.ToObject();
        }
    }
}
