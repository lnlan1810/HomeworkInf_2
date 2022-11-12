using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Attributes
{
    internal class HttpGET : Attribute
    {
        public string MethodUri;
        public HttpGET(string methodUri)
        {
            MethodUri = methodUri;
        }
        public HttpGET() { MethodUri = null; }
    }
}
