<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestIntroductionControl.ascx.cs" Inherits="TestIntroductionControl" %>

<div align="center" style="padding-top: 10px; ">
    <table width="650">
        <tr>
            <td align="left" valign="top">
    <table>
        <tr>
            <td align="left" colspan="3" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                    <div id="container" runat="server" style="padding:5px;">
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" valign="bottom">
                    <asp:Panel ID="pnlProcessingSel" runat="server">
                        <div ID="divProcessSel" align="center">
                        </div>
                    </asp:Panel>
                </td>
        </tr>
        <tr valign="bottom" style="height: 20px">
            <td align="right" height="20">
                <asp:Button ID="btnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    Text="Go to Previous Page" onclientclick="ShowSelProcess();" />
            </td>
            <td align="right" height="20">
                <asp:Button ID="btnProceed" runat="server" onclick="btnProceed_Click" 
                    Text="Go to next Page" onclientclick="ShowSelProcess();" />
            </td>
            <td align="right">
                &nbsp;</td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</div>

