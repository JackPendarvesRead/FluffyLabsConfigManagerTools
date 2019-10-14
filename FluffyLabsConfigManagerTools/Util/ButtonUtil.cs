using BepInEx;
using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Drawers;
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

        public void AddButtonConfig(ConfigDefinition configDefinition, string buttonName, Action buttonLogic)
        {            
            AddButtonConfig(configDefinition.Section, configDefinition.Key, buttonName, buttonLogic);
        }
        public void AddButtonConfig(string section, string key, string buttonName, Action buttonLogic)
        {
            plugin.Config.AddSetting<string>(
                section,
                key,
                "",
                new BepInEx.Configuration.ConfigDescription(
                    "This is a button",
                    null,
                    new ButtonDrawer().Draw(buttonName, buttonLogic)
                    ));
        }

        public void AddMultipleButtonConfig(ConfigDefinition configDefinition, Dictionary<string, Action> buttonDic)
        {
            AddMultipleButtonConfig(configDefinition.Section, configDefinition.Key, buttonDic);
        }
        public void AddMultipleButtonConfig(string section, string key, Dictionary<string, Action> buttonDic)
        {
            plugin.Config.AddSetting<string>(
                section,
                key,
                "",
                new BepInEx.Configuration.ConfigDescription(
                    "This is a button",
                    null,
                    new ButtonDrawer().DrawMultiple(buttonDic)
                    ));
        }
    }
}
