using ConfigurationManager;
using FluffyLabsConfigManagerTools.Infrastructure;
using FluffyLabsConfigManagerTools.Infrastructure.Constant;
using FluffyLabsConfigManagerTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawer
{
    internal class CheckboxTableDrawer : IDrawer
    {
        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                var setting = (CheckboxTable)seb.Get();
                GUILayout.BeginVertical();
                string currentLabel = setting.Items[0].xLabel;
                GUILayout.BeginHorizontal();
                foreach (CheckboxTableItem item in setting.Items)
                {
                    if (item.xLabel != currentLabel)
                    {
                        currentLabel = item.xLabel;
                        GUILayout.EndHorizontal();
                        GUILayout.BeginHorizontal();
                    }
                    var newValue = GUILayout.Toggle(item.Value, item.xLabel + item.yLabel, GUILayout.Width(DrawerConstants.FixedWidth));
                    setting = SetValueIfChanged(seb, setting, item, newValue);
                }
                GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            };
        }

        private static CheckboxTable SetValueIfChanged(SettingEntryBase seb, CheckboxTable setting, CheckboxTableItem item, bool newValue)
        {
            if (newValue != item.Value)
            {
                var newList = new List<CheckboxTableItem>();
                for (var i = 0; i < setting.Items.Count; i++)
                {

                    if (setting.Items[i].xLabel == item.xLabel && setting.Items[i].yLabel == item.yLabel)
                    {
                        newList.Add(new CheckboxTableItem(item.xLabel, item.yLabel, newValue));
                    }
                    else
                    {
                        newList.Add(setting.Items[i]);
                    }
                }
                setting.Items = newList;
                seb.Set(setting);
            }
            return setting;
        }
    }
}
