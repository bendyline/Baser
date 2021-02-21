using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bendyline.Base
{
    public class TypedArray
    {
        private ArrayBuffer arrayBuffer;

        public ArrayBuffer Buffer
        {
            get
            {
                return this.arrayBuffer;
            }
        }

        public int ByteLength
        {
            get
            {
                return this.arrayBuffer.ByteLength;
            }
        }

        public TypedArray(ArrayBuffer arrayBuffer)
        {
            this.arrayBuffer = arrayBuffer;
        }
    }
}
