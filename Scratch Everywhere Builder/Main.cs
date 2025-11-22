using Scratch_Everywhere_Builder.Sebx;

namespace Scratch_Everywhere_Builder
{
    public partial class Main : Form
    {
        private int childFormNumber = 0;
        public bool FileLoaded = false;
        public bool Saved = true;
        public SebxProject sebxproject;
        public string sebxpath;
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
                sebxpath = FileName;
                FileLoaded = true;
                CheckFileLoaded(sender, e);
                UpdatePreviewImage();
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "SE! Builder Projects (*.sebx)|*.sebx|All Files (*.*)|*.*";
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
        private void UpdatePreviewImage()
        {
            // check if icon and banner files exist
            if (!File.Exists(sebxproject.BannerFile.FullName))
            {
                bannerPictureBox.Image = Resources.SE__Builder_banner;
            }
            else if (!File.Exists(sebxproject.IconFile.FullName))
            {
                // access image from resources
                bannerPictureBox.Image = Resources.SE__Builder_icon;
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
            UpdatePreviewImage();
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


        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Form createwizard = new NewProject.NewProject();
            createwizard.ShowDialog();
            if (((NewProject.NewProject)createwizard).ProjectCreated)
            {
                sebxproject = ((NewProject.NewProject)createwizard).sebxproject;
                FileLoaded = true;
                CheckFileLoaded(sender, e);
                Saved = false;
                UpdatePreviewImage();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SebxProjectIO.Save(sebxproject, sebxpath);
            Saved = true;
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {

        }
    }
}

