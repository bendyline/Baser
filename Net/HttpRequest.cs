/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using Bendyline.Base.ScriptCompatibility;
using System;
using System.Net;

namespace Bendyline.Base
{
    public enum HttpRequestType
    {
        General = 0,
        JsonRead = 1,
        JsonWrite = 2
    }

    public enum ReadyState
    {
        Uninitialized = 0,
        Open = 1,
        Sent = 2,
        Receiving = 3,
        Loaded = 4
    }

    public class HttpRequest
    {
        private HttpWebRequest request;
        private ReadyState readyState;
        private String responseText;
        private int statusCode;
        private String statusText;

        private String originalUrl;
        private String originalVerb;
        private String originalBody;
        private Action onReadyStateChange;
        private Action onComplete;
        private ErrorAction onError;
        private object completionData = null;

        private HttpRequestType webRequestType = HttpRequestType.General;
        private bool authenticationRequired;
        private bool requestSent = false;
        private Operation operation;
        private object jsonResponseData;

        public object CompletionData
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
                return this.readyState;
            }
        }
        
        public string ResponseText
        {
            get
            {
                return this.responseText;
            }
        }       
        
        public int Status
        {
            get
            {
                return this.statusCode;
            }
        }
        
        public string StatusText
        {
            get
            {
                return this.statusText;
            }
        }

        public HttpRequest()
        {
        }

        public void InitializeAsJsonReadRequest(String url)        
        {
            this.originalUrl = url;

            this.webRequestType = HttpRequestType.JsonRead;
        }

        public void Initialize(HttpRequestType requestType)
        {
            this.webRequestType = requestType;
        }

        public void InternalInitializeAsJsonRequest()
        {
            this.request.Headers["Accept"] ="application/json;odata=minimalmetadata";
            this.request.Headers["Content-Type"] = "application/json";
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
                        if (this.webRequestType == HttpRequestType.JsonRead)
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

        public void Open(String verb, String url)
        {
            this.originalUrl = url;
            this.originalVerb = verb;
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

        public void SendWithBody(String body)
        {
            this.originalBody = body;

            this.SendRequest();
        }

        private void SendRequest()
        {
     
            String urlToRequest = this.originalUrl;

            urlToRequest = urlToRequest.Replace(" ", "%20");

            this.request = WebRequest.CreateHttp(urlToRequest);

            if (this.webRequestType == HttpRequestType.JsonRead)
            {

            }
            else if (!String.IsNullOrEmpty(this.originalVerb))
            {
                this.request.Method = this.originalVerb;
            }

            if (this.webRequestType == HttpRequestType.JsonRead || this.webRequestType == HttpRequestType.JsonWrite)
            {
                this.InternalInitializeAsJsonRequest();
            }

            throw new NotImplementedException();
            /*
            if (this.originalBody != null)
            {
                this.request.Send(this.originalBody);
            }
            else
            {
                this.request.Send();
            }*/

            this.requestSent = true;
        }
    }
}
