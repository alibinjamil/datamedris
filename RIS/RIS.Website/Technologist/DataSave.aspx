<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="DataSave.aspx.cs" Inherits="Technologist_DataSave" Title="DataMed | Radiology Information System | Add more data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
Do you want to enter another exam for <asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label> ?
    <br />
<br />
<asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="buttonStyle" OnClick="btnYes_Click" Width="100px"/>&nbsp;&nbsp;&nbsp;<asp:Button ID="btnNo"
        runat="server" Text="No" CssClass="buttonStyle" OnClick="btnNo_Click" Width="100px"/>
    <asp:HiddenField ID="lblPatientId" runat="server" />
</asp:Content>
