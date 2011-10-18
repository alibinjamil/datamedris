var editStudyDialog; 
$(document).ready(function() {
    jQuery("#editStudyDiv").dialog({
          bgiframe: true, autoOpen: false, height: 550, width:630, modal: true,
          close: function(ev, ui) {
             //do some thing
        }
    });
    jQuery("#findingDialogDiv").dialog({
          bgiframe: true, autoOpen: false, height: 610, width:1020, modal: true,
          close: function(ev, ui) {
             //do some thing
        }
    });    
    jQuery("#rejectExamDiv").dialog({
          bgiframe: true, autoOpen: false, height: 350, width:600, modal: true,
          close: function(ev, ui) {
             //do some thing
        }
      });
      jQuery("#reviseExamDiv").dialog({
          bgiframe: true, autoOpen: false, height: 100, width: 620, modal: true,
          close: function (ev, ui) {
              //do some thing
          }
      });
      jQuery("#addExamDiv").dialog({
          bgiframe: true, autoOpen: false, height: 550, width: 660, modal: true,
          close: function (ev, ui) {
              //do some thing
          }
      });     
});

  function openStudyEditWindow(studyId, currentIndex) {
    var data = "EditStudy.aspx?studyId=" + studyId;
    $('#editStudyFrm').attr("src", data);
    $("#editStudyDiv").dialog({ title: "Add notes &amp; information | " + studyList[currentIndex].PatientId + " | " + studyList[currentIndex].PatientName });
    $("#editStudyDiv").dialog('open'); 
}

/*function openRejectionWindow(studyId,currentIndex){
    openRejectionWindow(studyId,studyList[currentIndex].PatientId,studyList[currentIndex].PatientName);
}*/

function openRejectionWindow(studyId,patientId,patientName)
{
    var data = "RejectExam.aspx?studyId=" + studyId;
    $('#rejectExamFrame').attr("src",data); 
    $("#rejectExamDiv").dialog({ title: "Reject Exam | " + patientId + " | " + patientName });
    jQuery("#rejectExamDiv").dialog('open'); 
}

function closeStudyEditWindow(){
    jQuery("#editStudyDiv").dialog('close'); 
}

function closeRejectionWindow(){
    jQuery("#rejectExamDiv").dialog('close');
}

function addExamClick() {
    var data = "../Technologist/AddStudy.aspx";
    $('#addExamFrame').attr("src", "../Technologist/AddStudy.aspx");
    $("#addExamDiv").dialog({ title: "Add a new Exam | Step 1: Add Exam Information" });
    jQuery("#addExamDiv").dialog('open');
}