<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DownloadRefDataToExcel.ascx.cs" Inherits="DownloadRefDataToExcel" %>
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
                    Download Refference data</td>
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
                        Width="550px" onchange="ShowProcess();">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    <div ID="divProcess">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnShow" runat="server" onclick="btnShow_Click" 
                        Text="Export To Excell" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="Panel2" runat="server" Height="550px" ScrollBars="Auto" 
                        Width="850px">
                        <asp:GridView ID="GridView1" runat="server">
                        </asp:GridView>
                    </asp:Panel>
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
                    <table width="300">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
