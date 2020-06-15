using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluffyLabsConfigManagerTools.Util
{
    public class LinkedPercentageConfigUtil
    {
        private readonly ConfigFile config;

        public Dictionary<string, ConfigEntry<float>> ConfigEntryDic { get; set; }

        public LinkedPercentageConfigUtil(ConfigFile config)
        {
            this.config = config;
            ConfigEntryDic = new Dictionary<string, ConfigEntry<float>>();
        }

        public void AddLinkedConfig(string section, string description, params string[] keys)
        {
            foreach(var k in keys)
            {
                var entry = config.Bind(section, k, 0.0f, new ConfigDescription(description, new AcceptableValueRange<float>(0, 1)));
                ConfigEntryDic.Add(k, entry);
            }

            config.SettingChanged += Config_SettingChanged;
        }

        private bool balanceInProgress = false;
        private void Config_SettingChanged(object sender, SettingChangedEventArgs e)
        {
            if (!balanceInProgress)
            {
                var changed = ConfigEntryDic.Values.Where(x => x.Description == e.ChangedSetting.Description).FirstOrDefault();
                if (changed != null && ConfigEntryDic.Values.Select(x => x.Value).Sum() != 1.0f)
                {
                    var others = ConfigEntryDic.Values.Where(x => x != changed);
                    BalanceSetting(changed, others);
                }
            }
        }

        private void BalanceSetting(ConfigEntry<float> changed, IEnumerable<ConfigEntry<float>> others)
        {
            try
            {
                balanceInProgress = true;
                var totalOthers = others.Select(x => x.Value).Sum();
                var diff = 1.0f - changed.Value - totalOthers;
                var changeValue = diff / others.Count();
                RebalanceOthers(changeValue, others);
                var newSum = changed.Value + others.Select(x => x.Value).Sum();
                if (newSum != 1.0f)
                {
                    changed.Value += 1.0f - newSum;
                }
            }
            finally
            {
                balanceInProgress = false;
            }
        }

        private void RebalanceOthers(float changeValue, IEnumerable<ConfigEntry<float>> entries)
        {
            IEnumerable<ConfigEntry<float>> filtered;
            var leftover = 0f;
            if (changeValue < 0)
            {
                filtered = entries.Where(x => x.Value + changeValue < 0);
                foreach (var f in filtered)
                {
                    var diff = changeValue + f.Value;
                    leftover += diff;
                    f.Value = 0;
                };
            }
            else
            {
                filtered = entries.Where(x => x.Value + changeValue > 1);
                foreach (var f in filtered)
                {
                    var diff = f.Value + changeValue - 1;
                    leftover += diff;
                    f.Value = 1;
                }
            }

            if (filtered.Any())
            {
                var rest = entries.Where(x => !filtered.Contains(x)).ToArray();
                var newChange = changeValue + (leftover / rest.Count());
                RebalanceOthers(newChange, rest);
            }
            else
            {
                foreach (var e in entries)
                {
                    e.Value += changeValue;
                }
            }
        }
    }
}
