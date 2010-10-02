using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Radiologist_TestAX2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TableCell tc = new TableCell();
        tc.Text = GetRepPopupText();
        TableRow tr = new TableRow();
        tr.Cells.Add(tc);
        Table1.Rows.Add(tr);        
    }
    private string GetRepPopupText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table style='width:100%' CellPadding='0' CellSpacing='0'><tr><td class='imageColumn' style='width:50%;text-align:left'>");
        //if (studyList.AccessionNumber != null && studyList.AccessionNumber.Length > 0)
        {
            sb.Append("<img class='linkImage' onclick='invokeEFilm(\"").Append("9089").Append("\",\"").Append("2830").Append("\");' src='../Images/eFilm_blue.JPG' alt='Invoke eFilm' />");
        }
        sb.Append("</td></tr></table>");
        return sb.ToString();
    }
}
