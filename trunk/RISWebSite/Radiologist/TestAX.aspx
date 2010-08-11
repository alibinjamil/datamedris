<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestAX.aspx.cs" Inherits="Radiologist_TestAX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script language="javascript" type="text/javascript" src="../Includes/digiply/Findings.js"></script>
         <object id="eFilmControl" width="0px" height="0px"
          classid="CLSID:023E9FAE-9641-49B6-95A0-24F19E43698D" codebase="EFlimActiveX.CAB">  
        <param name="_Version" value="65536" />
        <param name="_ExtentX" value="2646" />
        <param name="_ExtentY" value="1323" />
        <param name="_StockProps" value="0" />
        </object>
        <input type="hidden" value="9089" name="eFilmPatId" id="eFilmPatId" />
        <input type="hidden" value="2830" name="eFilmANo" id="eFilmANo" />        
        <input type="button" value="Click" onclick="invokeEFilm('123','456');" />
    </div>
    </form>
</body>
</html>
