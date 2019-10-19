using BepInEx;
using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Drawers;
using FluffyLabsConfigManagerTools.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Util
{
    /// <summary>
    /// Class to help add button configurations
    /// </summary>
    public sealed class ButtonUtil
    {
        private readonly ConfigFile config;

        /// <summary>
        /// Class to help add button configurations
        /// </summary>
        /// <param name="config">The ConfigFile which to add ConfigEntry to</param>
        public ButtonUtil(ConfigFile config)
        {
            this.config = config;
        }

        /// <summary>
        /// Add a button configuration
        /// </summary>
        /// <param name="section">Button config section</param>
        /// <param name="key">Button config key</param>
        /// <param name="description">Button description</param>
        /// <param name="buttonDic">Dictionary of buttons where string=buttonName, Action=buttonLogic</param>
        public ConfigEntry<string> AddButtonConfig(string section,
            string key,
            string description,
            Dictionary<string, Action> buttonDic)
        {
            return AddButtonConfig(section,
                key,
                description,
                buttonDic,
                true,
                new ConfigurationManagerAttributes { HideDefaultButton = true }
                );
        }

        /// <summary>
        /// Add a button configuration
        /// </summary>
        /// <param name="section">Button config section</param>
        /// <param name="key">Button config key</param>
        /// <param name="description">Button description</param>
        /// <param name="buttonDic">Dictionary of buttons where string=buttonName, Action=buttonLogic</param>
        /// <param name="showLastPressedString">Set true to show string next to buttons of last button pressed</param>
        public ConfigEntry<string> AddButtonConfig(string section, 
            string key, 
            string description, 
            Dictionary<string, Action> buttonDic,
            bool showLastPressedString)
        {
            return AddButtonConfig(section,
                key,
                description,
                buttonDic,
                showLastPressedString,
                new ConfigurationManagerAttributes { HideDefaultButton = true }
                );
        }

        /// <summary>
        /// Add a button configuration - define your own ConfigurationManagerAttributes
        /// </summary>
        /// <param name="section">Button config section</param>
        /// <param name="key">Button config key</param>
        /// <param name="description">Button description</param>
        /// <param name="buttonDic">Dictionary of buttons where string=buttonName, Action=buttonLogic</param>
        /// <param name="showLastPressedString">Set true to show string next to buttons of last button pressed</param>
        /// <param name="attributes">Define your own custom attributes</param>
        public ConfigEntry<string> AddButtonConfig(
            string section, 
            string key, 
            string description,
            Dictionary<string, Action> buttonDic,
            bool showLastPressedString,
            ConfigurationManagerAttributes attributes)
        {
            return config.AddSetting<string>(
                section,
                key,
                "",
                new BepInEx.Configuration.ConfigDescription(
                    description,
                    null,
                    new ButtonDrawer(buttonDic, showLastPressedString).Draw(),
                    attributes
                    ));
        }
    }
}
