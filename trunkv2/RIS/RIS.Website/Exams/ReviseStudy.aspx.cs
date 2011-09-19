using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using RIS.Common;
using RIS.RISLibrary.Utilities;
using System.Data;

public partial class Exams_ReviseStudy : StudyPage
{
    protected override bool IsPopUp()
    {
        return true;
    }
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        
    }

    
    protected void btnRevise_Click(object sender, EventArgs e)
    {
        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            Study study = GetStudy();
            if (study != null)
            {
                // we are making sure there are just two level of studies
                try
                {
                    Study newStudy = new Study();
                    newStudy.ParentStudyId = study.StudyId;
                    /*if(study.OriginalStudyId != null)
                    {
                        newStudy.OriginalStudyId = study.OriginalStudyId;
                    }
                    else
                    {
                        newStudy.OriginalStudyId = study.StudyId;
                    }
                    //parent study has the max revision number. 
                    if (study.RevisionNumber.HasValue)
                    {
                        study.RevisionNumber++;
                    }
                    else
                    {
                        study.RevisionNumber = 1;
                    }
                    newStudy.RevisionNumber = study.RevisionNumber;*/

                    newStudy.StudyInstance = study.StudyInstance;
                    newStudy.StudyDate = study.StudyDate;
                    newStudy.ProcedureId = study.ProcedureId;
                    newStudy.PatientDOB = study.PatientDOB;
                    newStudy.PatientGender = study.PatientGender;
                    newStudy.PatientName = study.PatientName;
                    newStudy.OriginalPatientId = study.OriginalPatientId;
                    newStudy.ExternalPatientId = study.ExternalPatientId;
                    newStudy.ReferringPhysicianId = study.ReferringPhysicianId;
                    newStudy.PatientWeight = study.PatientWeight;
                    newStudy.ModalityId = study.ModalityId;
                    newStudy.StationId = study.StationId;
                    newStudy.StudyStatusId = Constants.StudyStatusTypes.New;
                    newStudy.CreatedBy = loggedInUserId;
                    newStudy.CreationDate = DateTime.Now;
                    newStudy.LastUpdateDate = DateTime.Now;
                    newStudy.LastUpdatedBy = loggedInUserId;
                    newStudy.IsManual = study.IsManual;
                    newStudy.AccessionNumber = study.AccessionNumber; //if we do not set AccessIOn Number then the study will not open in eFilm
                    newStudy.HospitalId = study.HospitalId;
                    newStudy.ClientId = study.ClientId;
                    newStudy.TechComments = study.TechComments;

                    newStudy.Heading = study.Heading;
                    newStudy.Description = study.Description;
                    newStudy.Impression = study.Impression;
                    newStudy.BodyPartId = study.BodyPartId;
                    newStudy.TemplateId = study.TemplateId;

                    foreach (Series series in study.Series)
                    {
                        Series newSeries = new Series();
                        newSeries.SeriesInstance = series.SeriesInstance;
                        newSeries.SeriesNumber = series.SeriesNumber;
                        newSeries.SeriesDate = series.SeriesDate;
                        newSeries.Description = series.Description;
                        newSeries.ModalityDetailId = series.ModalityDetailId;
                        newSeries.PatientPosition = series.PatientPosition;
                        newSeries.Contrast = series.Contrast;
                        newSeries.BodyPartExamined = series.BodyPartExamined;
                        newSeries.ProtocolName = series.ProtocolName;
                        newSeries.FrameOfReference = series.FrameOfReference;
                        newSeries.CreatedBy = loggedInUserId;
                        newSeries.CreationDate = DateTime.Now;
                        newSeries.LastUpdateDate = DateTime.Now;
                        newSeries.LastUpdatedBy = loggedInUserId;
                        foreach (RIS.Common.Image image in series.Images)
                        {
                            RIS.Common.Image newImage = new RIS.Common.Image();
                            newImage.ImageInstance = image.ImageInstance;
                            newImage.ImageClassUI = image.ImageClassUI;
                            newImage.ImageNumber = image.ImageNumber;
                            newImage.ImageDate = image.ImageDate;
                            newImage.EchoNumber = image.EchoNumber;
                            newImage.NumberOfFrames = image.NumberOfFrames;
                            newImage.AcquiredDate = image.AcquiredDate;
                            newImage.SliceLocation = image.SliceLocation;
                            newImage.NumberOfSamples = image.NumberOfSamples;
                            newImage.PhotoMetric = image.PhotoMetric;
                            newImage.Rows = image.Rows;
                            newImage.Columns = image.Columns;
                            newImage.BitsStored = image.BitsStored;
                            newImage.Path = image.Path;
                            newImage.DeviceName = image.DeviceName;
                            newImage.CreatedBy = loggedInUserId;
                            newImage.CreationDate = DateTime.Now;
                            newImage.LastUpdateDate = DateTime.Now;
                            newImage.LastUpdatedBy = loggedInUserId;
                            newSeries.Images.Add(newImage);
                        }
                        newStudy.Series.Add(newSeries);
                    }
                    newStudy.IsLatest = true;
                    study.IsLatest = false;
                    study.LastUpdateDate = DateTime.Now;
                    study.LastUpdatedBy = loggedInUserId;

                    Log newLog = new Log();
                    newLog.UserId = loggedInUserId;
                    newLog.Study = newStudy;
                    newLog.Action = Constants.LogActions.Created;
                    newLog.ActionTime = DateTime.Now;

                    Log log = new Log();
                    log.UserId = loggedInUserId;
                    log.Study = study;
                    log.Action = Constants.LogActions.Revised;
                    log.ActionTime = DateTime.Now;

                    DatabaseContext.AddToLogs(newLog);
                    DatabaseContext.AddToLogs(log);
                    DatabaseContext.AddToStudies(newStudy);
                    DatabaseContext.SaveChanges();
                    ClientScript.RegisterStartupScript(this.GetType(), "CloseFinding", "parent.document.aspnetForm.submit();", true);
                }
                catch (OptimisticConcurrencyException)
                {
                    HandleConcurrencyException();
                }
                //Response.Redirect("~/Exams/EditFinding.aspx?" + ParameterNames.Request.StudyId + "=" + newStudy.StudyId);
            }
        }
    }
}