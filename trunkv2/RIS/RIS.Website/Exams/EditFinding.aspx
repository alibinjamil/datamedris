<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditFinding.aspx.cs" Inherits="Radiologist_EditFinding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    


    <link href="../Common/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function rejectExam()
        {
            if(confirm("This exam will be routed back to the Radiology Technologist with your comments.  Click OK to continue or Cancel to return."))
            {                
                parent.openRejectionWindow(<%=Request[ParameterNames.Request.StudyId]%>,document.getElementById('lblPatientId').innerText,document.getElementById('lblPatientName').innerText);
                parent.closeFindingWindow();
            }
            return false;
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <div id="tabs"> 
        
            <table width="100%" border="0">
                <tr>
                    <td align="right" style="width:14%" class="heading">Patient:</td>
                    <td align="left" style="width:19%" class="normal">
                        <asp:Label ID="lblPatientName" runat="server" Text=""></asp:Label></td>
                    <td align="right" style="width:14%" class="heading">Patient ID:</td>
                    <td align="left" style="width:14%" class="normal"><asp:Label ID="lblPatientId" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading" style="width:14%" class="normal"> Status:</td>
                    <td align="left"  style="width:13%" class="normal"><asp:Label ID="lblStatus" runat="server" Text=""></asp:Label></td>
                    <td rowspan="3" style="width:10%;" valign="middle">
                        <asp:ImageButton ID="btnAmendments" runat="server" 
                            ImageUrl="~/Images/derivatives.png" onclick="btnAmendments_Click" Visible="false"/>
                        
                        <asp:HyperLink ID="hlAttach" runat="server" Target="_blank">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/attach-big.png"/>
                        </asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td align="right"  class="heading"> Exam Date:</td>
                    <td align="left" class="normal"><asp:Label ID="lblExamDate" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Modality:</td>
                    <td align="left" class="normal"><asp:Label ID="lblModality" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Procedure:
                    </td>
                    <td align="left" class="normal"><asp:Label ID="lblProcedure" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td align="right"  class="heading"> Radiologist:</td>
                    <td align="left" class="normal"><asp:Label ID="lblRadiologist" runat="server" Text=""></asp:Label></td>
                    <td align="right"  class="heading"> Physician:</td>
                    <td align="left" class="normal"><asp:Label ID="lblPhysician" runat="server" Text=""></asp:Label></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="heading" align="right">Tech Comments:</td>
                    <td colspan="6">
                    <asp:TextBox ID="techComments" runat="server" Width="850px" Rows="2" 
                            TextMode="MultiLine" ReadOnly="True" ></asp:TextBox></td>
                </tr>
                
            </table>
            <hr />
            <table >
                <tr>
                    <td align="left" colspan="7">
                        <asp:DropDownList ID="ddlBodyParts" runat="server" Visible="false" Width="150px"
                            AutoPostBack="True" onselectedindexchanged="ddlBodyParts_SelectedIndexChanged" >
                            <asp:ListItem Text="[-- Body Part --]" Value="0"></asp:ListItem>
                        </asp:DropDownList> 
                        <asp:DropDownList ID="ddlTemplates" runat="server" style="margin-left:10px;" Visible="false" Width="650px" >
                            <asp:ListItem Text="[-- Template --]" Value="0"></asp:ListItem>
                        </asp:DropDownList> 
                        <asp:Button ID="btnApplyTemplate" runat="server" 
                            Text="Apply Template" CssClass="buttonStyle"  
                            onclick="btnApply_Click" Visible="false"  ValidationGroup="Apply" />
                        
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <div style="text-align:left;float:left;" id="findingHeaderRad">                            
                            <div id="divTemplates"></div>
                        </div>
                        <div style="text-align:left;float:left;display:none;" id="findingHeaderTran">
                            <input type='button' value='Spell Check' onclick='onSpellCheckClick();' />
                        </div>
                        <div style="text-align:left;float:left;height:100%;padding-top:5px;">
                            <input type="hidden" name="currentIndex" id="currentIndex" value=""/>
                            <span class="errorText" id="errorMessage" />
                            <span class="saveFinding" id="savedMessage" />                        
                        </div>
                    </td>
                </tr>                
                <tr>
                    <td align="right">
                        <b>Heading:&nbsp;</b>
                    </td>
                    <td align="left" colspan="5" >
                       <asp:TextBox
                            ID="tbHeading" runat="server" Width="850px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" ControlToValidate="tbHeading" ValidationGroup="Verify" ></asp:RequiredFieldValidator>
                    </td>            
                </tr>
                <tr>
                    <td align="right" valign="middle"><b>Description:</b></td>
                    <td align="left" colspan="5" >
                        
                        <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" 
                            Width="850px" Rows="8"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="*" ControlToValidate="tbDescription" ValidationGroup="Verify"></asp:RequiredFieldValidator>                            
                    </td>            
                </tr>
                <tr>
                    <td align="right" valign="middle"><b>Impression:</b></td>
                    <td align="left" colspan="5" >                        
                        <asp:TextBox ID="tbImpression" runat="server" TextMode="MultiLine" 
                            Width="850px" Rows="3"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="*" ControlToValidate="tbImpression" ValidationGroup="Verify"></asp:RequiredFieldValidator>                                                        
                    </td>            
                </tr>                
                <asp:Panel ID="pnlAmendment" runat="server" Visible="false">
                <tr>
                    <td align="right" valign="middle"><b>Amendment:</b></td>
                    <td align="left" colspan="5" >
                    <asp:TextBox ID="tbAmendment" runat="server" TextMode="MultiLine" 
                            Width="850px" Rows="3"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbAmendment" ValidationGroup="Verify"></asp:RequiredFieldValidator>                                                        
                    </td>
                </tr>
                </asp:Panel>                
                <tr>
                    <td align="center" colspan="6" >
                        <asp:Button ID="btnReject" runat="server" 
                            Text="Reject Exam" CssClass="buttonStyleRed"  OnClientClick="return rejectExam();"
                            style="margin-right:20px" Visible="false"/>
                        <asp:Button ID="btnSave" runat="server" 
                            Text="Save & Close without Verification" CssClass="buttonStyle"  
                            style="margin-right:20px" onclick="btnSave_Click" Visible="false" ValidationGroup="Verify"/>
                        <asp:Button ID="btnVerify" runat="server" Text="Verify & Close" 
                            CssClass="buttonStyle" onclick="btnVerify_Click" Visible="false"  ValidationGroup="Verify"/>
                        
                    </td>            
                </tr>        
            </table>           
	
        </div>
    </form>

</body>
</html>
