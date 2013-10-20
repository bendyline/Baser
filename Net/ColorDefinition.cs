/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace Bendyline.Base
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

        protected override void InitializeForSerialization()
        {
            base.InitializeForSerialization();

            this.SerializableType.EnsureString("Red", "R");
            this.SerializableType.EnsureString("Green", "G");
            this.SerializableType.EnsureString("Blue", "B");
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
            return BitConverter.ToInt32(new byte[] { this.blue, this.green, this.red, alpha }, 0);
        }
    }
}
