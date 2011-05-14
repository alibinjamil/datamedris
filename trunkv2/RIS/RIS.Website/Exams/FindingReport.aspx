<%@ Page Language="C#" MasterPageFile="~/Common/Report.master" AutoEventWireup="true" CodeFile="FindingReport.aspx.cs" Inherits="Radiologist_FindingReport" Title="DataMed | Radiology Information System | Finding Report Print" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function onPrint() {
            window.print();
        }
    </script>
    <iframe id="ifrmPrint" src="#" style="width:0px; height:0px;"></iframe>
    <div style="text-align:right;">
        <asp:ImageButton ID="createTemplate"
                runat="server" ImageUrl="~/Images/report_user.png"  
                AlternateText="Create Template from this Report" 
                onclick="createTemplate_Click" style="margin-bottom:7px;margin-right:10px;"/>

                    
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
                runat="server" ImageUrl="~/Images/icon-print.jpg" OnClientClick="onPrint();return false;" AlternateText="Print the report"/>
    </div>
    <hr />
    <div id="contents" class="reportArea">
        <div style="text-align:center">
            <asp:Image ID="imgClientLogo" runat="server" />
        </div>
        <div>
            <div style="float:left;width:70%;"><asp:Label ID="lblClientAddress" runat="server" Text=""></asp:Label></div>
            <div style="float:left;width:30%;">Telephone:&nbsp;<asp:Label ID="lblClientTelephone" runat="server" Text=""></asp:Label></div>
            <div style="clear:both"></div>
        </div>
        <br />
        <br />    
        <hr />
        <div>
            <asp:Label ID="lblHospitalName" runat="server" Text="" CssClass="pageHeading"></asp:Label>                    
        </div>
        <hr />
        <br />
        <br />
        <div>
            <div style="float:left;width:50%;">
                <b>MRN:</b>&nbsp;<asp:Label ID="lblPatientID" runat="server" Text=""></asp:Label>
            </div>
            <div style="float:left;width:50%;">
                <b>Physician:</b>&nbsp;<asp:Label ID="lblRefPhy" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear:both"></div>
        </div>
        <div>
            <div style="float:left;width:50%;">
                <b>Patient Name:</b>&nbsp;<asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label>
            </div>
            <div style="float:left;width:50%;">
                <b>Date of Exam:</b>&nbsp;<asp:Label ID="lblExamDate" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear:both"></div>
        </div>
        <div>
            <div style="float:left;width:50%;">
                <b>Date of Birth:</b>&nbsp;<asp:Label ID="lblDOB" runat="server" Text=""></asp:Label>
            </div>
            <div style="float:left;width:50%;">
                <b>Accession:</b>&nbsp;<asp:Label ID="lblAccession" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear:both"></div>
        </div>
        <div>
            <div style="float:left;width:50%;">
                <b>Sex:</b>&nbsp;<asp:Label ID="lblGender" runat="server" Text=""></asp:Label>
            </div>
            <div style="float:left;width:50%;">
                <b>Date of Report:</b>&nbsp;<asp:Label ID="lblReportDate" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear:both"></div>
        </div>
        <br />
        <hr />
        <br />
        <div>
            <asp:Label ID="lblTranscription" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <hr />
        <div>
            <div style="float:left;width:40%;">
                <asp:Label ID="lblVerified" runat="server" Text="Electronically Approved and Signed by:" ></asp:Label>                
            </div>
            <div style="float:left;width:30%">
               <asp:Label ID="lblRadiologist" runat="server" Text=""></asp:Label>
            </div>
            <div style="float:left;width:30%">
                <asp:Label ID="lblReportDateTime" runat="server" Text=""></asp:Label>
            </div>
            <div style="clear:both"></div>
        </div>
        <hr />
        <br />
        <br />
        <br />
        <div style="width:100%;margin-top:10px;">
            <asp:Label ID="lblAmmentment" runat="server" Text=""></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <hr />        
        <div class="footer"><asp:Label ID="lblFooter" runat="server" Text=""></asp:Label></div>
        <hr />
    </div>     
</asp:Content>

