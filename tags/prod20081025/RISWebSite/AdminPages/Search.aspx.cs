using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Database;
using RIS.RISLibrary.Utilities;

public partial class AdminPages_Search : System.Web.UI.Page
{
    public static class Params
    {
        public static string UserId = "UserId";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                dtFrom.Day=1;
                dtFrom.Month=1;
                dtFrom.Year=2000;

                dtTo.Day=DateTime.Today.Day;
                dtTo.Month=DateTime.Today.Month;
                dtTo.Year=DateTime.Today.Year;

            }
           
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    
protected void  btnSearch_Click(object sender, EventArgs e)
{
    try
    {
        string whereClause = "";
        DataTable dtResults = new DataTable();
        whereClause += " and Convert(varchar(10),tlog.ActionTime,101) between '" + RISUtility.GetUSADate(DateTime.Parse(dtFrom.Month + "/" + dtFrom.Day + "/" + dtFrom.Year)) + "' and '" + RISUtility.GetUSADate(DateTime.Parse(dtTo.Month + "/" + dtTo.Day + "/" + dtTo.Year)) + "'";
        if (!tbPatientName.Text.Trim().Equals(""))
            whereClause += " and tPatients.[Name] ='" + tbPatientName.Text.Trim() + "'";
        if (!tbPatientId.Text.Trim().Equals(""))
            whereClause += " and tPatients.ExternalPatientId =" + tbPatientId.Text.Trim();
        if (!tbStudyInstance.Text.Trim().Equals(""))
            whereClause += " and tstudies.StudyInstance ='" + tbStudyInstance.Text.Trim() + "'";
        if (!tbUserName.Text.Trim().Equals(""))
            whereClause += " and  tusers.Name='" + tbUserName.Text.Trim() + "'";
        if (!tbLoginName.Text.Trim().Equals(""))
            whereClause += " and  tusers.loginname='" + tbLoginName.Text.Trim() + "'";
        if (ddlLogOptions.SelectedIndex > 0)
            whereClause += " and  tLog.Action='" + ddlLogOptions.Text.Trim() + "'";
        Label1.Text = whereClause;
    }
    catch(Exception ex)
    {
        lblError.Text = ex.Message;
    }
}
    protected void gvResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    
    }
    private DataTable GetData()
    {
        string whereClause = "";
        DataTable dtResults = new DataTable();
        whereClause += " and Convert(varchar(10),tlog.ActionTime,101) between '" + RISUtility.GetUSADate(DateTime.Parse(dtFrom.Month + "/" + dtFrom.Day + "/" + dtFrom.Year)) + "' and '" + RISUtility.GetUSADate(DateTime.Parse(dtTo.Month + "/" + dtTo.Day + "/" + dtTo.Year)) + "'";
        if (!tbPatientName.Text.Trim().Equals(""))
            whereClause += " and tPatients.[Name] ='" + tbPatientName.Text.Trim() + "'";
        if (!tbPatientId.Text.Trim().Equals(""))
            whereClause += " and tPatients.ExternalPatientId =" + tbPatientId.Text.Trim();
        if (!tbStudyInstance.Text.Trim().Equals(""))
            whereClause += " and tstudies.StudyInstance ='" + tbStudyInstance.Text.Trim() + "'";
        if (!tbUserName.Text.Trim().Equals(""))
            whereClause += " and  tusers.Name='" + tbUserName.Text.Trim() + "'";
        if (!tbLoginName.Text.Trim().Equals(""))
            whereClause += " and  tusers.loginname='" + tbLoginName.Text.Trim() + "'";
        if (ddlLogOptions.SelectedIndex > 0)
            whereClause += " and  tLog.Action='" + ddlLogOptions.Text.Trim() + "'";
        dtResults = RISProcedureCaller.GetPatientInfo(whereClause);
        return dtResults;
    }
    private void BindData()
    {
        string whereClause = "";
        DataTable dtResults = new DataTable();
        whereClause += " and Convert(varchar(10),tlog.ActionTime,101) between '" + RISUtility.GetUSADate(DateTime.Parse(dtFrom.Month + "/" + dtFrom.Day + "/" + dtFrom.Year)) + "' and '" + RISUtility.GetUSADate(DateTime.Parse(dtTo.Month + "/" + dtTo.Day + "/" + dtTo.Year)) + "'";
        if (!tbPatientName.Text.Trim().Equals(""))
            whereClause += " and tPatients.[Name] ='" + tbPatientName.Text.Trim() + "'";
        if (!tbPatientId.Text.Trim().Equals(""))
            whereClause += " and tPatients.ExternalPatientId =" + tbPatientId.Text.Trim();
        if (!tbStudyInstance.Text.Trim().Equals(""))
            whereClause += " and tstudies.StudyInstance ='" + tbStudyInstance.Text.Trim() + "'";
        if (!tbUserName.Text.Trim().Equals(""))
            whereClause += " and  tusers.Name='" + tbUserName.Text.Trim() + "'";
        if (!tbLoginName.Text.Trim().Equals(""))
            whereClause += " and  tusers.loginname='" + tbLoginName.Text.Trim() + "'";
        if (ddlLogOptions.SelectedIndex > 0)
            whereClause += " and  tLog.Action='" + ddlLogOptions.Text.Trim() + "'";
        dtResults = RISProcedureCaller.GetPatientInfo(whereClause);
        if (dtResults.Rows.Count > 0)
        {
            gvResult.DataSource = dtResults;
            gvResult.DataBind();
        }
        else
        {
            gvResult.DataSource = null;
            gvResult.DataBind();
        }
    }
}
  