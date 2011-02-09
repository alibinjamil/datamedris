<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTemplates.aspx.cs" Title="DataMed | Radiology Information System | Manage Templates"  MasterPageFile="~/Common/Main.master" Inherits="AdminPages_ManageTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 184px">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvTemplates" runat="server" AllowPaging="True"  AllowSorting="True"
                    AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    ForeColor="Black" GridLines="Vertical" Width="1000px" 
                    DataSourceID="edsTemplates">
                  
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Edit" SortExpression="TemplateId">
                            <EditItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("TemplateId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="hl1" runat="server" NavigateUrl='<%#"~/AdminPages/AddTemplate.aspx?templateId=" + Eval("TemplateId")%>'>Edit</asp:HyperLink>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TemplateName" HeaderText="TemplateName" 
                            SortExpression="TemplateName" ReadOnly="True" />
                                      
                        <asp:BoundField DataField="Heading" HeaderText="Heading" 
                            SortExpression="Heading" ReadOnly="True" />
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" ReadOnly="True" />
                        <asp:BoundField DataField="Impression" HeaderText="Impression" 
                            SortExpression="Impression" ReadOnly="True" />
                        <asp:BoundField DataField="BodyPartName" HeaderText="BodyPartName" 
                            SortExpression="BodyPartName" ReadOnly="True" />
                        <asp:BoundField DataField="ModalityName" HeaderText="ModalityName" 
                            ReadOnly="True" SortExpression="ModalityName" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="Gainsboro" />
                </asp:GridView>               
            </td>
        </tr>
        <tr>
            <td>
                <asp:EntityDataSource ID="edsTemplates" runat="server" 
                    AutoGenerateOrderByClause="True" ConnectionString="name=RISEntities" 
                    DefaultContainerName="RISEntities" EnableFlattening="False" 
                    EntitySetName="TemplateDetails" OrderBy="" 
                    Select="it.[TemplateId], it.[TemplateName], it.[Heading], it.[Description], it.[Impression], it.[BodyPartName], it.[ModalityName]">
                    <OrderByParameters>
                        <asp:Parameter DefaultValue="TemplateId" />
                    </OrderByParameters>
                </asp:EntityDataSource>
            </td>
        </tr>
    </table>
</asp:Content>