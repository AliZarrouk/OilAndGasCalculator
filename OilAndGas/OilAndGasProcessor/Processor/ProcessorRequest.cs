using Core.DataAccess;
using OilAndGasProcessor.Parser;

namespace OilAndGasProcessor.Processor
{
    public class ProcessorRequest : CoreRequest<FormOutput>
    {

        public ParserRequest GetParserRequest()
        {
            return new ParserRequest
            {
                Input = new FormOutput
                {
                    BaseHorizonText = Input.BaseHorizonText,
                    FluidContactText = Input.FluidContactText,
                    LateralText = Input.LateralText,
                    PrecisionText = Input.PrecisionText,
                    TopHorizonDepthValuesText = Input.TopHorizonDepthValuesText
                }
            };
        }
    }
}
