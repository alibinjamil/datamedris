<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DateControl.ascx.cs" Inherits="Common_DateControl" %>
<script language="javascript">
    function fillYear()
    {
        var i=0;
        var yearObj = document.getElementById("<%=this.YearAttributeName%>");
        yearObj.options.add(new Option("----","0"));
        for(i=1900;i<=<%=DateTime.Now.Year%>;i++)
        {
            yearObj.options.add(new Option(""+i,""+i));            
        }
        //yearObj.options[41].selected = true;    
    }
    function fillValues()
    {
        document.getElementById("<%=this.YearAttributeName%>").value = "<%=this.Year%>";
        document.getElementById("<%=this.MonthAttributeName%>").value = "<%=this.Month%>";
        document.getElementById("<%=this.DayAttributeName%>").value = "<%=this.Day%>";
    }
    function addDay(dayObj,day)
    {
        if(day < 10)
        {
            dayObj.options.add(new Option("0" + day.toString(),day.toString()));            
        }
        else
        {
            dayObj.options.add(new Option(day.toString(),day.toString()));            
        }
    }
    function checkDate()
    {
        var yearObj = document.getElementById("<%=this.YearAttributeName%>");
        var monthObj = document.getElementById("<%=this.MonthAttributeName%>");
        var dayObj = document.getElementById("<%=this.DayAttributeName%>");  
        var daySelectedIndex = dayObj.selectedIndex;
        //alert(dayObj.options.splice);
        //dayObj.options.splice(1,dayObj.length-2);
        var i = 1;        
        var count = dayObj.length;
        for(i=1;i<count;i++)
        {
            dayObj.options[1] = null;
        }
        var month = parseInt(monthObj.options[monthObj.selectedIndex].value);
        if( month == 4 || month == 6 || month == 9 || month == 11)
        {
           for(i=1;i<=30;i++)
           {
                addDay(dayObj,i);
           }
        }
        else if( month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
        {
           for(i=1;i<=31;i++)
           {
                addDay(dayObj,i);
           }
        }
        else if(month == 2)
        {
           for(i=1;i<=28;i++)
           {
                addDay(dayObj,i);
           }
            
            var year = parseInt(yearObj.options[yearObj.selectedIndex].value);            
            if(year%4==0)
            {
                addDay(dayObj,29);
            }
        }
        if(daySelectedIndex < dayObj.length)
            dayObj.selectedIndex = daySelectedIndex;
        else
            dayObj.selectedIndex = dayObj.length - 1;
    }
</script>
<select id="<%=this.MonthAttributeName%>" name="<%=this.MonthAttributeName%>" class="dropDownListStyle" onchange="checkDate()">
	<option value="0">--</option>
	<option value="1">01</option>
	<option value="2">02</option>
	<option value="3">03</option>
	<option value="4">04</option>
	<option value="5">05</option>
	<option value="6">06</option>
	<option value="7">07</option>
	<option value="8">08</option>
	<option value="9">09</option>
	<option value="10">10</option>
	<option value="11">11</option>
	<option value="12">12</option>
</select>
<select name="<%=this.DayAttributeName%>" id="<%=this.DayAttributeName%>" class="dropDownListStyle">
	<option value="0">--</option>
	<option value="1">01</option>
	<option value="2">02</option>
	<option value="3">03</option>
	<option value="4">04</option>
	<option value="5">05</option>
	<option value="6">06</option>
	<option value="7">07</option>
	<option value="8">08</option>
	<option value="9">09</option>
	<option value="10">10</option>
	<option value="11">11</option>
	<option value="12">12</option>
	<option value="13">13</option>
	<option value="14">14</option>
	<option value="15">15</option>
	<option value="16">16</option>
	<option value="17">17</option>
	<option value="18">18</option>
	<option value="19">19</option>
	<option value="20">20</option>
	<option value="21">21</option>
	<option value="22">22</option>
	<option value="23">23</option>
	<option value="24">24</option>
	<option value="25">25</option>
	<option value="26">26</option>
	<option value="27">27</option>
	<option value="28">28</option>
	<option value="29">29</option>
	<option value="30">30</option>
	<option value="31">31</option>
</select>
<select name="<%=this.YearAttributeName%>" id="<%=this.YearAttributeName%>" class="dropDownListStyle" onchange="checkDate()"></select>
<script language="javascript">
    fillYear();
    fillValues();
</script>
