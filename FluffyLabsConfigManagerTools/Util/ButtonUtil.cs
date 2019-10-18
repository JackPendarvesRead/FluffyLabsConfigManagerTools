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
    public class ButtonUtil
    {
        private readonly BaseUnityPlugin plugin;

        /// <summary>
        /// Class to help add button configurations
        /// </summary>
        /// <param name="plugin">The plugin to add ConfigEntry to</param>
        public ButtonUtil(BaseUnityPlugin plugin)
        {
            this.plugin = plugin;
        }
       
        /// <summary>
        /// Add a button configuration
        /// </summary>
        /// <param name="configDefinition">Bepinex ConfigDefinition of button</param>
        /// <param name="description">Button description</param>
        /// <param name="buttonDic">Dictionary of buttons where string=buttonName, Action=buttonLogic</param>
        public void AddButtonConfig(ConfigDefinition configDefinition, string description, Dictionary<string, Action> buttonDic)
        {
            AddButtonConfig(configDefinition.Section, configDefinition.Key, description, buttonDic);
        }
        /// <summary>
        /// Add a button configuration
        /// </summary>
        /// <param name="section">Button config section</param>
        /// <param name="key">Button config key</param>
        /// <param name="description">Button description</param>
        /// <param name="buttonDic">Dictionary of buttons where string=buttonName, Action=buttonLogic</param>
        public void AddButtonConfig(string section, string key, string description, Dictionary<string, Action> buttonDic)
        {            
            plugin.Config.AddSetting<string>(
                section,
                key,
                "",
                new BepInEx.Configuration.ConfigDescription(
                    description,
                    null,
                    new ButtonDrawer(buttonDic).Draw(), 
                    new ConfigurationManagerAttributes { HideDefaultButton = true }
                    ));
        }
    }
}
