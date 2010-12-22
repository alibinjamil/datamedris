using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;

using RIS.RISVoiceControl.Utils;
using RISVoiceControl.RISServer;
using RIS.RISVoiceControl;

namespace RIS.RISVoiceControl.Handlers
{
    class UploadData : IDisposable
    {
        public int chunkSize = 0;
        public FileStream uploadedFileStream = null;
        public string filePath = null;
        public string fileName = null;
        public int studyId = 0;
        public int findingId = 0;
        public int userId = 0;
        public void Dispose()
        {
            Logging.Instance.WriteLine("Disposed called for UploadData");
            if (uploadedFileStream != null) uploadedFileStream.Close();
        }
        public override string ToString()
        {
            StringBuilder returnValue = new StringBuilder();
            returnValue.Append("chunkSize:").Append(this.chunkSize);
            returnValue.Append("uploadedFileStream:").Append(uploadedFileStream.ToString());
            returnValue.Append("filePath:").Append((filePath == null)?"":filePath);
            returnValue.Append("studyId:").Append(studyId);
            returnValue.Append("findingId:").Append(findingId);
            returnValue.Append("userId:").Append(userId);
            return returnValue.ToString();
        }
    }
    public class RequestHandler : IDisposable
    {
        private VoiceControl voiceControl;    
        UploadData currentUploadData = null;

        public RequestHandler(VoiceControl voiceControl)
        {
            this.voiceControl = voiceControl;            
        }

        public void GetCompleteFile(int findingId,int userId)
        {
            AudioUpload upload = new AudioUpload();
            try
            {
                upload.GetCompleteFileCompleted += new GetCompleteFileCompletedEventHandler(OnCompleteDownloadCompleted);
                upload.GetCompleteFileAsync(findingId, userId);            
                Logging.Instance.WriteLine("Getting Asnc Complet File");                
            }
            catch (TargetInvocationException tie)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);                
            }
            catch (System.Net.WebException we)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);                
            }
            finally
            {
                upload.Dispose();
            }
        }

        public void UploadFile(string filePath,string fileName,int studyId,int userId, int findingId)
        {
            AudioUpload upload = new AudioUpload();
            try
            {
                currentUploadData = new UploadData();
                Logging.Instance.WriteLine("Current Uplaod Initialized");
                currentUploadData.chunkSize = upload.GetChunkSize();
                currentUploadData.uploadedFileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read);
                currentUploadData.studyId = studyId;
                currentUploadData.userId = userId;
                currentUploadData.findingId = findingId;
                Logging.Instance.WriteLine("currentUploadDate = " + currentUploadData.ToString());
                Logging.Instance.WriteLine("File Size: " + currentUploadData.uploadedFileStream.Length);
                UploadFile(true);
            }
            catch (TargetInvocationException tie)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            catch (System.Net.WebException we)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            finally
            {
                upload.Dispose();
            }
        }

        private void UploadFile(bool isStart)
        {
            if (currentUploadData == null) return;
            AudioUpload upload = new AudioUpload();
            try
            {
                if (currentUploadData.uploadedFileStream.Position > 1)
                    voiceControl.SetUploadPercentage(Convert.ToInt32((currentUploadData.uploadedFileStream.Position * 100) / currentUploadData.uploadedFileStream.Length));
                int toRead = Convert.ToInt32(Math.Min(currentUploadData.chunkSize, currentUploadData.uploadedFileStream.Length - currentUploadData.uploadedFileStream.Position));
                Logging.Instance.WriteLine("Bytes to read:" + toRead);
                Byte[] data = new Byte[toRead];
                currentUploadData.uploadedFileStream.Read(data, 0, Convert.ToInt32(toRead));
                bool isEnd = (currentUploadData.uploadedFileStream.Position >= currentUploadData.uploadedFileStream.Length) ? true : false;
                Logging.Instance.WriteLine("Is File End:" + isEnd);
               
                upload.UploadFileCompleted += new UploadFileCompletedEventHandler(OnUploadFileCompleted);
                upload.UploadFileAsync(data, currentUploadData.fileName, currentUploadData.studyId, currentUploadData.userId, currentUploadData.findingId, isEnd, isStart);
                Logging.Instance.WriteLine("Called Async upload file");                
            }
            catch (TargetInvocationException tie)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            catch (System.Net.WebException we)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            finally
            {
                upload.Dispose();
            }
        }

        private void OnUploadFileCompleted(object sender, UploadFileCompletedEventArgs ufa)
        {
            Logging.Instance.WriteLine("Async upload file completed");
            if (ufa.Cancelled == false && ufa.Error == null)
            {
                if (currentUploadData.uploadedFileStream.Position >= currentUploadData.uploadedFileStream.Length)
                {
                    currentUploadData.Dispose(); 
                    currentUploadData = null;
                    voiceControl.OnUploadComplete(ufa.Result);
                }
                else
                {
                    currentUploadData.findingId = ufa.Result;
                    UploadFile(false);
                }
            }
            else
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerReturnedError + ufa.Error.Message);
            }
        }


        private void OnCompleteDownloadCompleted(Object sender, GetCompleteFileCompletedEventArgs fca)
        {
            Logging.Instance.WriteLine("Download completed.");            
            //timerInit.Enabled = false;
            if (fca.Cancelled == false && fca.Error == null)
            {
                //Logging.Instance.WriteLine("Downloaded Content Size:" + fca.Result.Length);
                voiceControl.OnDownloadCompleted(fca.Result);                
                }
                else
                {
                Logging.Instance.WriteLine("Download returned error. " + fca.Error.Message);
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerReturnedError + fca.Error.Message);
            }
        }


        public bool IsFindingPresent(int findingId,int userId) 
        {
            bool result = false;
            AudioUpload upload = new AudioUpload();
            try
            {
                result = upload.IsFindingPresent(findingId, userId);
            }
            catch (TargetInvocationException tie)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            catch (System.Net.WebException we)
            {
                throw new VoiceControlExcpetion(Constants.Messages.Error.ServerNotAvailable);
            }
            finally
            {
                upload.Dispose();
            }
            return result;
        }

        public void Dispose()
        {
            AudioUpload upload = new AudioUpload();
            upload.Abort();
            upload.Dispose();
            upload = null;
        }
    }
}
