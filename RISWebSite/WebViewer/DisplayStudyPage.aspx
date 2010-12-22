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
<APPLET archive=radscaper.jar codebase=./ code=com.divinev.radscaper.Main.class width="100%" height="98%">
<PARAM NAME=Config VALUE=config.xml />
<%=Parameters %>
</APPLET>
</div>
<div>
<div>
</div>
</body>
</html>