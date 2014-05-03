<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WordTypeMemmoryQuestions.ascx.cs" Inherits="WordTypeMemmoryQuestions" %>

<asp:Timer runat="server" id="UpdateTimer" interval="1000" 
    ontick="UpdateTimer_Tick" Enabled="False" />
    
<asp:Panel ID="pnlMain" runat="server">

    <div class="questiondisplaywindowtopspace">
        <table border="2">
            <tr>
                <td>

    <table width="650px" style="height: 425px">
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
                <div align="center">
                    <asp:Panel ID="pnlClickHere" runat="server" 
                        Visible="False" Width="450px">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                                        Text="When you are ready"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Italic="True" 
                                        ForeColor="Gray" Text="click the button below"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                                        Text="to start the display"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClickHere" runat="server" 
                                        ImageUrl="~/images/click_here.gif" onclick="imgbtnClickHere_Click" 
                                        Visible="False" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Panel ID="pnlWordDisplay" runat="server" 
                                        Visible="False" Width="650px">
                <div align="center" style="width: 650px">
                   <%-- <asp:Panel ID="Panel1" runat="server">--%>
                        <asp:UpdatePanel ID="TimedPanel" runat="server" updatemode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger controlid="UpdateTimer" eventname="Tick" />
                            </Triggers>
                            <ContentTemplate>
                                <%--<div align="center">style="width: 100%"--%>
                                    
                                        <table ID="tblWordDisplay" runat="server" >
                                            <tr>
                                                <td ID="tcelWordDisplay" runat="server" 
                                                    
                                                    style="padding: 15px; border: 5px ridge #0000FF; font-weight: bold; text-align: center; ">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                   
                               <%-- </div>--%> 
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    <%--</asp:Panel>--%>
                </div></asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlQuestion" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                    <table>
                        <tr>
                            <td valign="top" width="20">
                                <asp:Label ID="lblNo1" runat="server"></asp:Label>
                            </td>
                            <td ID="tcellQues1" runat="server">
                                <asp:Label ID="lblQues1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuesID1" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblA" runat="server" Text="A" Visible="False"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbOption1" runat="server" GroupName="wt" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblB" runat="server" Visible="False">B</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbOption2" runat="server" GroupName="wt" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblC" runat="server" Visible="False">C</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbOption3" runat="server" GroupName="wt" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblD" runat="server" Visible="False">D</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbOption4" runat="server" GroupName="wt" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblE" runat="server" Visible="False">E</asp:Label>
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rbOption5" runat="server" GroupName="wt" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
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
            <td align="left" colspan="2" height="20">
                <div align="center">
                    <table style="width:50%;">
                        <tr>
                            <td>
                                <asp:Button ID="btnPrevious" runat="server" onclick="ptnPrevious_Click" 
                                    onclientclick="ShowSelProcess();" Text="Go to Previous Page" Width="175px" />
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Enabled="False" 
                                    onclick="btnSubmit_Click" onclientclick="ShowSelProcess();" 
                                    Text="Submit and Go to Next Page" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="30000" 
                    ontick="Timer1_Tick">
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





