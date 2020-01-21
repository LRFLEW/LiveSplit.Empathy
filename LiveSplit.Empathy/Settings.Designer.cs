namespace LiveSplit.Empathy
{
    partial class Settings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AutoStartBox = new System.Windows.Forms.CheckBox();
            this.AutoEndBox = new System.Windows.Forms.CheckBox();
            this.AutoMidBox = new System.Windows.Forms.CheckBox();
            this.AutoResetBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // AutoStartBox
            // 
            this.AutoStartBox.AutoSize = true;
            this.AutoStartBox.Checked = true;
            this.AutoStartBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoStartBox.Location = new System.Drawing.Point(4, 4);
            this.AutoStartBox.Name = "AutoStartBox";
            this.AutoStartBox.Size = new System.Drawing.Size(73, 17);
            this.AutoStartBox.TabIndex = 0;
            this.AutoStartBox.Text = "Auto Start";
            this.AutoStartBox.UseVisualStyleBackColor = true;
            this.AutoStartBox.CheckedChanged += new System.EventHandler(this.AutoStartBox_CheckedChanged);
            // 
            // AutoEndBox
            // 
            this.AutoEndBox.AutoSize = true;
            this.AutoEndBox.Checked = true;
            this.AutoEndBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoEndBox.Location = new System.Drawing.Point(4, 28);
            this.AutoEndBox.Name = "AutoEndBox";
            this.AutoEndBox.Size = new System.Drawing.Size(70, 17);
            this.AutoEndBox.TabIndex = 1;
            this.AutoEndBox.Text = "Auto End";
            this.AutoEndBox.UseVisualStyleBackColor = true;
            this.AutoEndBox.CheckedChanged += new System.EventHandler(this.AutoEndBox_CheckedChanged);
            // 
            // AutoMidBox
            // 
            this.AutoMidBox.AutoSize = true;
            this.AutoMidBox.Checked = true;
            this.AutoMidBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoMidBox.Location = new System.Drawing.Point(4, 52);
            this.AutoMidBox.Name = "AutoMidBox";
            this.AutoMidBox.Size = new System.Drawing.Size(82, 17);
            this.AutoMidBox.TabIndex = 2;
            this.AutoMidBox.Text = "Auto Middle";
            this.AutoMidBox.UseVisualStyleBackColor = true;
            this.AutoMidBox.CheckedChanged += new System.EventHandler(this.AutoMidBox_CheckedChanged);
            // 
            // AutoResetBox
            // 
            this.AutoResetBox.AutoSize = true;
            this.AutoResetBox.Location = new System.Drawing.Point(4, 75);
            this.AutoResetBox.Name = "AutoResetBox";
            this.AutoResetBox.Size = new System.Drawing.Size(100, 17);
            this.AutoResetBox.TabIndex = 3;
            this.AutoResetBox.Text = "Restart on Start";
            this.AutoResetBox.UseVisualStyleBackColor = true;
            this.AutoResetBox.CheckedChanged += new System.EventHandler(this.AutoResetBox_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AutoResetBox);
            this.Controls.Add(this.AutoMidBox);
            this.Controls.Add(this.AutoEndBox);
            this.Controls.Add(this.AutoStartBox);
            this.Name = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AutoStartBox;
        private System.Windows.Forms.CheckBox AutoEndBox;
        private System.Windows.Forms.CheckBox AutoMidBox;
        private System.Windows.Forms.CheckBox AutoResetBox;
    }
}
