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
    public class Agent : IDisposable
    {
        private static CookieCollection GetCookiesFromHeader(string header)
        {
            var headerCollection = new Collection<KeyValuePair<string, string>>();
            foreach (var item in header.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {

                var key = item.Substring(0, item.IndexOf(':'));
                var value = item.Substring(item.IndexOf(':') + 1);
                if (value.StartsWith(" ")) value = value.TrimStart();
                var keyValue = new KeyValuePair<string, string>(key, value);
                headerCollection.Add(keyValue);
            }
            var cookies = new CookieCollection();
            var cookieKV = headerCollection.First(it => string.Equals(it.Key, "cookie", StringComparison.InvariantCultureIgnoreCase));
            var cookieGroups = cookieKV.Value.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in cookieGroups)
            {
                var cookieName = item.Substring(0, item.IndexOf("="));
                if (cookieName.StartsWith("")) cookieName = cookieName.TrimStart();
                cookies.Add(new Cookie(cookieName, item.Substring(item.IndexOf("=") + 1)));
            }
            return cookies;
        }

        public Agent(Uri domain, string header) : this(domain, GetCookiesFromHeader(header))
        {

        }

        public Agent(Uri domain, CookieCollection cookies)
        {
            Cookies = cookies;
            Domain = domain;
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(Domain, Cookies);

            Handler = new HttpClientHandler() { CookieContainer = cookieContainer };

            Client = new HttpClient(Handler);
        }

        public Uri Domain { get; }

        public CookieCollection Cookies { get; } = new CookieCollection();
        public HttpMessageHandler Handler { get; private set; }
        public HttpClient Client { get; }

        public HttpResponseMessage Request(HttpRequestMessage message)
        {
            var task = Client.SendAsync(message);
            task.Wait();
            return task.Result;
        }

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                Handler.Dispose();
                Handler = null;
                _disposed = true;
            }
        }
    }
}
