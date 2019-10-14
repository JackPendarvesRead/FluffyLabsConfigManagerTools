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
    public class MacroUtil
    {
        private readonly BaseUnityPlugin plugin;

        public MacroUtil(BaseUnityPlugin plugin)
        {
            this.plugin = plugin;
        }

        public ConfigEntry<Macro> AddMacroConfig(string section, string key, string description, bool isAdvanced)
        {
            return plugin.Config.AddSetting<Macro>(                
                section,                
                key,               
                new Macro
                {
                    KeyboardShortcut = new BepInEx.Configuration.KeyboardShortcut(UnityEngine.KeyCode.None),
                    MacroString = "",
                    RepeatNumber = 1
                },
                new ConfigDescription(
                    description,    
                    null,
                    new ConfigurationManagerAttributes { IsAdvanced = isAdvanced }
                    )
                );
        }
    }
}
