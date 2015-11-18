/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public enum CommandResultStatus
    {
        Success = 0,
        SucessWithIssues = 1,
        InvalidConfiguration = 2,
        Failure = 3
    }

    public class CommandResult 
    {
        private CommandResultStatus status;
        private String summary;
        private static CommandResult successResult;
        private static CommandResult failureResult;
        private static CommandResult invalidConfigurationResult;

        public static CommandResult InvalidConfigurationResult
        {
            get
            {
                if (invalidConfigurationResult == null)
                {
                    invalidConfigurationResult = new CommandResult();
                    invalidConfigurationResult.Status = CommandResultStatus.Success;
                }

                return invalidConfigurationResult;
            }
        }

        public static CommandResult SuccessResult
        {
            get
            {
                if (successResult == null)
                {
                    successResult = new CommandResult();
                    successResult.Status = CommandResultStatus.Success;
                }

                return successResult;
            }
        }

        public static CommandResult FailureResult
        {
            get
            {
                if (failureResult == null)
                {
                    failureResult = new CommandResult();
                    failureResult.Status = CommandResultStatus.Failure;
                }

                return failureResult;
            }
        }

        public CommandResultStatus Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
            }
        }

        public String Summary
        {
            get
            {
                return this.summary;
            }

            set
            {
                this.summary = value;
            }
        }

        public CommandResult()
        {
        }
    }
}
