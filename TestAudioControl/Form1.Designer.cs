namespace TestAudioControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.voiceControl1 = new RIS.RISVoiceControl.VoiceControl();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 207);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // voiceControl1
            // 
            this.voiceControl1.BackColor = System.Drawing.Color.LightGray;
            this.voiceControl1.DownloadFile = true;
            this.voiceControl1.FindingId = 16;
            this.voiceControl1.IsTranscriptionist = true;
            this.voiceControl1.Location = new System.Drawing.Point(46, 39);
            this.voiceControl1.Name = "voiceControl1";
            this.voiceControl1.ReadOnly = true;
            this.voiceControl1.Size = new System.Drawing.Size(309, 126);
            this.voiceControl1.StudyId = 613;
            this.voiceControl1.TabIndex = 0;
            this.voiceControl1.UserId = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 266);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.voiceControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RIS.RISVoiceControl.VoiceControl voiceControl1;
        private System.Windows.Forms.Button button1;


    }
}

