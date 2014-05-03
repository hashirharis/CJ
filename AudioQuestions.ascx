<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AudioQuestions.ascx.cs" Inherits="AudioQuestions" EnableViewState="False" %>
<%@ Register Assembly="Media-Player-ASP.NET-Control" TagPrefix="cc" Namespace="Media_Player_ASP.NET_Control" %>

    <div align="left" class="questiondisplaywindowtopspace">



<asp:Timer runat="server" id="UpdateTimer" interval="1000" ontick="UpdateTimer_Tick" 
            Enabled="False" />
        <asp:Panel ID="pnlMain" runat="server">
        
        <table border="2" style="empty-cells: hide" width="100%">
            <tr>
                <td>


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
            <table style="empty-cells: hide">
        <tr>
            <td style="text-align: center">
            <div align="center">
                <asp:UpdatePanel ID="TimedPanel" runat="server" updatemode="Conditional">
                    <ContentTemplate>
                        <table style="width: 650px">
                            <tr>
                                <td style="text-align: center">
                                    <div align="center">
                                        <cc:Media_Player_Control ID="mpPlayer" runat="server" AutoStart="True" 
                                            EnableContextMenu="True" Enabled="False" FullScreen="False" MovieURL="" 
                                            Visible="False" />
                                        <asp:ImageButton ID="imgbtnPlay" runat="server" Height="240px" 
                                            ImageUrl="~/images/featuresPlayButton.gif" onclick="imgbtnPlay_Click" 
                                            Visible="False" Width="320px" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                    </Triggers>
                </asp:UpdatePanel>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width:100%; empty-cells: hide;">
                    <tr>
                        <td>
                            <div align="center">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblInstruction_ques" runat="server" Font-Bold="True" 
                                                Font-Size="12pt" Text="When the audio is complete,  &amp;nbsp;" Visible="False"></asp:Label>
                                            &nbsp;</td>
                                        <td>
                                            <asp:ImageButton ID="imgbtnClickHere" runat="server" 
                                                ImageUrl="~/images/click_here.gif" onclick="imgbtnClickHere_Click" 
                                                Visible="False" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblInstruction_ques2" runat="server" Font-Bold="True" 
                                                Font-Size="12pt" Text="&amp;nbsp; to view the question based on the audio" 
                                                Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td  valign="top">
                <asp:Panel ID="pnlQuestion" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                    <table width="100%">
                        <tr>
                            <td valign="top" width="20">
                                &nbsp;</td>
                            <td valign="top" width="20">
                                <asp:Label ID="lblNo1" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues1" runat="server">
                                <asp:Label ID="lblQues1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblQuesID1" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <table>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle" valign="top">
                                            <asp:Label ID="lblA1" runat="server" Text="A" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: middle" valign="top">
                                            <asp:RadioButton ID="rbQues1Answer1" runat="server" GroupName="Q1" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle" valign="top">
                                            <asp:Label ID="lblB1" runat="server" Text="B" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: middle" valign="top">
                                            <asp:RadioButton ID="rbQues1Answer2" runat="server" GroupName="Q1" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle" valign="top">
                                            <asp:Label ID="lblC1" runat="server" Text="C" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: middle" valign="top">
                                            <asp:RadioButton ID="rbQues1Answer3" runat="server" GroupName="Q1" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle" valign="top">
                                            <asp:Label ID="lblD1" runat="server" Text="D" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: middle" valign="top">
                                            <asp:RadioButton ID="rbQues1Answer4" runat="server" GroupName="Q1" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center; vertical-align: middle" valign="top">
                                            <asp:Label ID="lblE1" runat="server" Text="E" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: left; vertical-align: middle" valign="top">
                                            <asp:RadioButton ID="rbQues1Answer5" runat="server" GroupName="Q1" 
                                                Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>

        </table>
                </td>            
        </tr>
        <tr>
            <td align="right" colspan="2">
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="2">
                <div ID="divProcessSel" align="center">
                        </div>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right" style="text-align: right">
                <asp:Button ID="btnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    Text="Go to Previous Page" Width="175px" onclientclick="ShowSelProcess();" />
                        </td>
                        <td align="left" style="text-align: left">
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Submit and Go to Next Page" Enabled="False" onclientclick="ShowSelProcess();" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Timer ID="Timer1" runat="server" Enabled="False" 
                    ontick="Timer1_Tick">
                </asp:Timer>
            </td>
            <td align="left">
                <asp:Label ID="lblmessage" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
                </td>
            </tr>
        </table></asp:Panel>
</div>

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


<p>
    &nbsp;</p>

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


