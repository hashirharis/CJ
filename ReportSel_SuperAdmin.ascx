<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportSel_SuperAdmin.ascx.cs" Inherits="ReportSel_SuperAdmin" %>
<div align="left">
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
                    Report Selection (Super Admin)</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOrganizationList" runat="server" 
                        AppendDataBoundItems="True" AutoPostBack="True" 
                        DataSourceID="LinqOrganizationList" DataTextField="Name" 
                        DataValueField="OrganizationID" 
                        onselectedindexchanged="ddlOrganizationList_SelectedIndexChanged" Width="550px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqOrganizationList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (OrganizationID, Name)" TableName="Organizations" 
                        Where="Status == @Status &amp;&amp; AdminAccess == @AdminAccess">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                            <asp:Parameter DefaultValue="1" Name="AdminAccess" Type="Int32" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
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
                    Group</td>
                <td>
                    <asp:DropDownList ID="ddlUserGroup" runat="server" AppendDataBoundItems="True" 
                        Width="550px" AutoPostBack="True" 
                        onselectedindexchanged="ddlUserGroup_SelectedIndexChanged">
                        <asp:ListItem Value="0">--select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="GroupListSource" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (GroupName, GroupUserID, OrganizationID)" 
                        TableName="GroupUsers">
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    Users List</td>
                <td>
                    <asp:DropDownList ID="ddlUserList" runat="server" 
                        onselectedindexchanged="ddlUserList_SelectedIndexChanged" 
                        AppendDataBoundItems="True" Width="550px">
                        
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqUserList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (UserName, UserId)" TableName="UserProfiles" 
                        Where="FirstLoginDate != @FirstLoginDate">
                        <WhereParameters>
                            <asp:Parameter DefaultValue="NULL" Name="FirstLoginDate" Type="DateTime" />
                        </WhereParameters>
                    </asp:LinqDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    Summary Report Type</td>
                <td>
                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="ddlReportType_SelectedIndexChanged">
                    <asp:ListItem>Interpretative Report</asp:ListItem>
                        <asp:ListItem>Indicative Report</asp:ListItem>
                        <asp:ListItem>Certification Report</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReportCategory" runat="server" Text="Report Category"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReportCategory" runat="server">
                        <asp:ListItem>Variablewise</asp:ListItem>
                        <asp:ListItem>TestSectionwise</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Summary Graph<br />
                    Score Type</td>
                <td >
                    <asp:DropDownList ID="ddlSummaryGraph" runat="server" 
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="Percentage">Percentage (%)</asp:ListItem>
                        <asp:ListItem Value="Percentile">Percentile</asp:ListItem>
                    </asp:DropDownList>
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
