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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "201"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "202"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "203"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "204"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "205"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "206"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "208"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "207"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "210"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "211"));
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
            Assert.IsTrue(response.Errors.Any(x => x.ErrorCode == "209"));
        }
    }
}
