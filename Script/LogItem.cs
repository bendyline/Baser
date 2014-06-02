/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public class LogItem
    {
        private Date created;
        private String message;
        private LogStatus status;
        private double timeTaken;
        private object details;
        private String id;
        private int eventId;

        public int EventId
        {
            get
            {
                return this.eventId;
            }

            set
            {
                this.eventId = value;
            }
        }

        public String Id
        {
            get
            {
                if (this.id == null)
                {
                    this.id = Utilities.CreateRandomId();
                }

                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        public bool HasId
        {
            get
            {
                return this.id != null;
            }
        }

        public Date Created
        {
            get
            {
                return this.created;
            }
        }

        public String Message
        {
            get
            {
                return this.message;
            }
        }

        public LogStatus Status
        {
            get
            {
                return this.status;
            }
        }

        public Double TimeTaken
        {
            get
            {
                return this.timeTaken;
            }
        }

        public object Details
        {
            get
            {
                return this.details;
            }

            set
            {
                this.details = value;
            }
        }

        public LogItem(int eventId, String message, LogStatus status, double timeTaken, object details)
        {
            this.eventId = eventId;

            Date now = Date.Now;

            this.created = new Date(now.GetUTCFullYear(), now.GetUTCMonth(), now.GetUTCDate(), now.GetUTCHours(), now.GetUTCMinutes(), now.GetUTCSeconds(), now.GetUTCMilliseconds());
            this.message = message;
            this.status = status;
            this.timeTaken = timeTaken;
            this.details = details;
        }
    }
}
