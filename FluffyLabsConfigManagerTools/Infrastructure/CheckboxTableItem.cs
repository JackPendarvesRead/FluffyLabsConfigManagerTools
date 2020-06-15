using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Infrastructure
{
    internal struct CheckboxTableItem
    {
        public bool Value { get; set; }
        public string xLabel { get; set; }
        public string yLabel { get; set; }

        public CheckboxTableItem(string xLabel, string yLabel, bool selected)
        {
            this.xLabel = xLabel;
            this.yLabel = yLabel;
            Value = selected;
        }

        private static TypeConverter GetTypeConverter()
        {
            return new TypeConverter
            {
                ConvertToString = (obj, type) =>
                {
                    try
                    {
                        var tableItem = (CheckboxTableItem)obj;
                        return tableItem.Value.ToString()
                        + SpecialCharacter.Delimiter
                        + tableItem.xLabel
                        + SpecialCharacter.Delimiter
                        + tableItem.yLabel;
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
                            KeyboardShortcut = BepInEx.Configuration.KeyboardShortcut.Deserialize(split[2]),
                            isRepeating = bool.Parse(split[3])
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
