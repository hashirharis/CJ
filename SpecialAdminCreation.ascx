<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialAdminCreation.ascx.cs" Inherits="SpecialAdminCreation" %>

<div align="left" style="padding-top: 10px; ">
    <asp:Panel ID="pnlUserCreation" runat="server">
        <table>
            <tr>
                <td align="left" valign="top">
                    <table >
                        <tr>
                            <td colspan="2" 
                valign="top">
                                <div class="titlemain">
                                    Special Admin&nbsp; Creation</div>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" >
                                User Name:</td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" Width="400px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="label" >
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
                                Name Of Organisation:</td>
                            <td>
                                <asp:DropDownList ID="ddlOrg" runat="server" 
                    AppendDataBoundItems="True" Width="402px" 
                                    DataSourceID="OrgLinqDataSource" DataTextField="Name" 
                                    DataValueField="OrganizationID">
                                    <asp:ListItem Value="0">--select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="OrgLinqDataSource" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (Name, OrganizationID)" TableName="Organizations" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td class="label">
                                Login From Date:</td>
                            <td>
                                <input id="txtLoginFromDate" type="text" runat="server" onclick="dispCal('txtLoginFromDate')" 
                    readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="label" >
                                Login To Date:</td>
                            <td>
                                <input id="txtLoginToDate" type="text" runat="server" onclick="dispCal('txtLoginToDate')" 
                    readonly="readonly" /></td>
                        </tr>
                        <tr>
                            <td class="label" >
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
                            <td >
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                    onclick="btnSubmit_Click" />
                                <asp:Button ID="btnReset" runat="server" Text="Reset" 
                    onclick="btnReset_Click" />
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" valign="top">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" 
                    Width="752px">
                                    <asp:GridView ID="gvwSpecialAdminCreation" runat="server" 
                    AutoGenerateColumns="False" 
                    onselectedindexchanged="gvwSpecialAdminCreation_SelectedIndexChanged" BackColor="#E2E2E2" GridLines="None" 
                        Width="98%" AllowPaging="True" AllowSorting="True" 
                                        onpageindexchanging="gvwSpecialAdminCreation_PageIndexChanging" 
                                        PageSize="20">
                                        <Columns>
                                        
                                            <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" 
                            SortExpression="UserName" />
                                            <asp:BoundField DataField="OrgName" HeaderText="Organization" 
                            ReadOnly="True" SortExpression="OrgName" />
                                            <asp:BoundField DataField="LoginFromDate" DataFormatString="{0:dd-MM-yyyy}" 
                                HeaderText="Login From" ReadOnly="True" SortExpression="LoginFromDate" >
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Wrap="False" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LoginToDate" DataFormatString="{0:dd-MM-yyyy}" 
                                HeaderText="Login To" ReadOnly="True" SortExpression="LoginToDate" >
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
                                            <asp:BoundField DataField="Status" SortExpression="Status">
                                                <ItemStyle Font-Size="1px" ForeColor="Silver" Width="1px" />
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
                                            &nbsp;</td>                                    
                                    
                                        <td>
                                            <div style="position: absolute; z-index: 1; width: 350px; height: 200px; left: 350px; bottom: 10%;">
                                                <asp:Panel ID="pnldelete" runat="server" Width="310px" Visible="False" 
                                                    BorderColor="#FFCC99" BorderStyle="Groove" BorderWidth="5px">
                                                    
                                                    <table bgcolor="#CCFFFF" style="width:100%;">
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" 
                                                                style="padding-left: 15px; padding-right: 15px; text-align: justify;">
                                                                <asp:Label ID="lblMessageDelete" runat="server" 
                                                                    
                                                                    Text="Are you sure you want to delete this client admin and the datas under this organization"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="text-align: center">
                                                                    <asp:Button ID="btnDeleteYes" runat="server" onclick="btnDeleteYes_Click" 
                                                                        Text="Yes" Width="50px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnDeleteNo" runat="server" onclick="btnReset_Click" 
                                                                        Text="No" Width="50px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    
                                                </asp:Panel>
                                                <asp:Panel ID="pnlDeleteConfirm" runat="server" Visible="False" Width="310px" 
                                                    BorderColor="#FFCC99" BorderStyle="Groove" BorderWidth="5px">
                                                    <table bgcolor="#CCFFFF" style="width:100%;">
                                                        <tr>
                                                            <td colspan="2" 
                                                                style="padding-right: 15px; padding-left: 15px; text-align: justify;">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" 
                                                                style="padding-right: 15px; padding-left: 15px; text-align: justify;">
                                                                <asp:Label ID="lblMessageDeleteConfirm" runat="server"></asp:Label>
                                                                
                                                                <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                                                    Text="Do you want to proceed?" Font-Bold="True"></asp:Label>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <div style="text-align: center">
                                                                    <asp:Button ID="btnDeleteYesConfirm" runat="server" 
                                                                        onclick="btnDeleteYesConfirm_Click" Text="Yes" Width="50px" />
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="btnDeleteNoConfirm" runat="server" onclick="btnReset_Click" 
                                                                        Text="No" Width="50px" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;</td>
                                                        </tr>
                                                    </table>
                                                    </asp:Panel>
                                            </div>
                                            </td>
                                    </tr>
                                </table>
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
   </asp:Panel>
</div>
