using System;
using System.Collections.Generic;
using System.Linq;

namespace Bendyline.Base
{
    public class Random
    {
        public static Random CreateWithSeed(int seed)
        {
            return new Random();
        }
        
        public Random()
        {

        }

        public int Next()
        {
            int newNumber = -1;

            Script.Literal(@"newNumber = Math.floor(Math.random() * Number.MAX_VALUE);");

            return newNumber;
        }

        public int NextInRange(int max)
        {
            int newNumber = -1;

            Script.Literal(@"newNumber = Math.floor(Math.random() * max);");

            return newNumber;
        }
    }
}
