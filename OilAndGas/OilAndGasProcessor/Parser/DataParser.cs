using System;
using System.Linq;
using System.Text;
using Core.Extensions;
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

                double lateral = -1;

                var response = new ParserResponse
                {
                    Result = new ParserOutput()
                };

                var errors = new StringBuilder();

                int nbr;

                double dbl;

                if (!double.TryParse(request.Input.LateralText, out dbl))
                {
                    errors.AppendLine(Resources.LateralNotParsable);
                }
                else if (dbl <= 0)
                {
                    errors.AppendLine(String.Format("Lateral {0}", Resources.NotUnderZero));
                }
                else
                {
                    lateral = dbl;
                }

                if (!double.TryParse(request.Input.BaseHorizonText, out dbl))
                {
                    errors.AppendLine(Resources.BHNotParsable);
                }
                else if (dbl < 0)
                {
                    errors.AppendLine(String.Format("Base Horizon {0}", Resources.NotUnderZero));
                }
                else
                {
                    response.Result.BaseHorizon = dbl;
                }

                if (!double.TryParse(request.Input.FluidContactText, out dbl))
                {
                    errors.AppendLine(Resources.FCNotParsable);
                }
                else if (dbl < 0)
                {
                    errors.AppendLine(String.Format("Fluid Contact {0}", Resources.NotUnderZero));
                }
                else
                {
                    response.Result.FluidContact = dbl;
                }

                if (!int.TryParse(request.Input.PrecisionText, out nbr))
                {
                    errors.AppendLine(Resources.PrecisionNotParsable);
                }
                else if (nbr < 0)
                {
                    errors.AppendLine(String.Format("Precision {0}", Resources.NotUnderZero));
                }
                else
                {
                    response.Result.Precision = nbr;
                }

                var textLines = request.Input.TopHorizonDepthValuesText.Split("\n".ToCharArray());

                if (textLines.Length < 2)
                    errors.AppendLine(Resources.MinimumNumberOfLines2);

                var nbrOfElements = textLines.Select(x => x.Split(" ".ToCharArray()).Count()).Distinct();

                if (nbrOfElements.Count() > 1)
                    errors.AppendLine(Resources.DepthValuesNotOfTheSameNumberErrorText);

                var firstLineWithNonParsableInt =
                    textLines.FirstOrDefault(l => l.Split(" ".ToCharArray()).ToList().Any(x => (!int.TryParse(x, out nbr)) || (nbr < 0)));

                if (firstLineWithNonParsableInt != null)
                    errors.AppendLine(Resources.SomeLinesHaveNonParsableToIntStringOrNegativeNumbers);

                if (errors.Length > 0)
                {
                    var distinctNumberOfColumns = textLines.ToList().Select(x => x.Split(" ".ToCharArray()).Length).Distinct().ToList();
                    var numberOfColumns = distinctNumberOfColumns.First();

                    var matrix = new int[numberOfColumns, textLines.Length];
                    var output = new Cell[(numberOfColumns - 1) * (textLines.Length - 1)];

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

                        output[i] = new Cell
                        {
                            A = matrix[line, column],
                            B = matrix[line, column + 1],
                            C = matrix[line + 1, column + 1],
                            D = matrix[line + 1, column],
                            Lateral = lateral
                        };
                    }
                }

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
    }
}
