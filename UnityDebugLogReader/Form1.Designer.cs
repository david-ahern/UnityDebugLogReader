namespace UnityDebugLogReader
{
    partial class Form1
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
            this.OutputTextbox = new System.Windows.Forms.RichTextBox();
            this.OpenStreamButton = new System.Windows.Forms.Button();
            this.CloseStreamButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearButton = new System.Windows.Forms.Button();
            this.LoadAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OutputTextbox
            // 
            this.OutputTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTextbox.BackColor = System.Drawing.SystemColors.Control;
            this.OutputTextbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.OutputTextbox.Location = new System.Drawing.Point(15, 25);
            this.OutputTextbox.Name = "OutputTextbox";
            this.OutputTextbox.ReadOnly = true;
            this.OutputTextbox.Size = new System.Drawing.Size(688, 515);
            this.OutputTextbox.TabIndex = 0;
            this.OutputTextbox.Text = "";
            // 
            // OpenStreamButton
            // 
            this.OpenStreamButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenStreamButton.Location = new System.Drawing.Point(542, 558);
            this.OpenStreamButton.Name = "OpenStreamButton";
            this.OpenStreamButton.Size = new System.Drawing.Size(80, 23);
            this.OpenStreamButton.TabIndex = 1;
            this.OpenStreamButton.Text = "Start Stream";
            this.OpenStreamButton.UseVisualStyleBackColor = true;
            this.OpenStreamButton.Click += new System.EventHandler(this.OpenStreamButton_Click);
            // 
            // CloseStreamButton
            // 
            this.CloseStreamButton.Location = new System.Drawing.Point(628, 558);
            this.CloseStreamButton.Name = "CloseStreamButton";
            this.CloseStreamButton.Size = new System.Drawing.Size(75, 23);
            this.CloseStreamButton.TabIndex = 2;
            this.CloseStreamButton.Text = "Stop Stream";
            this.CloseStreamButton.UseVisualStyleBackColor = true;
            this.CloseStreamButton.Click += new System.EventHandler(this.CloseStreamButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Stream: ";
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Location = new System.Drawing.Point(219, 9);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(18, 13);
            this.DirectoryLabel.TabIndex = 4;
            this.DirectoryLabel.Text = "dir";
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(55, 9);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(69, 13);
            this.StatusLabel.TabIndex = 5;
            this.StatusLabel.Text = "Streaming. . .";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Status:";
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearButton.Location = new System.Drawing.Point(96, 558);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 7;
            this.ClearButton.Text = "Clear Output";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // LoadAllButton
            // 
            this.LoadAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadAllButton.Location = new System.Drawing.Point(15, 558);
            this.LoadAllButton.Name = "LoadAllButton";
            this.LoadAllButton.Size = new System.Drawing.Size(75, 23);
            this.LoadAllButton.TabIndex = 8;
            this.LoadAllButton.Text = "Reload All";
            this.LoadAllButton.UseVisualStyleBackColor = true;
            this.LoadAllButton.Click += new System.EventHandler(this.LoadAllButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(715, 593);
            this.Controls.Add(this.LoadAllButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.DirectoryLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CloseStreamButton);
            this.Controls.Add(this.OpenStreamButton);
            this.Controls.Add(this.OutputTextbox);
            this.Name = "Form1";
            this.Text = "Unity Debug Log Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox OutputTextbox;
        private System.Windows.Forms.Button OpenStreamButton;
        private System.Windows.Forms.Button CloseStreamButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DirectoryLabel;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button LoadAllButton;
    }
}

