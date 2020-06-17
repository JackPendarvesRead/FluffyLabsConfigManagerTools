using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Infrastructure.ConfigEntry
{
    public class CheckboxTableConfigEntry
    {
        private ConfigEntry<CheckboxTable> configEntry;

        internal CheckboxTableConfigEntry(ConfigEntry<CheckboxTable> configEntry)
        {
            this.configEntry = configEntry;
        }

        public bool GetValue(string xValue, string yValue)
        {
            var entry = configEntry.Value.Items.Where(item => item.xLabel == xValue && item.yLabel == yValue).FirstOrDefault();
            return entry.Value;
        }
    }
}
