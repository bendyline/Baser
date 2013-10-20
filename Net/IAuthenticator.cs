/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

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
