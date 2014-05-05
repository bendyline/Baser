using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Specifies an abstract, implementation agnostic definition of a color.
    /// </summary>
    public class ColorDefinition : SerializableObject
    {
        private byte red = 0;
        private byte green = 0;
        private byte blue = 0;

        /// <summary>
        /// Red value of the color.
        /// </summary>
        [ScriptName("y_red")]
        public byte Red
        {
            get
            {
                return red;
            }
            set
            {
                red = value;
            }
        }

        /// <summary>
        /// Green value of the color.
        /// </summary>
        [ScriptName("y_green")]
        public byte Green
        {
            get
            {
                return green;
            }
            set
            {
                green = value;
            }
        }

        /// <summary>
        /// Blue value of the color.
        /// </summary>
        [ScriptName("y_blue")]
        public byte Blue
        {
            get
            {
                return blue;
            }
            set
            {
                blue = value;
            }
        }

        public ColorDefinition()
        {

        }

        protected override void InitForSerialization()
        {
            base.InitForSerialization();

            this.SerializableType.EnsureString("red", "R");
            this.SerializableType.EnsureString("green", "G");
            this.SerializableType.EnsureString("blue", "B");
        }

        /// <summary>
        /// Returns a full opacity version of the color as an Int32.
        /// </summary>
        /// <returns>Color integer.  Can be used in items such as WriteableBitmap pixel arrays.</returns>
        public int CreateColorInt()
        {
            return CreateColorIntWithAlpha(255);
        }

        /// <summary>
        /// Returns an adjustable opacity version of the color as an Int32.
        /// </summary>
        /// <param name="alpha">Alpha value of the color.  255 means fully opaque; 0 means fully transparent.</param>
        /// <returns>Color integer.</returns>
        public int CreateColorIntWithAlpha(byte alpha)
        {
            throw new Exception("Unimplemented");
            return -1;
        }
    }
}
