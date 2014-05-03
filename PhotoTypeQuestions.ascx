<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PhotoTypeQuestions.ascx.cs" Inherits="PhotoTypeQuestions" %>

<asp:Panel ID="pnlMain" runat="server">

    <div align="left" class="questiondisplaywindowtopspace">
        <table>
            <tr>
                <td align="left">


    <table width="650px">
        <tr>
            <td colspan="2" 
                valign="top">
                <div id="divtitle" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="divInstructions" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
            <table>
        <tr>
            <td width="20">
                &nbsp;</td>
            <td id="ques1" runat="server">
                            <asp:Image ID="imgQuestion1" runat="server" Visible="False" />
                        </td>
        </tr>
        <tr>
            <td width="20" valign="top">
                <asp:Label ID="lblNo1" runat="server"></asp:Label>
            </td>
            <td id="tcellQues1" runat="server">
                <asp:Label ID="lblQues1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20">
                <asp:Label ID="lblQuesID1" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:Image ID="imgQuestion1Sub1" runat="server" 
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td width="20">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA1" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer1" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                        <td>
                            <asp:Image ID="imgQues1Answer1" runat="server" Visible="False"/>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB1" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer2" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                        <td>
                            <asp:Image ID="imgQues1Answer2" runat="server" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC1" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer3" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                        <td>
                            <asp:Image ID="imgQues1Answer3" runat="server" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD1" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer4" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                        <td>
                            <asp:Image ID="imgQues1Answer4" runat="server" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE1" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer5" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                        <td>
                            <asp:Image ID="imgQues1Answer5" runat="server" Visible="False" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
                </td>            
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblmessage" runat="server"></asp:Label>
                <div ID="divProcessSel" align="center">
                        </div>
                                </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                <asp:Button ID="btnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    Text="Go to Previous Page" Width="175px" onclientclick="ShowSelProcess();" />
                        </td>
                        <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Submit and Go to Next Page" onclientclick="ShowSelProcess();" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Timer ID="Timer1" runat="server" Interval="15000" ontick="Timer1_Tick">
                </asp:Timer>
                                </td>
        </tr>
    </table>
                </td>
            </tr>
        </table>
</div>
</asp:Panel>
<div align="center" class="questionpopup">
    <asp:Panel ID="pnlpopup" runat="server" Visible="False" Width="300px">
        <table class="missedquestionidpopup">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblpopup" runat="server" ForeColor="Black"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <div align="center">
                        <asp:Button ID="btnYes" runat="server" onclick="btnYes_Click" Text="Yes" 
                            Width="75px" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnNo" runat="server" Text="No" Width="75px" 
                            onclick="btnNo_Click" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</div>


    <div align="center" class="questionpopup">
        <asp:Panel ID="pnlpopup_timer" runat="server" Visible="False" Width="500px">
            <table style="border: 5px groove #FF0066; width:100%; background-color: #9B9BBD;">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblpopup_timer" runat="server" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div align="center">
                            <table style="width: 200px;">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnYes_timer" runat="server" onclick="btnYes_timer_Click" 
                                            Text="Yes" Visible="False" Width="75px" />
                                        <asp:Button ID="btnYes_timer_Test" runat="server" 
                                            onclick="btnYes_timer_Test_Click" Text="OK" Visible="False" Width="75px" />
                                        <asp:Button ID="btnYes_timer_TestVariable" runat="server" 
                                            onclick="btnYes_timer_TestVariable_Click" Text="Yes" Visible="False" 
                                            Width="75px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnNo_timer" runat="server" onclick="btnNo_timer_Click" 
                                            Text="No" Visible="False" Width="75px" />
                                        <asp:Button ID="btnNo_timer_Test" runat="server" 
                                            onclick="btnNo_timer_Test_Click" Text="No" Visible="False" Width="75px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </div>





