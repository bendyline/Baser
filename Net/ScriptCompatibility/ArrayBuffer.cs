using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bendyline.Base
{
    public class ArrayBuffer
    {
        private int byteLength;

        public int ByteLength
        {
            get
            {
                return this.byteLength;
            }
        }

        public ArrayBuffer(int lengthInBytes)
        {
            this.byteLength = lengthInBytes;
        }

        public ArrayBuffer Slice(int begin, int end)
        {
            return null;
        }
    }
}
