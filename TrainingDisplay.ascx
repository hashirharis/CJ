<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TrainingDisplay.ascx.cs" Inherits="TrainingDisplay" %>

<div align="center" style="padding-top: 10px; ">
    <table>
        <tr>
            <td align="left" valign="top">
    <table style=" height: 350px;">
        <tr>
            <td align="left" colspan="3" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                    <div id="container" runat="server">
                    </div>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="3" valign="top">
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <table style="width:100%;">
                        <tr>
                            <td id="tcellDesc1" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td id="tcellVideoplayer" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton ID="lbtnreplay" runat="server" Font-Underline="False" 
                    ForeColor="Blue" onclick="lbtnreplay_Click" Visible="False">Replay</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td id="tcellDesc2" runat="server">
                                &nbsp;</td>
                        </tr>
                    </table>
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
                <asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" 
                    Text="Exit" Width="75px" 
                    onclientclick="return confirm('You can break the session. You can resume the session from here, when you login again next time. Do you want to exit?')" />
                                </td>
        </tr>
    </table>
            </td>
        </tr>
    </table>
</div>

