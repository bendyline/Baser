using System;

namespace Bendyline.Base
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