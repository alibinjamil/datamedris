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
using System.IO;
using System.Xml.Linq;
using System.Linq;

using RIS.RISLibrary.Database;

using RIS.RISLibrary.Utilities;
using RIS.Common;

using YuiNet.Util;
public partial class Radiologist_StudyList : StudyPage
{
    private double startPage = 1.0;
    private double endPage = 0.0;
    private static Dictionary<int, System.Drawing.Color> rowColors = null; 
    
    protected static System.Drawing.Color GetRowColor(int studyStatusTypeId)
    {
        if (rowColors == null)
        {
            rowColors = new Dictionary<int, System.Drawing.Color>();
            rowColors.Add(Constants.StudyStatusTypes.Dictated, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.MarkForRetranscription, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.New, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.PendingVerification, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.PreRelease, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.Qaed, System.Drawing.Color.FromArgb(255,255,204));
            rowColors.Add(Constants.StudyStatusTypes.Redictated, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.Rejected, System.Drawing.Color.FromArgb(204,204,153));
            rowColors.Add(Constants.StudyStatusTypes.Transcribed, System.Drawing.Color.White);
            rowColors.Add(Constants.StudyStatusTypes.Verified, System.Drawing.Color.White);
        }
        return rowColors[studyStatusTypeId];
    }

    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
       
        startPage = int.Parse(intStartPage.Value);
        endPage = startPage + WebConstants.Pages -1;
        if (IsPostBack == false)
        {          
            FillDDL();
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                lblClient.Visible = true;
                ddlClient.Visible = true;
                //ddlClient.DataSource
                List<RIS.Common.Client> clients = DatabaseContext.Clients.ToList();
                //Populating Drop down list of templates
                if (clients.Count > 0)
                {
                    ddlClient.DataSource = clients;
                    ddlClient.DataTextField = "Name";
                    ddlClient.DataValueField = "ClientId";
                    ddlClient.DataBind();
                }
            }
        }
        /*
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
         */
        if (Request["hfAction"] != null)
        {
            if (Request["hfAction"] == "release")
            {
                if (Request["releaseToRad"] != null)
                {
                    try
                    {
                        string[] studyIds = Request["releaseToRad"].Split(',');
                        foreach (string studyId in studyIds)
                        {
                            Study study = GetStudy(int.Parse(studyId));
                            if (study != null)
                            {
                                if (study.Hospital != null && study.ReferringPhysician != null)
                                {
                                    study.StudyStatusId = Constants.StudyStatusTypes.New;

                                    Log log = new Log();
                                    log.Study = study;
                                    log.ActionTime = DateTime.Now;
                                    log.Action = Constants.LogActions.ReleasedToRad;
                                    log.UserId = loggedInUserId;
                                    DatabaseContext.AddToLogs(log);
                                }
                                else
                                {
                                    SetErrorMessage("One or more exams could not be released to Radiologists as they have missing data");
                                }
                            }
                        }
                        DatabaseContext.SaveChanges();
                    }
                    catch (OptimisticConcurrencyException)
                    {
                        HandleConcurrencyException();
                    }
                }
            }
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
        hfLoggedInUserName.Value = loggedInUser.Name;
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
        BindModalities();

        BindStudyStatusTypes();
    }

    private void BindModalities()
    {
        ddlModality.DataSource = (from m in DatabaseContext.Modalities select m).ToList();
        ddlModality.DataTextField = "Name";
        ddlModality.DataValueField = "ModalityId";
        ddlModality.DataBind();
    }

    private void BindStudyStatusTypes()
    {
        ddlStatus.DataSource = (from s in DatabaseContext.StudyStatusTypes select s).ToList();
        ddlStatus.DataMember = "Status";
        ddlStatus.DataTextField = "Status";
        ddlStatus.DataValueField = "StudyStatusTypeId";
        ddlStatus.DataBind();
    }

    private void ExecuteProcedure()
    {
        Nullable<int> clientId = null;
        if (loggedInUserRoleId == Constants.Roles.ClientAdmin || loggedInUserRoleId == Constants.Roles.ClientTechnologist)
        {
            clientId = loggedInUserClientId;
        }
        else if (loggedInUserRoleId == Constants.Roles.Radiologist && ddlClient.SelectedIndex > 0)
        {
            clientId = int.Parse(ddlClient.SelectedValue);
        }
        StudyListModal modal = new StudyListModal(int.Parse(currentPage.Value), int.Parse(sortBy.Value), isAsc.Value, hfPatientId.Value, tbPatientId.Text, tbName.Text, int.Parse(ddlModality.SelectedValue), int.Parse(ddlStatus.SelectedValue), tbProcedure.Text, tbRadiologist.Text, tbPhysician.Text, int.Parse(ddlExamDate.SelectedValue) /*(int)Session["DateFilter"]*/, loggedInUserRoleId, loggedInUserId, clientId/*,loggedInUserHospitalId*/);
        /*
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
        }*/
        //modal = 
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
        cell.Text = GetRepPopupText(url.ToString(), "Report", "R", studyList);
        return cell;
    }
    private TableCell GetRejectionCell(StudyListPageObject studyList, int currentRow)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCell";
        cell.Text = cell.Text = "<img class='linkImage' src='../Images/delete.png' alt='Click to Reject this exam' title='Click to Reject this exam' onclick=\"openRejectionWindow(" + studyList.StudyId + "," + currentRow + ");\" />";
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
        text.Append(GetPatientNameText(studyList));
        cell.Text = text.ToString();
        return cell;
    }
    private TableCell GetNormalCell(object data,HorizontalAlign align)
    {
        TableCell cell = new TableCell();
        cell.HorizontalAlign = align;
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
        if (loggedInUserRoleId == Constants.Roles.Technologist)
        {
            StringBuilder url = new StringBuilder();
            url.Append(PagesFactory.GetUrl(PagesFactory.Pages.AddStudyGroupPage));
            url.Append("?");
            url.Append(ParameterNames.Request.StudyId);
            url.Append("=");
            url.Append(studyList.StudyId);
            HyperLink hl = new HyperLink();
            hl.Text = physician;
            hl.NavigateUrl = url.ToString();
            cell.Controls.Add(hl);
        }
        else
        {
            cell.Text = physician;
        }
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
        //zzTable1.Rows.AddAt(1,AddNavigation(recCount, currentPageInt));
        /*if (loggedInUserRoleId == Constants.Roles.ClientAdmin)
        {
            if (IsPostBack == false)
            {
                TableCell cell = new TableCell();
                cell.Text = "<input type='checkbox' onclick='selectAllClicked(this)'/>";
                cell.CssClass = "headingCellNoRight";
                cell.Width = Unit.Pixel(20);
                Table1.Rows[0].Cells[1].Width = Unit.Pixel(188);
                Table1.Rows[0].Cells.AddAt(0, cell);
            }
        }*/
        Table currentTable = null;
        if (loggedInUserRoleId == Constants.Roles.ClientAdmin 
            || loggedInUserRoleId == Constants.Roles.ClientTechnologist
            || loggedInUserRoleId == Constants.Roles.Admin)
        {
            currentTable = TableSU;
        }
        else
        {
            currentTable = Table1;
        }
        currentTable.Visible = true;
        foreach (StudyListPageObject studyList in studies)
        {            
            TableRow currentRow = new TableRow();
            currentRow.BackColor = GetRowColor(studyList.StatusId);
            //currentRow.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#EBEBEB");
            if (loggedInUserRoleId == Constants.Roles.ClientAdmin 
                || loggedInUserRoleId == Constants.Roles.Admin
                || loggedInUserRoleId == Constants.Roles.ClientTechnologist)
            {
                currentRow.Cells.Add(GetSelectCell(studyList));
            }
            else
            {
                //currentRow.Cells.Add(GetNormalCell("&nbsp;",HorizontalAlign.Left));
            }
            currentRow.Cells.Add(GetMultiRecordCell(studyList));
            currentRow.Cells.Add(GetPatientNameCell(studyList, rowCount));
            currentRow.Cells.Add(GetNormalCell(studyList.PatientId,HorizontalAlign.Left));
            currentRow.Cells.Add(GetNormalCell(studyList.Status,HorizontalAlign.Center));
            currentRow.Cells.Add(GetNormalCell(studyList.StudyDate,HorizontalAlign.Center));
            currentRow.Cells.Add(GetNormalCell(studyList.Modality,HorizontalAlign.Center));
            currentRow.Cells.Add(GetNormalCell(studyList.Procedure,HorizontalAlign.Left));
            currentRow.Cells.Add(GetNormalCell(studyList.Radiologist,HorizontalAlign.Left));
            currentRow.Cells.Add(GetNormalCell(studyList.Physician, HorizontalAlign.Left));
            //currentRow.Cells.Add(GetPhysicianCell(studyList));
            /*if ((loggedInUserRoleId == Constants.Roles.Radiologist && studyList.StatusId != Constants.StudyStatusTypes.Verified)  
                || studyList.StatusId == Constants.StudyStatusTypes.Rejected)
            {
                currentRow.Cells.Add(GetRejectionCell(studyList, rowCount));
            }
            else
            {
                currentRow.Cells.Add(GetNormalCell("&nbsp;", HorizontalAlign.Center));
            }*/
            if (loggedInUserRoleId == Constants.Roles.Radiologist)
            {
                if (studyList.StatusId == Constants.StudyStatusTypes.Verified)
                {
                    currentRow.Cells.Add(GetReviseExamCell(studyList, rowCount));
                }
                else
                {
                    currentRow.Cells.Add(GetDicationCell(studyList, rowCount));
                }
            }
            else if (studyList.StatusId != Constants.StudyStatusTypes.Verified 
                && (loggedInUserRoleId == Constants.Roles.ClientAdmin 
                || loggedInUserRoleId == Constants.Roles.Admin 
                || loggedInUserRoleId == Constants.Roles.ClientTechnologist
                || loggedInUserRoleId == Constants.Roles.HospitalAdmin))
            {
                currentRow.Cells.Add(GetEditCell(studyList,rowCount));
            }
            else
            {
                currentRow.Cells.Add(GetNormalCell("&nbsp;", HorizontalAlign.Center));
            }            
            currentRow.Cells.Add(GetReportCell(studyList));
            currentRow.Cells.Add(GetRadscaperCell(studyList));
            //currentRow.Cells.Add(GetDisplayStudyCell(studyList));
            //currentRow.Cells.Add(GetNormalCell("&nbsp;"));            
            currentTable.Rows.Add(currentRow);
            rowCount++;
        }
        /*TableRow emptyRow = new TableRow();
        emptyRow.Height = new Unit( (WebConstants.PageSize - rowCount) * 20);
        TableCell cell = new TableCell();
        cell.ColumnSpan = 11;
        emptyRow.Cells.Add(cell);
        Table1.Rows.Add(emptyRow);*/
        currentTable.Rows.Add(AddNavigation(recCount,currentPageInt));
    }

    private TableCell GetEditCell(StudyListPageObject studyList,int currentRow)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        cell.Text = "<img class='linkImage' src='../Images/edit.png' alt='Click to add/edit notes information' title='Click to add/edit notes information' onclick=\"openStudyEditWindow(" + studyList.StudyId + "," + currentRow + ");\" />";
        return cell;
    }
    private TableCell GetRadscaperCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        cell.Text = "<img class='linkImage' src='../Images/edoc_36.png' alt='Click to view Non-diagnostic image(s)' title='Click to view Non-diagnostic image(s)' onclick=\"openRadscaper('" + studyList.StudyId + "');\" >";
        return cell;
        
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
        cell.ColumnSpan = 11;

        if (recCount == 0) return null;
        int startPage = 0;
        int endPage = 0;
        if(currentPage % WebConstants.Pages == 0)
           startPage = ((currentPage - 1)/ WebConstants.Pages) * WebConstants.Pages + 1;
        else
            startPage = (currentPage/ WebConstants.Pages) * WebConstants.Pages + 1;
        if(currentPage % WebConstants.Pages == 0)
            endPage = (currentPage / WebConstants.Pages) * WebConstants.Pages;
        else
            endPage = (currentPage / WebConstants.Pages + 1) * WebConstants.Pages;
        int previousPage = endPage - WebConstants.Pages;
        int totalPages = int.Parse(System.Math.Ceiling((double)recCount * 1.0 / WebConstants.PageSize).ToString());
        bool showLast = true;
        if (endPage >= totalPages)
        {
            endPage = totalPages;
            showLast = false;
        }
        if (startPage > 1)
        {
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(1)\"> &nbsp;&nbsp; First " + WebConstants.PageSize + "&nbsp;&nbsp;</span>";
  
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(" + previousPage +")\"> &nbsp;&nbsp; Previous " + WebConstants.PageSize + "&nbsp;&nbsp;</span>";
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
        if (showLast)
        {
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(" + (endPage + 1) + ")\"> &nbsp;&nbsp; Next " + WebConstants.PageSize +"&nbsp;&nbsp;</span>";
            cell.Text += "|<span class=\"";
            cell.Text += "normalPageLink";
            cell.Text += "\" onclick=\"onPageLinkClick(" + totalPages + ")\"> &nbsp;&nbsp; Last " + WebConstants.PageSize +"&nbsp;&nbsp;</span>";

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
        /*data.Append("&");
        data.Append(ParameterNames.Request.FindingId);
        data.Append("=");
        data.Append(studyList.FindingId);*/
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

    private string GetPatientNameText(StudyListPageObject studyList)
    {
        if (loggedInUserRoleId == Constants.Roles.Radiologist)
        {
            StringBuilder text = new StringBuilder();
            text.Append("<a href=\"#\" onclick=\"invokeEFilm('");
            text.Append(studyList.OriginalPatientId).Append("','").Append(studyList.AccessionNumber).Append("');\">");
            text.Append(studyList.PatientName);
            text.Append("</a>");
            return text.ToString();
        }
        else
        {
            return studyList.PatientName;
        }
    }

    /*private string GetPopupText(string url,string windowName, string text)
    {
        return "<a href=\"#\" onclick=\"window.showModalDialog('" + url + "',this,'dialogHeight=600px;dialogWidth=700px;menubar=0;resizable=0;scrollbars=0;status=0;titlebar=0;toolbar=0;');\">" + text + "</a>";
    }*/
    private string GetPopupText(string url, string windowName, string text)
    {
        return "<a href=\"#\" onclick=\"window.open('" + url + "','myWindow');\">" + text + "</a>";
    }
    private TableCell GetDicationCell(StudyListPageObject studyList,int currentRow)
    {
        StringBuilder data = new StringBuilder();
        data.Append(ParameterNames.Request.StudyId);
        data.Append("=");
        data.Append(studyList.StudyId);
        /*data.Append("&");
        data.Append(ParameterNames.Request.FindingId);
        data.Append("=");
        data.Append(studyList.FindingId);*/

        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        cell.Text = "<img class='linkImage' onclick='showFindingDialog(" + currentRow + ",\"" + data.ToString() + "\");' alt='Click to Dictate' title='Click to Dictate' src='../Images/dictation_36.png'";
        return cell;
    }
    private TableCell GetReviseExamCell(StudyListPageObject studyList, int currentRow)
    {
        StringBuilder data = new StringBuilder();
        data.Append(ParameterNames.Request.StudyId);
        data.Append("=");
        data.Append(studyList.StudyId);
        /*data.Append("&");
        data.Append(ParameterNames.Request.FindingId);
        data.Append("=");
        data.Append(studyList.FindingId);*/

        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        cell.Text = "<img class='linkImage' onclick='showReviseExamDialog(" + currentRow + ",\"" + data.ToString() + "\");' alt='Click to Add a New Finding' title='Click to Add a New Finding' src='../Images/dictation_36.png'";
        return cell;
    }
    private TableCell GetReportCell(StudyListPageObject studyList)
    {
        StringBuilder url = new StringBuilder();
        url.Append(PagesFactory.GetUrl(PagesFactory.Pages.FindingReportPage));
        url.Append("?");
        url.Append(ParameterNames.Request.StudyId);
        url.Append("=");
        url.Append(studyList.StudyId);

        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        if (studyList.StatusId == Constants.StudyStatusTypes.PendingVerification)
        {
            cell.Text = "<img class='linkImage' onclick=\"window.open('" + url + "','Report','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/doc_red_36.png' alt='Click to view un-verified report' title='Click to view un-verified report'/>"; 
        }
        else if (studyList.StatusId == Constants.StudyStatusTypes.Verified)
        {
            cell.Text = "<img class='linkImage' onclick=\"window.open('" + url + "','Report','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/doc_green_36.png' alt='Click to view verified report' title='Click to view verified report'/>"; 
        }
        else
        {
            cell.Text = "&nbsp;";
        }
        return cell;        
    }
    private string GetRepPopupText(string url, string windowName, string text,StudyListPageObject studyList)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table style='width:100%' CellPadding='0' CellSpacing='0'><tr><td class='imageColumn' style='width:50%;text-align:left'>");
        if (studyList.AccessionNumber != null && studyList.AccessionNumber.Length > 0)
        {
            sb.Append("<img class='linkImage' onclick='invokeEFilm(\"").Append(studyList.PatientId).Append("\",\"").Append(studyList.AccessionNumber).Append("\");' src='../Images/efilm_36.png' alt='View exam in eFilm' title='View exam in eFilm'/>");
        }
        else
        {
            sb.Append("<img class='linkImage' src='../Images/eFilm_gray.JPG' alt='Incomplete data to invoke eFilm' title='Incomplete data to invoke eFilm'/>");
        }
        sb.Append("<img class='linkImage' src='../Images/radscaper.jpg' alt='View exam in Radscaper'  title='View exam in Radscaper' onclick=\"openRadscaper('").Append(studyList.StudyId).Append("');\" />");            
        if (studyList.StatusId == Constants.StudyStatusTypes.PendingVerification)
        {
            sb.Append("</td><td style='width:50%;text-align:left'>");
            sb.Append("<img class='linkImage' onclick=\"window.open('").Append(url).Append("','").Append(windowName).Append("','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/doc_red_36.png' alt='Print Non-Verified Report' title='Print Non-Verified Report'/>"); ;
        }
        else if (studyList.StatusId == Constants.StudyStatusTypes.Verified)
        {
            sb.Append("</td><td style='width:50%;text-align:left'>");
            sb.Append("<img class='linkImage' onclick=\"window.open('").Append(url).Append("','").Append(windowName).Append("','menubar=1,resizable=1,scrollbars=1,status=1,titlebar=1,toolbar=0');\" src='../Images/doc_green_36.png' alt='Print Verified Report' title='Print Verified Report'/>"); ;
        }
        else
        {
            sb.Append("&nbsp;");
        }
        
        sb.Append("</td></tr></table>");
        return sb.ToString();
    }
    private TableCell GetSelectCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        if (studyList.StatusId == Constants.StudyStatusTypes.Qaed)
        {
            cell.Text = "<input type='checkbox' value='" + studyList.StudyId + "' name='releaseToRad'/>";
        }
        else
        {
            cell.Text = "&nbsp;";
        }
        return cell;
    }
    private TableCell GetMultiRecordCell(StudyListPageObject studyList)
    {
        TableCell cell = new TableCell();
        cell.CssClass = "dataCellWhite";
        if (studyList.PatientRecordCount > 1)
        {
            cell.Text = "<img class='linkImage' onclick=\"searchPatient('" + studyList.PatientId + "');\" src='../Images/plus.gif' alt='Click to view all exams of " + studyList.PatientName + "' title='Click to view all exams of " + studyList.PatientName + "' />";
        }
        else
        {
            cell.Text = "&nbsp;";
        }
        return cell;
    }

    // Method to populate the drop down of templates

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


    protected void ddlClient_DataBound(object sender, EventArgs e)
    {
        ddlClient.Items.Insert(0,new ListItem("All","0"));
    }
    protected void ddlModality_DataBound(object sender, EventArgs e)
    {
        ddlModality.Items.Insert(0, new ListItem("All", "0"));
    }
    protected void ddlStatus_DataBound(object sender, EventArgs e)
    {
        ddlStatus.Items.Insert(0, new ListItem("All", "0"));
    }
}
