<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="SharedPages_Dashboard" Title="DataMed | Radiology Information System | Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <div class="dashboard" style="float:left;width:205px;" >
        <div class="dashboardHeader">Screens</div>
        <div class="dashboardBody">
            <div><asp:HyperLink ID="hlAddUser" runat="server" NavigateUrl="~/Admin/AddUser.aspx" Visible="false">Add a new User</asp:HyperLink></div>
            <div><asp:HyperLink ID="hlViewUser" runat="server" NavigateUrl="~/Admin/UsersList.aspx" Visible="false">View Users List</asp:HyperLink></div>
            <div><asp:HyperLink ID="hlAddHospital" runat="server" NavigateUrl="~/Admin/AddHospital.aspx" Visible="false">Add a Hospital</asp:HyperLink></div>
            <div><asp:HyperLink ID="hlViewHospital" runat="server" NavigateUrl="~/Admin/HospitalsList.aspx" Visible="false">View Hospitals List</asp:HyperLink></div>
            <div><asp:HyperLink ID="hlTemplateList" runat="server" NavigateUrl="~/AdminPages/ManageTemplates.aspx" Visible="false">View Templates List</asp:HyperLink></div>
            <div><asp:HyperLink ID="hlAddTemplate" runat="server" NavigateUrl="~/AdminPages/AddTemplate.aspx" Visible="false">Add Template</asp:HyperLink></div>

        </div>
    </div>
    <div style="float:left"></div>
    <div style="clear:both"></div>
</div>
</asp:Content>