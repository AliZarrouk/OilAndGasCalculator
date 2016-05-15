using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess;
using Core.Extensions;
using Core.Utilities;
using log4net;
using OilAndGasProcessor.DataModeling;

namespace OilAndGasProcessor.Parser
{

    public interface IDataParser
    {
        ParserResponse ParseFormData(ParserRequest request);
    }

    public class DataParser : IDataParser
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataParser));

        public ParserResponse ParseFormData(ParserRequest request)
        {
            try
            {
                Log.LogMethodArguments("ParseFormData", () => request.Id);

                Guard.Against<NullReferenceException>(request == null, "request cannot be null");
                Guard.Against<NullReferenceException>(request.Input == null, "request's input cannot be null");

                var response = new ParserResponse
                {
                    Result = new ParserOutput(),
                    Errors = new List<BaseError>()
                };

                var lateral = TryParsingDouble(request.Input.LateralText, response.Errors, "Lateral", Resources.LateralNotParsableErrorCode).GetValueOrDefault(-1);

                response.Result.BaseHorizon = TryParsingDouble(request.Input.BaseHorizonText, response.Errors, "Base Horizon",).GetValueOrDefault(-1);

                response.Result.FluidContact = TryParsingDouble(request.Input.FluidContactText, response.Errors, "Fluid Contact").GetValueOrDefault(-1);

                response.Result.Precision = TryParsingInt(request.Input.PrecisionText, response.Errors, "Precision").GetValueOrDefault(-1);

                response.Result.Cells = TryParsingDepthValues(request, response.Errors, lateral);

                if (errors.Length > 0)
                {
                    response.Result = null;
                }

                Log.Info("Exiting ParseFormData");
                return response;
            }
            catch (Exception exception)
            {
                Log.Error("Exception while parsing form output", exception);
                return null;
            }
        }

        private IEnumerable<Cell> TryParsingDepthValues(ParserRequest request, StringBuilder errors, double lateral)
        {
            int nbr;

            var textLines = request.Input.TopHorizonDepthValuesText.Split("\r\n".ToCharArray()).Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

            if (textLines.Length < 2)
                errors.AppendLine(Resources.MinimumNumberOfLines2);

            var nbrOfElements = textLines.Select(x => x.Split(" ".ToCharArray()).Count()).Distinct();

            if (nbrOfElements.Count() > 1)
                errors.AppendLine(Resources.DepthValuesNotOfTheSameNumberErrorText);

            var firstLineWithNonParsableInt =
                textLines.FirstOrDefault(
                    l => l.Split(" ".ToCharArray()).Where(x => !String.IsNullOrWhiteSpace(x)).Any(x => (!int.TryParse(x, out nbr)) || (nbr < 0)));

            if (firstLineWithNonParsableInt != null)
            {
                errors.AppendLine(Resources.SomeLinesHaveNonParsableToIntStringOrNegativeNumbers);
                return null;
            }

            if (errors.Length > 0)
                return null;

            var distinctNumberOfColumns =
                textLines.ToList().Select(x => x.Split(" ".ToCharArray()).Count(y => !String.IsNullOrWhiteSpace(y))).Distinct().ToList();

            if (distinctNumberOfColumns.Count() > 1)
            {
                errors.AppendLine(Resources.DepthValuesNotOfTheSameNumberErrorText);
                return null;
            }

            var numberOfColumns = distinctNumberOfColumns.First();

            var matrix = new int[numberOfColumns, textLines.Length];

            var output = new List<Cell>();

            for (var i = 0; i < textLines.Length; i++)
            {
                var numbers = textLines[i].Split(" ".ToCharArray());

                for (var j = 0; j < numbers.Length; j++)
                {
                    int parsedInt;
                    if (int.TryParse(numbers[j], out parsedInt))
                        matrix[j, i] = parsedInt;
                }
            }

            for (var i = 0; i < (numberOfColumns - 1) * (textLines.Length - 1); i++)
            {
                var line = i / (textLines.Length - 1);
                var column = i % (textLines.Length - 1);

                output.Add(
                    new Cell
                        {
                            A = matrix[line, column],
                            B = matrix[line, column + 1],
                            C = matrix[line + 1, column + 1],
                            D = matrix[line + 1, column],
                            Lateral = lateral
                        }
                );
            }

            return output;
        }

        private double? TryParsingDouble(string input, List<BaseError> errors, string valueName, string errorCode)
        {
            double output;

            if (!double.TryParse(input, out output))
            {
                errors.Add(new BaseError
                {
                    ErrorCode = int.Parse(errorCode),
                    ErrorMessage = String.Format("{0} {1}", valueName, Resources.NotParsableDouble)
                });
                return null;
            }

            if (output >= 0)
                return output;

            errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotUnderZero));
            return null;
        }

        private int? TryParsingInt(string input, List<BaseError> errors, string valueName, string errorCode)
        {
            int output;

            if (!int.TryParse(input, out output))
            {
                errors.Add(new BaseError
                {
                    ErrorCode = int.Parse(errorCode),
                    ErrorMessage = String.Format("{0} {1}", valueName, Resources.NotParsableInt)
                });
                return null;
            }

            if (output >= 0)
                return output;

            errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotUnderZero));
            return null;
        }
    }
}
