﻿/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bendyline.Base
{
    public class SerializableTypeAttribute : Attribute
    {
        private String tagName;

        public String TagName
        {
            get { return this.tagName; }
            set { this.tagName = value; }
        }
    }
}
