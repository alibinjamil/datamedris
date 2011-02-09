<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="HospitalsList.aspx.cs" Inherits="Admin_HospitalsList" Title="DataMed | Radiology Information System | Hospital Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="right" style="width:25%">>>&nbsp;&nbsp;</td>
            <td align="left">            
                <asp:HyperLink ID="hlUsersList" runat="server" NavigateUrl="~/Admin/AddHospital.aspx">Add New Hospital</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
            <td align="right">Client:</td> 
            <td align="left"><asp:DropDownList ID="ddlClients" runat="server" 
                    ondatabound="ddlClients_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlClients_SelectedIndexChanged"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>        
        <tr>
            <td colspan="2" align="left">
                <asp:GridView ID="gvHospitals" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                    DataKeyNames="HospitalId" ForeColor="Black" 
                    GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" 
                    BorderStyle="None" BorderWidth="1px" DataSourceID="edsHospital">
                    <RowStyle BackColor="#F7F7DE" />
                    <Columns>
       
                        <asp:TemplateField HeaderText="Edit" InsertVisible="False" 
                            SortExpression="UserId">
                            <EditItemTemplate>
                                <asp:HyperLink ID="Label1" runat="server" NavigateUrl='<%#"AddHospital.aspx?hospitalId=" +  Eval("HospitalId") %>'>Edit</asp:HyperLink>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="Label1" runat="server" NavigateUrl='<%#"AddHospital.aspx?hospitalId=" +  Eval("HospitalId") %>'>Edit</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ClientId" HeaderText="ClientId" 
                            SortExpression="ClientId" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Address" HeaderText="Address" 
                            SortExpression="Address" />
                        <asp:BoundField DataField="State" HeaderText="State" 
                            SortExpression="State" />
                        <asp:BoundField DataField="Zip" HeaderText="Zip" 
                            SortExpression="Zip" />
                        <asp:BoundField DataField="Code" HeaderText="Code" 
                            SortExpression="Code" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" 
                            SortExpression="CreatedBy" />
                        <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" 
                            SortExpression="CreationDate" />
                        <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" 
                            SortExpression="LastUpdatedBy" />
                        <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" 
                            SortExpression="LastUpdateDate" />
                        <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                        <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                        <asp:BoundField DataField="Fax" HeaderText="Fax" SortExpression="Fax" />
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
    <asp:EntityDataSource ID="edsHospital" runat="server" 
        AutoGenerateOrderByClause="True" AutoGenerateWhereClause="True" 
        ConnectionString="name=RISEntities" DefaultContainerName="RISEntities" 
        EnableFlattening="False" EntitySetName="Hospitals" EntityTypeFilter="Hospital" 
        Where="">
        <WhereParameters>
            <asp:ControlParameter ControlID="ddlClients" DbType="Int32" DefaultValue="0" 
                Name="ClientId" PropertyName="SelectedValue" />
        </WhereParameters>
    </asp:EntityDataSource>
    <br />
</asp:Content>

