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
using System.Collections.Generic;
using System.Text;
using RIS.RISLibrary.Utilities;

public partial class Common_TabControl : System.Web.UI.UserControl
{
    public string Selected = "Dashboard";
    private Dictionary<string, string> menuItems = new Dictionary<string, string>();
    private Dictionary<string, string> menuGroups = new Dictionary<string, string>();
    protected string LiList = "";
    protected void InitializeComponent()
    {
        string[] urls = Request.Url.AbsoluteUri.Split('/');
        string currentPage = urls[urls.Length - 1].Split('?')[0];
        
        menuItems.Add("Dashboard", "../SharedPages/Dashboard.aspx");
        menuItems.Add("Patients List", "../Radiologist/StudyList.aspx");

        if (Session[ParameterNames.Session.LoggedInUserRoleId] != null)
        {
            int roleId = (int)Session[ParameterNames.Session.LoggedInUserRoleId];
            if (roleId == Constants.Roles.Admin || roleId == Constants.Roles.ClientAdmin
                || roleId == Constants.Roles.HospitalAdmin)
            {
                menuItems.Add("User Administration", "../Admin/UsersList.aspx");
            }
            if (roleId == Constants.Roles.Admin || roleId == Constants.Roles.ClientAdmin)
            {
                menuItems.Add("Hospital Administration", "../Admin/HospitalsList.aspx");
            }
            if (roleId == Constants.Roles.ClientAdmin || roleId == Constants.Roles.ClientTechnologist)
            {
                menuItems.Add("Manual Exam", "../Technologist/AddStudy.aspx");
            }
            if (roleId == Constants.Roles.Radiologist)
            {
                menuItems.Add("Manage Templates", "../AdminPages/ManageTemplates.aspx");
            }
        }

        menuGroups.Add("Dashboard.aspx", "Dashboard");
        menuGroups.Add("StudyList.aspx", "Patients List");
        menuGroups.Add("AddUser.aspx", "User Administration");
        menuGroups.Add("UsersList.aspx", "User Administration");
        menuGroups.Add("HospitalsList.aspx", "Hospital Administration");
        menuGroups.Add("AddHospital.aspx", "Hospital Administration");
        menuGroups.Add("AddStudy.aspx", "Manual Exam");
        menuGroups.Add("DataSave.aspx", "Manual Exam");
        menuGroups.Add("ManageTemplates.aspx", "Manage Templates");
        menuGroups.Add("AddTemplate.aspx", "Manage Templates");

        StringBuilder lis = new StringBuilder();
        if (menuItems.Count > 0)
        {
            lis.Append("<div class='shadetabs'><ul>");
        }
        foreach (KeyValuePair<string, string> kvPair in menuItems)
        {
            lis.Append("<li ");
            if (menuGroups[currentPage] != null && kvPair.Key.Equals(menuGroups[currentPage]))
            {
                lis.Append("class='selected'");
            }
            lis.Append("><a href='").Append(kvPair.Value);
            /*if(Request[WebConstants.Request.DEPT_ORDER_ID] != null)
            {
                lis.Append("?").Append(WebConstants.Request.DEPT_ORDER_ID).Append("=").Append(Request[WebConstants.Request.DEPT_ORDER_ID]);
            }*/
            lis.Append("'>").Append(kvPair.Key).Append("</a></li>");            
        }
        if (menuItems.Count > 0)
        {
            lis.Append("</ul></div>");
        }
        LiList = lis.ToString();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    override protected void OnInit(EventArgs e)
    {
        InitializeComponent();
        base.OnInit(e); // be care of this
    }
    
}
