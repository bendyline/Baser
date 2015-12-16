/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

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
        private object data;
        private object tag;
        private String errorMessage;
        private String errorCode;

        public String ErrorCode
        {
            get
            {
                return this.errorCode;
            }

            set
            {
                this.errorCode = value;
            }
        }

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

        /// <summary>
        /// A user-defined object that qualifies or contains information about an asynchronous 
        /// operation.
        /// </summary>
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

        public static void NotifyAsynchronousSuccess(AsyncCallback doneCallback, object stateObject, object resultObject)
        {
            if (doneCallback == null)
            {
                return;
            }

            CallbackResult cr = new CallbackResult();

            cr.AsyncState = stateObject;
            cr.Data = resultObject;
            cr.CompletedSynchronously = false;
            cr.IsCompleted = true;

            doneCallback(cr);
        }

        public static void NotifySynchronousFailure(AsyncCallback doneCallback, object stateObject, String errorCode)
        {
            if (doneCallback == null)
            {
                return;
            }

            CallbackResult cr = new CallbackResult();

            cr.AsyncState = stateObject;
            cr.CompletedSynchronously = true;
            cr.IsCompleted = false;
            cr.ErrorCode = errorCode;

            doneCallback(cr);
        }

        public static void NotifySynchronousSuccess(AsyncCallback doneCallback, object stateObject, object resultObject)
        {
            if (doneCallback == null)
            {
                return;
            }

            CallbackResult cr = new CallbackResult();

            cr.AsyncState = stateObject;
            cr.Data = resultObject;
            cr.CompletedSynchronously = true;
            cr.IsCompleted = true;

            doneCallback(cr);
        }
    }
}
