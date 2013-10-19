using System;

namespace Bendyline.Base
{
    public class CallbackState
    {
        private AsyncCallback callback;
        private object callbackState;
        private object tag;

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

        public static CallbackState Wrap(AsyncCallback callback, object state)
        {
            CallbackState cs = new CallbackState();

            cs.Callback = callback;
            cs.State = state;

            return cs;
        }

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

                    try
                    {
                        cr.AsyncWaitHandle = result.AsyncWaitHandle;
                    }
                    catch (NotSupportedException)
                    {
                        ;
                    }

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
                UnwrapAndExecuteCallbacksAsError(result, ((CallbackResult)result).ErrorMessage);
            }
            else
            {
                UnwrapAndExecuteCallbacksAsError(result, null);
            }
        }

        public static void UnwrapAndExecuteCallbacksAsError(IAsyncResult result, String errorMessage)
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

                    try
                    {
                        cr.AsyncWaitHandle = result.AsyncWaitHandle;
                    }
                    catch (NotSupportedException)
                    {
                        ;
                    }

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

                    try
                    {
                        cr.AsyncWaitHandle = result.AsyncWaitHandle;
                    }
                    catch (NotSupportedException)
                    {
                        ;
                    }

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
