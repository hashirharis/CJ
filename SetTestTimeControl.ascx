<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SetTestTimeControl.ascx.cs" Inherits="SetTestTimeControl" %>
<div align="left" style="padding-top: 20px; padding-left: 20px;">
                <asp:Panel ID="Panel4" runat="server">
                    <table>
                        <tr>
                            <td colspan="2">
                                <div class="titlemain">
                                    Add Time for Test</div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlOrganization" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlOrganization_SelectedIndexChanged" 
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqOrganization" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (Name, OrganizationID, Status)" TableName="Organizations" 
                                    Where="AdminAccess == @AdminAccess &amp;&amp; Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="AdminAccess" Type="Int32" />
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Test</td>
                            <td>
                                <asp:DropDownList ID="ddlTestName" runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="ddlTestName_SelectedIndexChanged" 
                                    AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Test Section</td>
                            <td>
                                <asp:DropDownList ID="ddlTestSectionName" runat="server" AutoPostBack="True" 
                                    AppendDataBoundItems="True" 
                                    onselectedindexchanged="ddlTestSectionName_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqTestSection" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (SectionName, TestSectionId, TestId, Status)" 
                                    TableName="TestSectionsLists">
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Select Variable</td>
                            <td>
                                <asp:DropDownList ID="ddlVariableList" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlVariableList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Set Time</td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtHours" runat="server" MaxLength="2" Width="50px"
                                            inblur="myJSFunction(this);" onChange="myJSFunction(this);" >0</asp:TextBox>
                                        </td>
                                        <td>
                                            Hrs.</td>
                                        <td style="padding-left: 5px">
                                            <asp:TextBox ID="txtMinutes" runat="server" MaxLength="2" Width="50px"
                                             inblur="myJSFunction(this);" onChange="myJSFunction(this);">0</asp:TextBox>
                                        </td>
                                        <td>
                                            Min.</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Label ID="lblMessageTimes" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <table style="width: 200px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnAddTime" runat="server" onclick="btnAddTime_Click" 
                                                Text="Submit" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnResetTime" runat="server" onclick="btnResetTime_Click" 
                                                Text="Reset" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDeleteTime" runat="server" onclick="btnDeleteTime_Click" 
                                                Text="Delete" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 10px">
                                <asp:GridView ID="gvwTimeDetails" runat="server" AutoGenerateColumns="False" 
                                    onselectedindexchanged="gvwTimeDetails_SelectedIndexChanged">
                                    <Columns>
                                        <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                                        <asp:BoundField DataField="TestName" HeaderText="TestName" ReadOnly="True" 
                                            SortExpression="TestName" />
                                        <asp:BoundField DataField="SectionName" HeaderText="Section Name" 
                                            ReadOnly="True" SortExpression="SectionName" />
                                        <asp:BoundField DataField="VariableName" HeaderText="Variable Name" 
                                            SortExpression="VariableName" />
                                        <asp:BoundField DataField="TimeHours" HeaderText="Hours" ReadOnly="True" 
                                            SortExpression="TimeHours">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimeMinutes" HeaderText="Minutes" ReadOnly="True" 
                                            SortExpression="TimeMinutes">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" ReadOnly="True" 
                                            SortExpression="Status">
                                            <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId">
                                            <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestSectionId" ReadOnly="True" 
                                            SortExpression="TestSectionId">
                                            <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TestVariableId">
                                            <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TimerId" ReadOnly="True" SortExpression="TimerId">
                                            <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                <asp:LinqDataSource ID="LinqTimerDetails" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (TestName, SectionName, TimeHours, TimeMinutes, Status, TestId, TestSectionId, TimerId, VariableName, TestVariableId)" 
                                    TableName="View_TimerDetails">
                                </asp:LinqDataSource>
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
