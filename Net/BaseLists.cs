using System;

namespace Bendyline.Base
{
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
                    feedbackDataStore.Location = Utilities.FeedbackServicesBaseUrl;
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
