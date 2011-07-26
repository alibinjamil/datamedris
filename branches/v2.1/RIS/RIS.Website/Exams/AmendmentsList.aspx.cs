using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Xml.Linq;

using RIS.Common;
public partial class Exams_AmendmentsList : StudyPage
{
    protected override bool IsPopUp()
    {
        return false;
    }
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Study study = GetStudy();
            if (study != null)
            {
                lblExamDate.Text = study.StudyDate.Value.ToShortDateString();
                if (study.Modality != null)
                {
                    lblModality.Text = study.Modality.Name;
                }

                lblPatientId.Text = study.ExternalPatientId;
                lblPatientName.Text = study.PatientName;

                if (study.StudyStatusType != null)
                {
                    lblStatus.Text = study.StudyStatusType.Status;
                }

                if (study.ReferringPhysician != null)
                {
                    lblPhysician.Text = study.ReferringPhysician.Name;
                }

                if (study.Procedure != null)
                {
                    lblProcedure.Text = study.Procedure.Name;
                }

                if (study.Radiologist != null)
                {
                    lblRadiologist.Text = study.Radiologist.Name;
                }

                List<Study> parents = study.GetParents();
                gvAmendmentsList.DataSource = (from s in parents
                                               where s.Amendment != null
                                               orderby s.ReportDate descending
                                               select new
                                               {
                                                   Amendment = s.Amendment,
                                                   ReportDate = s.ReportDate,
                                                   RadiologistName = s.Radiologist.Name
                                               });
                gvAmendmentsList.DataBind();
            }
        }
    }
}