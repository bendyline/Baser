using System;
using System.Windows;


namespace Bendyline.Base
{
    public class ErrorHandler
    {
        private static ErrorHandler current;
        private Exception exceptionToHandle;
        private String userFeedbackMessage;

        public event EventHandler ExceptionPending;

        public String UserFeedbackMessage
        {
            get
            {
                return this.userFeedbackMessage;
            }

            set
            {
                this.userFeedbackMessage = value;
            }
        }

        public static ErrorHandler Current
        {
            get
            {
                if (current == null)
                {
                    current = new ErrorHandler();
                    current.LocalIntialize();
                }

                return current;
            }
        }

        internal void LocalIntialize()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        public static void Initialize()
        {
            ErrorHandler eh = ErrorHandler.Current;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            this.exceptionToHandle = e.ExceptionObject as Exception;

            if (this.ExceptionPending != null)
            {
                this.ExceptionPending(this, EventArgs.Empty);
            }
        }

        public void PostException()
        {
            if (this.exceptionToHandle == null)
            {
                return;
            }

            IDataStoreList exceptionsList = BaseLists.ExceptionsList;

            IDataStoreItem item = exceptionsList.CreateItem();
            
            String message = this.exceptionToHandle.Message;
            if (message.Length > 254)
            {
                message = message.Substring(0, 250) + "...";
            }

            String details = String.Empty;

            if (this.exceptionToHandle.InnerException != null)
            {
                details += "\r\n\r\nINNER EXCEPTION MESSAGE:\r\n" + this.exceptionToHandle.InnerException.Message;
                details += "\r\n\r\nINNER EXCEPTION STACK TRACE:\r\n" + this.exceptionToHandle.InnerException.StackTrace;
            }

            item.SetStringValue("Title", message);
            item.SetStringValue("Details", details);

            item.SetStringValue("Stack", this.exceptionToHandle.StackTrace);

            exceptionsList.BeginUpdate(this.UpdateComplete, null);

            this.exceptionToHandle = null;
        }


        private void UpdateComplete(IAsyncResult result)
        {

        }


    }
}
