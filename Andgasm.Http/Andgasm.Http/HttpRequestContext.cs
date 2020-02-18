using Andgasm.Http.Interfaces;
using System.Collections.Generic;

namespace Andgasm.Http
{
    public class HttpRequestContext : IHttpRequestContext
    {
        public int Timeout { get; set; }
        public string Method { get; set; }
        public string Host { get; set; }
        public string Accept { get; set; }
        public string UserAgent { get; set; }
        public string Referer { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public Dictionary<string, string> Cookies { get; set; }

        public HttpRequestContext()
        {
            Timeout = 60000;
            Headers = new Dictionary<string, string>();
            Cookies = new Dictionary<string, string>();
            Accept = "application/json";
        }

        public void AddCookie(string n, string v)
        {
            Cookies.Add(n, v);
        }

        public void AddHeader(string n, string v)
        {
            Headers.Add(n, v);
        }
    }
}
