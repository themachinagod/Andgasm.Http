using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Andgasm.Http.Tests
{
    [TestClass]
    public class RequestSimulationsShould
    {
        [TestMethod]
        public async Task CorrectlyGetHtmlWhen_SimulatingWhoScoredSeasonParticipantRequest()
        {
            HttpRequestManager _httpmanager = new HttpRequestManager();
            var realisedcookie = "";
            var cookieResp = await _httpmanager.Get("https://www.whoscored.com/");
            foreach (var sc in cookieResp.Headers.Where(x => x.Key == "Set-Cookie"))
            {
                foreach (var scv in sc.Value)
                {
                    var v = scv.Split(';')[0];
                    realisedcookie = $"{v}; {realisedcookie}";
                }
            }

            var ctx = new HttpRequestContext();
            ctx.Method = "GET";
            ctx.Host = "www.whoscored.com";
            ctx.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            ctx.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            ctx.AddHeader("Accept-Encoding", "gzip, deflate, br");
            ctx.AddHeader("Accept-Language", "en-GB,en;q=0.");
            ctx.AddCookie("Cookie", WebUtility.UrlEncode(realisedcookie));
            ctx.Timeout = 120000;
            var detailResp = await _httpmanager.Get("https://www.whoscored.com/Regions/252/Tournaments/2/Seasons/7361", ctx);

            Debug.Write(detailResp.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(detailResp.Content.ReadAsStringAsync().Result.Contains("Premier League tables"));
        }
    }
}
