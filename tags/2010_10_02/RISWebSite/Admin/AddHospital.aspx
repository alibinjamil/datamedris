<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddHospital.aspx.cs" Inherits="Admin_AddHospital" Title="DataMed | Radiology Information System | Add Hospital" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table border="0" cellpadding="3" cellspacing="5">
        <tr>
            <td align="right">>></td>
            <td align="left">
                <asp:HyperLink ID="hlHospitalsList" runat="server" NavigateUrl="~/Admin/HospitalsList.aspx">Hospitals List</asp:HyperLink>
            </td>
        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td align="right">Client:</td>
            <td align="left">
                <asp:DropDownList ID="ddlClients" runat="server" 
                    DataSourceID="odsClients" DataTextField="Name" 
                    DataValueField="ClientId" ondatabound="ddlClients_DataBound"></asp:DropDownList>                
            </td>
        </tr>
        <tr>
            <td align="right">Name:</td>
            <td align="left">
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="tbName" ValidationGroup="Add"></asp:RequiredFieldValidator> 
            </td>
                                
        </tr>
        <tr>
            <td>Code:</td>
            <td align="left">
                <ew:NumericBox ID="tbCode" runat="server">
                </ew:NumericBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbCode" ValidationGroup="Add"></asp:RequiredFieldValidator>                
            </td>
        </tr>

        <tr>
            <td  align="right">Address:</td>
            <td align="left">
                <asp:TextBox ID="tbAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="*" ControlToValidate="tbAddress" ValidationGroup="Add"></asp:RequiredFieldValidator>                 </td>
        </tr>
        <tr>
            <td align="right">City:</td>
            <td align="left">
                <asp:TextBox ID="tbCity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="tbCity" ValidationGroup="Add"></asp:RequiredFieldValidator>                                 
            </td>
        </tr>
        
        <tr>
            <td align="right">State:</td>
            <td align="left">
                <asp:DropDownList ID="ddlStates" runat="server">
	<asp:ListItem Value="AL">Alabama</asp:ListItem>
	<asp:ListItem Value="AK">Alaska</asp:ListItem>
	<asp:ListItem Value="AZ">Arizona</asp:ListItem>
	<asp:ListItem Value="AR">Arkansas</asp:ListItem>
	<asp:ListItem Value="CA">California</asp:ListItem>
	<asp:ListItem Value="CO">Colorado</asp:ListItem>
	<asp:ListItem Value="CT">Connecticut</asp:ListItem>
	<asp:ListItem Value="DC">District of Columbia</asp:ListItem>
	<asp:ListItem Value="DE">Delaware</asp:ListItem>
	<asp:ListItem Value="FL">Florida</asp:ListItem>
	<asp:ListItem Value="GA">Georgia</asp:ListItem>
	<asp:ListItem Value="HI">Hawaii</asp:ListItem>
	<asp:ListItem Value="ID">Idaho</asp:ListItem>
	<asp:ListItem Value="IL">Illinois</asp:ListItem>
	<asp:ListItem Value="IN">Indiana</asp:ListItem>
	<asp:ListItem Value="IA">Iowa</asp:ListItem>
	<asp:ListItem Value="KS">Kansas</asp:ListItem>
	<asp:ListItem Value="KY">Kentucky</asp:ListItem>
	<asp:ListItem Value="LA">Louisiana</asp:ListItem>
	<asp:ListItem Value="ME">Maine</asp:ListItem>
	<asp:ListItem Value="MD">Maryland</asp:ListItem>
	<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
	<asp:ListItem Value="MI">Michigan</asp:ListItem>
	<asp:ListItem Value="MN">Minnesota</asp:ListItem>
	<asp:ListItem Value="MS">Mississippi</asp:ListItem>
	<asp:ListItem Value="MO">Missouri</asp:ListItem>
	<asp:ListItem Value="MT">Montana</asp:ListItem>
	<asp:ListItem Value="NE">Nebraska</asp:ListItem>
	<asp:ListItem Value="NV">Nevada</asp:ListItem>
	<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
	<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
	<asp:ListItem Value="NM">New Mexico</asp:ListItem>
	<asp:ListItem Value="NY">New York</asp:ListItem>
	<asp:ListItem Value="NC">North Carolina</asp:ListItem>
	<asp:ListItem Value="ND">North Dakota</asp:ListItem>
	<asp:ListItem Value="OH">Ohio</asp:ListItem>
	<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
	<asp:ListItem Value="OR">Oregon</asp:ListItem>
	<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
	<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
	<asp:ListItem Value="SC">South Carolina</asp:ListItem>
	<asp:ListItem Value="SD">South Dakota</asp:ListItem>
	<asp:ListItem Value="TN">Tennessee</asp:ListItem>
	<asp:ListItem Value="TX">Texas</asp:ListItem>
	<asp:ListItem Value="UT">Utah</asp:ListItem>
	<asp:ListItem Value="VT">Vermont</asp:ListItem>
	<asp:ListItem Value="VA">Virginia</asp:ListItem>
	<asp:ListItem Value="WA">Washington</asp:ListItem>
	<asp:ListItem Value="WV">West Virginia</asp:ListItem>
	<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
	<asp:ListItem Value="WY">Wyoming</asp:ListItem>
</asp:DropDownList>
                
            </td>
        </tr>
        <tr>
            <td align="right">Zip:</td>
            <td align="left">
                <asp:TextBox ID="tbZip" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator
                    ID="RegularExpressionValidator1" runat="server" ErrorMessage="!" 
                    ValidationExpression="\d{5}(-\d{4})?" ControlToValidate="tbZip"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbZip" ValidationGroup="Add"></asp:RequiredFieldValidator>                                 
            </td>
        </tr>
        <tr>
            <td>Phone:</td>
            <td align="left">
                <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="tbPhone" ValidationGroup="Add"></asp:RequiredFieldValidator>                                 
            
            </td>
        </tr>
        <tr>
            <td>Fax:</td>            
            <td align="left">
                <asp:TextBox ID="tbFax" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="tbFax" ValidationGroup="Add"></asp:RequiredFieldValidator>                                 
            
            </td>
        </tr>
        
        
        <tr>
            <td></td>
            <td align="left">
                <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" ValidationGroup="Add" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" 
                    onclick="btnUpdate_Click"  Visible="false" ValidationGroup="Add"/>
            </td>
        </tr>        
    </table>
    <asp:ObjectDataSource ID="odsClients" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetClientsForUser" 
        TypeName="ClientTableAdapters.tClientsTableAdapter">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="userId" 
                SessionField="LoggedInUserId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

