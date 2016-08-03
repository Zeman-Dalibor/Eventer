namespace Eventer
{
    partial class DeletingForm
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
            this.components = new System.ComponentModel.Container();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelProgress = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonStorno = new System.Windows.Forms.Button();
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 37);
            this.progressBar.Margin = new System.Windows.Forms.Padding(3, 3, 3, 15);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(367, 23);
            this.progressBar.TabIndex = 0;
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(59, 21);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(18, 13);
            this.labelProgress.TabIndex = 6;
            this.labelProgress.Text = "-/-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Průběh:";
            // 
            // buttonStorno
            // 
            this.buttonStorno.Location = new System.Drawing.Point(304, 81);
            this.buttonStorno.Name = "buttonStorno";
            this.buttonStorno.Size = new System.Drawing.Size(75, 23);
            this.buttonStorno.TabIndex = 8;
            this.buttonStorno.Text = "Storno";
            this.buttonStorno.UseVisualStyleBackColor = true;
            this.buttonStorno.Click += new System.EventHandler(this.buttonStorno_Click);
            // 
            // timerProgress
            // 
            this.timerProgress.Enabled = true;
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // DeletingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 116);
            this.Controls.Add(this.buttonStorno);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeletingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "DeletingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeletingForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonStorno;
        private System.Windows.Forms.Timer timerProgress;
    }
}