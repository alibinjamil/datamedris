<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="SelectRole.aspx.cs" Inherits="SharedPages_SelectRole" Title="DataMed | Radiology Information System | Select Role" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
        <tr>
            <td>There are multiple user roles assigned to this account. 
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>Please select the Role you want to login as for this session. 
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlRoles" runat="server" CssClass="dropDownListStyle">
                </asp:DropDownList>
                <asp:Button ID="btnProceed" runat="server" Text="Proceed" OnClick="btnProceed_Click" CssClass="buttonStyle"/></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>

</asp:Content>

