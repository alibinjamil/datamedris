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
using System.Text;
using System.Collections.Generic;

using RIS.RISLibrary.Database;
using RIS.RISLibrary.Objects.RIS;
using RIS.RISLibrary.Utilities;

using YuiNet.Util;
public partial class Radiologist_StudyList : AuthenticatedPage
{
    private double startPage = 1.0;
    private double endPage = 0.0;

    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        startPage = int.Parse(intStartPage.Value);
        endPage = startPage + WebConstants.Pages -1;
        if (IsPostBack == false)
        {          
            FillDDL();
            if (loggedInUserRoleId == 2)
            {
                FillTemplatesList();
            }
            //Session["DateFilter"] = 30;
            //Session["FromDate"] = null;
            //Session["ToDate"] = null;
        }
        if (hfCarryStatus.Value.ToString() != "x")
        {
            string labels = hfCarryStatus.Value.ToString();
            string[] labelsArray = labels.Split(',');
            foreach (string label in labelsArray)
            {
                    cblStatus.Items.FindByText(label).Selected = true;
            }
        }
        else 
        {
            cblStatus.Items.FindByText("[All]").Selected = true;
            statusText.Text = "[All]";

        }
        if (hfCarryModality.Value.ToString() != "x")
        {
            string labels = hfCarryModality.Value.ToString();
            string[] labelsArray = labels.Split(',');
            foreach (string label in labelsArray)
            {
                    cblModality.Items.FindByText(label).Selected = true;
            }
        }
        else
        {
            cblModality.Items.FindByText("[All]").Selected = true;
            modalityText.Text = "[All]";

        }
        ExecuteProcedure();
        ClearData();
        StringBuilder url = new StringBuilder();
        url.Append("http://").Append(Request.Url.Authority);
        if(Request.ApplicationPath.Length > 0)
            url.Append(Request.ApplicationPath).Append("/");
        url.Append("WebServices/");
        hfWebServicesHomeURL.Value = url.ToString();
        url.Append("FindingService.asmx");
        hfSURL.Value = url.ToString();
        hfLoggedInUserId.Value = loggedInUserId.ToString();
        hfLoggedInUserName.Value = loggedInUser.Name.Value.ToString();
        hfLoggedInUserRoleId.Value = loggedInUserRoleId.ToString();
    }
    private void ClearData()
    {
        hfPatientId.Value = "";
    }
    protected override bool IsPopUp()
    {
        return false;
    }
    private void FillDDL()
    {
        DatabaseUtility.BindModalitiesDDL("All", cblModality);
        
        DatabaseUtility.BindStudyStatusTypesDDL("All", cblStatus);
    }

    private void ExecuteProcedure()
    {
        StudyListModal modal;
       // if (Session["FromDate"] != null && Session["ToDate"] != null)
       // {
             //modal = new StudyListModal(int.Parse(currentPage.Value), int.Parse(sortBy.Value), isAsc.Value, hfPatientId.Value, tbPatientId.Text, tbName.Text,
             //int.Parse(ddlModality.SelectedValue), int.Parse(ddlStatus.SelectedValue), tbProcedure.Text, tbRadiologist.Text, tbPhysician.Text, (DateTime)Session["FromDate"] , (DateTime)Session["ToDate"], loggedInUserRoleId, loggedInUserId);
            
       // }
       // else
       // { 
        int count = 0;
        int count1 = 0;
        if (cblModality.Items[0].Selected == true)
        {
            modalityText.Text = cblModality.Items[0].Text;
            for (int i = 1; i < cblModality.Items.Count; i++)
            {
                if (cblModality.Items[i].Selected == true)
                    cblModality.Items[i].Selected = false;
            }
        }
        if (cblStatus.Items[0].Selected == true)
        {
            statusText.Text = cblStatus.Items[0].Text;
            for (int i = 1; i < cblStatus.Items.Count; i++)
            {
                if (cblStatus.Items[i].Selected == true)
                    cblStatus.Items[i].Selected = false;
            }
        }
        for (int i = 0; i < cblStatus.Items.Count ; i++)
        {
            if (cblStatus.Items[i].Selected==true)
            {
                count++;
            }
        }
        for (int i = 0;i < cblModality.Items.Count; i++)
        {
            if (cblModality.Items[i].Selected == true)
            {
                count1++;
            }
        }

        int[] StatusIds= new int[count];
        int[] ModalityIds = new int[count1];
        int index = 0;
        int index1=0;
        for(int i = 0; i < cblStatus.Items.Count;i++)
        {
            if (cblStatus.Items[i].Selected==true)
            {
                StatusIds[index] = int.Parse(cblStatus.Items[i].Value.ToString());
                index++;
            }
        }
        for (int i = 0;i < cblModality.Items.Count; i++)
        {
            if (cblModality.Items[i].Selected == true)
            {
                ModalityIds[index1] = int.Parse(cblModality.Items[i].Value.ToString());
                index1++;
            }
        }
        modal = new StudyListModal(int.Parse(currentPage.Value), int.Parse(sortBy.Value), isAsc.Value, hfPatientId.Value, tbPatientId.Text, tbName.Text, tbProcedure.Text, tbRadiologist.Text, tbPhysician.Text, int.Parse(ddlExamDate.SelectedValue) /*(int)Session["DateFilter"]*/, loggedInUserRoleId, loggedInUserId, StatusIds, ModalityIds);
      //  } 
        int totalRecords = modal.GetRecordCount();
        if (totalRecords == 0)
        {
            Session[ParameterNames.Session.ErrorMessage] = Messages.Information.NoRecordsFound;
        }
        else
        {
            BuildTable(modal.GetData(), totalRecords);
        }
    }

    private TableCell GetIndexCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        cell.VerticalAlign = VerticalAlign.Middle;
        StringBuilder url = new StringBuilder();
        url.Append(PagesFactory.GetUrl(PagesFactory.Pages.FindingReportPage));
        url.Append("?");
        url.Append(ParameterNames.Request.StudyId);
        url.Append("=");
        url.Append(studyList.StudyId);
        cell.Text = GetRepPopupText(url.ToString(), "Report", "R", studyList.StatusId, studyList.PatientRecordCount, studyList.PatientId, studyList.PatientName);
        return cell;
    }

    private TableCell GetPatientNameCell(StudyListPageObject studyList,int currentRow)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        StringBuilder text = new StringBuilder();
        text.Append("<script language=\"javascript\" type=\"text/javascript\">");
        text.Append("studyList[").Append(currentRow).Append("] = eval(");
        JsonUtilities.WriteObjectToJSON(studyList, text);
        //text.Append("hello';");
        text.Append(");</script>");
        text.Append(GetFindingText(studyList,currentRow));
        cell.Text = text.ToString();
        return cell;
    }
    private TableCell GetNormalCell(object data)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        cell.Text = data.ToString();
        return cell;
    }

    private TableCell GetPhysicianCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        string physician = null;

        if (studyList.Physician == null)
        {
            physician = "(None)";
        }
        else
        {
            physician = studyList.Physician;
        }
        StringBuilder url = new StringBuilder();
        url.Append(PagesFactory.GetUrl(PagesFactory.Pages.AddStudyGroupPage));
        url.Append("?");
        url.Append(ParameterNames.Request.StudyId);
        url.Append("=");
        url.Append(studyList.StudyId);
        cell.Text = GetPopupText(url.ToString(), "AddStudyGroup", physician);
        return cell;
    }

    private TableCell GetDisplayStudyCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        string physician = null;

        if (studyList.Physician == null)
        {
            physician = "(None)";
        }
        else
        {
            physician = studyList.Physician;
        }
        StringBuilder url = new StringBuilder();
        url.Append(PagesFactory.GetUrl(PagesFactory.Pages.DisplayStudyPage));
        url.Append("?");
        url.Append(ParameterNames.Request.StudyId);
        url.Append("=");
        url.Append(studyList.StudyId);
        cell.Text = GetPopupText(url.ToString(), "AddStudyGroup", physician);
        return cell;
    }
    
    private void BuildTable(List<StudyListPageObject> studies, int recCount)
    {
        int rowCount = 0;
        int currentPageInt = int.Parse(currentPage.Value);
        foreach (StudyListPageObject studyList in studies)
        {            
            TableRow currentRow = new TableRow();
            currentRow.Cells.Add(GetIndexCell(studyList));
            currentRow.Cells.Add(GetPatientNameCell(studyList,rowCount));
            currentRow.Cells.Add(GetNormalCell(studyList.PatientId));
            currentRow.Cells.Add(GetNormalCell(studyList.Status));
            currentRow.Cells.Add(GetNormalCell(studyList.StudyDate));
            currentRow.Cells.Add(GetNormalCell(studyList.Modality));
            currentRow.Cells.Add(GetNormalCell(studyList.Procedure));
            currentRow.Cells.Add(GetNormalCell(studyList.Radiologist));
            //currentRow.Cells.Add(GetPhysicianCell(studyList));
            currentRow.Cells.Add(GetDisplayStudyCell(studyList));
            currentRow.Cells.Add(GetNormalCell("&nbsp;"));            
            Table1.Rows.Add(currentRow);
            rowCount++;
        }
        TableRow emptyRow = new TableRow();
        emptyRow.Height = new Unit( (WebConstants.PageSize - rowCount) * 20);
        TableCell cell = new TableCell();
        cell.ColumnSpan = 10;
        emptyRow.Cells.Add(cell);
        Table1.Rows.Add(emptyRow);
        Table1.Rows.Add(AddNavigation(recCount,currentPageInt));
    }

    private TableRow AddNavigation(int recCount, int currentPage)
    {
        TableRow paginationRow = new TableRow();
        paginationRow.CssClass = "footerRow";
        TableCell recDisplayCell = new TableCell();
        recDisplayCell.HorizontalAlign = HorizontalAlign.Left;
        recDisplayCell.ColumnSpan = 3;
        recDisplayCell.CssClass = "currentPageOfTotal";
        recDisplayCell.Text = "&nbsp;&nbsp;&nbsp;Displaying " + (((currentPage - 1) * WebConstants.PageSize) + 1) + " to " + Math.Min((currentPage * WebConstants.PageSize), recCount) + " of " + recCount;
        paginationRow.Cells.Add(recDisplayCell);
        TableCell cell = new TableCell();
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.ColumnSpan = 7;

        if (recCount == 0) return null;
        double total_pages = System.Math.Ceiling((double)recCount * 1.0 / WebConstants.PageSize);
        intTotalPages.Value = total_pages.ToString();
        if (endPage > total_pages)
        {
            endPage = total_pages;
        }
        if (startPage > 1)
        {
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(-1)\"> &nbsp;&nbsp; First &nbsp;&nbsp;</span>";
  
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(-2)\"> &nbsp;&nbsp; Previous &nbsp;&nbsp;</span>";
        }
        for (double i = startPage; i <= endPage; i++)
        {
            cell.Text += "|<span class=\"";
            if (i == currentPage)
                cell.Text += "selectedPageLink";
            else
                cell.Text += "normalPageLink";

            cell.Text += "\" onclick=\"onPageLinkClick(" + i.ToString() + ")\">&nbsp;&nbsp;" + i.ToString() + "&nbsp;&nbsp;</span>";

        }
        if (endPage < total_pages)
        {
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(-3)\"> &nbsp;&nbsp; Next &nbsp;&nbsp;</span>";
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(-4)\"> &nbsp;&nbsp; Last &nbsp;&nbsp;</span>";

        }
        cell.Text += "|&nbsp;&nbsp;";
        paginationRow.Cells.Add(cell);
        return paginationRow;
    }
    public bool changeSessionValue()
    {
        Session["IsPageOpened"] = false;
        return true;
    }

    private string GetFindingText(StudyListPageObject studyList,int currentRow)
    {
        StringBuilder data = new StringBuilder();
        data.Append(ParameterNames.Request.StudyId);
        data.Append("=");
        data.Append(studyList.StudyId);
        data.Append("&");
        data.Append(ParameterNames.Request.FindingId);
        data.Append("=");
        data.Append(studyList.FindingId);
        StringBuilder text = new StringBuilder();
        text.Append("<a href=\"#\" onclick=\"showFindingDialog(");
        text.Append(currentRow);
        text.Append(",'");
        text.Append(data);
        text.Append("');\">");
        text.Append(studyList.PatientName);
        text.Append("</a>");
        return text.ToString();
    }

    /*private string GetPopupText(string url,string windowName, string text)
    {
        return "<a href=\"#\" onclick=\"window.showModalDialog('" + url + "',this,'dialogHeight=600px;dialogWidth=700px;menubar=0;resizable=0;scrollbars=0;status=0;titlebar=0;toolbar=0;');\">" + text + "</a>";
    }*/
    private string GetPopupText(string url, string windowName, string text)
    {
        return "<a href=\"#\" onclick=\"window.open('" + url + "','myWindow');\">" + text + "</a>";
    }
    private string GetRepPopupText(string url, string windowName, string text,int statusTypeId, int patRecCount,string externalPatientId,string patientName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table style='width:100%' CellPadding='0' CellSpacing='0'><tr><td class='imageColumn' style='width:50%;text-align:left'>");
        if (statusTypeId == Constants.StudyStatusTypes.PendingVerification)
        {
            sb.Append("&nbsp;<img class='linkImage' onclick=\"window.open('").Append(url).Append("','").Append(windowName).Append("','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/RedPrint.jpg' alt='Print Non-Verified Report'/>"); ;
        }
        else if (statusTypeId == Constants.StudyStatusTypes.Verified)
        {
            sb.Append("&nbsp;<img class='linkImage' onclick=\"window.open('").Append(url).Append("','").Append(windowName).Append("','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/GreenPrint.jpg' alt='Print Verified Report'/>"); ;
        }
        else
        {
            sb.Append("&nbsp;");
        }
        
        if (patRecCount > 1)
        {
            if ( statusTypeId == Constants.StudyStatusTypes.PendingVerification || statusTypeId == Constants.StudyStatusTypes.Verified)
            {
                sb.Append("</td><td style='width:50%;text-align:left'>");
                sb.Append("<img class='linkImage' onclick=\"searchPatient('" + externalPatientId + "');\" src='../Images/Plus.jpg' alt='Show all records for " + patientName + "'/>"); ;
            }
            else
            {
                sb.Append("</td><td class='imageColumn'>");
                sb.Append("<img class='linkImage' onclick=\"searchPatient('" + externalPatientId + "');\" src='../Images/Plus.jpg' alt='Show all records for " + patientName + "'/>"); ;
            }
        }
        else
            sb.Append("</td><td class='imageColumn'>");
        sb.Append("</td></tr></table>");
        return sb.ToString();
    }
    // Method to populate the drop down of templates
    private void FillTemplatesList()
    {
        RISDatabaseAccessLayer db = new RISDatabaseAccessLayer();
        string query = "select tTemplates.TemplateId,tTemplates.[Name] from tTemplates inner join tTemplateUsers on tTemplates.TemplateId=tTemplateUsers.TemplateId where tTemplateUsers.UserId=" + loggedInUserId.ToString();
        SqlConnection con = (SqlConnection)db.GetConnection();
        SqlCommand cmd = new SqlCommand(query, con);
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        da.SelectCommand = cmd;
        da.Fill(dt);

        //Populating Drop down list of templates
        ddlTemplates.DataSource = dt;
        ddlTemplates.DataTextField = "Name";
        ddlTemplates.DataValueField = "TemplateId";
        ddlTemplates.DataBind();
    }

    public int FindingId
    {
        get
        {
            return 0;
        }
    }

    public int StudyId
    {
        get
        {
            return 0;
        }
    }

    public int UserId
    {
        get
        {
            return loggedInUserId;
        }
    }

    public bool IsRadiologist
    {
        get
        {
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public bool IsTranscriptionist
    {
        get
        {
            if (loggedInUserRoleId == Constants.Roles.Transcriptionist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
