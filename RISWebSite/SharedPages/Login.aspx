<%@ Page Language="C#" MasterPageFile="~/Common/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SharedPages_Login" Title="DataMed | Radiology Information System | Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width=100%>
        <tr>
            <td valign=middle align=center>
                <table width=100%>
                    <tr>
                        <td width="40%" align="right" class="heading">Login Name:</td>
                        <td align="left">
                            <asp:TextBox ID="tbLoginName" runat="server" CssClass="textBoxStyle" Width="148px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="height: 26px">Password:</td>
                        <td align="left" style="height: 26px">
                            <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="149px" CssClass="textBoxStyle"></asp:TextBox></td>
                    </tr>                    
                    <tr>
                        <td align="right" class="heading">
                            &nbsp;</td>
                        <td align="left">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click" CssClass="buttonStyle" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

