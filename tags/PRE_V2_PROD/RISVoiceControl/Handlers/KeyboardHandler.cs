using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace RIS.RISVoiceControl.Handlers
{
    public class KeyboardHandler : IDisposable
    {
        KeyboardHook keyboardHook = null;
        VoiceControl voiceControl = null;
        public KeyboardHandler(VoiceControl voiceControl)
        {
            this.voiceControl = voiceControl;
        }
        public void Engage()
        {
            //MessageBox.Show("Engagged");
            Disengage();
            keyboardHook = new KeyboardHook();
            keyboardHook.KeyIntercepted += new KeyboardHook.KeyboardHookEventHandler(keyboardHook_KeyIntercepted);
        }
        public void Disengage()
        {
            if (keyboardHook != null) keyboardHook.Dispose();
        }
        public void Dispose()
        {
            if (keyboardHook != null) keyboardHook.Dispose();
        }
        
        void keyboardHook_KeyIntercepted(KeyboardHook.KeyboardHookEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case 118://F7
                        voiceControl.OnRewindClick();
                        e.PassThrough = false;
                        break;
                    case 119://F8
                        voiceControl.OnForwardClick();
                        e.PassThrough = false;
                        break;
                    case 120://F9
                        if (voiceControl.CurrentState == VoiceControl.State.PLAY)
                            voiceControl.OnPauseClick();
                        else
                            voiceControl.OnPlayClick();
                        e.PassThrough = false;
                        break;
                    default:
                        e.PassThrough = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

    }
}
