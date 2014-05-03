<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Group_User.ascx.cs" Inherits="Group_User" %>
<link rel="stylesheet" type="text/css" href="FJAStyles.css" />
<div align="left" style="padding-top: 10px">
    <table>
        <tr>
            <td align="left" valign="top">
    
          <table align="left" style="height: 350px">
                <tr>
                    <td colspan="3" 
                        valign="top">
                        <div class="titlemain">
                         User Group</div>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        <asp:Label ID="lblOrganization" runat="server" Text="Organization:"></asp:Label>
                    </td>
                    <td colspan="2" style="color: #FF3300">
                        <asp:DropDownList ID="ddlOrganization" runat="server" 
                            AppendDataBoundItems="True" DataSourceID="LinqOrganization" 
                            DataTextField="Name" DataValueField="OrganizationID" Width="250px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:LinqDataSource ID="LinqOrganization" runat="server" 
                            ContextTypeName="AssesmentDataClassesDataContext" 
                            Select="new (Name, OrganizationID, Status)" TableName="Organizations" 
                            Where="Status == @Status &amp;&amp; AdminAccess == @AdminAccess">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                <asp:Parameter DefaultValue="1" Name="AdminAccess" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                        *</td>
                </tr>
                <tr>
                    <td class="label">
                        Vocation:</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlJobCategory" runat="server" 
                            AppendDataBoundItems="True" DataSourceID="LinqJobCategory" DataTextField="Name" 
                            DataValueField="JobCatID" Width="250px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:LinqDataSource ID="LinqJobCategory" runat="server" 
                            ContextTypeName="AssesmentDataClassesDataContext" 
                            Select="new (Name, JobCatID, Status, AdminAccess)" TableName="JobCategories" 
                            Where="Status == @Status">
                            <WhereParameters>
                                <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                            </WhereParameters>
                        </asp:LinqDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="label">
                        Group Name:</td>
                    <td colspan="2" style="color: #FF3300">
                        <asp:TextBox ID="txtGroupName" runat="server" Width="245px"></asp:TextBox>
                        *</td>
                </tr>
                <tr>
                    <td class="label">
                        Status:</td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlStatus" runat="server">
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="0">InActive</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="left">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                            Text="Submit" />
                        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                            Text="Reset" />
                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                            Text="Delete" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="Label6" runat="server" ForeColor="#FF3300">The fields marked * 
                        are mandatory</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="2">
                        <asp:Panel ID="Panel1" runat="server" Height="225px" HorizontalAlign="Left" 
                            ScrollBars="Auto" Width="400px">
                            <asp:GridView ID="gvwGroupUser" runat="server" AutoGenerateColumns="False" 
                            
    onselectedindexchanged="gvwGroupUser_SelectedIndexChanged" BackColor="#E2E2E2" GridLines="None">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                        <ControlStyle Width="30px" />
                                    </asp:CommandField>
                                    <asp:BoundField DataField="GroupName" HeaderText="Group Name" ReadOnly="True" 
                                    SortExpression="GroupName" />
                                    <asp:BoundField DataField="Status" ReadOnly="True" 
                                    SortExpression="Status" >
                                        <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="GroupUserID" ReadOnly="True" 
                                    SortExpression="GroupUserID">
                                        <FooterStyle ForeColor="White" Width="1px" />
                                        <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="OrganizationID" ReadOnly="True" 
                                    SortExpression="OrganizationID">
                                        <FooterStyle ForeColor="White" Width="1px" />
                                        <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JobCatID" ReadOnly="True" SortExpression="JobCatID">
                                        <FooterStyle ForeColor="White" Width="1px" />
                                        <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqGroupUser" runat="server" 
                                ContextTypeName="AssesmentDataClassesDataContext" 
                                Select="new (GroupUserID, OrganizationID, JobCatID, GroupName, Status, AdminAccess)" 
                                TableName="GroupUsers">
                            </asp:LinqDataSource>
                        </asp:Panel>
                    </td>
                </tr>
                </table>
         
            </td>
        </tr>
    </table>
</div>


         
    