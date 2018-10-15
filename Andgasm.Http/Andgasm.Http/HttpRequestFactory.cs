using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Andgasm.Http
{
    public static class HttpRequestFactory
    {
        #region Generic Model Serialisation
        public static async Task<T> Get<T>(string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            var response = await Get(requestUri.ToString());
            return response.ContentAsType<T>();
        }

        public static async Task Post<T>(T model, string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            await Post(requestUri.ToString(), model);
        }

        public static async Task<T> Post<T, K>(K model, string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            var response = await Post(requestUri.ToString(), model);
            return response.ContentAsType<T>();
        }

        public static async Task Put<T>(T model, string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            await Put(requestUri.ToString(), model);
        }

        public static async Task<T> Put<T, K>(K model, string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            var response = await Put(requestUri.ToString(), model);
            return response.ContentAsType<T>();
        }

        public static async Task Delete<T>(string apiroot, string apipath, params string[] pathvars)
        {
            var requestUri = BuildRequestUri(apiroot, apipath, pathvars);
            await Delete(requestUri.ToString());
        }
        #endregion

        #region Raw Response Serialisation
        public static async Task<HttpResponseMessage> Get(string requestUri)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Get)
                                .AddRequestUri(requestUri);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(
           string requestUri, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Post)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(
           string requestUri, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Put)
                                .AddRequestUri(requestUri)
                                .AddContent(new JsonContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(
           string requestUri, object value)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(new HttpMethod("PATCH"))
                                .AddRequestUri(requestUri)
                                .AddContent(new PatchContent(value));

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri)
        {
            var builder = new HttpRequestBuilder()
                                .AddMethod(HttpMethod.Delete)
                                .AddRequestUri(requestUri);

            return await builder.SendAsync();
        }
        #endregion

        #region Helpers
        private static string BuildRequestUri(string apiroot, string apipath, string[] pathvars)
        {
            var requestUri = new StringBuilder(string.Format("{0}/api/{1}/", apiroot, apipath));
            foreach (var v in pathvars)
            {
                requestUri.Append(v).Append('/');
            }
            return requestUri.ToString();
        }
        #endregion
    }
}
