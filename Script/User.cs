using Bendyline.Base;
using System;
using System.Net;
using System.Runtime.CompilerServices;
using System.Serialization;

namespace BL
{
    /// <summary>
    /// Specifies an abstract, implementation agnostic definition of a color.
    /// </summary>
    public class User : UserReference
    {
        private String firstName;
        private String lastName;
        private String contentContainer;
        private String thumbnailImage;

        private Operation userLoadedOperation;
        private bool isLoaded = false;

        public bool IsLoaded
        {
            get
            {
                return this.isLoaded;
            }

            set
            {
                this.isLoaded = true;
            }
        }

        [ScriptName("s_firstName")]
        public String FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        [ScriptName("s_lastName")]
        public String LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        [ScriptName("s_contentContainer")]
        public String ContentContainer
        {
            get
            {
                return this.contentContainer;
            }
            set
            {
                this.contentContainer = value;
            }
        }

        [ScriptName("s_thumbnailImage")]
        public String ThumbnailImage
        {
            get
            {
                return this.thumbnailImage;
            }
            set
            {
                this.thumbnailImage = value;
            }
        }

        public String ThumbnailImageUrl
        {
            get
            {
                return Context.Current.UserContentBasePath + this.ContentContainer + "/" + this.ThumbnailImage;
            }
        }

        public String Summary
        {
            get
            {
                if (this.NickName != null)
                {
                    return this.NickName;
                }

                if (this.FirstName != null & this.LastName != null)
                {
                    return this.FirstName + " " + this.LastName;
                }

                if (this.FirstName != null)
                {
                    return this.FirstName;
                }

                if (this.LastName != null)
                {
                    return this.LastName;
                }

                return this.Id;
            }
        }

        public void LoadUser(AsyncCallback callback, object state)
        {
            bool isNew = false;

            if (this.isLoaded)
            {
                if (callback != null)
                {
                    CallbackResult cr = new CallbackResult();
                    cr.Data = this;
                    cr.IsCompleted = true;
                    cr.AsyncState = state;

                    callback(cr);
                }

                return;
            }

            if (this.userLoadedOperation == null)
            {
                this.userLoadedOperation = new Operation();
                isNew = true;
            }

            this.userLoadedOperation.CallbackStates.Add(CallbackState.Wrap(callback, state));

            if (isNew)
            {
                XmlHttpRequest xhr = new XmlHttpRequest();
                this.userLoadedOperation.Tag = xhr;
                String endpoint = UrlUtilities.EnsurePathEndsWithSlash(Context.Current.WebServiceBasePath) + "api/user/" + this.Id;

                xhr.Open("GET", endpoint);
                xhr.SetRequestHeader("Accept", "application/json");
                xhr.SetRequestHeader("Content-Type", "application/json");

                xhr.Send("");
                xhr.OnReadyStateChange = new Action(this.LoadUserContinue);
            }
        }

        private void LoadUserContinue()
        {
            XmlHttpRequest xhr = (XmlHttpRequest)this.userLoadedOperation.Tag;

            if (xhr != null && xhr.ReadyState == ReadyState.Loaded)
            {
                object o = Json.Parse(xhr.ResponseText);

                this.ApplyObject(o);

                this.isLoaded = true;
                this.userLoadedOperation.CompleteAsAsyncDone(this);
            }
        }
    }
}
