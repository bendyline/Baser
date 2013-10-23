/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    public class Operation
    {
        private List<CallbackState> callbackStates;

        public List<CallbackState> CallbackStates
        {
            get
            {
                return this.callbackStates;
            }
        }

        public Operation()
        {
            this.callbackStates = new List<CallbackState>();
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

                    cs.Callback(cr);
                }
            }
        }
    }
}
