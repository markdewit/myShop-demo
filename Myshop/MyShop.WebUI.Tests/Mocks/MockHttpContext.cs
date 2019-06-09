using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.WebUI.Tests.Mocks
{
    class MockHttpContext : HttpContextBase
    {
        private MockRequest request;
        private MockResponse response;
        private HttpCookieCollection cookie;
        public MockHttpContext()
        {
            cookie =  new HttpCookieCollection();
            this.request = new MockRequest(cookie);
            this.response = new MockResponse(cookie);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return request;
            }
        }
        public override HttpResponseBase Response
        {
            get
            {
                return response;
            }
        }
    }

    public class MockResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection cookies;

        public MockResponse(HttpCookieCollection cookies)
        {
            this.cookies = cookies;
        }
        public override HttpCookieCollection Cookies {
            get {
                return cookies;
            }
        }
    }

    public class MockRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection cookies;

        public MockRequest(HttpCookieCollection cookies)
        {
            this.cookies = cookies;
        }
        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookies;
            }
        }
    }
}
