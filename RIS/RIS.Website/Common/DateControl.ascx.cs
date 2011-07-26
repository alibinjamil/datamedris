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

public partial class Common_DateControl : System.Web.UI.UserControl
{
    private int day = 0;
    private int month = 0;
    private int year = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (IsPostBack)
        {
            if (Request[MonthAttributeName] != null)
            {
                Month = int.Parse(Request[MonthAttributeName]);
                ViewState[MonthAttributeName] = Month;
            }
            else if (ViewState[MonthAttributeName] != null)
                Month = (int)ViewState[MonthAttributeName];
            if (Request[DayAttributeName] != null)
            {
                Day = int.Parse(Request[DayAttributeName]);
                ViewState[DayAttributeName] = Day;
            }
            else if (ViewState[DayAttributeName] != null)
                Day = (int)ViewState[DayAttributeName];
            if (Request[YearAttributeName] != null)
            {
                Year = int.Parse(Request[YearAttributeName]);
                ViewState[YearAttributeName] = Year;
            }
            else if (ViewState[YearAttributeName] != null)
                Year = (int)ViewState[YearAttributeName];
        }
    }
    public string MonthAttributeName
    {
        get
        {
            return this.ID + "_ddlMonth";
        }
    }
    public string DayAttributeName
    {
        get
        {
            return this.ID + "_ddlDay";
        }
    }
    public string YearAttributeName
    {
        get
        {
            return this.ID + "_ddlYear";
        }
    }

    public int Month
    {
        get
        {
            return month;
        }
        set
        {
            month = value;
        }
    }
    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
        }
    }
    public int Year
    {
        get
        {
            return year;
        }
        set
        {
            year = value;
        }
    }
    public DateTime Date
    {
        get
        {
            return new DateTime(Year, Month, Day);
        }   
        set
        {
            DateTime date = (DateTime)value;
            this.Day = date.Day;
            this.Month = date.Month;
            this.Year = date.Year;
        }
    }
    public void ClearSelection()
    {
        this.Day = 0;
        this.Month = 0;
        this.Year = 0;
    }
}
