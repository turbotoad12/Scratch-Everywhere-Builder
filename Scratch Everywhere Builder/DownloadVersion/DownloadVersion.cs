using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Scratch_Everywhere_Builder;

namespace Scratch_Everywhere_Builder.DownloadVersion
{
    public partial class DownloadVersion : Form
    {
        public DownloadVersion()
        {
            InitializeComponent();
        }

        private async void DownloadVersion_Load(object sender, EventArgs e)
        {
            try
            {
                Version.VersionInfo[] versions = await Version.GetVersionsFromGitHubAsync();
                foreach (Version.VersionInfo version in versions)
                {
                    listBox1.Items.Add(version.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to retrieve versions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a version to download.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string selected = listBox1.SelectedItem.ToString();
                var versionInfo = Version.Parse(selected!);
                await Version.InstallVersionFromGitHub(versionInfo, progressBar1);
                MessageBox.Show($"Version {versionInfo} installed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to install version: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
