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
    internal struct Conditional<T>
        where T : struct, IConvertible
    {
        public T Value { get; set; }
        public bool Condition { get; set; }

        static Conditional()
        {
            TomlTypeConverter.AddConverter(typeof(Conditional<T>), Conditional<T>.GetTypeConverter());
            ConfigurationManager.ConfigurationManager.RegisterCustomSettingDrawer(typeof(Conditional<T>), new ConditionalDrawer<T>().Draw());
        }

        private static TypeConverter GetTypeConverter()
        {
            return new TypeConverter
            {
                ConvertToObject = (s, type) =>
                {
                    try
                    {
                        var split = s.Split(SpecialCharacter.Delimiter);
                        return new Conditional<T>
                        {
                            Value = (T)Convert.ChangeType(split[0], typeof(T)),
                            Condition = bool.Parse(split[1])
                        };
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(ex);
                        throw;
                    }
                },
                ConvertToString = (obj, type) =>
                {
                    try
                    {
                        var x = (Conditional<T>)obj;
                        return string.Join(SpecialCharacter.Delimiter.ToString(), x.Value.ToString(), x.Condition.ToString());
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
