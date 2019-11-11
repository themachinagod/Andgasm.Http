using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Andgasm.Http.Interfaces
{
    public interface IHttpRequestManager
    {
        Task<T> Get<T>(string url, IHttpRequestContext context = null);
        Task Post<T>(T model, string url, IHttpRequestContext context = null);
        Task Put<T>(T model, string url, IHttpRequestContext context = null);
        Task Patch<T>(T model, string url, IHttpRequestContext context = null);
        Task Delete<T>(string url, IHttpRequestContext context = null);

        Task<HttpResponseMessage> Get(string requestUri, IHttpRequestContext context = null);
        Task<HttpResponseMessage> Post(string requestUri, object value, IHttpRequestContext context = null);
        Task<HttpResponseMessage> Put(string requestUri, object value, IHttpRequestContext context = null);
        Task<HttpResponseMessage> Patch(string requestUri, object value, IHttpRequestContext context = null);
        Task<HttpResponseMessage> Delete(string requestUri, IHttpRequestContext context = null);

    }
}
