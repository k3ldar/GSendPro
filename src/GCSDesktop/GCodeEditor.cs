using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSendDesktop
{
    public partial class GCodeEditor : Form
    {
        public GCodeEditor()
        {
            InitializeComponent();
        }

        #region Menu Items

        #region File Menu

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                //richTextBox.Text = File.ReadAllText(openFileDialog1.FileName);
                //fastColoredTextBox1.Text = richTextBox.Text;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion File Menu

        #endregion Menu Items
    }
}
