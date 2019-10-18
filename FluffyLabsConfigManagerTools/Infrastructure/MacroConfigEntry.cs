using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Infrastructure
{
    public class MacroConfigEntry
    {
        private readonly ConfigEntry<Macro> configEntry;

        internal MacroConfigEntry(ConfigEntry<Macro> configEntry)
        {
            this.configEntry = configEntry;
        }

        public string MacroString
        {
            get
            {
                return configEntry.Value.MacroString;
            }
            set
            {
                configEntry.Value.MacroString = value;
            }
        }

        public int RepeatCount
        {
            get
            {
                return configEntry.Value.RepeatNumber;
            }
            set
            {
                configEntry.Value.RepeatNumber = value;
            }
        }

        public KeyboardShortcut KeyboardShortcut
        {
            get
            {
                return configEntry.Value.KeyboardShortcut;
            }
            set
            {
                configEntry.Value.KeyboardShortcut = value;
            }
        }
    }
}
