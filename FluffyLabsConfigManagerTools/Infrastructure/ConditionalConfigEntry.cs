﻿using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Infrastructure
{
    public class ConditionalConfigEntry<T>
       where T : struct, IConvertible
    {
        private readonly ConfigEntry<Conditional<T>> configEntry;

        internal ConditionalConfigEntry(ConfigEntry<Conditional<T>> configEntry)
        {
            this.configEntry = configEntry;
        }

        public T Value
        {
            get
            {
                return configEntry.Value.Value;
            }
            set
            {
                configEntry.Value.Value = value;                
            }
        }
        public bool Condition
        {
            get
            {
                return configEntry.Value.Condition;
            }
            set
            {
                configEntry.Value.Condition = value;
            }
        }
    }
}
