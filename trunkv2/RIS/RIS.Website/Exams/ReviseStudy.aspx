<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviseStudy.aspx.cs" Inherits="Exams_ReviseStudy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Common/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">    
        This exam is already VERIFIED. Do you want to create a new Finding for this Exam?
        <br />
        <asp:Button ID="btnRevise" runat="server" Text="Revise Exam" 
            CssClass="buttonStyle" onclick="btnRevise_Click"/>
        <input type="button" class="buttonStyle" onclick="parent.document.aspnetForm.submit();" value="Cancel" />
    </div>
    </form>
</body>
</html>
