using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinectJam
{
    public static class Program
    {
        public static SelectionType _selection = SelectionType.Unknown;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Selection());
            KinectDisplay kinectDisplay = new KinectDisplay();
            kinectDisplay._selection = _selection;
            Application.Run(kinectDisplay);
        }

        public enum SelectionType
        {
            Unknown,
            RightArm,
            LeftArm
        }

    }
}
