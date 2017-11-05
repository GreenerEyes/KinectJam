using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KinectJam.Program;

namespace KinectJam
{
    public partial class Selection : Form
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectionType selection = SelectionType.Unknown;
            if (radioButton1.Checked)
                selection = SelectionType.RightArm;
            if (radioButton2.Checked)
                selection = SelectionType.LeftArm;
            _selection = selection;
            this.Close();
        }
    }
}
