using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

using RIS.RISVoiceControl.Utils;
using RIS.RISVoiceControl.Handlers;

namespace RIS.RISVoiceControl
{
    [ProgId("DigiPly.RIS.VoiceControl")]
    [ClassInterface(ClassInterfaceType.AutoDual),ComSourceInterfaces(typeof(ControlEvents))] //Implementing interface that will be visible from JS
    [Guid("A3993B96-F2DF-4dd9-8D37-5C55E59FF553")]
    [ComVisible(true)]
    public partial class VoiceControl : UserControl
    {
    
        [ComRegisterFunction()]
        public static void RegisterClass(string key)
        {
            // Strip off HKEY_CLASSES_ROOT\ from the passed key as I don't need it

            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open the CLSID\{guid} key for write access

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // And create the 'Control' key - this allows it to show up in 

            // the ActiveX control container 

            RegistryKey ctrl = k.CreateSubKey("Control");
            ctrl.Close();

            // Next create the CodeBase entry - needed if not string named and GACced.

            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // Finally close the main key

            k.Close();
        }

        [ComUnregisterFunction()]
        public static void UnregisterClass(string key)
        {
            StringBuilder sb = new StringBuilder(key);
            sb.Replace(@"HKEY_CLASSES_ROOT\", "");

            // Open HKCR\CLSID\{guid} for write access

            RegistryKey k = Registry.ClassesRoot.OpenSubKey(sb.ToString(), true);

            // Delete the 'Control' key, but don't throw an exception if it does not exist

            k.DeleteSubKey("Control", false);

            // Next open up InprocServer32

            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key, again not throwing if missing 

            k.DeleteSubKey("CodeBase", false);

            // Finally close the main key 

            k.Close();
        }
        [ComVisible(true)]
        public bool CheckClose()
        {
            Logging.Instance.WriteLine("CheckClose Called");
            /*if(currentState == State.DOWNLOAD)
            {
                return false;
            }
            return true;*/
            //MessageBox.Show("Here");
            isClosing = true;
            audioHandler.StopPlayback();
            audioHandler.StopRecording();
            audioHandler.RemoveFile();
            requestHandler.Dispose();
            keyBoardHandler.Disengage();
            return true;
        }
        [ComVisible(true)]
        public bool IsLoaded()
        {
            if(currentState == State.DOWNLOAD || currentState == State.WAIT)
            {
                return true;
            }
            return false;
        }
        public delegate void ControlEventHandler(string redirectUrl);


        /// <summary>
        /// This interface shows events to javascript
        /// </summary>
        [Guid("68BD4E0D-D7BC-4cf6-BEB7-CAB950161E79")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        public interface ControlEvents
        {
            //Add a DispIdAttribute to any members in the source interface to specify the COM DispId.
            [DispId(0x60020001)]
            void OnClose(string redirectUrl); //This method will be visible from JS
        }


        private int m_FindingId = 0;
        [ComVisible(true)]
        public int FindingId
        {
            get{ return m_FindingId; }
            set{ m_FindingId = value;}
        }

        private int m_StudyId = 0;
        [ComVisible(true)]
        public int StudyId
        {
            get{ return m_StudyId; }
            set{ m_StudyId = value;}
        }
        private int m_UserId = 0;
        [ComVisible(true)]
        public int UserId
        {
            get{ return m_UserId; }
            set{ m_UserId = value;}
        }

        private bool m_ReadOnly = false;
        [ComVisible(true)]
        public bool ReadOnly
        {
            get{ return m_ReadOnly; }
            set{ m_ReadOnly = value;}
        }

        private bool m_IsTranscriptionist = false;
        [ComVisible(true)]
        public bool IsTranscriptionist
        {
            get {return m_IsTranscriptionist; }
            set {m_IsTranscriptionist = value;}
        }

        private bool m_DownloadFile = true;
        public bool DownloadFile
        {
            get { return m_DownloadFile;  }
            set { m_DownloadFile = value; }
        }

        bool isClosing = false;

        public enum State
        {
            DOWNLOAD,
            DOWNLOADED,
            SAVE,
            SAVED,
            RECORD,
            PLAY,
            PAUSE,
            FORWARD,
            REWIND,
            STOP,
            WAIT,
            NULL
        }

        public State CurrentState
        {
            get { return currentState; }
        }

        AudioHandler audioHandler = null;
        RequestHandler requestHandler = null;
        KeyboardHandler keyBoardHandler = null;

        State currentState = State.NULL;
                  
        public VoiceControl()
        {
            currentState = State.NULL;
            //hasRecording = false;
            Logging.Instance.WriteLine("Constructor called.");
            InitializeComponent();
            InitializeHandlers();
            Logging.Instance.WriteLine("Component inialized");
            isClosing = false;
        }

        private void InitializeHandlers()
        {
            audioHandler = new AudioHandler(this);
            requestHandler = new RequestHandler(this);
            keyBoardHandler = new KeyboardHandler(this);
            //MessageBox.Show("Initializing...");
        }

        private void AudioControl_Load(object sender, EventArgs e)
        {
            Logging.Instance.WriteLine("Loading Control...");
            try
            {
                if (IsTranscriptionist)
                {
                    //MessageBox.Show("Is Tran");
                    keyBoardHandler.Engage();
                }
                audioHandler.EmptyAudioCache();

                if (requestHandler.IsFindingPresent(this.FindingId, this.UserId))
                {
                    Logging.Instance.WriteLine("Finding is greater than zero");
                    currentState = State.DOWNLOAD;
                    HandleDisplay();
                    if (DownloadFile)
                        requestHandler.GetCompleteFile(this.FindingId, this.UserId);
                }
                else
                {
                    Logging.Instance.WriteLine("Finding is less than zero");
                    currentState = State.WAIT;
                    HandleDisplay();
                }
            }
            catch (VoiceControlExcpetion vce)
            {
                SetError(vce.Message);
                Logging.Instance.WriteLine(vce.StackTrace);
            }
            catch (Exception ex)
            {
                Logging.Instance.WriteLine(ex.StackTrace);                
            }
        }

       

        public void OnDownloadCompleted(byte[] data)
        {
            Logging.Instance.WriteLine("Download completed.");
            //MessageBox.Show("VoiceControl...");
            if (data != null)
            {
                audioHandler.WriteFile(data);
                //MessageBox.Show("File Written");
                currentState = State.DOWNLOADED;
            }
            else
            {
                currentState = State.WAIT;
            }
            HandleDisplay();
        }




        private void btnRecord_Click(object sender, EventArgs e)
        {
            OnRecordClick();
        }

        private void OnRecordClick()
        {
            Logging.Instance.WriteLine("BtnRecord Pressed");
            if (currentState != State.PAUSE) //In Pause case the stuff has already been initialized so no need to init again
                audioHandler.StartRecording();                        
            currentState = State.RECORD;
            audioHandler.DoRecord = true;
            HandleDisplay();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {         
            OnStopClick();
        }
        public void OnStopClick()
        {
            Logging.Instance.WriteLine("OnStopClick Called");
            try
            {
                if (currentState == State.RECORD)
                {
                    audioHandler.StopRecording();
                }
                else
                {
                    audioHandler.StopPlayback();
                }                
                currentState = State.STOP;
                HandleDisplay();
            }
            catch (Exception ex)
            {
                Logging.Instance.WriteLine(ex.StackTrace);
                throw ex;
            }
        }
        private void btnPlay_Click(object sender, EventArgs e)
        {
            OnPlayClick();
        }

        public void OnPlayClick()
        {
            Logging.Instance.WriteLine("BtnPlay Pressed");
            if (currentState == State.PAUSE)
            {
                audioHandler.ResumeFile();
            }
            else
            {
                audioHandler.StartPlayback();
            }
            currentState = State.PLAY;
            HandleDisplay();
            if (rbLowSpeed.Checked)
            {
                audioHandler.SetSpeed(800);
            }
            else if (rbMidSpeed.Checked)
            {
                audioHandler.SetSpeed(900);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Logging.Instance.WriteLine("BtnSave Pressed");
            if (audioHandler.CanPlayback == false)
                return;
            currentState = State.SAVE;
            HandleDisplay();
            try
            {
                requestHandler.UploadFile(audioHandler.FilePath,audioHandler.FileName,this.StudyId,this.UserId,this.FindingId);
            }
            catch (VoiceControlExcpetion vce)
            {
                SetError(vce.Message);
            }
        }

        public void OnUploadComplete(int findingId)
        {
            this.FindingId = findingId;
            currentState = State.SAVED;
            HandleDisplay();
            audioHandler.AudioSaved();
        }

        private void AudioControl_Leave(object sender, EventArgs e)
        {
            OnStopClick();
            //MessageBox.Show("Leaving...");
        }

        private void wtimerInit_Tick(object sender, EventArgs e)
        {
            statusProgressBar.Increment(5);            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            OnPauseClick();
        }

        public void OnPauseClick()
        {
            if (currentState == State.PLAY)
            {
                audioHandler.PauseFile();
            }
            else
            {
                audioHandler.DoRecord = false;
            }
            currentState = State.PAUSE;
            HandleDisplay();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            OnForwardClick();
        }

        public void OnForwardClick()
        {
            currentState = State.FORWARD;
            HandleDisplay();
            audioHandler.ForwardFile();
        }

        private void btnRewind_Click(object sender, EventArgs e)
        {
            OnRewindClick();
        }

        public void OnRewindClick()
        {
            currentState = State.REWIND;
            HandleDisplay();
            audioHandler.RewindFile();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHighSpeed.Checked) audioHandler.SetSpeed(1000);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLowSpeed.Checked) audioHandler.SetSpeed(800);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMidSpeed.Checked) audioHandler.SetSpeed(900);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            audioHandler.SetSeekPosition(trackBar.Value);
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {

        }

        /*private void timerPlay_Tick(object sender, EventArgs e)
        {
            currentPlayBackInSeconds++;
            if (currentPlayBackInSeconds >= fileLengthInSeconds)
            {
                OnStopClick();
            }
        }*/
    }
}
