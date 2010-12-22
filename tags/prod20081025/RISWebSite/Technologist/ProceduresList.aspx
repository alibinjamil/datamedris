<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="ProceduresList.aspx.cs" Inherits="Technologist_ProceduresList" Title="DataMed | Radiology Information System | Procedures List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:left;margin-left:100px;">
        <p><asp:Label ID="LabelError" runat="server" Visible="false" CssClass="errorText" Text="Cannot delete Procedure. Reference Data is present elsewhere."></asp:Label></p>
        <p>
            <asp:Label ID="Label1" Width="150px" runat="server" Text="Modality:"></asp:Label><asp:DropDownList ID="DropDownListModalities" runat="server" DataSourceID="ObjectDataSourceModalities" DataTextField="Name" DataValueField="ModalityId">
            </asp:DropDownList></p>
        <p><asp:Label ID="Label2" Width="150px" runat="server" Text="Procedure Name:"></asp:Label><asp:TextBox ID="TextBoxProcedureName" runat="server"></asp:TextBox></p>
        <p><asp:Label ID="Label3" Width="150px" runat="server" Text="CPT Code:"></asp:Label><asp:TextBox ID="TextBoxCPTCode" runat="server"></asp:TextBox></p>
        <p><asp:Button ID="ButtonSave" runat="server" Text="Add Procedure" OnClick="ButtonSave_Click" /></p>
    </div>
    <div>
    <asp:GridView ID="GridViewProcedures" runat="server" AutoGenerateColumns="False"
        DataKeyNames="ProcedureId" DataSourceID="ObjectDataSourceProcedures"
        PageSize="25" AllowPaging="True" OnRowDeleted="GridViewProcedures_RowDeleted" AllowSorting="True" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" Text="Edit" CommandArgument='<%# Bind("ProcedureId") %>' OnCommand="LinkButtonEdit_Command" ></asp:LinkButton>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                        Text="Delete"></asp:LinkButton>
                        
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ProcedureId" HeaderText="ID" InsertVisible="False"
                ReadOnly="True" SortExpression="ProcedureId" />
            <asp:BoundField DataField="ProcedureName" HeaderText="Name" SortExpression="ProcedureName" />
            <asp:BoundField DataField="ModalityName" HeaderText="Modality" SortExpression="ModalityName" />             
            <asp:BoundField DataField="CPTCode" HeaderText="CPTCode" SortExpression="CPT Code" />
            <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
            <asp:BoundField DataField="CreationDate" HeaderText="CreationDate" SortExpression="CreationDate" />
            <asp:BoundField DataField="LastUpdatedBy" HeaderText="LastUpdatedBy" SortExpression="LastUpdatedBy" />
            <asp:BoundField DataField="LastUpdateDate" HeaderText="LastUpdateDate" SortExpression="LastUpdateDate" />
            
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ObjectDataSourceProcedures" runat="server" DeleteMethod="ProcedureDeleteCommand"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllProcedures"
        TypeName="ProceduresTableAdapters.ProcedureSelectCommandTableAdapter">
        <DeleteParameters>
            <asp:Parameter Name="original_ProcedureId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    &nbsp; &nbsp;&nbsp;&nbsp;<asp:ObjectDataSource ID="ObjectDataSourceModalities" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllModalities"
        TypeName="ModalitiesTableAdapters.tModalitiesTableAdapter"></asp:ObjectDataSource>
</asp:Content>

