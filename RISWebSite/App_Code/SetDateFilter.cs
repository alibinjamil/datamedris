using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for SetDateFilter
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SetDateFilter : System.Web.Services.WebService
{

    public SetDateFilter()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(EnableSession = true)]
    public string SetFilter(int SelectedValue)
    {
        Session["DateFilter"] = SelectedValue;
        return "Done";
    }
    [WebMethod(EnableSession = true)]
    public string SetCustomFilter(int FromDate, int ToDate)
    {
        Session["FromDate"] = new DateTime(2007, 11, 4);
        Session["ToDate"] = new DateTime(2007, 12, 7);
        return "Done";
    }
}

