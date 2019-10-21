using BepInEx;
using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Util
{
    /// <summary>
    /// Set up a MacroConfigEntry specifically designed to be used for macro configurations
    /// </summary>
    public sealed class MacroUtil
    {
        private readonly ConfigFile config;

        /// <summary>
        /// Set up a MacroConfigEntry specifically designed to be used for macro configurations
        /// </summary>
        /// <param name="config">The ConfigFile which to add ConfigEntry to</param>
        public MacroUtil(ConfigFile config)
        {
            this.config = config;
        }

        /// <summary>
        /// Set up a MacroConfigEntry specifically designed to be used for macro configurations
        /// </summary>
        /// <param name="section">MacroConfigEntry section</param>
        /// <param name="key">MacroConfigEntry key</param>
        /// <param name="description">MacroConfigEntry description</param>
        /// <returns>MacroConfigEntry</returns>
        public MacroConfigEntry AddMacroConfig(
            string section,
            string key,
            string description)
        {
            return AddMacroConfig(section, key, description, new ConfigurationManagerAttributes { HideDefaultButton = true });
        }


        /// <summary>
        /// Set up a MacroConfigEntry specifically designed to be used for macro configurations
        /// </summary>
        /// <param name="section">MacroConfigEntry section</param>
        /// <param name="key">MacroConfigEntry key</param>
        /// <param name="description">MacroConfigEntry description</param>
        /// <param name="isAdvanced">true if setting is advanced</param>
        /// <returns>MacroConfigEntry</returns>
        public MacroConfigEntry AddMacroConfig(string section, 
        string key, 
        string description, 
        ConfigurationManagerAttributes attributes)
        {
            var entry = config.AddSetting<Macro>(                
                section,                
                key,               
                new Macro
                {
                    KeyboardShortcut = new BepInEx.Configuration.KeyboardShortcut(UnityEngine.KeyCode.None),
                    MacroString = ">Type Macro Here<",
                    RepeatNumber = 1
                },
                new ConfigDescription(
                    description,    
                    null,
                    attributes
                    )
                );

            return new MacroConfigEntry(entry);
        }
    }
}
