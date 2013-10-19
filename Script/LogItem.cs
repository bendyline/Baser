using System;
using System.Net;

namespace BL
{
    public class LogItem
    {
        private Date created;
        private String message;
        private RichContentDocument document;
        private LogStatus status;
        private double timeTaken;
        private object details;
        private String id;

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

        public RichContentDocument Document
        {
            get
            {
                if (this.document == null && this.message != null)
                {
                    this.document = new RichContentDocument();
                    this.document.Text = this.message;
                }

                return this.document;
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

        public LogItem(String message, RichContentDocument document, LogStatus status, double timeTaken, object details)
        {
            this.created = Date.Now;
            this.message = message;
            this.document = document;
            this.status = status;
            this.timeTaken = timeTaken;
            this.details = details;
        }
    }
}
