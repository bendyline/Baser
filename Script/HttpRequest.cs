/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Serialization;
using System.Xml;

namespace BL
{
    public enum HttpRequestType
    {
        General = 0,
        JsonRead = 1,
        JsonWrite = 2
    }

    public class HttpRequest
    {
        private XmlHttpRequest request;
        private String originalUrl;
        private String originalVerb;
        private String originalBody;
        private Action onReadyStateChange;
        private Action onComplete;
        private String authorization;
        private ErrorAction onError;
        private object completionData = null;

        private HttpRequestType webRequestType = HttpRequestType.General;
        private bool authenticationRequired;
        private bool requestSent = false;
        private Operation operation;
        private object jsonResponseData;

        public object Source
        {
            get
            {
                return this.completionData;
            }

            set
            {
                this.completionData = value; 
            }
        }

        public object ResponseJson
        {
            get
            {
                return this.jsonResponseData;
            }
        }

        public Operation Operation
        {
            get
            {
                return this.operation;
            }

            set
            {
                this.operation = value;
            }
        }

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

        public Action OnComplete
        {
            get
            {
                return this.onComplete;
            }

            set
            {
                this.onComplete = value;
            }
        }

        public ErrorAction OnError
        {
            get
            {
                return this.onError;
            }

            set
            {
                this.onError = value;
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

        public String Verb
        {
            get
            {
                return this.originalVerb;
            }

            set
            {
                this.originalVerb = value;
            }
        }

        public String Body
        {
            get
            {
                return this.originalBody;
            }

            set
            {
                this.originalBody = value;
            }
        }

        public String Url
        {
            get
            {
                return this.originalUrl = Url;
            }

            set
            {
                this.originalUrl = value;
            }
        }

        public HttpRequestType RequestType
        {
            get
            {
                return this.webRequestType;
            }

            set
            {
                this.webRequestType = value;
            }
        }

        public HttpRequest()
        {
        }

        public void Initialize(HttpRequestType requestType)
        {
            this.webRequestType = requestType;
        }

        public void SetBearerToken(String token)
        {
            this.authorization = "Bearer " + token;
        }

        private void HandleReadyStateChange()
        {
            if (this.onReadyStateChange != null)
            {
                this.onReadyStateChange();
            }

            if (this.onComplete != null)
            {
                if (this.ReadyState == ReadyState.Loaded)
                {
                    if (this.Status == 200)
                    {
                        if (this.webRequestType == HttpRequestType.JsonRead || this.webRequestType == HttpRequestType.JsonWrite)
                        {
                            try
                            {
                                String responseContent = this.ResponseText;

                                this.jsonResponseData = Json.Parse(responseContent);
                            }
                            catch (Exception e)
                            {
                                Log.Error(e.Message);

                                this.HandleError("invalidJsonResponse", e.Message);
                                return;
                            }
                        }

                        this.onComplete();

                        if (this.operation != null)
                        {
                            if (this.completionData != null)
                            {
                                this.operation.CompleteAsAsyncDone(this.completionData);
                            }
                            else
                            {
                                this.operation.CompleteAsAsyncDone(this.jsonResponseData);
                            }
                        }
                    }
                    else
                    {
                        Log.Error("Error " + this.Status.ToString() + " when retrieiving " + this.originalUrl);

                        this.HandleError(this.Status.ToString(), "Could not retrieve data from a web service.");
                    }
                }
            }
        }

        private void HandleError(String errorCode, String errorText)
        {
            if (this.onError != null)
            {
                this.onError(errorCode, errorText);
            }

            if (this.operation != null)
            {
                this.operation.CompleteAsAsyncError(this, errorCode, errorText);
            }
        }

        public void Send()
        {
            if (this.authenticationRequired && !Context.Current.IsAuthenticated)
            {
                Context.Current.AuthenticatedChanged += Current_AuthenticatedChanged;
                return;
            }

            this.SendRequest();
        }

        private void Current_AuthenticatedChanged(object sender, BooleanEventArgs e)
        {
            if (!this.requestSent && Context.Current.IsAuthenticated)
            {
                this.SendRequest();
            }
        }

        private void SendRequest()
        {
            this.request = new XmlHttpRequest();
            this.request.OnReadyStateChange = this.HandleReadyStateChange;

            String urlToRequest = this.originalUrl;

            urlToRequest = urlToRequest.Replace(" ", "%20");

            if (this.webRequestType == HttpRequestType.JsonRead || String.IsNullOrEmpty(this.originalVerb))
            {
                this.request.Open(HttpVerb.Get, urlToRequest);
            }
            else
            {
                this.request.Open(this.originalVerb, urlToRequest);
            }

            if (this.webRequestType == HttpRequestType.JsonWrite || this.webRequestType == HttpRequestType.JsonRead)
            {
                this.request.SetRequestHeader("Accept", "application/json;odata.metadata=minimal;odata.streaming=true");
                this.request.SetRequestHeader("Content-Type", "application/json");
            }         

            if (this.authorization != null)
            {
                this.request.SetRequestHeader("Authorization", authorization);
            }

            if (this.authenticationRequired)
            {
                HttpRequest.AddWithCredentialsToXmlHttpRequest(this.request);
            }

            try
            {
                if (this.originalBody != null)
                {
                    this.request.Send(this.originalBody);
                }
                else
                {
                    this.request.Send();
                }
            }
            catch (Exception e)
            {
                this.HandleError(this.Status.ToString(), e.Message);
            }

            this.requestSent = true;
        }

        public static void AddWithCredentialsToXmlHttpRequest(XmlHttpRequest xmlHttpRequest)
        {
            Script.Literal("{0}.withCredentials=true", xmlHttpRequest);
        }
    }
}
