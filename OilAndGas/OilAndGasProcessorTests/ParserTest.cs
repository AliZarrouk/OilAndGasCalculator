using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OilAndGasProcessor.Parser;
using OilAndGasProcessor.Processor;

namespace OilAndGasProcessorTests
{
    [TestClass]
    public class ParserTest
    {

        private ParserRequest CreateBasicParserRequest()
        {
            var depthValues = new StringBuilder();
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");

            return new ParserRequest
            {
                Input = new FormOutput
                {
                    BaseHorizonText = "100",
                    FluidContactText = "100",
                    LateralText = "10",
                    PrecisionText = "2",
                    TopHorizonDepthValuesText = depthValues.ToString()
                }
            };
        }

        [TestMethod]
        public void LateralUnparsable()
        {
            var request = CreateBasicParserRequest();

            request.Input.LateralText = "unparsable";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Lateral is not a parsable double")));
        }

        [TestMethod]
        public void LateralUnderZero()
        {
            var request = CreateBasicParserRequest();

            request.Input.LateralText = "-2";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Lateral not Under 0")));
        }

        [TestMethod]
        public void BaseHorizonUnparsable()
        {
            var request = CreateBasicParserRequest();

            request.Input.BaseHorizonText = "unparsable";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Base Horizon is not a parsable double")));
        }

        [TestMethod]
        public void BaseHorizonUnderZero()
        {
            var request = CreateBasicParserRequest();

            request.Input.BaseHorizonText = "-2";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Base Horizon not Under 0")));
        }

        [TestMethod]
        public void FluidConctactUnparsable()
        {
            var request = CreateBasicParserRequest();

            request.Input.FluidContactText = "unparsable";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Fluid Contact is not a parsable double")));
        }

        [TestMethod]
        public void FluidConctactUnderZero()
        {
            var request = CreateBasicParserRequest();

            request.Input.FluidContactText = "-2";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Fluid Contact not Under 0")));
        }

        [TestMethod]
        public void PrecisionUnparsable()
        {
            var request = CreateBasicParserRequest();

            request.Input.PrecisionText = "unparsable";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Precision is not a parsable int")));
        }

        [TestMethod]
        public void PrecisionUnderZero()
        {
            var request = CreateBasicParserRequest();

            request.Input.PrecisionText = "-2";

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Precision not Under 0")));
        }

        [TestMethod]
        public void DepthValuesContainUnparsableInt()
        {
            var request = CreateBasicParserRequest();

            var depthValues = new StringBuilder();
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 unparsable");

            request.Input.TopHorizonDepthValuesText = depthValues.ToString();

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Some of the lines have non parsable to int strings or negative numbers")));
        }

        [TestMethod]
        public void DepthValuesLinesNotAllSameLength()
        {
            var request = CreateBasicParserRequest();

            var depthValues = new StringBuilder();
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 ");

            request.Input.TopHorizonDepthValuesText = depthValues.ToString();

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Depth values not of the same number in each line")));
        }

        [TestMethod]
        public void DepthValuesLinesMinimumTwo()
        {
            var request = CreateBasicParserRequest();

            var depthValues = new StringBuilder();
            depthValues.AppendLine("1 4 5 6 7 7");

            request.Input.TopHorizonDepthValuesText = depthValues.ToString();

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.ToList().Count, 1);
            Assert.IsTrue(response.Errors.Any(x => x.Contains("Minimum number of lines 2")));
        }
    }
}
