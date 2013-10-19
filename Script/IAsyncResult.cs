using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public interface IAsyncResult
    {
        object AsyncState { get; set; }
        object Data { get; set; }
        bool IsCompleted { get; set; }
        bool CompletedSynchronously { get; set; }
    }
}
