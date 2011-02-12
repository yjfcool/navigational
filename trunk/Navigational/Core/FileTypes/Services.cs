using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.FileTypes
{
    public static class Services
    {
        private static int _uniqueNumber = 0;

        /// <summary>
        /// Generates a unique int for the current running session.
        /// </summary>
        /// <returns></returns>
        public static int GenerateUniqueInt()
        {
            _uniqueNumber++;
            return _uniqueNumber;
        }
    }
}
