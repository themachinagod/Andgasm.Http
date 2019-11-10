using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Andgasm.Http.Interfaces
{
    public interface IHttpRequestManager
    {
        Task<HttpResponseMessage> Get(string requestUri);
        Task<T> Get<T>(string apiroot, string apipath, params string[] pathvars);
        Task<HttpResponseMessage> Post(string requestUri, object value);
        Task Post<T>(T model, string apiroot, string apipath, params string[] pathvars);
        Task<T> Post<T, K>(K model, string apiroot, string apipath, params string[] pathvars);
        Task<HttpResponseMessage> Put(string requestUri, object value);
        Task Put<T>(T model, string apiroot, string apipath, params string[] pathvars);
        Task<T> Put<T, K>(K model, string apiroot, string apipath, params string[] pathvars);
        Task<HttpResponseMessage> Patch(string requestUri, object value);
        Task<HttpResponseMessage> Delete(string requestUri);
        Task Delete<T>(string apiroot, string apipath, params string[] pathvars);

    }
}
