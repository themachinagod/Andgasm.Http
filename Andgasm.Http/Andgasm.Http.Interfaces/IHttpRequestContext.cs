using System.Collections.Generic;

namespace Andgasm.Http.Interfaces
{
    public interface IHttpRequestContext
    {
        int Timeout { get; set; }
        string Method { get; set; }
        string Host { get; set; }
        string Accept { get; set; }
        string UserAgent { get; set; }
        string Referer { get; set; }
        Dictionary<string, string> Headers { get; set; }
        Dictionary<string, string> Cookies { get; set; }

        void AddCookie(string n, string v);
        void AddHeader(string n, string v);
    }
}
