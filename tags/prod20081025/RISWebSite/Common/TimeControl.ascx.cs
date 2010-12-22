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

public partial class Common_TimeControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            ddlHour.Items.Add("--");
            ddlMin.Items.Add("--");
            for (int i = 0; i < 60; i++)
            {
                String temp = "";
                if (i < 10)
                    temp += "0";
                temp += i.ToString();
                ddlMin.Items.Add(temp);
            }
            for (int i = 0; i < 24; i++)
            {
                String temp = "";
                if (i < 10)
                    temp += "0";
                temp += i.ToString();
                ddlHour.Items.Add(temp);
            }
        }
    }
    
    public int Hour
    {
        get
        {
            if (ddlHour.SelectedIndex > 0)
            {
                return int.Parse(ddlHour.SelectedValue);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            ddlHour.SelectedValue = value.ToString();
        }
    }

    
    public int Minute
    {
        get
        {
            if (ddlMin.SelectedIndex > 0)
            {
                return int.Parse(ddlMin.SelectedValue);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            ddlMin.SelectedValue = value.ToString();
        }
    }
}
