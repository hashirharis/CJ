<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChangePasswordControl.ascx.cs" Inherits="ChangePasswordControl" %>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="450px">
    <div align="left" style="text-align: left">
        <table>
            <tr>
                <td>
                    <div class="titlemain">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlPasswordChange" runat="server" Height="200px">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    Current Password</td>
                                <td>
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    New Password</td>
                                <td>
                                    <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Confirm Password</td>
                                <td>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" 
                                        Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                        Text="Change Password" Width="125px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlPasswordMessage" runat="server" Height="200px" 
                        Visible="False">
                        <div style="padding: 50px 25px 25px 25px; text-align: center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#009933" 
                                Text="Updation successfull..."></asp:Label>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
