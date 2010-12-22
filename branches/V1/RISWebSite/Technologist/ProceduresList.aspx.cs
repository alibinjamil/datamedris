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

public partial class Technologist_ProceduresList : AuthenticatedPage
{
    protected override void Page_Load_Extended(object sender, EventArgs e)
    {
        LabelError.Visible = false;
        if (Request["ProcedureId"] != null && Request["ProcedureId"].Length > 0)
        {
            ButtonSave.Text = "Update Procedure";
            ProceduresTableAdapters.ProcedureSelectCommandTableAdapter procTA = new ProceduresTableAdapters.ProcedureSelectCommandTableAdapter();
            Procedures.ProcedureSelectCommandDataTable procDT = procTA.GetProcedure(int.Parse(Request["ProcedureId"]));
            IEnumerator procIEnum = procDT.GetEnumerator();
            if(procIEnum.MoveNext())
            {
                Procedures.ProcedureSelectCommandRow procDR = (Procedures.ProcedureSelectCommandRow)procIEnum.Current;
                try
                {
                    TextBoxProcedureName.Text = procDR.ProcedureName;
                    DropDownListModalities.SelectedValue = procDR.ModalityId.ToString();
                    TextBoxCPTCode.Text = procDR.CPTCode;
                }
                catch (StrongTypingException ex)
                { }
            }            
        }
    }

    protected override bool IsPopUp()
    {
        return false;
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        ProceduresTableAdapters.ProcedureSelectCommandTableAdapter procTA = new ProceduresTableAdapters.ProcedureSelectCommandTableAdapter();
        if (Request["ProcedureId"] != null && Request["ProcedureId"].Length > 0)
        {
            procTA.ProcedureUpdateCommand(TextBoxProcedureName.Text,int.Parse(DropDownListModalities.SelectedValue),TextBoxCPTCode.Text,loggedInUserId,int.Parse(Request["ProcedureId"]));
            Response.Redirect("~/Technologist/ProceduresList.aspx");
        }
        else
        {
            procTA.ProcedureInsertCommand(int.Parse(DropDownListModalities.SelectedValue), TextBoxProcedureName.Text, TextBoxCPTCode.Text, loggedInUserId);
        }
    }
    protected void GridViewProcedures_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.Exception != null)
        {
            LabelError.Visible = true;
            e.ExceptionHandled = true;
        }
    }
    protected void LinkButtonEdit_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/Technologist/ProceduresList.aspx?ProcedureId=" + e.CommandArgument);
    }
}
