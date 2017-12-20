using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using static KinectJam.Program;
using Microsoft.Kinect.Toolkit.Fusion;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

// Control+k+c to comment line
// Control+k+u to uncomment line

/*References
 * https://code.msdn.microsoft.com/windowsdesktop/Beginning-Kinect-for-a198d400
 * https://msdn.microsoft.com/en-us/library/jj131025.aspx
 * */

namespace KinectJam
{
    public partial class KinectDisplay : Form
    {
        private KinectSensorChooser _chooser;
        private KinectSensor _sensor;
        private Skeleton[] _skeletonData;
        private Bitmap _bitmap;
        private byte[] _colorData;
        private IntPtr _colorPtr;

        private const float _renderWidth = 640.0f;
        private const float _renderHeight = 480.0f;

        private const double _jointThickness = 3;
        private const double _bodyCenterThickness = 10;
        private const double _clipBoundsThickness = 10;
        private const int _maxPowerSize = 1500;

        //private readonly System.Windows.Media.Brush _centerPointBrush = System.Windows.Media.Brushes.Blue;
        //private readonly System.Windows.Media.Brush _trackedJointBrush = System.Windows.Media.Brushes.Red;
        //private readonly System.Windows.Media.Brush _inferredJointBrush = System.Windows.Media.Brushes.Yellow;

        //private readonly System.Windows.Media.Pen _trackedBonePen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Green, 6);
        //private readonly System.Windows.Media.Pen _inferredBonePen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.Gray, 1);

        private Graphics _graphics;
        private Rectangle _rectangle = new Rectangle(340, 90, 190, 150);
        private Pen _penBlack = new Pen(Brushes.Black, 6);
        private Pen _penGreen = new Pen(Brushes.Green, 6);
        private Pen _penRed = new Pen(Brushes.Red, 6);
        private Pen _penBlue = new Pen(Brushes.Blue, 6);
        private Pen _penWhite = new Pen(Brushes.White, 6);
        private Pen _borderPen = new Pen(Brushes.Black, 3.0f);

        private bool _exerciseStarted = false;

        private Joint _initialShoulderPoint = new Joint();
        private Joint _initialElbowPoint = new Joint();
        private Joint _initialWristPoint = new Joint();

        private Joint _wristFinal = new Joint();
        private Joint _wristInitial = new Joint();

        private double _totalDistance = 0;
        private double _totalWork = 0;
        private double _totalFilteredWork = 0;

        private double _totalTime = 0;

        public SelectionType _selection;
        public int frame = 0;

        private double _alpha = 0.98;
        private double _previousFilteredWork = 0;

        private double[] timeArray = new double[50];
        private double[] workArray = new double[50];

        public KinectDisplay()
        {
            InitializeComponent();
            double initialTime = -50.0 * (1.0 / 30.0);
            //timeArray.Add(initialTime);
            for (int i = 0; i < 50; i++)
            {
                workArray[i] = 0;
                if (i == 49)
                    timeArray[i] = 0;
                else
                    timeArray[i] = Math.Round(initialTime += (1.0 / 30.0), 2);
            }

            PowerGraph.ChartAreas[0].AxisY.Maximum = _maxPowerSize;
            PowerGraph.ChartAreas[0].AxisY.Minimum = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _chooser = new KinectSensorChooser();
            _chooser.KinectChanged += ChooserSensorChanged;
            _chooser.Start();

            _graphics = video.CreateGraphics();
        }

        private void ChooserSensorChanged(object sender, KinectChangedEventArgs e)
        {
            KinectSensor oldSensor = e.OldSensor;
            StopKinect(oldSensor);

            _sensor = e.NewSensor;
            if (_sensor == null)
                return;

            _sensor.SkeletonStream.Enable();
            _sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
            //_sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

            _skeletonData = new Skeleton[_sensor.SkeletonStream.FrameSkeletonArrayLength];
            _sensor.AllFramesReady += NewSensor_AllFramesReady;

            try
            {
                _sensor.Start();
                rtbMessages.Text = "Kinect Started" + "\r";
                CurrentAngleTextbox.Text = _sensor.ElevationAngle.ToString();
                
            }
            catch (System.IO.IOException)
            {
                rtbMessages.Text = "Kinect Not Started" + "\r";
                _chooser.TryResolveConflict();
            }

        }

        private void NewSensor_DepthFrameReady(object sender, AllFramesReadyEventArgs e)
        {
            using (DepthImageFrame frame = e.OpenDepthImageFrame())
            {
                _bitmap = CreateBitMapFromDepthFrame(frame);
            }
        }

        private void NewSensor_ColorFrameReady(object sender, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame == null) return;
                if (_colorData == null)
                    _colorData = new byte[colorFrame.PixelDataLength];
                colorFrame.CopyPixelDataTo(_colorData);
                Marshal.FreeHGlobal(_colorPtr);
                _colorPtr = Marshal.AllocHGlobal(_colorData.Length);
                Marshal.Copy(_colorData, 0, _colorPtr, _colorData.Length);

                _bitmap = new Bitmap(
                    colorFrame.Width,
                    colorFrame.Height,
                    colorFrame.Width * colorFrame.BytesPerPixel,
                    PixelFormat.Format32bppRgb,
                    _colorPtr);

                //colorFrame.CopyPixelDataTo(_colorData);
                //_bitmap = CreateBitMapFromColorFrame(colorFrame, _colorData);
                ////_bitmap = CreateBitMapFromColorFrame(frame);
            }
        }

        private void NewSensor_SkeletonFrameReady(object sender, AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null && this._skeletonData != null)
                {
                    skeletonFrame.CopySkeletonDataTo(this._skeletonData);
                }
            }

            if (_bitmap != null)
                using (_graphics = Graphics.FromImage(_bitmap))
                {
                    DrawSkeletons();
                }
        }

        private void NewSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            _bitmap = new Bitmap(640, 480, PixelFormat.Format16bppRgb565);
            //NewSensor_DepthFrameReady(sender, e);
            NewSensor_ColorFrameReady(sender, e);
            NewSensor_SkeletonFrameReady(sender, e);

            video.Image = _bitmap;

            //CurrentAngleTextbox.Text = _sensor.ElevationAngle.ToString();

            foreach (Skeleton skeleton in this._skeletonData)
            {
                if (skeleton != null)
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (_bitmap != null)
                        {
                            using (_graphics = Graphics.FromImage(_bitmap))
                            {
                                DepthImagePoint shoulder = GetDepthPoint(skeleton.Joints[JointType.ShoulderRight]);
                                //ColorImagePoint shoulder = GetColorPoint(skeleton.Joints[JointType.ShoulderRight]);

                                Rectangle exerciseArea = new Rectangle(shoulder.X - 25, shoulder.Y - 110, 275, 220);

                                double angle = GetAngle(GetDepthPoint(skeleton.Joints[JointType.ShoulderRight]), GetDepthPoint(skeleton.Joints[JointType.ElbowRight]), GetDepthPoint(skeleton.Joints[JointType.WristRight]));
                                //double angle = GetAngleColor(GetColorPoint(skeleton.Joints[JointType.ShoulderRight]), GetColorPoint(skeleton.Joints[JointType.ElbowRight]), GetColorPoint(skeleton.Joints[JointType.WristRight]));

                                bool isInInitialPosition = angle >= 0 && angle <= 10;
                                if (isInInitialPosition || _exerciseStarted)
                                {
                                    if (isInInitialPosition && !_exerciseStarted)
                                    {
                                        _initialShoulderPoint = skeleton.Joints[JointType.ShoulderRight];
                                        _initialElbowPoint = skeleton.Joints[JointType.ElbowRight];
                                        _initialWristPoint = skeleton.Joints[JointType.WristRight];

                                        _wristFinal = _initialWristPoint;
                                        _wristInitial = _initialWristPoint;
                                        _exerciseStarted = true;
                                    }
                                    StringBuilder stringBuilder = new StringBuilder();


                                    _wristFinal = skeleton.Joints[JointType.WristRight];

                                    double instantDistance = Distance(_wristFinal, _wristInitial);
                                    _totalDistance += instantDistance;

                                    _totalTime += (1.0 / 30.0);

                                    double accelerationX = Accel(_wristFinal.Position.X, _wristInitial.Position.X);
                                    double accelerationY = Accel(_wristFinal.Position.Y, _wristInitial.Position.Y);
                                    double forceX = Force(accelerationX);
                                    double forceY = Force(accelerationY, true);

                                    double work = Work(TotalForce(forceX, forceY), instantDistance);
                                    _totalWork += work;
                                    double power = Power(work);

                                    double filteredwork = FilteredWork(_alpha, _previousFilteredWork, work);
                                    _totalFilteredWork += filteredwork;
                                    double filteredpower = Power(filteredwork);

                                    //stringBuilder.AppendLine(string.Format("Acceleration X: {0} (m/s^2)", Math.Round(accelerationX,2)));
                                    //stringBuilder.AppendLine(string.Format("Acceleration Y: {0} (m/s^2)", Math.Round(accelerationY)));
                                    //stringBuilder.AppendLine(string.Format("Force X: {0} (N)", Math.Round(forceX,2)));
                                    //stringBuilder.AppendLine(string.Format("Force Y: {0} (N)", Math.Round(forceY,2)));

                                    stringBuilder.AppendLine(string.Format("Total Distance: {0} (m)", Math.Round(_totalDistance, 2)));
                                    stringBuilder.AppendLine(string.Format("Total Work: {0} (J)", Math.Round(_totalWork, 1)));
                                    stringBuilder.AppendLine(string.Format("Power: {0} (J/s)", Math.Round(power, 1)));
                                    stringBuilder.AppendLine(string.Format("Filtered Work: {0} (J)", Math.Round(_totalFilteredWork, 1)));
                                    stringBuilder.AppendLine(string.Format("Filtered Power: {0} (J/s)", Math.Round(filteredpower, 1)));

                                    DistanceWorkTextBox.Text = string.Empty;
                                    DistanceWorkTextBox.Text = stringBuilder.ToString();
                                    GraphingPower(_totalTime, Math.Round(filteredpower, 2));

                                    _wristInitial = _wristFinal;

                                    _previousFilteredWork = filteredwork;

                                    if (_exerciseStarted && (angle >= 80 && angle <= 100))
                                        _graphics.DrawRectangle(_penBlue, exerciseArea);
                                    else
                                        _graphics.DrawRectangle(_penGreen, exerciseArea);

                                }
                                else
                                    _graphics.DrawRectangle(_penRed, exerciseArea);
                            }
                        }
                    }
            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    sensor.Stop();
                    sensor.AudioSource.Stop();
                }
            }
        }

        private Bitmap CreateBitMapFromDepthFrame(DepthImageFrame frame)
        {
            if (frame != null)
            {
                Bitmap bitmapImage = new Bitmap(frame.Width, frame.Height, PixelFormat.Format16bppRgb565);
                using (_graphics = Graphics.FromImage(bitmapImage))
                {
                    _graphics = Graphics.FromImage(bitmapImage);
                    _graphics.Clear(Color.FromArgb(0, 34, 68));

                    short[] pixelData = new short[frame.PixelDataLength];
                    frame.CopyPixelDataTo(pixelData);
                    BitmapData bitmapData = bitmapImage.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                    IntPtr ptr = bitmapData.Scan0;
                    Marshal.Copy(pixelData, 0, ptr, frame.Width * frame.Height);
                    bitmapImage.UnlockBits(bitmapData);

                    return bitmapImage;
                }
            }
            return null;
        }

        private Bitmap CreateBitMapFromColorFrame(ColorImageFrame frame, byte[] colorData)
        {
            if (frame != null)
            {
                Bitmap bitmapImage = new Bitmap(frame.Width, frame.Height, PixelFormat.Format16bppRgb565);
                using (_graphics = Graphics.FromImage(bitmapImage))
                {
                    _graphics = Graphics.FromImage(bitmapImage);
                    _graphics.Clear(Color.FromArgb(0, 34, 68));

                    //byte[] pixelData = new byte[frame.PixelDataLength];
                    //frame.CopyPixelDataTo(pixelData);
                    BitmapData bitmapData = bitmapImage.LockBits(new Rectangle(0, 0, frame.Width, frame.Height), ImageLockMode.WriteOnly, bitmapImage.PixelFormat);
                    IntPtr ptr = bitmapData.Scan0;
                    Marshal.Copy(colorData, 0, ptr, frame.Width * frame.Height);
                    bitmapImage.UnlockBits(bitmapData);

                    return bitmapImage;
                }
            }
            return null;
        }


        // F12 for viewing properties.
        // skeleton.Joints[JointType.Head]
        private void DrawSkeletons()
        {
            foreach (Skeleton skeleton in this._skeletonData)
            {
                if (skeleton != null)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        DrawTrackedSkeletonJoints(skeleton.Joints);
                    }
                    else if (skeleton.TrackingState == SkeletonTrackingState.PositionOnly)
                    {
                        DrawSkeletonPosition(skeleton.Position);
                    }
                }
            }
        }

        private double GetAngle(DepthImagePoint shoulder, DepthImagePoint elbow, DepthImagePoint wrist)
        {
            double dotProduct = DotProduct(shoulder, elbow, wrist);
            return Math.Acos(dotProduct) * (180 / Math.PI);
        }

        //private double GetAngleColor(ColorImagePoint shoulder, ColorImagePoint elbow, ColorImagePoint wrist)
        //{
        //    double dotProduct = DotProductColor(shoulder, elbow, wrist);
        //    return Math.Acos(dotProduct) * (180 / Math.PI);
        //}

        private double DotProduct(DepthImagePoint joint1, DepthImagePoint joint2, DepthImagePoint joint3)
        {
            Vector3 vectorA = new Vector3();
            vectorA.X = (joint1.X - joint2.X);
            vectorA.Y = (joint1.Y - joint2.Y);
            Vector3 vectorB = new Vector3();
            vectorB.X = (joint2.X - joint3.X);
            vectorB.Y = (joint2.Y - joint3.Y);

            double u = vectorA.X * vectorB.X;
            double v = vectorA.Y * vectorB.Y;
            double total = u + v;
            double magnitude = Magnitude(vectorA) * Magnitude(vectorB);
            return (u + v) / (Magnitude(vectorA) * Magnitude(vectorB));
        }

        //private double DotProductColor(ColorImagePoint joint1, ColorImagePoint joint2, ColorImagePoint joint3)
        //{
        //    Vector3 vectorA = new Vector3();
        //    vectorA.X = (joint1.X - joint2.X);
        //    vectorA.Y = (joint1.Y - joint2.Y);
        //    Vector3 vectorB = new Vector3();
        //    vectorB.X = (joint2.X - joint3.X);
        //    vectorB.Y = (joint2.Y - joint3.Y);

        //    double u = vectorA.X * vectorB.X;
        //    double v = vectorA.Y * vectorB.Y;
        //    double total = u + v;
        //    double magnitude = Magnitude(vectorA) * Magnitude(vectorB);
        //    return (u + v) / (Magnitude(vectorA) * Magnitude(vectorB));
        //}
        
        private double Distance(Joint final, Joint initial)
        {
            double changeX = Math.Pow(final.Position.X - initial.Position.X, 2);
            double changeY = Math.Pow(final.Position.Y - initial.Position.Y, 2);
            return Math.Sqrt(changeX + changeY);
        }

        private double Accel(float finalPosition, float initialPosition)
        {
            double change = finalPosition - initialPosition;
            return (2 * (change)) / Math.Pow(1.0 / 30.0, 2.0);
        }

        private double Force(double acceleration, bool accountForGravity = false)
        {
            double heldWeight = 0;
            if (double.TryParse(heldWeightTextbox.Text, out heldWeight))
            {
                heldWeight = heldWeight * 0.4536;
                return accountForGravity ? (heldWeight * acceleration) - (9.81 * heldWeight) : heldWeight * acceleration;
            }
            return 0;
        }

        private double TotalForce(double forceX, double forceY)
        {
            double fx = Math.Pow(forceX, 2);
            double fy = Math.Pow(forceY, 2);
            return Math.Sqrt(fx + fy);
        }

        private double Work(double totalForce, double distance)
        {
            return totalForce * distance;
        }

        private double FilteredWork(double alpha, double previousFilteredWork, double work)
        {
            // y(n) = alpha*y(n-1) + (1-alpha)*x(n)
            return alpha *previousFilteredWork + (1.0 - alpha) * work;
        }

        private double Power(double work)
        {
            return work * 30.0;
        }

        private double Magnitude(Vector3 vector)
        {
            return Math.Sqrt(Math.Pow(vector.X, 2) + Math.Pow(vector.Y, 2));
        }

        private void DrawTrackedSkeletonJoints(JointCollection jointCollection)
        {
            DrawBone(jointCollection[JointType.Head], jointCollection[JointType.ShoulderCenter]);
            
            DrawBone(jointCollection[JointType.ShoulderCenter], jointCollection[JointType.ShoulderLeft]);
            DrawBone(jointCollection[JointType.ShoulderLeft], jointCollection[JointType.ElbowLeft]);
            DrawBone(jointCollection[JointType.ElbowLeft], jointCollection[JointType.WristLeft]);
            DrawBone(jointCollection[JointType.WristLeft], jointCollection[JointType.HandLeft]);

            DrawBone(jointCollection[JointType.ShoulderCenter], jointCollection[JointType.ShoulderRight]);
            DrawBone(jointCollection[JointType.ShoulderRight], jointCollection[JointType.ElbowRight]);
            DrawBone(jointCollection[JointType.ElbowRight], jointCollection[JointType.WristRight]);
            DrawBone(jointCollection[JointType.WristRight], jointCollection[JointType.HandRight]);

            DrawBone(jointCollection[JointType.ShoulderCenter], jointCollection[JointType.Spine]);
            DrawBone(jointCollection[JointType.Spine], jointCollection[JointType.HipCenter]);

            DrawBone(jointCollection[JointType.HipCenter], jointCollection[JointType.HipLeft]);
            DrawBone(jointCollection[JointType.HipLeft], jointCollection[JointType.KneeLeft]);
            DrawBone(jointCollection[JointType.KneeLeft], jointCollection[JointType.AnkleLeft]);
            DrawBone(jointCollection[JointType.AnkleLeft], jointCollection[JointType.FootLeft]);

            DrawBone(jointCollection[JointType.HipCenter], jointCollection[JointType.HipRight]);
            DrawBone(jointCollection[JointType.HipRight], jointCollection[JointType.KneeRight]);
            DrawBone(jointCollection[JointType.KneeRight], jointCollection[JointType.AnkleRight]);
            DrawBone(jointCollection[JointType.AnkleRight], jointCollection[JointType.FootRight]);

            DrawPoint(jointCollection[JointType.Head]);
            DrawPoint(jointCollection[JointType.ShoulderCenter]);

            DrawPoint(jointCollection[JointType.ShoulderLeft]);
            DrawPoint(jointCollection[JointType.ElbowLeft]);
            DrawPoint(jointCollection[JointType.WristLeft]);
            DrawPoint(jointCollection[JointType.HandLeft]);

            DrawPoint(jointCollection[JointType.ShoulderRight]);
            DrawPoint(jointCollection[JointType.ElbowRight]);
            DrawPoint(jointCollection[JointType.WristRight]);
            DrawPoint(jointCollection[JointType.HandRight]);

            DrawPoint(jointCollection[JointType.Spine]);
            DrawPoint(jointCollection[JointType.HipCenter]);

            DrawPoint(jointCollection[JointType.HipLeft]);
            DrawPoint(jointCollection[JointType.KneeLeft]);
            DrawPoint(jointCollection[JointType.AnkleLeft]);
            DrawPoint(jointCollection[JointType.FootLeft]);

            DrawPoint(jointCollection[JointType.HipRight]);
            DrawPoint(jointCollection[JointType.KneeRight]);
            DrawPoint(jointCollection[JointType.AnkleRight]);
            DrawPoint(jointCollection[JointType.FootRight]);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.Head]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderCenter]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.Spine]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HipCenter]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderRight]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ElbowRight]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.WristRight]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HandRight]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderLeft]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ElbowLeft]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.WristLeft]));
            stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HandLeft]));

            JointCoordinatesTextBox.Text = string.Empty;
            JointCoordinatesTextBox.Text = stringBuilder.ToString();
        }

        private DepthImagePoint GetDepthPoint(Joint joint)
        {
            return _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, DepthImageFormat.Resolution640x480Fps30);
        }
        
        //private ColorImagePoint GetColorPoint(Joint joint)
        //{
        //    return _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(joint.Position, ColorImageFormat.RgbResolution640x480Fps30);
        //}

        private string PrintJointCoordinates(Joint joint)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DepthImagePoint depthPoint = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, DepthImageFormat.Resolution640x480Fps30);
            stringBuilder.Append(string.Format("{0} x: {1} y: {2} Depth: {3}", joint.JointType.ToString(), depthPoint.X, depthPoint.Y, depthPoint.Depth));
            return stringBuilder.ToString();
        }

        //private string PrintJointCoordinatesFromColor(Joint joint)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    ColorImagePoint imagePoint = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(joint.Position, ColorImageFormat.RgbResolution640x480Fps30);
        //    stringBuilder.Append(string.Format("{0} x: {1} y: {2} Depth: {3}", joint.JointType.ToString(), imagePoint.X, imagePoint.Y, imagePoint.Depth));
        //    return stringBuilder.ToString();
        //}

        private void DrawBone(Joint jointFrom, Joint jointTo)
        {
            DepthImagePoint p1 = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(jointFrom.Position, DepthImageFormat.Resolution640x480Fps30);
            DepthImagePoint p2 = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(jointTo.Position, DepthImageFormat.Resolution640x480Fps30);
            if (!video.IsDisposed)
                _graphics.DrawLine(_penWhite, p1.X, p1.Y, p2.X, p2.Y);
        }

        //private void DrawBoneFromColor(Joint jointFrom, Joint jointTo)
        //{
        //    ColorImagePoint p1 = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(jointFrom.Position, ColorImageFormat.RgbResolution640x480Fps30);
        //    ColorImagePoint p2 = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(jointTo.Position, ColorImageFormat.RgbResolution640x480Fps30);
        //    if (!video.IsDisposed)
        //        _graphics.DrawLine(_penWhite, p1.X, p1.Y, p2.X, p2.Y);
        //}

        private void DrawPoint(Joint joint)
        {
            DepthImagePoint point = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, DepthImageFormat.Resolution640x480Fps30);
            if (!video.IsDisposed)
            {
                RectangleF rectangle = new RectangleF(point.X, point.Y, 10, 10);
                _graphics.DrawEllipse(_penRed, rectangle);
                _graphics.FillEllipse(Brushes.Red, rectangle);
            }
        }

        //private void DrawPointFromColor(Joint joint)
        //{
        //    ColorImagePoint point = _sensor.CoordinateMapper.MapSkeletonPointToColorPoint(joint.Position, ColorImageFormat.RgbResolution640x480Fps30);
        //    if(!video.IsDisposed)
        //    {
        //        RectangleF rectangle = new RectangleF(point.X, point.Y, 10, 10);
        //        _graphics.DrawEllipse(_penRed, rectangle);
        //        _graphics.FillEllipse(Brushes.Red, rectangle);
        //    }
        //}

        private void DrawSkeletonPosition(SkeletonPoint position)
        {

        }

        private void DrawTrackedBoneLine(SkeletonPoint jointFromPosition, SkeletonPoint jointToPosition)
        {
            _graphics.DrawLine(_penBlack, jointFromPosition.X*100, jointFromPosition.Y*100, jointToPosition.X*100, jointFromPosition.Y*100);
        }

        private void DrawNonTrackedBoneLine(SkeletonPoint jointFromPosition, SkeletonPoint jointToPosition)
        {

        }

        private void weightLabel_Click(object sender, EventArgs e)
        {

        }

        private void GraphingPower(double time, double powerValue)
        {
            if (PowerGraph.IsHandleCreated)
            {
                workArray[workArray.Length - 1] = powerValue;
                Array.Copy(workArray, 1, workArray, 0, workArray.Length - 1);
                timeArray[timeArray.Length - 1] = Math.Round(time, 2);
                Array.Copy(timeArray, 1, timeArray, 0, timeArray.Length - 1);

                PowerGraph.Series["PowerData"].Points.Clear();
                for (int i = 0; i < workArray.Count() - 1; i++)
                {
                    PowerGraph.Series["PowerData"].Points.AddXY(timeArray[i], workArray[i]);
                }
            }
        }

        private void IncreaseAngleButton_Click(object sender, EventArgs e)
        {
            _sensor.ElevationAngle++;
            CurrentAngleTextbox.Text = _sensor.ElevationAngle.ToString();
        }

        private void DecreaseAngleButton_Click(object sender, EventArgs e)
        {
            _sensor.ElevationAngle--;
            CurrentAngleTextbox.Text = _sensor.ElevationAngle.ToString();
        }
    }
}
