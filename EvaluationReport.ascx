<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EvaluationReport.ascx.cs" Inherits="EvaluationReport" %>

<div align="center" style="padding-top: 10px; ">
    <table>
        <tr>
            <td align="left">
    <table>
        <tr>
            <td colspan="3" 
                valign="top">
                <div class="titleObjective">
                Evaluation Report</div>
            </td>
        </tr>
        <tr>
            <td  colspan="3" style="font-weight: bolder">
                <table width="100%">
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td valign="top">
                            <div style="color: Red;" align="right">Instructions:
                            </div>
                                    </td>
                                    <td>
                                        If your score is equal to or above the cut-off score, you can proceed to the next section. If you haven’t, you have to go back to the training section, go through training again, and then retake the evaluation.</td>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        &nbsp;</td>
                                                    <td height="30" style="font-size: 13px">
                            <span style="color: #0099FF">NOTE:</span> Passing the evaluation test is necessary for proceeding to the next phase of CODE-I.</td>
                                                </tr>
                                            </table>
                                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td  colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" height="150" valign="top">
                <div align="center">
                    <table>
                        <tr>
                            <td align="left" valign="top">
                                <table>
                                    <tr>
                                        <td class="label">
                                            Your Score (%)</td>
                                        <td>
                <asp:TextBox ID="txtScore" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Required Score to Pass (%)</td>
                                        <td>
                <asp:TextBox ID="txtPass" runat="server" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="label">
                                            Result</td>
                                                        <td>
                <asp:TextBox ID="txtResult" runat="server" ReadOnly="True"></asp:TextBox>
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
            <td>
                <asp:Button ID="btnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    Text="Go to Training Page" Width="175px" />
            </td>
            <td align="right">
                <asp:Button ID="btnTraining" runat="server" onclick="btnTraining_Click" 
                    Text="Go to Next Page" />
            </td>
            <td align="right">
                <asp:Button ID="btnExit" runat="server" onclick="btnExit_Click" Text="Exit" 
                    Width="75px" 
                    onclientclick="return confirm('You can break the session. You can resume the session from here, when you login again next time. Do you want to exit?')" />
            </td>
        </tr>
        </table>
            </td>
        </tr>
    </table>
</div>
