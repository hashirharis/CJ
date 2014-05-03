<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FillBalnksQues.ascx.cs" Inherits="FillBalnksQues" %>
<asp:Panel ID="pnlMain" runat="server" HorizontalAlign="Center">
<div align="left" class="questiondisplaywindowtopspace">    
   
    <table>
        <tr>
            <td colspan="2" valign="top">
                <table width="650">
                    <tr>
                        <td valign="top">
                            <div ID="divtitle" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div ID="divInstructions" runat="server">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div ID="divProcessSel" align="center">
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel ID="pnlQuestion1" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td valign="top" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="20">
                                <asp:Label ID="lblNo1" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues1" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" width="20">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1Sub1" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" width="20">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID1" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAns1" runat="server" TextMode="MultiLine" Visible="False" 
                                    Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel ID="pnlQuestion2" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion2" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNo2" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues2" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1Sub2" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID2" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAns2" runat="server" TextMode="MultiLine" Visible="False" 
                                    Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel ID="pnlQuestion3" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion3" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNo3" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues3" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1Sub3" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID3" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAns3" runat="server" TextMode="MultiLine" Visible="False" 
                                    Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel ID="pnlQuestion4" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion4" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNo4" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues4" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1Sub4" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID4" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAns4" runat="server" TextMode="MultiLine" Visible="False" 
                                    Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel ID="pnlQuestion5" runat="server" Visible="False">
                    <table>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion5" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="lblNo5" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues5" runat="server">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                <asp:Image ID="imgQuestion1Sub5" runat="server" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID5" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAns5" runat="server" TextMode="MultiLine" Visible="False" 
                                    Width="450px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: middle; text-align: center">
                <asp:Button ID="ptnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    onclientclick="ResetScroll()" Text="Go to Previous Page" Width="175px" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    onclientclick="ResetScroll()" Text="Submit and Go To Next Page" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Timer ID="Timer1" runat="server" Interval="25000" ontick="Timer1_Tick">
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




