using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities;
using log4net;

namespace OilAndGasProcessor.DataModeling
{
    public class Cell
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Cell));

        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double Lateral { get; set; }

        public IEnumerable<Cell> DivideCell(int precision)
        {
            try
            {
                var output = new List<Cell> { this };

                if (precision == 0)
                    return output;

                var sync = new object();

                for (var i = 0; i < precision; i++)
                {
                    var temp = output;

                    output = new List<Cell>();

                    Parallel.ForEach(temp, cell =>
                    {
                        lock (sync)
                        {
                            output.AddRange(cell.DivideCellTo4());
                        }
                    });
                }

                return output;
            }
            catch (Exception e)
            {
                Log.Error("Error while dividing cell", e);
                throw;
            }
        }

        public double GetVolumeFromCellToFluidContact(double fluidContact, double baseHorizon)
        {
            try
            {
                if (!IsCellHigherThanFluidContact(fluidContact))
                    return 0;

                if (GetBaseCell(baseHorizon).IsCellHigherThanFluidContact(fluidContact))
                    return MathUtilities.GetRectangularTankVolume(Lateral, Lateral, baseHorizon);

                return MathUtilities.GetRectangularTankVolume(Lateral, Lateral, fluidContact - GetCentralPointDepth());
            }
            catch (Exception e)
            {
                Log.Error("Error while dividing cell", e);
                throw;
            }
        }

        public bool IsCellHigherThanFluidContact(double fluidContact)
        {
            try
            {
                return ((A < fluidContact) && (B < fluidContact) && (C < fluidContact) && (D < fluidContact));
            }
            catch (Exception e)
            {
                Log.Error("Error while dividing cell", e);
                throw;
            }
        }

        private IEnumerable<Cell> DivideCellTo4()
        {
            var output = new List<Cell>();
            var cP = GetCentralPointDepth();

            output.Add(new Cell
            {
                A = A,
                B = (A + B) / 2,
                C = cP,
                D = (A + D) / 2,
                Lateral = Lateral / 2
            });

            output.Add(new Cell
            {
                A = (A + B) / 2,
                B = B,
                C = (B + C) / 2,
                D = cP,
                Lateral = Lateral / 2
            });

            output.Add(new Cell
            {
                A = cP,
                B = (B + C) / 2,
                C = C,
                D = (C + D) / 2,
                Lateral = Lateral / 2
            });

            output.Add(new Cell
            {
                A = (A + D) / 2,
                B = cP,
                C = (C + D) / 2,
                D = D,
                Lateral = Lateral / 2
            });

            return output;
        }

        private double GetCentralPointDepth()
        {
            return MathUtilities.GetCentralPoint(A, B, C, D);
        }

        private Cell GetBaseCell(double baseHorizon)
        {
            return new Cell
            {
                A = A + baseHorizon,
                B = B + baseHorizon,
                C = C + baseHorizon,
                D = D + baseHorizon,
                Lateral = Lateral
            };
        }


    }
}
