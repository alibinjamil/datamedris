using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Text;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

/// <summary>
/// Summary description for FindingService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FindingService : System.Web.Services.WebService {

    public FindingService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() 
    {

        return "Hello World";
    }

    [WebMethod]
    public string GetFindingData(int studyId,int findingId)
    {
        //FindingPageObject findingPage = new FindingPageObject();
      
        StringBuilder text = new StringBuilder();
        FindingObject finding = new FindingObject();
        finding.FindingId.Value = findingId;
        finding.Load();
        if (finding.IsLoaded)
        {
            text.Append(finding.TextualTranscript.Value);
        }    
        return text.ToString();
    }
    [WebMethod]
    public int SaveFinding(int studyId, int findingId,int userId,string findingText)
    {
        return UpdateFinding(studyId, findingId, userId, findingText,false,false);
    }

    private int UpdateFinding(int studyId,int findingId, int userId, string findingText,bool isTran,bool removeAudioData)
    {
        FindingObject finding = new FindingObject();
        if (findingId > 0)
        {
            finding.FindingId.Value = findingId;
            finding.Load();
            if (finding.IsLoaded && removeAudioData)
            {
                //if(finding.TextualTranscript.Value.Equals(findingText) return
                finding.AudioData.Value = null;
            }
        }
        else
        {
            finding.StudyId.Value = studyId;
        }
        if (isTran)
        {
            finding.TranscriptUserId.Value = userId;
            finding.TranscriptionDate.Value = DateTime.Now;
        }
        finding.TextualTranscript.Value = findingText;
        finding.Save();
        return findingId;
    }

    private void UpdateStudy(int studyId, int findingId, int userId, string findingText, int status,bool removeAudioData)
    {
        bool isTran = false;
        if (status == Constants.StudyStatusTypes.PendingVerification) isTran = true;
        findingId = UpdateFinding(studyId,findingId, userId, findingText,isTran,removeAudioData);
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load();
        if (study.IsLoaded)
        {
            if (study.LatestFindingId.Value == null)
            {
                study.LatestFindingId.Value = findingId;
            }
            study.StudyStatusId.Value = status;
            study.Save(userId);
        }
    }

    [WebMethod]
    public void ApproveStudy(int studyId, int findingId, int userId, string findingText)
    {
        UpdateStudy(studyId, findingId, userId, findingText, Constants.StudyStatusTypes.Verified,true);
    }
    [WebMethod]
    public void MarkStudy(int studyId, int findingId, int userId, string findingText)
    {
        UpdateStudy(studyId, findingId, userId, findingText, Constants.StudyStatusTypes.PendingVerification,false);
    }

}

