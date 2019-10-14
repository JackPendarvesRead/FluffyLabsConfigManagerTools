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
    public class ConditionalUtil
    {
        private readonly BaseUnityPlugin plugin;

        public ConditionalUtil(BaseUnityPlugin plugin)
        {
            this.plugin = plugin;
        }

        public ConfigEntry<Conditional<T>> AddConditionalConfig<T>(string section, string key, T defaultValue, bool defaultCondition, ConfigDescription description)
            where T : struct, IConvertible
        {
            return plugin.Config
                .AddSetting<Conditional<T>>(                
                section,
                key,
                new Conditional<T> { Condition = defaultCondition, Value = defaultValue },
                description);                
        }
    }
}
