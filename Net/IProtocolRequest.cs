using System;

namespace Bendyline.Base
{
    public interface IProtocolRequest
    {
        String Url { get; set; }
        void InvokeRequest();
    }
}
