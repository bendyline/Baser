﻿using System;
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
        private String preferencesData;
        private Nullable<long> profileAppId;

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
                this.isLoaded = value;
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

                this.NotifyPropertyChanged("FirstName");
            }
        }

        [ScriptName("i_profileAppId")]
        public Nullable<long> ProfileAppId
        {
            get
            {
                return this.profileAppId;
            }
            set
            {
                this.profileAppId = value;

                this.NotifyPropertyChanged("ProfileAppId");
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

                this.NotifyPropertyChanged("LastName");
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

                this.NotifyPropertyChanged("ContentContainer");

            }
        }

        [ScriptName("s_preferences")]
        public String PreferencesData
        {
            get
            {
                return this.preferencesData;
            }
            set
            {
                this.preferencesData = value;

                this.NotifyPropertyChanged("Preferences");

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

                this.NotifyPropertyChanged("ThumbnailImage");
            }
        }

        public String ThumbnailImageUrl
        {
            get
            {
                if (this.ThumbnailImage == null)
                {
                    return null;
                }

                if (this.ThumbnailImage.StartsWith("[ir]"))
                {
                    return Context.Current.ResourceBasePath + "qla/images/" + this.ThumbnailImage.Substring(4, this.ThumbnailImage.Length);
                }
                else
                {

                    return Context.Current.UserContentBasePath + this.ContentContainer + "/" + this.ThumbnailImage;
                }
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
            if (this.Id == null)
            {
                throw new Exception("ID is not set.");
            }

            bool isNew = false;

            if (this.isLoaded)
            {
                CallbackResult.NotifySynchronousSuccess(callback, state, this);

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

                xhr.OnReadyStateChange = new Action(this.LoadUserContinue);
                xhr.Send("");
            }
        }

        private void LoadUserContinue()
        {
            XmlHttpRequest xhr = (XmlHttpRequest)this.userLoadedOperation.Tag;

            if (xhr != null && xhr.ReadyState == ReadyState.Loaded)
            {
                this.ApplyString(xhr.ResponseText);

                this.isLoaded = true;
                this.userLoadedOperation.CompleteAsAsyncDone(this);
                this.userLoadedOperation = null;
            }
        }
    }
}
