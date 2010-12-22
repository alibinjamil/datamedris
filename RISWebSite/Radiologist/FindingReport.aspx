<%@ Page Language="C#" MasterPageFile="~/Common/Report.master" AutoEventWireup="true" CodeFile="FindingReport.aspx.cs" Inherits="Radiologist_FindingReport" Title="DataMed | Radiology Information System | Finding Report Print" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" width="100%">
        <tr>
            <td colspan="2" class="heading">
                <div style="width:90%;text-align:center">
                Scott Regional Hospital <br /> 317 Highway 13 S <br /> Morton, MS 39117 <br /> (601) 732-6301
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
            <td style="width:50%">Name:&nbsp;<asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label>
            </td>
            <td style="width:50%">Patient ID:&nbsp;<asp:Label ID="lblPatientId" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50%">Gender:&nbsp;<asp:Label ID="lblSex" runat="server" Text=""></asp:Label>
            </td>
            <td style="width:50%">Age:&nbsp;<asp:Label ID="lblAge" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:50%">Physician:&nbsp;<asp:Label ID="lblDoctor" runat="server" Text=""></asp:Label>
            </td>
            <td style="width:50%">Study Date:&nbsp;<asp:Label ID="lblStudyDate" runat="server" Text=""></asp:Label>
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
            <td colspan="2"><asp:Label ID="lblRadiologist" runat="server" Text=""></asp:Label>:&nbsp;<asp:Label ID="lblTranscriptionist" runat="server" Text=""></asp:Label>&nbsp;DD:&nbsp;<asp:Label ID="lblDictationDate" runat="server" Text=""></asp:Label>&nbsp;DT:&nbsp;<asp:Label ID="lblTranscriptionDate" runat="server" Text=""></asp:Label></td>
        </tr>        
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

         <tr>
            <td colspan="2"><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
        </tr>
         <tr>
            <td colspan="2">&nbsp;</td>
        </tr>

         <tr>
            <td colspan="2"><asp:Label ID="lblManualStatus" runat="server" Text=""></asp:Label></td>
        </tr>

        
    </table>
</asp:Content>

