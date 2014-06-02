/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    public class Operation
    {
        private List<CallbackState> callbackStates;
        private object tag;

        public List<CallbackState> CallbackStates
        {
            get
            {
                return this.callbackStates;
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

        public Operation()
        {
            this.callbackStates = new List<CallbackState>();
        }

        public void AddCallback(AsyncCallback c, object state)
        {
            if (c != null)
            {
                CallbackState cs = new CallbackState();
                cs.Callback = c;
                cs.State = state;

                this.callbackStates.Add(cs);
            }
        }

        public void CompleteAsAsyncDone(object data)
        {
            CallbackResult cr = new CallbackResult();
            cr.IsCompleted = true;
            cr.CompletedSynchronously = true;
            cr.Data = data;

            this.Complete(cr);
        }

        public void Complete(IAsyncResult template)
        {
            foreach (CallbackState cs in this.callbackStates)
            {
                if (cs.Callback != null)
                {
                    CallbackResult cr = new CallbackResult();

                    cr.AsyncState = cs.State;
                    cr.IsCompleted = template.IsCompleted;
                    cr.CompletedSynchronously = template.CompletedSynchronously;
                    cr.Tag = cs.Tag;
                    cr.Data = template.Data;

                    cs.Callback(cr);
                }
            }
        }
    }
}
