using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.DataAccess;
using Core.Utilities;
using log4net;
using OilAndGasProcessor.Calculator;
using OilAndGasProcessor.Enums;
using OilAndGasProcessor.Parser;

namespace OilAndGasProcessor.Processor
{
    public interface IDataProcessor
    {
        ProcessorResponse ProcessData(ProcessorRequest request);
        event EventHandler<ProgressEventArgs> ProgressDone;
    }

    public class DataProcessor : IDataProcessor
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataProcessor));
        public event EventHandler<ProgressEventArgs> ProgressDone;

        private IDataParser parser;
        private IVolumeCalculator calculator;

        public DataProcessor(IDataParser parser, IVolumeCalculator calculator)
        {
            this.parser = parser;
            this.calculator = calculator;
        }

        public ProcessorResponse ProcessData(ProcessorRequest request)
        {
            try
            {
                Guard.Against<NullReferenceException>(request == null, "request cannot be null");
                Guard.Against<NullReferenceException>(request.Input == null, "request's input cannot be null");

                var dataParserResponse = parser.ParseFormData(request.GetParserRequest());

                if (dataParserResponse == null)
                {
                    throw new Exception("IDataParser response is null");
                }

                if (dataParserResponse.Result == null)
                {
                    return new ProcessorResponse
                    {
                        Result = null,
                        Errors = dataParserResponse.Errors
                    };
                }

                calculator.ProgressDone += OnProgressDone;

                var calculatorResponse = calculator.CalculateVolume(GetCalculatorRequest(dataParserResponse, Unit.CubicMeter));

                if (calculatorResponse == null)
                {
                    throw new Exception("IVolumeCalculator response is null");
                }

                if (calculatorResponse.Result == null)
                {
                    return new ProcessorResponse
                    {
                        Result = null,
                        Errors = calculatorResponse.Errors
                    };
                }

                return new ProcessorResponse
                {
                    Errors = null,
                    Result = calculatorResponse.Result.ToDictionary(item => item.Key, item => item.Value.ToString(CultureInfo.InvariantCulture))
                };
            }
            catch (Exception e)
            {
                Log.Error("Exception while processing the data", e);
                return new ProcessorResponse
                {
                    Result = null,
                    Errors = new List<BaseError> { new BaseError { ErrorException = e } }
                };
            }
        }

        private CalculatorRequest GetCalculatorRequest(ParserResponse parserResponse, Unit baseUnit)
        {
            return new CalculatorRequest
            {
                Input = new CalculatorInput
                {
                    BaseHorizon = parserResponse.Result.BaseHorizon,
                    Cells = parserResponse.Result.Cells,
                    FluidContact = parserResponse.Result.FluidContact,
                    Precision = parserResponse.Result.Precision,
                    BaseUnit = baseUnit
                }
            };
        }

        private void OnProgressDone(object sender, ProgressEventArgs e)
        {
            EventHandler<ProgressEventArgs> handler = ProgressDone;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
