using ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawers
{
    internal class ButtonDrawer
    {
        public Action<SettingEntryBase> Draw(string buttonName, Action buttonLogic)
        {
            return (seb) =>
            {
                GUILayout.BeginVertical();
                if (GUILayout.Button(buttonName, GUILayout.ExpandWidth(true)))
                {
                    buttonLogic();
                }
                GUILayout.EndVertical();
            };
        }

        public Action<SettingEntryBase> DrawMultiple(Dictionary<string, Action> buttonDictionary)
        {
            return (seb) =>
            {
                GUILayout.BeginVertical();
                foreach (var button in buttonDictionary)
                {
                    if (GUILayout.Button(button.Key, GUILayout.ExpandWidth(true)))
                    {
                        button.Value();
                    }
                }
                GUILayout.EndVertical();
            };
        }
    }
}
