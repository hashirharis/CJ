<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportExcelFile.aspx.cs" Inherits="ExportExcelFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Browse a file</title>
    <script type="text/JavaScript" src="FJAJScript.js"></script>
</head>
<script type="text/javascript">
function SetExcelFile(sDATE,sControl)
{
	if ( window.opener != null && window.opener.ChangeFile != null )
	{
		window.opener.GetScoreFile(sDATE,sControl);
		window.close();
	}
	else
	{
		alert('Original window has closed.  Date cannot be set.');
	}
}


</script>

<body style="background-repeat: repeat; background-image: url('images/popup_bg.jpg')">
    <form id="form1" runat="server">
    <table width="250"><tr><td>
    <div>
    
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    
    </div>
   </td> </tr><tr><td>
            &nbsp;</td> </tr><tr><td>
    <asp:Panel ID="pnlProcessingSel" runat="server" Width="200px">
                        <div id="divProcessSel" align="left">
                        </div>
                    </asp:Panel></td> </tr><tr><td>
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Right">
        <asp:FileUpload ID="FileUpload1" runat="server" />
    </asp:Panel>
    </td></tr><tr><td>
            &nbsp;</td></tr><tr><td>
            &nbsp;</td></tr><tr><td>
                <div align="center">
        <asp:Button ID="Button1" runat="server" OnClientClick="ShowSelProcess1();" 
            onclick="Button1_Click" Text="Add" Width="75px" />
                </div>
    </td></tr></table>
    </form>
</body>
</html>
