namespace OOP_Cashup
{
    partial class frmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.tabPageChangeLog = new System.Windows.Forms.TabPage();
            this.tabPageFAQ = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageLisence = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rtxtbChangelog = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.tabPageChangeLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageAbout);
            this.tabControl1.Controls.Add(this.tabPageChangeLog);
            this.tabControl1.Controls.Add(this.tabPageFAQ);
            this.tabControl1.Controls.Add(this.tabPageLisence);
            this.tabControl1.Location = new System.Drawing.Point(1, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 447);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.richTextBox1);
            this.tabPageAbout.Controls.Add(this.label2);
            this.tabPageAbout.Controls.Add(this.label1);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(792, 421);
            this.tabPageAbout.TabIndex = 0;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // tabPageChangeLog
            // 
            this.tabPageChangeLog.Controls.Add(this.rtxtbChangelog);
            this.tabPageChangeLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageChangeLog.Name = "tabPageChangeLog";
            this.tabPageChangeLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageChangeLog.Size = new System.Drawing.Size(792, 421);
            this.tabPageChangeLog.TabIndex = 1;
            this.tabPageChangeLog.Text = "Change Log";
            this.tabPageChangeLog.UseVisualStyleBackColor = true;
            // 
            // tabPageFAQ
            // 
            this.tabPageFAQ.Location = new System.Drawing.Point(4, 22);
            this.tabPageFAQ.Name = "tabPageFAQ";
            this.tabPageFAQ.Size = new System.Drawing.Size(792, 421);
            this.tabPageFAQ.TabIndex = 2;
            this.tabPageFAQ.Text = "FAQ";
            this.tabPageFAQ.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "CashUp";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(291, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Authored by: Joshua Smith";
            // 
            // tabPageLisence
            // 
            this.tabPageLisence.Location = new System.Drawing.Point(4, 22);
            this.tabPageLisence.Name = "tabPageLisence";
            this.tabPageLisence.Size = new System.Drawing.Size(792, 421);
            this.tabPageLisence.TabIndex = 3;
            this.tabPageLisence.Text = "Lisence";
            this.tabPageLisence.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 89);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(778, 326);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // rtxtbChangelog
            // 
            this.rtxtbChangelog.Location = new System.Drawing.Point(0, 0);
            this.rtxtbChangelog.Name = "rtxtbChangelog";
            this.rtxtbChangelog.Size = new System.Drawing.Size(792, 421);
            this.rtxtbChangelog.TabIndex = 0;
            this.rtxtbChangelog.Text = "Changelog goes here from file.";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAbout";
            this.Text = "frmAbout";
            this.Load += new System.EventHandler(this.frmAbout_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.tabPageChangeLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageAbout;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageChangeLog;
        private System.Windows.Forms.TabPage tabPageFAQ;
        private System.Windows.Forms.TabPage tabPageLisence;
        private System.Windows.Forms.RichTextBox rtxtbChangelog;
    }
}