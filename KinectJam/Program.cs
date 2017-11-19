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
            Application.Run(new KinectDisplay());
        }

        public enum SelectionType
        {
            Unknown,
            RightArm,
            LeftArm
        }

    }
}
