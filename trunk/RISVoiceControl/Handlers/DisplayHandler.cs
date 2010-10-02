using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using RIS.RISVoiceControl.Utils;

namespace RIS.RISVoiceControl
{
    public partial class VoiceControl : UserControl
    {
        private void HandleDisplay()
        {
            //MessageBox.Show("Handling Display");
            btnRecord.Enabled = !this.ReadOnly;
            if (isClosing == false)
            {
                switch (currentState)
                {
                    case State.DOWNLOAD:
                        HandleDownloadDisplay();
                        break;
                    case State.DOWNLOADED:
                        HandleDownloadedDisplay();
                        break;
                    case State.RECORD:
                        HandleRecordDisplay();
                        break;
                    case State.WAIT:
                        HandleWaitDisplay();
                        break;
                    case State.STOP:
                        HandleStopDisplay();
                        break;
                    case State.PLAY:
                        HandlePlayDisplay();
                        break;
                    case State.SAVE:
                        HandleSaveDisplay();
                        break;
                    case State.SAVED:
                        HandleSavedDisplay();
                        break;
                    case State.PAUSE:
                        HandlePauseDisplay();
                        break;
                    case State.FORWARD:
                        HandleForwardDisplay();
                        break;
                    case State.REWIND:
                        HandleRewindDisplay();
                        break;
                }
            }
            //MessageBox.Show("Display Handled!!");
        }
        private void HandleDownloadDisplay()
        {
            panelMain.Visible = false;
            timerInit.Enabled = true;
            statusProgressBar.Visible = true;
        }
        private void HandleDownloadedDisplay()
        {
            //MessageBox.Show("Here...");
            //MessageBox.Show(panelMain.ToString());
            panelMain.Visible = true;
            //MessageBox.Show(statusProgressBar.ToString());
            statusProgressBar.Visible = false;
            SetMessage("Ready");
            btnPlay.Enabled = true;
            timerInit.Enabled = false;
            btnSave.Enabled = false;
        }
        private void HandleWaitDisplay()
        {
            statusProgressBar.Visible = false;
            SetMessage("Ready");
            timerInit.Enabled = false;
            btnPlay.Enabled = false;
            panelMain.Visible = true;
            btnSave.Enabled = false;
        }

        private void HandleRecordDisplay()
        {
            SetStatus(Constants.Messages.Information.Recording, Color.Red, true);
            btnPlay.Enabled = false;
            btnPause.Enabled = true;
        }

        private void HandleStopDisplay()
        {
            SetMessage(Constants.Messages.Information.Stopped);
            if (audioHandler.CanPlayback)
                btnPlay.Enabled = true;
            else
                btnPlay.Enabled = false;

            btnForward.Enabled = false;
            btnRewind.Enabled = false;      
            btnPause.Enabled = false;

            if (audioHandler.HasRecording)
                btnSave.Enabled = !ReadOnly;
            else
                btnSave.Enabled = false;
        }
        private void HandlePlayDisplay()
        {
            SetMessage("Playing...");
            btnPause.Enabled = true;
            btnForward.Enabled = true;
            btnRewind.Enabled = true;
            btnSave.Enabled = false;
            //isPlaying = true;
            btnRecord.Enabled = false;
        }
        private void HandleForwardDisplay()
        {
            SetMessage("Forwarding...");
            btnPause.Enabled = true;
            btnSave.Enabled = false;
            //isPlaying = true;
            btnRecord.Enabled = false;
        }
        private void HandleRewindDisplay()
        {
            SetMessage("Rewinding...");
            btnPause.Enabled = true;
            btnSave.Enabled = false;
            //isPlaying = true;
            btnRecord.Enabled = false;
        }

        private void HandleSaveDisplay()
        {
            btnSave.Enabled = false;
            panelMain.Visible = false;
            statusProgressBar.Visible = true;
        }
        private void HandleSavedDisplay()
        {
            panelMain.Visible = true;
            statusProgressBar.Visible = false;
            SetMessage(Constants.Messages.Information.Saved);
        }
        private void HandlePauseDisplay()
        {
            SetMessage(Constants.Messages.Information.Paused);
        }

        private void SetMessage(string text)
        {
            SetStatus(text, Color.Black, true);
        }
        private void SetError(string text)
        {
            SetStatus(text, Color.Red, false);
        }

        private void SetStatus(string text, Color color, bool showControl)
        {
            lblStatus.Text = text;
            lblStatus.ForeColor = color;
            if (!showControl)
            {
                panelMain.Visible = false;
            }
        }

        public void SetUploadPercentage(int percentage)
        {
            statusProgressBar.Value = percentage;
        }

        public void SetTrackPosition(int position)
        {
            trackBar.Value = position;
        }

        public void SetTimeLabels(int fileLength)
        {
            int timeInSecs = fileLength / 1000;
            timeInSecs += (fileLength%1000 == 0)?0:1;
            lblEndTime.Text = GetTime(timeInSecs);
            timeInSecs += (fileLength % 2 == 0) ? 0 : 1;
            lblMidTime.Text = GetTime(timeInSecs / 2);
            trackBar.Maximum = timeInSecs;
        }
        private string GetTime(int time)
        {
            StringBuilder sb = new StringBuilder();
            if (time / 60 < 10)
                sb.Append("0");
            sb.Append(time / 60);
            sb.Append(":");
            if (time % 60 < 10)
                sb.Append("0");
            sb.Append(time % 60);
            return sb.ToString();
        }
    }
}
