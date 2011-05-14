<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditStudy.aspx.cs" Inherits="Radiologist_EditStudy" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
   
</head>
<body>
    <form id="form1" runat="server">   
  
    <div>
        <asp:Label ID="Label6" runat="server" Text="Patient ID:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:TextBox ID="tbPatientId" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="tbPatientId" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>        
        <asp:HyperLink ID="hlAddAttachment1" runat="server" Target="_blank">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attach a new document to this Exam"/>                        
        </asp:HyperLink>
    </div>
    <div>
        <asp:Label ID="Label7" runat="server" Text="Patient Name:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:TextBox ID="tbPatientName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="tbPatientName" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label8" runat="server" Text="Patient Date of Birth:" Font-Bold="true" Width="200px"></asp:Label>
        <ew:CalendarPopup ID="tbDOB" runat="server"></ew:CalendarPopup>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="tbDOB" runat="server" ErrorMessage="*" ValidationGroup="Update"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label9" runat="server" Text="Patient Gender:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:RadioButtonList ID="rbGender"   runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Selected="True">M</asp:ListItem>
            <asp:ListItem>F</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <asp:Label ID="Label10" runat="server" Text="Patient Weight:" Font-Bold="true" Width="200px"></asp:Label>
        <ew:NumericBox ID="tbPatientWeight"  runat="server"></ew:NumericBox>
        
    </div>
    <div>
        <asp:Label ID="lblClient" runat="server" Text="Client:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlClient" runat="server" 
            ondatabound="ddlClient_DataBound" AutoPostBack="True" 
            onselectedindexchanged="ddlClient_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfv3" ControlToValidate="ddlClient" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>        
        
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Hospital:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlHospitals" runat="server" AutoPostBack="True" 
            ondatabound="ddlHospitals_DataBound" 
            onselectedindexchanged="ddlHospitals_SelectedIndexChanged" >
        </asp:DropDownList>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/AddHospital.aspx" Target="_blank"><asp:Image runat="server" ID="Image21" ImageUrl="~/Images/add.png" /></asp:HyperLink>
        <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Hospital"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Referring Physician:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlRefPhy" runat="server" 
            ondatabound="ddlRefPhy_DataBound" ></asp:DropDownList>
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Admin/AddUser.aspx" Target="_blank"><asp:Image runat="server" ID="Image2" ImageUrl="~/Images/add.png" /></asp:HyperLink>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlRefPhy" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="Body Part Examined:" Font-Bold="true" Width="200px"></asp:Label>    
        <asp:DropDownList ID="ddlBodyParts" runat="server" 
            ondatabound="ddlBodyParts_DataBound" >
        </asp:DropDownList> 
        <asp:RequiredFieldValidator ID="rfv2" ControlToValidate="ddlBodyParts" runat="server" ErrorMessage="*" InitialValue="[-- Select --]" ValidationGroup="Update"></asp:RequiredFieldValidator>        
    </div>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Tech Comments:" Font-Bold="true" Width="200px"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="tbTechComments" runat="server" TextMode="MultiLine" Rows="5" Width="500px"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="Label5" runat="server" Text="Rejection Reason:" Font-Bold="true" Width="200px"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="tbRejectionReason" runat="server" TextMode="MultiLine" Rows="5" Width="500px" ReadOnly="true"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="btnSave" runat="server" Text="Update" onclick="btnSave_Click" ValidationGroup="Update" Visible="false"/>
        <asp:Button ID="btnRelease" runat="server" Text="Update & Release to Radiologist" onclick="btnRelease_Click" ValidationGroup="Update" Visible="false"/>
        <asp:Button ID="btnHospital" runat="server" Text="Update Hospital Only" onclick="btnHospital_Click" ValidationGroup="Hospital" Visible="false"/>
        <asp:Button ID="btnUnrelease" runat="server" Text="Call Back this Exam" onclick="btnUnrelease_Click" Visible="false"/>
        <input type="button" value="Cancel" onclick="parent.closeStudyEditWindow();" />
        </div>
    
    </form>
</body>
</html>
