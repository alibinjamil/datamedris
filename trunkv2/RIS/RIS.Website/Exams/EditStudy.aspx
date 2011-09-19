<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditStudy.aspx.cs" Inherits="Radiologist_EditStudy" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
	<link rel="stylesheet" href="../Includes/jquery/themes/base/jquery.ui.all.css"/> 
	<script type="text/javascript" src="../Includes/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="../Includes/jquery/jquery-ui-1.8.1.custom.min.js"></script> 
	<script type="text/javascript">
	    $(function () {
	        $("#tabs").tabs();
	        if ("undefined" !== typeof selectedTab) {
	            $("#tabs").tabs("select", parseInt(selectedTab));
	        }
	        if ("undefined" !== typeof showRadError) {
                alert('At least one Radiologist must be selected.');
            }
	    });

	    function checkValidation(checkOnlyHospital) {
	        if ($("#<%=tbPatientId.ClientID%>").val().length == 0
                || $("#<%=tbPatientName.ClientID%>").val().length == 0) {

	            $("#tabs").tabs("select", 0);
	        }else if($("#<%=ddlClient.ClientID%>").val() == "-1" 
                || $("#<%=ddlHospitals.ClientID%>").val() == "-1" 
                || (checkOnlyHospital == false && $("#<%=ddlRefPhy.ClientID%>").val() == "-1")
                || (checkOnlyHospital == false && $("#<%=ddlBodyParts.ClientID%>").val() == "-1") 
                || (checkOnlyHospital == true  && $("#<%=lbRadiologists.ClientID%> option").size() == 0) ){

                    $("#tabs").tabs("select", 1);
            }            

        }
    </script> 

    
</head>
<body>
    <form id="form1" runat="server">   
  
    <div id="tabs" style="font-size:10pt;width:100%;">
	    <ul>
		    <li><a href="#tabs-1" >Patient Information</a></li>
		    <li><a href="#tabs-2">Exam Information</a></li>
	    </ul>
	    <div id="tabs-1" style="height:370px">
		    <div>
                <asp:Label ID="Label6" runat="server" Text="Patient ID:" Font-Bold="true" Width="175px"></asp:Label>
                <asp:TextBox ID="tbPatientId" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="tbPatientId" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>        
                <asp:HyperLink ID="hlAddAttachment1" runat="server" Target="_blank">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attach a new document to this Exam"/>                        
                </asp:HyperLink>
            </div>
            <div>
                <asp:Label ID="Label7" runat="server" Text="Patient Name:" Font-Bold="true" Width="175px"></asp:Label>
                <asp:TextBox ID="tbPatientName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="tbPatientName" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="Label8" runat="server" Text="Patient Date of Birth:" Font-Bold="true" Width="175px"></asp:Label>
                <ew:CalendarPopup ID="tbDOB" runat="server"></ew:CalendarPopup>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="tbDOB" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="Label9" runat="server" Text="Patient Gender:" Font-Bold="true" Width="175px"></asp:Label>
                <asp:RadioButtonList ID="rbGender"   runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Selected="True">M</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div>
                <asp:Label ID="Label10" runat="server" Text="Patient Weight:" Font-Bold="true" Width="175px"></asp:Label>
                <ew:NumericBox ID="tbPatientWeight"  runat="server"></ew:NumericBox>
        
            </div>
	    </div>
	    <div id="tabs-2" style="height:370px">
            <div>
                <asp:Label ID="lblClient" runat="server" Text="Client:" Font-Bold="true" Width="160px"></asp:Label>
                <asp:DropDownList ID="ddlClient" runat="server" 
                    ondatabound="ddlClient_DataBound" AutoPostBack="True" 
                    onselectedindexchanged="ddlClient_SelectedIndexChanged" >
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv3" ControlToValidate="ddlClient" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>        
        
            </div>
            <div>
                <asp:Label ID="Label3" runat="server" Text="Hospital:" Font-Bold="true" Width="160px"></asp:Label>
                <asp:DropDownList ID="ddlHospitals" runat="server" AutoPostBack="True" 
                    ondatabound="ddlHospitals_DataBound" 
                    onselectedindexchanged="ddlHospitals_SelectedIndexChanged" >
                </asp:DropDownList>
                <asp:HyperLink ID="hlAddHospital" runat="server" NavigateUrl="~/Admin/AddHospital.aspx" Target="_blank"><asp:Image runat="server" ID="Image21" ImageUrl="~/Images/add.png" /></asp:HyperLink>
                <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Hospital"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="Label1" runat="server" Text="Referring Physician:" Font-Bold="true" Width="160px"></asp:Label>
                <asp:DropDownList ID="ddlRefPhy" runat="server" 
                    ondatabound="ddlRefPhy_DataBound" ></asp:DropDownList>
                <asp:HyperLink ID="hlAddUser" runat="server" NavigateUrl="~/Admin/AddUser.aspx" Target="_blank"><asp:Image runat="server" ID="Image2" ImageUrl="~/Images/add.png" /></asp:HyperLink>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlRefPhy" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
            </div>
            <div>
                <asp:Label ID="Label4" runat="server" Text="Body Part Examined:" Font-Bold="true" Width="160px"></asp:Label>    
                <asp:DropDownList ID="ddlBodyParts" runat="server" 
                    ondatabound="ddlBodyParts_DataBound" >
                </asp:DropDownList> 
                <asp:RequiredFieldValidator ID="rfv2" ControlToValidate="ddlBodyParts" runat="server" ErrorMessage="*" InitialValue="[-- Select --]" ValidationGroup="Update"></asp:RequiredFieldValidator>        
            </div>
            <div>
                <div style="float:left;width:165px;margin-top:30px;">
                    <asp:Label ID="Label2" runat="server" Text="Tech Comments:" Font-Bold="true" Width="160px"></asp:Label>
                </div>
                <div style="float:left;">
                    <asp:TextBox ID="tbTechComments" runat="server" TextMode="MultiLine" Rows="5" Width="340px"></asp:TextBox>
                </div>
                <div style="clear:both"></div>
            </div>
            <div>
                
            </div>
            <div>
                <div style="float:left;width:165px;margin-top:30px;">
                    <asp:Label ID="Label5" runat="server" Text="Rejection Reason:" Font-Bold="true" Width="160px"></asp:Label>
                </div>
                <div style="float:left;">
                    <asp:TextBox ID="tbRejectionReason" runat="server" TextMode="MultiLine" Rows="5" Width="340px" ReadOnly="true"></asp:TextBox>
                </div>
                <div style="clear:both"></div>
            </div>	
                
            <div>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                    
                </table>
            </div>
	    </div>	
    </div>    
    
    <div style="text-align:center">
        <asp:Button ID="btnSave" runat="server" Text="Update" onclick="btnSave_Click" ValidationGroup="Update" Visible="false" OnClientClick="checkValidation(false);"/>
        <asp:Button ID="btnRelease" runat="server" Text="Update & Release to Radiologist" onclick="btnRelease_Click" ValidationGroup="Update" Visible="false" OnClientClick="checkValidation(false);"/>
        <asp:Button ID="btnHospital" runat="server" Text="Update Hospital Only" onclick="btnHospital_Click" ValidationGroup="Hospital" Visible="false" OnClientClick="checkValidation(true);"/>
        <asp:Button ID="btnUnrelease" runat="server" Text="Call Back this Exam" onclick="btnUnrelease_Click" Visible="false"/>
        <input type="button" value="Cancel" onclick="parent.closeStudyEditWindow();" />
    </div>
    
    </form>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs();
        });
	</script>
</body>
</html>
