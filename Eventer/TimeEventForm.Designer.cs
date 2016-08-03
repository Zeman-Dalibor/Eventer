namespace Eventer
{
    partial class TimeEventForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeEventForm));
            this.labelNotificationLabel = new System.Windows.Forms.Label();
            this.labelDescription = new System.Windows.Forms.Label();
            this.labelDescLable = new System.Windows.Forms.Label();
            this.labelEndDate = new System.Windows.Forms.Label();
            this.labelToLabel = new System.Windows.Forms.Label();
            this.labelNotification = new System.Windows.Forms.Label();
            this.labelStartDate = new System.Windows.Forms.Label();
            this.labelFromLabel = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelNameLabel = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNotificationLabel
            // 
            this.labelNotificationLabel.AutoSize = true;
            this.labelNotificationLabel.Location = new System.Drawing.Point(12, 148);
            this.labelNotificationLabel.Name = "labelNotificationLabel";
            this.labelNotificationLabel.Size = new System.Drawing.Size(66, 13);
            this.labelNotificationLabel.TabIndex = 34;
            this.labelNotificationLabel.Text = "Upozornění:";
            this.labelNotificationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDescription
            // 
            this.labelDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDescription.Location = new System.Drawing.Point(84, 35);
            this.labelDescription.Margin = new System.Windows.Forms.Padding(3);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(330, 88);
            this.labelDescription.TabIndex = 33;
            this.labelDescription.Text = "-";
            // 
            // labelDescLable
            // 
            this.labelDescLable.AutoSize = true;
            this.labelDescLable.Location = new System.Drawing.Point(42, 35);
            this.labelDescLable.Margin = new System.Windows.Forms.Padding(3);
            this.labelDescLable.Name = "labelDescLable";
            this.labelDescLable.Size = new System.Drawing.Size(36, 13);
            this.labelDescLable.TabIndex = 25;
            this.labelDescLable.Text = "Popis:";
            this.labelDescLable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEndDate
            // 
            this.labelEndDate.AutoSize = true;
            this.labelEndDate.Location = new System.Drawing.Point(246, 129);
            this.labelEndDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelEndDate.Name = "labelEndDate";
            this.labelEndDate.Size = new System.Drawing.Size(10, 13);
            this.labelEndDate.TabIndex = 26;
            this.labelEndDate.Text = "-";
            // 
            // labelToLabel
            // 
            this.labelToLabel.AutoSize = true;
            this.labelToLabel.Location = new System.Drawing.Point(216, 129);
            this.labelToLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelToLabel.Name = "labelToLabel";
            this.labelToLabel.Size = new System.Drawing.Size(24, 13);
            this.labelToLabel.TabIndex = 27;
            this.labelToLabel.Text = "Do:";
            // 
            // labelNotification
            // 
            this.labelNotification.AutoSize = true;
            this.labelNotification.Location = new System.Drawing.Point(84, 148);
            this.labelNotification.Margin = new System.Windows.Forms.Padding(3);
            this.labelNotification.Name = "labelNotification";
            this.labelNotification.Size = new System.Drawing.Size(10, 13);
            this.labelNotification.TabIndex = 28;
            this.labelNotification.Text = "-";
            // 
            // labelStartDate
            // 
            this.labelStartDate.AutoSize = true;
            this.labelStartDate.Location = new System.Drawing.Point(84, 129);
            this.labelStartDate.Margin = new System.Windows.Forms.Padding(3);
            this.labelStartDate.Name = "labelStartDate";
            this.labelStartDate.Size = new System.Drawing.Size(10, 13);
            this.labelStartDate.TabIndex = 29;
            this.labelStartDate.Text = "-";
            // 
            // labelFromLabel
            // 
            this.labelFromLabel.AutoSize = true;
            this.labelFromLabel.Location = new System.Drawing.Point(54, 129);
            this.labelFromLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelFromLabel.Name = "labelFromLabel";
            this.labelFromLabel.Size = new System.Drawing.Size(24, 13);
            this.labelFromLabel.TabIndex = 30;
            this.labelFromLabel.Text = "Od:";
            this.labelFromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.Location = new System.Drawing.Point(84, 16);
            this.labelName.Margin = new System.Windows.Forms.Padding(3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(330, 13);
            this.labelName.TabIndex = 31;
            this.labelName.Text = "-";
            // 
            // labelNameLabel
            // 
            this.labelNameLabel.AutoSize = true;
            this.labelNameLabel.Location = new System.Drawing.Point(37, 16);
            this.labelNameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.labelNameLabel.Name = "labelNameLabel";
            this.labelNameLabel.Size = new System.Drawing.Size(41, 13);
            this.labelNameLabel.TabIndex = 32;
            this.labelNameLabel.Text = "Jméno:";
            this.labelNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(269, 164);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(126, 28);
            this.buttonOK.TabIndex = 35;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // TimeEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 215);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelNotificationLabel);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelDescLable);
            this.Controls.Add(this.labelEndDate);
            this.Controls.Add(this.labelToLabel);
            this.Controls.Add(this.labelNotification);
            this.Controls.Add(this.labelStartDate);
            this.Controls.Add(this.labelFromLabel);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(452, 254);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(452, 254);
            this.Name = "TimeEventForm";
            this.ShowInTaskbar = false;
            this.Text = "TimeEventForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNotificationLabel;
        private System.Windows.Forms.Label labelDescription;
        private System.Windows.Forms.Label labelDescLable;
        private System.Windows.Forms.Label labelEndDate;
        private System.Windows.Forms.Label labelToLabel;
        private System.Windows.Forms.Label labelNotification;
        private System.Windows.Forms.Label labelStartDate;
        private System.Windows.Forms.Label labelFromLabel;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelNameLabel;
        private System.Windows.Forms.Button buttonOK;
    }
}