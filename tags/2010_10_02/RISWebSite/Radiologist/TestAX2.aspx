<%@ Page Language="C#" MasterPageFile="~/Common/Main.master" AutoEventWireup="true" CodeFile="TestAX2.aspx.cs" Inherits="Radiologist_TestAX2" Title="Untitled Page" %>
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
        function onSearchClick()
        {
            aspnetForm.submit();
        }
    </script>
    <object id="eFilmControl" width="0px" height="0px"
          classid="CLSID:023E9FAE-9641-49B6-95A0-24F19E43698D" codebase="EFlimActiveX.CAB">  
        <param name="_Version" value="65536" />
        <param name="_ExtentX" value="2646" />
        <param name="_ExtentY" value="1323" />
        <param name="_StockProps" value="0" />
    </object>
    <asp:Table ID="Table1" runat="server">
    </asp:Table>
    <input type="hidden" value="9089" name="eFilmPatId" id="eFilmPatId" />
    <input type="hidden" value="2830" name="eFilmANo" id="eFilmANo" />
</asp:Content>

