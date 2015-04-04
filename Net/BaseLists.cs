/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
{
    /// <summary>
    /// Contains basic lists used for service-level common tasks.
    /// </summary>
    public static class BaseLists
    {
        private static IDataStore feedbackDataStore;
        private static IDataStoreList feedbackList;
        private static IDataStoreList exceptionList;

        public static IDataStore FeedbackDataStore
        {
            get
            {
                if (feedbackDataStore == null)
                {
                    feedbackDataStore = DataStore.Create(DataStoreType.SharePoint);
                    feedbackDataStore.RequiresAuthentication = false;
                    feedbackDataStore.Location = Context.Current.FeedbackUrl;
                }

                return feedbackDataStore;
            }
        }

        public static IDataStoreList FeedbackList
        {
            get
            {
                if (feedbackList == null)
                {
                    feedbackList = FeedbackDataStore.AssumeList("Feedback");

                    feedbackList.AssumeField("Title", DataStoreFieldType.Text);
                    feedbackList.AssumeField("Content", DataStoreFieldType.Note);
                }

                return feedbackList;
            }
        }

        public static IDataStoreList ExceptionsList
        {
            get
            {
                if (exceptionList == null)
                {
                    exceptionList = FeedbackDataStore.AssumeList("Exceptions");

                    exceptionList.AssumeField("Title", DataStoreFieldType.Text);
                    exceptionList.AssumeField("Details", DataStoreFieldType.Note);
                    exceptionList.AssumeField("Stack", DataStoreFieldType.Note);
                }

                return exceptionList;
            }
        }
    }
}
