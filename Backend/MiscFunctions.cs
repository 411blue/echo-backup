using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend
{
    public static class MiscFunctions
    {
        private static Random random = null;
        public static void initRandom()
        {
            if (random == null)
            {
                random = new Random();
            }
        }
        public static int Next()
        {
            initRandom();
            return random.Next();
        }

        public static string DBDateAndTime()
        {
            DateTime dt = DateTime.Now;
            return String.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        }
    }
}
