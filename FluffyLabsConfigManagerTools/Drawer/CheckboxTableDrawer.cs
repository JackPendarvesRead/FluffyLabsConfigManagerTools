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
        private static float fixedWidth;
        private List<string> distinctXLabels;
        private List<string> distinctYLabels;


        //public CheckboxTableDrawer(List<string> xUnits, List<string> yUnits)
        //{
        //    distinctXLabels = xUnits.Distinct().ToList();
        //    distinctYLabels = yUnits.Distinct().ToList();
        //}

        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                var checkboxTable = (CheckboxTable)seb.Get();
                fixedWidth = 500 / (distinctXLabels.Count + 1);
                GUILayout.BeginVertical();                
                DrawTopRowLabels(checkboxTable);
                foreach (var yLabel in distinctYLabels)
                {
                    DrawToggleRow(seb, checkboxTable, yLabel);
                }
                //DrawToggles(seb, checkboxTable);
                GUILayout.EndVertical();
            };
        }

        private static void DrawToggleRow(SettingEntryBase seb, CheckboxTable checkboxTable, string yLabel)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(yLabel, GUILayout.Width(fixedWidth));
            foreach (CheckboxTableItem item in checkboxTable.Items.Where(item => item.yLabel == yLabel))
            {                
                var newValue = GUILayout.Toggle(item.Value, "", GUILayout.Width(fixedWidth));
                SetValueIfChanged(seb, checkboxTable, item, newValue);
            }
            GUILayout.EndHorizontal();
        }

        private static void DrawToggles(SettingEntryBase seb, CheckboxTable checkboxTable)
        {
            GUILayout.BeginHorizontal();
            string currentLabel = checkboxTable.Items[0].yLabel;
            GUILayout.Label(currentLabel, GUILayout.Width(fixedWidth));
            foreach (CheckboxTableItem item in checkboxTable.Items)
            {
                if (item.yLabel != currentLabel)
                {
                    currentLabel = item.yLabel;
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label(currentLabel, GUILayout.Width(fixedWidth));
                }
                var newValue = GUILayout.Toggle(item.Value, "", GUILayout.Width(fixedWidth));
                SetValueIfChanged(seb, checkboxTable, item, newValue);
            }
            GUILayout.EndHorizontal();
        }

        private static void DrawTopRowLabels(CheckboxTable checkboxTable)
        {

            GUILayout.BeginHorizontal(GUILayout.Width(fixedWidth));
            GUILayout.Label("", GUILayout.Width(fixedWidth));
            foreach (var xLabel in checkboxTable.Items.Select(x => x.xLabel).Distinct())
            {
                GUILayout.Label(xLabel, GUILayout.Width(fixedWidth));
            }
            GUILayout.EndHorizontal();
        }

        private static void SetValueIfChanged(SettingEntryBase seb, CheckboxTable setting, CheckboxTableItem item, bool newValue)
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
        }
    }
}
