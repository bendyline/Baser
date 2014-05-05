/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public class Context 
    {
        private static Context current = new Context();
        private String versionHash;
        private String userName;
        private int expId;
        private int tokenId;

        public int Tokenid
        {
            get
            {
                return this.tokenId;
            }

            set
            {
                this.tokenId = value;
            }
        }

        public int ExpId
        {
            get
            {
                return this.expId;
            }

            set
            {
                this.expId = value;
            }
        }

        public static Context Current
        {
            get
            {
                return current;
            }
        }

        public String UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
            }
        }

        public String VersionHash
        {
            get
            {
                return this.versionHash;
            }

            set
            {
                this.versionHash = value;
            }
        }

        public static void Set(int tokenId, int expId, String versionHash, String userName)
        {
            Context pc = Context.Current;

            pc.ExpId = expId;
            pc.Tokenid = tokenId;
            pc.VersionHash = versionHash;
            pc.UserName = userName;
        }
    }
}
