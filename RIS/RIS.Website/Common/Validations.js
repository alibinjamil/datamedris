// JScript File
function ValidateDDL(sender, args)
{    
    var obj = document.getElementById(sender.controltovalidate);
    if(obj.selectedIndex == 0)
    {
        args.IsValid = false;
        return false;
    }
    args.IsValid = true;
    return true;
}

function ValidateDate(controlName)
{
    var obj = document.getElementById(controlName + "_ddlMonth");
    if(obj.selectedIndex == 0)
    {
        return false;
    }
    obj = document.getElementById(controlName + "_ddlDay");
    if(obj.selectedIndex == 0)
    {
         return false;
    }                
    obj = document.getElementById(controlName + "_ddlYear");
    if(obj.selectedIndex == 0)
    {
        return false;
    }
    return true;            
}