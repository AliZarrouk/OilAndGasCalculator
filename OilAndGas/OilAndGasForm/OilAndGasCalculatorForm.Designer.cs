using OilAndGasProcessor.Calculator;

namespace OilAndGasForm
{
    partial class OilAndGasCalculatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DepthValuesTextBox = new System.Windows.Forms.TextBox();
            this.DepthValuesLabel = new System.Windows.Forms.Label();
            this.FluidContactLabel = new System.Windows.Forms.Label();
            this.LateralLabel = new System.Windows.Forms.Label();
            this.BaseHorizonLabel = new System.Windows.Forms.Label();
            this.PrecisionLabel = new System.Windows.Forms.Label();
            this.FluidConactTextBox = new System.Windows.Forms.TextBox();
            this.LateralTextBox = new System.Windows.Forms.TextBox();
            this.BaseHorizonTextBox = new System.Windows.Forms.TextBox();
            this.SetDefaultValuesButton = new System.Windows.Forms.Button();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.InformationButton = new System.Windows.Forms.Button();
            this.CubicMeterRadioButton = new System.Windows.Forms.RadioButton();
            this.CubicFeetRadioButton = new System.Windows.Forms.RadioButton();
            this.BarrelRadioButton = new System.Windows.Forms.RadioButton();
            this.PrecisionTextBox = new System.Windows.Forms.TextBox();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ElapsedTimeLabel = new System.Windows.Forms.Label();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // DepthValuesTextBox
            // 
            this.DepthValuesTextBox.Location = new System.Drawing.Point(12, 54);
            this.DepthValuesTextBox.Multiline = true;
            this.DepthValuesTextBox.Name = "DepthValuesTextBox";
            this.DepthValuesTextBox.Size = new System.Drawing.Size(898, 299);
            this.DepthValuesTextBox.TabIndex = 0;
            // 
            // DepthValuesLabel
            // 
            this.DepthValuesLabel.AutoSize = true;
            this.DepthValuesLabel.Location = new System.Drawing.Point(12, 20);
            this.DepthValuesLabel.Name = "DepthValuesLabel";
            this.DepthValuesLabel.Size = new System.Drawing.Size(182, 17);
            this.DepthValuesLabel.TabIndex = 1;
            this.DepthValuesLabel.Text = "Depth values of top horizon";
            // 
            // FluidContactLabel
            // 
            this.FluidContactLabel.AutoSize = true;
            this.FluidContactLabel.Location = new System.Drawing.Point(20, 382);
            this.FluidContactLabel.Name = "FluidContactLabel";
            this.FluidContactLabel.Size = new System.Drawing.Size(88, 17);
            this.FluidContactLabel.TabIndex = 2;
            this.FluidContactLabel.Text = "Fluid contact";
            // 
            // LateralLabel
            // 
            this.LateralLabel.AutoSize = true;
            this.LateralLabel.Location = new System.Drawing.Point(20, 423);
            this.LateralLabel.Name = "LateralLabel";
            this.LateralLabel.Size = new System.Drawing.Size(52, 17);
            this.LateralLabel.TabIndex = 3;
            this.LateralLabel.Text = "Lateral";
            // 
            // BaseHorizonLabel
            // 
            this.BaseHorizonLabel.AutoSize = true;
            this.BaseHorizonLabel.Location = new System.Drawing.Point(216, 382);
            this.BaseHorizonLabel.Name = "BaseHorizonLabel";
            this.BaseHorizonLabel.Size = new System.Drawing.Size(93, 17);
            this.BaseHorizonLabel.TabIndex = 4;
            this.BaseHorizonLabel.Text = "Base Horizon";
            // 
            // PrecisionLabel
            // 
            this.PrecisionLabel.AutoSize = true;
            this.PrecisionLabel.Location = new System.Drawing.Point(216, 425);
            this.PrecisionLabel.Name = "PrecisionLabel";
            this.PrecisionLabel.Size = new System.Drawing.Size(66, 17);
            this.PrecisionLabel.TabIndex = 5;
            this.PrecisionLabel.Text = "Precision";
            // 
            // FluidConactTextBox
            // 
            this.FluidConactTextBox.Location = new System.Drawing.Point(119, 377);
            this.FluidConactTextBox.Name = "FluidConactTextBox";
            this.FluidConactTextBox.Size = new System.Drawing.Size(75, 22);
            this.FluidConactTextBox.TabIndex = 6;
            // 
            // LateralTextBox
            // 
            this.LateralTextBox.Location = new System.Drawing.Point(119, 420);
            this.LateralTextBox.Name = "LateralTextBox";
            this.LateralTextBox.Size = new System.Drawing.Size(75, 22);
            this.LateralTextBox.TabIndex = 7;
            // 
            // BaseHorizonTextBox
            // 
            this.BaseHorizonTextBox.Location = new System.Drawing.Point(324, 374);
            this.BaseHorizonTextBox.Name = "BaseHorizonTextBox";
            this.BaseHorizonTextBox.Size = new System.Drawing.Size(75, 22);
            this.BaseHorizonTextBox.TabIndex = 8;
            // 
            // SetDefaultValuesButton
            // 
            this.SetDefaultValuesButton.Location = new System.Drawing.Point(15, 458);
            this.SetDefaultValuesButton.Name = "SetDefaultValuesButton";
            this.SetDefaultValuesButton.Size = new System.Drawing.Size(116, 40);
            this.SetDefaultValuesButton.TabIndex = 10;
            this.SetDefaultValuesButton.Text = "Default Values";
            this.SetDefaultValuesButton.UseVisualStyleBackColor = true;
            this.SetDefaultValuesButton.Click += new System.EventHandler(this.SetDefaultValuesButton_Click);
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(434, 409);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(102, 40);
            this.ProcessButton.TabIndex = 11;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // InformationButton
            // 
            this.InformationButton.Location = new System.Drawing.Point(808, 12);
            this.InformationButton.Name = "InformationButton";
            this.InformationButton.Size = new System.Drawing.Size(102, 25);
            this.InformationButton.TabIndex = 12;
            this.InformationButton.Text = "Information";
            this.InformationButton.UseVisualStyleBackColor = true;
            this.InformationButton.Click += new System.EventHandler(this.InformationButton_Click);
            // 
            // CubicMeterRadioButton
            // 
            this.CubicMeterRadioButton.AutoSize = true;
            this.CubicMeterRadioButton.Location = new System.Drawing.Point(562, 375);
            this.CubicMeterRadioButton.Name = "CubicMeterRadioButton";
            this.CubicMeterRadioButton.Size = new System.Drawing.Size(104, 21);
            this.CubicMeterRadioButton.TabIndex = 13;
            this.CubicMeterRadioButton.TabStop = true;
            this.CubicMeterRadioButton.Text = "Cubic Meter";
            this.CubicMeterRadioButton.UseVisualStyleBackColor = true;
            //this.CubicMeterRadioButton.CheckedChanged += new System.EventHandler(this.CubicMeterRadioButton_CheckedChanged);
            // 
            // CubicFeetRadioButton
            // 
            this.CubicFeetRadioButton.AutoSize = true;
            this.CubicFeetRadioButton.Checked = true;
            this.CubicFeetRadioButton.Location = new System.Drawing.Point(796, 374);
            this.CubicFeetRadioButton.Name = "CubicFeetRadioButton";
            this.CubicFeetRadioButton.Size = new System.Drawing.Size(96, 21);
            this.CubicFeetRadioButton.TabIndex = 14;
            this.CubicFeetRadioButton.TabStop = true;
            this.CubicFeetRadioButton.Text = "Cubic Feet";
            this.CubicFeetRadioButton.UseVisualStyleBackColor = true;
            //this.CubicFeetRadioButton.CheckedChanged += new System.EventHandler(this.CubicFeetRadioButton_CheckedChanged);
            // 
            // BarrelRadioButton
            // 
            this.BarrelRadioButton.AutoSize = true;
            this.BarrelRadioButton.Location = new System.Drawing.Point(696, 374);
            this.BarrelRadioButton.Name = "BarrelRadioButton";
            this.BarrelRadioButton.Size = new System.Drawing.Size(67, 21);
            this.BarrelRadioButton.TabIndex = 15;
            this.BarrelRadioButton.TabStop = true;
            this.BarrelRadioButton.Text = "Barrel";
            this.BarrelRadioButton.UseVisualStyleBackColor = true;
            //this.BarrelRadioButton.CheckedChanged += new System.EventHandler(this.BarrelRadioButton_CheckedChanged);
            // 
            // PrecisionTextBox
            // 
            this.PrecisionTextBox.Location = new System.Drawing.Point(324, 423);
            this.PrecisionTextBox.Name = "PrecisionTextBox";
            this.PrecisionTextBox.Size = new System.Drawing.Size(75, 22);
            this.PrecisionTextBox.TabIndex = 17;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1000;
            this.Timer1.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(352, 470);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(47, 17);
            this.TimeLabel.TabIndex = 18;
            this.TimeLabel.Text = "Time :";
            // 
            // ElapsedTimeLabel
            // 
            this.ElapsedTimeLabel.AutoSize = true;
            this.ElapsedTimeLabel.Location = new System.Drawing.Point(418, 470);
            this.ElapsedTimeLabel.Name = "ElapsedTimeLabel";
            this.ElapsedTimeLabel.Size = new System.Drawing.Size(16, 17);
            this.ElapsedTimeLabel.TabIndex = 19;
            this.ElapsedTimeLabel.Text = "0";
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(562, 418);
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(348, 22);
            this.ResultTextBox.TabIndex = 20;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(562, 458);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(348, 23);
            this.ProgressBar.TabIndex = 21;
            // 
            // OilAndGasCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 510);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ElapsedTimeLabel);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.PrecisionTextBox);
            this.Controls.Add(this.BarrelRadioButton);
            this.Controls.Add(this.CubicFeetRadioButton);
            this.Controls.Add(this.CubicMeterRadioButton);
            this.Controls.Add(this.InformationButton);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.SetDefaultValuesButton);
            this.Controls.Add(this.BaseHorizonTextBox);
            this.Controls.Add(this.LateralTextBox);
            this.Controls.Add(this.FluidConactTextBox);
            this.Controls.Add(this.PrecisionLabel);
            this.Controls.Add(this.BaseHorizonLabel);
            this.Controls.Add(this.LateralLabel);
            this.Controls.Add(this.FluidContactLabel);
            this.Controls.Add(this.DepthValuesLabel);
            this.Controls.Add(this.DepthValuesTextBox);
            this.Name = "OilAndGasCalculatorForm";
            this.Text = "Oil and Gas Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DepthValuesTextBox;
        private System.Windows.Forms.Label DepthValuesLabel;
        private System.Windows.Forms.Label FluidContactLabel;
        private System.Windows.Forms.Label LateralLabel;
        private System.Windows.Forms.Label BaseHorizonLabel;
        private System.Windows.Forms.Label PrecisionLabel;
        private System.Windows.Forms.TextBox FluidConactTextBox;
        private System.Windows.Forms.TextBox LateralTextBox;
        private System.Windows.Forms.TextBox BaseHorizonTextBox;
        private System.Windows.Forms.Button SetDefaultValuesButton;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Button InformationButton;
        private System.Windows.Forms.RadioButton CubicMeterRadioButton;
        private System.Windows.Forms.RadioButton CubicFeetRadioButton;
        private System.Windows.Forms.RadioButton BarrelRadioButton;
        private System.Windows.Forms.TextBox PrecisionTextBox;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label ElapsedTimeLabel;
        private System.Windows.Forms.TextBox ResultTextBox;
        private int Ticks;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private delegate void SetProgressCallBack(object sender, ProgressEventArgs args);
    }
}

