using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using ConfigurationManager;
using FluffyLabsConfigManagerTools.Extension;
using FluffyLabsConfigManagerTools.Infrastructure;
using FluffyLabsConfigManagerTools.Infrastructure.Constant;
using FluffyLabsConfigManagerTools.Interfaces;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawer
{
    internal class ConditionalDrawer<T> : IDrawer
        where T : struct, IConvertible
    {
        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                GUILayout.BeginHorizontal();
                var setting = (Conditional<T>)seb.Get();
                var condition = setting.Condition;
                string label = condition ? "Enabled" : "Disabled";
                var newCondition = GUILayout.Toggle(condition, label, GUILayout.Width(DrawerConstants.FixedWidth));
                if (condition != newCondition)
                {
                    setting.Condition = newCondition;
                    seb.Set(setting);
                }
                if (condition)
                {
                    var number = setting.Value.FloatToString<T>();
                    var result = GUILayout.TextField(number, GUILayout.ExpandWidth(true));
                    if (result != number)
                    {
                        try
                        {
                            setting.Value = (T)Convert.ChangeType(result, typeof(T));
                            seb.Set(setting);
                        }    
                        catch (Exception ex)
                        {
                            Debug.LogError(ex);
                        }
                    }
                }
                GUILayout.EndHorizontal();
            };
        }
    }
}
