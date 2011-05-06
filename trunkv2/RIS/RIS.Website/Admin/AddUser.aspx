<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="Admin_AddUser" Title="DataMed | Radiology Information System | Add User" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>

<%@ Register src="../Common/SecretQuestions.ascx" tagname="SecretQuestions" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="3" cellspacing="5">
        <tr>
            <td align="right">>></td>
            <td align="left">
                <asp:HyperLink ID="hlUsersList" runat="server" NavigateUrl="~/Admin/UsersList.aspx">Users List</asp:HyperLink>
            </td>
        </tr>
        <tr><td colspan="2">&nbsp;</td></tr>
        <asp:Panel ID="pnlHospital" runat="server" Visible="false">
        <!--Add panel for clients-->
        
        <tr>
            <td align="right">Hospital:</td>
            <td align="left">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <asp:ListBox ID="lbNotHospitals" runat="server"  
                                DataTextField="Name" DataValueField="HospitalId" Width="250px"></asp:ListBox>
                        
                        </td>
                        <td align="center" style="width:170px;">
                            <asp:Button ID="btnAddHospital" runat="server" Text="Add Hospital(s) >>" onclick="btnAddHospital_Click" CssClass="buttonStyle" Width="150px" ValidationGroup="Hospital"/>
                            <asp:Button ID="btnRemoveHospital" runat="server" Text="<< Remove Hospital(s)" onclick="btnRemoveHospital_Click" CssClass="buttonStyle" Width="150px" ValidationGroup="Hospital"/>
                        </td>
                        <td>
                            <asp:ListBox ID="lbHospitals" runat="server"  
                                DataTextField="Name" DataValueField="HospitalId" Width="250px"></asp:ListBox>

                        </td>
                    </tr>
                </table>
            </td>
                                
        </tr>
        </asp:Panel>
        <tr>
            <td  align="right">Role:</td>
            <td align="left">
                <asp:DropDownList ID="ddlRoles" runat="server" 
                    onselectedindexchanged="ddlRoles_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv3" runat="server" ErrorMessage="*" ControlToValidate="ddlRoles" InitialValue="0" ValidationGroup="Add"></asp:RequiredFieldValidator>                 </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:CheckBox ID="cbSms" runat="server" 
                    Text="Send SMS to this user when ever Exam is Verified" 
                    oncheckedchanged="cbSms_CheckedChanged" Visible="true" 
                    AutoPostBack="True"/>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:CheckBox ID="cbAllowOthersToViewExam" runat="server" 
                    Text="Allow other Referring Physicians to view the Exams of this User" 
                    oncheckedchanged="cbSms_CheckedChanged" Visible="false"        />            
            </td>
        </tr>

        <asp:Panel ID="pnlRefPhy" runat="server" Visible="true">
        
        <tr>
            <td align="right">Carrier: </td>
            <td align="left">
                <asp:DropDownList ID="ddlCarriers" runat="server"                    
                     DataTextField="Name" DataValueField="CarrierId" OnDataBound="ddlCarriers_DataBound"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCarrier" runat="server"
                    ErrorMessage="*" CssClass="errorText" ControlToValidate="ddlCarriers" InitialValue="0" ValidationGroup="Add" Enabled="false"></asp:RequiredFieldValidator>
                
            </td>
        </tr>
        
        <tr>
            <td align="right">Cell Number:</td>
            <td align="left">
                <ew:NumericBox ID="tbCellNumber"  runat="server">
                
                &nbsp;
                
                </ew:NumericBox>
                <asp:RequiredFieldValidator ID="rfvCellNumber" runat="server"
                    ErrorMessage="*" CssClass="errorText" ControlToValidate="tbCellNumber" ValidationGroup="Add" Enabled="false"></asp:RequiredFieldValidator>
                                
                
                
            </td>
        </tr>
        </asp:Panel>
        
        <tr>
            <td align="right">
             Name:
             </td>
             <td align="left">
                <asp:TextBox ID="tbName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" ErrorMessage="*" ControlToValidate="tbName" ValidationGroup="Add" CssClass="errorText"></asp:RequiredFieldValidator>                 
            </td>
        </tr>
        <tr>
            <td align="right">Login Name:</td>
            <td align="left">
                <asp:TextBox ID="tbLoginName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbLoginName" ValidationGroup="Add" CssClass="errorText"></asp:RequiredFieldValidator>                                 
            </td>
        </tr>
        <tr><td></td>
            <td align="left">
                <asp:LinkButton ID="lbChangePassword" runat="server" Visible="false" 
                    onclick="lbChangePassword_Click">Change Password</asp:LinkButton>
                <asp:LinkButton ID="lbCancelChangePassword" runat="server" Visible="false" 
                    onclick="lbCancelChangePassword_Click">Cancel Change Password</asp:LinkButton>
            </td>
        </tr>
        <asp:Panel ID="pnlChangePassword" runat="server">
        
        <tr>
            <td align="right">Password:</td>
            <td align="left">
                <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbPassword" ValidationGroup="Add" CssClass="errorText"></asp:RequiredFieldValidator>                 
            </td>
        </tr>
        <tr>
            <td align="right">Confirm Password:</td>
            <td align="left">
                <asp:TextBox ID="tbConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="tbConfirmPassword" ValidationGroup="Add" CssClass="errorText"></asp:RequiredFieldValidator>                 
            </td>
        </tr>
        </asp:Panel>
        <tr>
            <td align="right">Active?</td>
            <td align="left">
                <asp:CheckBox ID="cbActive" runat="server">
                </asp:CheckBox></td>
        </tr>
        <tr>
            <td align="right">Secret Question</td>
            <td align="left">
                <uc1:SecretQuestions ID="SecretQuestions1" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="right">Answer</td>
            <td align="left">
                <asp:TextBox ID="tbAnswer" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" ErrorMessage="*" 
                    ControlToValidate="tbAnswer" ValidationGroup="Add" CssClass="errorText"></asp:RequiredFieldValidator>                 
            </td>
        </tr>
        <tr>
            <td align="right">Force Reset Password?</td>
            <td align="left">
                <asp:CheckBox ID="cbResetPassword" runat="server">
                </asp:CheckBox></td>
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

    </asp:Content>

