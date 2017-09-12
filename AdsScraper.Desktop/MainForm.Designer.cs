namespace AdsScraper.Desktop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.fetchAdsButton = new System.Windows.Forms.Button();
            this.EventLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // fetchAdsButton
            //
            this.fetchAdsButton.Location = new System.Drawing.Point(429, 29);
            this.fetchAdsButton.Name = "fetchAdsButton";
            this.fetchAdsButton.Size = new System.Drawing.Size(107, 23);
            this.fetchAdsButton.TabIndex = 0;
            this.fetchAdsButton.Text = "Fetch car ads";
            this.fetchAdsButton.UseVisualStyleBackColor = true;
            this.fetchAdsButton.Click += new System.EventHandler(this.FetchAdsButton_Click);
            //
            // eventsTextBox
            //
            this.EventLog.Location = new System.Drawing.Point(12, 119);
            this.EventLog.Multiline = true;
            this.EventLog.Name = "EventLog";
            this.EventLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.EventLog.Size = new System.Drawing.Size(394, 287);
            this.EventLog.TabIndex = 1;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Events log";
            //
            // urlTextBox
            //
            this.urlTextBox.Location = new System.Drawing.Point(12, 31);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(394, 20);
            this.urlTextBox.TabIndex = 3;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "URL";
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 429);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.urlTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EventLog);
            this.Controls.Add(this.fetchAdsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "AdsScraper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fetchAdsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Label label2;
    }
}

