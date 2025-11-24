namespace Scratch_Everywhere_Builder.NewProject
{
    partial class NewProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProject));
            ProjectPathBox = new TextBox();
            browseButton = new Button();
            label1 = new Label();
            cancelbutton = new Button();
            createButton = new Button();
            label2 = new Label();
            textBox2 = new TextBox();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // ProjectPathBox
            // 
            ProjectPathBox.Location = new Point(12, 70);
            ProjectPathBox.Name = "ProjectPathBox";
            ProjectPathBox.Size = new Size(350, 27);
            ProjectPathBox.TabIndex = 0;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(368, 68);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(94, 29);
            browseButton.TabIndex = 1;
            browseButton.Text = "Browse";
            browseButton.UseVisualStyleBackColor = true;
            browseButton.Click += browseButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 47);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 2;
            label1.Text = "Project Path";
            label1.Click += label1_Click;
            // 
            // cancelbutton
            // 
            cancelbutton.Location = new Point(368, 287);
            cancelbutton.Name = "cancelbutton";
            cancelbutton.Size = new Size(94, 29);
            cancelbutton.TabIndex = 3;
            cancelbutton.Text = "Cancel";
            cancelbutton.UseVisualStyleBackColor = true;
            cancelbutton.Click += cancelbutton_Click;
            // 
            // createButton
            // 
            createButton.Location = new Point(268, 287);
            createButton.Name = "createButton";
            createButton.Size = new Size(94, 29);
            createButton.TabIndex = 4;
            createButton.Text = "Create";
            createButton.UseVisualStyleBackColor = true;
            createButton.Click += createButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 117);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 5;
            label2.Text = "Project Name";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(117, 114);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(345, 27);
            textBox2.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 148);
            label3.Name = "label3";
            label3.Size = new Size(135, 20);
            label3.TabIndex = 8;
            label3.Text = "Project Description";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 171);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ScrollBars = RichTextBoxScrollBars.None;
            richTextBox1.Size = new Size(450, 47);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "";
            // 
            // NewProject
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 328);
            Controls.Add(richTextBox1);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(createButton);
            Controls.Add(cancelbutton);
            Controls.Add(label1);
            Controls.Add(browseButton);
            Controls.Add(ProjectPathBox);
            Cursor = Cursors.Default;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "NewProject";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create A Project";
            Load += NewProject_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox ProjectPathBox;
        private Button browseButton;
        private Label label1;
        private Button cancelbutton;
        private Button createButton;
        private Label label2;
        private TextBox textBox2;
        private Label label3;
        private RichTextBox richTextBox1;
    }
}