<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddRadiologists.aspx.cs" Inherits="Technologist_AddRadiologists" Title="DataMed | Radiology Information System | Add Exam | Step 2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Step 2: Assign Exam</h1>
    <div style="width:100%">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="dataEntryTable">
            <tr style="height:25px">
                <td>Exam Status:</td>
                <td align="left" style="width:170px;">
                    <asp:DropDownList ID="ddlExamStatus" runat="server" 
                        ondatabound="ddlExamStatus_DataBound">                        
                    </asp:DropDownList>
                </td>
                <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlExamStatus"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>
                </td>                       
            </tr>
            
            <tr>
                <td>
                    <asp:ListBox ID="lbNotRadiologists" runat="server"  
                        DataTextField="Name" DataValueField="UserId" Width="160px"></asp:ListBox>
                        
                </td>
                <td align="center" style="width:170px;">
                    <asp:Button ID="btnAddRadiologist" runat="server" Text="Add Radiologist(s) >>" onclick="btnAddRadiologist_Click" CssClass="buttonStyle" Width="200px" />
                    <asp:Button ID="btnRemoveRadiologist" runat="server" Text="<< Remove Radiologist(s)" onclick="btnRemoveRadiologist_Click" CssClass="buttonStyle" Width="200px" />
                </td>
                <td>
                    <asp:ListBox ID="lbRadiologists" runat="server"  
                        DataTextField="Name" DataValueField="UserId" Width="160px"></asp:ListBox>
                </td>                       
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>Dictating Radiologist:</td>
                <td align="left">
                    <asp:DropDownList runat="server" ID="ddlRadiologist" 
                        ondatabound="ddlRadiologist_DataBound" 
                        onselectedindexchanged="ddlRadiologist_SelectedIndexChanged"></asp:DropDownList></td>
                <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRadiologist"
                                ErrorMessage="*" InitialValue="0" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td  align="left" colspan="3">
                    <asp:RadioButtonList ID="rblReportType" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="rblReportType_SelectedIndexChanged">
                        <asp:ListItem Value="1" Text="Add Report Manually" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Upload and Attach a Report in PDF Format" ></asp:ListItem>
                        <asp:ListItem Value="3" Text="Scan and Attach a Report" ></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            
            <tr><td colspan="3">
            <asp:Panel ID="pnlManual" runat="server">
            <table>
            <tr>
                <td>Heading:</td>
                <td><asp:TextBox ID="tbHeading" runat="server" width="500px"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="tbHeading" ValidationGroup="Save" ></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Description:</td>
                <td><asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" width="500px"
                            Rows="8"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="*" ControlToValidate="tbDescription" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>Impression:</td>
                <td><asp:TextBox ID="tbImpression" runat="server" TextMode="MultiLine" width="500px"
                            Rows="3"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="*" ControlToValidate="tbImpression" ValidationGroup="Save"></asp:RequiredFieldValidator></td>
            </tr>
            </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlUpload" Visible="false">
                <table>
                <tr><td colspan="3">Select a PDF file to show as the report for this Exam:</td></tr>
                <tr><td colspan="3"><asp:FileUpload runat="server" ID="fileAttach" /></td></tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlAlreadyUploaded" Visible="false">
                <table>
                <tr><td colspan="3">Report already attached with this Exam.</td></tr>
                <tr><td colspan="3"><asp:HyperLink ID="hlDownloadReport" runat="server" NavigateUrl="~/WebScan/DownloadAttachment.aspx">Download Report</asp:HyperLink>
                    
                        &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnRemoveReport" runat="server" 
                                onclick="btnRemoveReport_Click">Remove Report</asp:LinkButton>
                        </td>
                    </caption>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlScan" Visible="false">
                <table><tr><td colspan="3">Click <asp:HyperLink ID="hlScan" runat="server">here</asp:HyperLink> to invoke the Scanning Page.&nbsp;&nbsp; </td></tr></table>
            </asp:Panel>
            </td>
            </tr>
            
            
            
            <tr>
                <td colspan="3">
                    <asp:Button runat="server" ID="btnSave" Text="Save Exam & Continue" onclick="btnSave_Click" CssClass="buttonStyle" ValidationGroup="Save"/>                      
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

