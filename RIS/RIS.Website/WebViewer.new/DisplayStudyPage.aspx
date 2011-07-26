<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DisplayStudyPage.aspx.cs" Inherits="WebViewer_DiplayStudyPage" Title="Untitled Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head><title>
	DataMed | Radiology Information System | View Study
</title>
    <link href="Common/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div>
<APPLET height="100px" width="100%" id="appletObj" archive=radscaper.jar codebase=./ code=com.divinev.radscaper.Main.class >
<PARAM NAME=Config VALUE=config.xml />
<%=Parameters %>
</APPLET>
</div>
<script type="text/javascript">
    var ih =  (document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight ) - 20;
    document.getElementById("appletObj").height = ih + "px";
</script>
</body>
</html>