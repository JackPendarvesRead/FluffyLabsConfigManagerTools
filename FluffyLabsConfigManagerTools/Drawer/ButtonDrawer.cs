﻿using ConfigurationManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawers
{
    public class ButtonDrawer
    {
        public Action<SettingEntryBase> Draw(string buttonName, Action buttonLogic)
        {
            return (seb) =>
            {
                bool PressButton()
                {
                    return GUILayout.Button(buttonName, GUILayout.ExpandWidth(true));
                }
                if (PressButton())
                {
                    buttonLogic();
                }
            };
        }

        public Action<SettingEntryBase> DrawMultiple(Dictionary<string, Action> buttonDictionary)
        {
            return (seb) =>
            {
                foreach (var button in buttonDictionary)
                {
                    bool PressButton()
                    {
                        return GUILayout.Button(button.Key, GUILayout.ExpandWidth(true));
                    }
                    if (PressButton())
                    {
                        button.Value();
                    }
                }
            };
        }
    }
}
