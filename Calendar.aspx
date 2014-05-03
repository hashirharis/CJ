<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendar</title>
</head>
<script type="text/javascript">
function SelectDate(sDATE,sControl)
{
	if ( window.opener != null && window.opener.ChangeDate != null )
	{
		window.opener.ChangeDate(sDATE,sControl);
		window.close();
	}
	else
	{
		alert('Original window has closed.  Date cannot be set.');
	}
}


</script>
<body>
    <form id="form1" runat="server" style="vertical-align:top">
    <div style="vertical-align:top">
   
<table id="tbl_control" cellSpacing="0" cellPadding="0" border="0">
	<tr>
		<td> 
            <asp:Calendar ID="Calendar1" runat="server" 
                onselectionchanged="Calendar1_SelectionChanged" BackColor="White" 
                BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                ForeColor="#003399" Height="200px" Width="220px">
                <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                <WeekendDayStyle BackColor="#CCCCFF" />
                <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                    Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
            </asp:Calendar>
          
        </td>
	</tr>
</table>
    
    </div>
    </form>
</body>
</html>
