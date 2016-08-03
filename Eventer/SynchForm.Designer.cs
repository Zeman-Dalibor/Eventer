namespace Eventer
{
    partial class SynchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelLastSynch = new System.Windows.Forms.Label();
            this.checkedListBoxLocalChanges = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelIP = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.checkedListBoxRemoteChanges = new System.Windows.Forms.CheckedListBox();
            this.buttonSynchronize = new System.Windows.Forms.Button();
            this.timerLocalChanges = new System.Windows.Forms.Timer(this.components);
            this.timerRemoteChanges = new System.Windows.Forms.Timer(this.components);
            this.progressBarLocal = new System.Windows.Forms.ProgressBar();
            this.progressBarRemote = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.labelProgressLocal = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.labelProgressRemote = new System.Windows.Forms.Label();
            this.timerSynchronize = new System.Windows.Forms.Timer(this.components);
            this.labelLoading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Jméno:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Identifikátor:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(97, 22);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(10, 13);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "-";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(97, 46);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(10, 13);
            this.labelID.TabIndex = 1;
            this.labelID.Text = "-";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(320, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Poslední synchronizace:";
            // 
            // labelLastSynch
            // 
            this.labelLastSynch.AutoSize = true;
            this.labelLastSynch.Location = new System.Drawing.Point(449, 22);
            this.labelLastSynch.Name = "labelLastSynch";
            this.labelLastSynch.Size = new System.Drawing.Size(10, 13);
            this.labelLastSynch.TabIndex = 1;
            this.labelLastSynch.Text = "-";
            // 
            // checkedListBoxLocalChanges
            // 
            this.checkedListBoxLocalChanges.DisplayMember = "ListBoxString";
            this.checkedListBoxLocalChanges.FormattingEnabled = true;
            this.checkedListBoxLocalChanges.HorizontalScrollbar = true;
            this.checkedListBoxLocalChanges.Location = new System.Drawing.Point(12, 87);
            this.checkedListBoxLocalChanges.Name = "checkedListBoxLocalChanges";
            this.checkedListBoxLocalChanges.Size = new System.Drawing.Size(431, 364);
            this.checkedListBoxLocalChanges.TabIndex = 3;
            this.checkedListBoxLocalChanges.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxLocalChanges_ItemCheck);
            this.checkedListBoxLocalChanges.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxLocalChanges_MouseDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(423, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "IP:";
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(449, 46);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(10, 13);
            this.labelIP.TabIndex = 1;
            this.labelIP.Text = "-";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(177, 562);
            this.progressBar.Maximum = 1000;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(514, 32);
            this.progressBar.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 572);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Průběh:";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(76, 572);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(18, 13);
            this.labelProgress.TabIndex = 5;
            this.labelProgress.Text = "-/-";
            // 
            // checkedListBoxRemoteChanges
            // 
            this.checkedListBoxRemoteChanges.DisplayMember = "ListBoxString";
            this.checkedListBoxRemoteChanges.FormattingEnabled = true;
            this.checkedListBoxRemoteChanges.HorizontalScrollbar = true;
            this.checkedListBoxRemoteChanges.Location = new System.Drawing.Point(480, 87);
            this.checkedListBoxRemoteChanges.Name = "checkedListBoxRemoteChanges";
            this.checkedListBoxRemoteChanges.Size = new System.Drawing.Size(431, 364);
            this.checkedListBoxRemoteChanges.TabIndex = 3;
            this.checkedListBoxRemoteChanges.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxRemoteChanges_ItemCheck);
            this.checkedListBoxRemoteChanges.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.checkedListBoxRemoteChanges_MouseDoubleClick);
            // 
            // buttonSynchronize
            // 
            this.buttonSynchronize.Enabled = false;
            this.buttonSynchronize.Location = new System.Drawing.Point(735, 562);
            this.buttonSynchronize.Name = "buttonSynchronize";
            this.buttonSynchronize.Size = new System.Drawing.Size(176, 32);
            this.buttonSynchronize.TabIndex = 2;
            this.buttonSynchronize.Text = "Synchronizovat";
            this.buttonSynchronize.UseVisualStyleBackColor = true;
            this.buttonSynchronize.Click += new System.EventHandler(this.buttonSynchronize_Click);
            // 
            // timerLocalChanges
            // 
            this.timerLocalChanges.Enabled = true;
            this.timerLocalChanges.Tick += new System.EventHandler(this.timerLocalChanges_Tick);
            // 
            // timerRemoteChanges
            // 
            this.timerRemoteChanges.Enabled = true;
            this.timerRemoteChanges.Tick += new System.EventHandler(this.timerRemoteChanges_Tick);
            // 
            // progressBarLocal
            // 
            this.progressBarLocal.Location = new System.Drawing.Point(140, 457);
            this.progressBarLocal.Maximum = 1000;
            this.progressBarLocal.Name = "progressBarLocal";
            this.progressBarLocal.Size = new System.Drawing.Size(303, 23);
            this.progressBarLocal.TabIndex = 4;
            // 
            // progressBarRemote
            // 
            this.progressBarRemote.Location = new System.Drawing.Point(608, 457);
            this.progressBarRemote.Maximum = 1000;
            this.progressBarRemote.Name = "progressBarRemote";
            this.progressBarRemote.Size = new System.Drawing.Size(303, 23);
            this.progressBarRemote.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 467);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Průběh:";
            // 
            // labelProgressLocal
            // 
            this.labelProgressLocal.AutoSize = true;
            this.labelProgressLocal.Location = new System.Drawing.Point(62, 467);
            this.labelProgressLocal.Name = "labelProgressLocal";
            this.labelProgressLocal.Size = new System.Drawing.Size(18, 13);
            this.labelProgressLocal.TabIndex = 5;
            this.labelProgressLocal.Text = "-/-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(477, 467);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Průběh:";
            // 
            // labelProgressRemote
            // 
            this.labelProgressRemote.AutoSize = true;
            this.labelProgressRemote.Location = new System.Drawing.Point(527, 467);
            this.labelProgressRemote.Name = "labelProgressRemote";
            this.labelProgressRemote.Size = new System.Drawing.Size(18, 13);
            this.labelProgressRemote.TabIndex = 5;
            this.labelProgressRemote.Text = "-/-";
            // 
            // timerSynchronize
            // 
            this.timerSynchronize.Enabled = true;
            this.timerSynchronize.Tick += new System.EventHandler(this.timerSynchronize_Tick);
            // 
            // labelLoading
            // 
            this.labelLoading.AutoSize = true;
            this.labelLoading.ForeColor = System.Drawing.Color.DimGray;
            this.labelLoading.Location = new System.Drawing.Point(732, 546);
            this.labelLoading.Name = "labelLoading";
            this.labelLoading.Size = new System.Drawing.Size(107, 13);
            this.labelLoading.TabIndex = 8;
            this.labelLoading.Text = "Probíhá načítání . . .";
            // 
            // SynchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 606);
            this.Controls.Add(this.labelLoading);
            this.Controls.Add(this.labelProgressRemote);
            this.Controls.Add(this.labelProgressLocal);
            this.Controls.Add(this.labelProgress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBarRemote);
            this.Controls.Add(this.progressBarLocal);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.checkedListBoxRemoteChanges);
            this.Controls.Add(this.checkedListBoxLocalChanges);
            this.Controls.Add(this.buttonSynchronize);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.labelLastSynch);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SynchForm";
            this.Text = "SynchForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SynchForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SynchForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelLastSynch;
        private System.Windows.Forms.CheckedListBox checkedListBoxLocalChanges;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.CheckedListBox checkedListBoxRemoteChanges;
        private System.Windows.Forms.Button buttonSynchronize;
        private System.Windows.Forms.Timer timerLocalChanges;
        private System.Windows.Forms.Timer timerRemoteChanges;
        private System.Windows.Forms.ProgressBar progressBarLocal;
        private System.Windows.Forms.ProgressBar progressBarRemote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelProgressLocal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelProgressRemote;
        private System.Windows.Forms.Timer timerSynchronize;
        private System.Windows.Forms.Label labelLoading;
    }
}