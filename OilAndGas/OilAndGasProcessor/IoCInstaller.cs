using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OilAndGasProcessor.Calculator;
using OilAndGasProcessor.Parser;
using OilAndGasProcessor.Processor;

namespace OilAndGasProcessor
{
    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataParser>().ImplementedBy<DataParser>()
                );

            container.Register(
                Component.For<IVolumeCalculator>().ImplementedBy<VolumeCalculator>()
                );

            container.Register(
                Component.For<IDataProcessor>().ImplementedBy<DataProcessor>()
                );
        }
    }
}
