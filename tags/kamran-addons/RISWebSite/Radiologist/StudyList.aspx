<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="StudyList.aspx.cs" Inherits="Radiologist_StudyList" Title="DataMed | Radiology Information System | Study List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript" src="../Includes/digiply/Findings.js"></script>
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
                var imgObj = document.getElementById("ctl00_ContentPlaceHolder1_Image" + sortBy);
                var tableCellObj = document.getElementById("ctl00_ContentPlaceHolder1_HTableCell" + sortBy);
                tableCellObj.className = "selectedHeadingCell";
                if(document.getElementById("ctl00_ContentPlaceHolder1_isAsc").value == "0")
                {
                    imgObj.src="../Images/sort_desc.png"
                }
                imgObj.style.display = "inline";
            }
        }
        function searchPatient(externalPatientId)
        {
            clearForm();
            document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value = "-1";
            document.getElementById("ctl00_ContentPlaceHolder1_hfPatientId").value = externalPatientId;
            aspnetForm.submit();
        }
        function resetFilter()
        {
            clearForm();
            aspnetForm.submit();
        }
        
    </script>
    <asp:HiddenField ID="hfSURL" runat="server" />
    <asp:HiddenField ID="hfWebServicesHomeURL" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserId" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserName" runat="server" />
    <asp:HiddenField ID="hfLoggedInUserRoleId" runat="server" /><asp:HiddenField ID="hfFromDate" runat="server" />
    <asp:HiddenField ID="hfToDate" runat="server" />
    <div id="findingDialogDiv">        
        <div class="bd">
            <div id="header">
                <table width="100%" border="0">
                    <tr>                    
                        <td style="height: 24px" align="center" ><div id="voiceControlDiv"></div></td>
                       <%-- <td align="right" valign="top" style="height: 24px"><input type="button" value="Close | X" class="buttonStyle"  onclick="closeWindow();" /></td>--%>
                    </tr>
                </table>
            </div>
             <div id="reportDiv" class="yui-navset" style="width:100%">
               <ul id="tabButtons" class="yui-nav" visible="false">
                    <li><a href="#tabStudy"><em>Study</em></a></li><%if (loggedInUserRoleId == 2)%><%{%><li><a href="#tabReport"><em>Report</em></a></li><li class="selected"><a href="#tabImages"><em>Images</em></a></li><%}%><%else%><%{%><li class="selected"><a href="#tabReport"><em>Report</em></a></li><%}%></ul>                    
                    <div class="yui-content">
                    <div id="tabStudy">
                        <table width="100%" border="0" style="height:250px" >
                            <tr>
                                <td align="right" style="width:15%" class="heading"></td>
                                <td align="left" style="width:20%" ><span id="lblPatientName"/></td>
                            </tr>
                            <tr>
                                <td align="right" style="width:15%" class="heading">ID:</td>
                                <td align="left" style="width:20%"><span id="lblPatientId" /></td>                
                            </tr>
                            <tr>        
                                <td align="right"  class="heading"> Status:</td>
                                <td align="left"><span id="lblStatus" /></td>
                            </tr>
                            <tr>
                                <td align="right"  class="heading" style="height: 8px"> Exam Date:</td>
                                <td align="left" style="height: 8px"><span id="lblExamDate" /></td>                    
                            </tr>
                            <tr>
                                <td align="right"  class="heading"> Modality:</td>
                                <td align="left"><span id="lblModality" /></td>                                
                            </tr>
                            <tr>
                                <td align="right"  class="heading"> Procedure:</td>
                                <td align="left"><span id="lblProcedure"/></td>
                            </tr>
                            <tr>
                                <td align="right"  class="heading"> Radiologist:</td>
                                <td align="left"><span id="lblRadiologist" /></td>                    
                            </tr>
                             <tr>
                                <td align="right"  class="heading"> Physician:</td>
                                <td align="left"><span id="lblPhysician" /></td>
                            </tr>                                       
                        </table>                              
                    </div>
                    <div id='tabReport'>					  
                      <table width="100%" border="0" style="height:250px;">
		                <tr>
			                <td colspan="6" style="width: 736px">							
				                <input type="hidden" name="currentIndex" id="currentIndex" value=""/>
				                <span class="error" id="errorMessage" />
				                <span class="saveFinding" id="savedMessage" />
			                </td>
		                </tr>
		                    <tr><td class="heading" align="left" style="width: 25px; height: 1px;">
                                <asp:Label ID="lblTemplate" runat="server" Text="ReportTemplate:"></asp:Label></td>
		                        <td align="left" style="width: 10px; height: 1px;"><asp:DropDownList ID="ddlTemplates" runat="server" AppendDataBoundItems="True">
                                <asp:ListItem Value="0">-- Select a Template --</asp:ListItem>
                                </asp:DropDownList>&nbsp;</td>
                                <td align="left" class="heading" style="width: 588px; height: 1px;">
                                <input id="btnLoadTemplate" type="button" value="Apply Template" onclick="getReportText()"/></td>
                                <td align="left" class="heading" style="width: 290px; height: 1px;">
                                </td>
                                <td align="left" class="heading" style="width: 21px; height: 1px;">
                                </td>
                                <td align="left" class="heading" style="width: 5px; height: 1px;">
                                </td>
                            </tr>
                        <tr>
                            <td align="left" colspan="6" style="width: 736px">
                                <div id="tranTextDiv"></div>                
                            </td>            
                        </tr>					
		                </table>
                    </div>
                    <%if(loggedInUserRoleId==2)%>
                    <%{%>                                                
                    <div id="tabImages" style="height:490px"></div>                      
                    <%}%>
                </div>			
	        </div>
            <div id="buttonsDiv" style="text-align:center"></div>  
       </div>
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
   <div id="statusDiv">
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
   </div>
    <table style="width:100%;" cellpadding="0" cellspacing="0" border="0">
        <tr>
        <td class="filterArea" >
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
        </td>
        <td style="width:770px;vertical-align:top;text-align:left">
        <div>
            <asp:HiddenField ID="currentPage" runat="server" Value="1" />
            <asp:HiddenField ID="intTotalPages" runat="server" />
            <asp:HiddenField ID="intStartPage" runat="server" Value="1" />
            <asp:HiddenField ID="sortBy" runat="server" Value="4"/>
            <asp:HiddenField ID="isAsc" runat="server" Value="0"/>
            <asp:HiddenField ID="doLog" runat="server" Value="0" />
            <asp:HiddenField ID="hfPatientId" runat="server" /><asp:HiddenField ID="hfCarryStatus" runat="server" Value="x" /><asp:HiddenField ID="hfCarryModality" runat="server" Value="x" />
            <asp:Table ID="Table1" runat="server" CssClass="dataTable" CellPadding="0" CellSpacing="0" Width="820px">
                <asp:TableRow ID="TableRow1" runat="server" >
                    <asp:TableCell ID="TableCell1" runat="server" Width="25px" CssClass="headingCell">&nbsp;</asp:TableCell>
                    <asp:TableCell id="HTableCell1" runat="server" Width="130px" CssClass="headingCell" onclick="onSortClick('1');">Patient&nbsp;<img id="Image1" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell2" runat="server" Width="50px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('2');">ID&nbsp;<img id="Image2" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell3" runat="server" Width="120px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('3');">Status&nbsp;<img  id="Image3" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell4" runat="server" Width="90px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('4');">Exam Date&nbsp;<img  id="Image4" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell5" runat="server" Width="70px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('5');">Modality&nbsp;<img  id="Image5" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell6" runat="server" Width="160px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('6');">Procedure&nbsp;<img id="Image6" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell7" runat="server" Width="100px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('7');">Radiologist&nbsp;<img id="Image7" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>
                    <asp:TableCell id="HTableCell8" runat="server" Width="70px" VerticalAlign="Middle" CssClass="headingCell" onclick="onSortClick('8');">Physician&nbsp;<img id="Image8" runat="server" src="../Images/sort_asc.png" alt="Ascending" style="display:none"/></asp:TableCell>            
                    <asp:TableCell ID="TableCell2" runat="server" Width="25px" CssClass="headingCell">&nbsp;</asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="TableRow2" runat="server" >
                    <asp:TableCell ID="TableCell9" runat="server" CssClass="firstSearchCell"><img id="refreshBtn" src="../Images/refresh.gif" class="linkImage" onclick="onClearForm();" alt="Reset"/>
                        </asp:TableCell>
                    <asp:TableCell ID="TableCell3" runat="server" CssClass="searchCell">
                        <asp:TextBox ID="tbName" runat="server" Width="125px" Height="15px" CssClass="textBoxStyle"></asp:TextBox></asp:TableCell>
                    <asp:TableCell ID="TableCell111" runat="server" CssClass="searchCell">
                        <asp:TextBox ID="tbPatientId" runat="server" Width="45px" Height="15px" CssClass="textBoxStyle"></asp:TextBox></asp:TableCell>
                    <asp:TableCell ID="TableCell4" runat="server" CssClass="searchCell" VerticalAlign="Middle" HorizontalAlign="Left">
                        <asp:TextBox ID="statusText" runat="server" Width="90px" Height="15px" CssClass="textBoxStyle"></asp:TextBox>
                        <input type="button" style="height:20px; width:20px; margin-left:-1" value="v" onclick="showStatusDialog();"/>
<%--                        <asp:DropDownList ID="ddlStatus" runat="server" Width="120px" Height="21px" CssClass="dropDownListStyle">
                        </asp:DropDownList>--%></asp:TableCell>
                    <asp:TableCell ID="TableCell5" runat="server" CssClass="searchCell">
                    <asp:DropDownList ID="ddlExamDate" runat="server" Width="90px" Height="21px" CssClass="dropDownListStyle">
                            <asp:ListItem Value="-1" Text="All Days"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Today"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Yesterday"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Last 3 Days"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Last 7 Days"></asp:ListItem>
                            <asp:ListItem Value="14" Text="Last 14 Days"></asp:ListItem>
                            <asp:ListItem Value="30"  Selected="True" Text="Last 30 Days"></asp:ListItem>
                            <%--<asp:ListItem Value="custom" Text="Custom"></asp:ListItem>--%>
                        </asp:DropDownList>
<%--                    <select  id="examDateList" onchange="setValue();" style="height:20px; visibility=hidden; width:90px; font-size:8pt; font-family:Arial">
                        <option value="-1">All Days</option>
                        <option value="0">Today</option>
                        <option value="1">Yesterday</option>
                        <option value="3">Last 3 Days</option>
                        <option value="3">Last 3 Days</option>
                        <option value="7">Last 7 Days</option>
                        <option value="14">Last 14 Days</option>
                        <option value="30" selected="selected">Last 30 Days</option>
                        <option value="custom">Custom</option>
                        </select>--%></asp:TableCell>
                    <asp:TableCell ID="TableCell6" runat="server" CssClass="searchCell">
                        <asp:TextBox ID="modalityText" runat="server" Width="40px" Height="15px" CssClass="textBoxStyle"></asp:TextBox>
                        <input type="button" style="height:20px; width:20px; margin-left:-1" value="v" onclick="showModalityDialog();"/>
<%--                    <asp:DropDownList ID="ddlModality" runat="server" Width="70px" Height="21px" CssClass="dropDownListStyle">
                        </asp:DropDownList>--%></asp:TableCell>
                    <asp:TableCell ID="TableCell7" runat="server" CssClass="searchCell"><asp:TextBox ID="tbProcedure" runat="server" Width="155px" Height="15px" CssClass="textBoxStyle"></asp:TextBox></asp:TableCell>
                    <asp:TableCell ID="TableCell8" runat="server" CssClass="searchCell"><asp:TextBox ID="tbRadiologist" runat="server" Width="95px" Height="15px" CssClass="textBoxStyle"></asp:TextBox></asp:TableCell>
                    <asp:TableCell ID="TableCell10" runat="server" CssClass="searchCell"><asp:TextBox ID="tbPhysician" runat="server" Width="65px" Height="15px" CssClass="textBoxStyle"></asp:TextBox></asp:TableCell>            
                    <asp:TableCell ID="TableCell11" runat="server" CssClass="lastSearchCell"><asp:ImageButton ID="btnSearch"
                            runat="server" ImageUrl="~/Images/search.gif" AlternateText="Search"/></asp:TableCell>                                
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

