using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Text;
using System.Configuration;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

/// <summary>
/// Summary description for AudioUpload
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AudioUpload : System.Web.Services.WebService {

    public AudioUpload () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public object UploadCompleteFile(byte[] buffer, string fileName, int studyId, int radiologistId,int findingId)
    {
        /*BinaryWriter binWriter = new BinaryWriter(File.Open(GetPhysicalPath(fileName), FileMode.CreateNew, FileAccess.Write));
        binWriter.Write(buffer);
        binWriter.Close();
        return SaveFinding(buffer,studyId, fileName, radiologistId,findingId);*/
        return null;
    }
    [WebMethod]
    public int UploadFile(byte[] buffer, string fileName, int studyId, int radiologistId, int findingId, bool isEnd,bool isStart)
    {
        int savedFindingId = SaveFinding(buffer,studyId, fileName, radiologistId,findingId,isStart);
        if (isEnd) SaveStudy(studyId, (int)savedFindingId, radiologistId);
        return savedFindingId;
    }

    [WebMethod]
    public bool IsFindingPresent(int findingId, int radiologistId)
    {
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = findingId;
        finding.Load(radiologistId);
        return finding.IsLoaded;
    }

    [WebMethod]
    public long GetFileSize(int findingId, int radiologistId)
    {
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = findingId;
        finding.Load(radiologistId);
        if (finding.IsLoaded)
        {
            return ((byte[])finding.AudioData.Value).Length;            
        }
        return 0;
    }

    
    [WebMethod]
    public byte[] GetCompleteFile(int findingId, int radiologistId)
    {
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = findingId;
        finding.Load(radiologistId);
        if (finding.IsLoaded)
        {
            return (byte[])finding.AudioData.Value;
        }
        return null;
    }

    [WebMethod]
    public int GetChunkSize()
    {
        return 100 * 1024;
    }
    private int SaveFinding(byte[] data,int studyId, string fileName, int radiologistId,int findingId,bool isStart)
    {
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = findingId;
        finding.Load();
        if (finding.IsLoaded && isStart == false)
        {
            byte[] currentData = (byte[])finding.AudioData.Value;
            byte[] finalData = new byte[currentData.Length + data.Length]; 
            Buffer.BlockCopy(currentData, 0, finalData, 0, currentData.Length);
            Buffer.BlockCopy(data,0, finalData, currentData.Length, data.Length);
            finding.AudioData.Value = finalData;
        }                    
        else
        {
            finding.StudyId.Value = studyId;
            finding.AudioFileName.Value = fileName;
            finding.AudioData.Value = data;
        }
        UserObject radiologist = new UserObject();
        radiologist.UserId.Value = radiologistId;
        radiologist.Load();
        if (radiologist.IsLoaded)
        {
            finding.AudioUserId.Value = radiologistId;
            finding.AudioUserName.Value = radiologist.Name.Value;
        }
        finding.AudioDate.Value = DateTime.Now;
        finding.Save(radiologistId);        
        return int.Parse(finding.FindingId.Value.ToString());
    }
    private void SaveStudy(int studyId, int findingId, int radiologistId)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load(radiologistId);
        if (study.IsLoaded)
        {
            LogObject log = new LogObject();
            log.UserId.Value = radiologistId;
            log.StudyId.Value = study.StudyId.Value;
            log.PatientId.Value = study.PatientId.Value;
            log.ActionTime.Value = DateTime.Now;

            if (study.LatestFindingId.Value != null)
            {
                study.StudyStatusId.Value = Constants.StudyStatusTypes.Redictated;
                log.Action.Value = Constants.LogActions.RedictatedStudy;
            }
            else
            {
                study.StudyStatusId.Value = Constants.StudyStatusTypes.Dictated;
                study.LatestFindingId.Value = findingId;
                log.Action.Value = Constants.LogActions.DictatedStudy;
            }
            study.Save(radiologistId);
            log.Save();
        }
    }
    private string GetVirtualPath(string fileName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(ConfigurationManager.AppSettings["AudioDirectory"]);
        sb.Append("/");
        sb.Append(fileName);
        return sb.ToString();
    }
    private string GetPhysicalPath(string fileName)
    {
        return Server.MapPath(GetVirtualPath(fileName));
    }
}

