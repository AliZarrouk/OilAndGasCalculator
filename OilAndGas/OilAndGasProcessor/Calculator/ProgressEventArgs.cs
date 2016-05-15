using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OilAndGasProcessor.Calculator
{
    public class ProgressEventArgs : EventArgs
    {
        public ProgressEventArgs(int total)
        {
            Total = total;
        }
        public int Total { get; private set; }
    }
}
