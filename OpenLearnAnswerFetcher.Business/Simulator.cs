using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenLearnAnswerFetcher.Business
{
    public abstract class Simulator<T>
    {

        private static ICollection<KeyValuePair<string, string>> GetQueryFromString(string queryString)
        {
            var rt = new Collection<KeyValuePair<string, string>>();
            var queryStringGroup = queryString.Split(new[] { "&" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in queryStringGroup)
            {
                var key = item.Substring(0, item.IndexOf('='));
                var value = item.Substring(item.IndexOf('=') + 1);
                var keyValue = new KeyValuePair<string, string>(key, value);
                rt.Add(keyValue);
            }
            return rt;
        }

        private static ICollection<KeyValuePair<string, string>> GetHeaderFromString(string header)
        {
            var rt = new Collection<KeyValuePair<string, string>>();
            foreach (var item in header.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {

                var key = item.Substring(0, item.IndexOf(':'));
                var value = item.Substring(item.IndexOf(':') + 1);
                if (value.StartsWith(" ")) value = value.TrimStart();
                var keyValue = new KeyValuePair<string, string>(key, value);
                rt.Add(keyValue);
            }
            return rt;
        }
        public Simulator(Agent agent, string uri, HttpMethod method, string queryString, string header) : this(agent, uri, method, GetQueryFromString(queryString), GetHeaderFromString(header))
        {


        }
        public Simulator(Agent agent, string uri, HttpMethod method, ICollection<KeyValuePair<string, string>> queryString, string header) : this(agent, uri, method, queryString, GetHeaderFromString(header))
        {


        }
        public Simulator(Agent agent, string uri, HttpMethod method, ICollection<KeyValuePair<string, string>> queryString, ICollection<KeyValuePair<string, string>> header)
        {
            Agent = agent;
            UriString = uri;
            Method = method;
            QueryString = queryString;
            Header = header;

        }

        public virtual Agent Agent { get; }
        public virtual HttpMethod Method { get; set; } = HttpMethod.Get;

        private Uri _uri { get; set; }

        private string UriString { get; set; }
        public virtual Uri Uri
        {
            get
            {
                if (QueryString.Count > 0)
                {
                    var query = string.Join("&", QueryString.Join(QueryString, q1 => q1.Key, q2 => q2.Key, (q1, q2) => $"{q1.Key}={q2.Value}"));
                    if (UriString.Contains("?"))
                    {
                        UriString = $"{UriString}&{query}";
                    }
                    else
                        UriString = $"{UriString}?{query}";
                }
                if (_uri == null)
                    _uri = new Uri(UriString);
                return _uri;

            }
        }

        public virtual ICollection<KeyValuePair<string, string>> QueryString { get; }
        public virtual string Body { get; set; }

        public virtual ICollection<KeyValuePair<string, string>> Header { get; }
        public abstract IEnumerable<T> Simulate(HttpContent content);

        public abstract T SimulateOne(HttpContent content);

    }
}
