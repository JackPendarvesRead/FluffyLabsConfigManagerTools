using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Drawers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Infrastructure
{
    public struct Macro
    {
        static Macro()
        {            
            TomlTypeConverter.AddConverter(typeof(Macro), Macro.GetTypeConverter());
            ConfigurationManager.ConfigurationManager.RegisterCustomSettingDrawer(typeof(Macro), new MacroDrawer().Draw());            
        }

        public string MacroString { get; set; }
        public int RepeatNumber { get; set; }
        public BepInEx.Configuration.KeyboardShortcut KeyboardShortcut { get; set; }

        public static TypeConverter GetTypeConverter()
        {
            return new TypeConverter
            {
                ConvertToString = (obj, type) =>
                {
                    try
                    {
                        var macro = (Macro)obj;
                        var kb = macro.KeyboardShortcut.Serialize();
                        return macro.MacroString + SpecialCharacter.Delimiter + macro.RepeatNumber.ToString() + SpecialCharacter.Delimiter + kb;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                        throw;
                    }
                },
                ConvertToObject = (s, type) =>
                {
                    try
                    {
                        var split = s.Split(SpecialCharacter.Delimiter);
                        return new Macro
                        {
                            MacroString = split[0],
                            RepeatNumber = Int32.Parse(split[1]),
                            KeyboardShortcut = BepInEx.Configuration.KeyboardShortcut.Deserialize(split[2])
                        };
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                        throw;
                    }
                }
            };
        }
    }
}
