using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bendyline.Base
{
    public class Uint8Array : TypedArray
    {
        private byte[] data;

        public byte this[int index]
        {
            get
            {
                return this.data[index];
            }
            set
            {
                this.data[index] = value;
            }
        }

        public Uint8Array(ArrayBuffer dataBuffer) : base(dataBuffer)
        {
            this.data = new byte[dataBuffer.ByteLength];
        }
    }
}
