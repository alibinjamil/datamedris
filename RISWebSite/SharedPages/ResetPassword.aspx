<%@ Page Language="C#" MasterPageFile="~/Common/Login.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="SharedPages_ResetPassword" Title="DataMed | Radiology Information System | Reset Password" %>

<%@ Register src="../Common/SecretQuestions.ascx" tagname="SecretQuestions" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Please reset your password</h1>
        <table width="100%" border="0" cellspacing="0" cellpadding="3"> 
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">Secret Question:</font></div></td> 
        <td><uc1:SecretQuestions ID="SecretQuestions1" runat="server" />
          </td> 
      </tr>
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">Answer:</font></div></td> 
        <td>
            <asp:TextBox ID="tbAnswer" runat="server" Width="149px" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="tbAnswer"></asp:RequiredFieldValidator>            
        </td> 
      </tr> 
       
        
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">New Password:</font></div></td> 
        <td>
            <asp:TextBox ID="tbPassword" runat="server" Width="149px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="*" ControlToValidate="tbPassword"></asp:RequiredFieldValidator>                        
          </td> 
      </tr>
      <tr> 
        <td><div align="right"><font size="-1" face="Verdana, Arial, Helvetica, sans-serif">Answer:</font></div></td> 
        <td>
            <asp:TextBox ID="tbConfirmPassword" runat="server" Width="149px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="*" ControlToValidate="tbConfirmPassword"></asp:RequiredFieldValidator>                                    
        </td> 
      </tr> 
       
      <tr> 
        <td>&nbsp;</td> 
        <td>
            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" 
                CssClass="buttonStyle" onclick="btnResetPassword_Click" />
            
        </td> 
         
      </tr>
        </asp:Panel>
        
    </table>

</asp:Content>

