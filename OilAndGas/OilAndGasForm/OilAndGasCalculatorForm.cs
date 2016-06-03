using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Utilities;
using OilAndGasProcessor.Calculator;
using OilAndGasProcessor.Enums;
using OilAndGasProcessor.Processor;

namespace OilAndGasForm
{
    public partial class OilAndGasCalculatorForm : Form
    {
        public OilAndGasCalculatorForm()
        {
            InitializeComponent();
        }

        private void SetDefaultValuesButton_Click(object sender, EventArgs e)
        {

        }

        private async void ProcessButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dataProcessor = IoC.Container.Resolve<IDataProcessor>();

                if (dataProcessor == null)
                {
                    throw new Exception("Retrieveing instance of IDataProcessor failed");
                }

                var processorRequest = new ProcessorRequest
                {
                    Input = new FormOutput
                    {
                        BaseHorizonText = BaseHorizonTextBox.Text,
                        FluidContactText = FluidConactTextBox.Text,
                        LateralText = LateralTextBox.Text,
                        PrecisionText = PrecisionTextBox.Text,
                        TopHorizonDepthValuesText = DepthValuesTextBox.Text
                    }
                };

                Ticks = 0;
                ElapsedTimeLabel.Text = Ticks.ToString(CultureInfo.InvariantCulture);
                Timer1.Start();

                var t = Task.Run(() =>
                    {
                        dataProcessor.ProgressDone += OnProgressDone;

                        var response = dataProcessor.ProcessData(processorRequest);

                        if (response.Result == null)
                        {
                            MessageBox.Show(String.Join(",", response.Errors.Select(x => x.ErrorException.Message)), "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                        else
                        {
                            ResultTextBox.Text = response.Result[Unit.CubicMeter];
                        }
                    });

                await t;

                Timer1.Stop();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnProgressDone(object sender, ProgressEventArgs args)
        {
            if (ProgressBar.InvokeRequired)
            {
                var d = new SetProgressCallBack(OnProgressDone);
                Invoke(d, new[] { sender, args });
            }
            else
            {
                if (args.Total > 0)
                {
                    ProgressBar.Maximum = args.Total;
                    ProgressBar.Value = 0;
                }
                else
                    ProgressBar.Increment(1);
            }

        }

        private void InformationButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("bla", "bla",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Ticks++;
            ElapsedTimeLabel.Text = Ticks.ToString(CultureInfo.InvariantCulture);
        }
    }
}
