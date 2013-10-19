using System;
using System.Threading;

namespace Bendyline.Base
{
    public class CallbackResult : IAsyncResult
    {
        private object asyncState;
        private WaitHandle waitHandle;
        private bool completedSynchronously;
        private bool isCompleted;
        private object tag;
        private String errorMessage;

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

        // Summary:
        //     Gets a user-defined object that qualifies or contains information about an
        //     asynchronous operation.
        //
        // Returns:
        //     A user-defined object that qualifies or contains information about an asynchronous
        //     operation.
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
        //     Gets a System.Threading.WaitHandle that is used to wait for an asynchronous
        //     operation to complete.
        //
        // Returns:
        //     A wait handle that is used to wait for an asynchronous operation to complete.
        public WaitHandle AsyncWaitHandle 
        {
            get
            {
                return this.waitHandle;
            }

            set
            {
                this.waitHandle = value;
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
