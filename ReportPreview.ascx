<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportPreview.ascx.cs" Inherits="ReportPreview" %>
<div align="left">
    <asp:Panel ID="Panel1" runat="server">
        <table align="left" width="800">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td ID="tcellReportTitle" runat="server" 
                                style="text-align: center; vertical-align: middle" colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="color: #76923C; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 20px; text-align: center; vertical-align: middle;" 
                                colspan="4">
                                Common IT Aptitude Test Report of&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblName" runat="server" ForeColor="Blue"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" 
                                style="color: #76923C; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; text-align: left; vertical-align: middle;">
                                Developed by Career Judge: Center For Career Profiling and Development</td>
                        </tr>
                        <tr>
                            <td colspan="4" 
                                style="color: #76923C; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px; text-align: left; vertical-align: middle;">
                                Dated :
                                <asp:Label ID="lblReportDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center; vertical-align: middle; color: #FFFF00; font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 20px;" 
                                colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td runat="server" style="text-align: center; vertical-align: middle;" 
                                colspan="4">
                                <div>
                                    <table>
                                        <tr>
                                            <td ID="tcellUserDetails" runat="server" 
                                                style="text-align: left; vertical-align: middle">
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td ID="tcellDescription1" runat="server" colspan="4" 
                                style="padding: 5px 15px 5px 15px; text-align: justify;">
                                <asp:Label ID="lblSummary1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgGraph" runat="server" />
                                            </td>
                                            <td ID="tcelGraphHelp" runat="server" 
                                                style="text-align: left; vertical-align: middle">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" ID="tcellDescription2" 
                                style="padding: 5px 25px 5px 30px; text-align: justify;" colspan="4">
                                <asp:Label ID="lblSummary2" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td ID="tcellDescriptionReport" runat="server" colspan="4" 
                                style="padding: 5px 25px 5px 30px; text-align: justify;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td ID="tcellBarGraph" runat="server" colspan="4" 
                                style="padding: 5px 25px 5px 30px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td runat="server" colspan="4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td ID="tcellConclution" runat="server" colspan="4" 
                                style="padding: 5px 15px 5px 15px; text-align: justify;">
                                <asp:Label ID="lblConclusion" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView1" runat="server" Visible="False">
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" Visible="False">
                                </asp:GridView>
                            </td>
                            <td>
                                <asp:GridView ID="GridView3" runat="server" Visible="False">
                                </asp:GridView>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtValues" runat="server" Width="402px" Visible="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtParts" runat="server" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
