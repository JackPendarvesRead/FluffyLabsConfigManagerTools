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
    /// <summary>
    /// Class to add conditional configurations
    /// </summary>
    public class ConditionalUtil
    {
        private readonly BaseUnityPlugin plugin;

        /// <summary>
        /// Class to add conditional configurations
        /// </summary>
        /// <param name="plugin">The plugin to add ConfigEntry to</param>
        public ConditionalUtil(BaseUnityPlugin plugin)
        {
            this.plugin = plugin;
        }

        /// <summary>
        /// Class to add conditional configurations
        /// </summary>
        /// <typeparam name="T">Conditional type</typeparam>
        /// <param name="section">Conditional config section</param>
        /// <param name="key">Conditional config key</param>
        /// <param name="defaultValue">Default value of conditional type</param>
        /// <param name="defaultCondition">Default condition</param>
        /// <param name="description">Description of conditional config</param>
        /// <returns>ConfigEntry</returns>
        public ConditionalConfigEntry<T> AddConditionalConfig<T>(string section, string key, T defaultValue, bool defaultCondition, ConfigDescription description)
            where T : struct, IConvertible
        {
            var entry = plugin.Config
                .AddSetting<Conditional<T>>(                
                section,
                key,
                new Conditional<T> { Condition = defaultCondition, Value = defaultValue },
                description);

            return new ConditionalConfigEntry<T>(entry);
        }
    }
}
