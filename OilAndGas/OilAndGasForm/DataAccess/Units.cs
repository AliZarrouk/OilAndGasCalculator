using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilAndGasForm.DataAccess
{
    public enum Units
    {
        [Description("Cubic Meter")]
        CubicMeter,
        [Description("Barrel")]
        Barrel,
        [Description("Cubic Feet")]
        CubicFeet
    }
}
