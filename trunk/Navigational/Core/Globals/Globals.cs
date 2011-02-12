using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Globals
{
    /// <summary>
    /// Provides global properties to use across application wide.
    /// </summary>
    public class Globals
    {
        #region GlobalConfiguration
        public static string GPSFileFolder
        {
            get { return Configuration.GetInstance().GlobalConfiguration.OpenGPSFilePath; }
            set { Configuration.GetInstance().GlobalConfiguration.OpenGPSFilePath = value; }
        }


        public static string Units
        {
            get { return Configuration.GetInstance().GlobalConfiguration.Units; }
            set { Configuration.GetInstance().GlobalConfiguration.Units = value; }
        }


        public static string Language
        {
            get { return Configuration.GetInstance().GlobalConfiguration.Language; }
            set { Configuration.GetInstance().GlobalConfiguration.Language = value; }
        }

        public static string MapLanguage
        {
            get { return Configuration.GetInstance().GlobalConfiguration.MapLanguage; }
            set { Configuration.GetInstance().GlobalConfiguration.MapLanguage = value; }
        }

        public static string MapType
        {
            get { return Configuration.GetInstance().GlobalConfiguration.MapType; }
            set { Configuration.GetInstance().GlobalConfiguration.MapType = value; }
        }

        public static string MapStartupLocation
        {
            get { return Configuration.GetInstance().GlobalConfiguration.MapStartupLocation; }
            set { Configuration.GetInstance().GlobalConfiguration.MapStartupLocation = value; }
        } 
        #endregion

        #region Track Editor Configuration.
        public static string MarkerSize
        {
            get { return Configuration.GetInstance().TrackEditorConfiguration.MarkerSize; }
            set { Configuration.GetInstance().TrackEditorConfiguration.MarkerSize = value; }
        }
        public static string MarkerColor
        {
            get { return Configuration.GetInstance().TrackEditorConfiguration.MarkerColor; }
            set { Configuration.GetInstance().TrackEditorConfiguration.MarkerSize = value; }
        }
        public static string MarkerPenWidth
        {
            get { return Configuration.GetInstance().TrackEditorConfiguration.MarkerPenWidth; }
            set { Configuration.GetInstance().TrackEditorConfiguration.MarkerPenWidth = value; }
        }
        public static string EditorButtonsSize
        {
            get { return Configuration.GetInstance().TrackEditorConfiguration.EditorButtonsSize; }
            set { Configuration.GetInstance().TrackEditorConfiguration.EditorButtonsSize = value; }
        }
        #endregion

    }
}
