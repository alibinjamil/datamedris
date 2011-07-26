<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" Inherits="AdminPages_ManageUsers" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
<div align="center">
 
      <table class="dataEntryTable">
          <tr>
              <td align="center" >
      <asp:GridView ID="GridView1" AllowSorting="True" AllowPaging="True" Runat="server"
        DataSourceID="odsClients" DataKeyNames="UserId"
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" PageSize="20" Width="100%">
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="~/AdminPages/AddUsers.aspx?UserId={0}"
                NavigateUrl="~/AdminPages/AddUsers.aspx" Text="Edit" />
          <asp:BoundField ReadOnly="True" HeaderText="ID" DataField="UserId" SortExpression="UserId" />
          <asp:BoundField ReadOnly="True" HeaderText="Login Name" DataField="LoginName" SortExpression="LoginName" />
          <asp:BoundField HeaderText="Password" DataField="Password" SortExpression="Password" />
          <asp:BoundField HeaderText="Name" DataField="Name" SortExpression="Name" />
          <asp:BoundField HeaderText="Is Active" DataField="IsActive" SortExpression="IsActive" />
        </Columns>
          <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
          <RowStyle BackColor="Silver" ForeColor="#333333" />
          <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
          <PagerStyle BackColor="Gray" ForeColor="#333333" HorizontalAlign="Center" />
          <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="White" />
      </asp:GridView>
      </td>
          </tr>
          <tr>
              <td align="left" style="width: 526px">
                  &nbsp;<asp:ObjectDataSource ID="odsClients" runat="server" SelectMethod="GetUsersByClient"
                      TypeName="UsersTableAdapters.tUsersTableAdapter" OldValuesParameterFormatString="original_{0}">
                      <SelectParameters>
                          <asp:SessionParameter DefaultValue="0" Name="clientId" SessionField="LoggedInUserClientId"
                              Type="Int32" />
                      </SelectParameters>
                  </asp:ObjectDataSource>

</td>
          </tr>
      </table>
                  &nbsp;

</div>
</asp:Content>

