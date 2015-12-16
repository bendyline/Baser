using System;

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
        }

        public static void Initialize()
        {
            ErrorHandler eh = ErrorHandler.Current;
        }

        public void PostException()
        {
            if (this.exceptionToHandle == null)
            {
                return;
            }
            
            this.exceptionToHandle = null;
        }
    }
}
