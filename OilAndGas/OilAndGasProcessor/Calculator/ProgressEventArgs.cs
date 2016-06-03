using System;

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
