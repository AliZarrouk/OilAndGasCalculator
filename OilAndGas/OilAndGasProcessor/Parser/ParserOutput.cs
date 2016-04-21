using System.Collections.Generic;
using OilAndGasProcessor.DataModeling;

namespace OilAndGasProcessor.Parser
{
    public class ParserOutput
    {
        public IEnumerable<Cell> Cells { get; set; }

        public double FluidContact { get; set; }

        public double BaseHorizon { get; set; }

        public int Precision { get; set; }
    }
}
