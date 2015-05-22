using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Html;
using System.Serialization;

namespace BL
{
    public class EnsureContentContainerRequest : SerializableObject
    {
        private String contentContainerId;

        [ScriptName("s_contentContainerId")]
        public String ContentContainerId
        {
            get
            {
                return this.contentContainerId;
            }

            set
            {
                this.contentContainerId = value;
            }
        }
    }
}