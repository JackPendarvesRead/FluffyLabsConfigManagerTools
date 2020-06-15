using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ConfigDescription description)
        {
            var entry = config.Bind<CheckboxTable>(section, key, new CheckboxTable(xLabels, yLabels), description);
            return new CheckboxTableConfigEntry(entry);
        }
    }
}
