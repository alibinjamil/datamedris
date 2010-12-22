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

function callCountURL(functionName,callbackName,data)
{
    var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfWebServicesHomeURL").value + "RecordCountService.asmx/" + functionName;
    data += "&loggedInUserId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserId").value;
    data += "&loggedInUserRoleId=" + document.getElementById("ctl00_ContentPlaceHolder1_hfLoggedInUserRoleId").value;
    YAHOO.util.Connect.asyncRequest("POST",sURL,callbackName,data);
}

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
    if(studyList[currentIndex].Status != "Verified")
    {        
        if(loggedInUserRoleId == 2) // Rad
        {
            showVoiceControl(studyList[currentIndex],loggedInUserId,loggedInUserRoleId);        
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
            showVoiceControl(studyList[currentIndex],loggedInUserId,loggedInUserRoleId);        
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
    if(studyList[currentIndex].FindingId > 0)
    {
        loadingFinding = true;
        var sURL = document.getElementById("ctl00_ContentPlaceHolder1_hfSURL").value + "/GetFindingData";
        var conn = YAHOO.util.Connect.asyncRequest("POST", sURL, callback,data);        
    }
    document.getElementById("tranTextDiv").innerHTML += "<textarea rows='14' cols='120' id='findingTextArea' class='textBoxStyle'></textarea>";    
        
    hideLoading();
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
    if(loggedInUserRoleId == 2)
        isRadiologist = true;
    else if(loggedInUserRoleId == 5)
        isTranscriptionist = true;

    var voiceControlObj = document.getElementById("voiceControlDiv");
    voiceControlObj.innerHTML = "<OBJECT id='VoiceControl' name='VoiceControl' classid='clsid:A3993B96-F2DF-4dd9-8D37-5C55E59FF553' VIEWASTEXT codebase='VoiceControl.cab'>";
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
							{ width : "650px",
							  fixedcenter : true,
							  close : false,
			                  zindex:3,
			                  modal:true,							  
							  visible : false, 
							  constraintoviewport : true
							});

     findingDialog.setHeader("Add Finding");
     findingDialog.render(document.body);
     YAHOO.util.Event.addListener(findingDialog,"close","test");
     callCountURL("GetAllCounts",allCountCallback,"");
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
