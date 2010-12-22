<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddStudyGroup.aspx.cs" Inherits="Radiologist_AddStudyGroup" Title="DataMed | Radiology Information System | Add Group to Study" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2" class="pageHeading" style="width: 50%"> Manage Study</td>
        </tr>
        <tr>
            <td valign="middle">            
                Tech Comments:
            </td>
            <td>
                <asp:TextBox ID="tbTechComments" runat="server"></asp:TextBox>    
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Save Study Details" OnClick="btnSave_Click" />
            </td>
        </tr>
        
        <tr>
            <td align="center" colspan="2" class="pageHeading" style="width: 50%"> &nbsp;</td>
        </tr>
        <tr>
            <td>Group:</td>
            <td>
                <asp:DropDownList ID="ddlGroups" runat="server" DataSourceID="odsRemainingGroups" DataTextField="Name" DataValueField="GroupId">
                </asp:DropDownList>
            </td>            
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAddGroup" runat="server" Text="Add Group to Study" OnClick="btnAddGroup_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">Groups Assigned</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="gvGroups" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="GroupId" DataSourceID="odsGroups" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsGroups" runat="server" DeleteMethod="Delete" InsertMethod="Insert"
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetGroupsByStudyId"
                    TypeName="GroupsTableAdapters.tGroupsTableAdapter">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_GroupId" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="studyId" QueryStringField="studyId"
                            Type="Int32" />
                    </SelectParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Name" Type="String" />
                        <asp:Parameter Name="IsDefault" Type="String" />
                        <asp:Parameter Name="Description" Type="String" />
                        <asp:Parameter Name="CreatedBy" Type="Int32" />
                        <asp:Parameter Name="CreationDate" Type="DateTime" />
                        <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
                        <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
                    </InsertParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsRemainingGroups" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetRemainingGroupsForStudy" TypeName="GroupsTableAdapters.tGroupsTableAdapter">
                    <SelectParameters>
                        <asp:QueryStringParameter DefaultValue="0" Name="studyId" QueryStringField="studyId"
                            Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>       
    </table>
</asp:Content>

