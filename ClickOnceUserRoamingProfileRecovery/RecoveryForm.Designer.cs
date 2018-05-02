namespace ClickOnceRecovery
{
    partial class RecoveryForm
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
            this.lblProcessElement = new System.Windows.Forms.Label();
            this.prbElementMain = new System.Windows.Forms.ProgressBar();
            this.lblProgessTitle = new System.Windows.Forms.Label();
            this.lblProcessFolder = new System.Windows.Forms.Label();
            this.prbFolderMain = new System.Windows.Forms.ProgressBar();
            this.lblProcessFile = new System.Windows.Forms.Label();
            this.prbFileMain = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblProcessElement
            // 
            this.lblProcessElement.AutoSize = true;
            this.lblProcessElement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblProcessElement.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProcessElement.Location = new System.Drawing.Point(4, 24);
            this.lblProcessElement.Name = "lblProcessElement";
            this.lblProcessElement.Size = new System.Drawing.Size(35, 13);
            this.lblProcessElement.TabIndex = 6;
            this.lblProcessElement.Text = "label1";
            // 
            // prbElementMain
            // 
            this.prbElementMain.BackColor = System.Drawing.Color.LightGray;
            this.prbElementMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.prbElementMain.Location = new System.Drawing.Point(7, 40);
            this.prbElementMain.Name = "prbElementMain";
            this.prbElementMain.Size = new System.Drawing.Size(499, 28);
            this.prbElementMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbElementMain.TabIndex = 7;
            this.prbElementMain.UseWaitCursor = true;
            // 
            // lblProgessTitle
            // 
            this.lblProgessTitle.AutoSize = true;
            this.lblProgessTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProgessTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblProgessTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProgessTitle.Location = new System.Drawing.Point(0, 0);
            this.lblProgessTitle.Name = "lblProgessTitle";
            this.lblProgessTitle.Size = new System.Drawing.Size(41, 13);
            this.lblProgessTitle.TabIndex = 5;
            this.lblProgessTitle.Text = "label1";
            // 
            // lblProcessFolder
            // 
            this.lblProcessFolder.AutoSize = true;
            this.lblProcessFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblProcessFolder.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProcessFolder.Location = new System.Drawing.Point(9, 84);
            this.lblProcessFolder.Name = "lblProcessFolder";
            this.lblProcessFolder.Size = new System.Drawing.Size(35, 13);
            this.lblProcessFolder.TabIndex = 8;
            this.lblProcessFolder.Text = "label1";
            // 
            // prbFolderMain
            // 
            this.prbFolderMain.BackColor = System.Drawing.Color.LightGray;
            this.prbFolderMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.prbFolderMain.Location = new System.Drawing.Point(12, 100);
            this.prbFolderMain.Name = "prbFolderMain";
            this.prbFolderMain.Size = new System.Drawing.Size(499, 28);
            this.prbFolderMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbFolderMain.TabIndex = 9;
            this.prbFolderMain.UseWaitCursor = true;
            // 
            // lblProcessFile
            // 
            this.lblProcessFile.AutoSize = true;
            this.lblProcessFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblProcessFile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblProcessFile.Location = new System.Drawing.Point(9, 143);
            this.lblProcessFile.Name = "lblProcessFile";
            this.lblProcessFile.Size = new System.Drawing.Size(35, 13);
            this.lblProcessFile.TabIndex = 10;
            this.lblProcessFile.Text = "label1";
            // 
            // prbFileMain
            // 
            this.prbFileMain.BackColor = System.Drawing.Color.LightGray;
            this.prbFileMain.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.prbFileMain.Location = new System.Drawing.Point(12, 159);
            this.prbFileMain.Name = "prbFileMain";
            this.prbFileMain.Size = new System.Drawing.Size(499, 28);
            this.prbFileMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbFileMain.TabIndex = 11;
            this.prbFileMain.UseWaitCursor = true;
            // 
            // RecoveryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(520, 212);
            this.Controls.Add(this.lblProcessFile);
            this.Controls.Add(this.prbFileMain);
            this.Controls.Add(this.lblProcessFolder);
            this.Controls.Add(this.prbFolderMain);
            this.Controls.Add(this.lblProcessElement);
            this.Controls.Add(this.prbElementMain);
            this.Controls.Add(this.lblProgessTitle);
            this.Name = "RecoveryForm";
            this.Text = "Backup and recover";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcessElement;
        private System.Windows.Forms.ProgressBar prbElementMain;
        private System.Windows.Forms.Label lblProgessTitle;
        private System.Windows.Forms.Label lblProcessFolder;
        private System.Windows.Forms.ProgressBar prbFolderMain;
        private System.Windows.Forms.Label lblProcessFile;
        private System.Windows.Forms.ProgressBar prbFileMain;
    }
}

