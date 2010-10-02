namespace RIS.RISVoiceControl
{
    partial class VoiceControl
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
            this.components = new System.ComponentModel.Container();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblMidTime = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.rbHighSpeed = new System.Windows.Forms.RadioButton();
            this.rbMidSpeed = new System.Windows.Forms.RadioButton();
            this.rbLowSpeed = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.timerInit = new System.Windows.Forms.Timer(this.components);
            this.toolTipRecord = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipPlay = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipStop = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipSave = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipPause = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipForward = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipRewind = new System.Windows.Forms.ToolTip(this.components);
            this.toolTipFootPedal = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnRewind = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.lblEndTime);
            this.panelMain.Controls.Add(this.lblMidTime);
            this.panelMain.Controls.Add(this.lblStartTime);
            this.panelMain.Controls.Add(this.rbHighSpeed);
            this.panelMain.Controls.Add(this.rbMidSpeed);
            this.panelMain.Controls.Add(this.rbLowSpeed);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.btnForward);
            this.panelMain.Controls.Add(this.btnRewind);
            this.panelMain.Controls.Add(this.btnPause);
            this.panelMain.Controls.Add(this.btnSave);
            this.panelMain.Controls.Add(this.btnStop);
            this.panelMain.Controls.Add(this.btnPlay);
            this.panelMain.Controls.Add(this.btnRecord);
            this.panelMain.Controls.Add(this.trackBar);
            this.panelMain.Location = new System.Drawing.Point(9, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(292, 101);
            this.panelMain.TabIndex = 7;
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTime.Location = new System.Drawing.Point(255, 31);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(34, 14);
            this.lblEndTime.TabIndex = 23;
            this.lblEndTime.Text = "00:00";
            // 
            // lblMidTime
            // 
            this.lblMidTime.AutoSize = true;
            this.lblMidTime.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMidTime.Location = new System.Drawing.Point(129, 31);
            this.lblMidTime.Name = "lblMidTime";
            this.lblMidTime.Size = new System.Drawing.Size(34, 14);
            this.lblMidTime.TabIndex = 22;
            this.lblMidTime.Text = "00:00";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(3, 31);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(34, 14);
            this.lblStartTime.TabIndex = 21;
            this.lblStartTime.Text = "00:00";
            // 
            // rbHighSpeed
            // 
            this.rbHighSpeed.AutoSize = true;
            this.rbHighSpeed.Checked = true;
            this.rbHighSpeed.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHighSpeed.Location = new System.Drawing.Point(239, 80);
            this.rbHighSpeed.Name = "rbHighSpeed";
            this.rbHighSpeed.Size = new System.Drawing.Size(50, 18);
            this.rbHighSpeed.TabIndex = 19;
            this.rbHighSpeed.TabStop = true;
            this.rbHighSpeed.Text = "1.0 X";
            this.rbHighSpeed.UseVisualStyleBackColor = true;
            this.rbHighSpeed.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbMidSpeed
            // 
            this.rbMidSpeed.AutoSize = true;
            this.rbMidSpeed.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMidSpeed.Location = new System.Drawing.Point(177, 80);
            this.rbMidSpeed.Name = "rbMidSpeed";
            this.rbMidSpeed.Size = new System.Drawing.Size(56, 18);
            this.rbMidSpeed.TabIndex = 18;
            this.rbMidSpeed.Text = "0.90 X";
            this.rbMidSpeed.UseVisualStyleBackColor = true;
            this.rbMidSpeed.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // rbLowSpeed
            // 
            this.rbLowSpeed.AutoSize = true;
            this.rbLowSpeed.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLowSpeed.Location = new System.Drawing.Point(121, 80);
            this.rbLowSpeed.Name = "rbLowSpeed";
            this.rbLowSpeed.Size = new System.Drawing.Size(56, 18);
            this.rbLowSpeed.TabIndex = 17;
            this.rbLowSpeed.Text = "0.80 X";
            this.rbLowSpeed.UseVisualStyleBackColor = true;
            this.rbLowSpeed.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "Playback Speed:";
            // 
            // trackBar
            // 
            this.trackBar.Location = new System.Drawing.Point(6, 3);
            this.trackBar.Margin = new System.Windows.Forms.Padding(1);
            this.trackBar.Name = "trackBar";
            this.trackBar.Size = new System.Drawing.Size(279, 45);
            this.trackBar.TabIndex = 20;
            this.trackBar.TickFrequency = 5;
            this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
            this.trackBar.Scroll += new System.EventHandler(this.trackBar_Scroll);
            // 
            // timerInit
            // 
            this.timerInit.Interval = 500;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.statusProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 104);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(309, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 17);
            this.lblStatus.Text = "Status";
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(220, 16);
            this.statusProgressBar.Visible = false;
            // 
            // btnForward
            // 
            this.btnForward.BackColor = System.Drawing.Color.White;
            this.btnForward.Enabled = false;
            this.btnForward.Image = global::RISVoiceControl.Properties.Resources.Forward;
            this.btnForward.Location = new System.Drawing.Point(127, 52);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(35, 23);
            this.btnForward.TabIndex = 15;
            this.toolTipForward.SetToolTip(this.btnForward, "Forward");
            this.btnForward.UseVisualStyleBackColor = false;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnRewind
            // 
            this.btnRewind.BackColor = System.Drawing.Color.White;
            this.btnRewind.Enabled = false;
            this.btnRewind.Image = global::RISVoiceControl.Properties.Resources.Rewind;
            this.btnRewind.Location = new System.Drawing.Point(45, 52);
            this.btnRewind.Name = "btnRewind";
            this.btnRewind.Size = new System.Drawing.Size(35, 23);
            this.btnRewind.TabIndex = 14;
            this.toolTipRewind.SetToolTip(this.btnRewind, "Rewind");
            this.btnRewind.UseVisualStyleBackColor = false;
            this.btnRewind.Click += new System.EventHandler(this.btnRewind_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.White;
            this.btnPause.Enabled = false;
            this.btnPause.Image = global::RISVoiceControl.Properties.Resources.Pause;
            this.btnPause.Location = new System.Drawing.Point(168, 52);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(35, 23);
            this.btnPause.TabIndex = 12;
            this.toolTipPause.SetToolTip(this.btnPause, "Pause");
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Image = global::RISVoiceControl.Properties.Resources.Save;
            this.btnSave.Location = new System.Drawing.Point(250, 52);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 23);
            this.btnSave.TabIndex = 10;
            this.toolTipSave.SetToolTip(this.btnSave, "Save & Upload");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.White;
            this.btnStop.Image = global::RISVoiceControl.Properties.Resources.Stop;
            this.btnStop.Location = new System.Drawing.Point(209, 52);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(35, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.toolTipStop.SetToolTip(this.btnStop, "Stop");
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.White;
            this.btnPlay.Image = global::RISVoiceControl.Properties.Resources.Play;
            this.btnPlay.Location = new System.Drawing.Point(86, 52);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(35, 23);
            this.btnPlay.TabIndex = 7;
            this.toolTipPlay.SetToolTip(this.btnPlay, "Play/Resume");
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackColor = System.Drawing.Color.White;
            this.btnRecord.Image = global::RISVoiceControl.Properties.Resources.Record;
            this.btnRecord.Location = new System.Drawing.Point(4, 52);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(35, 23);
            this.btnRecord.TabIndex = 6;
            this.toolTipRecord.SetToolTip(this.btnRecord, "Start/Resume Recording");
            this.btnRecord.UseVisualStyleBackColor = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // VoiceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelMain);
            this.Name = "VoiceControl";
            this.Size = new System.Drawing.Size(309, 126);
            this.Load += new System.EventHandler(this.AudioControl_Load);
            this.Leave += new System.EventHandler(this.AudioControl_Leave);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Timer timerInit;
        private System.Windows.Forms.ToolTip toolTipRecord;
        private System.Windows.Forms.ToolTip toolTipSave;
        private System.Windows.Forms.ToolTip toolTipStop;
        private System.Windows.Forms.ToolTip toolTipPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.ToolTip toolTipPause;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnRewind;
        private System.Windows.Forms.ToolTip toolTipForward;
        private System.Windows.Forms.ToolTip toolTipRewind;
        private System.Windows.Forms.ToolTip toolTipFootPedal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbMidSpeed;
        private System.Windows.Forms.RadioButton rbLowSpeed;
        private System.Windows.Forms.RadioButton rbHighSpeed;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label lblMidTime;
    }
}
