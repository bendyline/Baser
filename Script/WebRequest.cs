/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml;

namespace BL
{
    public class WebRequest
    {
        private XmlHttpRequest request;
        private String originalUrl;
        private Action onReadyStateChange;
        private bool authenticationRequired;
        private bool requestSent = false;

        public bool AuthenticationRequired
        {
            get
            {
                return this.authenticationRequired;
            }

            set
            {
                if (this.authenticationRequired == value)
                {
                    return;
                }

                this.authenticationRequired = value;

                if (this.authenticationRequired)
                {
                    WebRequest.SendWithCredentials(this.request);
                }
            }
        }

        public Action OnReadyStateChange
        {
            get
            {
                return this.onReadyStateChange;
            }

            set
            {
                this.onReadyStateChange = value;
            }
        }

        public ReadyState ReadyState
        {
            get
            {
                return this.request.ReadyState;
            }
        }
        
        public string ResponseText
        {
            get
            {
                return this.request.ResponseText;
            }
        }

        public XmlDocument ResponseXml
        {
            get
            {
                return this.request.ResponseXml;
            }
        }
        
        public int Status
        {
            get
            {
                return this.request.Status;
            }
        }
        
        public string StatusText
        {
            get
            {
                return this.request.StatusText;
            }
        }

        public WebRequest()
        {
            this.request = new XmlHttpRequest();
            this.request.OnReadyStateChange = this.HandleReadyStateChange;
        }
        public void InitializeAsJsonReadRequest(String url, Action readyStateChange)        
        {
            this.onReadyStateChange = readyStateChange;
            this.originalUrl = url;
            this.request.Open(HttpVerb.Get, url);
            this.InitializeAsJsonRequest();
        }

        private void InitializeAsJsonRequest()
        {
            this.request.SetRequestHeader("Accept", "application/json;odata=minimalmetadata");
            this.request.SetRequestHeader("Content-Type", "application/json");
        }

        private void HandleReadyStateChange()
        {
            if (this.onReadyStateChange != null)
            {
                this.onReadyStateChange();
            }
        }

        public void Open(String verb, String url)
        {
            this.originalUrl = url;
            this.request.Open(verb, url);
        }

        public void Send()
        {
            if (this.authenticationRequired && !Context.Current.IsAuthenticated)
            {
                Context.Current.AuthenticatedChanged += Current_AuthenticatedChanged;
                return;
            }

            this.requestSent = true;
            this.request.Send();
        }

        private void Current_AuthenticatedChanged(object sender, BooleanEventArgs e)
        {
            if (!this.requestSent && Context.Current.IsAuthenticated)
            {
                this.requestSent = true;
                this.request.Send();
            }
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
