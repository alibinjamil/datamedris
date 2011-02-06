using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using RIS.Common;
/// <summary>
/// Summary description for GenericPage
/// </summary>
public abstract class GenericPage : System.Web.UI.Page
{
    protected void SetErrorMessage(string error)
    {
        SetMessage(error, "lblError");
    }
    protected void SetInfoMessage(string info)
    {
        SetMessage(info, "lblInfo");
    }
    private void SetMessage(string msg, string control)
    {
        if (this.Page.Master != null)
        {
            Control ctrl = this.Page.Master.FindControl(control);
            if (ctrl != null)
            {
                ctrl.Visible = true;
                ((Label)ctrl).Text = msg;
            }
        }
    }
    private RISEntities dbContenxt = null;
    protected RISEntities DatabaseContext
    {
        get
        {
            if (dbContenxt == null)
            {
                dbContenxt = new RISEntities();
            }
            return dbContenxt;
        }
    }
    protected override void OnLoad(EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        SetErrorMessage("");
        SetInfoMessage("");

        base.OnLoad(e);


    }


}
