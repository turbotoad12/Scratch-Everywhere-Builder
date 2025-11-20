using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Scratch_Everywhere_Builder
{
    public partial class Main : Form
    {
        private int childFormNumber = 0;
        public bool FileLoaded = false;
        public string CurrentSebxPath = "";
        public Main(string SebxPath)
        {
            InitializeComponent();

            // Accept if the path points to an existing file, or the special token "none" (case-insensitive).
            if (File.Exists(SebxPath) || string.Equals(SebxPath, "none", StringComparison.OrdinalIgnoreCase))
            {
                CurrentSebxPath = SebxPath;
                FileLoaded = true;
            }
        }

        //private void ShowNewForm(object sender, EventArgs e)
        //{
        //    Form childForm = new Form();
        //    childForm.MdiParent = this;
        //    childForm.Text = "Window " + childFormNumber++;
        //    childForm.Show();
        //}

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckFileLoaded(object sender, EventArgs e)
        {
            if (!FileLoaded)
            {
                MessageBox.Show("No file loaded. Please load a .sebx file to proceed.", "File Not Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MainPanel.Visible = false;
                MainPanel.Enabled = false;
            } else {
                MainPanel.Visible = true;
                MainPanel.Enabled = true;
            }
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CheckFileLoaded(sender, e);
        }
    }
}
