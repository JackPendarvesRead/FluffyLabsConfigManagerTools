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
        private ConfigEntry<Macro> configEntry;

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
                configEntry.Value = new Macro
                {
                    KeyboardShortcut = configEntry.Value.KeyboardShortcut,
                    MacroString = value,
                    RepeatNumber = configEntry.Value.RepeatNumber
                };
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
                configEntry.Value = new Macro
                {
                    KeyboardShortcut = configEntry.Value.KeyboardShortcut,
                    MacroString = configEntry.Value.MacroString,
                    RepeatNumber = value
                };
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
                configEntry.Value = new Macro
                {
                    KeyboardShortcut = value,
                    MacroString = configEntry.Value.MacroString,
                    RepeatNumber = configEntry.Value.RepeatNumber
                };
            }
        }
    }
}
