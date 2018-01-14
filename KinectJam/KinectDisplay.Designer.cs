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
            this.DistanceWorkTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DistanceWorkTextBox.Location = new System.Drawing.Point(659, 378);
            this.DistanceWorkTextBox.Name = "DistanceWorkTextBox";
            this.DistanceWorkTextBox.Size = new System.Drawing.Size(555, 203);
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
            this.AngleSlider.LargeChange = 2;
            this.AngleSlider.Location = new System.Drawing.Point(311, 532);
            this.AngleSlider.Maximum = 24;
            this.AngleSlider.Minimum = -24;
            this.AngleSlider.Name = "AngleSlider";
            this.AngleSlider.Size = new System.Drawing.Size(212, 45);
            this.AngleSlider.TabIndex = 10;
            this.AngleSlider.TickFrequency = 2;
            this.AngleSlider.Scroll += new System.EventHandler(this.AngleSlider_Scroll);
            this.AngleSlider.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AngleSlider_MouseDown);
            this.AngleSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AngleSlider_MouseUp_1);
            // 
            // KinectDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 589);
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
    }
}

