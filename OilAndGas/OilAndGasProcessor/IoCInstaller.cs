﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using OilAndGasProcessor.Parser;

namespace OilAndGasProcessor
{
    public class IoCInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IDataParser>().ImplementedBy<DataParser>()
                );

            
        }
    }
}
