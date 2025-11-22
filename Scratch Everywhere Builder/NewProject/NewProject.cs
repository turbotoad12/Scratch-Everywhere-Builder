using Scratch_Everywhere_Builder.Sebx;

namespace Scratch_Everywhere_Builder.NewProject
{

    public partial class NewProject : Form
    {
        public bool ProjectCreated = false;
        public SebxProject sebxproject;
        public NewProject()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NewProject_Load(object sender, EventArgs e)
        {

        }


        private void browseButton_Click(object sender, EventArgs e)
        {
            Dialogs.FolderDialog dialog = new();
            ProjectPathBox.Text = dialog.ShowDialog().FullName;
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            // First, validate inputs
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter a project name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(ProjectPathBox.Text))
            {
                MessageBox.Show("Please select a project path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Please enter a project description.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //
            // Create project logic
            //
            // Create folder structure
            Directory.CreateDirectory(ProjectPathBox.Text);
            Directory.CreateDirectory(Path.Combine(ProjectPathBox.Text, "Assets"));
            Directory.CreateDirectory(Path.Combine(ProjectPathBox.Text, "Code"));
            // Copy default assets
            File.Copy(Utils.DefaultIcon, Path.Combine(ProjectPathBox.Text, "Assets", "icon.png"));
            File.Copy(Utils.DefaultBanner, Path.Combine(ProjectPathBox.Text, "Assets", "banner.png"));
            File.Copy(Utils.DefaultProject, Path.Combine(ProjectPathBox.Text, "Code", "project.sb3"));

            SebxProject sebxProject = new SebxProject
            {
                ProjectName = textBox2.Text,
                ProjectDescription = richTextBox1.Text,
                IconFile = new FileInfo("Assets\\icon.png"),
                BannerFile = new FileInfo("Assets\\banner.png"),
                Sb3Folder = new DirectoryInfo(Path.Combine(ProjectPathBox.Text, "Code")),
                TargetVersion = new Version.VersionInfo()
            };

            ProjectCreated = true;
            sebxproject = sebxProject;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelbutton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
