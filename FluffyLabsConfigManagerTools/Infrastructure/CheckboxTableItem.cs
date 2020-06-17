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
    }
}
