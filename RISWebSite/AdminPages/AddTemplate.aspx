<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTemplate.aspx.cs" Title="DataMed | Radiology Information System | Add Templates"  validateRequest="false" MasterPageFile="~/Common/Main.master" Inherits="AdminPages_AddTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:4px;">Template Name:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:TextBox ID="tbName" runat="server" Width="580px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv1" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="tbName"></asp:RequiredFieldValidator>
        </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:4px;">Modality:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:DropDownList ID="ddlModalities" runat="server" Width="585px" 
                DataSourceID="ODSModalities" DataTextField="Name" DataValueField="ModalityId" 
                ondatabound="ddlModalities_DataBound">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfv6" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="ddlModalities" InitialValue="0"></asp:RequiredFieldValidator>
        </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:4px;">Body Part:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:TextBox ID="tbBodyPart" runat="server" Width="580px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv2" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="tbBodyPart"></asp:RequiredFieldValidator>                            </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:20px;">Heading:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:TextBox ID="tbHeading" runat="server" Rows="3" TextMode="MultiLine" 
                Width="580px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="tfv3" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="tbHeading"></asp:RequiredFieldValidator>
         </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:140px">Description:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:TextBox ID="tbDescription" runat="server" Rows="20" TextMode="MultiLine" 
                Width="580px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv4" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="tbDescription"></asp:RequiredFieldValidator>
        </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="margin-top:5px">
        <div class="heading" style="float:left;text-align:right;width:300px;margin-top:40px;">Impression:</div>
        <div style="float:left;text-align:left;width:600px;">
            <asp:TextBox ID="tbImpression" runat="server" Rows="5" TextMode="MultiLine" 
                Width="580px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfv5" runat="server" ErrorMessage="*" CssClass="errorText" ControlToValidate="tbImpression"></asp:RequiredFieldValidator>                
        </div>
        <div style="float:left;text-align:left;width:100px;"></div>
        <div style="clear:both"></div>
    </div>
    <div style="text-align:center">
        <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="buttonStyle" 
            Width="100px" onclick="btnAdd_Click"/>
        <asp:Button ID="btnUpdate" runat="server" Text="Save" CssClass="buttonStyle" 
            Width="100px" onclick="btnUpdate_Click" Visible="False"/>
    </div>
    <div>
        <asp:ObjectDataSource ID="ODSModalities" runat="server" DeleteMethod="Delete" 
            InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
            SelectMethod="GetAllModalities" 
            TypeName="ModalitiesTableAdapters.tModalitiesTableAdapter" 
            UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="Original_ModalityId" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="LastUpdatedBy" Type="Int32" />
                <asp:Parameter Name="Original_ModalityId" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="CreatedBy" Type="Int32" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>

