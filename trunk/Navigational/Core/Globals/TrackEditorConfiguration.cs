using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Core.Globals
{
    public class TrackEditorConfiguration : ConfigurationSection
    {
        public TrackEditorConfiguration()
        {
        }

        [ConfigurationProperty("Name"), DefaultSettingValue("TrackEditorConfiguration")]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("MarkerSize", DefaultValue="4")]
        public string MarkerSize
        {
            get { return (string)this["MarkerSize"]; }
            set { this["MarkerSize"] = value; }
        }


        [ConfigurationProperty("MarkerColor", DefaultValue = "-16776961")]
        public string MarkerColor
        {
            get { return (string)this["MarkerColor"]; }
            set { this["MarkerColor"] = value; }
        }

        [ConfigurationProperty("MarkerPenWidth", DefaultValue="2")]
        public string MarkerPenWidth
        {
            get { return (string)this["MarkerPenWidth"]; }
            set { this["MarkerPenWidth"] = value; }
        }

        [ConfigurationProperty("EditorButtonsSize", DefaultValue="35,35")]
        public string EditorButtonsSize
        {
            get { return (string)this["EditorButtonsSize"]; }
            set { this["EditorButtonsSize"] = value; }
        }

        [ConfigurationProperty("GhostMarkerThresholdDistance", DefaultValue = "50")]
        public string GhostMarkerThresholdDistance
        {
            get { return (string)this["GhostMarkerThresholdDistance"]; }
            set { this["GhostMarkerThresholdDistance"] = value; }
        }
        
    }
}
