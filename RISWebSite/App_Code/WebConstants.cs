using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Constants
/// </summary>
public static class WebConstants
{
    public static int PageSize = 15; // this is for total number of record per page
    public static int Pages = 10;// This is for Total Numbers between Previous and Next...change the value in  onPageLinkClick function i.e 10;

}
