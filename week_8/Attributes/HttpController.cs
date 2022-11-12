﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Attributes
{
    internal class HttpController : Attribute
    {
        public string ControllerName;
        public HttpController(string controllerName)
        {
            ControllerName = controllerName;
        }

        //public string ClassUri;
        //public HttpController(string classUri)
        //{
        //    ClassUri = classUri;
        //}
        //public HttpController() { ClassUri = null; }
    }
}
