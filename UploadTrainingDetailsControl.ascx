<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadTrainingDetailsControl.ascx.cs" Inherits="UploadTrainingDetailsControl" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>

<script type="text/javascript" src="FJAJScript.js"></script>
<script type="text/javascript"><!--
 
window.onload = limitImages;
function setLimit(imgElem,prop,lim)
{
	for(var i=0,limit=document.images.length; i < limit; ++i)
	{
		if(imgElem==document.images[i])
		{
			if( lim < document.images[i][prop] )
			{
				imgElem[prop]=lim;
			}
			break;
		}
	}
}
function limitImages()
{
	var widthLimit=50;
	var heightLimit=50;
	var imgs = document.getElementsByTagName("img");
 
	for( var i=0, limit=imgs.length; i < limit; ++i)
	{
		if( -1 < imgs[i].parentNode.className.indexOf("container") )
		{
			setLimit(imgs[i],"width",50);
			setLimit(imgs[i],"height",50);
		}
	}
}
//--></script>


<div align="left">
    <table>
        <tr>
            <td valign="top" align="left">
                <div id="container" align="left">
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                <table>
                                    <tr>
                                        <td colspan="2" valign="top" 
                                            style="font-weight: bold; background-image: url('file:///F:/Bipson/LIVEPROJECTS_FROM_23-05-09/FJAApp2009/images/ControlHeader_bg.gif'); background-repeat: no-repeat; padding-top: 10px; padding-left: 50px; width: 400px; height: 45px;">
                                            Training details entry form</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblMesssage" runat="server" ForeColor="#FF3300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Training Type:</td>
                                        <td>
                                            <asp:DropDownList ID="ddlIntroductioncategories" runat="server" 
                                                AppendDataBoundItems="True" AutoPostBack="True" onchange="ShowSelProcess();"
                                                onselectedindexchanged="ddlIntroductioncategories_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--select--</asp:ListItem>
                                                <asp:ListItem Value="1">Introduction</asp:ListItem>
                                                <asp:ListItem Value="2">Important Facts</asp:ListItem>
                                                <asp:ListItem Value="3">Key Terms and Concepts</asp:ListItem>
                                                <asp:ListItem Value="4">Task Entry Format</asp:ListItem>
                                                <asp:ListItem Value="5">Subtask/Activity Entry Format</asp:ListItem>
                                                <asp:ListItem Value="6">Self Rating and Report</asp:ListItem>
                                                <asp:ListItem Value="7">Introduction To Questionaire</asp:ListItem>
                                            </asp:DropDownList>
                                            <div align="center">
                                            <asp:Panel ID="pnlProcessingSel" runat="server">
                                                <div ID="divProcessSel"  align="center">
                                                   </div>
                                            </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Section 1</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox1" runat="Server" Height="200px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print" 
                                                ToolbarStyleConfiguration="NotSet" UpdateToolbar="True"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Image1</td>
                                        <td id="imgcell1" height="100">
                                            <img id="Image1" runat="server" alt="" src="" class="mainimageEdit" /></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            &nbsp;</td>
                                        <td>
                                            <input id="btnBrowse1" onclick="DispImageInCell('imgcell1',1)" type="button" 
                                                value="Browse for image .." /><asp:Button ID="btnImageDel1" runat="server" 
                                                onclick="btnImageDel1_Click" Text="Delete Image" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Section 2</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox2" runat="Server" Height="150px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print" 
                                                ToolbarStyleConfiguration="NotSet"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Image2</td>
                                        <td id="imgcell2" height="100">
                                            <img id="Image2" runat="server" alt="" src="" class="mainimageEdit" /></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            &nbsp;</td>
                                        <td id="imgcell6">
                                            <input id="btnBrowse2" onclick="DispImageInCell('imgcell2',2)" type="button" 
                                                value="Browse for image .." /><asp:Button ID="btnImageDel2" runat="server" 
                                                onclick="btnImageDel2_Click" Text="Delete Image" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Section 3</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox3" runat="Server" Height="150px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print" 
                                                ToolbarStyleConfiguration="NotSet"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Image 3</td>
                                        <td id="imgcell3" height="100">
                                            <img id="Image3" runat="server" alt="" src="" class="mainimageEdit" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <input id="btnBrowse3" onclick="DispImageInCell('imgcell3',3)" type="button" 
                                                value="Browse for image .." /><asp:Button ID="btnImageDel3" runat="server" 
                                                onclick="btnImageDel3_Click" Text="Delete Image" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Section 4</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox4" runat="Server" Height="150px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print" 
                                                ToolbarStyleConfiguration="NotSet"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 4</td>
                                        <td id="imgcell4" height="100">
                                            <img id="Image4" runat="server" alt="" src="" class="mainimageEdit" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <input id="btnBrowse4" onclick="DispImageInCell('imgcell4',4)" type="button" 
                                                value="Browse for image .." /><asp:Button ID="btnImageDel4" runat="server" 
                                                onclick="btnImageDel4_Click" Text="Delete Image" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Section 5</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox5" runat="Server" Height="150px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 5</td>
                                        <td id="imgcell5" height="100">
                                            <img id="Image5" runat="server" alt="" src="" class="mainimageEdit" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <input id="btnBrowse5" onclick="DispImageInCell('imgcell5',5)" type="button" 
                                                value="Browse for image .." /><asp:Button ID="btnImageDel5" runat="server" 
                                                onclick="btnImageDel5_Click" Text="Delete Image" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <b>Video File Details</b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Section 6(Video)</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox6" runat="Server" Height="100px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Video File:
                                        </td>
                                        <td>
                                            <input id="txtVideoFile" runat="server" readonly="readonly" type="text" 
                                                style="width: 200px;" /><input 
                                                id="btnBrowseVideo" onclick="dispAnimationFile('txtVideoFile')" type="button" 
                                                value="Browse Video File ..." /><asp:Button ID="btnImageDel6" 
                                                runat="server" onclick="btnImageDel6_Click" Text="Delete VideoFile" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Section 7(Video)</td>
                                        <td>
                                            <FTB:FreeTextBox ID="FreeTextBox7" runat="Server" Height="100px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;InsertRule|Cut,Copy,Paste;Undo,Redo,Print"></FTB:FreeTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" OnClientClick="ShowProcess();"
                                                Text="Submit" Width="100px" />
                                            <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                                                Text="Reset" Width="100px" />
                                            <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                                Text="Delete" Width="100px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <div align="center">
                                            <asp:Panel ID="pnlProcessing" runat="server">
                                                <div ID="divProcess"  align="center">
                                                   </div>
                                            </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</div>
