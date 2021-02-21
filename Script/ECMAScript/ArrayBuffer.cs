// ArrayBuffer.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    [Imported]
    [IgnoreNamespace]
    [ScriptName("ArrayBuffer")]
    public class ArrayBuffer
    {
        public int ByteLength;


        public ArrayBuffer(int lengthInBytes)
        {
            ;
        }

        public ArrayBuffer Slice(int begin, int end)
        {
            return null;
        }
    }
}
