using System;
using System.Windows.Forms;
using Core.Utilities;
using OilAndGasProcessor;

namespace OilAndGasForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IoC.Container.Install(new IoCInstaller());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OilAndGasCalculatorForm());
        }
    }
}
