using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
/// <summary>
/// Summary description for FindingHTMLGenerator
/// </summary>
public class FindingHTMLGenerator
{
	public FindingHTMLGenerator()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string getInnerHTML()
    {
        string patientName = "Ali Bin Jamil";
        string patientId = "1234";
        string status = "Approved";
        string examDate = "12/12/2001 10:00:01";
        string modality = "CR";
        string procedure = "Hello";
        string radiologist = "Mariam Alvi";
        string physician = "Physician";
        StringBuilder innerHTML = new StringBuilder();
        innerHTML.Append("<table width=\"650px\" border=\"0\">");
        innerHTML.Append("<tr>");
        innerHTML.Append("<td align=\"right\" style=\"width:20%\" class=\"heading\">Patient:</td>");
        innerHTML.Append("<td align=\"left\" style=\"width:30%\" >").Append(patientName).Append("</td>");
        innerHTML.Append("<td align=\"right\" style=\"width:20%\" class=\"heading\">Patient ID:</td>");
        innerHTML.Append("<td align=\"left\" style=\"width:30%\">").Append(patientId).Append("</td>");
        innerHTML.Append("</tr>");
        innerHTML.Append("<tr>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\"> Status:</td>");
        innerHTML.Append("<td align=\"left\">").Append(status).Append("</td>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\"> Exam Date:</td>");
        innerHTML.Append("<td align=\"left\">").Append(examDate).Append("</td>");
        innerHTML.Append("</tr>");
        innerHTML.Append("<tr>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\"> Modality:</td>");
        innerHTML.Append("<td align=\"left\">").Append(modality).Append("</td>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\"> Procedure:</td>");
        innerHTML.Append("<td align=\"left\">").Append(procedure).Append("</td>");
        innerHTML.Append("</tr>");
        innerHTML.Append("<tr>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\"> Radiologist:</td>");
        innerHTML.Append("<td align=\"left\">").Append(radiologist).Append("</td>");
        innerHTML.Append("<td align=\"right\"  class=\"heading\">Physician:</td>");
        innerHTML.Append("<td align=\"left\">").Append(physician).Append("</td>");
        innerHTML.Append("</tr>");
   /*     innerHTML.Append("<tr>
        innerHTML.Append("<td align="center"  class="heading" colspan="4" > 
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
    </table>   */
        return innerHTML.ToString();
    }
}
