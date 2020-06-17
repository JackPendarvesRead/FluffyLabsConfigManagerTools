using BepInEx.Configuration;
using FluffyLabsConfigManagerTools.Drawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FluffyLabsConfigManagerTools.Infrastructure
{
    internal struct CheckboxTable
    {
        public List<CheckboxTableItem> Items { get; set; }

        static CheckboxTable()
        {
            TomlTypeConverter.AddConverter(typeof(CheckboxTable), GetTypeConverter());
            ConfigurationManager.ConfigurationManager.RegisterCustomSettingDrawer(typeof(CheckboxTable), new CheckboxTableDrawer().Draw());
        }

        public CheckboxTable(List<string> xLabels, List<string> yLabels)
        {
            Items = new List<CheckboxTableItem>();
            foreach (var x in xLabels)
            {
                foreach(var y in yLabels)
                {
                    Items.Add(new CheckboxTableItem(x, y, true));
                }
            }
        }

        public CheckboxTable(List<CheckboxTableItem> Items)
        {
            this.Items = Items;
        }

        //public List<CheckboxTableItem> GetNewItemsList(string xValue, string yValue, bool newValue)
        //{
        //    var newItems = Items;
        //    var changedEntry = newItems.Where(item => item.xLabel == xValue && item.yLabel == yValue).First();
        //    changedEntry.Value = newValue;
        //    return newItems;
        //}

        private static TypeConverter GetTypeConverter()
        {
            return new TypeConverter
            {
                ConvertToString = (obj, type) =>
                {
                    try
                    {
                        var sb = new StringBuilder();
                        var table = (CheckboxTable)obj;

                        for(var i =0; i < table.Items.Count; i++)
                        {
                            sb.Append(table.Items[i].xLabel + 
                                SpecialCharacter.Delimiter2 +
                                table.Items[i].yLabel +
                                SpecialCharacter.Delimiter2 +
                                table.Items[i].Value);
                            if(i < table.Items.Count - 1)
                            {
                                sb.Append(SpecialCharacter.Delimiter);
                            }
                        }
                        return sb.ToString();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Exception caught in ConvertToString TypeConverter: " + ex);
                        throw;
                    }
                },
                ConvertToObject = (s, type) =>
                {
                    try
                    {
                        var split = s.Split(SpecialCharacter.Delimiter);
                        var items = new List<CheckboxTableItem>();
                        foreach(var item in split)
                        {
                            var split2 = item.Split(SpecialCharacter.Delimiter2);
                            items.Add(new CheckboxTableItem
                            {
                                xLabel = split2[0],
                                yLabel = split2[1],
                                Value = Convert.ToBoolean(split2[2])
                            });
                        }
                        return new CheckboxTable(items);                        
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("Exception caught in ConvertToObject TypeConverter: " + ex);
                        throw;
                    }
                }
            };
        }
    }
}
