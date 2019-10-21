using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Configuration;
using ConfigurationManager;
using FluffyLabsConfigManagerTools.Extension;
using FluffyLabsConfigManagerTools.Infrastructure;
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
                var x = (Conditional<T>)seb.Get();
                var condition = x.Condition;
                string label = "Disabled";
                if (x.Condition)
                {
                    label = "Enabled";
                }
                var newCondition = GUILayout.Toggle(condition, label, GUILayout.Width(DrawerConstants.FixedWidth));
                if (condition != newCondition)
                {
                    x.Condition = newCondition;
                    seb.Set(x);
                }
                if (condition)
                {
                    var number = x.Value.FloatToString<T>();
                    var result = GUILayout.TextField(number, GUILayout.ExpandWidth(true));
                    if (result != number)
                    {
                        try
                        {
                            x.Value = (T)Convert.ChangeType(result, typeof(T));
                            seb.Set(x);
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
