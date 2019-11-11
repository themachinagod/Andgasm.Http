using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;

namespace Andgasm.Http.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            HttpRequestBuilder b = new HttpRequestBuilder();
            b.AddRequestUri("https://www.whoscored.com/");
            b.AddMethod(HttpMethod.Get);
            b.AddUserAgent("Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.131 Safari/537.36");
            var r = Task.FromResult(b.SendAsync());
        }
    }
}
