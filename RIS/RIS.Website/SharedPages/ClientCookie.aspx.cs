using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using RIS.RISLibrary.Objects.RIS;
public partial class SharedPages_ClientCookie : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request[ParameterNames.Request.ClientId] != null)
        {
            ClientObject client = new ClientObject();
            client.ClientGUID.Value = Request[ParameterNames.Request.ClientId];
            client.Load();
            if(client.IsLoaded)
            {
                HttpCookie cookie = new HttpCookie(ParameterNames.Cookie.ClientId, Request[ParameterNames.Request.ClientId]);
                cookie.Expires = DateTime.Now.AddYears(10);
                Response.SetCookie(cookie);
            }
        }
    }
}