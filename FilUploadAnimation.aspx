<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FilUploadAnimation.aspx.cs" Inherits="FilUploadAnimation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<script type="text/javascript">
function SelectImage(sDATE,sControl)
{
	if ( window.opener != null && window.opener.ChangeFile != null )
	{
		window.opener.ChangeFile(sDATE,sControl);
		window.close();
	}
	else
	{
		alert('Original window has closed.  Date cannot be set.');
	}
}


</script>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
    
    </div>
    <asp:Panel ID="Panel1" runat="server">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Add" />
    </asp:Panel>
    </form>
</body>
</html>
