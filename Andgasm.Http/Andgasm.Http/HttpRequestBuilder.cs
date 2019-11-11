using Andgasm.Http.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Andgasm.Http
{
    public class HttpRequestBuilder
    {
        #region Fields
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string bearerToken = "";
        private string useragent = "";
        private string host = "";
        private string referer = "";
        private string acceptHeader = "application/json";
        private bool setgzip = false;
        private TimeSpan timeout = new TimeSpan(0, 0, 30);

        private Dictionary<string, string> headercollection = new Dictionary<string, string>();
        private Dictionary<string, string> cookiecollection = new Dictionary<string, string>();
        #endregion

        #region Constructors
        public HttpRequestBuilder()
        {
        }
        #endregion

        #region Fluent Builder Operations
        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddUserAgent(string useragent)
        {
            this.useragent = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddHost(string host)
        {
            this.host = host;
            return this;
        }

        public HttpRequestBuilder AddReferer(string referer)
        {
            this.referer = referer;
            return this;
        }

        public HttpRequestBuilder AddGzipCompression()
        {
            this.setgzip = true;
            return this;
        }

        public HttpRequestBuilder AddHeaders(Dictionary<string, string> headercollection)
        {
            this.headercollection = headercollection;
            return this;
        }

        public HttpRequestBuilder AddCookies(Dictionary<string, string> cookiecollection)
        {
            this.cookiecollection = cookiecollection;
            return this;
        }

        public HttpRequestBuilder AddTimeout(TimeSpan timeout)
        {
            this.timeout = timeout;
            return this;
        }
        #endregion

        public static HttpRequestBuilder BuildRequestFromContext(HttpRequestBuilder builder, IHttpRequestContext context)
        {
            if (context != null)
            {
                builder.AddTimeout(new System.TimeSpan(0, 0, 0, context.Timeout));
                builder.AddHost(context.Host);
                builder.AddAcceptHeader(context.Accept);
                builder.AddUserAgent(context.UserAgent);
                builder.AddReferer(context.Referer);
                builder.AddGzipCompression();
                builder.AddHeaders(context.Headers);
                builder.AddCookies(context.Cookies);
            }
            return builder;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            var request = new HttpRequestMessage
            {
                Method = this.method,
                RequestUri = new Uri(this.requestUri)
            };
            request.Headers.Accept.Clear();
            if (this.content != null) request.Content = this.content;
            if (!string.IsNullOrWhiteSpace(this.bearerToken)) request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", this.bearerToken);
            if (!string.IsNullOrWhiteSpace(this.useragent)) request.Headers.UserAgent.Add(new ProductInfoHeaderValue(this.useragent));
            if (!string.IsNullOrWhiteSpace(this.host)) request.Headers.Host = this.host;
            if (!string.IsNullOrWhiteSpace(this.referer)) request.Headers.Referrer = new Uri(this.referer);
            if (!string.IsNullOrWhiteSpace(this.acceptHeader)) request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.acceptHeader));

            foreach (var hi in headercollection) request.Headers.Add(hi.Key, hi.Value);
            foreach (var hi in cookiecollection) request.Headers.Add(hi.Key, hi.Value);

            var client = new HttpClient();
            client.Timeout = this.timeout;
            if (this.setgzip) client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return await client.SendAsync(request);
        }
    }
}
