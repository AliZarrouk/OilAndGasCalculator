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
                Guard.Against<NullReferenceException>(String.IsNullOrWhiteSpace(request.Input.BaseHorizonText), "request's input's BaseHorizonText cannot be null");
                Guard.Against<NullReferenceException>(String.IsNullOrWhiteSpace(request.Input.LateralText), "request's input's LateralText cannot be null");
                Guard.Against<NullReferenceException>(String.IsNullOrWhiteSpace(request.Input.PrecisionText), "request's input's PrecisionText cannot be null");
                Guard.Against<NullReferenceException>(String.IsNullOrWhiteSpace(request.Input.TopHorizonDepthValuesText), "request's input's TopHorizonDepthValuesText cannot be null");
                Guard.Against<NullReferenceException>(String.IsNullOrWhiteSpace(request.Input.FluidContactText), "request's input's FluidContactText cannot be null");

                var response = new ParserResponse
                {
                    Result = new ParserOutput(),
                    Errors = new List<BaseError>()
                };

                var lateral = TryParsingDouble(request.Input.LateralText, response.Errors as List<BaseError>, "Lateral", Resources.LateralNotParsableErrorCode, Resources.LateralUnderZeroErrorCode).GetValueOrDefault(-1);

                response.Result.BaseHorizon = TryParsingDouble(request.Input.BaseHorizonText, response.Errors as List<BaseError>, "Base Horizon", Resources.BHNotParsableErrorCode, Resources.BHUnderZeroErrorCode).GetValueOrDefault(-1);

                response.Result.FluidContact = TryParsingDouble(request.Input.FluidContactText, response.Errors as List<BaseError>, "Fluid Contact", Resources.FCNotParsableErrorCode, Resources.FCUnderZeroErrorCode).GetValueOrDefault(-1);

                response.Result.Precision = TryParsingInt(request.Input.PrecisionText, response.Errors as List<BaseError>, "Precision", Resources.PrecisionNotParsableErrorCode, Resources.PrecisionUnderZeroErrorCode).GetValueOrDefault(-1);

                response.Result.Cells = TryParsingDepthValues(request, response.Errors as List<BaseError>, lateral);

                if (response.Errors.Any())
                {
                    response.Result = null;
                }
                else
                {
                    response.Errors = null;
                }

                Log.Info("Exiting ParseFormData");
                return response;
            }
            catch (Exception exception)
            {
                Log.Error("Exception while parsing form output", exception);
                return new ParserResponse
                {
                    Result = null,
                    Errors = new List<BaseError> { 
                        new BaseError
                        {
                            ErrorException = exception
                        }
                    }
                };
            }
        }

        private IEnumerable<Cell> TryParsingDepthValues(ParserRequest request, List<BaseError> errors, double lateral)
        {
            int nbr;

            var textLines = request.Input.TopHorizonDepthValuesText.Split("\r\n".ToCharArray()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            if (textLines.Length < 2)
                errors.Add(new BaseError
                {
                    ErrorCode = Resources.MinNumberOfLinesErrorCode,
                    ErrorMessage = Resources.MinimumNumberOfLines2
                });

            var firstLineWithNonParsableInt =
                 textLines.FirstOrDefault(
                     l => l.Split(" ".ToCharArray()).Where(x => !String.IsNullOrWhiteSpace(x)).Any(x => (!int.TryParse(x, out nbr)) || (nbr < 0)));

            if (firstLineWithNonParsableInt != null)
            {
                errors.Add(new BaseError
                {
                    ErrorCode = Resources.SomeLinesHaveNonParsableToIntStringOrNegativeNumbersErrorCode,
                    ErrorMessage = Resources.SomeLinesHaveNonParsableToIntStringOrNegativeNumbers
                });
                return null;
            }

            if (errors.Any())
                return null;

            var distinctNumberOfColumns =
                textLines.ToList().Select(x => x.Split(" ".ToCharArray()).Count(y => !String.IsNullOrWhiteSpace(y))).Distinct().ToList();

            if (distinctNumberOfColumns.Count() > 1)
            {
                errors.Add(new BaseError
                {
                    ErrorCode = Resources.DepthValuesNotOfTheSameNumberErrorCode,
                    ErrorMessage = Resources.DepthValuesNotOfTheSameNumberErrorText
                });
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

        private double? TryParsingDouble(string input, List<BaseError> errors, string valueName, string errorCodeUnparsable, string errorCodeUnderZero)
        {
            double output;

            if (!double.TryParse(input, out output))
            {
                errors.Add(new BaseError
                {
                    ErrorCode = errorCodeUnparsable,
                    ErrorMessage = string.Format("{0} {1}", valueName, Resources.NotParsableDouble)
                });
                return null;
            }

            if (output >= 0)
                return output;

            errors.Add(new BaseError
            {
                ErrorCode = errorCodeUnderZero,
                ErrorMessage = string.Format("{0} {1}", valueName, Resources.NotUnderZero)
            });

            return null;
        }

        private int? TryParsingInt(string input, List<BaseError> errors, string valueName, string notParsableErrorCode, string notUnderZeroErrorCode)
        {
            int output;

            if (!int.TryParse(input, out output))
            {
                errors.Add(new BaseError
                {
                    ErrorCode = notParsableErrorCode,
                    ErrorMessage = string.Format("{0} {1}", valueName, Resources.NotParsableInt)
                });
                return null;
            }

            if (output >= 0)
                return output;

            errors.Add(
                new BaseError
                {
                    ErrorCode = notUnderZeroErrorCode,
                    ErrorMessage = string.Format("{0} {1}", valueName, Resources.NotUnderZero)
                }
                );

            return null;
        }
    }
}
