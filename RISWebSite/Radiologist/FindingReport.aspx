<%@ Page Language="C#" MasterPageFile="~/Common/Report.master" AutoEventWireup="true" CodeFile="FindingReport.aspx.cs" Inherits="Radiologist_FindingReport" Title="DataMed | Radiology Information System | Finding Report Print" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" width="100%">
        <tr>
            <td colspan="2" align="right">
                <asp:ImageButton ID="viewBtn" runat="server" 
                    ImageUrl="~/Images/attach-big.png" AlternateText="List of Views for this Exam" 
                    style="margin-bottom:7px;margin-right:10px;" onclick="viewBtn_Click" />
                <asp:ImageButton ID="pdfBtn" runat="server" 
                    ImageUrl="~/Images/pdf-icon.gif" AlternateText="Get PDF for the Report" 
                    onclick="pdfBtn_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="faxBtn" runat="server" 
                    ImageUrl="~/Images/faxIcon.gif" AlternateText="Fax the report to Hopsital" 
                    onclick="faxBtn_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:ImageButton ID="printBtn"
                    runat="server" ImageUrl="~/Images/icon-print.jpg" OnClientClick="window.print();return false;" AlternateText="Print the report"/>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="heading">
                <div style="width:90%;text-align:center">
                    <asp:Label ID="lblClientName" runat="server" Text=""></asp:Label>
                    <br />                    
                    <asp:Label ID="lblClientAddress" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblHospitalName" runat="server" Text=""></asp:Label>
                    
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="heading">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" class="heading">
                &nbsp;
            </td>
        </tr>        
        <tr>
            <td style="width:50%"><b>PATIENT NAME:
            </b>
            </td>
            <td style="width:50%"><asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50%"><b>DATE OF BIRTH:
            </b>
            </td>
            <td style="width:50%"><asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50%"><b>DATE OF EXAM:
            </b>
            </td>
            <td style="width:50%"><asp:Label ID="lblStudyDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
         <tr>
            <td style="width:50%"><b>TYPE OF EXAM:</b></td>
            <td style="width:50%"><asp:Label ID="lblModality" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
         <tr>
            <td style="width:50%"><b>REFERRING PHYSICAN:</b></td>
            <td style="width:50%"><asp:Label ID="lblRefPhy" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
         <tr>
            <td style="width:50%"><b>REPORT DATE:</b></td>
            <td style="width:50%"><asp:Label ID="lblReportDate" runat="server" Text=""></asp:Label>
            </td>            
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
        
        <tr>
            <td colspan="2"><asp:Label ID="lblTranscription" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>        
         <tr>
            <td colspan="2"><asp:Label ID="lblRadiologist" runat="server" Text=""></asp:Label></td>
        </tr>        
         <tr>
            <td colspan="2"><asp:Label ID="lblReportDateTime" runat="server" Text=""></asp:Label></td>
        </tr>

         <tr>
            <td colspan="2"><b>Report Status: <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></b></td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

         <tr>
            <td colspan="2"><asp:Label ID="lblManualStatus" runat="server" Text=""></asp:Label></td>
        </tr>

        
    </table>
</asp:Content>

