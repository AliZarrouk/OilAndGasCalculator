using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OilAndGasProcessor.Enums;
using OilAndGasProcessor.Parser;

namespace OilAndGasProcessor.Calculator
{
    public class CalculatorInput : ParserOutput
    {
        public Unit BaseUnit { get; set; }
    }
}
