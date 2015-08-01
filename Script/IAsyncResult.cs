/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;

namespace BL
{
    /// <summary>
    /// Provides a standard interface for passing the results of a callback.
    /// </summary>
    public interface IAsyncResult
    {
        /// <summary>
        /// Arbitrary state passed by the caller of an asynchronous call, to "hold on to" and "hand back" when an 
        /// asynchronous call is complete.
        /// </summary>
        object AsyncState { get; set; }

        /// <summary>
        /// Results of the asynchronous call, if necessary.
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// Indicates whether the call is completed or not.
        /// </summary>
        bool IsCompleted { get; set; }

        bool IsError { get; }

        String ErrorCode { get; set; }

        String ErrorMessage { get; set; }

        /// <summary>
       /// Indicates whether the call was completed immediately or not.
       /// </summary>
        bool CompletedSynchronously { get; set; }
    }
}
