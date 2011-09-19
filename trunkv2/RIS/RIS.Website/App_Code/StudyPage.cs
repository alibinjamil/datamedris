using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using RIS.Common;
/// <summary>
/// This class assumes that a StudyId has been passed to this page and has a function to load that study
/// </summary>
public abstract class StudyPage : AuthenticatedPage
{
   
	public StudyPage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    protected Study GetStudy()
    {
        Study study = null;
        if(Request[ParameterNames.Request.StudyId] != null)
        {
            int studyId = int.Parse(Request[ParameterNames.Request.StudyId]);
            study = GetStudy(studyId);
        }
        return study;
    }

    protected Study GetStudy(int studyId)
    {
        return (from s in DatabaseContext.Studies
                where s.StudyId == studyId
                select s).FirstOrDefault();
    }
    protected void HandleConcurrencyException()
    {
        HttpContext.Current.Session[ParameterNames.Session.ExceptionString] = "Data concurrency issue, Study has been saved before you could make your changes. Please refresh your screen";
        Response.Redirect("~/SharedPages/ErrorPage.aspx");
    }

    /*protected List<Study> GetAllParents(int studyId)
    {
        List<Study> parents = new List<Study>();
        Study study = GetStudy(studyId);
        while (study != null)
        {
            parents.Add(study);
            study = study.ParentStudy;
        }
        return parents;
    }

    protected List<Study> GetAllParents()
    {
        List<Study> parents = new List<Study>();
        Study study = GetStudy();
        while (study != null)
        {
            parents.Add(study);
            study = study.ParentStudy;
        }
        return parents;
    }*/
}