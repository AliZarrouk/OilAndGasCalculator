using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    Result = new ParserOutput()
                };

                var errors = new StringBuilder();

                var lateral = TryParsingDouble(request.Input.LateralText, errors, "Lateral").GetValueOrDefault(-1);

                response.Result.BaseHorizon = TryParsingDouble(request.Input.BaseHorizonText, errors, "Base Horizon").GetValueOrDefault(-1);

                response.Result.FluidContact = TryParsingDouble(request.Input.FluidContactText, errors, "Fluid Contact").GetValueOrDefault(-1);

                response.Result.Precision = TryParsingInt(request.Input.PrecisionText, errors, "Precision").GetValueOrDefault(-1);

                TryParsingDepthValues(request, errors, lateral, response.Result.Cells);

                if (errors.Length == 0)
                {
                    response.Errors = errors.ToString().Split("\n".ToCharArray());
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

        private void TryParsingDepthValues(ParserRequest request, StringBuilder errors, double lateral, IEnumerable<Cell> output)
        {
            int nbr;

            var textLines = request.Input.TopHorizonDepthValuesText.Split("\n".ToCharArray());

            if (textLines.Length < 2)
                errors.AppendLine(Resources.MinimumNumberOfLines2);

            var nbrOfElements = textLines.Select(x => x.Split(" ".ToCharArray()).Count()).Distinct();

            if (nbrOfElements.Count() > 1)
                errors.AppendLine(Resources.DepthValuesNotOfTheSameNumberErrorText);

            var firstLineWithNonParsableInt =
                textLines.FirstOrDefault(
                    l => l.Split(" ".ToCharArray()).ToList().Any(x => (!int.TryParse(x, out nbr)) || (nbr < 0)));

            if (firstLineWithNonParsableInt != null)
            {
                errors.AppendLine(Resources.SomeLinesHaveNonParsableToIntStringOrNegativeNumbers);
                return;
            }

            if (errors.Length <= 0)
                return;

            var distinctNumberOfColumns =
                textLines.ToList().Select(x => x.Split(" ".ToCharArray()).Length).Distinct().ToList();
            var numberOfColumns = distinctNumberOfColumns.First();

            var matrix = new int[numberOfColumns, textLines.Length];

            output = new List<Cell>();

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

                (output as List<Cell>).Add(
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
        }

        private double? TryParsingDouble(string input, StringBuilder errors, string valueName)
        {
            double output;

            if (!double.TryParse(input, out output))
            {
                errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotParsableDouble));
                return null;
            }

            if (output >= 0)
                return output;

            errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotUnderZero));
            return null;
        }

        private int? TryParsingInt(string input, StringBuilder errors, string valueName)
        {
            int output;

            if (!int.TryParse(input, out output))
            {
                errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotParsableInt));
                return null;
            }

            if (output >= 0)
                return output;

            errors.AppendLine(String.Format("{0} {1}", valueName, Resources.NotUnderZero));
            return null;
        }
    }
}
