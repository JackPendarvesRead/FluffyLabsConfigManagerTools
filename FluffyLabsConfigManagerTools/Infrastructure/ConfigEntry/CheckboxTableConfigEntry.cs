using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Infrastructure
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
            var entry = configEntry.Value.Items.Where(item => item.xLabel == xValue && item.yLabel == yValue).First();
            return entry.Value;
        }

        //public bool GetValue(Func<CheckboxTableItem, bool> selector)
        //{
        //    var entry = configEntry.Value.Items.Where(selector).First();
        //    return entry.Value;
        //}
    }
}
