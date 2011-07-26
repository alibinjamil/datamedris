<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="AddStudy.aspx.cs" Inherits="Technologist_AddStudy" Title="DataMed | Radiology Information System | Add Finding Report" %>
<%@ Register Src="../Common/TimeControl.ascx" TagName="TimeControl" TagPrefix="uc2" %>
<%@ Register Src="../Common/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="../Includes/yui/2.7.0/build/yahoo-dom-event/yahoo-dom-event.js"></script>

    <script type="text/javascript" src="../Includes/yui/2.7.0/build/animation/animation-min.js"></script>
    <script type="text/javascript" src="../Includes/yui/2.7.0/build/datasource/datasource-min.js"></script>
    <script type="text/javascript" src="../Includes/yui/2.7.0/build/autocomplete/autocomplete-min.js"></script>


    <script type="text/javascript" language="javascript">
        
        function SetPreReleaseStatus(){
            document.getElementById("<%=hfStatus.ClientID%>").value = "8";
        }
        function SetNewStatus(){
            document.getElementById("<%=hfStatus.ClientID%>").value = "1";
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

    <asp:HiddenField ID="hfStatus" runat="server" Value="8" />
    <asp:Wizard ID="Wizard1" runat="server" Height="500px" Width="1000px" 
        BackColor="#F7F6F3" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" 
        Font-Names="Verdana" Font-Size="0.8em" ActiveStepIndex="0" 
        OnFinishButtonClick="Wizard1_FinishButtonClick" 
        onactivestepchanged="Wizard1_ActiveStepChanged">
        <WizardSteps>
            <asp:WizardStep runat="server" StepType="Start" Title="Patient Information" >     

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
                            Client:&nbsp; 
                </td>
                <td align="left" style="width: 60%; height: 20px;">
                    <asp:DropDownList ID="ddlClient" runat="server" 
                        CssClass="dropDownListStyle" DataSourceID="ODSClients" 
                        DataTextField="Name" DataValueField="ClientId" 
                        OnDataBound="ddlClient_DataBound" 
                        OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" >
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
                                AutoPostBack="true" DataSourceID="ODSHospital" DataTextField="Name" 
                                DataValueField="HospitalId">
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
            </asp:WizardStep>
            <asp:WizardStep runat="server" StepType="Finish" Title="Confirm Information" >
               <table style="left: 0px; width: 100%; top: 0px;height:100%" >
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Patient ID:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblPatientID" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            First Name:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Last Name:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblLastName" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Date of Birth:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblDOB" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 20px;">
                            Gender:
                        </td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 20px;">
                            &nbsp;</td>
                    </tr>
                <tr>
                <td align="right" class="heading" style="width: 30%;">
                            Client:&nbsp; 
                </td>
                <td align="left" style="width: 60%; height: 20px;">
                    <asp:Label ID="lblClient" runat="server"></asp:Label>
                 </td>
                <td align="left" style="width: 10%; height: 20px;">
                    &nbsp;</td>
                 
                 </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; ">
                            Hospital:
                        </td>
                        <td align="left" style="width: 60%; height: 20px;">
                            <asp:Label ID="lblHospital" runat="server"></asp:Label>
                            </td>
                        <td align="left" style="width: 10%; height: 20px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; ">
                            Exam Date:
                        </td>
                        <td align="left" style="width: 60%; height: 20px;">
                            <asp:Label ID="lblExamDate" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 20px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%">
                            Modality:</td>
                        <td align="left" style="width: 60%; height: 20px">
                            <asp:Label ID="lblModality" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 22px">
                            Procedure:</td>
                        <td align="left" style="width: 60%; height: 22px">
                            <asp:Label ID="lblProcedure" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 22px">
                            &nbsp;</td>
                    </tr>
                    
                    <tr><td align="right" class="heading" style="width: 30%; height: 22px;">
                            Referring Physician:</td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblRefPhy" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" class="heading" style="width: 30%; height: 22px;">
                            Tech Comments:&nbsp;
                        </td>
                        <td align="left" style="width: 60%; height: 22px;">
                            <asp:Label ID="lblTechComments" runat="server"></asp:Label>
                        </td>
                        <td align="left" style="width: 10%; height: 22px;">
                            &nbsp;</td>
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
            <asp:Button ID="PrereleaseButton" runat="server" CommandName="MoveComplete" CssClass="buttonStyle"
                Text="Save without Releasing to Radiologist" OnClientClick="SetPreReleaseStatus();" />
            <asp:Button ID="FinishButton" runat="server" CommandName="MoveComplete" CssClass="buttonStyle"
                Text="Save & Release to Radiologist" OnClientClick="SetNewStatus();"/>&nbsp;
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
    <asp:ObjectDataSource ID="ODSHospital" runat="server" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetHospitalsForUser" 
        TypeName="HospitalsTableAdapters.tHospitalsTableAdapter" UpdateMethod="Update">
        <DeleteParameters>
            <asp:Parameter Name="Original_HospitalId" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="ClientId" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="Int32" />
            <asp:Parameter Name="CreationDate" Type="DateTime" />
            <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
            <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
            <asp:Parameter Name="Original_HospitalId" Type="Int32" />
        </UpdateParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="userId" 
                SessionField="LoggedInUserId" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="ClientId" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="Code" Type="String" />
            <asp:Parameter Name="CreatedBy" Type="Int32" />
            <asp:Parameter Name="CreationDate" Type="DateTime" />
            <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
            <asp:Parameter Name="LastUpdateDate" Type="DateTime" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Fax" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    &nbsp;<asp:ObjectDataSource ID="ODSClients" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetClientsForUser" 
        TypeName="ClientTableAdapters.tClientsTableAdapter">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="userId" 
                SessionField="LoggedInUserId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
&nbsp;<asp:ObjectDataSource ID="ODSRadiologist" runat="server" OldValuesParameterFormatString="original_{0}"
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

