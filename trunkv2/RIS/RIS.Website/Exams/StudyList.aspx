<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="StudyList.aspx.cs" Inherits="Radiologist_StudyList" Title="DataMed | Radiology Information System | Exams List" %>
<asp:Content ID="conten2" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    
    <script type="text/javascript" src="../Includes/jquery/jquery-1.4.min.js"></script>
    <script type="text/javascript" src="../Includes/jquery/jquery-ui-1.8.1.custom.min.js"></script>
    <script type="text/javascript" src="../Includes/jquery/animatedcollapse.js"></script>
	<link type="text/css" href="../Includes/jquery/themes/base/jquery.ui.all.css" rel="stylesheet" /> 
	<link type="text/css" href="../Includes/jquery/demos/demos.css" rel="stylesheet" /> 

    <link href="../Common/jHtmlArea.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../Includes/digiply/Findings.js"></script>    
    <script language="javascript" type="text/javascript" src="../Includes/digiply/EditStudy.js"></script>
    <script type="text/javascript">
        animatedcollapse.addDiv('container', 'fade=1,hide=0');
        animatedcollapse.init();
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript" language="JavaScript" src="../Common/ieSpell.js"></script>

    
      <script language="javascript" type="text/javascript">
        function applyAllFilter(dayCount)
        {
            clearForm();
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = dayCount;
            aspnetForm.submit();
        }
        function applyNewFilter(dayCount)
        {
            clearForm();
            document.getElementById("ctl00_ContentPlaceHolder1_ddlStatus").value = 1;
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = dayCount;
            aspnetForm.submit();
        }
        function applyUserFilter(statusType)
        {
            clearForm();
            document.getElementById("ctl00_ContentPlaceHolder1_ddlStatus").value = statusType;
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = 30;
            document.getElementById("ctl00_ContentPlaceHolder1_tbRadiologist").value = document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserName").value;
            aspnetForm.submit();
        }
        //\\\\\\\\\\\\\\\my code
        function getCustomDate()
        {
         var fromDateYear=document.getElementById("ct100_ContentPlaceHolder1_dp1");
         alert(fromDateYear);
         var url =document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value;
         url+="SetDateFilter.asmx/SetCustomFilter";
         var data="FromDate=1&ToDate=2";
         YAHOO.util.Connect.asyncRequest("Post",url,customDateFilter,data);
         customDateDialog.hide();
        } 

        function onPageLinkClick(currPage)
        {
            aspnetForm.ctl00_ContentPlaceHolder1_doLog.value = "1";
            //currPage=-1 For First
            //currPage=-2 For Previous
            //currPage=-3 For Next
            //currPage=-4 For Last
            if(currPage=="-1" || currPage=="-2" || currPage=="-3" || currPage=="-4")
            {
                aspnetForm.ctl00_ContentPlaceHolder1_Navigation.value=currPage;
                if(currPage=="-1")
                    aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value = 1;// parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value) + 10 ;    
                else if(currPage=="-2")
                    aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value =  parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value) - 10 ;
                else if(currPage=="-3")
                    aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value =  parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value) + 10 ;
                else if(currPage=="-4")
                {
                    var Q=parseInt(parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intTotalPages.value)/10);
                    var Fraction=parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intTotalPages.value)-Q*10
                    //alert(Q); alert(Fraction);
                   aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value= parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intTotalPages.value) - Fraction+1 ; 
                    //alert(aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value);
                    aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value =  parseInt(aspnetForm.ctl00_ContentPlaceHolder1_intTotalPages.value); 
                    //alert(aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value);
                    aspnetForm.submit();
                    return;
                }
                  
                aspnetForm.ctl00_ContentPlaceHolder1_intStartPage.value= parseInt(aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value) ;
                aspnetForm.submit();
            }
            else
            {
                aspnetForm.ctl00_ContentPlaceHolder1_currentPage.value = currPage;
                aspnetForm.submit();
            }             
        }

        function onSortClick(sortBy)
        {
            var sortByObj = document.getElementById("ctl00_ContentPlaceHolder1_sortBy");
            var isAscObj = document.getElementById("ctl00_ContentPlaceHolder1_isAsc");
            var currentSortBy = sortByObj.value;
            var currentIsAsc = isAscObj.value;
            if(currentSortBy == sortBy)
            {
                if(currentIsAsc == "1") isAscObj.value = "0";
                else if (currentIsAsc == "0") isAscObj.value = "1";
                else isAscObj.value = "1";
            }
            else
            {
                isAscObj.value = "1";
            }
            sortByObj.value = sortBy;
            aspnetForm.submit();
        }
        function onClearForm()
        {
            //test();
            clearForm();
            
            onPageLinkClick(-1);
            
            //aspnetForm.submit();
        }
        function clearForm()
        {
            document.getElementById("ctl00_ContentPlaceHolder1_tbName").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_tbPatientId").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_ddlStatus").selectedIndex = 0;
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = "30";
            document.getElementById("ctl00_ContentPlaceHolder1_ddlModality").selectedIndex = 0;
            document.getElementById("ctl00_ContentPlaceHolder1_tbProcedure").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_tbRadiologist").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_tbPhysician").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_sortBy").value = "4";
            document.getElementById("ctl00_ContentPlaceHolder1_isAsc").value = "0";        
            document.getElementById("ctl00_ContentPlaceHolder1_currentPage").value = "1";        
        }
        function renderImages()
        {
            var sortBy = document.getElementById("ctl00_ContentPlaceHolder1_sortBy").value;
            if(sortBy != "0")
            {
                var imgObj = document.getElementById("ctl00_ContentPlaceHolder1_<%=((loggedInUserRoleId == RIS.RISLibrary.Utilities.Constants.Roles.ClientAdmin)?"SU":"")%>Image" + sortBy);
                var tableCellObj = document.getElementById("ctl00_ContentPlaceHolder1_<%=((loggedInUserRoleId == RIS.RISLibrary.Utilities.Constants.Roles.ClientAdmin)?"SU":"")%>HTableCell" + sortBy);
                if(imgObj != null && tableCellObj != null)
                {
                    tableCellObj.className = "selectedHeadingCell";
                    if(document.getElementById("ctl00_ContentPlaceHolder1_isAsc").value == "0")
                    {
                        imgObj.src="../Images/sort_desc.png"
                    }
                    imgObj.style.display = "inline";
                }
            }
        }
        function searchPatient(externalPatientId)
        {
            clearForm();
            
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = "-1";
            document.getElementById("ctl00_ContentPlaceHolder1_hfPatientId").value = externalPatientId;
            document.getElementById("hfSearchArea").value = "1";                
            //alert(externalPatientId);
            //patient id is when user enters patient id himself, that is searched using like operation. hidden is used when + is pressed as there we need =
            //although we are setting value here to show to the user, but at the back end it first checks for hidden value then for this value if the prev is not present
            document.getElementById("ctl00_ContentPlaceHolder1_tbPatientId").value = externalPatientId;
            aspnetForm.submit();
        }
        function resetFilter()
        {
            clearForm();
            aspnetForm.submit();
        }
        function onSearchClick()
        {
            document.getElementById("ctl00_ContentPlaceHolder1_currentPage").value = "1";
            
            aspnetForm.submit();
        }
        function toggleSearchBar()
        {
            if(document.getElementById("hfSearchArea").value == "0")
            {
                document.getElementById("hfSearchArea").value = "1";                
            }
            else
            {
                document.getElementById("hfSearchArea").value = "0";                
            }            
            showSearchBar();
        }
        function showSearchBar()
        {
            if(document.getElementById("hfSearchArea").value == "0")
            {
                animatedcollapse.hide('container');
                document.getElementById("imgSearch").src = "../Images/Search_Show.jpg";
            }
            else
            {
                animatedcollapse.show('container');            
                document.getElementById("imgSearch").src = "../Images/Search_Hide.jpg";  
            }                    
        }
        function selectAllClicked(chkBoxObj)        
        {
            var chkBoxList = document.getElementsByName("releaseToRad");
            for(var i=0;i<chkBoxList.length;i++)
            {
                chkBoxList[i].checked = chkBoxObj.checked;
            }
        }
        function releaseToRadsClick()
        {
            
            document.getElementById("hfAction").value = "release";
            aspnetForm.submit();
        }
    </script>
    <div id="eFilmDiv" class="errorText" style="text-align:left;"></div>
    <asp:HiddenField ID="hfSURL" runat="server" />
    <asp:HiddenField ID="hfWebServicesHomeURL" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserId" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserName" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserRoleId" runat="server" /><asp:HiddenField ID="hfFromDate" runat="server" />
    <asp:HiddenField ID="hfToDate" runat="server" />
    <input id="hfAction" name="hfAction" type="hidden" />
    <input id="hfSearchArea" name="hfSearchArea" type="hidden" value='<%=(Request["hfSearchArea"] == null)?"1":Request["hfSearchArea"]%>'/>
    
    <div id="editStudyDiv" title="Add notes &amp; information">
        <iframe id="editStudyFrm" frameborder="0" src="" width=600px" height="510px"></iframe>     
    </div>
        
    <div id="findingDialogDiv" title="Report">           

            <iframe id="findingFrame" frameborder="0"  width=990px" height="560px"></iframe>

    </div>    
    <div id="reviseExamDiv" title="Revise Exam">
        <iframe id="reviseExamFrame" frameborder="0"  width=600px" height="60px"></iframe>
    </div>
    <div id="rejectExamDiv" title="Report">                

            <iframe id="rejectExamFrame" frameborder="0"  width=550px" height="300px"></iframe>

    </div>  
    
<%--   <div id="customDateDialogeDiv">
   <div class="bd">  
   
   <table height="100%" width="100%"><tr>
       <td align="right" class="heading" style="width: 858px; height: 6px">
       From Date:</td>
       <td style="width: 1121px; height: 6px;" align="left"> 
       </td>
       <td align="left" style="width: 785px; height: 6px; text-align: right">
           <strong><span style="font-size: 8pt">
           To Date: </span></strong>
       </td>
       <td align="left" style="width: 1444px; height: 6px;">
           <strong><span style="font-size: 8pt"></span></strong>
       </td>
       <td align="left" style="width: 1016px; height: 6px">
       </td>
   </tr>
       <tr>
           <td align="right" class="heading" style="width: 858px">
           </td>
           <td style="width: 1121px" align="left">
<p>
    &nbsp;</p></td>
           <td align="left" style="width: 785px">
           </td>
           <td align="left" style="width: 1444px">
           </td>
           <td align="left" style="width: 1016px">
           </td>
       </tr>
     <tr>
         <td style="width: 858px">
         </td>
         <td style="width: 1121px; text-align: center;" align="left">
             &nbsp;
             <input type="submit" onclick="getCustomDate();" style="width: 90px; height: 25px" id="btnOkCustomeDate" value="Ok"/></td>
         <td align="left" style="width: 785px">
         </td>
         <td align="left" style="width: 1444px">
             <input id="btnCancel" style="width: 90px; height: 25px" type="button" value="Cancel" onclick="customeDateDialogCancel();" /></td>
         <td align="left" style="width: 1016px">
         </td>
     </tr></table>
    </div>
   </div>--%>
   <%-- <div id="statusDiv">
   <div class="bd">  
   <table><tr><td><asp:CheckBoxList ID="cblStatus" runat="server" Height="24px"></asp:CheckBoxList></td></tr>
   </table>
   </div>
   </div>
   <div id="modalityDiv">
   <div class="bd">  
   <table><tr><td style="width: 95px"><asp:CheckBoxList ID="cblModality" runat="server">
              </asp:CheckBoxList></td></tr>
   </table>
   </div>
   </div> --%>
    <table style="width:100%;" cellpadding="0" cellspacing="0" border="0">
        <tr style="cursor:pointer" onclick="toggleSearchBar();"> 
            <td align="left"><img src="../Images/Search_Show.jpg" alt="Search" id="imgSearch"/></td>
        </tr>
        <tr >
            <td align="left">
                <div id="container" style="margin-top:10px">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="searchLabel" align="right">Patient Name:&nbsp;</td>
                            <td><asp:TextBox ID="tbName" runat="server" Width="200px" CssClass="searchField"></asp:TextBox></td>
                            <td class="searchLabel" align="right">Patient Id:&nbsp;</td>
                            <td><asp:TextBox ID="tbPatientId" runat="server" Width="116px"  CssClass="searchField"></asp:TextBox></td>
                            <td class="searchLabel" align="right">Physician:&nbsp;</td>
                            <td><asp:TextBox ID="tbPhysician" runat="server" Width="65px" Height="15px"  CssClass="searchField"></asp:TextBox></td>
                            <td class="searchLabel" align="right">Radiologist:&nbsp;</td>
                            <td><asp:TextBox ID="tbRadiologist" runat="server" Width="150px"  CssClass="searchField"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="searchLabel" align="right">Exam Status:&nbsp;</td>
                            <td><asp:DropDownList ID="ddlStatus" runat="server" Width="120px" Height="21px"  
                                    CssClass="searchField" ondatabound="ddlStatus_DataBound"></asp:DropDownList></td>
                            <td class="searchLabel" align="right">Exam Date:&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlExamDate" runat="server" Width="120px"  CssClass="searchField">
                                    <asp:ListItem Value="-1" Text="All Days"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Today"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Yesterday"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Last 3 Days"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Last 7 Days"></asp:ListItem>
                                    <asp:ListItem Value="14" Text="Last 14 Days"></asp:ListItem>
                                    <asp:ListItem Value="30"  Selected="True" Text="Last 30 Days"></asp:ListItem>
                                    <%--<asp:ListItem Value="custom" Text="Custom"></asp:ListItem>--%>
                                </asp:DropDownList>                            
                            </td>
                            <td class="searchLabel" align="right">Modality:&nbsp;</td>
                            <td><asp:DropDownList ID="ddlModality" runat="server" Width="70px"  
                                    CssClass="searchField" ondatabound="ddlModality_DataBound"></asp:DropDownList></td>
                            <td class="searchLabel" align="right">Procedure:&nbsp;</td>
                            <td><asp:TextBox ID="tbProcedure" runat="server" Width="150px"  CssClass="searchField"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="searchLabel" align="right">
                                <asp:Label ID="lblClient" runat="server" Text="Client:&nbsp;" Visible="false" CssClass="searchLabel"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlClient" runat="server" 
                                    ondatabound="ddlClient_DataBound" Visible="false"  CssClass="searchField">
                                </asp:DropDownList>
                            </td>
                            <td colspan="6" align="right">
                                <img id="refreshBtn" src="../Images/Reset_Button.jpg" class="linkImage" onclick="onClearForm();" alt="Reset"/>
                                <img id="searBtn" src="../Images/Search_Button.jpg" class="linkImage" onclick="onSearchClick();" alt="Search" style="margin-right:25px;"/>                                
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
       
        </tr>
        <tr>
        <%-- <td class="filterArea" >
        <table style="border-color:Black;border-width:thin" cellpadding="3" cellspacing="0">
            <tr><td class="filterText" onclick="resetFilter();">RESET WORKLIST</td></tr>
            <tr style="height:15px;"><td></td></tr>
            <tr><td class="filterTextHeading">Show all exams for:</td></tr>
            <tr><td class="filterText" onclick="applyAllFilter(0);">Today (<span class="filterTextSpan" id="spanAll0">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyAllFilter(1);">Yesterday (<span class="filterTextSpan" id="spanAll1">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyAllFilter(3);">Last 3 days (<span class="filterTextSpan" id="spanAll3">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyAllFilter(7);">Last 7 days (<span class="filterTextSpan" id="spanAll7">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyAllFilter(30);">Last 30 days (<span class="filterTextSpan" id="spanAll30">...</span>)</td></tr>
            <tr style="height:15px;"><td>&nbsp;</td></tr>
            <tr><td class="filterTextHeading">Show new exams for:</td></tr>
            <tr><td class="filterText" onclick="applyNewFilter(0);">Today (<span class="filterTextSpan" id="spanNew0">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyNewFilter(1);">Yesterday (<span class="filterTextSpan" id="spanNew1">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyNewFilter(3);">Last 3 days (<span class="filterTextSpan" id="spanNew3">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyNewFilter(7);">Last 7 days (<span class="filterTextSpan" id="spanNew7">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyNewFilter(30);">Last 30 days (<span class="filterTextSpan" id="spanNew30">...</span>)</td></tr>
            <tr style="height:15px;"><td>&nbsp;</td></tr>
            <tr><td class="filterTextHeading">Show exams I dictated in last 30 days:</td></tr>
            <tr><td class="filterText" onclick="applyUserFilter(0);">All (<span class="filterTextSpan" id="spanUser0">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyUserFilter(5);">Verified (<span class="filterTextSpan" id="spanUser5">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyUserFilter(4);">Pending Verification (<span class="filterTextSpan" id="spanUser4">...</span>)</td></tr>
            <tr><td class="filterText" onclick="applyUserFilter(7);">Re-dictated (<span class="filterTextSpan" id="spanUser7">...</span>)</td></tr>
        </table>
        </td>--%>
        <td>
        <div>
            <asp:HiddenField ID="currentPage" runat="server" Value="1" />
            <asp:HiddenField ID="intTotalPages" runat="server" />
            <asp:HiddenField ID="intStartPage" runat="server" Value="1" />
            <asp:HiddenField ID="sortBy" runat="server" Value="4"/>
            <asp:HiddenField ID="isAsc" runat="server" Value="0"/>
            <asp:HiddenField ID="doLog" runat="server" Value="0" />
            <asp:HiddenField ID="hfPatientId" runat="server" /><asp:HiddenField ID="hfCarryStatus" runat="server" Value="x" /><asp:HiddenField ID="hfCarryModality" runat="server" Value="x" />
            <div style="text-align:left">
            <%if (loggedInUserRoleId == RIS.RISLibrary.Utilities.Constants.Roles.ClientAdmin
                  || loggedInUserRoleId == RIS.RISLibrary.Utilities.Constants.Roles.ClientTechnologist)
              { %>
            <input type="button" value="Release to Radiologist(s)" onclick="releaseToRadsClick();" />
            <%} %>
            </div>
            <asp:Table ID="Table1" runat="server" CssClass="dataTable" CellPadding="0" CellSpacing="0" Width="100%" style="margin-top:3px;" Visible="false">
 
            
                <asp:TableRow ID="TableRow1" runat="server" Width="100%">
                    <asp:TableCell ID="TableCell1" runat="server" Width="24px" CssClass="headingCell" >&nbsp;</asp:TableCell>
                    <asp:TableCell id="HTableCell1" runat="server" Width="188px" CssClass="headingCell" onclick="onSortClick('1');">                        
                        Patient&nbsp;<img id="Image1" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell2" runat="server" Width="120px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('2');">ID&nbsp;<img id="Image2" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell3" runat="server" Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('3');">Status&nbsp;<img  id="Image3" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell4" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('4');">Exam Date&nbsp;<img  id="Image4" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell5" runat="server" Width="70px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('5');">Modality&nbsp;<img  id="Image5" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell6" runat="server" Width="130px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('6');">Procedure&nbsp;<img id="Image6" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell7" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('7');">Radiologist&nbsp;<img id="Image7" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="TableCell9" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('8');">Physician&nbsp;<img id="Image8" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell ID="TableCell4" runat="server" Width="108px" CssClass="headingCellNoLeftNoRight" ColumnSpan="3">Clickable Icons</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <asp:Table ID="TableSU" runat="server" CssClass="dataTable" CellPadding="0" CellSpacing="0" Width="100%" style="margin-top:3px;" Visible="false">
 
            
                <asp:TableRow ID="TableRow2" runat="server" Width="100%">
                    <asp:TableCell ID="TableCell5" runat="server" Width="20px" CssClass="headingCellNoRight" ><input type='checkbox' onclick='selectAllClicked(this)'/></asp:TableCell>
                    <asp:TableCell id="TableCell6" runat="server" Width="24px" CssClass="headingCell"></asp:TableCell>
                    <asp:TableCell id="SUHTableCell1" runat="server" Width="178px" CssClass="headingCell" onclick="onSortClick('1');">                        
                        Patient&nbsp;<img id="SUImage1" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHHTableCell2" runat="server" Width="110px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('2');">ID&nbsp;<img id="SUImage2" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHHTableCell3" runat="server" Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('3');">Status&nbsp;<img  id="SUImage3" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHTableCell4" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('4');">Exam Date&nbsp;<img  id="SUImage4" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHTableCell5" runat="server" Width="70px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('5');">Modality&nbsp;<img  id="SUImage5" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHTableCell6" runat="server" Width="130px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('6');">Procedure&nbsp;<img id="SUImage6" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="SUHTableCell7" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('7');">Radiologist&nbsp;<img id="SUImage7" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="TableCell7" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('8');">Physician&nbsp;<img id="SUImage8" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>                    
                    <asp:TableCell ID="TableCell14" runat="server" Width="108px" CssClass="headingCellNoLeftNoRight" ColumnSpan="3">Clickable Icons</asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
            <script language="javascript" type="text/javascript">    
                renderImages();
            </script><asp:HiddenField ID="Navigation" runat="server" Value="0"/>        
        </div>
        </td>
        </tr>
    </table>
</asp:Content>

