/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;

namespace BL
{
    public static class HtmlUtilities
    {
        public static void SetInnerHtml(Element e, String html)
        {
     //       Script.Literal(@"{0}.innerHTML={1};", e, html);
       
            Script.Literal(@"if (typeof window.toStaticHTML == ""undefined"") {{{0}.innerHTML={1};}} else {{{0}.innerHTML=window.toStaticHTML({1});}}", e, html);
        }
    }
}
