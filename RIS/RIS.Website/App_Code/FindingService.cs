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
    public int SaveFinding(int studyId, int findingId,int userId,string heading,string description,string impression,int studyStatusId)
    {
        return UpdateStudy(studyId, findingId, userId, heading, description, impression, studyStatusId, false);
    }

    private int UpdateFinding(int studyId,int findingId, int userId, string heading,string description,string impression,bool isTran,bool removeAudioData)
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
            //adding this code to put in radiologist is and name. 
            finding.AudioDate.Value = DateTime.Now;
            finding.AudioUserId.Value = userId;
            UserObject user = new UserObject();
            user.UserId.Value = userId;
            user.Load(userId);
            if (user.IsLoaded)
            {
                finding.AudioUserName.Value = user.Name.Value;
            }
        }
        if (isTran)
        {
            finding.TranscriptUserId.Value = userId;
            finding.TranscriptionDate.Value = DateTime.Now;
        }
        finding.TextualTranscript.Value = "<data><heading>" + heading + "</heading><description>" + description + "</description><impression>"
            + impression + "</impression></data>";
        finding.Save(userId);
        //very bad programming, but needs to be done for now. 
        return int.Parse(finding.FindingId.Value.ToString());
    }

    private int UpdateStudy(int studyId, int findingId, int userId, string heading, string description, string impression, int status, bool removeAudioData)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load();
        if (study.IsLoaded)
        {
            findingId = UpdateFinding(studyId, findingId, userId, heading, description, impression, false, removeAudioData);
            study.LatestFindingId.Value = findingId;
            study.StudyStatusId.Value = status;
            study.Save(userId);
            return findingId;
        }
        return findingId;
    }

    [WebMethod]
    public void ApproveStudy(int studyId, int findingId, int userId, string heading, string description, string impression)
    {
        StudyObject study = new StudyObject();
        study.StudyId.Value = studyId;
        study.Load(userId);
        if (study.IsLoaded)
        {
            UpdateStudy(studyId, findingId, userId, heading, description, impression, Constants.StudyStatusTypes.Verified, true);
            LogObject log = new LogObject();
            log.Action.Value = Constants.LogActions.VerifiedStudy;
            log.ActionTime.Value = DateTime.Now;
            log.PatientId.Value = study.PatientId.Value;
            log.StudyId.Value = study.StudyId.Value;
            log.UserId.Value = userId;
            log.Save();
        }
    }
    [WebMethod]
    public void MarkStudy(int studyId, int findingId, int userId, string heading, string description, string impression)
    {
        UpdateStudy(studyId, findingId, userId, heading,description,impression, Constants.StudyStatusTypes.PendingVerification,false);
    }

}

