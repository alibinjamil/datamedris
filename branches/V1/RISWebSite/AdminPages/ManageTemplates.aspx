<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageTemplates.aspx.cs" Title="DataMed | Radiology Information System | Manage Templates"  MasterPageFile="~/Common/Main.master" Inherits="AdminPages_ManageTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="width: 184px">
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdTemplates" runat="server" AllowPaging="True" 
                    DataSourceID="odsTemplates" AutoGenerateColumns="False" BackColor="White" 
                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                    ForeColor="Black" GridLines="Vertical" Width="1000px" DataKeyNames="TemplateId">
                    <Columns>
                        <asp:TemplateField HeaderText="TemplateId" InsertVisible="False" 
                            SortExpression="TemplateId">
                            <EditItemTemplate>
                                
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("TemplateId") %>'></asp:Label>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#"~/AdminPages/AddTemplate.aspx?TemplateId=" + Eval("TemplateId") %>'>Edit</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ModalityName" HeaderText="ModalityName" 
                            SortExpression="ModalityName" />
                        <asp:BoundField DataField="TemplateName" HeaderText="TemplateName" 
                            SortExpression="TemplateName" />
                                      
                        <asp:BoundField DataField="BodyPart" HeaderText="BodyPart" 
                            SortExpression="BodyPart" />
                        <asp:BoundField DataField="Heading" HeaderText="Heading" 
                            SortExpression="Heading" />
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            SortExpression="Description" />
                        <asp:BoundField DataField="Impression" HeaderText="Impression" 
                            SortExpression="Impression" />
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
                <asp:ObjectDataSource ID="odsTemplates" runat="server" 
                    SelectMethod="GetTemplatesForUser" 
                    TypeName="TemplatesTableAdapters.tTemplatesTableAdapter" DeleteMethod="Delete" 
                    InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
                    UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_TemplateId" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="ModalityId" Type="Int32" />
                        <asp:Parameter Name="BodyPart" Type="String" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Text" Type="String" />
                        <asp:Parameter Name="Heading" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Impression" Type="String" />
                        <asp:Parameter Name="Original_TemplateId" Type="Int32" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="userId" 
                            SessionField="LoggedInUserId" Type="Int32" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="ModalityId" Type="Int32" />
                        <asp:Parameter Name="BodyPart" Type="String" />
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="Text" Type="String" />
                        <asp:Parameter Name="Heading" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="Impression" Type="String" />
                    </InsertParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
</asp:Content>