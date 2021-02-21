// Uint8Array.cs
//

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BL
{
    [Imported]
    [IgnoreNamespace]
    [ScriptName("Unit8Array")]
    public class Uint8Array : TypedArray
    {

        public byte this[int index]
        {
            get
            {
                return 0;
            }
            set
            {
                ;
            }
        }

        public Uint8Array(ArrayBuffer arrayBuffer) : base(arrayBuffer)
        {
            ;
        }
    }
}
