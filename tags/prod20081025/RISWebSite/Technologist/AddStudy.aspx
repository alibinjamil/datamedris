<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddStudy.aspx.cs" Inherits="Technologist_AddStudy" Title="DataMed | Radiology Information System | Add Finding Report" %>
<%@ Register Src="../Common/TimeControl.ascx" TagName="TimeControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="../Includes/yui/2.7.0/build/yahoo-dom-event/yahoo-dom-event.js"></script>

    <script type="text/javascript" src="../Includes/yui/2.7.0/build/animation/animation-min.js"></script>
    <script type="text/javascript" src="../Includes/yui/2.7.0/build/datasource/datasource-min.js"></script>
    <script type="text/javascript" src="../Includes/yui/2.7.0/build/autocomplete/autocomplete-min.js"></script>


    <script type="text/javascript" language="javascript">
        
        function CopyClipboard()
        {
            var text = window.clipboardData.getData("Text");
            var obj = document.getElementById("ctl00_ContentPlaceHolder1_Wizard1_tbFinding");
            obj.value = text;
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
      
    <asp:Wizard ID="Wizard1" runat="server" Height="300px" Width="600px" BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ActiveStepIndex="0" OnFinishButtonClick="Wizard1_FinishButtonClick">
        <WizardSteps>
            <asp:WizardStep runat="server" StepType="Start" Title="Patient Information" >     

                <table class="dataEntryTable" style="left: 0px; width: 100%; top: 0px" >
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
                            First Name:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbPatFName" runat="server" CssClass="textBoxStyle" Width="153px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbPatFName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Last Name:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:TextBox ID="tbPatLName" runat="server" CssClass="textBoxStyle" Width="153px"></asp:TextBox>
                        </td>
                        <td align="left" style="width: 10%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbPatLName"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
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
                </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Step" Title="Study Information">
                <table class="dataEntryTable" style="left: 0px; top: 0px; width: 100%;">
                <tr>
                <td align="right" class="heading" style="width: 30%;">
                            Exam Date: 
                </td>
                <td align="left" style="width: 60%; height: 20px;">
                    <uc1:DateControl ID="dcExamDate" runat="server" /> 
                 </td>
                <td align="left" style="width: 10%; height: 20px;">
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateExamDate"
                        ErrorMessage="*"></asp:CustomValidator>
                </td>
                 
                 </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%">
                            Exam Time:</td>
                        <td align="left" style="width: 60%; height: 20px;">
                            <uc2:TimeControl ID="tcExamTime" runat="server">
                            </uc2:TimeControl>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%">
                            Modality:</td>
                        <td align="left" style="width: 60%; height: 20px">
                            <asp:DropDownList ID="ddlModality" runat="server" CssClass="dropDownListStyle" DataSourceID="ODSModalities"
                                DataTextField="Name" DataValueField="ModalityId" AutoPostBack="True" OnDataBound="ddlModality_DataBound">
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
                            <asp:DropDownList ID="ddlProcedures" runat="server" CssClass="dropDownListStyle"
                                DataSourceID="ODSProcedures" DataTextField="ProcedureName" DataValueField="ProcedureId" OnDataBound="ddlProcedures_DataBound">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%; height: 22px">
                            <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="ValidateDDL"
                                ErrorMessage="*" ControlToValidate="ddlProcedures"></asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%;">
                    Radiologist:</td>
                        <td align="left" style="width: 60%; height: 20px">
                            <div id="myAutoComplete" style="width:15em;padding-bottom:2em;">
                                <asp:TextBox ID="tbRadiologist" runat="server" CssClass="textBoxStyle"></asp:TextBox>	                            
	                            <div id="myContainer"></div>
                            </div>
                        </td>
                        <td align="left" style="width: 50px; height: 10%">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbRadiologist"
                                ErrorMessage="*"></asp:RequiredFieldValidator>                    
                        </td>
                    </tr>
                    
                    <tr><td align="right" class="heading" style="width: 30%; height: 22px;">
                            Referring Physician:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:DropDownList ID="ddlRef" runat="server" CssClass="dropDownListStyle" DataSourceID="ODSRefPhysician" DataTextField="Name" DataValueField="UserId" OnDataBound="ddlRef_DataBound">
                            </asp:DropDownList>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="ValidateDDL"
                                ControlToValidate="ddlRef" ErrorMessage="*"></asp:CustomValidator>
                        </td>
                    </tr>
                 </table>
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Finish" Title="Finding Information">
                <table class="dataEntryTable">
                <tr>
                <td align="center" class="heading" style="height: 16px;" colspan="3">
                    Copy report from other system to clipboard and then click the "Copy from Clipboard"
                    button to insert report below.</td>
                 
                 </tr>
                    <tr>
                        <td align="right" class="heading" style="height: 16px; text-align: center;" colspan="3">
                            <input id="btnCopy" type="button" value="Copy from Clipboard" class="buttonStyle" onclick="CopyClipboard();"/></td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="text-align: left;" colspan="3">
                            &nbsp;<asp:TextBox ID="tbFinding" runat="server" Rows="14" TextMode="MultiLine" Width="94%" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbFinding"
                                ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                 </table>            
            </asp:WizardStep>
        </WizardSteps>
        <StepStyle BorderWidth="0px" ForeColor="#5D7B9D" />
        <SideBarStyle BackColor="Gray" Width="150px" BorderWidth="0px" Font-Size="Larger" VerticalAlign="Top" HorizontalAlign="Right" />
        <NavigationButtonStyle CssClass="buttonStyle" />
        <SideBarButtonStyle BorderWidth="0px" Font-Names="Verdana" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" BorderStyle="None"  Font-Bold="True" Font-Size="0.9em"
            ForeColor="White" HorizontalAlign="Center" />
        <HeaderTemplate>
            <asp:Label ID="Label1" runat="server" CssClass="pageHeading" ForeColor="white"  Text="Add Report Manually"></asp:Label>
        </HeaderTemplate>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" CssClass="buttonStyle"
                Text="Next" OnClick="StartNextButton_Click" />            
            <input id="cmdCancel" class="buttonStyle" type="button" value="Cancel" onclick="redirect()"/>
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <asp:Button ID="StepPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                CssClass="buttonStyle" Text="Previous" />
            <asp:Button ID="StepNextButton" runat="server" CommandName="MoveNext" CausesValidation="true" CssClass="buttonStyle"
                Text="Next" OnClick="StepNextButton_Click" />&nbsp;
            <input id="btnStepCancel" class="buttonStyle" type="button" value="Cancel" onclick="redirect()"/>
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <asp:Button ID="FinishPreviousButton" runat="server" CausesValidation="False" CommandName="MovePrevious"
                CssClass="buttonStyle" Text="Previous" />
            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" CssClass="buttonStyle"
                Text="Finish" />&nbsp;
            <input id="btnFinishCancel" class="buttonStyle" type="button" value="Cancel" onclick="redirect()"/>
        </FinishNavigationTemplate>
        
    </asp:Wizard>
    <asp:ObjectDataSource ID="ODSModalities" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllModalities" TypeName="ModalitiesTableAdapters.tModalitiesTableAdapter">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSProcedures" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetProceduresByModality" TypeName="ProceduresTableAdapters.ProcedureSelectCommandTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="Wizard1$ddlModality" DefaultValue="0" Name="ModalityId"
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    &nbsp;
    <asp:ObjectDataSource ID="ODSRefPhysician" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetActiveUsersByRole"
        TypeName="UsersTableAdapters.tUsersTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="3" Name="RoleId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODSRadiologist" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetActiveUsersByRole" TypeName="UsersTableAdapters.tUsersTableAdapter">
        <SelectParameters>
            <asp:Parameter DefaultValue="2" Name="RoleId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<script type="text/javascript">
var arrayRadiologists = [
<%=Radiologists %>
];

YAHOO.example.BasicLocal = function() {
    // Use a LocalDataSource
    var oDS = new YAHOO.util.LocalDataSource(arrayRadiologists);

    // Instantiate the AutoComplete
    var oAC = new YAHOO.widget.AutoComplete("ctl00_ContentPlaceHolder1_Wizard1_tbRadiologist", "myContainer", oDS);
    oAC.prehighlightClassName = "yui-ac-prehighlight";
    oAC.useShadow = true;
    
    return {
        oDS: oDS,
        oAC: oAC
    };
}();
</script>
    
</asp:Content>

