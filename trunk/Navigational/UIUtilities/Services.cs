using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Navigational.UIUtilities
{
    public static class Services
    {
        /// <summary>
        /// Gets if the current code is running under Visual Studio design time.
        /// </summary>
        /// <returns></returns>
        public static bool IsDesignMode()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return true;
            }
            return false;
        }
    }
}
