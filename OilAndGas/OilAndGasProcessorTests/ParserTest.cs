using System;
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
        public void OneOfTheRequestValuesIsNull()
        {
            var response = new DataParser().ParseFormData(null);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.IsNotNull(response.Errors);
            Assert.AreEqual(response.Errors.First().ErrorException.Message, "request cannot be null");
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

        [TestMethod]
        public void ParsingCorrectValuesWorks()
        {
            var request = CreateBasicParserRequest();

            var response = new DataParser().ParseFormData(request);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.IsNull(response.Errors);
            Assert.AreEqual(response.Result.BaseHorizon, 100);
            Assert.AreEqual(response.Result.FluidContact, 100);
            Assert.AreEqual(response.Result.Precision, 2);
            Assert.IsNotNull(response.Result.Cells);
            Assert.AreEqual(response.Result.Cells.Count(), 20);
            Assert.AreEqual(response.Result.Cells.First().Lateral, 10);

            const double tolerance = 0.1;

            Assert.IsTrue(response.Result.Cells.Count(x => (Math.Abs(x.A - 1) < tolerance) && (Math.Abs(x.B - 1) < tolerance) && (Math.Abs(x.C - 4) < tolerance) && (Math.Abs(x.D - 4) < tolerance)) == 4);
            Assert.IsTrue(response.Result.Cells.Count(x => (Math.Abs(x.C - 5) < tolerance) && (Math.Abs(x.C - 5) < tolerance) && (Math.Abs(x.A - 4) < tolerance) && (Math.Abs(x.B - 4) < tolerance)) == 4);
            Assert.IsTrue(response.Result.Cells.Count(x => (Math.Abs(x.A - 5) < tolerance) && (Math.Abs(x.B - 5) < tolerance) && (Math.Abs(x.C - 6) < tolerance) && (Math.Abs(x.D - 6) < tolerance)) == 4);
            Assert.IsTrue(response.Result.Cells.Count(x => (Math.Abs(x.A - 6) < tolerance) && (Math.Abs(x.B - 6) < tolerance) && (Math.Abs(x.C - 7) < tolerance) && (Math.Abs(x.D - 7) < tolerance)) == 4);
            Assert.IsTrue(response.Result.Cells.Count(x => (Math.Abs(x.A - 7) < tolerance) && (Math.Abs(x.B - 7) < tolerance) && (Math.Abs(x.C - 7) < tolerance) && (Math.Abs(x.D - 7) < tolerance)) == 4);
        }
    }
}
