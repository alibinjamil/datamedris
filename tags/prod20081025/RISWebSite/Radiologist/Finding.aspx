<%@ Page Language="C#" MasterPageFile="~/Common/Popup.master" AutoEventWireup="true" CodeFile="Finding.aspx.cs" Inherits="Radiologist_Finding" Title="DataMed | Radiology Information System | Add Finding to Study" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="JavaScript" src="../Common/ieSpell.js"></script>
<script language="javascript" type="text/javascript">
function onSpellCheckClick()
{	
	var node = document.getElementById("ctl00_ContentPlaceHolder1_tbTrancription");
	checknodespelling(node); 
}
function OnMarkForApprovalClick()
{
    needToConfirm = false;
    childClose();
}
function childClose()
{
    <%if(IsVerified == false) %>
    <%{ %> 
        document.VoiceControl.CheckClose();
    <%}%>
    return true;
}
function OnApproveClick()
{
    needToConfirm = false;
    childClose();
}
</script>
    <table width="650px" border="0">

<!--        <tr>
            <td align="right" width="100px"> Audio Dictation
            </td>
            <td> [Audio Control]
            </td>
        </tr>-->
        <tr>
            <td align="right" style="width:20%" class="heading">Patient:</td>
            <td align="left" style="width:30%" ><asp:Label ID="lblPatientName" runat="server"></asp:Label></td>
            <td align="right" style="width:20%" class="heading">Patient ID:</td>
            <td align="left" style="width:30%"><asp:Label ID="lblPatientId" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right"  class="heading"> Status:</td>
            <td align="left"><asp:Label ID="lblStatus" runat="server"></asp:Label></td>
            <td align="right"  class="heading"> Exam Date:
            </td>
            <td align="left"><asp:Label ID="lblExamDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right"  class="heading"> Modality:
            </td>
            <td align="left"><asp:Label ID="lblModality" runat="server"></asp:Label></td>
            <td align="right"  class="heading"> Procedure:
            </td>
            <td align="left"><asp:Label ID="lblProcedure" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td align="right"  class="heading"> Radiologist:
            </td>
            <td align="left"><asp:Label ID="lblRadiologist" runat="server"></asp:Label></td>
            <td align="right"  class="heading"> 
                Physician:
            </td>
            <td align="left"><asp:Label ID="lblPhysician" runat="server"></asp:Label></td>
        </tr>      
        <tr>
            <td align="center"  class="heading" colspan="4" > 
                <%if(IsVerified == false) %>
                <%{ %>
                <OBJECT id="VoiceControl" name=”VoiceControl" classid="clsid:A3993B96-F2DF-4dd9-8D37-5C55E59FF553" VIEWASTEXT codebase="VoiceControl.cab">                
                    <PARAM name="FindingId" value="<%=FindingId%>" /> 
                    <PARAM name="StudyId" value="<%=StudyId%>" /> 
                    <PARAM name="UserId" value="<%=UserId%>" />                 
                    <PARAM name="ReadOnly" value="<%=!CanRecord%>" />
                    <param name="IsTranscriptionist" value="<%=IsTranscriptionist %>" />
                </object>
                <%} %>
                &nbsp;
            </td>
        </tr>             
        <tr>
            <td align="center"  class="heading" colspan="4" > Report
            <asp:HiddenField ID="lblFindingId" runat="server" />
            <asp:HiddenField ID="lblStudyId" runat="server" />
</td>
        </tr>
        <tr>
        <td align="left" colspan="4" ><input type="button" value="Spell Check" onclick="onSpellCheckClick();" />                 
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbTrancription"
                    ErrorMessage="Maximum characters allowed are 6000." ValidationExpression="^[\s\S]{0,6000}$"></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="rfvTbTranscription" runat="server" ControlToValidate="tbTrancription"
                                Enabled="False" ErrorMessage="Transcription is Required."></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
                        <td align="left" colspan="4" >
                <asp:TextBox ID="tbTrancription" runat="server" MaxLength="6000" Rows="14" TextMode="MultiLine"
                    Width="640px" CssClass="textBoxStyle"></asp:TextBox>&nbsp;
                        </td>            

        </tr>
        <tr>
            <td colspan="4" align="center" >
                <asp:Button ID="btnMarkForApproval" runat="server" Text="Mark for Verification" OnClick="btnMarkForApproval_Click" CssClass="buttonStyle"  OnClientClick="OnMarkForApprovalClick();"/>                
                
                <asp:Button ID="btnApprove" runat="server" Text="Verify & Close" OnClick="btnApprove_Click" CssClass="buttonStyle" OnClientClick="OnApproveClick()" />
            </td>            
        </tr>        
    </table>   
</asp:Content>

