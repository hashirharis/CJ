<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportAccess.ascx.cs" Inherits="ReportAccess" %>

<div align="left" style="padding-top: 10px">
<table bgcolor="Black" border="2">
    <tr>
        <td align="left" valign="top">

    <table bgcolor="White" style="width: 500px; height: 350px">
        <tr>
            <td height="10" valign="top" width="25">
                &nbsp;</td>
            <td height="10" valign="top" colspan="4">
                <div class="titlemain">
                Report Access</div>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="top" colspan="4">
                <asp:Panel ID="pnlOrganization" runat="server" HorizontalAlign="Left" 
                    Width="100%">
                    <table style="width:100%;">
                        <tr>
                            <td valign="bottom">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Organisation List"></asp:Label>
                            </td>
                            <td valign="bottom" width="75px">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Status"></asp:Label>
                            </td>
                            <td colspan="2" width="150px">
                                <asp:Label ID="Label5" runat="server" 
                                    Text="Click on below button to add report access to all the users under selected Organisation."></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlOrganisation" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" DataTextField="Name" 
                                    DataValueField="OrganizationID" 
                                    onselectedindexchanged="ddlOrganisation_SelectedIndexChanged" Width="100%">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqOrganisation" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (OrganizationID, Name)" TableName="Organizations" 
                                    Where="AdminAccess == @AdminAccess &amp;&amp; Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="AdminAccess" Type="Int32" />
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStaus_Org" runat="server">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnOrganisation" runat="server" onclick="btnOrganisation_Click" 
                                    Text="Submit" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold; width: 190px;">
                Test List</td>
            <td height="10" valign="bottom" style="font-weight: bold" width="75px;">
                Status</td>
            <td height="10" valign="top" colspan="2" width="200px;">
                <asp:Label ID="Label6" runat="server" 
                    Text="Click on below button to add report access to all the users under selected Test."></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold; width: 190px;">
                <asp:DropDownList ID="ddlTestList" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" onselectedindexchanged="ddlTestList_SelectedIndexChanged" 
                    Width="100%">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                            </td>
            <td height="10" valign="bottom" style="font-weight: bold" width="75px;">
                                <asp:DropDownList ID="ddlStaus_Test" runat="server">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
            <td height="10" valign="top" colspan="2" width="200px;">
                <asp:Button ID="btnTestList" runat="server" onclick="btnTestList_Click" 
                    Text="Submit" />
                            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold; width: 190px;">
                <asp:Label ID="Label2" runat="server" Text="Group List"></asp:Label>
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold" width="75px;">
                Status</td>
            <td height="10" valign="top" colspan="2" width="200px;">
                <asp:Label ID="Label7" runat="server" 
                    Text="Click on below button to add report access to all the users under selected Group."></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:DropDownList ID="ddlGroup" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" DataTextField="GroupName" DataValueField="GroupUserID" 
                    onselectedindexchanged="ddlGroup_SelectedIndexChanged" Width="100%">
                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqGroup" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (GroupUserID, GroupName)" TableName="GroupUsers">
                </asp:LinqDataSource>
            </td>
            <td height="10" valign="top">
                <asp:DropDownList ID="ddlStaus_Grp" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">InActive</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:Button ID="btnGroup" runat="server" onclick="btnGroup_Click" 
                    Text="Submit" />
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold">
                User search</td>
            <td height="10" valign="bottom" style="font-weight: bold">
                &nbsp;</td>
            <td height="10" valign="top" colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold" colspan="2">
                <asp:TextBox ID="txtUserSearch" runat="server" Width="100%"></asp:TextBox>
                </td>
            <td height="10" valign="top" colspan="2">
                <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" 
                    Text="Search" Font-Bold="True" />
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="bottom" style="font-weight: bold">
                <asp:Label ID="Label3" runat="server" Text="User List"></asp:Label>
                </td>
            <td height="10" valign="bottom" style="font-weight: bold">
                Status</td>
            <td height="10" valign="top" colspan="2">
                <asp:Label ID="Label8" runat="server" 
                    Text="Click on below button to add report access to selected users."></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top" class="label">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:GridView ID="gvwUserList" runat="server" AutoGenerateColumns="False" 
                    AllowPaging="True" onpageindexchanging="gvwUserList_PageIndexChanging" 
                    PageSize="25">
                    <Columns>
                    <asp:TemplateField>
                                            <AlternatingItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </AlternatingItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                                        </asp:TemplateField>
                        <asp:BoundField DataField="UserName" HeaderText="UserName" ReadOnly="True" 
                            SortExpression="UserName" />
                        <asp:BoundField DataField="ReportAccess" HeaderText="Report Access" 
                            ReadOnly="True" SortExpression="ReportAccess" />
                        <asp:BoundField DataField="UserId" ReadOnly="True" ShowHeader="False" 
                            SortExpression="UserId" >
                            <ItemStyle Font-Size="1pt" ForeColor="White" Width="1px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqUserList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (UserName, ReportAccess, UserId)" TableName="UserProfiles" 
                    Where="Status == @Status">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
            <td height="10" valign="top">
                <asp:DropDownList ID="ddlStaus_User" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">InActive</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:Button ID="btnUser" runat="server" onclick="btnUser_Click" Text="Submit" />
            </td>
        </tr>
        <tr>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top" colspan="4">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
        </tr>
        <tr>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                    Text="Reset" />
            </td>
            <td height="10" valign="top">
                &nbsp;</td>
            <td height="10" valign="top">
                &nbsp;</td>
        </tr>
        </table>


        </td>
    </tr>
    
</table>
</div>