using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OilAndGasProcessorTests
{
    [TestClass]
    public class ParserTest
    {

        [TestMethod]
        public void LateralUnparsable()
        {
            var lateral = "unparsable";

        }

        [TestMethod]
        public void LateralUnderZero()
        {
            var lateral = "-2";
        }

        [TestMethod]
        public void BaseHorizonUnparsable()
        {
            var bh = "unparsable";
        }

        [TestMethod]
        public void BaseHorizonUnderZero()
        {
            var bh = "-2";
        }

        [TestMethod]
        public void FluidConctactUnparsable()
        {
            var fc = "unparsable";
        }

        [TestMethod]
        public void FluidConctactUnderZero()
        {
            var fc = "-2";
        }

        [TestMethod]
        public void PrecisionUnparsable()
        {
            var precision = "unparsable";
        }

        [TestMethod]
        public void PrecisionUnderZero()
        {
            var precision = "-2";
        }

        [TestMethod]
        public void DepthValuesContainUnparsableInt()
        {
            var depthValues = new StringBuilder();
            depthValues.Append("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 unparsable");

        }

        [TestMethod]
        public void DepthValuesLinesNotAllSameLength()
        {
            var depthValues = new StringBuilder();
            depthValues.Append("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 7");
            depthValues.AppendLine("1 4 5 6 7 ");
        }
    }
}
