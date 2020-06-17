using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Drawer;
using FluffyLabsConfigManagerTools.Infrastructure;
using FluffyLabsConfigManagerTools.Infrastructure.ConfigEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Util
{
    public class CheckboxTableUtil
    {
        private readonly ConfigFile config;

        public CheckboxTableUtil(ConfigFile config)
        {
            this.config = config;
        }

        public CheckboxTableConfigEntry CreateTable(
            string section,
            string key,
            List<string> xLabels,
            List<string> yLabels,
            string description)
        {
            var newDefault = new CheckboxTable(xLabels, yLabels);
            var configDefinition = new ConfigDefinition(section, key);
            var configDescription = new ConfigDescription(description, null, new ConfigurationManagerAttributes() { HideDefaultButton = true, HideSettingName = true });
            Debug.Log("Trying to find entry...");
            if (config.TryGetEntry<CheckboxTable>(configDefinition, out ConfigEntry<CheckboxTable> currentEntry))
            {
                Debug.Log("GOT THE CURRENT ENTRY");
                var currentDefault = (CheckboxTable)currentEntry.DefaultValue;
                if (!newDefault.Items.Select(x => x.xLabel).Equals(currentDefault.Items.Select(x => x.xLabel)) ||
                    !newDefault.Items.Select(x => x.yLabel).Equals(currentDefault.Items.Select(x => x.yLabel)))
                {
                    Debug.Log("IT DIDNT MATCH");
                    currentEntry.Value = newDefault;
                }
                else
                {
                    Debug.Log("IT MATCHED!!!");
                }
            }
            else
            {
                Debug.Log("Didn't find entry");
            }
            var newEntry = config.Bind<CheckboxTable>(configDefinition, newDefault, configDescription);
            return new CheckboxTableConfigEntry(newEntry);
        }
    }
}
