using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

using RIS.RISVoiceControl.AudioLibrary;
using RIS.RISVoiceControl.Utils;

namespace RIS.RISVoiceControl.Handlers
{
    public class AudioHandler : IDisposable
    {
        public void Dispose()
        {
            if (m_Writer != null) m_Writer.Close();
            if (m_Recorder != null) m_Recorder.Dispose();
            RemoveFile();
        }
        int fileLength = 0;
        int currentFilePosition = 0;

        private WaveInRecorder m_Recorder = null;
        private byte[] m_RecBuffer = null;
        private Mp3Writer m_Writer = null;
        private string filePath = null;
        private string fileName = null;

        Timer timerPlay = null;

        VoiceControl voiceControl = null;

        bool doRecord = true;
        FileStream currentFile = null;
        public bool DoRecord
        {
            get
            {
                return doRecord;
            }
            set
            {
                doRecord = value;
            }
        }

        public AudioHandler(VoiceControl voiceControl)
        {
            timerPlay = new Timer();
            timerPlay.Interval = 1000;
            timerPlay.Tick += new System.EventHandler(OnTimerPlayEvent);
            timerPlay.Enabled = false;
            this.voiceControl = voiceControl;
        }

        private void OnTimerPlayEvent(object source, EventArgs e)
        {
            switch (voiceControl.CurrentState)
            { 
                case VoiceControl.State.PLAY:
                    currentFilePosition += 1000;
                    break;
                case VoiceControl.State.FORWARD:
                    currentFilePosition += 2000;
                    break;
                case VoiceControl.State.REWIND:
                    currentFilePosition -= 1000;
                    break;
            }

            if (currentFilePosition >= fileLength || currentFilePosition <= 0)
            {
                voiceControl.OnStopClick();
                currentFilePosition = 0;
            }
            int position = currentFilePosition / 1000;
            voiceControl.SetTrackPosition(position);
        }


        public void EmptyAudioCache()
        {
            string[] fileNames = Directory.GetFiles(Constants.ClientDirectory);
            foreach (string fileName in fileNames)
            {
                try
                {
                    DateTime fileCreationTime = File.GetCreationTime(fileName);
                    if (fileCreationTime.AddHours(Constants.FileCreationLimitInHours) <= DateTime.Now)
                    {
                        File.Delete(fileName);
                    }
                }
                catch (Exception ex)
                {
                    StreamWriter writer = new StreamWriter(fileName + ".log");
                    writer.WriteLine(ex.StackTrace);
                    writer.Close();
                }
            }
        }

        public void WriteFile(byte[] data)
        {
            filePath = GetFilePath();
            BinaryWriter binWriter = new BinaryWriter(File.Open(filePath, FileMode.Create, FileAccess.Write));
            binWriter.Write(data);
            binWriter.Close();
        }
        private string GetFilePath()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            //sb.Append(this.StudyId);
            //sb.Append(this.UserId);
            sb.Append(random.Next(1, int.MaxValue).ToString());
            sb.Append(".mp3");
            fileName = sb.ToString();
            return GetFilePath(fileName);
        }

        private string GetFilePath(string pFileName)
        {
            StringBuilder sb = new StringBuilder();
            if (Directory.Exists(Constants.ClientDirectory) == false)
            {
                Directory.CreateDirectory(Constants.ClientDirectory);
            }
            sb.Append(Constants.ClientDirectory);
            sb.Append("\\");
            sb.Append(pFileName);
            return sb.ToString();
        }

        public void StartRecording()
        {
            Logging.Instance.WriteLine("Starting recording...");
            StopRecording();
            Logging.Instance.WriteLine("StartRecording has stoped recording");
            try
            {
                WaveFormat fmt = new WaveFormat(44100, 16, 2);
                Logging.Instance.WriteLine("Created fmt");
                Mp3WriterConfig config = new Mp3WriterConfig(fmt);
                Logging.Instance.WriteLine("created config");
                if (filePath == null)
                {
                    Logging.Instance.WriteLine("File Path is null");
                    filePath = GetFilePath();
                    Logging.Instance.WriteLine("File Path:" + filePath);
                }
                //DoRecord = false;
                currentFile = new FileStream(filePath, FileMode.Append);
                m_Writer = new Mp3Writer(currentFile, config);
                Logging.Instance.WriteLine("writer intialized");
                m_Recorder = new WaveInRecorder(-1, fmt, 16384, 3, new BufferDoneEventHandler(DataArrived));
                Logging.Instance.WriteLine("recorder initialized");
            }
            catch (Exception ex)
            {
                Logging.Instance.WriteLine(ex.StackTrace);
                StopRecording();
                throw ex;
            }
            Logging.Instance.WriteLine("leaving start recording");
        }

        public bool CanPlayback
        {
            get
            {
                return (filePath == null)?false:true;
            }
        }

        bool hasRecording = false;
        public bool HasRecording
        {
            get
            {
                return hasRecording;
            }
        }

        public void AudioSaved()
        {
            hasRecording = false;
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }
        }

        public void StopPlayback()
        {
            StopFile();
            CloseFile();
        }

        public void PauseFile()
        {
            string Pcommand = "pause " + fileName;
            WaveNative.mciSendString(Pcommand, null, 0, IntPtr.Zero);
            timerPlay.Enabled = false;
        }

        public void ResumeFile()
        {
            string Pcommand = "resume " + fileName;
            WaveNative.mciSendString(Pcommand, null, 0, IntPtr.Zero);
            timerPlay.Enabled = true;
        }

        private void PlayFile()
        {
            string Pcommand = "play " + fileName + " from " + currentFilePosition;
            WaveNative.mciSendString(Pcommand, null, 0, IntPtr.Zero);
            timerPlay.Enabled = true;
        }

        private void StopFile()
        {
            string PCommand = "stop " + fileName;
            WaveNative.mciSendString(PCommand, null, 0, IntPtr.Zero);
            timerPlay.Enabled = false;
            currentFilePosition = 0;
        }

        public void ForwardFile()
        {
            SetPosition();
            //timerPlay.Enabled = true;
        }

        public void RewindFile()
        {
            SetPosition();
            //timerPlay.Enabled = true;
        }

        public void SetSpeed(int speed)
        {
            string PCommand = "set " + fileName + " speed " + speed;
            WaveNative.mciSendString(PCommand, null, 0, IntPtr.Zero);
        }

        private void SetPosition()
        {
            string Pcommand = "status " + fileName + " position";
            int i = 128;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(i);
            WaveNative.mciSendString(Pcommand, stringBuilder, i, IntPtr.Zero);
            if(stringBuilder.Length > 0)
                currentFilePosition = Convert.ToInt32(stringBuilder.ToString());
            string PCommand = "stop " + fileName;
            WaveNative.mciSendString(PCommand, null, 0, IntPtr.Zero);
            timerPlay.Enabled = true;
        }

        public void SetSeekPosition(int positionInSeconds)
        {
            StopFile();
            currentFilePosition = positionInSeconds * 1000;
            PlayFile();
        }


        private void OpenFile()
        {
            string Pcommand = "open \"" + filePath + "\" type mpegvideo alias " + fileName;
            WaveNative.mciSendString(Pcommand, null, 0, IntPtr.Zero);
            Logging.Instance.WriteLine("File opened");
            Pcommand = "status " + fileName + " length";
            int i = 128;
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(i);
            WaveNative.mciSendString(Pcommand, stringBuilder, i, IntPtr.Zero);
            if (stringBuilder.Length > 0)
            {
                fileLength = int.Parse(stringBuilder.ToString());
                voiceControl.SetTimeLabels(fileLength);
            }
            Logging.Instance.WriteLine("Length in seconds: " + fileLength);
        }
        private void CloseFile()
        {
            string Pcommand = "close " + fileName;
            WaveNative.mciSendString(Pcommand, null, 0, IntPtr.Zero);
        }

        public void StartPlayback()
        {
            OpenFile();
            PlayFile();
        }

        public void StopRecording()
        {
            Logging.Instance.WriteLine("Stoping recording...");
            try
            {
                hasRecording = true;
                if (m_Recorder != null)
                    try
                    {
                        Logging.Instance.WriteLine("Recorder is not null");
                        m_Recorder.Dispose();
                    }
                    finally
                    {
                        m_Recorder = null;
                    }
                if (m_Writer != null)
                    try
                    {
                        Logging.Instance.WriteLine("Writer is not null");
                        m_Writer.Close();
                    }
                    finally
                    {
                        m_Writer = null;
                    }
                MessageBox.Show("Hello");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                Logging.Instance.WriteLine(ex.StackTrace);
            }
            Logging.Instance.WriteLine("Stopped Recording!");
        }

        private void DataArrived(IntPtr data, int size)
        {
            Logging.Instance.WriteLine("Data Arrived");
            try
            {
                Logging.Instance.WriteLine("DoRecord:" + DoRecord);
                if (DoRecord)
                {
                    if (m_RecBuffer == null || m_RecBuffer.Length < size)
                        m_RecBuffer = new byte[size];
                    Logging.Instance.WriteLine("Copying...");
                    System.Runtime.InteropServices.Marshal.Copy(data, m_RecBuffer, 0, size);
                    Logging.Instance.WriteLine("Copied");
                    m_Writer.Write(m_RecBuffer, 0, m_RecBuffer.Length);
                    Logging.Instance.WriteLine("Written");
                }
            }
            catch (Exception ex)
            {
                Logging.Instance.WriteLine(ex.StackTrace);
            }
        }

        public void RemoveFile()
        {
            if (currentFile != null) currentFile.Close();
            if (filePath != null) File.Delete(filePath);
        }
    }
}
