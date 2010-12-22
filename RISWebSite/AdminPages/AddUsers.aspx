<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddUsers.aspx.cs" Inherits="AdminPages_AddUsers" Title="DataMed | Radiology Information System | Add Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   

<div align="center">
 
      <table class="dataEntryTable">
          <tr>
              <td align="right" class="heading">
              </td>
              <td align="left">
                  <asp:Label ID="lblError" runat="server" Width="132px"></asp:Label></td>
              <td>
                <asp:Label ID="lbUserId" runat="server" Width="0px" Visible="False"></asp:Label></td>
          </tr>
        <tr>
            <td class="heading" align="right">Login Name:
            </td>
            <td align="left">
                <asp:TextBox ID="tbLoginName" runat="server"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="rfvLoginName" runat="server" ControlToValidate="tbLoginName"
                    Display="Dynamic" ErrorMessage="Login Name is missing"></asp:RequiredFieldValidator></td>
        </tr>
          <tr>
              <td align="right" class="heading">
                  Password:</td>
              <td align="left">
                  <asp:TextBox ID="tbPassword" runat="server"></asp:TextBox></td>
              <td style="text-align: left">
                  <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword"
                      Display="Dynamic" ErrorMessage="Password is missing"></asp:RequiredFieldValidator></td>
          </tr>
          <tr>
              <td align="right" class="heading" style="height: 26px">
                  Confirm Password:</td>
              <td align="left" style="height: 26px">
                  <asp:TextBox ID="tbConfirmPassword" runat="server"></asp:TextBox></td>
              <td style="text-align: left; height: 26px;">
                  <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="tbConfirmPassword"
                      Display="Dynamic" ErrorMessage="Confirm Password is missing"></asp:RequiredFieldValidator>
                  <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="tbPassword"
                      ControlToValidate="tbConfirmPassword" Display="Dynamic" ErrorMessage="Both password must be same"></asp:CompareValidator></td>
          </tr>
          <tr>
              <td align="right" class="heading">
                  First Name:</td>
              <td align="left">
                  <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox></td>
              <td style="text-align: left">
                  <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbFirstName"
                      Display="Dynamic" ErrorMessage="First Name is missing"></asp:RequiredFieldValidator></td>
          </tr>
          <tr>
              <td align="right" class="heading">
                  Last Name:</td>
              <td align="left">
                  <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox></td>
              <td style="text-align: left">
                  <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="tbLastName"
                      Display="Dynamic" ErrorMessage="Last Name is missing"></asp:RequiredFieldValidator></td>
          </tr>
          <tr>
              <td align="right" class="heading">
                  Is Active:</td>
              <td align="left">
                  <asp:CheckBox ID="cbIsActive" runat="server" /></td>
              <td>
              </td>
          </tr>
          <tr>
              <td class="heading" align="left">
                  Define Roles:</td>
              <td>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td class="heading">
                  <asp:ListBox ID="lbUserRoles" runat="server" Width="150px" SelectionMode="Multiple"></asp:ListBox></td>
              <td>
                  <asp:Button ID="btnAddRole" runat="server" Text="<< Add" Width="100px" OnClick="btnAddRole_Click" /><br />
                  <asp:Button ID="btnRemoveRole" runat="server" Text="Remove >>" Width="100px" OnClick="btnRemoveRole_Click" /></td>
              <td>
                  <asp:ListBox ID="lbOtherRoles" runat="server" SelectionMode="Multiple" Width="150px"></asp:ListBox></td>
          </tr>
          <tr>
              <td class="heading">
              </td>
              <td>
                  &nbsp;</td>
              <td>
              </td>
          </tr>
          <tr>
              <td class="heading">
              </td>
              <td>
                  <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" /></td>
              <td>
              </td>
          </tr>
      </table>
    &nbsp;

</div>
</asp:Content>

