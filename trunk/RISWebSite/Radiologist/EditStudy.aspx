﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditStudy.aspx.cs" Inherits="Radiologist_EditStudy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
   
</head>
<body>
    <form id="form1" runat="server">   
  
    <div>
        <asp:Label ID="lblClient" runat="server" Text="Client:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlClient" runat="server" DataSourceID="odsClients"
            DataTextField="Name" DataValueField="ClientId" 
            ondatabound="ddlClient_DataBound" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfv3" ControlToValidate="ddlClient" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>        
        <asp:HyperLink ID="hlAddAttachment1" runat="server" Target="_blank">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/attach.png" AlternateText="Attach a new document to this Exam"/>                        
        </asp:HyperLink>
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Hospital:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlHospitals" runat="server" DataSourceID="odsHospitals" AutoPostBack="True"
            DataTextField="Name" DataValueField="HospitalId" 
            ondatabound="ddlHospitals_DataBound" >
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfv1" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlHospitals" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Hospital"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Referring Physician:" Font-Bold="true" Width="200px"></asp:Label>
        <asp:DropDownList ID="ddlRefPhy" runat="server" 
            ondatabound="ddlRefPhy_DataBound" DataSourceID="odsUsers" DataTextField="Name" 
            DataValueField="UserId" ></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlRefPhy" runat="server" ErrorMessage="*" InitialValue="-1" ValidationGroup="Update"></asp:RequiredFieldValidator>
    </div>
    <div>
        <asp:Label ID="Label4" runat="server" Text="Body Part Examined:" Font-Bold="true" Width="200px"></asp:Label>    
        <asp:DropDownList ID="ddlBodyParts" runat="server" >
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
        <asp:ObjectDataSource ID="odsHospitals" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetHospitalsForUser" 
            TypeName="HospitalsTableAdapters.tHospitalsTableAdapter">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="0" Name="userId" SessionField="LoggedInUserId" 
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsUsers" runat="server" 
            OldValuesParameterFormatString="original_{0}" 
            SelectMethod="GetUsersForHospital" 
            TypeName="UsersTableAdapters.tUsersTableAdapter">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlClient" DefaultValue="0" Name="clientId" 
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlHospitals" DefaultValue="0" 
                    Name="hospitalId" PropertyName="SelectedValue" Type="Int32" />
                <asp:Parameter DefaultValue="3" Name="roleId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsClients" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllClients" 
            TypeName="ClientTableAdapters.tClientsTableAdapter"></asp:ObjectDataSource>
    </div>
    
    </form>
</body>
</html>
