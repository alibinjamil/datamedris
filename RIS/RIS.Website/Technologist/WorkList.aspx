<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="WorkList.aspx.cs" Inherits="Technologist_WorkList" Title="DataMed | Radiology Information System | Add Work List" %>

<%@ Register Src="../Common/TimeControl.ascx" TagName="TimeControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script language="javascript" clientvalidationfunction="ValidateDate" controltovalidate="dcD">

          function ValidateDate(sender, args)
          {
            var obj = document.getElementById("dcDOB_ddlMonth");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;
                return false;
            }
            obj = document.getElementById("dcDOB_ddlDay");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }                
            obj = document.getElementById("dcDOB_ddlYear");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }
            return true;        
          }

        </script>
                <table class="dataEntryTable">
                    <tr>
                        <td align="center" class="heading" colspan="3" style="font-size: 14pt">
                            Add Work List</td>
                    </tr>
                    <tr>
                        <td align="center" class="heading" colspan="3">
                            <asp:Label ID="lblConfirmSave" runat="server" Text="Please confirm the data before saving." Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="heading" colspan="3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="width: 150px;" align="right" class="heading">
                            Patient ID (MRN):</td>
                        <td style="width: 150px;" align="left">
                            <asp:TextBox ID="tbPatId" runat="server" Width="153px" OnTextChanged="OnPatientIdChange" autopostback="true" CssClass="textBoxStyle"></asp:TextBox>
                            <asp:Label ID="lblPatientId" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPatId"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 150px; height: 22px;">
                            First Name:</td>
                        <td align="left" style="width: 150px; height: 22px;">
                            <asp:TextBox ID="tbPatFName" runat="server" Width="153px" CssClass="textBoxStyle"></asp:TextBox>
                            <asp:Label ID="lblFirstName" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px; height: 22px;" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbPatFName"></asp:RequiredFieldValidator></td>
                        
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 150px; height: 22px;">
                            Last Name:</td>
                        <td align="left" style="width: 150px; height: 22px;">
                            <asp:TextBox ID="tbPatLName" runat="server" Width="153px" CssClass="textBoxStyle"></asp:TextBox>
                            <asp:Label ID="lblLastName" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px; height: 22px;" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="tbPatLName"></asp:RequiredFieldValidator></td>
                        
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 150px; height: 16px;">
                            Date of Birth:
                            <br />
                            <asp:Label ID="lblBO" runat="server" Text="(" Visible="False"></asp:Label>
                            <asp:Label ID="lblYear" runat="server" ForeColor="Blue" Text="yyyy" Visible="False"></asp:Label>
                            <asp:Label ID="lblMonth" runat="server" ForeColor="Green" Text="mm" Visible="False"></asp:Label>
                            <asp:Label ID="lblDay" runat="server" ForeColor="Brown" Text="dd" Visible="False"></asp:Label>
                            <asp:Label ID="lblBE" runat="server" Text=")" Visible="False"></asp:Label></td>
                        <td align="left" style="width: 150px; height: 16px;">
                            <uc1:DateControl id="dcDOB" runat="server">
                            </uc1:DateControl>
                            <asp:Label ID="lblDOB" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp;
                        </td>
                        <td style="width:100px; height: 16px;" align="left">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="*" ClientValidationFunction="ValidateDate"></asp:CustomValidator></td>
                        
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 150px">
                            Gender:
                        </td>
                        <td align="left" style="width: 150px">
                            <asp:RadioButtonList ID="rblGender" runat="server" Height="14px" Width="74px" RepeatDirection="Horizontal">
                                <asp:ListItem>M</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblGender" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="width:100px" align="left">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rblGender"
                                ErrorMessage="*"></asp:RequiredFieldValidator></td>
                        
                    </tr>
                    <tr>
                        <td style="width: 150px" align="right" class="heading">
                            Referring Physician:
                        </td>
                        <td align="left" style="width: 150px">
                            <asp:DropDownList ID="ddlRefPhysician" runat="server" Width="153px" CssClass="dropDownListStyle">
                            </asp:DropDownList>
                            <asp:Label ID="lblRefPhy" runat="server" Visible="False"></asp:Label>
                        </td>
                        <td style="width:100px" align="left">
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="*" ClientValidationFunction="ValidateDDL" ControlToValidate="ddlRefPhysician"></asp:CustomValidator></td>
                        
                    </tr>
                    <tr>
                        <td style="width: 150px" align="right" class="heading">
                            Modality:</td>
                        <td style="width: 150px" align="left">
                            <asp:DropDownList ID="ddlModality" runat="server" Width="153px" OnSelectedIndexChanged="OnModalityChange" AutoPostBack="True" CssClass="dropDownListStyle">
                            </asp:DropDownList>
                            <asp:Label ID="lblModality" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px" align="left">
                            <asp:CustomValidator ID="CustomValidator4" runat="server" ErrorMessage="*" ClientValidationFunction="ValidateDDL" ControlToValidate="ddlModality"></asp:CustomValidator></td>
                        
                    </tr>
                    <tr>
                        <td style="width: 150px" align="right" class="heading">
                            Station Name:</td>
                        <td style="width: 150px" align="left">
                            <asp:DropDownList ID="ddlStationName" runat="server" Width="153px" CssClass="dropDownListStyle">
                            </asp:DropDownList>
                            <asp:Label ID="lblStation" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px" align="left">
                            <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="*" ClientValidationFunction="ValidateDDL" ControlToValidate="ddlStationName"></asp:CustomValidator></td>
                            
                    </tr>
                    <tr>
                        <td style="width: 150px" align="right" class="heading">
                            Procedure:</td>
                        <td style="width: 150px" align="left">
                            <asp:DropDownList ID="ddlProcedure" runat="server" Width="266px" CssClass="dropDownListStyle">
                            </asp:DropDownList>
                            <asp:Label ID="lblProcedure" runat="server" Visible="False"></asp:Label></td>
                        <td style="width:100px" align="left">
                            <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="*" ClientValidationFunction="ValidateDDL" ControlToValidate="ddlProcedure" ></asp:CustomValidator></td>
                        
                    </tr>                
                    <tr>
                        <td align="right" class="heading" style="width: 150px; height: 18px;">
                        </td>
                        <td align="left" style="width: 150px; height: 18px;">
                            &nbsp;</td>
                        <td style="width:100px; height: 18px;" align="left">
                        </td>
                            
                    </tr>
                    <tr>
                        <td align="center"  colspan="3" >
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CssClass="buttonStyle" Width="100px"/>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"  CssClass="buttonStyle" Width="100px" Visible="False"/>
                            <asp:Button ID="btnConfirmSave" runat="server" Text="Confirm Save"  CssClass="buttonStyle" Width="150px" OnClick="btnConfirmSave_Click" Visible="False"/></td>
                         
                    </tr>
                </table>
    <br />
</asp:Content>

