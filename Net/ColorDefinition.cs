/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Globalization;

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

        public bool IsPrimarilyLight
        {

            get
            {
                int average = (this.red + this.green + this.blue) / 3;

                average = (average + Math.Max(Math.Max(this.red, this.green), this.blue)) / 2;

                return average > 170;
            }
        }


        public static ColorDefinition CreateFromString(String color)
        {
            ColorDefinition cd = new ColorDefinition();

            color = color.Trim();

            if (color.StartsWith("rgb(") && color.EndsWith(")"))
            {
                String[] vals = color.Substring(4, color.Length - 5).Split(',');

                if (vals.Length == 3)
                {
                    cd.Red = Byte.Parse(vals[0]);
                    cd.Green = Byte.Parse(vals[1]);
                    cd.Blue = Byte.Parse(vals[2]);
                }
            }
            else if (color.Length == 7 && color.StartsWith("#"))
            {
                cd.Red = Byte.Parse(color.Substring(1, 2), NumberStyles.AllowHexSpecifier);
                cd.Green = Byte.Parse(color.Substring(3, 2), NumberStyles.AllowHexSpecifier);
                cd.Blue = Byte.Parse(color.Substring(5, 2), NumberStyles.AllowHexSpecifier);
            }

            return cd;
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
