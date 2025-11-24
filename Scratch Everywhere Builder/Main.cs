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
                sebxpath = SebxPath;
                UpdatePreviewImage();
                UpdateversionListBox();
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
                UpdateversionListBox();
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
            // If no project loaded or project object is null, show defaults
            if (!FileLoaded || sebxproject == null)
            {
                bannerPictureBox.Image = Resources.SE__Builder_banner;
                iconPictureBox.Image = Resources.SE__Builder_icon;
                return;
            }

            // Banner image: use project banner if available and exists, otherwise default
            try
            {
                if (sebxproject.BannerFile == null || string.IsNullOrWhiteSpace(sebxproject.BannerFile.FullName) || !File.Exists(sebxproject.BannerFile.FullName))
                {
                    bannerPictureBox.Image = Resources.SE__Builder_banner;
                }
                else
                {
                    using var img = Image.FromFile(sebxproject.BannerFile.FullName);
                    bannerPictureBox.Image = new Bitmap(img);
                }
            }
            catch
            {
                bannerPictureBox.Image = Resources.SE__Builder_banner;
            }

            // Icon image: use project icon if available and exists, otherwise default
            try
            {
                if (sebxproject.IconFile == null || string.IsNullOrWhiteSpace(sebxproject.IconFile.FullName) || !File.Exists(sebxproject.IconFile.FullName))
                {
                    iconPictureBox.Image = Resources.SE__Builder_icon;
                }
                else
                {
                    using var img = Image.FromFile(sebxproject.IconFile.FullName);
                    iconPictureBox.Image = new Bitmap(img);
                }
            }
            catch
            {
                iconPictureBox.Image = Resources.SE__Builder_icon;
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

        public void RefreshUI()
        {
            UpdatePreviewImage();
            UpdateversionListBox();
            maskedTextBox1.Text = sebxproject.ProjectName;
            richTextBox1.Text = sebxproject.ProjectDescription;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            CheckFileLoaded(sender, e);
            UpdatePreviewImage();
            UpdateversionListBox();
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void AnyTextBox_TextChanged(object sender, EventArgs e)
        {
            Saved = false;
        }
        // Build function
        private async void buildToolStripButton1_Click(object sender, EventArgs e)
        {
            // validate state
            if (!Saved)
            {
                var result = MessageBox.Show(
                    "You have unsaved changes. Save before building?",
                    "Save File?",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question
                );
                if (result == DialogResult.Cancel)
                {
                    return; // don't build
                }
                else if (result == DialogResult.Yes)
                {
                    saveToolStripButton_Click(sender, e);
                }
                // No = build without saving
            }
            if (string.IsNullOrWhiteSpace(sebxpath) || sebxproject == null)
            {
                MessageBox.Show("No project loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileInfo sebxfile = new FileInfo(sebxpath);

            Builder builder = new Builder(sebxproject, sebxfile.Directory!);
            try
            {
                await builder.Build(progressbar);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Build failed: {ex.Message}", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            progressbar.Visible = false;
            progressbar.Value = 0;
        }
        public void ShowProgressBar()
        {
            progressbar.Visible = true;
        }
        public void SetStatusText(string text)
        {
            toolStripStatusLabel.Text = $"Status: {text}";
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
            if (!Saved)
            {
                SebxProjectIO.Save(sebxproject, sebxpath);
                Saved = true;
            }
            else if (string.IsNullOrWhiteSpace(sebxpath))
            {
                SaveAsToolStripMenuItem_Click(sender, e);
            }
        }
        private void UpdateversionListBox()
        {
            versionListBox.Items.Clear();
            string[] versions = Version.GetAvailableVersions(true);
            versionListBox.Items.AddRange(versions);
        }

        private void versionListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (versionListBox.SelectedItem != null)
            {
                sebxproject.TargetVersion = Version.Parse(versionListBox.SelectedItem.ToString());
            }
        }

        private void downloadnewversionsbutton_Click(object sender, EventArgs e)
        {
            // open new form to download new versions
            Form downloadform = new DownloadVersion.DownloadVersion();
            downloadform.ShowDialog();
            UpdateversionListBox();
        }
    }
}

