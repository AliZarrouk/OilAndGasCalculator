namespace Core.Utilities
{
    public static class MathUtilities
    {
        // http://www.metric-conversions.org/volume/cubic-feet-to-us-oil-barrels.htm
        private static readonly double[,] Conversions = { { 1, 0.0283168, 0.1781076 }, { 35.31467, 1, 6.289811 }, { 5.6146, 0.15899, 1 } };


        public static double GetCentralPoint(double a, double b, double c, double d)
        {
            return (a + b + c + d) / 4;
        }

        public static double GetRectangularTankVolume(double x, double y, double z)
        {
            return x * y * z;
        }
    }
}
