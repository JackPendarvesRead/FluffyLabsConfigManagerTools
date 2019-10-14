﻿using BepConfigManagerTest.Drawers;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BepConfigManagerTest.Infrastructure
{
    struct Conditional<T>
        where T : struct, IConvertible
    {
        static Conditional()
        {
            TomlTypeConverter.AddConverter(typeof(Conditional<T>), Conditional<T>.GetTypeConverter());
            ConfigurationManager.ConfigurationManager.RegisterCustomSettingDrawer(typeof(Conditional<T>), new ConditionalDrawer<T>().Draw());
        }

        public T Value { get; set; }
        public bool Condition { get; set; }

        public static TypeConverter GetTypeConverter()
        {
            return new TypeConverter
            {
                ConvertToObject = (s, type) =>
                {
                    try
                    {
                        var split = s.Split(Special.Delimiter);
                        return new GenericConditional<T>
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
                        var x = (GenericConditional<T>)obj;
                        return string.Join(Special.Delimiter.ToString(), x.Value.ToString(), x.Condition.ToString());
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
