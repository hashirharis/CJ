<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddTestSections.ascx.cs" Inherits="AddTestSections" %>
<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<asp:Panel ID="Panel4" runat="server">
    <table>
        <tr>
            <td style="font-weight: bold">
                Add Sections for Test</td>
        </tr>
        <tr>
            <td style="text-align: left">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:DropDownList ID="ddlOrganization" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                DataSourceID="LinqOrganizationList" DataTextField="Name" 
                                DataValueField="OrganizationID" 
                                onselectedindexchanged="ddlOrganization_SelectedIndexChanged" Width="402px">
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
                        <td>
                            Test Name</td>
                        <td colspan="4">
                            <asp:DropDownList ID="ddlTestName" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="True" onselectedindexchanged="ddlTestName_SelectedIndexChanged" 
                                Width="402px">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Section Name</td>
                        <td colspan="4">
                            <asp:TextBox ID="txtSectionName" runat="server" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="4">
                            <table width="200">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAddSectionName" runat="server" 
                                            onclick="btnAddSectionName_Click" Text="Add" Width="55px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnResetSectionName" runat="server" 
                                            onclick="btnResetSectionName_Click" Text="Reset" Width="55px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDeleteSectionName" runat="server" 
                                            onclick="btnDeleteSectionName_Click" Text="Delete" Width="55px" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="4">
                            <asp:Label ID="lblMessageSectionName" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td colspan="4" style="text-align: left">
                            <asp:GridView ID="gvwSectionName" runat="server" AutoGenerateColumns="False" 
                                onselectedindexchanged="gvwSectionName_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                    <asp:BoundField DataField="SectionName" HeaderText="Section Name" 
                                        ReadOnly="True" SortExpression="SectionName" />
                                    <asp:BoundField DataField="TestSectionId" ReadOnly="True" 
                                        SortExpression="TestSectionId" HeaderText="Section ID">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId">
                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqSectionNameList" runat="server" 
                                ContextTypeName="AssesmentDataClassesDataContext" 
                                Select="new (TestSectionId, TestId, SectionName)" TableName="TestSectionsLists">
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="font-weight: bold">
                            Add Result Bands Under each Test Sections</td>
                    </tr>
                    <tr>
                        <td colspan="5" style="text-align: left">
                            <table>
                                <tr>
                                    <td>
                                        Section Name List</td>
                                    <td colspan="5" style="text-align: left">
                                        <asp:DropDownList ID="ddlSectionNameList" runat="server" 
                                            AppendDataBoundItems="True" AutoPostBack="True" 
                                            onselectedindexchanged="ddlSectionNameList_SelectedIndexChanged" Width="400px">
                                            <asp:ListItem Value="0">-- Select a Section from the List --</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinqDataSource ID="LinqTestSectionNameList" runat="server" 
                                            ContextTypeName="AssesmentDataClassesDataContext" 
                                            Select="new (TestSectionId, SectionName, AdminAccess)" 
                                            TableName="TestSectionsLists">
                                        </asp:LinqDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="4">
                                        Display Name</td>
                                </tr>
                                <tr>
                                    <td>
                                        Marks From</td>
                                    <td>
                                        <asp:TextBox ID="txtSectionMarksFrom" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
                                    </td>
                                    <td colspan="4" rowspan="4">
                                        <FTB:FreeTextBox ID="txtSectionDisplayName" runat="Server" Height="100px" 
                                            Text="" 
                                            ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                                            ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="525px">
                                        </FTB:FreeTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Marks To</td>
                                    <td>
                                        <asp:TextBox ID="txtSectionMarksTo" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Bench Mark</td>
                                    <td>
                                        <asp:TextBox ID="txtSectionBenchMark" runat="server" MaxLength="3" Width="75px"></asp:TextBox>
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
                                        &nbsp;</td>
                                    <td colspan="4">
                                        <table width="200">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAddSectionBands" runat="server" 
                                                        onclick="btnAddSectionBands_Click" Text="Add" Width="55px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnResetSectionBands" runat="server" 
                                                        onclick="btnResetSectionBands_Click" Text="Reset" Width="55px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnDeleteSectionBands" runat="server" 
                                                        onclick="btnDeleteSectionBands_Click" Text="Delete" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="5">
                                        <asp:Label ID="lblMessageSectionBand" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <asp:Panel ID="Panel5" runat="server">
                                            <asp:GridView ID="gvwSectionBands" runat="server" AutoGenerateColumns="False" 
                                                onselectedindexchanged="gvwSectionBands_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                                    <asp:BoundField DataField="MarkFrom" HeaderText="Mark From" ReadOnly="True" 
                                                        SortExpression="MarkFrom" />
                                                    <asp:BoundField DataField="MarkTo" HeaderText="Mark To" ReadOnly="True" 
                                                        SortExpression="MarkTo" />
                                                    <asp:BoundField DataField="DisplayName" HeaderText="Display Name" 
                                                        ReadOnly="True" SortExpression="DisplayName" />
                                                    <asp:BoundField DataField="BenchMark" HeaderText="Bench Mark" ReadOnly="True" 
                                                        SortExpression="BenchMark" />
                                                    <asp:BoundField DataField="SectionBandId" ReadOnly="True" 
                                                        SortExpression="SectionBandId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SectionId" ReadOnly="True" 
                                                        SortExpression="SectionId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:LinqDataSource ID="LinqSectionBandDetails" runat="server" 
                                                ContextTypeName="AssesmentDataClassesDataContext" 
                                                Select="new (SectionBandId, TestId, SectionId, BenchMark, MarkFrom, MarkTo, DisplayName, Description)" 
                                                TableName="TestSectionResultBands">
                                            </asp:LinqDataSource>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
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
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
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
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>
