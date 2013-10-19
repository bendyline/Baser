using System;
using System.Threading;

namespace BL
{
    public class CallbackResult : IAsyncResult
    {
        private object asyncState;
        private bool completedSynchronously;
        private bool isCompleted;
        private object tag;
        private object data;
        private String errorMessage;

        /// <summary>
        /// Returns a human-readable error message that came from this operation.
        /// </summary>
        public String ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }

            set
            {
                this.errorMessage = value;
            }
        }

       
        ///<summary>
        /// Gets a user-defined object that qualifies or contains information about an asynchronous operation.
        ///</summary>
        public object AsyncState 
        {
            get
            {
                return this.asyncState;
            }

            set
            {
                this.asyncState = value;
            }
        }

        //
        // Summary:
        //     Gets a value that indicates whether the asynchronous operation completed
        //     synchronously.
        //
        // Returns:
        //     true if the asynchronous operation completed synchronously; otherwise, false.
        public bool CompletedSynchronously 
        {
            get
            {
                return this.completedSynchronously;
            }

            set
            {
                this.completedSynchronously = value;
            }
        }
        
        //
        // Summary:
        //     Gets a value that indicates whether the asynchronous operation has completed.
        //
        // Returns:
        //     true if the operation is complete; otherwise, false.
        public bool IsCompleted 
        {
            get
            {
                return this.isCompleted;
            }

            set
            {
                this.isCompleted = value;
            }
        }

        public object Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        public object Tag
        {
            get
            {
                return this.tag;
            }

            set
            {
                this.tag = value;
            }
        }

    }
}
