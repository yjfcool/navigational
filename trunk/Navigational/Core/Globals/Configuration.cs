using System.Configuration;

namespace Core.Globals
{
    public class Configuration
    {
        private System.Configuration.Configuration _Configuration = null;
        private GlobalConfiguration _GlobalConfiguration = new GlobalConfiguration();
        private TrackEditorConfiguration _TrackEditorConfiguration = new TrackEditorConfiguration();

        #region Singleton.
        private static Configuration _Instance = null;


        private Configuration()
        {
            OpenConfigurationFile();
        }

        /// <summary>
        /// Gets Configuration class instance.
        /// </summary>
        /// <returns></returns>
        public static Configuration GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new Configuration();
            }
            return _Instance;
        }
        #endregion

        public GlobalConfiguration GlobalConfiguration
        {
            get
            {
                return _GlobalConfiguration;
            }
        }

        public TrackEditorConfiguration TrackEditorConfiguration
        {
            get
            {
                return _TrackEditorConfiguration;
            }
        }


        private void OpenConfigurationFile()
        {
            _Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming);

            string configurationName = "GlobalConfiguration";
            _GlobalConfiguration = new GlobalConfiguration();
            _GlobalConfiguration.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
            CreateConfigurationSection(configurationName, _GlobalConfiguration);
            _GlobalConfiguration = (GlobalConfiguration)_Configuration.GetSection(configurationName);


            configurationName = "TrackEditorConfiguration";
            _TrackEditorConfiguration = new Core.Globals.TrackEditorConfiguration();
            _TrackEditorConfiguration.SectionInformation.AllowExeDefinition = ConfigurationAllowExeDefinition.MachineToLocalUser;
            CreateConfigurationSection(configurationName, _TrackEditorConfiguration);
            _TrackEditorConfiguration = (TrackEditorConfiguration)_Configuration.GetSection(configurationName);
               
        }

        private void CreateConfigurationSection(string configurationName, ConfigurationSection configurationSection)
        {
            if (_Configuration.Sections[configurationName] == null)
            {
                _Configuration.Sections.Add(configurationName, configurationSection);
                _Configuration.Save(ConfigurationSaveMode.Full, true);
            }
        }


        /// <summary>
        /// Saves applicatino configuration.
        /// </summary>
        public void SaveConfiguration()
        {
            _Configuration.Save();
        }
    }
}
