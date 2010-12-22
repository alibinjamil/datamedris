<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAttachment.aspx.cs" Inherits="WebScan_AddAttachment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
		<title>DataMed | Radiology Information System | Add View</title>
		<meta content="False" name="vs_snapToGrid">
		<meta http-equiv="Content-Language" content="en-us">
		<LINK href="style.css" type="text/css" rel="stylesheet">
			<script language="javascript" id="clientEventHandlersJS">
<!--
var em = "";
//====================Page Onload  Start==================//
function CheckIfImagesInBuffer() {
    if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0)
	{
        //alert("There is no image in buffer");
        em = em + "There is no image in buffer.\n";
        document.all.emessage.innerText = em;
        return;
	}
}
function CheckErrorString() {
    if (frmScan.DynamicWebTwain1.ErrorCode != 0)
	{
        em = em + frmScan.DynamicWebTwain1.ErrorString + "\n";
        document.all.emessage.innerText = em;
        return;
	}
}
function pageonload() {
	frmScan.DynamicWebTwain1.MaxImagesInBuffer = 4;
	frmScan.DynamicWebTwain1.MouseShape = true;
	 
    var i;
    with(document.all){
        source.options.length=0;
        for(i=0;i<frmScan.DynamicWebTwain1.SourceCount;i++)
        {
            source.options.add(new Option(frmScan.DynamicWebTwain1.SourceNameItems(i),"i"));
        }
    }
    with(document.all){
        Resolution.options.length=0;
        Resolution.options.add(new Option("100",100));
        Resolution.options.add(new Option("150",150));
        Resolution.options.add(new Option("200",200));
        Resolution.options.add(new Option("300",300));
        
        InterpolationMethod.options.length = 0;
        InterpolationMethod.options.add(new Option("NearestNeighbor",1));
        InterpolationMethod.options.add(new Option("Bilinear",2));
        InterpolationMethod.options.add(new Option("Bicubic",3));
        
        //tbFileNameforSave.value = "WebTWAINImage";
        //txt_filePathforSave.value ="C:\\";
        //tbFileName.value = "WebTWAINImage";
        ADF.checked = true;
        //MultiPageTIFF_save.disabled = true;
        //MultiPagePDF_save.disabled = true;
        MultiPageTIFF.disabled = true;
        //MultiPagePDF.disabled = true;
        
        PreviewMode.options.length = 0;
        PreviewMode.options.add(new Option("1X1",0));
        PreviewMode.options.add(new Option("2X2",1));
        PreviewMode.selectedIndex = 0;
        
        frmScan.DynamicWebTwain1.SetViewMode(1,1);
    }
}

//====================Page Onload End====================//

//====================Preview Group Start====================//
function btnFirstImage_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer = 0;
	frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
	frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
}
function btnPreImage_onclick() {
	CheckIfImagesInBuffer();
	if (frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer == 0)
		return;
	frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer - 1;
	frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
	frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
}
function btnNextImage_onclick() {
	CheckIfImagesInBuffer();
    if (frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer == frmScan.DynamicWebTwain1.HowManyImagesInBuffer - 1)
        return;
    frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer + 1;
    frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
		frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
}
function btnLastImage_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer = frmScan.DynamicWebTwain1.HowManyImagesInBuffer - 1;
	frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
	frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
}
function btnRemoveCurrentImage_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.RemoveImage(frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);
	if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0){
    	frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
    	frmScan.CurrentImage.value = "";
    	return;
	}
	else{
    	frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
    	frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
	}
}
function btnRemoveAllImages_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.RemoveAllImages();
	frmScan.TotalImage.value = "0";
    frmScan.CurrentImage.value = "";
}
//====================Preview Group End====================//

//====================Edit Image Group Start=====================//
function btnShowImageEditor_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.ShowImageEditor();
}
function btnRotateRight_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.RotateRight(frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);
}
function btnRotateLeft_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.RotateLeft(frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);
}
function btnMirror_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.Mirror(frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);
}
function btnFlip_onclick() {
	CheckIfImagesInBuffer();
	frmScan.DynamicWebTwain1.Flip(frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);
}
/*----------------------Crop Method---------------------*/
function btnCrop_onclick() {
    if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0)
	  {
	  	  em = em + "There is no image in buffer.\n"
        document.all.emessage.innerText = em;
        return;
	  }
     document.getElementById("Crop").style.visibility="visible";
}
function btnCropCancel_onclick(){
     document.getElementById("Crop").style.visibility="hidden";
}
function btnCropOK_onclick(){
    if(document.all.img_left.value == ""){
        em = em + "Please input left value.\n"
        document.all.emessage.innerText = em;
        return;
    }
    if(document.all.img_top.value == ""){
        em = em + "Please input top value.\n"
        document.all.emessage.innerText = em;
        return;    
    }
    if(document.all.img_right.value == ""){
        em = em + "Please input right value.\n"
        document.all.emessage.innerText = em;
        return;   
    }
    if(document.all.img_bottom.value == ""){
        em = em + "Please input bottom value.\n"
        document.all.emessage.innerText = em;
        return;    
    }
    frmScan.DynamicWebTwain1.Crop(
        frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer,
        document.all.img_left.value,
        document.all.img_top.value,
        document.all.img_right.value,
        document.all.img_bottom.value);

     document.getElementById("Crop").style.visibility="hidden";
}
/*-----------------------------------------------------*/
//====================Edit Image Group End==================//


function btnScan_onclick() {
    var i;
    with(document.all){
	    frmScan.DynamicWebTwain1.SelectSourceByIndex(source.selectedIndex);
	    frmScan.DynamicWebTwain1.IfShowUI = ShowUI.checked;
	    frmScan.DynamicWebTwain1.CloseSource();
	    frmScan.DynamicWebTwain1.OpenSource();
	
            for(i=0;i<3;i++)
            {
                if(document.getElementsByName("PixelType").item(i).checked==true)
                    frmScan.DynamicWebTwain1.PixelType = i;
            }
	    frmScan.DynamicWebTwain1.IfDisableSourceAfterAcquire = true;
	    frmScan.DynamicWebTwain1.Resolution = Resolution.value;

	    if (frmScan.DynamicWebTwain1.Resolution != Resolution.value)
	    {
	    	em = em + "Fail to set resolution.\nCurrent source does not support the resolution you set.\n";
	    	document.all.emessage.innerText = em;
	    	return;
	    }

	    frmScan.DynamicWebTwain1.IfFeederEnabled = ADF.checked ;
	    frmScan.DynamicWebTwain1.IfDuplexEnabled = Duplex.checked ;
	    em = em + "Pixel Type: " + frmScan.DynamicWebTwain1.PixelType + "\nResolution: " + frmScan.DynamicWebTwain1.Resolution + "\n";
	    document.all.emessage.innerText = em;
	    frmScan.DynamicWebTwain1.AcquireImage();
	}
}

/*----------------Change Image Size--------------------*/
function btnChangeImageSize_onclick(){
    if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0)
	  {
	  	  em = em + "There is no image in buffer.\n"
        document.all.emessage.innerText = em;
        return;
	  }
     document.getElementById("ImgSizeEditor").style.visibility="visible";
}
function btnCancel_onclick() {
     document.getElementById("ImgSizeEditor").style.visibility="hidden";
}

function btnOK_onclick(){
    if(document.all.img_height.value == ""){
    	  em = em + "Please input the height.\n";
        document.all.emessage.innerText = em;
        return;    
     }
    if(document.all.img_width.value == ""){
    	  em = em + "Please input the width.\n";
        document.all.emessage.innerText = em;
        return;   	  
    }


    frmScan.DynamicWebTwain1.ChangeImageSize(
        frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer,
        document.all.img_width.value,
        document.all.img_height.value,
        document.all.InterpolationMethod.selectedIndex + 1);
    document.getElementById ("ImgSizeEditor").style.visibility = "hidden";
}

/*-----------------Save Image Group---------------------*/
function btnSave_onclick(){
    if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0)
	  {
	  	  em = em + "There is no image in buffer.\n"
        document.all.emessage.innerText = em;
        return;
	  }
    var i,strimgType_save;
    for(i=0;i<5;i++){
        if(document.getElementsByName("imgType_save").item(i).checked == true){
           strimgType_save  = document.getElementsByName("imgType_save").item(i).value;
           break;
        }
    }
    if(document.all.txt_filePathforSave.value.charAt(document.all.txt_filePathforSave.value.length-1) != "\\")
	    document.all.txt_filePathforSave.value = document.all.txt_filePathforSave.value + "\\";
    var strFilePath = document.all.txt_filePathforSave.value+document.all.tbFileNameforSave.value+"."+strimgType_save;

   if(strimgType_save == "tif" && document.all.MultiPageTIFF_save.checked){
        frmScan.DynamicWebTwain1.SaveAllAsMultiPageTIFF(strFilePath);
        }
    else if(strimgType_save == "pdf" && document.all.MultiPagePDF_save.checked){
        frmScan.DynamicWebTwain1.SaveAllAsPDF(strFilePath);}
    else{
        with(document.all){
            switch(i){
                case 0:frmScan.DynamicWebTwain1.saveasbmp(strFilePath , frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);break;
                case 1:frmScan.DynamicWebTwain1.SaveasJPEG(strFilePath ,frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);break;
                case 2:frmScan.DynamicWebTwain1.SaveasTIFF(strFilePath ,frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);break;
                case 3:frmScan.DynamicWebTwain1.SaveasPNG(strFilePath , frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);break;
                case 4:frmScan.DynamicWebTwain1.SaveasPDF(strFilePath , frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer);break;
	        } 
	    }
	}
	CheckErrorString();
}
/*-------------------------------------------------------*/

/*-----------------Upload Image Group---------------------*/
function btnUpload_onclick(){
    
    if (frmScan.DynamicWebTwain1.HowManyImagesInBuffer == 0)
	  {
	  	  em = em + "There is no image in buffer.\n";
        document.all.emessage.innerText = em;
        return;
	  }
	var i,strHTTPServer,strActionPage,strImageType;
    if(document.getElementById("tbFileName").value == ""){
    	em = em + "Please input file name.\n";
        document.all.emessage.innerText = em;
        return;
    }
    strHTTPServer = "<%=Request.Url.Authority%>";
    strActionPage = "/WebScan/SaveAttachment.aspx";
    for(i=0;i<5;i++){
        if(document.getElementsByName("ImageType").item(i).checked == true){
           strImageType  = i;
           break;
        }
    }
    frmScan.DynamicWebTwain1.SetHTTPFormField("<%=ParameterNames.Request.StudyId%>","<%=Request[ParameterNames.Request.StudyId]%>");
    frmScan.DynamicWebTwain1.SetHTTPFormField("<%=ParameterNames.Request.UserId%>","<%=loggedInUserId%>");
    frmScan.DynamicWebTwain1.SetHTTPFormField("<%=ParameterNames.Request.Name%>",escape(frmScan.tbFileName.value));
    frmScan.DynamicWebTwain1.SetHTTPFormField("<%=ParameterNames.Request.Description%>",escape(frmScan.tbDescription.value));
    if(strImageType == 2 && document.all.MultiPageTIFF.checked){
        frmScan.DynamicWebTwain1.HTTPUploadAllThroughPostAsMultiPageTIFF(
             strHTTPServer, 
             strActionPage,
             document.all.tbFileName.value + document.getElementsByName("ImageType").item(i).value);
             //frmScan.submit();
    }
    else if(strImageType == 4 && document.all.MultiPagePDF.checked){
        frmScan.DynamicWebTwain1.HTTPUploadAllThroughPostAsPDF(
             strHTTPServer, 
             strActionPage,
             document.all.tbFileName.value + document.getElementsByName("ImageType").item(i).value);
             //frmScan.submit();
    }
    else{
        frmScan.DynamicWebTwain1.HTTPUploadThroughPostEx(
            strHTTPServer, 
            frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer,
            strActionPage,
            document.all.tbFileName.value + document.getElementsByName("ImageType").item(i).value,
            strImageType);
            //frmScan.submit();
    }
    
    CheckErrorString();
    /*if (frmScan.DynamicWebTwain1.ErrorCode == 0)
			frmScan.submit();*/
}
/*------------------radio response----------------------------*/
function rdTIFFsave_onclick(){
    document.all.MultiPageTIFF_save.disabled = false;
    
    document.all.MultiPageTIFF_save.checked = false;
    document.all.MultiPagePDF_save.checked = false;
    document.all.MultiPagePDF_save.disabled = true;
}
function rdPDFsave_onclick(){
    document.all.MultiPagePDF_save.disabled = false;

    document.all.MultiPageTIFF_save.checked = false;
    document.all.MultiPagePDF_save.checked = false;
    document.all.MultiPageTIFF_save.disabled = true;
}
function rdsave_onclick(){
    document.all.MultiPageTIFF_save.checked = false;
    document.all.MultiPagePDF_save.checked = false;
    
    document.all.MultiPageTIFF_save.disabled = true;
    document.all.MultiPagePDF_save.disabled = true;
}
function rdTIFF_onclick(){
    document.all.MultiPageTIFF.disabled = false;
    
    document.all.MultiPageTIFF.checked = false;
    document.all.MultiPagePDF.checked = false;
    document.all.MultiPagePDF.disabled = true;
}
function rdPDF_onclick(){
    document.all.MultiPagePDF.disabled = false;

    document.all.MultiPageTIFF.checked = false;
    document.all.MultiPagePDF.checked = false;
    document.all.MultiPageTIFF.disabled = true;
}
function rd_onclick(){
    document.all.MultiPageTIFF.checked = false;
    document.all.MultiPagePDF.checked = false;
    
    document.all.MultiPageTIFF.disabled = true;
    document.all.MultiPagePDF.disabled = true;
}
/*------------------select menu response----------------------------*/

function slPreviewMode(){
		if (document.all.PreviewMode.selectedIndex == 0)
			frmScan.DynamicWebTwain1.SetViewMode(1, 1);
		else
			frmScan.DynamicWebTwain1.SetViewMode(2, 2);
    //frmScan.DynamicWebTwain1.SetViewMode(document.all.PreviewMode.selectedIndex+1,document.all.PreviewMode.selectedIndex+1);
}
function DynamicWebTwain1_OnPostTransfer() {

		if (document.all.DiscardBlank.checked == true) {
			var NewlyScannedImage = frmScan.DynamicWebTwain1.CurrentImageIndexInbuffer;
			if (frmScan.DynamicWebTwain1.IsBlankImage(NewlyScannedImage))
				frmScan.DynamicWebTwain1.RemoveImage(NewlyScannedImage);
		}
		frmScan.TotalImage.value = frmScan.DynamicWebTwain1.HowManyImagesInBuffer;
		frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer+1;
}
//-->
function DynamicWebTwain1_OnMouseClick(index) {
		frmScan.CurrentImage.value = frmScan.DynamicWebTwain1.CurrentImageIndexInBuffer + 1;
}
//-->
        </script>
<script language=javascript for=DynamicWebTwain1 event=OnPostTransfer>
<!--
 DynamicWebTwain1_OnPostTransfer();
//-->
        </script>
<script language=javascript for=DynamicWebTwain1 event=OnMouseClick(index)>
<!--
 DynamicWebTwain1_OnMouseClick(index);
//-->
        </script>
        </script>
    <style>
    input{
    font:normal 11px verdana;}
    </style>   

	</head>
	<body onload ="pageonload();" >
		<center>
			<form id="frmScan" action="saveattachment.aspx">
				
				
				<table class="body_Narrow_Width" cellSpacing="0" cellPadding="0" align="center" border="0">
				
	                <tr vAlign="top" align="left">
						<td><img src="<%=GetHeaderURL()%>" /></td>
					</tr>

				</table>
                 <table style="margin-top:20px;">
                <tr>
                <td>
                <asp:HyperLink ID="hlAddAttachment1" runat="server">
                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/table.png" />                        
                </asp:HyperLink>
                </td>
                
                <td>
                    <asp:HyperLink ID="hlAddAttachment2" runat="server">
                        Go Back to List of Views for this Exam
                    </asp:HyperLink>
                </td>
                </tr>
                </table>				
				<table height="10" cellSpacing="0" cellPadding="0" width="772" border="0" nof="ly">
					<tr vAlign="top" align="left">
						<td background="menu_back.gif" height="20"></td>
					</tr>
					<tr><td bgColor="#ffffff"><br></td></tr>
				</table>
				<table height="705" cellSpacing="0" cellPadding="0" width="1000" bgColor="#ffffff" border="0" nof="ly">
					<object classid = "clsid:5220cb21-c88d-11cf-b347-00aa00a28331" viewastext>
						<param name="LPKPath" value="DynamicWebTwain.lpk" />
					</object>
					<tr>
						<td vAlign="top" align="center" width="60%">
							<table width ="90%">
								<tr>
									<td align ="center">
										<object id="DynamicWebTwain1" codeBase="DynamicWebTWAIN.cab#version=6.1" height="528" width="100%"
											classid="clsid:E7DA7F8D-27AB-4EE9-8FC0-3FEC9ECFE758" viewastext>
											<param name="_cx" value="3784" />
											<param name="_cy" value="4128" />
											<param name="JpgQuality" value="80" />
											<param name="Manufacturer" value="DynamSoft Corporation" />
											<param name="ProductFamily" value="Dynamic Web TWAIN" />
											<param name="ProductName" value="Dynamic Web TWAIN" />
											<param name="VersionInfo" value="Dynamic Web TWAIN 6.1" />
											<param name="TransferMode" value="0" />
											<param name="BorderStyle" value="0" />
											<param name="FTPUserName" value="" />
											<param name="FTPPassword" value="" />
											<param name="FTPPort" value="21" />
											<param name="HTTPUserName" value="" />
											<param name="HTTPPassword" value="" />
											<param name="HTTPPort" value="443" />
											<param name="ProxyServer" value="" />
											<param name="IfDisableSourceAfterAcquire" value="0" />
											<param name="IfShowUI" value="-1" />
											<param name="IfModalUI" value="-1" />
											<param name="IfTiffMultiPage" value="0" />
											<param name="IfThrowException" value="0" />
											<param name="MaxImagesInBuffer" value="1" />
											<param name="TIFFCompressionType" value="0" />
											<param name="IfFitWindow" value="-1" />
											<param name="IfSSL" value="true" />
										</object>
									</td>
								</tr>
								<tr>
									<td align="center">
										<p><input id="btnFirstImage" onclick="return btnFirstImage_onclick()" type="button" value=" |< ">&nbsp;
											<input id="btnPreImage" onclick="return btnPreImage_onclick()" type="button" value=" < ">&nbsp;&nbsp;
											<input type="text" name="CurrentImage" size="2" id="CurrentImage" readonly="readOnly"/>/<input type="text" name="TotalImage" size="2" id="TotalImage" readonly="readOnly" value="0"/>&nbsp;&nbsp;
											<input id="btnNextImage" onclick="return btnNextImage_onclick()" type="button" value=" > ">&nbsp;
											<input id="btnLastImage" onclick="return btnLastImage_onclick()" type="button" value=" >| ">
											Preview Mode
											<select size="1" name="PreviewMode" onchange ="slPreviewMode();">
											</select>
											<br>
											<input id="btnRemoveCurrentImage" onclick="return btnRemoveCurrentImage_onclick()" type="button"
												value="Remove Current Image"><input id="btnRemoveAllImages" onclick="return btnRemoveAllImages_onclick()" type="button"
												value="Remove All Images"><br>
											</p>
									</td>
								</tr>
								<TR>
									<TD align="center">
										<P align="left">Message:<br>
											<textarea name="emessage" rows="6" cols="50"  readonly="readOnly"></textarea>&nbsp;</P>
									</TD>
								</TR>
							</table>
						</td>
						<td vAlign="top" width ="40%">
							<table width="90%" bgColor="#f0f0f0">
								<tr>
									<td width="9" height="30"></td>
									<td height="30"> 
                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/arrow1.gif" />                                  </td>
									<td height="30" align="left"><b>Custom Scan</b></td>
								</tr>
								<tr>
									<td vAlign="middle" width="3%"></td>
									<td vAlign="middle" width="3%"></td>
									<td vAlign="middle" width="94%" align="left">Select Source: &nbsp;
										<select size="1" name="source">
										</select></td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>If Show UI <input type="checkbox" value="ON" name="ShowUI">&nbsp;
									</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>If Discard Blank Page <input type="checkbox" value="ON" name="DiscardBlank">&nbsp;
									</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>Pixel Type
										<groupbox><input type="radio"  value="V15" name="PixelType">BW <input type="radio" value="V13" CHECKED name="PixelType">Gray <input type="radio" value="V14" name="PixelType">RGB</groupbox></td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>Resolution&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<select size="1" name="Resolution">
										</select></td>
								</tr>
								<tr>
									<td vAlign="top" colSpan="3" height="35">
										<p align="center">
                                        <input id="btnScan" type="button" style ="size:10; width: 104; height:37; font-family:Arial Black; color:#FE8E14; font-size:14pt; font-style:italic" value="Scan" onclick ="btnScan_onclick();"></p>
									</td>
								</tr>
							</table>
							<table>
								<tr>
									<br>
								</tr>
							</table>
							<table width="90%" bgColor="#f0f0f0">
								<tr>
									<td height="30"></td>
									<td height="30">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/arrow1.gif" /></td>
									<td height="30" align="left"><b>Use ADF</b></td>
								</tr>
								<tr>
									<td width="3%"></td>
									<td width="3%"></td>
									<td width="94%" align="left">
									    ADF <input type="checkbox" value="ON" name="ADF">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									    Duplex <input type="checkbox" value="ON" name="Duplex">
									</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td vAlign="top" height="35"></td>
								</tr>
							</table>
							<table>
								<tr>
									<br>
								</tr>
							</table>
							<table width="90%" bgColor="#f0f0f0">
								<tr>
									<td height="25"></td>
									<td height="25">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/arrow1.gif" /></td>
									<td height="25" align="left"><b>Edit Image</b></td>
								</tr>
								<tr>
								    <td></td>
								    <td></td>
								    <td><p>
								    <div style="border:1px solid #000000; position: absolute; height: 97px; z-index: 1; left: 703px; top: 424px; background-color: #f0f0f0; width:275px; visibility: hidden" id="ImgSizeEditor">	
										<table border="0" style="border-collapse: collapse" width="100%" id="table1">
                                          <tr>
                                            <td width="94"></td>
                                            <td></td>
                                          </tr>
                                          <tr>
                                            <td width="94">New Height:</td>
                                            <td>
										<input type="text" name="img_height" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))">pixel</td>
                                          </tr>
                                          <tr>
                                            <td width="94">New Width:</td>
                                            <td>
										<input type="text" name="img_width" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))">pixel</td>
                                          </tr>
                                          <tr>
                                            <td width="94">Interpolation 
                                        method</td>
                                            <td><select size="1" name="InterpolationMethod">
                                        </select></td>
                                          </tr>
                                          <tr>
                                            <td width="94">
										<input type="button" value="     OK     " name="btn_OK" onclick ="return btnOK_onclick()" style="float: right"></td>
                                            <td>
										<input type="button" value="  Cancel  " name="btn_Cancel" onclick ="return btnCancel_onclick()"></td>
                                          </tr>
                                          <tr>
                                            <td width="94">
										    </td>
                                            <td></td>
                                          </tr>
                                        </table>
</div></p>								<p>
									<div style="border:1px solid #000000; position: absolute; height: 125px; z-index: 1; left: 703px; top: 433px; background-color: #f0f0f0; width:275px; visibility: hidden" id="crop">	
									<table width =100%>
									<tr>
									<td width =50%></td>
									<td width =50%></td>
									</tr>
									<tr>
									<td width =50% height="26">left:&nbsp; <input type="text" name="img_left" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))"></td>
									<td width =50% height="26">top:&nbsp;&nbsp;&nbsp;&nbsp; <input type="text" name="img_top" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))"></td>
									</tr>
									<tr><td>right:<input type="text" name="img_right" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))"></td><td>
                                      bottom:<input type="text" name="img_bottom" size="10" onkeyup="if(event.keyCode !=37 &amp;&amp; event.keyCode!=39) value=value.replace(/\D/g,'');"
													onpaste="clipboardData.setData('text',clipboardData.getData('text').replace(/\D/g,''))"></td></tr>
									<tr><td>(Unit: inch)</td></tr>
									<tr><td></td><td><input type="button" value="    OK    " name="btn_OK1" onclick ="return btnCropOK_onclick()"><input type="button" value="Cancel" name="btn_Cancel1" onclick ="return btnCropCancel_onclick()"></td></tr>
									<tr><td></td><td rowspan="2"></td></tr>
									<tr>
										<td></td>
									</tr>
									</table>
										
                                    </div>
									</p></td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>
									    <input id="btnShowImageEditor" onclick="return btnShowImageEditor_onclick()" type="button" value="Show Image Editor">
									    <input id="btnRotateRight" onclick="return btnRotateRight_onclick()" type="button" value="Rotate Right"> 
									    <input id="btnRotateLeft" onclick="return btnRotateLeft_onclick()" type="button" value="Rotate Left">
									</td>
								</tr>
								
								<tr>
									<td></td>
									<td></td>
									<td>
										<input id="btnMirror" onclick="return btnMirror_onclick()" type="button" value="Mirror">
										<input id="btnFlip" onclick="return btnFlip_onclick()" type="button" value=" Flip ">
										<input id="btnCrop" type="button" value="Crop" onclick="return btnCrop_onclick()">
										<input id="btnChangeImageSize" type="button" value="Change Image Size" onclick ="return btnChangeImageSize_onclick()">
									</td>
								</tr>
								
							</table>
							<table>
								<tr>
									<br>
								</tr>
							</table>
							
							<table>
								<tr>
									<br>
								</tr>
							</table>
							<table width="90%" bgColor="#f0f0f0">
								<tr>
									<td height="30"></td>
									<td height="30">
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/arrow1.gif" /></td>
									<td height="30" align="left"><b>Upload Image</b></td>
								</tr>
								<tr>
									<td width="3%"></td>
									<td width="3%"></td>
									<td width="94%" align="left">Name: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="text" size="20" name="tbFileName" id="tbFileName"/></td>
								</tr>
								<tr>
									<td width="3%"></td>
									<td width="3%"></td>
									<td width="94%" align="left" valign="middle">Description: <textarea type="text" size="20" rows="3" name="tbDescription" id="tbDescription"></textarea></td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td><label><input type="hidden" value=".bmp" name="ImageType" onclick ="rd_onclick();"  ></label>
										<label>
											<input type="radio" value=".jpg" name="ImageType" checked onclick ="rd_onclick();" disabled></label>JPEG
										<label>
											<input type="radio" value=".tif" name="ImageType" onclick ="rdTIFF_onclick();" disabled></label>TIFF
										<label>
											<input type="radio" value=".png" name="ImageType" onclick ="rd_onclick();" disabled></label>PNG
										<label>
											<input type="radio" value=".pdf" name="ImageType" onclick ="rdPDF_onclick();" checked></label>PDF</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td>
										<p><input type="checkbox" value="ON" name="MultiPageTIFF">Multi-Page TIFF <input type="checkbox" value="ON" name="MultiPagePDF">Multi-Page 
											PDF</p>
									</td>
								</tr>
								<tr>
									<td vAlign="top" colSpan="3" height="35">
										<p align="center"><input id="btnUpload" type="button" value="Upload Image" onclick ="return btnUpload_onclick()"></p>
									</td>
								</tr>
							</table>
					<tr>
						<td><br>
						</td>
					</tr>
					</TD></TR></table>
				<table cellSpacing="0" cellPadding="0" width="772" bgColor="#ffffff" border="0" nof="ly">
					<tr vAlign="top" align="left">
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="772" bgColor="#ffffff" border="0" nof="ly">
					<tr vAlign="top" align="left">
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="772" bgColor="#ffffff" border="0" nof="ly">
					<tr vAlign="top" align="left">
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="772" bgColor="#ffffff" border="0" nof="ly">
					<tr vAlign="top" align="left">
					</tr>
				</table>
				<table cellSpacing="0" cellPadding="0" width="772" bgColor="#ffffff" border="0" nof="ly">
					<tr vAlign="top" align="left">
						<td>
						     <div style="text-align:right">
                            <a href="http://www.datamedusa.com" target="_blank"><img alt="DataMed" src="../Images/Powered-by-DataMed.jpg" style="border:0px;"/></a>
                            </div>   
						</td>
					</tr>
				</table>
			</form>
		</center>
		

        
	</body>
</html>
