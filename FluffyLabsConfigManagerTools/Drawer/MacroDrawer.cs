using ConfigurationManager;
using FluffyLabsConfigManagerTools.Infrastructure;
using FluffyLabsConfigManagerTools.Infrastructure.Constant;
using FluffyLabsConfigManagerTools.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Drawer
{
    internal class MacroDrawer : IDrawer
    {
        public Action<SettingEntryBase> Draw()
        {
            return (seb) =>
            {
                GUILayout.BeginVertical();
                DrawMacroStringBox(seb);
                DrawRepeatCountBox(seb);
                DrawKeyboardShortcutBox(seb);
                GUILayout.Space(20);
                GUILayout.EndVertical();
            };
        }

        private Texture2D GetBackground()
        {
            var background = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            background.SetPixel(0, 0, Color.black);
            background.Apply();
            return background;
        }

        private void DrawMacroStringBox(SettingEntryBase seb)
        {
            var style = new GUIStyle
            {
                normal = new GUIStyleState { textColor = Color.white, background = GetBackground() },
                wordWrap = true
            };

            var macro = (Macro)seb.Get();
            var text = macro.MacroString;
            var result = GUILayout.TextField(text, style, GUILayout.ExpandWidth(true), GUILayout.MinHeight(80f));
            if (result != text)
            {
                try
                {
                    macro.MacroString = result;
                    seb.Set(macro);
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
        }

        private void DrawRepeatCountBox(SettingEntryBase seb)
        {
            var macro = (Macro)seb.Get();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Repeat:", GUILayout.Width(DrawerConstants.FixedWidth));
            var condition = macro.isRepeating;
            string label = "";
            //string label = condition ? "Enabled" : "Disabled";
            var newCondition = GUILayout.Toggle(condition, label, GUILayout.Width(DrawerConstants.FixedWidth));
            if (condition != newCondition)
            {
                macro.isRepeating = newCondition;
                seb.Set(macro);
            }
            GUILayout.FlexibleSpace();
            if (macro.isRepeating)
            {
                var repeatCount = macro.RepeatNumber.ToString();
                var result = GUILayout.TextField(repeatCount, GUILayout.Width(DrawerConstants.FixedWidth));
                if (result != repeatCount)
                {
                    try
                    {
                        macro.RepeatNumber = Int32.Parse(result);
                        seb.Set(macro);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                    }
                }
            }            
            GUILayout.EndHorizontal();
        }

        private static readonly IEnumerable<KeyCode> _keysToCheck =
            BepInEx.Configuration.KeyboardShortcut
            .AllKeyCodes
            .Except(new[] { KeyCode.Mouse0, KeyCode.None })
            .ToArray();

        private List<KeyCode> keyCodeList = new List<KeyCode>();
        private SettingEntryBase currentKeyToSet;
        private void DrawKeyboardShortcutBox(SettingEntryBase seb)
        {
            GUILayout.BeginHorizontal();
            if (seb == currentKeyToSet)
            {
                DrawEdittingKeyboard(seb);
            }
            else
            {
                DrawAwaitingEditKeyboard(seb);
            }
            GUILayout.EndHorizontal();
        }

        private void DrawAwaitingEditKeyboard(SettingEntryBase seb)
        {
            var macro = (Macro)seb.Get();
            GUILayout.Label("Shortcut:", GUILayout.Width(DrawerConstants.FixedWidth));
            GUILayout.BeginVertical();
            if (macro.KeyboardShortcut.MainKey == KeyCode.None)
            {
                GUILayout.Label("N/A", GUILayout.ExpandWidth(true));
            }
            else
            {
                GUILayout.Label($"(Main) {macro.KeyboardShortcut.MainKey.ToString()}", GUILayout.ExpandWidth(true));
                foreach (var mod in macro.KeyboardShortcut.Modifiers)
                {
                    GUILayout.Label($"+ {mod.ToString()}", GUILayout.ExpandWidth(true));
                }
            }
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            if (GUILayout.Button("Edit", GUILayout.Width(DrawerConstants.FixedWidth)))
            {
                currentKeyToSet = seb;
            }
            if (GUILayout.Button("Clear", GUILayout.Width(DrawerConstants.FixedWidth)))
            {
                macro.KeyboardShortcut = BepInEx.Configuration.KeyboardShortcut.Empty;
                seb.Set(macro);
            }
            GUILayout.EndVertical();
        }

        private void DrawEdittingKeyboard(SettingEntryBase seb)
        {
            var macro = (Macro)seb.Get();
            GUIUtility.keyboardControl = -1;
            Event e = Event.current;
            if (e.isKey
                && _keysToCheck.Contains(e.keyCode)
                && !keyCodeList.Contains(e.keyCode))
            {
                keyCodeList.Add(e.keyCode);
            }

            if (keyCodeList.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var code in keyCodeList)
                {
                    sb.Append(code.ToString());
                    if (keyCodeList.Last() != code)
                    {
                        sb.Append(" + ");
                    }
                }
                GUILayout.Label(sb.ToString(), GUILayout.ExpandWidth(true));
            }
            else
            {
                GUILayout.Label("Press any key combination...", GUILayout.ExpandWidth(true));
            }
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            if (GUILayout.Button("OK", GUILayout.Width(DrawerConstants.FixedWidth)))
            {
                if (keyCodeList.Count > 0)
                {
                    if (keyCodeList.Count > 1)
                    {
                        macro.KeyboardShortcut = new BepInEx.Configuration.KeyboardShortcut(keyCodeList.Last(), keyCodeList.Where(k => k != keyCodeList.Last()).ToArray());
                    }
                    else
                    {
                        macro.KeyboardShortcut = new BepInEx.Configuration.KeyboardShortcut(keyCodeList[0]);
                    }
                    seb.Set(macro);
                }
                keyCodeList.Clear();
                currentKeyToSet = null;
            }
            if (GUILayout.Button("Cancel", GUILayout.Width(DrawerConstants.FixedWidth)))
            {
                keyCodeList.Clear();
                currentKeyToSet = null;
            }
            GUILayout.EndVertical();
        }
    }
}
