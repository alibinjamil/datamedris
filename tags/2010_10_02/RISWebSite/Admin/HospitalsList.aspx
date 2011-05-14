﻿<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="HospitalsList.aspx.cs" Inherits="Admin_HospitalsList" Title="DataMed | Radiology Information System | Hospital Management" %>

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
                    DataSourceID="odsClients" DataTextField="Name" 
                    DataValueField="ClientId" ondatabound="ddlClients_DataBound" AutoPostBack="true"></asp:DropDownList></td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>        
        <tr>
            <td colspan="2" align="left">
                <asp:GridView ID="gvUsers" runat="server" AllowPaging="True" 
                    AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                    DataKeyNames="HospitalId" DataSourceID="odsHospitals" ForeColor="#333333" 
                    GridLines="None">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
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
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
    </table>
      <asp:ObjectDataSource ID="odsClients" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetClientsForUser" 
    TypeName="ClientTableAdapters.tClientsTableAdapter">
          <SelectParameters>
              <asp:SessionParameter DefaultValue="0" Name="userId" 
                  SessionField="LoggedInUserId" Type="Int32" />
          </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsHospitals" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetHospitalByClient" 
        TypeName="HospitalsTableAdapters.tHospitalsTableAdapter" 
        DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_HospitalId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ClientId" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="Int32" />
            <asp:Parameter Name="CreationDate" Type="DateTime" />
            <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
            <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
            <asp:Parameter Name="Original_HospitalId" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlClients" DefaultValue="0" Name="clientId" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="ClientId" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="Int32" />
            <asp:Parameter Name="CreationDate" Type="DateTime" />
            <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
            <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    <br />
</asp:Content>
