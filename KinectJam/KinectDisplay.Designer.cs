namespace KinectJam
{
    partial class KinectDisplay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rtbMessages = new System.Windows.Forms.RichTextBox();
            this.video = new System.Windows.Forms.PictureBox();
            this.DistanceWorkTextBox = new System.Windows.Forms.RichTextBox();
            this.weightLabel = new System.Windows.Forms.Label();
            this.heldWeightTextbox = new System.Windows.Forms.TextBox();
            this.PowerGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.IncreaseAngleButton = new System.Windows.Forms.Button();
            this.DecreaseAngleButton = new System.Windows.Forms.Button();
            this.CurrentAngleTextbox = new System.Windows.Forms.TextBox();
            this.AngleSlider = new System.Windows.Forms.TrackBar();
            this.MinAngleLabel = new System.Windows.Forms.Label();
            this.MaxAngleLabel = new System.Windows.Forms.Label();
            this.DistanceLabel = new System.Windows.Forms.Label();
            this.WorkLabel = new System.Windows.Forms.Label();
            this.PowerLabel = new System.Windows.Forms.Label();
            this.FilteredWorkLabel = new System.Windows.Forms.Label();
            this.FilteredPowerLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PauseButton = new System.Windows.Forms.Button();
            this.ContinueButton = new System.Windows.Forms.Button();
            this.RecordButton = new System.Windows.Forms.Button();
            this.AngleGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.FilteredFrequencyTextBox = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.bodyWeightTextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.armLengthTextbox = new System.Windows.Forms.TextBox();
            this.TestTextBox = new System.Windows.Forms.RichTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.video)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbMessages
            // 
            this.rtbMessages.Location = new System.Drawing.Point(12, 453);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.Size = new System.Drawing.Size(264, 50);
            this.rtbMessages.TabIndex = 0;
            this.rtbMessages.Text = "";
            // 
            // video
            // 
            this.video.Location = new System.Drawing.Point(12, 12);
            this.video.Name = "video";
            this.video.Size = new System.Drawing.Size(529, 429);
            this.video.TabIndex = 1;
            this.video.TabStop = false;
            // 
            // DistanceWorkTextBox
            // 
            this.DistanceWorkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceWorkTextBox.Location = new System.Drawing.Point(750, 379);
            this.DistanceWorkTextBox.Name = "DistanceWorkTextBox";
            this.DistanceWorkTextBox.Size = new System.Drawing.Size(166, 155);
            this.DistanceWorkTextBox.TabIndex = 3;
            this.DistanceWorkTextBox.Text = "";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(547, 15);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(86, 13);
            this.weightLabel.TabIndex = 4;
            this.weightLabel.Text = "Held Weight (lb):";
            this.weightLabel.Click += new System.EventHandler(this.weightLabel_Click);
            // 
            // heldWeightTextbox
            // 
            this.heldWeightTextbox.Location = new System.Drawing.Point(639, 12);
            this.heldWeightTextbox.Name = "heldWeightTextbox";
            this.heldWeightTextbox.Size = new System.Drawing.Size(119, 20);
            this.heldWeightTextbox.TabIndex = 5;
            // 
            // PowerGraph
            // 
            chartArea7.Name = "ChartArea1";
            this.PowerGraph.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.PowerGraph.Legends.Add(legend7);
            this.PowerGraph.Location = new System.Drawing.Point(547, 56);
            this.PowerGraph.Name = "PowerGraph";
            series16.BorderWidth = 5;
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series16.Legend = "Legend1";
            series16.Name = "PowerData";
            series17.BorderWidth = 5;
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series17.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series17.Legend = "Legend1";
            series17.Name = "GoalLevel";
            this.PowerGraph.Series.Add(series16);
            this.PowerGraph.Series.Add(series17);
            this.PowerGraph.Size = new System.Drawing.Size(386, 317);
            this.PowerGraph.TabIndex = 6;
            this.PowerGraph.Text = "chart1";
            // 
            // IncreaseAngleButton
            // 
            this.IncreaseAngleButton.Location = new System.Drawing.Point(282, 495);
            this.IncreaseAngleButton.Name = "IncreaseAngleButton";
            this.IncreaseAngleButton.Size = new System.Drawing.Size(99, 23);
            this.IncreaseAngleButton.TabIndex = 7;
            this.IncreaseAngleButton.Text = "Increase Angle";
            this.IncreaseAngleButton.UseVisualStyleBackColor = true;
            this.IncreaseAngleButton.Click += new System.EventHandler(this.IncreaseAngleButton_Click);
            // 
            // DecreaseAngleButton
            // 
            this.DecreaseAngleButton.Location = new System.Drawing.Point(281, 551);
            this.DecreaseAngleButton.Name = "DecreaseAngleButton";
            this.DecreaseAngleButton.Size = new System.Drawing.Size(99, 23);
            this.DecreaseAngleButton.TabIndex = 8;
            this.DecreaseAngleButton.Text = "Decrease Angle";
            this.DecreaseAngleButton.UseVisualStyleBackColor = true;
            this.DecreaseAngleButton.Click += new System.EventHandler(this.DecreaseAngleButton_Click);
            // 
            // CurrentAngleTextbox
            // 
            this.CurrentAngleTextbox.Location = new System.Drawing.Point(281, 524);
            this.CurrentAngleTextbox.Name = "CurrentAngleTextbox";
            this.CurrentAngleTextbox.Size = new System.Drawing.Size(100, 20);
            this.CurrentAngleTextbox.TabIndex = 9;
            // 
            // AngleSlider
            // 
            this.AngleSlider.LargeChange = 0;
            this.AngleSlider.Location = new System.Drawing.Point(12, 513);
            this.AngleSlider.Maximum = 24;
            this.AngleSlider.Minimum = -24;
            this.AngleSlider.Name = "AngleSlider";
            this.AngleSlider.Size = new System.Drawing.Size(264, 45);
            this.AngleSlider.SmallChange = 0;
            this.AngleSlider.TabIndex = 10;
            this.AngleSlider.TickFrequency = 2;
            this.AngleSlider.Scroll += new System.EventHandler(this.AngleSlider_Scroll);
            this.AngleSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AngleSlider_MouseDown);
            this.AngleSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AngleSlider_MouseUp_1);
            // 
            // MinAngleLabel
            // 
            this.MinAngleLabel.AutoSize = true;
            this.MinAngleLabel.Location = new System.Drawing.Point(9, 561);
            this.MinAngleLabel.Name = "MinAngleLabel";
            this.MinAngleLabel.Size = new System.Drawing.Size(22, 13);
            this.MinAngleLabel.TabIndex = 11;
            this.MinAngleLabel.Text = "-24";
            // 
            // MaxAngleLabel
            // 
            this.MaxAngleLabel.AutoSize = true;
            this.MaxAngleLabel.Location = new System.Drawing.Point(251, 561);
            this.MaxAngleLabel.Name = "MaxAngleLabel";
            this.MaxAngleLabel.Size = new System.Drawing.Size(25, 13);
            this.MaxAngleLabel.TabIndex = 12;
            this.MaxAngleLabel.Text = "+24";
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceLabel.Location = new System.Drawing.Point(547, 376);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(197, 31);
            this.DistanceLabel.TabIndex = 13;
            this.DistanceLabel.Text = "Total Distance:";
            // 
            // WorkLabel
            // 
            this.WorkLabel.AutoSize = true;
            this.WorkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkLabel.Location = new System.Drawing.Point(547, 410);
            this.WorkLabel.Name = "WorkLabel";
            this.WorkLabel.Size = new System.Drawing.Size(0, 31);
            this.WorkLabel.TabIndex = 14;
            // 
            // PowerLabel
            // 
            this.PowerLabel.AutoSize = true;
            this.PowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerLabel.Location = new System.Drawing.Point(547, 441);
            this.PowerLabel.Name = "PowerLabel";
            this.PowerLabel.Size = new System.Drawing.Size(0, 31);
            this.PowerLabel.TabIndex = 15;
            // 
            // FilteredWorkLabel
            // 
            this.FilteredWorkLabel.AutoSize = true;
            this.FilteredWorkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredWorkLabel.Location = new System.Drawing.Point(547, 407);
            this.FilteredWorkLabel.Name = "FilteredWorkLabel";
            this.FilteredWorkLabel.Size = new System.Drawing.Size(183, 31);
            this.FilteredWorkLabel.TabIndex = 16;
            this.FilteredWorkLabel.Text = "Filtered Work:";
            // 
            // FilteredPowerLabel
            // 
            this.FilteredPowerLabel.AutoSize = true;
            this.FilteredPowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredPowerLabel.Location = new System.Drawing.Point(547, 438);
            this.FilteredPowerLabel.Name = "FilteredPowerLabel";
            this.FilteredPowerLabel.Size = new System.Drawing.Size(197, 31);
            this.FilteredPowerLabel.TabIndex = 17;
            this.FilteredPowerLabel.Text = "Filtered Power:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(922, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "(m)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(922, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 31);
            this.label2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(922, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 31);
            this.label3.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(922, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 31);
            this.label4.TabIndex = 21;
            this.label4.Text = "(J)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(922, 438);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "(J/s)";
            // 
            // PauseButton
            // 
            this.PauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseButton.Location = new System.Drawing.Point(1206, 379);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(119, 31);
            this.PauseButton.TabIndex = 23;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // ContinueButton
            // 
            this.ContinueButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContinueButton.Location = new System.Drawing.Point(1206, 416);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(119, 31);
            this.ContinueButton.TabIndex = 24;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // RecordButton
            // 
            this.RecordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordButton.Location = new System.Drawing.Point(1206, 513);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(119, 35);
            this.RecordButton.TabIndex = 25;
            this.RecordButton.Text = "Record";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.RecordButton_Click);
            // 
            // AngleGraph
            // 
            chartArea8.Name = "ChartArea1";
            this.AngleGraph.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.AngleGraph.Legends.Add(legend8);
            this.AngleGraph.Location = new System.Drawing.Point(939, 56);
            this.AngleGraph.Name = "AngleGraph";
            series18.BorderWidth = 5;
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series18.Legend = "Legend1";
            series18.Name = "AngleR";
            series19.BorderWidth = 5;
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series19.Legend = "Legend1";
            series19.Name = "FilteredAR";
            series20.BorderWidth = 5;
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series20.Legend = "Legend1";
            series20.Name = "AngleLeft";
            this.AngleGraph.Series.Add(series18);
            this.AngleGraph.Series.Add(series19);
            this.AngleGraph.Series.Add(series20);
            this.AngleGraph.Size = new System.Drawing.Size(386, 317);
            this.AngleGraph.TabIndex = 26;
            this.AngleGraph.Text = "chart1";
            // 
            // FilteredFrequencyTextBox
            // 
            this.FilteredFrequencyTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredFrequencyTextBox.Location = new System.Drawing.Point(750, 538);
            this.FilteredFrequencyTextBox.Name = "FilteredFrequencyTextBox";
            this.FilteredFrequencyTextBox.Size = new System.Drawing.Size(166, 41);
            this.FilteredFrequencyTextBox.TabIndex = 27;
            this.FilteredFrequencyTextBox.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(547, 538);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 31);
            this.label6.TabIndex = 28;
            this.label6.Text = "Frequency:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(922, 541);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 31);
            this.label7.TabIndex = 29;
            this.label7.Text = "(Hz)";
            // 
            // bodyWeightTextbox
            // 
            this.bodyWeightTextbox.Location = new System.Drawing.Point(862, 12);
            this.bodyWeightTextbox.Name = "bodyWeightTextbox";
            this.bodyWeightTextbox.Size = new System.Drawing.Size(147, 20);
            this.bodyWeightTextbox.TabIndex = 30;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(768, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 31;
            this.label8.Text = "Body Weight (lb):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1037, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 32;
            this.label9.Text = "Arm Length (inches):";
            // 
            // armLengthTextbox
            // 
            this.armLengthTextbox.Location = new System.Drawing.Point(1147, 12);
            this.armLengthTextbox.Name = "armLengthTextbox";
            this.armLengthTextbox.Size = new System.Drawing.Size(150, 20);
            this.armLengthTextbox.TabIndex = 33;
            // 
            // TestTextBox
            // 
            this.TestTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TestTextBox.Location = new System.Drawing.Point(996, 379);
            this.TestTextBox.Name = "TestTextBox";
            this.TestTextBox.Size = new System.Drawing.Size(198, 49);
            this.TestTextBox.TabIndex = 34;
            this.TestTextBox.Text = "";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(547, 472);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 31);
            this.label10.TabIndex = 35;
            this.label10.Text = "Internal Work:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(547, 503);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(197, 31);
            this.label11.TabIndex = 36;
            this.label11.Text = "Internal Power:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(922, 503);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 31);
            this.label12.TabIndex = 37;
            this.label12.Text = "(J/s)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(922, 472);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 31);
            this.label13.TabIndex = 38;
            this.label13.Text = "(J)";
            // 
            // KinectDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 593);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.TestTextBox);
            this.Controls.Add(this.armLengthTextbox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bodyWeightTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.FilteredFrequencyTextBox);
            this.Controls.Add(this.AngleGraph);
            this.Controls.Add(this.RecordButton);
            this.Controls.Add(this.ContinueButton);
            this.Controls.Add(this.PauseButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilteredPowerLabel);
            this.Controls.Add(this.FilteredWorkLabel);
            this.Controls.Add(this.PowerLabel);
            this.Controls.Add(this.WorkLabel);
            this.Controls.Add(this.DistanceLabel);
            this.Controls.Add(this.MaxAngleLabel);
            this.Controls.Add(this.MinAngleLabel);
            this.Controls.Add(this.AngleSlider);
            this.Controls.Add(this.CurrentAngleTextbox);
            this.Controls.Add(this.DecreaseAngleButton);
            this.Controls.Add(this.IncreaseAngleButton);
            this.Controls.Add(this.PowerGraph);
            this.Controls.Add(this.heldWeightTextbox);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.DistanceWorkTextBox);
            this.Controls.Add(this.video);
            this.Controls.Add(this.rtbMessages);
            this.Name = "KinectDisplay";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.video)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbMessages;
        private System.Windows.Forms.PictureBox video;
        private System.Windows.Forms.RichTextBox DistanceWorkTextBox;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.TextBox heldWeightTextbox;
        private System.Windows.Forms.DataVisualization.Charting.Chart PowerGraph;
        private System.Windows.Forms.Button IncreaseAngleButton;
        private System.Windows.Forms.Button DecreaseAngleButton;
        private System.Windows.Forms.TextBox CurrentAngleTextbox;
        private System.Windows.Forms.TrackBar AngleSlider;
        private System.Windows.Forms.Label MinAngleLabel;
        private System.Windows.Forms.Label MaxAngleLabel;
        private System.Windows.Forms.Label DistanceLabel;
        private System.Windows.Forms.Label WorkLabel;
        private System.Windows.Forms.Label PowerLabel;
        private System.Windows.Forms.Label FilteredWorkLabel;
        private System.Windows.Forms.Label FilteredPowerLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart AngleGraph;
        private System.Windows.Forms.RichTextBox FilteredFrequencyTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox bodyWeightTextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox armLengthTextbox;
        private System.Windows.Forms.RichTextBox TestTextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
    }
}

