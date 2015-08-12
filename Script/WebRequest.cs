/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BL
{

    public class WebRequest
    {
        private XmlHttpRequest request;


        /*[IntrinsicProperty]
        [ScriptName("onreadystatechange")]
        public Action OnReadyStateChange { get; set; }
        [IntrinsicProperty]
        public ReadyState ReadyState { get; }
        [IntrinsicProperty]
        public string ResponseText { get; }
        [IntrinsicProperty]
        [ScriptName("responseXML")]
        public XmlDocument ResponseXml { get; }
        [IntrinsicProperty]
        public int Status { get; }
        [IntrinsicProperty]
        public string StatusText { get; }*/

        public WebRequest()
        {
            this.request = new XmlHttpRequest();

            WebRequest.SendWithCredentials(this.request);
        }

        public void Open(String verb, String url)
        {
            this.request.Open(verb, url);
        }

        public void Send()
        {
            this.request.Send();
        }

        public void SendWithBody(String body)
        {
            this.request.Send(body);
        }



        public static void SendWithCredentials(XmlHttpRequest xmlHttpRequest)
        {
            Script.Literal("{0}.withCredentials=true", xmlHttpRequest);
        }
    }
}
