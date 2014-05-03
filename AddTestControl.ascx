<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddTestControl.ascx.cs" Inherits="AddTestControl" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>


<div align="left" style="padding-top: 10px">
    <table>
        <tr>
            <td align="left" valign="top">
    <table >
        
        <tr>
            <td colspan="3" 
                valign="top">
                <div class="titlemain">
                Add Test</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlOrganizations" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    DataSourceID="LinqOrganizationList" DataTextField="Name" 
                    DataValueField="OrganizationID" 
                    onselectedindexchanged="ddlOrganizations_SelectedIndexChanged" 
                    Width="502px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqOrganizationList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Name, OrganizationID)" TableName="Organizations" 
                    Where="Status == @Status &amp;&amp; AdminAccess == @AdminAccess">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                        <asp:Parameter DefaultValue="1" Name="AdminAccess" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td class="label">
                Test Name</td>
            <td colspan="2">
                <asp:TextBox ID="txtTestName" runat="server" Width="500px" MaxLength="299"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                Report Type</td>
            <td>
                <asp:DropDownList ID="ddlReportType" runat="server">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    <asp:ListItem>Interpretative Report</asp:ListItem>
                    <asp:ListItem>Indicative Report</asp:ListItem>
                    <asp:ListItem>Certification Report</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:CheckBox ID="chbGroupReport" runat="server" Text="Group Report Access" />
            </td>
        </tr>
        <tr>
            <td class="label">
                Instructions</td>
            <td colspan="2">
                <FTB:FreeTextBox ID="FreeTextBox1" runat="Server" Height="300px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                                              ToolbarStyleConfiguration="NotSet" 
                    UpdateToolbar="True"></FTB:FreeTextBox></td>
        </tr>
        <tr>
            <td>
                Status</td>
            <td colspan="2">
                <asp:DropDownList ID="ddlStatus" runat="server" Width="128px">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td colspan="2">
                <table style="width: 200px;">
                    <tr>
                        <td>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />
                        </td>
                        <td>
                <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
                    Text="Reset" />
                        </td>
                        <td>
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Panel ID="Panel1" runat="server" Height="150px" HorizontalAlign="Left" 
                    ScrollBars="Auto" Width="650px">
                    <asp:GridView ID="gvwTestLists" runat="server" AutoGenerateColumns="False" 
                        BackColor="#E2E2E2" GridLines="None" 
                        onselectedindexchanged="gvwTestLists_SelectedIndexChanged" PageSize="20">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" />
                            <asp:BoundField DataField="OrganizationName" HeaderText="OrganizationName" 
                                ReadOnly="True" SortExpression="OrganizationName" />
                            <asp:BoundField DataField="TestName" HeaderText="TestName" ReadOnly="True" 
                                SortExpression="TestName" />
                            <asp:BoundField DataField="PassMark" ReadOnly="True" 
                                SortExpression="PassMark" >
                                <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" ReadOnly="True" SortExpression="Status">
                                <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId" 
                                HeaderText="Test ID">
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqTestDetails" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (TestId, TestName, OrganizationName, Status, Instructions, Description, PassMark)" 
                        TableName="TestLists">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        </table>

            </td>
        </tr>
    </table>

</div>