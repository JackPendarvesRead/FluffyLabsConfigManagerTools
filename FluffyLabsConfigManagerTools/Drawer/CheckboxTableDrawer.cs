using ConfigurationManager;
using FluffyLabsConfigManagerTools.Infrastructure;
using FluffyLabsConfigManagerTools.Infrastructure.Constant;
using FluffyLabsConfigManagerTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var currentLabel = setting.Items.First().xLabel;

                GUILayout.BeginHorizontal();
                GUILayout.Label(currentLabel);
                foreach (CheckboxTableItem item in setting.Items)
                {
                    if(item.xLabel != currentLabel)
                    {
                        GUILayout.EndHorizontal();
                        currentLabel = item.xLabel;
                        GUILayout.FlexibleSpace();
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(currentLabel);
                    }
                    var newValue = GUILayout.Toggle(item.Value, item.yLabel, GUILayout.Width(DrawerConstants.FixedWidth));
                    if (newValue != item.Value)
                    {
                        setting.SetValue(item.xLabel, item.yLabel, newValue);
                        seb.Set(setting);
                    }
                }
            };
        }
    }
}
