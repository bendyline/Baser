// Uint8Array.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    [Imported]
    [IgnoreNamespace]
    [ScriptName("TypedArray")]
    public class TypedArray
    {
        public ArrayBuffer Buffer;
        public int ByteLength;

        public TypedArray(ArrayBuffer arrayBuffer)
        {
            ;
        }
    }
}
