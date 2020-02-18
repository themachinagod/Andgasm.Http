using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Diagnostics;
using System.Linq;

namespace Andgasm.Http.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            // for whoscored cookie init request test
            HttpRequestManager rm = new HttpRequestManager();
            var ctx = new HttpRequestContext();
           

            //var ctx = new HttpRequestContext();

            ctx.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729)";
            ctx.Host = "www.whoscored.com";
            ctx.Cookies.Add("Cookie", "visid_incap_774904=dTceNZ8SRq60mj9xHyUBBLjNS14AAAAAQ0IPAAAAAACAvlSSAVP0no4BrdBQMhIoR+pZSZgM7b4g; incap_ses_458_774904=fAtbTfECminUFVs9+SRbBmjcS14AAAAAkQX0uNiLkt9fdjbpI4Y6Pg==; incap_ses_874_774904=GVISb4W+gyxqjv29VxMhDKPcS14AAAAAVLRqjnl5Xp8fLVjCSkZtgw==");

            var n2 = await rm.Get("https://www.whoscored.com/", ctx);

            Debug.Write(n2.Content.ReadAsStringAsync().Result);
            Assert.IsTrue(n2.Content.ReadAsStringAsync().Result.Contains("Premier League tables"));
        }
    }
}
