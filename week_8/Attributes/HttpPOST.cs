using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Attributes
{
    internal class HttpPOST : Attribute
    {
        public string MethodUri;
        public HttpPOST(string methodUri)
        {
            MethodUri = methodUri;
        }
        public HttpPOST() { MethodUri = null; }
    }
}
