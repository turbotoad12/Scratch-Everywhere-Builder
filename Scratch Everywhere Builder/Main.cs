using Scratch_Everywhere_Builder.Sebx;
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
        public bool Saved = true;
        public SebxProject sebxproject;
        public Main(string SebxPath)
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;

            // Hide panel by default until a file is confirmed
            MainPanel.Visible = false;
            MainPanel.Enabled = false;

            if (File.Exists(SebxPath))
            {
                sebxproject = SebxProjectIO.Load(SebxPath);
                FileLoaded = true;
                CheckFileLoaded(null, null);
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
            openFileDialog.Filter = "Scratch Everywhere Builder Projects (*.sebx)|*.sebx|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                sebxproject = SebxProjectIO.Load(FileName);
                FileLoaded = true;
                CheckFileLoaded(sender, e);
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Scratch Everywhere Builder Projects (*.sebx)|*.sebx|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                SebxProjectIO.Save(sebxproject, FileName);
                Saved = true;
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
                // Do NOT show the warning anymore
                MainPanel.Visible = false;
                MainPanel.Enabled = false;
            }
            else
            {
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
        
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void AnyTextBox_TextChanged(object sender, EventArgs e)
        {
            Saved = false;
        }
        // Build function
        private void buildToolStripButton1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Saved)
            {
                var result = MessageBox.Show(
                    "Save changes before exiting?",
                    "Save File?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true; // don't close
                }
                else if (result == DialogResult.Yes)
                {
                    // Call save function here
                }
                // No = close without saving
            }
        }

    }
}
