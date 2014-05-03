<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectiveQuestns.ascx.cs" Inherits="ObjectiveQuestns" EnableViewState="False" %>


    <div align="left" class="questiondisplaywindowtopspace">
    <asp:Panel ID="pnlMain" runat="server">
           
        <table>
            
            <tr>
                <td align="left">

    <table width="650px">
        <tr>
            <td 
                valign="top">
                <div id="divtitle" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divInstructions" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div ID="divProcessSel" align="center">
                        </div>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
            <table style="empty-cells: hide">
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlQuestion1" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                     <table>
        <tr>
            <td width="20" valign="top">
                <asp:Label ID="lblNo1" runat="server"></asp:Label>
            </td>
            <td id="tcellQues1" runat="server">
                <asp:Label ID="lblQues1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label ID="lblQuesID1" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA1" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer1" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB1" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer2" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC1" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer3" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD1" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer4" runat="server" GroupName="Q1" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE1" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer5" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblF1" runat="server" Text="F" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer6" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblG1" runat="server" Text="G" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer7" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblH1" runat="server" Text="H" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer8" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblI1" runat="server" Text="I" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer9" runat="server" GroupName="Q1" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblJ1" runat="server" Text="J" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues1Answer10" runat="server" GroupName="Q1" 
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
       
        <tr>
            <td valign="top" colspan="2">
                <asp:Panel ID="pnlQuestion2" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                      <table>
        <tr>
            <td valign="top">
                <asp:Label ID="lblNo2" runat="server"></asp:Label>
            </td>
            <td id="tcellQues2" runat="server">
                <asp:Label ID="lblQues2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuesID2" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA2" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer1" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB2" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer2" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC2" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer3" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD2" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer4" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE2" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer5" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblF2" runat="server" Text="F" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer6" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblG2" runat="server" Text="G" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer7" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblH2" runat="server" Text="H" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer8" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblI2" runat="server" Text="I" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer9" runat="server" GroupName="Q2" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblJ2" runat="server" Text="J" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues2Answer10" runat="server" GroupName="Q2" 
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
      
        <tr>
            <td valign="top" colspan="2">
                <asp:Panel ID="pnlQuestion3" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                      <table>
        <tr>
            <td valign="top">
                <asp:Label ID="lblNo3" runat="server"></asp:Label>
            </td>
            <td id="tcellQues3" runat="server">
                <asp:Label ID="lblQues3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuesID3" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA3" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer1" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB3" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer2" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC3" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer3" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD3" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer4" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE3" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer5" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblF3" runat="server" Text="F" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer6" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblG3" runat="server" Text="G" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer7" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblH3" runat="server" Text="H" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer8" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblI3" runat="server" Text="I" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer9" runat="server" GroupName="Q3" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblJ3" runat="server" Text="J" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues3Answer10" runat="server" GroupName="Q3" 
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
      
        <tr>
            <td valign="top" colspan="2" >
                <asp:Panel ID="pnlQuestion4" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                      <table>
        <tr>
            <td valign="top" >
                <asp:Label ID="lblNo4" runat="server"></asp:Label>
            </td>
            <td id="tcellQues4" runat="server">
                <asp:Label ID="lblQues4" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQuesID4" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA4" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer1" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB4" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer2" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC4" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer3" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD4" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer4" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE4" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer5" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblF4" runat="server" Text="F" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer6" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblG4" runat="server" Text="G" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer7" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblH4" runat="server" Text="H" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer8" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblI4" runat="server" Text="I" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer9" runat="server" GroupName="Q4" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblJ4" runat="server" Text="J" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues4Answer10" runat="server" GroupName="Q4" 
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
      
        <tr>
            <td valign="top" colspan="2">
                <asp:Panel ID="pnlQuestion5" runat="server" HorizontalAlign="Left" 
                    Visible="False">
                     <table>
        <tr>
            <td valign="top">
                <asp:Label ID="lblNo5" runat="server"></asp:Label>
            </td>
            <td id="tcellQues5" runat="server">
                <asp:Label ID="lblQues5" runat="server"></asp:Label>
            </td>
        </tr>
       
        <tr>
            <td >
                <asp:Label ID="lblQuesID5" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                <table>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblA5" runat="server" Text="A" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer1" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblB5" runat="server" Text="B" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer2" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblC5" runat="server" Text="C" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer3" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblD5" runat="server" Text="D" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer4" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblE5" runat="server" Text="E" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer5" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblF5" runat="server" Text="F" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer6" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblG5" runat="server" Text="G" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer7" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblH5" runat="server" Text="H" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer8" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblI5" runat="server" Text="I" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer9" runat="server" GroupName="Q5" 
                                Visible="False" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td valign="top" style="text-align: center; vertical-align: middle">
                            <asp:Label ID="lblJ5" runat="server" Text="J" Visible="False"></asp:Label>
                        </td>
                        <td valign="top" style="text-align: left; vertical-align: middle">
                            <asp:RadioButton ID="rbQues5Answer10" runat="server" GroupName="Q5" 
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
        
                </td>            
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblmessage" runat="server"></asp:Label>
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
                    Text="Go to Previous Page" Width="175px" onclientclick="ResetScroll()" />
                        </td>
                        <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Submit and Go to Next Page" onclientclick="ResetScroll()" />
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
                <div style="position: absolute; z-index: 1; width: 350px; height: 200px; left: 30%; bottom: 30%;" 
                    id="divPopMessage">
                    <asp:Panel ID="pnlMessage" runat="server" Visible="False" Width="350px">
                        <table style="width:100%;" bgcolor="#FFCC99">
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        You have completed this section.
                                        <br />
                                        Are you sure You want to go to next section?</div>
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
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnNoExit" runat="server" onclick="btnNoExit_Click" 
                                            Text="No" Width="75px" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                                            Text="Cancel" Visible="False" Width="75px" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel ID="pnlMessage_confirm" runat="server" Visible="False" Width="350px">
                <table style="width:100%;" bgcolor="#FF99CC">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center">
                                If You proceed you cant access this section later.
                                <br />
                                Are you sure You want to proceed to next section?</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center">
                                <asp:Button ID="btnYes_confirm" runat="server" onclick="btnYes_confirm_Click" 
                                    Text="Yes" Width="75px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnNoExit0" runat="server" onclick="btnNoExit_Click" Text="No" 
                                    Width="75px" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel_confirm" runat="server" 
                                    onclick="btnCancel_confirm_Click" Text="Cancel" Width="75px" 
                                    Visible="False" />
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
                                <asp:GridView ID="GridView1" runat="server" 
                    AutoGenerateColumns="False" Visible="False">
                                    <Columns>
                                        <asp:BoundField DataField="SectionName" HeaderText="SectionName" 
                                            ReadOnly="True" SortExpression="SectionName" />
                                        <asp:BoundField DataField="SectionNameSub1" HeaderText="SectionNameSub1" 
                                            ReadOnly="True" SortExpression="SectionNameSub1" />
                                        <asp:BoundField DataField="TestId" HeaderText="TestId" ReadOnly="True" 
                                            SortExpression="TestId" />
                                        <asp:BoundField DataField="TestSectionId" HeaderText="TestSectionId" 
                                            ReadOnly="True" SortExpression="TestSectionId" />
                                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqQuestionCount" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (SectionName, SectionNameSub1, TestId, TestSectionId)" 
                    TableName="QuestionCounts">
                </asp:LinqDataSource>
                                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Timer ID="Timer1" runat="server" Interval="10000" ontick="Timer1_Tick">
                </asp:Timer>
                                            <asp:GridView ID="GridView2" runat="server">
                </asp:GridView>
                                            </td>
        </tr>
    </table>
                </td>
            </tr>
        </table>
        </asp:Panel>
</div>


       <%-- </div>--%>
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
                                <asp:Button ID="btnYespopup" runat="server" onclick="btnYespopup_Click" 
                                    Text="Yes" Width="75px" />
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


       <%-- </div>--%>

    <div align="center" class="questionpopup" id="sessiontimeoutmsglayer">
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





       <%-- </div>--%>

    <div align="center" class="questionpopup">
        <asp:Panel ID="pnlpopup_previous" runat="server" Visible="False" Width="350px">
            <table style="border: 5px groove #FF0066; width:100%; background-color: #9B9BBD;">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblpopup_previous" runat="server" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div align="center">
                            <asp:Button ID="btnOk_Previous" runat="server" Text="OK" 
                                Width="75px" onclick="btnOk_Previous_Click" />
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





