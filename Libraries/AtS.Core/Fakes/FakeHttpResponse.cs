using System.Text;
using System.Web;

namespace TWYK.Core.Fakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        public FakeHttpResponse() {
            Cookies = new HttpCookieCollection();
        }

        private readonly StringBuilder _outputString = new StringBuilder();

        public string ResponseOutput => _outputString.ToString();

        public override int StatusCode { get; set; }

        public override string RedirectLocation { get; set; }

        public override void Write(string s) {
            _outputString.Append(s);
        }

        public override string ApplyAppPathModifier(string virtualPath) {
            return virtualPath;
        }

        public override HttpCookieCollection Cookies { get; }
    }
}