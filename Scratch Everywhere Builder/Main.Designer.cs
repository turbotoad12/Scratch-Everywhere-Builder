namespace Scratch_Everywhere_Builder
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            menuStrip = new MenuStrip();
            fileMenu = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsMenu = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            viewMenu = new ToolStripMenuItem();
            toolBarToolStripMenuItem = new ToolStripMenuItem();
            statusBarToolStripMenuItem = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            toolStrip = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            seperator1 = new ToolStripSeparator();
            buildToolStripButton1 = new ToolStripButton();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolTip = new ToolTip(components);
            MainPanel = new Panel();
            panel1 = new Panel();
            richTextBox1 = new RichTextBox();
            label2 = new Label();
            maskedTextBox1 = new MaskedTextBox();
            label1 = new Label();
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            MainPanel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, toolsMenu, viewMenu, helpMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(8, 3, 0, 3);
            menuStrip.Size = new Size(843, 30);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator3, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            fileMenu.ImageTransparentColor = SystemColors.ActiveBorder;
            fileMenu.Name = "fileMenu";
            fileMenu.Size = new Size(46, 24);
            fileMenu.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Black;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(224, 26);
            newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Black;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(224, 26);
            openToolStripMenuItem.Text = "&Open";
            openToolStripMenuItem.Click += OpenFile;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(221, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Black;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(224, 26);
            saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(224, 26);
            saveAsToolStripMenuItem.Text = "Save &As";
            saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(221, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += ExitToolsStripMenuItem_Click;
            // 
            // toolsMenu
            // 
            toolsMenu.DropDownItems.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            toolsMenu.Name = "toolsMenu";
            toolsMenu.Size = new Size(58, 24);
            toolsMenu.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Image = Resources.Options_16x16;
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(224, 26);
            optionsToolStripMenuItem.Text = "&Options";
            // 
            // viewMenu
            // 
            viewMenu.DropDownItems.AddRange(new ToolStripItem[] { toolBarToolStripMenuItem, statusBarToolStripMenuItem });
            viewMenu.Name = "viewMenu";
            viewMenu.Size = new Size(55, 24);
            viewMenu.Text = "&View";
            // 
            // toolBarToolStripMenuItem
            // 
            toolBarToolStripMenuItem.Checked = true;
            toolBarToolStripMenuItem.CheckOnClick = true;
            toolBarToolStripMenuItem.CheckState = CheckState.Checked;
            toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            toolBarToolStripMenuItem.Size = new Size(224, 26);
            toolBarToolStripMenuItem.Text = "&Toolbar";
            toolBarToolStripMenuItem.Click += ToolBarToolStripMenuItem_Click;
            // 
            // statusBarToolStripMenuItem
            // 
            statusBarToolStripMenuItem.Checked = true;
            statusBarToolStripMenuItem.CheckOnClick = true;
            statusBarToolStripMenuItem.CheckState = CheckState.Checked;
            statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            statusBarToolStripMenuItem.Size = new Size(224, 26);
            statusBarToolStripMenuItem.Text = "&Status Bar";
            statusBarToolStripMenuItem.Click += StatusBarToolStripMenuItem_Click;
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, toolStripSeparator8 });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(55, 24);
            helpMenu.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(224, 26);
            aboutToolStripMenuItem.Text = "&About ... ...";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(221, 6);
            // 
            // toolStrip
            // 
            toolStrip.ImageScalingSize = new Size(20, 20);
            toolStrip.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, seperator1, buildToolStripButton1 });
            toolStrip.Location = new Point(0, 30);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(843, 27);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "ToolStrip";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = (Image)resources.GetObject("newToolStripButton.Image");
            newToolStripButton.ImageTransparentColor = Color.Black;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(29, 24);
            newToolStripButton.Text = "New";
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = (Image)resources.GetObject("openToolStripButton.Image");
            openToolStripButton.ImageTransparentColor = Color.Black;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(29, 24);
            openToolStripButton.Text = "Open";
            openToolStripButton.Click += OpenFile;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = (Image)resources.GetObject("saveToolStripButton.Image");
            saveToolStripButton.ImageTransparentColor = Color.Black;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(29, 24);
            saveToolStripButton.Text = "Save";
            // 
            // seperator1
            // 
            seperator1.Name = "seperator1";
            seperator1.Size = new Size(6, 27);
            // 
            // buildToolStripButton1
            // 
            buildToolStripButton1.Image = Resources.Package_16x16;
            buildToolStripButton1.ImageTransparentColor = Color.Magenta;
            buildToolStripButton1.Name = "buildToolStripButton1";
            buildToolStripButton1.Size = new Size(67, 24);
            buildToolStripButton1.Text = "Build";
            buildToolStripButton1.Click += buildToolStripButton1_Click;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(20, 20);
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 671);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 19, 0);
            statusStrip.Size = new Size(843, 26);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(49, 20);
            toolStripStatusLabel.Text = "Status";
            // 
            // MainPanel
            // 
            MainPanel.BackColor = SystemColors.Control;
            MainPanel.Controls.Add(panel1);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 57);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(843, 614);
            MainPanel.TabIndex = 4;
            MainPanel.Visible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(maskedTextBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(376, 295);
            panel1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(11, 84);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            richTextBox1.Size = new Size(353, 47);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "";
            richTextBox1.TextChanged += AnyTextBox_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 61);
            label2.Name = "label2";
            label2.Size = new Size(135, 20);
            label2.TabIndex = 2;
            label2.Text = "Project Description";
            // 
            // maskedTextBox1
            // 
            maskedTextBox1.Location = new Point(116, 20);
            maskedTextBox1.Name = "maskedTextBox1";
            maskedTextBox1.Size = new Size(248, 27);
            maskedTextBox1.TabIndex = 1;
            maskedTextBox1.MaskInputRejected += maskedTextBox1_MaskInputRejected;
            maskedTextBox1.TextChanged += AnyTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 23);
            label1.Name = "label1";
            label1.Size = new Size(99, 20);
            label1.TabIndex = 0;
            label1.Text = "Project Name";
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(843, 697);
            Controls.Add(MainPanel);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(5);
            Name = "Main";
            Text = "Scratch Everywhere Builder";
            Load += Main_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            MainPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private Panel MainPanel;
        private Panel panel1;
        private MaskedTextBox maskedTextBox1;
        private RichTextBox richTextBox1;
        private Label label2;
        private Label label1;
        private ToolStripSeparator seperator1;
        private ToolStripButton buildToolStripButton1;
    }
}



