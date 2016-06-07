using System;
using System.Collections.Generic;
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
        private ProcessorResponse _result;

        public OilAndGasCalculatorForm()
        {
            InitializeComponent();
        }

        private void SetDefaultValuesButton_Click(object sender, EventArgs e)
        {
            DepthValuesTextBox.Text = System.IO.File.ReadAllText(Resources.DefaultDepthValuesFilePath);
            FluidConactTextBox.Text = Resources.FluidContactDefaultValue;
            LateralTextBox.Text = Resources.LateralDefaultValue;
            BaseHorizonTextBox.Text = Resources.BaseHorizonDefaultValue;
            PrecisionTextBox.Text = 2.ToString(CultureInfo.InvariantCulture);
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

                ResultTextBox.Text = Resources.CalculatingText;
                Ticks = 0;
                ElapsedTimeLabel.Text = Ticks.ToString(CultureInfo.InvariantCulture);
                Timer1.Start();

                var t = Task.Run(() =>
                    {
                        dataProcessor.ProgressDone += OnProgressDone;

                        _result = dataProcessor.ProcessData(processorRequest);
                    });

                await t;

                Timer1.Stop();

                if (_result.Result == null)
                {
                    MessageBox.Show(String.Join(",", _result.Errors.Select(x => x.ErrorException.Message)),
                        Resources.ProcessorResponseErrorHeader,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    ShowResult();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resources.ProcessorResponseErrorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            Ticks++;
            ElapsedTimeLabel.Text = Ticks.ToString(CultureInfo.InvariantCulture);
        }

        private void ShowResult()
        {
            if (_result.Result == null) return;

            var checkedRadio = String.Concat(CubicFeetRadioButton.Checked ? "1" : "0",
                BarrelRadioButton.Checked ? "1" : "0", CubicMeterRadioButton.Checked ? "1" : "0");

            switch (checkedRadio)
            {
                case "100":
                    ResultTextBox.Text = _result.Result[Unit.CubicFeet];
                    break;
                case "001":
                    ResultTextBox.Text = _result.Result[Unit.CubicMeter];
                    break;
                case "010":
                    ResultTextBox.Text = _result.Result[Unit.Barrel];
                    break;
            }
        }

        private void CubicFeetRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void BarrelRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowResult();
        }

        private void CubicMeterRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            ShowResult();
        }
    }
}
