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
using System.IO;

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
        private const int _maxAngleSize = 180;

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

        private Joint _elbowFinal = new Joint();
        private Joint _elbowInitial = new Joint();

        private Joint _initialShoulderPointLeft = new Joint();
        private Joint _initialElbowPointLeft = new Joint();
        private Joint _initialWristPointLeft = new Joint();
        private Joint _wristFinalLeft = new Joint();
        private Joint _wristInitialLeft = new Joint();
        private Joint _elbowFinalLeft = new Joint();
        private Joint _elbowInitialLeft = new Joint();

        private double _initialAngle = 0;
        private double _finalAngle = 0;
        private double _initialAngleLeft = 0;
        private double _finalAngleLeft = 0;

        private double _totalDistance = 0;
        private double _totalWork = 0;
        private double _totalFilteredWork = 0;

        private double _angularVelocity = 0;
        private double _angularVelocityLeft = 0;

        private double _relativeVelocity = 0;
        private double _relativeVelocityLeft = 0;

        private double _totalInternalWork = 0;
        private double _previousFilteredInternalWork = 0;

        private double _armLengthCalculated = 0;

        private double _totalTime = 0;

        public SelectionType _selection;
        public int frame = 0;

        private double _alpha = 0.98;
        private double _alphaFrequency = 0.98;
        private double _previousFilteredWork = 0;
        private double _previousFilteredAngle = 0;
        private double _previousAngle = 0;

        private double[] timeArray = new double[50];
        private double[] workArray = new double[50];
        private double[] goalArray = new double[50];

        private double[] angleArray = new double[50];
        private double[] filteredAngleArray = new double[50];
        private double[] angleArrayLeft = new double[50];

        private double _goalLevel = 300;

        private bool sliderMouseDown = false;
        private bool sliderScrolling = false;

        private bool _paused = false;

        List<double> angleList = new List<double>();
        List<double> crossList = new List<double>();
        List<double> timeScale = new List<double>();
        List<double> amplitudeOfCrossList = new List<double>();
        List<double> frequencyList = new List<double>();
        List<double> filteredFrequencyList = new List<double>();

        private double _initialCrossPoint = 0;
        private double _finalCrossPoint = 0;
        private double _filteredFrequency = 0;
        private double _previouslyFilteredFrequency = 0;

        public KinectDisplay()
        {
            InitializeComponent();
            double initialTime = -50.0 * (1.0 / 30.0);
            //timeArray.Add(initialTime);
            for (int i = 0; i < 50; i++)
            {
                angleArray[i] = 0;

                workArray[i] = 0;
                if (i == 49)
                    timeArray[i] = 0;
                else
                    timeArray[i] = Math.Round(initialTime += (1.0 / 30.0), 2);
            }

            PowerGraph.ChartAreas[0].AxisY.Maximum = _maxPowerSize;
            PowerGraph.ChartAreas[0].AxisY.Minimum = 0;

            AngleGraph.ChartAreas[0].AxisY.Maximum = _maxAngleSize;
            AngleGraph.ChartAreas[0].AxisY.Minimum = 0;
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
                AngleSlider.Value = _sensor.ElevationAngle;
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
                                
                                Rectangle exerciseArea = new Rectangle(shoulder.X - 25, shoulder.Y - 110, 275, 220);

                                double elbowAngle = GetAngle(GetDepthPoint(skeleton.Joints[JointType.ShoulderRight]), GetDepthPoint(skeleton.Joints[JointType.ElbowRight]), GetDepthPoint(skeleton.Joints[JointType.WristRight]));
                                double angle = 180.0 - GetAngle(GetDepthPoint(skeleton.Joints[JointType.Spine]), GetDepthPoint(skeleton.Joints[JointType.ShoulderRight]), GetDepthPoint(skeleton.Joints[JointType.ElbowRight]));
                                double angleLeft = 180.0 - GetAngle(GetDepthPoint(skeleton.Joints[JointType.Spine]), GetDepthPoint(skeleton.Joints[JointType.ShoulderLeft]), GetDepthPoint(skeleton.Joints[JointType.ElbowLeft]));

                                bool isInInitialPosition = elbowAngle >= 0 && elbowAngle <= 10;
                                if (isInInitialPosition || _exerciseStarted)
                                {
                                    if (isInInitialPosition && !_exerciseStarted)
                                    {
                                        _initialShoulderPoint = skeleton.Joints[JointType.ShoulderRight];
                                        _initialElbowPoint = skeleton.Joints[JointType.ElbowRight];
                                        _initialWristPoint = skeleton.Joints[JointType.WristRight];

                                        _initialShoulderPointLeft = skeleton.Joints[JointType.ShoulderLeft];
                                        _initialElbowPointLeft = skeleton.Joints[JointType.ElbowLeft];
                                        _initialWristPointLeft = skeleton.Joints[JointType.WristLeft];

                                        _wristFinal = _initialWristPoint;
                                        _wristInitial = _initialWristPoint;

                                        _wristFinalLeft = _initialWristPointLeft;
                                        _wristInitialLeft = _initialWristPointLeft;

                                        _elbowFinal = _initialElbowPoint;
                                        _elbowInitial = _initialElbowPoint;

                                        _elbowFinalLeft = _initialElbowPointLeft;
                                        _elbowInitialLeft = _initialElbowPointLeft;

                                        _initialAngle = angle;
                                        _finalAngle = angle;

                                        _initialAngleLeft = angleLeft;
                                        _finalAngleLeft = angleLeft;

                                        _armLengthCalculated = Distance(skeleton.Joints[JointType.ShoulderRight], skeleton.Joints[JointType.HandRight]);

                                        _exerciseStarted = true;
                                    }

                                    StringBuilder stringBuilder = new StringBuilder();
                                    
                                    angleList.Add(angle);

                                    _finalAngle = 180 - GetAngle(GetDepthPoint(skeleton.Joints[JointType.Spine]), GetDepthPoint(skeleton.Joints[JointType.ShoulderRight]), GetDepthPoint(skeleton.Joints[JointType.ElbowRight]));
                                    _finalAngleLeft = 180 - GetAngle(GetDepthPoint(skeleton.Joints[JointType.Spine]), GetDepthPoint(skeleton.Joints[JointType.ShoulderLeft]), GetDepthPoint(skeleton.Joints[JointType.ElbowLeft]));

                                    _wristFinal = skeleton.Joints[JointType.WristRight];
                                    _elbowFinal = skeleton.Joints[JointType.ElbowRight];

                                    _wristFinalLeft = skeleton.Joints[JointType.WristLeft];
                                    _elbowFinalLeft = skeleton.Joints[JointType.ElbowLeft];

                                    double instantWristDistance = Distance(_wristFinal, _wristInitial);
                                    double instantElbowDistance = Distance(_elbowFinal, _elbowInitial);

                                    double instantDistance = instantWristDistance + instantElbowDistance;

                                    double instantWristDistanceLeft = Distance(_wristFinalLeft, _wristInitialLeft);
                                    double instantElbowDistanceLeft = Distance(_elbowFinalLeft, _elbowInitialLeft);

                                    double instantDistanceLeft = instantWristDistanceLeft + instantElbowDistanceLeft;

                                    // Only takes into account right wrist
                                    _totalDistance += instantDistance;


                                    _totalTime += (1.0 / 30.0);

                                    timeScale.Add(_totalTime);


                                    // Equation for Internal Work Method

                                    _angularVelocity = AngularVelocity(_finalAngle, _initialAngle);
                                    _angularVelocityLeft = AngularVelocity(_finalAngleLeft, _initialAngleLeft);

                                    _relativeVelocity = RelativeVelocity(_elbowFinal, _elbowInitial);
                                    _relativeVelocityLeft = RelativeVelocity(_elbowFinalLeft, _elbowInitialLeft);

                                    double internalWork = InternalWork(_relativeVelocity, _angularVelocity);
                                    double internalWorkLeft = InternalWork(_relativeVelocityLeft, _angularVelocityLeft);

                                    double instantInternalWork = internalWork + internalWorkLeft;

                                    _totalInternalWork += instantInternalWork;

                                    double filteredInternalWork = Filter(_alpha, _previousFilteredInternalWork, instantInternalWork);
                                    double filteredInternalPower = Power(filteredInternalWork);

                                    // Other method

                                    double wristWork = JointWork(_wristFinal, _wristInitial, instantWristDistance);
                                    double elbowWork = ElbowWork(_elbowFinal, _elbowInitial, instantElbowDistance);

                                    double wristWorkLeft = JointWork(_wristFinalLeft, _wristInitialLeft, instantWristDistanceLeft);
                                    double elbowWorkLeft = ElbowWork(_elbowFinalLeft, _elbowInitialLeft, instantElbowDistanceLeft);

                                    double work = wristWork + elbowWork;
                                    double workLeft = wristWorkLeft + elbowWorkLeft;

                                    double instantWork = work + workLeft;

                                    _totalWork += instantWork;
                                    double power = Power(instantWork);

                                    double filteredwork = Filter(_alpha, _previousFilteredWork, instantWork);
                                    _totalFilteredWork += filteredwork;
                                    double filteredpower = Power(filteredwork);

                                    double filteredangle = Filter(_alpha, _previousFilteredAngle, angle);


                                    if (_paused == false)
                                    {
                                        //stringBuilder.AppendLine(string.Format("Acceleration X: {0} (m/s^2)", Math.Round(accelerationX,2)));
                                        //stringBuilder.AppendLine(string.Format("Acceleration Y: {0} (m/s^2)", Math.Round(accelerationY)));
                                        //stringBuilder.AppendLine(string.Format("Force X: {0} (N)", Math.Round(forceX,2)));
                                        //stringBuilder.AppendLine(string.Format("Force Y: {0} (N)", Math.Round(forceY,2)));
                                        //stringBuilder.AppendLine(string.Format("{0}", Math.Round(_totalWork, 0)));
                                        //stringBuilder.AppendLine(string.Format("{0}", Math.Round(power, 0)));

                                        stringBuilder.AppendLine(string.Format("{0}", Math.Round(_totalDistance, 0)));
                                        stringBuilder.AppendLine(string.Format("{0}", Math.Round(_totalFilteredWork, 0)));
                                        stringBuilder.AppendLine(string.Format("{0}", Math.Round(filteredpower, 0)));

                                        //stringBuilderInternalWork.AppendLine(string.Format("{0}", Math.Round(internalWork, 0)));
                                        stringBuilder.AppendLine(string.Format("{0}", Math.Round(_totalInternalWork, 0)));
                                        stringBuilder.AppendLine(string.Format("{0}", Math.Round(filteredInternalPower, 0)));

                                        DistanceWorkTextBox.Text = string.Empty;
                                        DistanceWorkTextBox.Text = stringBuilder.ToString();

                                        StringBuilder stringBuilderInternalWork = new StringBuilder();

                                        stringBuilderInternalWork.AppendLine(string.Format("{0}", Math.Round(_armLengthCalculated, 3)));

                                        TestTextBox.Text = string.Empty;
                                        TestTextBox.Text = stringBuilderInternalWork.ToString();
                                    }

                                    if (_previousAngle > _previousFilteredAngle + 0.5 && angle < filteredangle - 0.5)
                                    {
                                        _finalCrossPoint = _totalTime;
                                        crossList.Add(_totalTime);
                                        amplitudeOfCrossList.Add(angle);
                                        double _crossDifference = _finalCrossPoint - _initialCrossPoint;
                                        double _instantFrequency = 1.0 / (_crossDifference);
                                        frequencyList.Add(_instantFrequency);
                                        
                                        _filteredFrequency = Filter(_alphaFrequency, _previouslyFilteredFrequency, _instantFrequency);
                                        filteredFrequencyList.Add(_filteredFrequency);

                                        StringBuilder stringBuilderFrequency = new StringBuilder();

                                        stringBuilderFrequency.AppendLine(string.Format("{0}", Math.Round(_filteredFrequency, 3)));
                                        FilteredFrequencyTextBox.Text = string.Empty;
                                        FilteredFrequencyTextBox.Text = stringBuilderFrequency.ToString();

                                        _previouslyFilteredFrequency = _filteredFrequency;
                                        _initialCrossPoint = _finalCrossPoint;
                                    }

                                    GraphingPower(_totalTime, Math.Round(filteredpower, 2), _goalLevel);
                                    GraphingAngle(_totalTime, Math.Round(angle, 2), filteredangle, angleLeft);

                                    _wristInitial = _wristFinal;
                                    _elbowInitial = _elbowFinal;

                                    _wristInitialLeft = _wristFinalLeft;
                                    _elbowInitialLeft = _elbowFinalLeft;

                                    _initialAngle = _finalAngle;
                                    _initialAngleLeft = _finalAngleLeft;

                                    _previousFilteredWork = filteredwork;
                                    _previousFilteredAngle = filteredangle;

                                    _previousFilteredInternalWork = filteredInternalWork;

                                    _previousAngle = angle;

                                    if (_exerciseStarted && (elbowAngle >= 80 && elbowAngle <= 100))
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

        private double ForceElbow(double acceleration, bool accountForGravityArm = false)
        {
            double bodyWeightText = 0;
            if (double.TryParse(bodyWeightTextbox.Text, out bodyWeightText))
            {
                double armWeightlb = bodyWeightText * 0.05;
                double armWeight = armWeightlb * 0.4536;
                return accountForGravityArm ? (armWeight * acceleration) - (9.81 * armWeight) : armWeight * acceleration;
            }
            return 0;
        }

        private double JointWork(Joint _jointFinal, Joint _jointInitial, double instantJointDistance)
        {
            double accelerationX = Accel(_jointFinal.Position.X, _jointInitial.Position.X);
            double accelerationY = Accel(_jointFinal.Position.Y, _jointInitial.Position.Y);
            double forceX = Force(accelerationX);
            double forceY = Force(accelerationY, true);

            return Work(TotalForce(forceX, forceY), instantJointDistance);
        }

        private double ElbowWork(Joint _elbowFinalValue, Joint _elbowInitialValue, double instantElbowDistanceValue)
        {
            double accelerationEX = Accel(_elbowFinalValue.Position.X, _elbowInitialValue.Position.X);
            double accelerationEY = Accel(_elbowFinalValue.Position.Y, _elbowInitialValue.Position.Y);
            double forceEX = ForceElbow(accelerationEX);
            double forceEY = ForceElbow(accelerationEY, true);

            return Work(TotalForce(forceEX, forceEY), instantElbowDistanceValue); 
        }

        private double RelativeVelocity(Joint _finalPosition, Joint _initialPosition)
        {
            double changePosition = Distance(_finalPosition, _initialPosition);
            return changePosition / (1.0 / 30.0);

        }

        private double AngularVelocity(double finalAngle, double initialAngle)
        {
            double angleChange = finalAngle - initialAngle;
            return angleChange / (1.0 / 30.0);
        }

        private double RadiusOfGyration()
        {
            double bodyWeightDoub = 0;
            double armLengthDouble = 0;

            if (double.TryParse(bodyWeightTextbox.Text, out bodyWeightDoub) && double.TryParse(armLengthTextbox.Text, out armLengthDouble))
            {
                double Mass = bodyWeightDoub * 0.05 * 0.4536;

                double Length = armLengthDouble * 0.0254;

                double momentOfInertia = (1 / 3) * Mass * Math.Pow(Length, 2);

                return Math.Sqrt(momentOfInertia / Mass);
            }
            return 0;
        }


        private double InternalWork(double relativeVelocity, double angularVelocity)
        {
            double bodyWeightDouble = 0;
            if (double.TryParse(bodyWeightTextbox.Text, out bodyWeightDouble))
            {
                double mass = bodyWeightDouble * 0.05 * 0.4536;

                return (0.5 * mass * Math.Pow(relativeVelocity, 2)) + (0.5 * mass * Math.Pow(RadiusOfGyration(), 2) * Math.Pow(angularVelocity, 2));
            }
            return 0;
        }

        private double Filter(double alpha, double previousFilter, double filter)
        {
            // y(n) = alpha*y(n-1) + (1-alpha)*x(n)
            return alpha *previousFilter + (1.0 - alpha) * filter;
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

            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.Head]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderCenter]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.Spine]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HipCenter]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderRight]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ElbowRight]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.WristRight]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HandRight]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ShoulderLeft]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.ElbowLeft]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.WristLeft]));
            //stringBuilder.AppendLine(PrintJointCoordinates(jointCollection[JointType.HandLeft]));

            //JointCoordinatesTextBox.Text = string.Empty;
            //JointCoordinatesTextBox.Text = stringBuilder.ToString();
        }

        private DepthImagePoint GetDepthPoint(Joint joint)
        {
            return _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, DepthImageFormat.Resolution640x480Fps30);
        }
        
        //private string PrintJointCoordinates(Joint joint)
        //{
        //    StringBuilder stringBuilder = new StringBuilder();
        //    DepthImagePoint depthPoint = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position, DepthImageFormat.Resolution640x480Fps30);
        //    stringBuilder.Append(string.Format("{0} x: {1} y: {2} Depth: {3}", joint.JointType.ToString(), depthPoint.X, depthPoint.Y, depthPoint.Depth));
        //    return stringBuilder.ToString();
        //}

        private void DrawBone(Joint jointFrom, Joint jointTo)
        {
            DepthImagePoint p1 = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(jointFrom.Position, DepthImageFormat.Resolution640x480Fps30);
            DepthImagePoint p2 = _sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(jointTo.Position, DepthImageFormat.Resolution640x480Fps30);
            if (!video.IsDisposed)
                _graphics.DrawLine(_penWhite, p1.X, p1.Y, p2.X, p2.Y);
        }

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

        private void GraphingPower(double time, double powerValue, double goalLevel)
        {
            if (PowerGraph.IsHandleCreated && _paused == false)
            {
                workArray[workArray.Length - 1] = powerValue;
                Array.Copy(workArray, 1, workArray, 0, workArray.Length - 1);
                timeArray[timeArray.Length - 1] = Math.Round(time, 2);
                Array.Copy(timeArray, 1, timeArray, 0, timeArray.Length - 1);

                goalArray[goalArray.Length - 1] = goalLevel;
                Array.Copy(goalArray, 1, goalArray, 0, goalArray.Length - 1);

                PowerGraph.Series["PowerData"].Points.Clear();
                for (int i = 0; i < workArray.Count() - 1; i++)
                {
                    PowerGraph.Series["PowerData"].Points.AddXY(timeArray[i], workArray[i]);
                }

                PowerGraph.Series["GoalLevel"].Points.Clear();
                for (int i = 0; i < goalArray.Count() - 1; i++)
                {
                    PowerGraph.Series["GoalLevel"].Points.AddXY(timeArray[i], goalArray[i]);
                }
            }
        }

        private void GraphingAngle(double time, double angleValue, double filteredValue, double angleValueLeft)
        {
            if (AngleGraph.IsHandleCreated && _paused == false)
            {
                angleArray[angleArray.Length - 1] = angleValue;
                Array.Copy(angleArray, 1, angleArray, 0, angleArray.Length - 1);
                timeArray[timeArray.Length - 1] = Math.Round(time, 2);
                Array.Copy(timeArray, 1, timeArray, 0, timeArray.Length - 1);

                filteredAngleArray[filteredAngleArray.Length - 1] = filteredValue;
                Array.Copy(filteredAngleArray, 1, filteredAngleArray, 0, filteredAngleArray.Length - 1);

                angleArrayLeft[angleArrayLeft.Length - 1] = angleValueLeft;
                Array.Copy(angleArrayLeft, 1, angleArrayLeft, 0, angleArrayLeft.Length - 1);

                AngleGraph.Series["AngleR"].Points.Clear();
                for (int i = 0; i < angleArray.Count() - 1; i++)
                {
                    AngleGraph.Series["AngleR"].Points.AddXY(timeArray[i], angleArray[i]);
                }

                AngleGraph.Series["FilteredAR"].Points.Clear();
                for (int i = 0; i < filteredAngleArray.Count() - 1; i++)
                {
                    AngleGraph.Series["FilteredAR"].Points.AddXY(timeArray[i], filteredAngleArray[i]);
                }

                AngleGraph.Series["AngleLeft"].Points.Clear();
                for (int i = 0; i < angleArrayLeft.Count() - 1; i++)
                {
                    AngleGraph.Series["AngleLeft"].Points.AddXY(timeArray[i], angleArrayLeft[i]);
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

        private void AngleSlider_Scroll(object sender, EventArgs e)
        {
            sliderScrolling = true;
        }

        private void AngleSlider_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (sliderMouseDown == true && sliderScrolling == true)
            {
                _sensor.ElevationAngle = AngleSlider.Value;
                CurrentAngleTextbox.Text = _sensor.ElevationAngle.ToString();
                sliderScrolling = false;
                sliderMouseDown = false;
            }
        }

        private void AngleSlider_MouseDown(object sender, MouseEventArgs e)
        {
            sliderMouseDown = true;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            _paused = true;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            _paused = false;
        }

        private void RecordButton_Click(object sender, EventArgs e)
        {
            string folderName = @"C:\TestData";
            System.IO.Directory.CreateDirectory(folderName);

            string fileNameAngle = "TestFile.csv";
            string pathString = System.IO.Path.Combine(folderName, fileNameAngle);
            //string filePath = @"C:\Test.csv";

            var angleAndTime = angleList.Zip(timeScale, (a, t) => new { Angle = a, Time = t });
           
            //var angleWithCross = angleAndTime.Zip(crossPoint, (s, c) => new { Signal = s, Cross = c });

            using (StreamWriter writer = new StreamWriter(pathString))
                foreach (var value in angleAndTime)
                {
                    writer.WriteLine(value.Time + "," + value.Angle);
                }

            string fileNameTime = "TestFileFrequency.csv";
            string secondPathString = System.IO.Path.Combine(folderName, fileNameTime);
            var crossPoint = crossList.Zip(amplitudeOfCrossList, (ct, ca) => new { crossTime = ct, crossAmplitude = ca });
            var frequencies = frequencyList.Zip(filteredFrequencyList, (f, ff) => new { IFrequency = f, FFrequency = ff });
            var armFrequency = crossPoint.Zip(frequencies, (lpf, f) => new { LowPassFilter = lpf, Frequency = f });

            using (StreamWriter timewriter = new StreamWriter(secondPathString))
                foreach (var item in armFrequency)
                {
                    timewriter.WriteLine(item.LowPassFilter.crossTime + "," + item.LowPassFilter.crossAmplitude + "," + item.Frequency.IFrequency + "," + item.Frequency.FFrequency);
                }

            //string fileNameFrequency = "TestFileFrequency.csv";
            //string thirdPathString = System.IO.Path.Combine(folderName, fileNameFrequency);

            //using (StreamWriter freqwriter = new StreamWriter(thirdPathString))
            //    foreach (var item in frequencyList)
            //    {
            //        freqwriter.WriteLine(item);
            //    }
        }
    }
}
