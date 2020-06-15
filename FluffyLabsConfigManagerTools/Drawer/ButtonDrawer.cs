using ConfigurationManager;
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
    internal class ButtonDrawer : IDrawer
    {
        private readonly Dictionary<string, Action> buttonDictionary;
        private readonly bool drawLabel;

        public ButtonDrawer(Dictionary<string, Action> buttonDictionary, bool drawLabel)
        {
            this.buttonDictionary = buttonDictionary;
            this.drawLabel = drawLabel;
        }

        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                if (drawLabel)
                {
                    var labelString = seb.Get().ToString();
                    GUILayout.Label(labelString, GUILayout.Width(DrawerConstants.FixedWidth));
                }               
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
