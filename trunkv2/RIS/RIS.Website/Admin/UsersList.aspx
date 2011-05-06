<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="UsersList.aspx.cs" Inherits="Admin_UsersList" Title="DataMed | Radiology Information System | User Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="right">>>&nbsp;&nbsp;</td>
            <td colspan="5" align="left">            
                <asp:HyperLink ID="hlUsersList" runat="server" NavigateUrl="~/Admin/AddUser.aspx">Add New User</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="6">&nbsp;</td>
        </tr>
        <tr>
            <td align="right">Client:</td> 
            <td align="left"><asp:DropDownList ID="ddlClients" runat="server" 
                    ondatabound="ddlClients_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlClients_SelectedIndexChanged"></asp:DropDownList></td>
            <td align="right">Hospital:</td>           
            <td align="left">
                <asp:DropDownList ID="ddlHospitals" runat="server" 
                    ondatabound="ddlHospitals_DataBound"></asp:DropDownList></td>
            <td align="right">Role:</td>
            <td align="left"><asp:DropDownList ID="ddlRoles" runat="server"></asp:DropDownList></td>
            <td align="left">
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    onclick="btnSearch_Click" /></td>
        </tr>
        <tr>
            <td colspan="7" align="left">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="False" 
                    AutoGenerateColumns="False" CellPadding="4" 
                    DataKeyNames="UserId" ForeColor="Black" 
                    GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" 
                    BorderStyle="None" BorderWidth="1px" style="margin-top: 0px">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
       
                        <asp:TemplateField HeaderText="UserId" InsertVisible="False" 
                            SortExpression="UserId">
                            <EditItemTemplate>
                                <asp:HyperLink ID="Label1" runat="server" NavigateUrl='<%#"AddUser.aspx?userId=" +  Eval("UserId") %>'>Edit</asp:HyperLink>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="Label1" runat="server" NavigateUrl='<%#"AddUser.aspx?userId=" +  Eval("UserId") %>'>Edit</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LoginName" HeaderText="LoginName" 
                            SortExpression="LoginName" />
                        <asp:BoundField DataField="Password" HeaderText="Password" 
                            SortExpression="Password" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" 
                            SortExpression="IsActive" />
                        <asp:BoundField DataField="LastLoginDate" HeaderText="LastLoginDate" 
                            SortExpression="LastLoginDate" />
                        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" 
                            SortExpression="CreationDate" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                            SortExpression="CreatedBy" />
                        <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" 
                            SortExpression="LastUpdatedBy" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" 
                            SortExpression="LastUpdateDate" />
                    </Columns>
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    </asp:Content>

