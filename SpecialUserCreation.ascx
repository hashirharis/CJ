<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialUserCreation.ascx.cs" Inherits="SpecialUserCreation" %>
<div align="left" style="padding-top: 10px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <table>
            <tr>
                <td align="left" valign="top">
                    <table style=" height: 350px">
                        <tr>
                            <td colspan="2" valign="top">
                                <div class="titlemain">
                                    User Creation</div>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Name of Test :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestLists" runat="server" AppendDataBoundItems="True" 
                                    Width="402px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqTestLists" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Name of Test1 :</td>
                            <td>
                                <asp:DropDownList ID="ddlTestlIst2" runat="server" AppendDataBoundItems="True" 
                                    Width="402px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTestlIst2_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestId, TestName, OrganizationName)" TableName="TestLists" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Group Name :</td>
                            <td>
                                <asp:DropDownList ID="ddlUserGroup" runat="server" AppendDataBoundItems="True" 
                                    Width="402px">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="GrpUserLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (GroupName, GroupUserID, OrganizationID)" TableName="GroupUsers">
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                User Name:</td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Password:</td>
                            <td>
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                EmailId</td>
                            <td>
                                <asp:TextBox ID="txtEmailId" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                User Type :</td>
                            <td>
                                <asp:DropDownList ID="ddlUserType" runat="server" Width="128px">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                    <asp:ListItem>OrgAdmin</asp:ListItem>
                                    <asp:ListItem>GrpAdmin</asp:ListItem>
                                    <asp:ListItem>User</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Login From Date:</td>
                            <td>
                                <input ID="txtLoginFromDate" runat="server" 
                                    onclick="dispCal('txtLoginFromDate')" readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="label">
                                Login To Date:</td>
                            <td>
                                <input ID="txtLoginToDate" runat="server" onclick="dispCal('txtLoginToDate')" 
                                    readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="label">
                                Status:</td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" Width="128px">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">InActive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                                    Text="Submit" />
                                <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                                    Text="Reset" />
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                    Text="Delete" />
                                <asp:Button ID="btnBulkCreation" runat="server" onclick="btnBulkCreation_Click" 
                                    Text="Bulk Upload" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="200" valign="top">
                                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Auto" 
                                    Width="752px">
                                    <asp:GridView ID="gvwPasswordCreation" runat="server" AllowPaging="True" 
                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="#E2E2E2" 
                                        GridLines="None" onpageindexchanging="gvwPasswordCreation_PageIndexChanging" 
                                        onselectedindexchanged="gvwPassword_SelectedIndexChanged" PageSize="50" 
                                        Width="98%">
                                        <Columns>
                                            <asp:TemplateField>
                                                <AlternatingItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </AlternatingItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" Width="25px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" 
                                                SortExpression="UserName" />
                                            <asp:BoundField DataField="Password" HeaderText="Password" ReadOnly="True" 
                                                SortExpression="Password" />
                                            <asp:BoundField DataField="UserType" HeaderText="User Type" ReadOnly="True" 
                                                SortExpression="UserType" />
                                            <asp:BoundField DataField="OrgName" HeaderText="Organization" ReadOnly="True" 
                                                SortExpression="OrgName" />
                                            <asp:BoundField DataField="GroupName" HeaderText="Group" ReadOnly="True" 
                                                SortExpression="GroupName" />
                                            <asp:BoundField DataField="TestName" HeaderText="Test Name" 
                                                SortExpression="TestName" />
                                            <asp:BoundField DataField="LoginFromDate" DataFormatString="{0:dd-MM-yyyy}" 
                                                HeaderText="Login From" ReadOnly="True" SortExpression="LoginFromDate">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LoginToDate" DataFormatString="{0:dd-MM-yyyy}" 
                                                HeaderText="Login To" ReadOnly="True" SortExpression="LoginToDate">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserId" ReadOnly="True" SortExpression="UserId">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="OrganizationID" ReadOnly="True" 
                                                SortExpression="OrganizationID">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GroupUserID" ReadOnly="True" 
                                                SortExpression="GroupUserID">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ReportAccess" ReadOnly="True" 
                                                SortExpression="ReportAccess">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status" SortExpression="Status">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TestId">
                                                <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                                            </asp:BoundField>
                                            <asp:CommandField SelectText="Edit" ShowSelectButton="True">
                                                <ControlStyle Width="30px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinqDataSource ID="UserLinqDataSource" runat="server" 
                                        ContextTypeName="AssesmentDataClassesDataContext" 
                                        Select="new (UserName, Password, UserType, OrgName, GroupName, UserId, OrganizationID, GroupUserID, LoginFromDate, LoginToDate, ReportAccess, Status, EmailId,TestId)" 
                                        TableName="View_UserDetails">
                                    </asp:LinqDataSource>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width:150px;">
                                            <asp:Button ID="btnDeleteAll" runat="server" onclick="btnDeleteAll_Click" 
                                                Text="Delete Selected Users" Width="150px" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblMessageDelAll" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
<asp:Panel ID="pnlBulkUserCreation" runat="server" Visible="False">
    <div align="left">
        <table style="width:100%;">
            <tr>
                <td colspan="2" valign="top">
                    <div class="titlemain">
                        Import User Details from an Excel File
                    </div>
                </td>
            </tr>
            <tr>
                <td class="label">
                    Prefix</td>
                <td>
                    <asp:TextBox ID="txtPrefix" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="label">
                    User Details File(select excel files only)</td>
                <td>
                    <asp:TextBox ID="txtfilename" runat="server" BackColor="White" Enabled="False" 
                        Width="300px"></asp:TextBox>
                    <input ID="btnBrowse" onclick="dispBulkUserExcelFile('txtfilename')" 
                        type="button" value="Browse file ...." /></td>
            </tr>
            <tr>
                <td class="label">
                    Excel Sheet Name</td>
                <td>
                    <asp:TextBox ID="txtSheetName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage0" runat="server" ForeColor="#FF3300"></asp:Label>
                    <asp:Panel ID="pnlProcessingSel" runat="server">
                        <div ID="divProcessSel" align="center">
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnImportUserList" runat="server" 
                        onclick="btnImportUserList_Click" OnClientClick="ShowSelProcess();" 
                        Text="Import Data from Excel file" Width="200px" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Text="Reset" />
                    &nbsp;
                    <asp:Button ID="btnGoBack" runat="server" onclick="btnGoBack_Click" 
                        Text="Go Back" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtErrorList" runat="server" Height="200px" 
                        TextMode="MultiLine" Visible="False" Width="100%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btndownload" runat="server" onclick="btndownload_Click" 
                        Text="Download Error list" Visible="False" />
                </td>
            </tr>
        </table>
    </div>
</asp:Panel>
<p>
    &nbsp;</p>
