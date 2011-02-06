// JScript File
function onSpellCheckClick()
{	
	var node = document.getElementById("findingTextArea");
	if(node != null)
	    checknodespelling(node); 
}
var loadingDialog = null;
var dialogDiv = null;
var findingDialog = null;
var studyList = new Array();
var sURL = null;
var customDateDialog=null;
var statusDialog=null;
var modalityDialog=null;
var loadingVoiceControl = false;
var loadingFinding = false;
var saveFindingTimer = null;
/*var callback = {
    success : function(o) {
        if(o.responseXML.documentElement.firstChild != null)
        {
           document.getElementById('findingTextArea').value = o.responseXML.documentElement.firstChild.nodeValue;                                
           $("#findingTextArea").htmlarea("updateHtmlArea");
        }
        loadingFinding = false;
        hideLoading();
    },
    failure : function(o) {
        loadingFinding = false;
        hideLoading();
    }
}*/

var saveFindingCallback = {
    success : function(o) {
        //put check here                
        var currentIndex = parseInt(document.getElementById("currentIndex").value);
        if(currentIndex >= 0)
        {
            var currentTime = new Date();
            document.getElementById("savedMessage").innerText = "Finding was last saved at " + getTimeString(currentTime.getHours()) + ":" + getTimeString(currentTime.getMinutes()) + ":" + getTimeString(currentTime.getSeconds());
            if(o.responseXML.documentElement.firstChild != null)
            {                
                studyList[currentIndex].FindingId = parseInt(o.responseXML.documentElement.firstChild.nodeValue);            
            }
            //saveFindingTimer = setTimeout("saveFinding()",30000);
        }
    },
    failure : function(o) {
        if(document.getElementById("currentIndex").value != "-1");
            //saveFindingTimer = setTimeout("saveFinding()",30000);
    }
}

var approveFindingCallback = {
    success : function(o) {
        loadingDialog.hide();
        closeWindow();
    },
    failure : function(o) {
        loadingDialog.hide();
        closeWindow();
    }
}

var allCountCallback = {
    success : function(o) {
        //showCounts(o,"spanAll");
        callCountURL("GetNewCounts",newCountCallback,"");
    },
    failure : function(o) {
        callCountURL("GetNewCounts",newCountCallback,"");
    }
}

function showCounts(o,spanName)
{
    if(o.responseXML.documentElement.firstChild != null)
    {
       var allCounts = o.responseXML.documentElement.firstChild.nodeValue.split(",");
       var i=0;
       for(i=0;i<allCounts.length;i++)
       {
            var keyValue = allCounts[i].split("=");
            document.getElementById(spanName+ keyValue[0]).innerText = keyValue[1]; 
       }           
    } 
}

var newCountCallback = {
    success : function(o) {
        //showCounts(o,"spanNew");
        callCountURL("GetUserCounts",userCountCallback,"loggedInUserName=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserName").value);
    },
    failure : function(o) {
        callCountURL("GetUserCounts",userCountCallback,"loggedInUserName=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserName").value);
    }
}

var userCountCallback = {
    success : function(o) {
        //showCounts(o,"spanUser");
    },
    failure : function(o) {
    }
}
//--------------------------logging Call back does nothing
var logCallback = {
    success : function(o) {
    },
    failure : function(o) {
    }
}

//--------------------------------------------------------------
var templateTextCallBack={
    success : function(o){  if(o.responseText!== undefined)
                               {
                                 document.getElementById("findingTextArea").value= o.responseXML.documentElement.firstChild.nodeValue;
                                 $("#findingTextArea").htmlarea("updateHtmlArea");
                               }
                         },
    failure : function(o){  if(o.responseText!== undefined)
                               {
                                 document.getElementById("findingTextArea").value= o.responseText;
                               }
                         }
                         }
function customeDateDialogCancel()
{
    customDateDialog.hide();
}

//----------- fill year----------------------------
//function fillYear()
// {
//   var i=0;
//   var yearObj1 = document.getElementById("ddlYearFrom");
//   yearObj1.options.add(new Option("----","0"));
//   for(i=1900;i<=<%=DateTime.Now.Year%>;i++)
//    {
//      yearObj1.options.add(new Option(""+i,""+i));            
//    }   
// }
function callCountURL(functionName,callbackName,data)
{
    var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value + "RecordCountService.asmx/" + functionName;
    data += "&loggedInUserId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value;
    data += "&loggedInUserRoleId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserRoleId").value;
    YAHOO.util.Connect.asyncRequest("POST",sURL,callbackName,data);
}
//*************************************************************
function setValue()
{
    var url =document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value;
    if (document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value != "custom")
    {
         url+="SetDateFilter.asmx/SetFilter";
         var data="SelectedValue="+document.getElementById("ctl00_ContentPlaceHolder1_ddlExamDate").value;
         YAHOO.util.Connect.asyncRequest("Post",url,dateFilter,data);
    }
    else
    {
        customDateDialog.show();     
    }
}
//function getCustomDate()
//{
//         var fromDateYear=document.getElementById("ct100_ContentPlaceHolder1_dcFromDate_ddlDay").value;
//         alert(fromDateYear);
//         var url =document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value;
//         url+="SetDateFilter.asmx/SetCustomFilter";
//         var data="FromDate=1&ToDate=2";
//         YAHOO.util.Connect.asyncRequest("Post",url,customDateFilter,data);
//         customDateDialog.hide();
//} 
var dateFilter= {   success: function(o){var x= o.responseText;}, failure: function(o){var x=o.responseText;}}
var customDateFilter={   success: function(o){customDateDialog.hide();}, failure: function(o){var x= o.responseText;}} 

function getTimeString(data)
{
    if(data < 10)
        return "0" + data;
    return "" + data;
}
function closeFindingWindow()
{
    jQuery("#findingDialogDiv").dialog('close'); 
}

function showFindingDialog(currentIndex,data)
{   

    document.getElementById("editFindingContentsDiv").src = "EditFinding.aspx?StudyId=" + studyList[currentIndex].StudyId;
    
    $("#findingDialogDiv").dialog({ title: "Report | " + studyList[currentIndex].PatientId + " | " + studyList[currentIndex].PatientName });
    jQuery("#findingDialogDiv").dialog('open'); 
    //if(studyList[currentIndex].FindingId > 0)
    /*
    document.getElementById("findingDialogDiv").style.display = "block";
    loadingDialog.show();
    document.getElementById("savedMessage").innerText = "";
    loadingVoiceControl = false;    
    loadingFinding = false;
    document.getElementById("lblPatientName").innerText = studyList[currentIndex].PatientName;
    document.getElementById("lblPatientId").innerText = studyList[currentIndex].PatientId;
    document.getElementById("lblStatus").innerText = studyList[currentIndex].Status;
    document.getElementById("lblExamDate").innerText = studyList[currentIndex].StudyTimeStamp;
    document.getElementById("lblModality").innerText = studyList[currentIndex].Modality;
    document.getElementById("lblProcedure").innerText = studyList[currentIndex].Procedure;
    document.getElementById("lblRadiologist").innerText = studyList[currentIndex].Radiologist;
    document.getElementById("lblPhysician").innerText = studyList[currentIndex].Physician;
    document.getElementById("currentIndex").value = currentIndex;
    document.getElementById("techComments").value = studyList[currentIndex].TechComments;
    var loggedInUserRoleId = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserRoleId").value);
    var loggedInUserId = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value);
    document.getElementById("tranTextDiv").innerHTML = "";
    //showViewer();
    if(studyList[currentIndex].Status != "Verified")
    {
        //showVoiceControl(studyList[currentIndex],loggedInUserId,loggedInUserRoleId);        
        
        if(loggedInUserRoleId == 2) // Rad
        {            
            
            //get the templates list
            
            //get templates only if it is not verified
            if(studyList[currentIndex].Status != "Verified")
            {
                
                if(studyList[currentIndex].RadiologistId != 0 && studyList[currentIndex].RadiologistId != loggedInUserId)
                {
                    document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='alert(\"Verification of this report is limited to dictating radiologist\");' />";
                }
                else
                {
                    document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Save & Close without Verification' class='buttonStyle' onclick='markStudy();' />";
                    document.getElementById("buttonsDiv").innerHTML += "&nbsp;&nbsp;&nbsp;&nbsp;";
                    document.getElementById("buttonsDiv").innerHTML += "<input type='button' value='Verify & Close' class='buttonStyle' onclick='approveStudy();' />";
                }
            }
        }
        else if(loggedInUserRoleId == 5) //Tran
        {
            if(studyList[currentIndex].Status != "New")
            {            
                //saveFindingTimer = setTimeout("saveFinding()",5000);
                document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Mark for Verification' class='buttonStyle' onclick='markStudy();' />";
                document.getElementById("findingHeaderTran").style.display = "";
            }
        }
        else if(loggedInUserRoleId == 7) // Chief Tech
        {
            if(studyList[currentIndex].IsManual == "Y")
            {
                document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='approveStudy();' />";
            }
        }
    }
    if(loggedInUserRoleId == 2 && studyList[currentIndex].Status != "Verified")
    {
        //Change this if you enable templates
        //document.getElementById("findingHeaderRad").style.display = "";    
    }
    //if(studyList[currentIndex].FindingId > 0)
    {
        loadingFinding = true;
        var data = "findingId=" + studyList[currentIndex].FindingId;
        $.post('FindingText.aspx',data, function(data) {            
            $('#tranTextDiv').html(data);
            //make the post to load templates
            if(loggedInUserRoleId == 2 && studyList[currentIndex].Status != "Verified")
            {            
                var templateData = 'userId=' + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value
                    + "&modalityName=" + studyList[currentIndex].Modality            
                $.post('TemplateList.aspx',templateData, function(data) {                   
                    $('#divTemplates').html(data);
                    loadingFinding = false;
                    hideLoading();
                    //saveFindingTimer = setTimeout("saveFinding()",5000);
                });
            }
            else
            {
                loadingFinding = false;
                hideLoading();            
            }
        });   
        //var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/GetFindingData";
        //var conn = YAHOO.util.Connect.asyncRequest("POST", sURL, callback,data);        
    }
    if(loggedInUserRoleId == 2) // Rad
    {   
        //var imagesDiv = document.getElementById("tabImages");
        //var url = "../WebViewer/DisplayStudyPage.aspx?StudyId=" + studyList[currentIndex].StudyId;
        //imagesDiv.innerHTML ="<table style='width:100%' cellpadding='0' cellspacing='0' border='0'><tr><td align='left' valign='top'><iframe src='" + url + "'name='MyIFrame' height='490px' width='920px' FRAMEBORDER='0' MARGINWIDTH='0px' MARGINHEIGHT='0px' ></iframe></td><td align='right' valign='top'><img src='../Images/zoom.png' style='cursor:hand;' alt='Click to enlarge' onclick='goToStudyDisplay();'/></td></tr></table>";
                              //+"<iframe src='" + url + "' width='100%' height='95%' FRAMEBORDER='0' MARGINWIDTH='0px' MARGINHEIGHT='0px' ></iframe><img src='../Images/zoom.png' style='cursor:hand;' alt='Click to enlarge' onclick='goToStudyDisplay();'/>";
    }
    
    //$("#findingTextArea").htmlarea();
    hideLoading();
    //document.getElementById("tabButtons").style.display="none";
    //document.getElementById("header").style.display="none";*/
}

function showNewFindingDialog(currentIndex, data) {

    if (confirm("This exam is already VERIFIED. Do you want to create a new Finding for this Exam?")) {
        document.getElementById("editFindingContentsDiv").src = "ReviseStudy.aspx?StudyId=" + studyList[currentIndex].StudyId;

        $("#findingDialogDiv").dialog({ title: "Report | " + studyList[currentIndex].PatientId + " | " + studyList[currentIndex].PatientName });
        jQuery("#findingDialogDiv").dialog('open');
    }
}


function saveFinding()
{
    var currentIndex = parseInt(document.getElementById("currentIndex").value);
    if(currentIndex >= 0)
    {
        var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/SaveFinding";
        var data = getData(currentIndex) + "&studyStatusId=" + studyList[currentIndex].StatusId;
        YAHOO.util.Connect.asyncRequest("POST",sURL,saveFindingCallback,data);
    }        
}

function getData(currentIndex)
{
    var data = "studyId=" + studyList[currentIndex].StudyId + "&findingId=" + studyList[currentIndex].FindingId;
    data += "&userId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value; 
    data += "&heading=" + $("#headingTextBox").val() + "&description=" + $("#descriptionTextBox").val() + "&impression=" + $("#impressionTextBox").val();    
    return data;
}

function approveStudy()
{
    if(verifyFindingText())
    {
        //clearTimeout(saveFindingTimer);
        updateStudy("ApproveStudy");
    }
}
function goToStudyDisplay()
{
    var currentIndex = parseInt(document.getElementById("currentIndex").value);      
    var url = "../WebViewer/DisplayStudyPage.aspx?StudyId=" + studyList[currentIndex].StudyId;
    window.open(url,"Images");
}
//Function to request server for report text
function getReportText()
{
        alert("here");
       var currentIndex = parseInt(document.getElementById("currentIndex").value);
       var templateId = document.getElementById("ddlTemplates").value;
       alert(templateId);
       if(templateId > 0)
       { 
            var data="templateId="+templateId;
            $.post('FindingText.aspx',data, function(data) {
                    $('#tranTextDiv').html(data);
            });           
           /*var sUrl= document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value+"/TemplatesData.asmx/TemplateText";
           YAHOO.util.Connect.asyncRequest("Post",sUrl,templateTextCallBack,data);
           if(studyList[currentIndex].Status == "New")
           document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='approveStudy();' />"; */
       }
}

function updateStudy(url)
{
    var currentIndex = parseInt(document.getElementById("currentIndex").value);
    if(currentIndex >= 0)
    {
        //findingDialog.hide();
        loadingDialog.setHeader("Saving, please wait...");
        loadingDialog.show();
        var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/"  + url;
        YAHOO.util.Connect.asyncRequest("POST",sURL,approveFindingCallback,getData(currentIndex));
    }
}

function markStudy()
{
    if(verifyFindingText())
    {
        //clearTimeout(saveFindingTimer);
        updateStudy("MarkStudy");
    }
}

function verifyFindingText()
{
    return verifyText($("#headingTextBox").val()) 
        && verifyText($("#descriptionTextBox").val()) 
        && verifyText($("#impressionTextBox").val()) ;
}
function verifyText(findingText)
{
    findingText.replace(/^\s+|\s+$/g,"");
    if(findingText.length <= 0)
    {
        document.getElementById("errorMessage").innerText = "Please enter valid transcription text."
        return false;
    }    
    return true;
}
function hideLoading()
{
    if(!loadingFinding && !loadingVoiceControl)
    {
        loadingDialog.hide();
        findingDialog.show();
    }    
}

function showVoiceControl(currentStudyList,loggedInUserId,loggedInUserRoleId)
{
    var isTranscriptionist = false;
    var isRadiologist = false;
    //loadingVoiceControl = true;
    var voiceControlObj = document.getElementById("voiceControlDiv");
    if(loggedInUserRoleId == 2)
      {
        isRadiologist = true;
        voiceControlObj.innerHTML = "<OBJECT  visible='false' id='VoiceControl' name='VoiceControl' classid='clsid:A3993B96-F2DF-4dd9-8D37-5C55E59FF553' VIEWASTEXT codebase='VoiceControl.cab'>";
      }
    else if(loggedInUserRoleId == 5)
      {
        isTranscriptionist = true;
        voiceControlObj.innerHTML = "<OBJECT id='VoiceControl' visible='false' name='VoiceControl' classid='clsid:A3993B96-F2DF-4dd9-8D37-5C55E59FF553' VIEWASTEXT codebase='VoiceControl.cab'>";
      }
    voiceControlObj.innerHTML += "<PARAM name='FindingId=' value='" + currentStudyList.FindingId + "' /> ";
    voiceControlObj.innerHTML += "<PARAM name='StudyId' value='" + currentStudyList.StudyId + "' />"; 
    voiceControlObj.innerHTML += "<PARAM name='UserId' value='" + loggedInUserId + "' />";                 
    voiceControlObj.innerHTML += "<PARAM name='ReadOnly' value='" + !isRadiologist + "' />";
    voiceControlObj.innerHTML += "<PARAM name='IsTranscriptionist' value='" + isTranscriptionist +"' />"
    voiceControlObj.innerHTML += "</OBJECT>";    
    
    document.VoiceControl.StudyId = currentStudyList.StudyId;
    document.VoiceControl.FindingId = currentStudyList.FindingId;
    document.VoiceControl.UserId = loggedInUserId;
    document.VoiceControl.ReadOnly = !isRadiologist;
    document.VoiceControl.IsTranscriptionist = isTranscriptionist;
    //setTimeout(checkVoiceControl(),1000);      
}

function showViewer()
{   
    var voiceControlObj = document.getElementById("voiceControlDiv");
    voiceControlObj.innerHTML = "<object classid='clsid:8AD9C840-044E-11D1-B3E9-00805F499D93' >"; 
    voiceControlObj.innerHTML += "<param name='archive' value='radscaper.jar' />";
    voiceControlObj.innerHTML += "<param name='code' value='com.divinev.radscaper.Main.class' />";
    voiceControlObj.innerHTML += "<param name='codebase' value='../WebViewer' />";    
    voiceControlObj.innerHTML += "<param name='Config' value='config.xml' /> ";
    voiceControlObj.innerHTML += "<param name='DicomImg1' value='http://localhost/WebViewer/data/70355/1.2.826.0.2.202387.846877.6153696.20081020203026656_0001_000001_1224527063021c.dcm' /> ";
    voiceControlObj.innerHTML += "</object> ";
}

function checkVoiceControl()
{
    //alert("checking...");
    if(document.VoiceControl.IsLoaded())
    {
        loadingVoiceControl = false;
        hideLoading();
    }
    else
    {
        setTimeout(checkVoiceControl(),1000);      
    }    
}
var flagControl=false;
function showHideControl()
{
    if(flagControl)
    {
        document.getElementById("header").style.display="none";
        flagControl=false;
        document.getElementById("controlLink").firstChild.nodeValue="Show VoiceControl"; 
    }
    else
    { 
        document.getElementById("header").style.display="";
        flagControl=true;
        document.getElementById("controlLink").firstChild.nodeValue="Hide VoiceControl";
    }
}
var flagTabs=false;
function showHideTabs()
{
    if(flagTabs)
    {
        document.getElementById("tabButtons").style.display="none";
        flagTabs=false;
        document.getElementById("tabsLink").firstChild.nodeValue="Show Tabs"; 
    }
    else
    { 
        document.getElementById("tabButtons").style.display="";
        flagTabs=true;
        document.getElementById("tabsLink").firstChild.nodeValue="Hide Tabs";
    }
}
function showStatusDialog()
{
     statusDialog.show();
}
function applyStatus()
{ 
    
    var labels="";
    var statusList=document.getElementById("ctl00_ContentPlaceHolder1_cblStatus");
    var selectedStatus=statusList.getElementsByTagName("input");
    var count=0;
    for(var i=0; i<selectedStatus.length; i++)
    {
        if(selectedStatus[i].checked==true)
            {
                var label= selectedStatus[i].parentElement.getElementsByTagName("label");
                if(labels!="")
                    {
                        labels+=","
                    }
                labels+=""+label[0].innerHTML;
                count++;
            }
    }
    document.getElementById("ctl00_ContentPlaceHolder1_hfCarryStatus").value=labels;  
    if(count>1)
    {
        document.getElementById("ctl00_ContentPlaceHolder1_statusText").value="Multiple";
    }
    else if(count==1)
    {
        for(var i=0; i<selectedStatus.length; i++)
        {
            if(selectedStatus[i].checked==true)
            {
                break;
            }
        }
        var label= selectedStatus[i].parentElement.getElementsByTagName("label");
        document.getElementById("ctl00_ContentPlaceHolder1_statusText").value=label[0].innerHTML;
    }
    else
    {
        document.getElementById("ctl00_ContentPlaceHolder1_statusText").value="[All]";
        document.getElementById("ctl00_ContentPlaceHolder1_hfCarryStatus").value="x";
    }
    if(selectedStatus[0].checked==true)
        document.getElementById("ctl00_ContentPlaceHolder1_statusText").value="[All]";
    statusDialog.hide();
}
function cancelStatus()
{
    statusDialog.hide();
}
function myWindow()
{ alert("123");}
function showModalityDialog()
{
     modalityDialog.show();
}
function applyModality()
{ 
    
    var labels="";
    var modalityList=document.getElementById("ctl00_ContentPlaceHolder1_cblModality");
    var selectedModality=modalityList.getElementsByTagName("input");
    //var selectedStatus= statusList.length;
    var count=0;
    //alert(selectedStatus[2].getElementByTagName("label").innerHTML);
    debugger
    for(var i=0; i<selectedModality.length; i++)
    {
        if(selectedModality[i].checked==true)
            {
                var label= selectedModality[i].parentElement.getElementsByTagName("label");
                if(labels!="")
                    {
                        labels+=","
                    }
                labels+=""+label[0].innerHTML;
                count++;
            }
    }
    document.getElementById("ctl00_ContentPlaceHolder1_hfCarryModality").value=labels;
    if(count>1)
    {
        document.getElementById("ctl00_ContentPlaceHolder1_modalityText").value="Multiple";
    }
    else if(count==1)
    {
        for(var i=0; i<selectedModality.length; i++)
        {
            if(selectedModality[i].checked==true)
            {
                break;
            }
        }
        var label= selectedModality[i].parentElement.getElementsByTagName("label");
        document.getElementById("ctl00_ContentPlaceHolder1_modalityText").value=label[0].innerHTML;
    }
    else
    {
        document.getElementById("ctl00_ContentPlaceHolder1_modalityText").value="[All]";
        document.getElementById("ctl00_ContentPlaceHolder1_hfCarryModality").value="x";
    }
    if(selectedModality[0].checked==true)
        document.getElementById("ctl00_ContentPlaceHolder1_modalityText").value="[All]";
    modalityDialog.hide();
}
function cancelModality()
{
    modalityDialog.hide();
}
function init() 
{  
    loadingDialog  = new YAHOO.widget.Panel("wait",  
			            { width:"240px", 
			              fixedcenter:true, 
			              close:false, 
			              draggable:false, 
			              zindex:4,
			              modal:true,
			              visible:false
			            } 
		            );

    loadingDialog.setHeader("Loading, please wait...");
    loadingDialog.setBody('<img src="../../Images/rel_interstitial_loading.gif" />');
    loadingDialog.render();
    
     /*findingDialog = new YAHOO.widget.Dialog("findingDialogDiv", 
							{ width : "950px",
						       fixedcenter: true,
						       visible: false,
						       draggable: false,
						       close: true,
						       modal: true,
						       text: "Do you want to continue?",
						       icon: YAHOO.widget.SimpleDialog.ICON_HELP,
						       constraintoviewport: true
							});*/

    // findingDialog.setHeader("Add Finding"); 
     //findingDialog.setHeader('<div style="float:left;">Finding</div><div style="float:right;text-align:right;"><input type="button" value=" X " onclick="closeWindow();"/></div>');
     //findingDialog.render("container");
     //YAHOO.util.Event.addListener(findingDialog,"close","test");
     //callCountURL("GetAllCounts",allCountCallback,"");
     /*
     customDateDialog=new YAHOO.widget.Dialog("customDateDialogeDiv",
                                                 { width:"500px",
                                                   fixedcenter:true,
                                                   close:false, 
			                                       draggable:false,
                                                   zindex:3,
			                                       modal:true,							  
							                       visible : false, 
							                       constraintoviewport : true
							                      }
							                     );
     customDateDialog.setHeader("Select Date");
     customDateDialog.render(document.body);
     
     statusDialog=new YAHOO.widget.Dialog("statusDiv",
                                                 { width:"200px",
                                                   fixedcenter:true,
                                                   close:false, 
			                                       draggable:false,
                                                   zindex:3,
			                                       modal:true,							  
							                       visible : false, 
							                       constraintoviewport : true
							                      }
							                     );
     statusDialog.setHeader('<div><table width="100%"><tr><td align="left"><td>Status</td><td width="100%" align="right"><input type="button" value="Apply" onclick="applyStatus();">&nbsp;&nbsp;<input type="button" value="cancel" onclick="cancelStatus();"></td></tr></table></div>');
     statusDialog.render(document.body);
          
     modalityDialog=new YAHOO.widget.Dialog("modalityDiv",
                                                 { width:"200px",
                                                   fixedcenter:true,
                                                   close:false, 
			                                       draggable:false,
                                                   zindex:3,
			                                       modal:true,							  
							                       visible : false, 
							                       constraintoviewport : true
							                      }
							                     );
     modalityDialog.setHeader('<table width="100%"><tr><td align="left"><td>Modality</td><td width="100%" align="right"><input type="button" value="Apply" onclick="applyModality();">&nbsp;&nbsp;<input type="button" value="cancel" onclick="cancelModality();"></td></tr></table>');
     modalityDialog.render(document.body);*/
     
     //commenting tab view as this is not needed. We are opening RadScaper from main window
     //var tabView = new YAHOO.widget.TabView('reportDiv'); 
     showSearchBar();

}
function closeWindow()
{
    document.getElementById("currentIndex").value = "-1";
    if(document.VoiceControl != null)
    {
        document.VoiceControl.CheckClose();
    }
    findingDialog.hide();
    document.aspnetForm.submit();
}

function log(studyId,patientId,action){
    var data="studyId=" + studyId + "&patientId=" + patientId + "&action=" + action;
    var sUrl= document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value+"/LoggingService.asmx/Log";
    YAHOO.util.Connect.asyncRequest("Post",sUrl,logCallback,data);
}

function invokeEFilm(patientId,accessionNumber,studyId)
{
    createEFilmAX(patientId,accessionNumber);
    var eFilmDiv = document.getElementById("eFilmDiv");
    var eFilmControl = document.getElementById('eFilmControl');
    if(eFilmControl != null && eFilmControl.openStudy != null)
    {        		
		var result = eFilmControl.openStudy();
		if(result)
		{		
			log(studyId,patientId,"Viewed Exam");
			eFilmDiv.innerHTML = "";		
		}
		else
		{
            eFilmDiv.innerHTML = "eFilm could not be loaded for the selected study.";		
		}
    }
    else
    {
        //invokeEFilm(patientId,
		eFilmDiv.innerHTML = "ActiveX not installed. Please click here to install it.";
    }
}
function createEFilmAX(patientId,accessionNumber)
{
	var eFilmDiv = document.getElementById("eFilmDiv");
	var obj = " <OBJECT ID='eFilmControl' WIDTH=0 HEIGHT=0 CLASSID='CLSID:023E9FAE-9641-49B6-95A0-24F19E43698D' VIEWASTEXT CODEBASE='EFlimActiveX.CAB'> ";  
    obj += " <PARAM NAME='_Version' VALUE='65536'/> ";
    obj += " <PARAM NAME='_ExtentX' VALUE='2646'/> ";
    obj += " <PARAM NAME='_ExtentY' VALUE='1323'/> ";
    obj += " <PARAM NAME='_StockProps' VALUE='0'/>  ";
	obj += " <PARAM NAME='patientId' VALUE='" + patientId + "'/>  ";
	obj += " <PARAM NAME='accessionNo' VALUE='" + accessionNumber + "'/>  ";
	obj += " </OBJECT> ";	
	eFilmDiv.innerHTML = obj;
}

function openRadscaper(studyId)
{
    if(confirm("The image(s) you are about to view is/are provided for NON-DIAGNOSTIC purposes only.  Click OK to acknowledge and continue, or, Cancel to return"))
    {   
        var wOpen;
        var sOptions;

        sOptions = 'status=yes,menubar=no,scrollbars=yes,resizable=yes,toolbar=no';
        sOptions = sOptions + ',width=' + (screen.availWidth - 10).toString();
        sOptions = sOptions + ',height=' + (screen.availHeight - 122).toString();
        sOptions = sOptions + ',screenX=0,screenY=0,left=0,top=0';

        wOpen = window.open( '', 'Radscaper', sOptions );
        wOpen.location = '../WebViewer/DisplayStudyPage.aspx?StudyId=' + studyId;
        wOpen.focus();
        wOpen.moveTo( 0, 0 );
        wOpen.resizeTo( screen.availWidth, screen.availHeight );
        return wOpen;
    }
}

//YAHOO.util.Event.onDOMReady(init);
