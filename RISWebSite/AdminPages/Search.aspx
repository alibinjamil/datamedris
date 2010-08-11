<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="AdminPages_Search" Title="DataMed | Radiology Information System | Search" %>

<%@ Register Src="../Common/DateControl.ascx" TagName="DateControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
<script language="javascript" clientvalidationfunction="ValidateFromDate" controltovalidate="dtFrom">

          function ValidateFromDate(sender, args)
          {
            var obj = document.getElementById("dtFrom_ddlMonth");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;
                return false;
            }
            obj = document.getElementById("dtFrom_ddlDay");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }                
            obj = document.getElementById("dtFrom_ddlYear");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }
            return true;        
          }
 </script>
 <script language="javascript" clientvalidationfunction="ValidateToDate" controltovalidate="dtTo">

          function ValidateToDate(sender, args)
          {
            var obj = document.getElementById("dtTo_ddlMonth");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;
                return false;
            }
            obj = document.getElementById("dtTo_ddlDay");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }                
            obj = document.getElementById("dtTo_ddlYear");
            if(obj.selectedIndex == 0)
            {
                args.IsValid = false;            
                return false;
            }
            return true;        
          }
 </script>
 <script language="javascript" clientvalidationfunction="CompareDates">

          function CompareDates(sender, args)
          {
            var FromMonth = document.getElementById("dtFrom_ddlMonth").value ;
            var FromDay = document.getElementById("dtFrom_ddlDay").value;
            var FromYear = document.getElementById("dtFrom_ddlYear").value;
            var ToMonth = document.getElementById("dtTo_ddlMonth").value ;
            var ToDay = document.getElementById("dtTo_ddlDay").value;
            var ToYear = document.getElementById("dtTo_ddlYear").value;
            if(FromMonth!=0 && FromDay !=0 && FromYear !=0 && ToMonth !=0 && ToDay !=0 && ToYear !=0)
            {
                var fromDate = new Date(FromYear,FromMonth-1,FromDay);
                var toDate = new Date(ToYear,ToMonth-1,ToDay);
                
                if (fromDate > toDate)
                {
                    args.IsValid = false;  
                    return false; 
                    }
                else
                    return true;
            }            
            else
            {
                return true;        
            }
          }
 </script>
 
<div align="center">
 
      <table class="dataEntryTable">
          <tr>
              <td align="right" class="heading">
              </td>
              <td align="left" style="text-align: center">
                  </td>
              <td>
                </td>
          </tr>
          <tr>
             
              <td align="center"colspan="3">
                 <table class="dataEntryTable" width="100%">
                     <tr>
                         <td align="right" class="heading">
                         </td>
                         <td align="left">
                  <asp:Label ID="lblError" runat="server" Width="132px"></asp:Label></td>
                     </tr>
                  <tr>
                      <td align="right" class="heading">
                          From Date:</td>
                      <td align="left">
                          <uc1:DateControl ID="dtFrom" runat="server" />
                          <asp:CustomValidator ClientValidationFunction="ValidateFromDate" Display="Dynamic" ErrorMessage="*" ID="cvFromDate" runat="server"></asp:CustomValidator></td>
                   </tr>
                  <tr>
                      <td align="right" class="heading">
                          To Date:</td>
                      <td align="left" >
                          <uc1:DateControl ID="dtTo" runat="server" />
                          <asp:CustomValidator ID="cvToDate" runat="server" ClientValidationFunction="ValidateToDate"
                              Display="Dynamic" ErrorMessage="*"></asp:CustomValidator>
                          <asp:CustomValidator ID="cvCompare" runat="server" ClientValidationFunction="CompareDates"
                              Display="Dynamic" ErrorMessage="*">From Date must be less then ToDate</asp:CustomValidator></td>
                    </tr>
                  <tr>
                      <td align="right" class="heading" >
                          Patient Name:
                      </td>
                      <td align="left" >
                          <asp:TextBox ID="tbPatientName" runat="server"></asp:TextBox></td>
                     </tr>
                  <tr>
                      <td align="right" class="heading" >
                          Patient Id:</td>
                      <td align="left" >
                          <asp:TextBox ID="tbPatientId" runat="server"></asp:TextBox></td>
                      
                  </tr>
                  <tr>
                      <td style="height: 26px" align="right" class="heading">
                          Study Instance:</td>
                      <td style="height: 26px" align="left">
                          <asp:TextBox ID="tbStudyInstance" runat="server"></asp:TextBox></td>
                  
                  </tr>
                  <tr>
                      <td class="heading" align="right">
                          User Name:</td>
                      <td align="left">
                          <asp:TextBox ID="tbUserName" runat="server"></asp:TextBox></td>
                      
                  </tr>
                  <tr>
                      <td align="right" class="heading">
                          Login Name:</td>
                      <td align="left">
                          <asp:TextBox ID="tbLoginName" runat="server"></asp:TextBox></td>
                      
                  </tr>
                  <tr>
                      <td align="right" class="heading">
                          Log options:</td>
                      <td align="left">
                          <asp:DropDownList ID="ddlLogOptions" runat="server" Width="156px">
                              <asp:ListItem></asp:ListItem>
                              <asp:ListItem>Login</asp:ListItem>
                              <asp:ListItem>Viewed Study</asp:ListItem>
                          </asp:DropDownList></td>
                      
                  </tr>
                  <tr>
                      <td align="left" class="heading" style="height: 26px">
                      </td>
                      <td style="text-align: left; height: 26px;">
                          <asp:Button ID="btnSearch" runat="server" CssClass="buttonStyle" OnClick="btnSearch_Click"
                              Text="Search" /></td>
                     
                  </tr>
              </table>
              </td>
             
          </tr>
          <tr>
              <td class="heading" colspan="3">
                  <asp:GridView ID="gvResult" runat="server" AllowPaging="True" AllowSorting="True"
                      AutoGenerateColumns="False" CellPadding="4" DataKeyNames="LogId"
                      ForeColor="#333333" GridLines="None" PageSize="5" OnPageIndexChanging="gvResult_PageIndexChanging" DataSourceID="ObjectDataSource1">
                      <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                      <Columns>
                          <asp:BoundField DataField="LogId" HeaderText="LogId" SortExpression="LogId" />
                          <asp:BoundField DataField="ActionTime" HeaderText="ActionTime" SortExpression="ActionTime" />
                          <asp:BoundField DataField="Action" HeaderText="Action" SortExpression="Action" />
                          <asp:BoundField DataField="loginname" HeaderText="LoginName" SortExpression="loginname" />
                          <asp:BoundField DataField="PatientName" HeaderText="PatientName" SortExpression="PatientName" />
                          <asp:BoundField DataField="StudyInstance" HeaderText="StudyInstance" SortExpression="StudyInstance" />
                          <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
                          <asp:BoundField DataField="ExternalPatientId" HeaderText="ExternalPatientId" SortExpression="ExternalPatientId" />
                          <asp:BoundField DataField="StudyId" HeaderText="StudyId" SortExpression="StudyId" />
                      </Columns>
                      <RowStyle BackColor="Silver" ForeColor="#333333" />
                      <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                      <PagerStyle BackColor="Gray" ForeColor="#333333" HorizontalAlign="Center" />
                      <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White" />
                      <AlternatingRowStyle BackColor="White" />
                  </asp:GridView>
                  &nbsp; &nbsp; &nbsp;
                  &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
              </td>
          </tr>
          <tr>
              <td class="heading">
              </td>
              <td>
                  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetPatientInfo"
                      TypeName="RIS.RISLibrary.Database.RISProcedureCaller">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="Label1" Name="whereClause"
                              PropertyName="Text" Type="String" />
                      </SelectParameters>
                  </asp:ObjectDataSource>
                  <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>&nbsp;</td>
              <td>
              </td>
          </tr>
          <tr>
              <td class="heading">
              </td>
              <td>
                  
              </td>
              <td>
              </td>
          </tr>
      </table>
    &nbsp;

</div>
</asp:Content>

