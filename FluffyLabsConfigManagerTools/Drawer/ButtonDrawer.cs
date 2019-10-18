﻿using ConfigurationManager;
using FluffyLabsConfigManagerTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawers
{
    internal class ButtonDrawer : IDrawer
    {
        private readonly Dictionary<string, Action> buttonDictionary;

        public ButtonDrawer(Dictionary<string, Action> buttonDictionary)
        {
            this.buttonDictionary = buttonDictionary;
        }

        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                var labelString = seb.Get().ToString();
                GUILayout.Label(labelString, GUILayout.Width(100f));
                GUILayout.BeginVertical();
                foreach (var button in buttonDictionary)
                {
                    if (GUILayout.Button(button.Key, GUILayout.ExpandWidth(true)))
                    {                        
                        button.Value();
                        seb.Set(button.Key);
                    }
                }
                GUILayout.EndVertical();
            };
        }
    }
}
