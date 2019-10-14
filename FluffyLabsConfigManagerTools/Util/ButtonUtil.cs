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
    public class ButtonUtil
    {
        private readonly BaseUnityPlugin plugin;

        public ButtonUtil(BaseUnityPlugin plugin)
        {
            this.plugin = plugin;
        }
       
        public void AddButtonConfig(ConfigDefinition configDefinition, string description, Dictionary<string, Action> buttonDic)
        {
            AddButtonConfig(configDefinition.Section, configDefinition.Key, description, buttonDic);
        }
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
