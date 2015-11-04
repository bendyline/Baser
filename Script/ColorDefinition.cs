using System;
using System.Runtime.CompilerServices;

namespace BL
{
    /// <summary>
    /// Specifies an abstract, implementation agnostic definition of a color.
    /// </summary>
    public class ColorDefinition : SerializableObject
    {
        private int red = 0;
        private int green = 0;
        private int blue = 0;
        private Nullable<double> alpha = null;

        public Nullable<double> Alpha
        {
            get
            {
                return this.alpha;
            }

            set
            {
                this.alpha = value;
            }
        }

        /// <summary>
        /// Red value of the color.
        /// </summary>
        [ScriptName("i_red")]
        public int Red
        {
            get
            {
                return this.red;
            }
            set
            {
                this.red = value;

                this.red = Math.Round(this.red);

                if (this.red< 0)
                {
                    this.red = 0;
                }

                if (this.red > 255)
                {
                    this.red = 255;
                }
            }
        }

        /// <summary>
        /// Green value of the color.
        /// </summary>
        [ScriptName("i_green")]
        public int Green
        {
            get
            {
                return this.green;
            }
            set
            {
                this.green = value;

                this.green = Math.Round(this.green);

                if (this.green < 0)
                {
                    this.green = 0;
                }

                if (this.green > 255)
                {
                    this.green = 255;
                }
            }
        }

        /// <summary>
        /// Blue value of the color.
        /// </summary>
        [ScriptName("i_blue")]
        public int Blue
        {
            get
            {
                return this.blue;
            }
            set
            {
                this.blue = value;

                this.blue = Math.Round(this.blue);

                if (this.blue < 0)
                {
                    this.blue = 0;
                }

                if (this.blue > 255)
                {
                    this.blue = 255;
                }
            }
        }

        public ColorDefinition()
        {

        }

        public static ColorDefinition GenerateRandomDarkColorFromSeed(long id)
        {
            String color = String.Empty; 

            ColorDefinition cd = new ColorDefinition();
            cd.Red = Math.Floor(Utilities.GenerateSeededRandom(id) * 128);
            cd.Green = Math.Floor(Utilities.GenerateSeededRandom(id + 1) * 128);
            cd.Blue = Math.Floor(Utilities.GenerateSeededRandom(id + 2) * 128);

            return cd;
        }

        public bool IsPrimarilyLight
        {

            get
            {
                int average = (this.red + this.green + this.blue) / 3;

                average = (average + Math.Max(this.red, this.green, this.blue)) / 2;

                return average > 170;
            }
        }
        public bool IsPrimarilyLightLight
        {

            get
            {
                int average = (this.red + this.green + this.blue) / 3;

                average = (average + Math.Max(this.red, this.green, this.blue)) / 2;

                return average > 215;
            }
        }


        public static bool IsPrimarilyLightColor(String color)
        {
            ColorDefinition cd = ColorDefinition.CreateFromString(color);

            return cd.IsPrimarilyLight;
        }

        public static bool IsPrimarilyLightLightColor(String color)
        {
            ColorDefinition cd = ColorDefinition.CreateFromString(color);

            return cd.IsPrimarilyLightLight;
        }

        public static ColorDefinition CreateFromString(String color)
        {
            ColorDefinition cd = new ColorDefinition();

            color = color.Trim();

            if (color.StartsWith("rgb(") && color.EndsWith(")"))
            {
                String[] vals = color.Substring(4, color.Length - 1).Split(",");

                if (vals.Length == 3)
                {
                    cd.Red = Int32.Parse(vals[0]);
                    cd.Green = Int32.Parse(vals[1]);
                    cd.Blue = Int32.Parse(vals[2]);
                }
            }
            else if (color.Length == 7 && color.StartsWith("#"))
            {
                cd.Red = Int32.Parse(color.Substring(1, 3), 16);
                cd.Green = Int32.Parse(color.Substring(3, 5), 16);
                cd.Blue = Int32.Parse(color.Substring(5, 7), 16);
            }

            return cd;
        }
    
        public ColorDefinition GetPercentageAdjustedColor(double percentageDifference)
        {
            ColorDefinition cd = new ColorDefinition();

            cd.Red = this.Red + (int)(this.Red * percentageDifference);            
            cd.Green = this.Green + (int)(this.Green * percentageDifference);
            cd.Blue = this.Blue + (int)(this.Blue * percentageDifference);

            cd.Alpha = this.alpha;
            
            return cd;
        }

        public String ToRGBAString()
        {
            String result = "rgba(";

            result += this.Red + "," + this.Green + "," + this.Blue;

            if (this.alpha != null)
            {
                result += "," + this.alpha;
            }

            result += ")";

            return result;
        }

        public override string ToString()
        {
            String result = this.Blue.ToString(16);

            while (result.Length < 2)
            {
                result = "0" + result;
            }

            result = this.Green.ToString(16) + result;

            while (result.Length < 4)
            {
                result = "0" + result;
            }

            result = this.Red.ToString(16) + result;

            while (result.Length < 6)
            {
                result = "0" + result;
            }

            return "#" + result;
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
        }
    }
}
