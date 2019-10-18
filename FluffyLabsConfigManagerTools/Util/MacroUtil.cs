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

        public MacroConfigEntry AddMacroConfig(string section, string key, string description, bool isAdvanced)
        {
            var entry = plugin.Config.AddSetting<Macro>(                
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
                    new ConfigurationManagerAttributes { IsAdvanced = isAdvanced }
                    )
                );

            return new MacroConfigEntry(entry);
        }
    }
}
