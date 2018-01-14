﻿namespace KinectJam
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            ((System.ComponentModel.ISupportInitialize)(this.video)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PowerGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AngleSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbMessages
            // 
            this.rtbMessages.Location = new System.Drawing.Point(12, 498);
            this.rtbMessages.Name = "rtbMessages";
            this.rtbMessages.Size = new System.Drawing.Size(264, 83);
            this.rtbMessages.TabIndex = 0;
            this.rtbMessages.Text = "";
            // 
            // video
            // 
            this.video.Location = new System.Drawing.Point(12, 12);
            this.video.Name = "video";
            this.video.Size = new System.Drawing.Size(640, 480);
            this.video.TabIndex = 1;
            this.video.TabStop = false;
            // 
            // DistanceWorkTextBox
            // 
            this.DistanceWorkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceWorkTextBox.Location = new System.Drawing.Point(862, 374);
            this.DistanceWorkTextBox.Name = "DistanceWorkTextBox";
            this.DistanceWorkTextBox.Size = new System.Drawing.Size(166, 203);
            this.DistanceWorkTextBox.TabIndex = 3;
            this.DistanceWorkTextBox.Text = "";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(658, 12);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(86, 13);
            this.weightLabel.TabIndex = 4;
            this.weightLabel.Text = "Held Weight (lb):";
            this.weightLabel.Click += new System.EventHandler(this.weightLabel_Click);
            // 
            // heldWeightTextbox
            // 
            this.heldWeightTextbox.Location = new System.Drawing.Point(750, 9);
            this.heldWeightTextbox.Name = "heldWeightTextbox";
            this.heldWeightTextbox.Size = new System.Drawing.Size(183, 20);
            this.heldWeightTextbox.TabIndex = 5;
            // 
            // PowerGraph
            // 
            chartArea2.Name = "ChartArea1";
            this.PowerGraph.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.PowerGraph.Legends.Add(legend2);
            this.PowerGraph.Location = new System.Drawing.Point(659, 29);
            this.PowerGraph.Name = "PowerGraph";
            series2.BorderWidth = 5;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "PowerData";
            this.PowerGraph.Series.Add(series2);
            this.PowerGraph.Size = new System.Drawing.Size(555, 343);
            this.PowerGraph.TabIndex = 6;
            this.PowerGraph.Text = "chart1";
            // 
            // IncreaseAngleButton
            // 
            this.IncreaseAngleButton.Location = new System.Drawing.Point(553, 498);
            this.IncreaseAngleButton.Name = "IncreaseAngleButton";
            this.IncreaseAngleButton.Size = new System.Drawing.Size(99, 23);
            this.IncreaseAngleButton.TabIndex = 7;
            this.IncreaseAngleButton.Text = "Increase Angle";
            this.IncreaseAngleButton.UseVisualStyleBackColor = true;
            this.IncreaseAngleButton.Click += new System.EventHandler(this.IncreaseAngleButton_Click);
            // 
            // DecreaseAngleButton
            // 
            this.DecreaseAngleButton.Location = new System.Drawing.Point(553, 554);
            this.DecreaseAngleButton.Name = "DecreaseAngleButton";
            this.DecreaseAngleButton.Size = new System.Drawing.Size(99, 23);
            this.DecreaseAngleButton.TabIndex = 8;
            this.DecreaseAngleButton.Text = "Decrease Angle";
            this.DecreaseAngleButton.UseVisualStyleBackColor = true;
            this.DecreaseAngleButton.Click += new System.EventHandler(this.DecreaseAngleButton_Click);
            // 
            // CurrentAngleTextbox
            // 
            this.CurrentAngleTextbox.Location = new System.Drawing.Point(553, 528);
            this.CurrentAngleTextbox.Name = "CurrentAngleTextbox";
            this.CurrentAngleTextbox.Size = new System.Drawing.Size(100, 20);
            this.CurrentAngleTextbox.TabIndex = 9;
            // 
            // AngleSlider
            // 
            this.AngleSlider.LargeChange = 0;
            this.AngleSlider.Location = new System.Drawing.Point(311, 532);
            this.AngleSlider.Maximum = 24;
            this.AngleSlider.Minimum = -24;
            this.AngleSlider.Name = "AngleSlider";
            this.AngleSlider.Size = new System.Drawing.Size(212, 45);
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
            this.MinAngleLabel.Location = new System.Drawing.Point(308, 564);
            this.MinAngleLabel.Name = "MinAngleLabel";
            this.MinAngleLabel.Size = new System.Drawing.Size(22, 13);
            this.MinAngleLabel.TabIndex = 11;
            this.MinAngleLabel.Text = "-24";
            // 
            // MaxAngleLabel
            // 
            this.MaxAngleLabel.AutoSize = true;
            this.MaxAngleLabel.Location = new System.Drawing.Point(501, 564);
            this.MaxAngleLabel.Name = "MaxAngleLabel";
            this.MaxAngleLabel.Size = new System.Drawing.Size(25, 13);
            this.MaxAngleLabel.TabIndex = 12;
            this.MaxAngleLabel.Text = "+24";
            // 
            // DistanceLabel
            // 
            this.DistanceLabel.AutoSize = true;
            this.DistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceLabel.Location = new System.Drawing.Point(659, 379);
            this.DistanceLabel.Name = "DistanceLabel";
            this.DistanceLabel.Size = new System.Drawing.Size(197, 31);
            this.DistanceLabel.TabIndex = 13;
            this.DistanceLabel.Text = "Total Distance:";
            // 
            // WorkLabel
            // 
            this.WorkLabel.AutoSize = true;
            this.WorkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WorkLabel.Location = new System.Drawing.Point(659, 410);
            this.WorkLabel.Name = "WorkLabel";
            this.WorkLabel.Size = new System.Drawing.Size(153, 31);
            this.WorkLabel.TabIndex = 14;
            this.WorkLabel.Text = "Total Work:";
            // 
            // PowerLabel
            // 
            this.PowerLabel.AutoSize = true;
            this.PowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PowerLabel.Location = new System.Drawing.Point(659, 441);
            this.PowerLabel.Name = "PowerLabel";
            this.PowerLabel.Size = new System.Drawing.Size(99, 31);
            this.PowerLabel.TabIndex = 15;
            this.PowerLabel.Text = "Power:";
            // 
            // FilteredWorkLabel
            // 
            this.FilteredWorkLabel.AutoSize = true;
            this.FilteredWorkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredWorkLabel.Location = new System.Drawing.Point(659, 472);
            this.FilteredWorkLabel.Name = "FilteredWorkLabel";
            this.FilteredWorkLabel.Size = new System.Drawing.Size(183, 31);
            this.FilteredWorkLabel.TabIndex = 16;
            this.FilteredWorkLabel.Text = "Filtered Work:";
            // 
            // FilteredPowerLabel
            // 
            this.FilteredPowerLabel.AutoSize = true;
            this.FilteredPowerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilteredPowerLabel.Location = new System.Drawing.Point(659, 503);
            this.FilteredPowerLabel.Name = "FilteredPowerLabel";
            this.FilteredPowerLabel.Size = new System.Drawing.Size(197, 31);
            this.FilteredPowerLabel.TabIndex = 17;
            this.FilteredPowerLabel.Text = "Filtered Power:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1034, 379);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "(m)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1034, 410);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 31);
            this.label2.TabIndex = 19;
            this.label2.Text = "(J)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1034, 441);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 31);
            this.label3.TabIndex = 20;
            this.label3.Text = "(J/s)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1034, 472);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 31);
            this.label4.TabIndex = 21;
            this.label4.Text = "(J)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1034, 503);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "(J/s)";
            // 
            // PauseButton
            // 
            this.PauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PauseButton.Location = new System.Drawing.Point(1095, 379);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(119, 31);
            this.PauseButton.TabIndex = 23;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // KinectDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 589);
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
    }
}

