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
            this.AutoSplitBox = new System.Windows.Forms.CheckBox();
            this.AutoMidBox = new System.Windows.Forms.CheckBox();
            this.AutoResetBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // AutoStartBox
            // 
            this.AutoStartBox.AutoSize = true;
            this.AutoStartBox.Checked = true;
            this.AutoStartBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoStartBox.Location = new System.Drawing.Point(3, 3);
            this.AutoStartBox.Name = "AutoStartBox";
            this.AutoStartBox.Size = new System.Drawing.Size(148, 17);
            this.AutoStartBox.TabIndex = 0;
            this.AutoStartBox.Text = "Start Timer on New Game";
            this.AutoStartBox.UseVisualStyleBackColor = true;
            this.AutoStartBox.CheckedChanged += new System.EventHandler(this.AutoStartBox_CheckedChanged);
            // 
            // AutoEndBox
            // 
            this.AutoEndBox.AutoSize = true;
            this.AutoEndBox.Checked = true;
            this.AutoEndBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoEndBox.Location = new System.Drawing.Point(3, 26);
            this.AutoEndBox.Name = "AutoEndBox";
            this.AutoEndBox.Size = new System.Drawing.Size(162, 17);
            this.AutoEndBox.TabIndex = 1;
            this.AutoEndBox.Text = "End Timer on Final Cutscene";
            this.AutoEndBox.UseVisualStyleBackColor = true;
            this.AutoEndBox.CheckedChanged += new System.EventHandler(this.AutoEndBox_CheckedChanged);
            // 
            // AutoSplitBox
            // 
            this.AutoSplitBox.AutoSize = true;
            this.AutoSplitBox.Checked = true;
            this.AutoSplitBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoSplitBox.Location = new System.Drawing.Point(3, 49);
            this.AutoSplitBox.Name = "AutoSplitBox";
            this.AutoSplitBox.Size = new System.Drawing.Size(156, 17);
            this.AutoSplitBox.TabIndex = 3;
            this.AutoSplitBox.Text = "Split on Memory Completion";
            this.AutoSplitBox.UseVisualStyleBackColor = true;
            this.AutoSplitBox.CheckedChanged += new System.EventHandler(this.AutoSplitBox_CheckedChanged);
            // 
            // AutoMidBox
            // 
            this.AutoMidBox.AutoSize = true;
            this.AutoMidBox.Checked = true;
            this.AutoMidBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoMidBox.Location = new System.Drawing.Point(3, 72);
            this.AutoMidBox.Name = "AutoMidBox";
            this.AutoMidBox.Size = new System.Drawing.Size(134, 17);
            this.AutoMidBox.TabIndex = 2;
            this.AutoMidBox.Text = "Split on Map Transition";
            this.AutoMidBox.UseVisualStyleBackColor = true;
            this.AutoMidBox.CheckedChanged += new System.EventHandler(this.AutoMidBox_CheckedChanged);
            // 
            // AutoResetBox
            // 
            this.AutoResetBox.AutoSize = true;
            this.AutoResetBox.Checked = true;
            this.AutoResetBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoResetBox.Location = new System.Drawing.Point(3, 95);
            this.AutoResetBox.Name = "AutoResetBox";
            this.AutoResetBox.Size = new System.Drawing.Size(154, 17);
            this.AutoResetBox.TabIndex = 3;
            this.AutoResetBox.Text = "Reset Timer on New Game";
            this.AutoResetBox.UseVisualStyleBackColor = true;
            this.AutoResetBox.CheckedChanged += new System.EventHandler(this.AutoResetBox_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AutoResetBox);
            this.Controls.Add(this.AutoMidBox);
            this.Controls.Add(this.AutoSplitBox);
            this.Controls.Add(this.AutoEndBox);
            this.Controls.Add(this.AutoStartBox);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(200, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox AutoStartBox;
        private System.Windows.Forms.CheckBox AutoEndBox;
        private System.Windows.Forms.CheckBox AutoSplitBox;
        private System.Windows.Forms.CheckBox AutoMidBox;
        private System.Windows.Forms.CheckBox AutoResetBox;
    }
}
