using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;

namespace GPSTracka
{
    public static class ConfigurationManager
    {
        #region Private Members

        private static NameValueCollection appSettings = new NameValueCollection();

        private static string configFile;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets configuration settings in the appSettings section.
        /// </summary>

        public static NameValueCollection AppSettings
        {
            get
            {
                return appSettings;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ConfigurationManager()
        {
            // Determine the location of the config file
            ConfigurationManager.configFile = String.Format("{0}.config", System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            // Ensure configuration file exists
            if (!File.Exists(ConfigurationManager.configFile))
            {
                throw new FileNotFoundException(String.Format("Configuration file ({0}) could not be found.", ConfigurationManager.configFile));
            }

            // Load config file as an XmlDocument
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(ConfigurationManager.configFile);

            // Add keys and values to the AppSettings NameValueCollection
            foreach (XmlNode appSettingNode in myXmlDocument.SelectNodes("/configuration/appSettings/add"))
            {
                ConfigurationManager.AppSettings.Add(appSettingNode.Attributes["key"].Value, appSettingNode.Attributes["value"].Value);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves changes made to the configuration settings.
        /// </summary>
        public static void Save()
        {
            // Load config file as an XmlDocument
            XmlDocument myXmlDocument = new XmlDocument();
            myXmlDocument.Load(ConfigurationManager.configFile);

            // Get the appSettings node
            XmlNode appSettingsNode = myXmlDocument.SelectSingleNode("/configuration/appSettings");

            if (appSettingsNode != null)
            {
                // Remove all previous appSetting nodes
                appSettingsNode.RemoveAll();

                foreach (string key in AppSettings.AllKeys)
                {
                    // Create a new appSetting node
                    XmlElement appSettingNode = myXmlDocument.CreateElement("add");

                    // Create the key attribute and assign its value
                    XmlAttribute keyAttribute = myXmlDocument.CreateAttribute("key");
                    keyAttribute.Value = key;

                    // Create the value attribute and assign its value
                    XmlAttribute valueAttribute = myXmlDocument.CreateAttribute("value");
                    valueAttribute.Value = AppSettings[key];

                    // Append the key and value attribute to the appSetting node
                    appSettingNode.Attributes.Append(keyAttribute);
                    appSettingNode.Attributes.Append(valueAttribute);

                    // Append the appSetting node to the appSettings node
                    appSettingsNode.AppendChild(appSettingNode);
                }
            }

            // Save config file
            myXmlDocument.Save(ConfigurationManager.configFile);
        }

        #endregion
    }
}
