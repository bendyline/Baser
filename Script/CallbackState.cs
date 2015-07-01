/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    /// <summary>
    /// Internal class that holds a collection of state items needed to process an asynchronous call, including the 
    /// callback to fire when the call is complete.
    /// </summary>
    public class CallbackState
    {
        private AsyncCallback callback;
        private object callbackState;
        private object tag;

        /// <summary>
        /// Callback to fire.
        /// </summary>
        public AsyncCallback Callback
        {
            get
            {
                return this.callback;
            }

            set
            {
                this.callback = value;
            }
        }

        /// <summary>
        /// Temporary state passed by the caller to the asynchronous call, to "hold on to" during the call.
        /// </summary>
        public object State
        {
            get
            {
                return this.callbackState;
            }

            set
            {
                this.callbackState = value;
            }
        }

        /// <summary>
        /// Arbitrary extra data used by the callback, internally.
        /// </summary>
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

        public CallbackState()
        {

        }

        public void NotifyComplete(object data, bool completedSynchronously)
        {
            CallbackResult cr = new CallbackResult();

            cr.Data = data;
            cr.IsCompleted = true;
            cr.CompletedSynchronously = completedSynchronously;
            cr.AsyncState = this.State;

            this.Callback(cr);
        }

        /// <summary>
        /// Helper function to construct a new callback state.
        /// </summary>
        /// <param name="callback">Callback to fire when the call is complete.</param>
        /// <param name="state">State passed in by the caller.  Can be null.</param>
        /// <returns>A new callbackstate object that the asynchronous call can use.</returns>
        public static CallbackState Wrap(AsyncCallback callback, object state)
        {
            CallbackState cs = new CallbackState();

            cs.Callback = callback;
            cs.State = state;

            return cs;
        }

        /// <summary>
        /// Helper function to construct a callback state, with an optional "tag' blob of data.
        /// </summary>
        /// <param name="callback">Callback to fire when the call is complete.</param>
        /// <param name="state"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static CallbackState WrapWithTag(AsyncCallback callback, object state, object tag)
        {
            CallbackState cs = new CallbackState();

            cs.Callback = callback;
            cs.State = state;
            cs.Tag = tag;

            return cs;
        }

        public static void UnwrapAndExecuteCallbacksWithTag(IAsyncResult result, object tag)
        {
            if (result.AsyncState is CallbackState)
            {
                CallbackState cs = (CallbackState)result.AsyncState;

                if (cs.Callback != null)
                {
                    CallbackResult cr = new CallbackResult();
                    cr.CompletedSynchronously = result.CompletedSynchronously;
                    cr.IsCompleted = result.IsCompleted;
                    cr.Data = result.Data;
                    cr.AsyncState = cs.State;
                    cr.Tag = tag;

                    cs.Callback(cr);
                }
            }
        }
        
        public static void UnwrapAndExecuteCallbacksAsError(IAsyncResult result)
        {
            if (result is CallbackResult)
            {
                UnwrapAndExecuteCallbacksAsErrorWithMessage(result, ((CallbackResult)result).ErrorMessage);
            }
            else
            {
                UnwrapAndExecuteCallbacksAsErrorWithMessage(result, null);
            }
        }

        public static void UnwrapAndExecuteCallbacksAsErrorWithMessage(IAsyncResult result, String errorMessage)
        {
            if (result.AsyncState is CallbackState)
            {
                CallbackState cs = (CallbackState)result.AsyncState;

                if (cs.Callback != null)
                {
                    CallbackResult cr = new CallbackResult();

                    cr.CompletedSynchronously = result.CompletedSynchronously;
                    cr.IsCompleted = false;
                    cr.ErrorMessage = errorMessage;
                    cr.Data = result.Data;
                    cr.AsyncState = cs.State;

                    if (result is CallbackResult)
                    {
                        cr.Tag = ((CallbackResult)result).Tag;
                    }

                    cs.Callback(cr);
                }
            }
        }

        public static void UnwrapAndExecuteCallbacks(IAsyncResult result)
        {
            if (result.AsyncState is CallbackState)
            {
                CallbackState cs = (CallbackState)result.AsyncState;

                if (cs.Callback != null)
                {
                    CallbackResult cr = new CallbackResult();
                    cr.CompletedSynchronously = result.CompletedSynchronously;
                    cr.IsCompleted = result.IsCompleted;
                    cr.Data = result.Data;
                    cr.AsyncState = cs.State;

                    if (result is CallbackResult)
                    {
                        cr.Tag = ((CallbackResult)result).Tag;
                    }

                    cs.Callback(cr);
                }
            }
        }
    }
}
