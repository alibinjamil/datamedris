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
var callback = {
    success : function(o) {
        if(o.responseXML.documentElement.firstChild != null)
           document.getElementById('findingTextArea').value = o.responseXML.documentElement.firstChild.nodeValue;                                
        loadingFinding = false;
        hideLoading();
    },
    failure : function(o) {
        loadingFinding = false;
        hideLoading();
    }
}

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
            setTimeout("saveFinding()",30000);
        }
    },
    failure : function(o) {
        if(document.getElementById("currentIndex").value != "-1")
            setTimeout("saveFinding()",30000);
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
        showCounts(o,"spanAll");
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
        showCounts(o,"spanNew");
        callCountURL("GetUserCounts",userCountCallback,"loggedInUserName=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserName").value);
    },
    failure : function(o) {
        callCountURL("GetUserCounts",userCountCallback,"loggedInUserName=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserName").value);
    }
}

var userCountCallback = {
    success : function(o) {
        showCounts(o,"spanUser");
    },
    failure : function(o) {
    }
}
//--------------------------------------------------------------
var templateTextCallBack={
    success : function(o){  if(o.responseText!== undefined)
                               {
                                 document.getElementById("findingTextArea").value= o.responseXML.documentElement.firstChild.nodeValue;
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
function showFindingDialog(currentIndex,data)
{   
    //if(studyList[currentIndex].FindingId > 0)
    
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
    var loggedInUserRoleId = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserRoleId").value);
    var loggedInUserId = parseInt(document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value);
    document.getElementById("tranTextDiv").innerHTML = "";
    //showViewer();
    if(studyList[currentIndex].Status != "Verified")
    {
        showVoiceControl(studyList[currentIndex],loggedInUserId,loggedInUserRoleId);        
        
        if(loggedInUserRoleId == 2) // Rad
        {
            
            if(studyList[currentIndex].Status == "Pending Verification")
            {
                if(studyList[currentIndex].RadiologistId != loggedInUserId)
                {
                    document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='alert(\"Verification of this report is limited to dictating radiologist\");' />";
                }
                else
                {
                    document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='approveStudy();' />";
                }
            }
        }
        else if(loggedInUserRoleId == 5) //Tran
        {
            if(studyList[currentIndex].Status != "New")
            {            
                setTimeout("saveFinding()",30000);
                document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Mark for Verification' class='buttonStyle' onclick='markStudy();' />";
                document.getElementById("tranTextDiv").innerHTML = "<input type='button' value='Spell Check' onclick='onSpellCheckClick();' /><br/>"
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
        document.getElementById("ctl00_ContentPlaceHolder1_ddlTemplates").style.display="";
        document.getElementById("ctl00_ContentPlaceHolder1_lblTemplate").style.display="";
        document.getElementById("btnLoadTemplate").style.display="";    
    }
    else
    {
        document.getElementById("ctl00_ContentPlaceHolder1_ddlTemplates").style.display="none";
        document.getElementById("ctl00_ContentPlaceHolder1_lblTemplate").style.display="none";
        document.getElementById("btnLoadTemplate").style.display="none";
    }
    if(studyList[currentIndex].FindingId > 0)
    {
        loadingFinding = true;
        var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/GetFindingData";
        var conn = YAHOO.util.Connect.asyncRequest("POST", sURL, callback,data);        
    }
    if(loggedInUserRoleId == 2) // Rad
    {   
        var imagesDiv = document.getElementById("tabImages");
        var url = "../WebViewer/DisplayStudyPage.aspx?StudyId=" + studyList[currentIndex].StudyId;
        imagesDiv.innerHTML ="<table style='width:100%' cellpadding='0' cellspacing='0' border='0'><tr><td align='left' valign='top'><iframe src='" + url + "'name='MyIFrame' height='490px' width='920px' FRAMEBORDER='0' MARGINWIDTH='0px' MARGINHEIGHT='0px' ></iframe></td><td align='right' valign='top'><img src='../Images/zoom.png' style='cursor:hand;' alt='Click to enlarge' onclick='goToStudyDisplay();'/></td></tr></table>";
                              //+"<iframe src='" + url + "' width='100%' height='95%' FRAMEBORDER='0' MARGINWIDTH='0px' MARGINHEIGHT='0px' ></iframe><img src='../Images/zoom.png' style='cursor:hand;' alt='Click to enlarge' onclick='goToStudyDisplay();'/>";
    }
    document.getElementById("tranTextDiv").innerHTML += "<textarea rows='14' cols='95' id='findingTextArea' class='textBoxStyle'></textarea>";
    hideLoading();
    document.getElementById("tabButtons").style.display="none";
    document.getElementById("header").style.display="none";
}

function saveFinding()
{
    var currentIndex = parseInt(document.getElementById("currentIndex").value);
    if(currentIndex >= 0)
    {
        var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/SaveFinding";
        YAHOO.util.Connect.asyncRequest("POST",sURL,saveFindingCallback,getData(currentIndex));
    }        
}

function getData(currentIndex)
{
    var data = "studyId=" + studyList[currentIndex].StudyId + "&findingId=" + studyList[currentIndex].FindingId;
    data += "&userId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value; 
    data += "&findingText=" + document.getElementById("findingTextArea").value;    
    return data;
}

function approveStudy()
{
    updateStudy("ApproveStudy");
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
       var currentIndex = parseInt(document.getElementById("currentIndex").value);
       var templateId=document.getElementById("ctl00_ContentPlaceHolder1_ddlTemplates").value;
       if(templateId>0)
       { 
       var data="templateId="+templateId;
       var sUrl= document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value+"/TemplatesData.asmx/TemplateText";
       YAHOO.util.Connect.asyncRequest("Post",sUrl,templateTextCallBack,data);
       if(studyList[currentIndex].Status == "New")
       document.getElementById("buttonsDiv").innerHTML = "<input type='button' value='Verify & Close' class='buttonStyle' onclick='approveStudy();' />";
 
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
    updateStudy("MarkStudy");
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
        voiceControlObj.innerHTML = "<OBJECT height='40px' visible='false' width='120px' id='VoiceControl' name='VoiceControl' classid='clsid:A3993B96-F2DF-4dd9-8D37-5C55E59FF553' VIEWASTEXT codebase='VoiceControl.cab'>";
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
    loadingDialog.render(document.body);
    
     findingDialog = new YAHOO.widget.Dialog("findingDialogDiv", 
							{ width : "990px",
							  height : "575px",
							  //fixedcenter : true,
							  close : false,
			                  zindex:3,
			                  modal:true,							  
							  visible : false, 
							  constraintoviewport : true
							});

    // findingDialog.setHeader("Add Finding"); 
     findingDialog.setHeader('<div><table width="100%"><tr><td align="left" width="50%">Add Finding</td><td align="right" width="50%"><a id="tabsLink" href="#" onclick="showHideTabs();">Show Tabs</a>&nbsp;&nbsp;<a id="controlLink" href="#" onclick="showHideControl();">Show VoiceControl</a>&nbsp;&nbsp;<input type="button" value=" x " onclick="closeWindow();"></td></tr></table></div>');
     findingDialog.render(document.body);
     YAHOO.util.Event.addListener(findingDialog,"close","test");
     callCountURL("GetAllCounts",allCountCallback,"");
     
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
     modalityDialog.render(document.body);
     
     var tabView = new YAHOO.widget.TabView('reportDiv'); 
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


YAHOO.util.Event.onDOMReady(init);
