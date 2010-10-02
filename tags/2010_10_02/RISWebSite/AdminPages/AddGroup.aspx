<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddGroup.aspx.cs" Inherits="AdminPages_AddGroup" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <table class="dataEntryTable">
        <tr>
            <td class="heading" align="right">Group Name:
            </td>
            <td align="left">
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbName"
                    Display="Dynamic" ErrorMessage="Name is missing" ValidationGroup="Add"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="heading" align="right">Group Description:
            </td>
            <td align="left">
                <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox></td>
            <td style="text-align: left">
                <asp:RequiredFieldValidator ID="rfvLoginName" runat="server" ControlToValidate="tbDesc"
                    Display="Dynamic" ErrorMessage="Description is missing" ValidationGroup="Add"></asp:RequiredFieldValidator></td>
        </tr>
          <tr>
              <td class="heading" align="left">
                  Add Users:</td>
              <td>
              </td>
              <td>
              </td>
          </tr>
          <tr>
              <td class="heading">
                  <asp:ListBox ID="lbUsers" runat="server" Width="150px" SelectionMode="Multiple" DataSourceID="odsUserGroups" DataTextField="LoginName" DataValueField="UserId"></asp:ListBox></td>
              <td>
                  <asp:Button ID="btnAddUser" runat="server" Text="<< Add" Width="100px" OnClick="btnAddRole_Click" /><br />
                  <asp:Button ID="btnRemoveUser" runat="server" Text="Remove >>" Width="100px" OnClick="btnRemoveRole_Click" /></td>
              <td>
                  <asp:ListBox ID="lbOtherUsers" runat="server" SelectionMode="Multiple" Width="150px" DataSourceID="odsUserNotGroups" DataTextField="LoginName" DataValueField="UserId"></asp:ListBox></td>
          </tr>
                  
    </table>
    <asp:ObjectDataSource ID="odsUserGroups" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsersByGroup" TypeName="UsersTableAdapters.tUsersTableAdapter">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="groupId" QueryStringField="groupId"
                Type="Int32" />
            <asp:SessionParameter DefaultValue="0" Name="clientId" SessionField="loggedInUserClientId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsUserNotGroups" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetUsersNotForGroup" TypeName="UsersTableAdapters.tUsersTableAdapter">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="groupId" QueryStringField="groupId"
                Type="Int32" />
            <asp:SessionParameter DefaultValue="0" Name="clientId" SessionField="loggedInUserClientId"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" ValidationGroup="Add" />

</asp:Content>

