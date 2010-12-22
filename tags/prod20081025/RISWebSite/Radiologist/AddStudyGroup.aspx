<%@ Page Language="C#" MasterPageFile="~/Common/Popup.master" AutoEventWireup="true" CodeFile="AddStudyGroup.aspx.cs" Inherits="Radiologist_AddStudyGroup" Title="DataMed | Radiology Information System | Add Group to Study" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">
function childClose()
{
    return true;
}
</script>

    <table width="400PX" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="2" class="pageHeading" style="width: 50%"> Manage Groups for Study</td>
        </tr>
        <tr>
            <td align="center" colspan="2" class="pageHeading" style="width: 50%"> &nbsp;</td>
        </tr>
        
        <tr class="headingRow">
            <td style="width: 50%" align="right">Study Id:&nbsp;&nbsp;
            </td>
            <td>
                <asp:Label ID="lblStudyId" runat="server" Text="Label"></asp:Label></td>
        </tr>
       
        <tr>
            <td style="width: 50%">
                <asp:DropDownList ID="ddlGroups" runat="server" Width="95%">
                </asp:DropDownList>
            </td>            
            <td><asp:Button ID="btnAdd" runat="server" Text="Allow group to view Study" OnClick="btnAdd_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 50%">
                <asp:Table ID="dataTable" runat="server" Width="100%" CellPadding="0" CellSpacing ="0">
                </asp:Table>
            </td>
        </tr>
        
    </table>
</asp:Content>

