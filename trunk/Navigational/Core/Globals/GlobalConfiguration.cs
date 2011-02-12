using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Core.Globals
{
    public class GlobalConfiguration : ConfigurationSection
    {
        public GlobalConfiguration()
        {
        }


        [ConfigurationProperty("Name"), DefaultSettingValue("GlobalConfiguration")]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("OpenGPSFilePath", DefaultValue = @"C:\")]
        public string OpenGPSFilePath
        {
            get { return (string)this["OpenGPSFilePath"]; }
            set { this["OpenGPSFilePath"] = value; }
        }

        [ConfigurationProperty("Units", DefaultValue = "Kilometers")]
        public string Units
        {
            get { return (string)this["Units"]; }
            set { this["Units"] = value; }
        }

        [ConfigurationProperty("Language", DefaultValue = "English")]
        public string Language
        {
            get { return (string)this["Language"]; }
            set { this["Language"] = value; }
        }

        [ConfigurationProperty("MapLanguage", DefaultValue = "English")]
        public string MapLanguage
        {
            get { return (string)this["MapLanguage"]; }
            set { this["MapLanguage"] = value; }
        }

        [ConfigurationProperty("MapType", DefaultValue = "GoogleHybrid")]
        public string MapType
        {
            get { return (string)this["MapType"]; }
            set { this["MapType"] = value; }
        }

        [ConfigurationProperty("RightToLeft", DefaultValue = "False")]
        public string RightToLeft
        {
            get { return (string)this["RightToLeft"]; }
            set { this["RightToLeft"] = value; }
        }


        [ConfigurationProperty("MapStartupLocation", DefaultValue = "32.30744,34.8476")]
        public string MapStartupLocation
        {
            get { return (string)this["MapStartupLocation"]; }
            set { this["MapStartupLocation"] = value; }
        }

        [ConfigurationProperty("TrackSimplifierThresholdDistance", DefaultValue = "100")]
        public string TrackSimplifierThresholdDistance
        {
            get { return (string)this["TrackSimplifierThresholdDistance"]; }
            set { this["TrackSimplifierThresholdDistance"] = value; }
        }
    }
}
