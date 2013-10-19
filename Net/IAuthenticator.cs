using System;
using System.Collections.Generic;
using System.Net;

namespace Bendyline.Base
{
    public interface IAuthenticator
    {
        AuthenticatorStatus GetStatusByUrl(String url);

        UserInfo ActiveUser
        {
            get;
        }

        event LoginEventHandler AuthenticationRequestFailed;
        event LoginEventHandler AuthenticationChanged;
        event LoginEventHandler LoginPending;
        
        void ContinueLogin(String userLoginName, String password);
        void AuthenticateForRequest(IProtocolRequest protocolRequest);
        void ModifyRequest(WebRequest request);
    }
}
