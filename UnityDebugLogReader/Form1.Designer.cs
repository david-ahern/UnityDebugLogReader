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
            this.Output = new System.Windows.Forms.RichTextBox();
            this.OpenStreamButton = new System.Windows.Forms.Button();
            this.CloseStreamButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Output
            // 
            this.Output.BackColor = System.Drawing.SystemColors.Control;
            this.Output.Location = new System.Drawing.Point(12, 12);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(691, 528);
            this.Output.TabIndex = 0;
            this.Output.Text = "";
            // 
            // OpenStreamButton
            // 
            this.OpenStreamButton.Location = new System.Drawing.Point(505, 558);
            this.OpenStreamButton.Name = "OpenStreamButton";
            this.OpenStreamButton.Size = new System.Drawing.Size(75, 23);
            this.OpenStreamButton.TabIndex = 1;
            this.OpenStreamButton.Text = "OpenStream";
            this.OpenStreamButton.UseVisualStyleBackColor = true;
            this.OpenStreamButton.Click += new System.EventHandler(this.OpenStreamButton_Click);
            // 
            // CloseStreamButton
            // 
            this.CloseStreamButton.Location = new System.Drawing.Point(603, 558);
            this.CloseStreamButton.Name = "CloseStreamButton";
            this.CloseStreamButton.Size = new System.Drawing.Size(75, 23);
            this.CloseStreamButton.TabIndex = 2;
            this.CloseStreamButton.Text = "CloseStream";
            this.CloseStreamButton.UseVisualStyleBackColor = true;
            this.CloseStreamButton.Click += new System.EventHandler(this.CloseStreamButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(715, 593);
            this.Controls.Add(this.CloseStreamButton);
            this.Controls.Add(this.OpenStreamButton);
            this.Controls.Add(this.Output);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Output;
        private System.Windows.Forms.Button OpenStreamButton;
        private System.Windows.Forms.Button CloseStreamButton;
    }
}

