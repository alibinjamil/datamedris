<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Common/Main.master" CodeFile="AddStudy.aspx.cs" Inherits="Technologist_AddStudy" Title="DataMed | Radiology Information System | Add Exam | Step 1" %>
<%@ Register Src="../Common/TimeControl.ascx" TagName="TimeControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>


    <asp:Content ID="conten2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
	<link rel="stylesheet" href="../Includes/jquery/themes/base/jquery.ui.all.css"/> 
	<script type="text/javascript" src="../Includes/jquery/jquery-1.4.2.min.js"></script> 
	<script type="text/javascript" src="../Includes/jquery/jquery-ui-1.8.1.custom.min.js"></script> 
    <script type="text/javascript" language="javascript">
        
        function SetPreReleaseStatus(){
            
        }
        function SetNewStatus(){
            
        }
        
        
        function ValidateDOB(sender,args)
        {         
            args.IsValid = ValidateDate("dcDOB");
        }     
        function ValidateExamDate(sender,args)
        {
            args.IsValid =  ValidateDate("dcExamDate");
        }
        function ValidateTime(sender,args)
        {
            var hourObj = document.getElementById("ctl00_ContentPlaceHolder1_Wizard1_tcExamTime_ddlHour");
            var timeObj = document.getElementById("ctl00_ContentPlaceHolder1_Wizard1_tcExamTime_ddlMin");
            if(timeObj.selectedIndex == 0 || hourObj.selectedIndex == 0)
                 args.IsValid = false;
            else
                args.IsValid = true;            
        }        
      function redirect()
      {      
        if(confirm("Are you sure to cancel?"))        
            window.location.replace("../Radiologist/StudyList.aspx");                   
      }
    </script>
    </asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Step 1: Add Exam Information</h1>
    <table style="left: 0px; width: 100%; top: 0px;height:100%" >
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Patient ID:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbPatId" runat="server" AutoPostBack="True" CssClass="textBoxStyle"
                                OnTextChanged="OnPatientIdChange" Width="153px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPatId"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Patient Name:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbPatientName" runat="server" CssClass="textBoxStyle" 
                                Width="153px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPatientName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Patient Weight:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbPatWeight" runat="server" CssClass="textBoxStyle" 
                                Width="153px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Date of Birth:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <uc1:DateControl ID="dcDOB" runat="server" />
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateDOB"
                                ErrorMessage="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Gender:
                        </td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:RadioButtonList ID="rblGender" runat="server" Height="14px" RepeatDirection="Horizontal"
                                Width="74px" >
                                <asp:ListItem>M</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="left" style="width: 10%; height: 20px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rblGender"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                <td align="right" class="heading" style="width: 30%;">
                            Client:&nbsp; 
                </td>
                <td align="left" style="width: 60%; height: 20px;">
                    <asp:DropDownList ID="ddlClient" runat="server" 
                        CssClass="dropDownListStyle" 
                        OnDataBound="ddlClient_DataBound" 
                        OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" 
                        AutoPostBack="True" >
                    </asp:DropDownList>
                 </td>
                <td align="left" style="width: 10%; height: 20px;">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlClient"
                                ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>                    </td>
                 
                 </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; ">
                            Hospital:
                        </td>
                        <td align="left" style="width: 60%; height: 20px;">
                            <asp:DropDownList ID="ddlHospital" runat="server" 
                                CssClass="dropDownListStyle" OnDataBound="ddlHospital_DataBound" 
                                OnSelectedIndexChanged="ddlHospital_SelectedIndexChanged" 
                                AutoPostBack="true">
                            </asp:DropDownList>
                            </td>
                        <td align="left" style="width: 10%; height: 20px;">
<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlHospital"
                                ErrorMessage="*" InitialValue="0"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; ">
                            Exam Date:
                        </td>
                        <td align="left" style="width: 60%; height: 20px;">
                            <uc1:DateControl ID="dcExamDate" runat="server" />
                        </td>
                        <td align="left" style="width: 10%; height: 20px;">
                            <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                ClientValidationFunction="ValidateExamDate" ErrorMessage="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%">
                            Modality:</td>
                        <td align="left" style="width: 60%; height: 20px">
                            <asp:DropDownList ID="ddlModality" runat="server" CssClass="dropDownListStyle" 
                                AutoPostBack="True" OnDataBound="ddlModality_DataBound" 
                                OnSelectedIndexChanged="ddlModality_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="ValidateDDL"
                                ErrorMessage="*" ControlToValidate="ddlModality"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 22px">
                            Procedure:</td>
                        <td align="left" style="width: 60%; height: 22px">
                            <asp:DropDownList ID="ddlProcedures" runat="server" 
                                CssClass="dropDownListStyle" OnDataBound="ddlProcedures_DataBound">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%; height: 22px">
                            <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="ValidateDDL"
                                ErrorMessage="*" ControlToValidate="ddlProcedures"></asp:CustomValidator>
                        </td>
                    </tr>
                    
                    <tr><td align="right" class="heading" style="width: 30%; height: 22px;">
                            Referring Physician:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:DropDownList ID="ddlRef" runat="server" CssClass="dropDownListStyle" OnDataBound="ddlRef_DataBound" >
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateDDL"
                                ControlToValidate="ddlRef" ErrorMessage="*"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 22px;">
                            Tech Comments:&nbsp;
                        </td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbTechComments" runat="server" Rows="5" TextMode="MultiLine" 
                                Width="500px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            &nbsp;</td>
                    </tr>
                                
                </table>
                

                <div style="text-align:center;width:100%">
                    <asp:Button runat="server" ID="btnSave" Text="Save Exam & Continue" 
                        onclick="btnSave_Click" />
                </div>
 </asp:Content>
