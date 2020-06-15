using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools
{
    [BepInDependency(ConfigurationManager.ConfigurationManager.GUID)]
    [BepInPlugin(PluginGuid, pluginName, pluginVersion)]
    public class FluffyConfigLabsPlugin : BaseUnityPlugin
    {
        public const string PluginGuid = "com.FluffyMods." + pluginName;
        private const string pluginName = "FluffyLabsConfigManagerTools";
        private const string pluginVersion = "1.0.1";
    }
}
