<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserTrainingControl.ascx.cs" Inherits="UserTrainingControl" %>


<link rel="Stylesheet" type="text/css" href="FJAStyles.css"  />


<div align="center" style="padding-top: 10px;">
    <table>
        <tr>
            <td align="left" valign="top">
                <table style="width:755px; ">
                    <tr>
                        <td 
                            valign="top">
                            <div class="titleCodeI">
                                CODE-I Training </div>
                        </td>
                    </tr>
                    <tr>
                        <td 
                            valign="top">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="justify" 
                            
                            
                            
                            style="height: 425px; background-image: url('images/Code-I_Training.png'); background-repeat: no-repeat; padding-top: 5px; padding-left: 30px;">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">
                            <table style="width:100%;">
                                <tr>
                                    <td>
                <asp:Button ID="ptnPrevious" runat="server" onclick="ptnPrevious_Click" 
                    Text="Back to Previous section" Width="175px" />
                                    </td>
                                    <td>
            <asp:Button ID="btnProceed" runat="server" onclick="btnProceed_Click" 
                Text="Go to next section" />
                                    </td>
                                    <td>
                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                    Text="Exit" Width="75px" 
                                            onclientclick="return confirm('You can break the session. You can resume the session from here, when you login again next time. Do you want to exit?')" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        </table>
</div>

