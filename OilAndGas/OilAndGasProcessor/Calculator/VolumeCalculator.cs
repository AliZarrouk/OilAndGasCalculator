using System;
using System.Collections.Generic;
using System.Linq;
using Core.DataAccess;
using Core.Extensions;
using Core.Utilities;
using log4net;
using OilAndGasProcessor.DataModeling;
using OilAndGasProcessor.Enums;

namespace OilAndGasProcessor.Calculator
{

    public interface IVolumeCalculator
    {
        CalculatorResponse CalculateVolume(CalculatorRequest request);

        event EventHandler<ProgressEventArgs> ProgressDone;
    }

    public class VolumeCalculator : IVolumeCalculator
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(VolumeCalculator));

        // http://www.metric-conversions.org/volume/cubic-feet-to-us-oil-barrels.htm
        private readonly double[,] _conversions = { { 1, 0.0283168, 0.1781076 }, { 35.31467, 1, 6.289811 }, { 5.6146, 0.15899, 1 } };

        public event EventHandler<ProgressEventArgs> ProgressDone;

        public CalculatorResponse CalculateVolume(CalculatorRequest request)
        {
            try
            {
                Log.LogMethodArguments("ParseFormData", () => request.Id);

                Guard.Against<NullReferenceException>(request == null, "request cannot be null");
                Guard.Against<NullReferenceException>(request.Input == null, "request's input cannot be null");
                Guard.Against<NullReferenceException>(request.Input.Cells == null, "request's input's cells cannot be null");

                var nbrOfInitalCells = request.Input.Cells.Count();
                OnProgressDone(new ProgressEventArgs(nbrOfInitalCells));

                var result = request.Input.Cells.AsParallel().Sum(
                    initialCell => DivideAndCalculateCell(request.Input.FluidContact, request.Input.Precision, initialCell, request.Input.BaseHorizon)
                    );

                var response = new CalculatorResponse
                {
                    Result = CreateResultDictionary(request.Input.BaseUnit, result)
                };

                return response;
            }
            catch (Exception exception)
            {
                Log.Error("Exception while calculating volume", exception);
                return new CalculatorResponse
                {
                    Result = null,
                    Errors = new List<BaseError> { new BaseError { ErrorException = exception } }
                };
            }
        }

        private double DivideAndCalculateCell(double fluidContact, int precision, Cell initialCell, double baseHorizon)
        {
            Log.LogMethodArguments("DivideAndCalculateCell", () => fluidContact, () => precision, () => baseHorizon);

            var res = initialCell.DivideCell(precision)
                .Where(x => x != null)
                .AsParallel()
                .Sum(x => x.GetVolumeFromCellToFluidContact(fluidContact, baseHorizon));

            OnProgressDone(new ProgressEventArgs(0));
            return res;
        }

        private void OnProgressDone(ProgressEventArgs e)
        {
            EventHandler<ProgressEventArgs> handler = ProgressDone;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Dictionary<Unit, double> CreateResultDictionary(Unit baseUnit, double valueInBaseUnit)
        {
            Log.LogMethodArguments("CreateResultDictionary", baseUnit.ToString, () => valueInBaseUnit);
            return Enum.GetValues(typeof(Unit)).Cast<Unit>().ToDictionary(unit => unit, unit => _conversions[(int)baseUnit, (int)unit] * valueInBaseUnit);
        }
    }
}
