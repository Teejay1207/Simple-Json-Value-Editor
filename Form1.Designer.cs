namespace Simple_Json_Value_Editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewJson;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox chkValue;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            byTeejayMerksToolStripMenuItem = new ToolStripMenuItem();
            splitContainer1 = new SplitContainer();
            treeViewJson = new TreeView();
            btnApply = new Button();
            lblValue = new Label();
            lblPath = new Label();
            txtValue = new TextBox();
            chkValue = new CheckBox();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.FromArgb(32, 32, 32);
            menuStrip1.ForeColor = Color.LightGray;
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(505, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.FromArgb(32, 32, 32);
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, saveAsToolStripMenuItem, byTeejayMerksToolStripMenuItem });
            fileToolStripMenuItem.ForeColor = Color.LightGray;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.BackColor = Color.FromArgb(45, 45, 48);
            openToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(180, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.BackColor = Color.FromArgb(45, 45, 48);
            saveToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(180, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.BackColor = Color.FromArgb(45, 45, 48);
            saveAsToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Size = new Size(180, 22);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // byTeejayMerksToolStripMenuItem
            // 
            byTeejayMerksToolStripMenuItem.BackColor = Color.FromArgb(45, 45, 48);
            byTeejayMerksToolStripMenuItem.ForeColor = Color.WhiteSmoke;
            byTeejayMerksToolStripMenuItem.Name = "byTeejayMerksToolStripMenuItem";
            byTeejayMerksToolStripMenuItem.Size = new Size(180, 22);
            byTeejayMerksToolStripMenuItem.Text = "By: TeejayMerks";
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.FromArgb(37, 37, 38);
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.FromArgb(37, 37, 38);
            splitContainer1.Panel1.Controls.Add(treeViewJson);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.BackColor = Color.FromArgb(37, 37, 38);
            splitContainer1.Panel2.Controls.Add(btnApply);
            splitContainer1.Panel2.Controls.Add(lblValue);
            splitContainer1.Panel2.Controls.Add(lblPath);
            splitContainer1.Panel2.Controls.Add(txtValue);
            splitContainer1.Panel2.Controls.Add(chkValue);
            splitContainer1.Size = new Size(505, 378);
            splitContainer1.SplitterDistance = 221;
            splitContainer1.TabIndex = 1;
            // 
            // treeViewJson
            // 
            treeViewJson.BackColor = Color.FromArgb(30, 30, 30);
            treeViewJson.Dock = DockStyle.Fill;
            treeViewJson.ForeColor = Color.LightGray;
            treeViewJson.HideSelection = false;
            treeViewJson.LineColor = Color.FromArgb(60, 60, 60);
            treeViewJson.Location = new Point(0, 0);
            treeViewJson.Name = "treeViewJson";
            treeViewJson.Size = new Size(221, 378);
            treeViewJson.TabIndex = 0;
            treeViewJson.AfterSelect += treeViewJson_AfterSelect;
            // 
            // btnApply
            // 
            btnApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnApply.BackColor = Color.FromArgb(45, 45, 48);
            btnApply.FlatAppearance.BorderColor = Color.FromArgb(70, 70, 70);
            btnApply.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
            btnApply.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 55, 55);
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.ForeColor = Color.WhiteSmoke;
            btnApply.Location = new Point(174, 339);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(94, 27);
            btnApply.TabIndex = 3;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Click += btnApply_Click;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.ForeColor = Color.LightGray;
            lblValue.Location = new Point(12, 35);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(38, 15);
            lblValue.TabIndex = 1;
            lblValue.Text = "Value:";
            // 
            // lblPath
            // 
            lblPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPath.BackColor = Color.Transparent;
            lblPath.ForeColor = Color.LightGray;
            lblPath.Location = new Point(12, 9);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(258, 23);
            lblPath.TabIndex = 0;
            lblPath.Text = "Select a value node on the left.";
            // 
            // txtValue
            // 
            txtValue.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValue.BackColor = Color.FromArgb(25, 25, 25);
            txtValue.Cursor = Cursors.IBeam;
            txtValue.ForeColor = Color.WhiteSmoke;
            txtValue.Location = new Point(10, 53);
            txtValue.Multiline = true;
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(260, 28);
            txtValue.TabIndex = 3;
            txtValue.WordWrap = false;
            // 
            // chkValue
            // 
            chkValue.BackColor = Color.FromArgb(25, 25, 25);
            chkValue.Cursor = Cursors.Hand;
            chkValue.FlatStyle = FlatStyle.System;
            chkValue.ForeColor = Color.WhiteSmoke;
            chkValue.Location = new Point(10, 53);
            chkValue.Name = "chkValue";
            chkValue.Size = new Size(260, 28);
            chkValue.TabIndex = 2;
            chkValue.UseVisualStyleBackColor = false;
            chkValue.Visible = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog1.Title = "Open JSON file";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog1.Title = "Save JSON file as";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 37, 38);
            ClientSize = new Size(505, 402);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            ForeColor = Color.LightGray;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Simple Json Value Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private ToolStripMenuItem byTeejayMerksToolStripMenuItem;
    }
}
