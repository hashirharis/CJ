<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WelcomePage.ascx.cs" Inherits="WelcomePage" %>

<link rel="Stylesheet" type="text/css" href="FJAStyles.css"  />

<div align="center" style="padding-top: 10px;">
    <table>
        <tr>
            <td align="left" valign="top">
                <table style="width:650px; ">
                    <tr>
                        <td 
                            valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td 
                            valign="top">
                            <div class="titlewelcome">
                                                            <table style="width:100%;">
                                                                <tr>
                                                                    <td>
                                                                        <img alt="" src="images/Take%20a%20test%20image%20Big.jpg" /></td>
                                                                    <td>
                                                                        <table style="width:100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <img alt="" src="images/click%20here%20to%20take%20your%20test.jpg" /></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <div align="center">
                                                                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                                                                            ImageUrl="~/images/Test page Go Button.jpg" onclick="ImageButton1_Click" />
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</div>