<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="ModalitiesList.aspx.cs" Inherits="Technologist_ModalitiesList" Title="DataMed | Radiology Information System | Modalities List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:left;margin-left:150px;">
        <p><asp:Label ID="LabelError" runat="server" Visible="false" CssClass="errorText" Text="Cannot delete Modality. Reference Data is present elsewhere."></asp:Label></p>
        <p><asp:Label ID="Label3" Width="100px" runat="server" Text="Name:"></asp:Label><asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="ButtonSave" runat="server" Text="Add Modality" OnClick="ButtonSave_Click" />
        </p>
    </div>
    
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ModalityId"
        DataSourceID="ObjectDataSourceModalities" AllowPaging="True" AllowSorting="True" OnRowDeleted="GridView1_RowDeleted" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:TemplateField HeaderText="ModalityId" InsertVisible="False" SortExpression="ModalityId">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ModalityId") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ModalityId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name" SortExpression="Name">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" ReadOnly="True"/>
            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" SortExpression="CreationDate" ReadOnly="True"/>
            <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" SortExpression="LastUpdatedBy" ReadOnly="True"/>
            <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" SortExpression="LastUpdateDate" ReadOnly="True"/>
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
    &nbsp;
    <asp:ObjectDataSource ID="ObjectDataSourceModalities" runat="server" DeleteMethod="Delete"
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllModalities"
        TypeName="ModalitiesTableAdapters.tModalitiesTableAdapter" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_ModalityId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />            
            <asp:SessionParameter Name="LastUpdatedBy" Type="int32" DefaultValue="-1" SessionField="LoggedInUserId" />            
            <asp:Parameter Name="Original_ModalityId" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="Int32" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>

