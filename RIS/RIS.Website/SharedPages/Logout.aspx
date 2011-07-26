<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="SharedPages_Logout" Title="DataMed | Radiology Information System | Bye" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="align:center;">
Are you sure you want to Logout from Data Med RIS ?<br />
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="buttonStyle"/>&nbsp;&nbsp;<asp:Button ID="btnCancel"
        runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="buttonStyle"/></div>
</asp:Content>

