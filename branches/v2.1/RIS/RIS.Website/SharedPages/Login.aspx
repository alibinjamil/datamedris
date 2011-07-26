<%@ Page Language="C#" MasterPageFile="~/Common/Login.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SharedPages_Login" Title="DataMed | Radiology Information System | Welcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table width="100%" border="0" cellspacing="0" cellpadding="3"> 
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">User ID:</font></div></td> 
        <td><asp:TextBox ID="tbLoginName" runat="server" Width="148px"></asp:TextBox></td> 
      </tr> 
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">Password:</font></div></td> 
        <td><asp:TextBox ID="tbPassword" runat="server" TextMode="Password" Width="149px" ></asp:TextBox></td> 
      </tr> 
      <tr> 
        <td>&nbsp;</td> 
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click" CssClass="buttonStyle" />
            <a href="ForgotPassword.aspx">Forgot Password?</a>
        </td> 
      </tr> 
    </table>
</asp:Content>

