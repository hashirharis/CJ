<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportSel_GroupAdmin.ascx.cs" Inherits="ReportSel_GroupAdmin" %>
<div>
    <asp:Panel ID="Panel1" runat="server">
        <table>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="titlemain">
                    Report Selection (Group Admin)</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Test</td>
                <td>
                    <asp:DropDownList ID="ddlTestList" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlTestList_SelectedIndexChanged" 
                        Width="550px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Users List</td>
                <td>
                    <asp:DropDownList ID="ddlUserList" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlUserList_SelectedIndexChanged" 
                        Width="550px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqUserList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (UserName, UserId, UserType)" TableName="UserProfiles" 
                        
                        Where="UserType != @UserType && UserType != @UserType1 && UserType != @UserType2">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="OrgAdmin" Name="UserType" Type="String" />
                            <asp:Parameter DefaultValue="GrpAdmin" Name="UserType1" Type="String" />
                            <asp:Parameter DefaultValue="SuperAdmin" Name="UserType2" Type="String" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table width="300">
                        <tr>
                            <td>
                                <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" 
                                    Text="Show Report" />
                            </td>
                            <td>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" Width="75px" />
                            </td>
                            <td>
                                <asp:Button ID="btnExit" runat="server" Text="Exit" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
