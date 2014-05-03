<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InfoKit.ascx.cs" Inherits="InfoKit" %>

<div>
    <asp:Panel ID="Panel1" runat="server" Width="925px">
        <table cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left" valign="top">
                    <div align="left" 
                        style="background-position: left; padding-left: 25px; background-image: url('images/InfoKit page image.png'); background-repeat: no-repeat; width: 325px; height: 526px;">
                        <table>
                            <tr>
                                <td height="65">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlDownloadFileList" runat="server" Height="400px" 
                                        HorizontalAlign="Left" ScrollBars="Auto" Width="300px">
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                    <div align="left" style="width: 620px;">
                        <asp:Panel ID="Panel2" runat="server" Height="550px" HorizontalAlign="Left" 
                            ScrollBars="Auto" Width="600px">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td ID="tcelPhotoimage" runat="server" colspan="2" rowspan="5" width="100">
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-right-style: groove; border-right-width: 3px; border-right-color: #000000; border-top-style: groove; border-top-width: 3px; border-top-color: #000000;" 
                                                                width="600">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-right-style: groove; border-right-width: 3px; border-right-color: #000000">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-right-style: groove; border-right-width: 3px; border-right-color: #000000">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="border-right-style: groove; border-right-width: 3px; border-right-color: #000000">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="22">
                                                                &nbsp;</td>
                                                            <td ID="tcellInfoKitdetails" runat="server" colspan="2" height="400" 
                                                                style="border-right-style: groove; border-right-width: 3px; border-right-color: #000000; border-bottom-style: groove; border-bottom-width: 3px; border-bottom-color: #000000; border-left-style: groove; border-left-width: 3px; border-left-color: #000000; vertical-align: top; text-align: left;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="22">
                                                                &nbsp;</td>
                                                            <td width="78">
                                                                &nbsp;</td>
                                                            <td width="600">
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>

