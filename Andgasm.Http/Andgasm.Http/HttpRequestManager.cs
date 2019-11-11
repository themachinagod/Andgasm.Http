using Andgasm.Http.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace Andgasm.Http
{
    public class HttpRequestManager : IHttpRequestManager
    {
        #region Simplified Generic Model Serialisation
        public async Task<T> Get<T>(string url, IHttpRequestContext context = null)
        {
            var response = await Get(url, context);
            return response.ContentAsType<T>();
        }

        public async Task Post<T>(T model, string url, IHttpRequestContext context = null)
        {
            await Post(url, model);
        }

        public async Task Put<T>(T model, string url, IHttpRequestContext context = null)
        {
            await Put(url, model);
        }

        public async Task Patch<T>(T model, string url, IHttpRequestContext context = null)
        {
            await Patch(url, model);
        }

        public async Task Delete<T>(string url, IHttpRequestContext context = null)
        {
            await Delete(url);
        }
        #endregion

        #region Raw Response Serialisation
        public async Task<HttpResponseMessage> Get(string requestUri, IHttpRequestContext context = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri);
            builder = HttpRequestBuilder.BuildRequestFromContext(builder, context);
            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Post(string requestUri, object value, IHttpRequestContext context = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));
            builder = HttpRequestBuilder.BuildRequestFromContext(builder, context);
            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Put(string requestUri, object value, IHttpRequestContext context = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));
            builder = HttpRequestBuilder.BuildRequestFromContext(builder, context);
            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Patch(string requestUri, object value, IHttpRequestContext context = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value));
            builder = HttpRequestBuilder.BuildRequestFromContext(builder, context);
            return await builder.SendAsync();
        }

        public async Task<HttpResponseMessage> Delete(string requestUri, IHttpRequestContext context = null)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri);
            builder = HttpRequestBuilder.BuildRequestFromContext(builder, context);
            return await builder.SendAsync();
        }
        #endregion
    }
}
