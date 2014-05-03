<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadScoringData.ascx.cs" Inherits="UploadScoringData" %>


<div align="left">
    <table>
        <tr>
        <td colspan="2" 
                            valign="top">
                            <div class="titlemain">
                            Import Scoring data from an Excel File 
                            </div>
            </td>            
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="label">
                Score File(select excel files only)</td>
            <td>
                <asp:TextBox ID="txtfilename" runat="server" Width="200px" BackColor="White" 
                    Enabled="False"></asp:TextBox>
                <input id="btnBrowse" type="button" 
                    value="Browse file ...." onclick="dispExcelFile('txtfilename')" /></td>
        </tr>
        <tr>
            <td class="label">
                Excel Sheet Name</td>
            <td>
                <asp:TextBox ID="txtSheetName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;<div align="center">
                <asp:Button ID="btnUpdateAndImport" runat="server" onclick="btnUpdateAndImport_Click" 
                    Text="Update and Import Data from Excel file" Width="250px" 
                        ToolTip="Click here to update existing data and import the fresh data to DB" />
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnReplaceAndImport" runat="server" onclick="btnReplaceAndImport_Click" 
                    Text="Replace and Import Data from Excel file" Width="250px" 
                        ToolTip="Click here to remove existing data and import the fresh data to DB" />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" ForeColor="Maroon" 
                                Text="You Can Reffer the TestId,TestSectionId and Variable names by selecting an Organization from the below list"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlOrganizationList" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                DataSourceID="LinqOrganizationList" DataTextField="Name" 
                                DataValueField="OrganizationID" 
                                onselectedindexchanged="ddlOrganizationList_SelectedIndexChanged">
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
                            <asp:GridView ID="gvwTest" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" onpageindexchanging="gvwTest_PageIndexChanging" 
                                onselectedindexchanged="gvwTest_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                                    <asp:BoundField DataField="TestId" HeaderText="Test ID" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvwSectionNameList" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" 
                                onpageindexchanging="gvwSectionNameList_PageIndexChanging" 
                                onselectedindexchanged="gvwSectionNameList_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField DataField="SectionName" HeaderText="Test Section Name" />
                                    <asp:BoundField DataField="TestsectionId" HeaderText="Test Section ID" />
                                    <asp:BoundField DataField="TestId" HeaderText="Test ID" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqTestSectionList" runat="server" 
                                ContextTypeName="AssesmentDataClassesDataContext" 
                                Select="new (TestId, TestSectionId, SectionName, Status)" 
                                TableName="TestSectionsLists">
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvwVariableNameList" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" 
                                onpageindexchanging="gvwVariableNameList_PageIndexChanging" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="SectionName" HeaderText="Variable Name" />
                                    <asp:BoundField DataField="SectionId" HeaderText="Variable Id" />
                                    <asp:BoundField DataField="TestSectionId" HeaderText="Test Section Id" />
                                    <asp:BoundField DataField="TestId" HeaderText="Test ID" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                            <asp:LinqDataSource ID="LinqVariableNameList" runat="server" 
                                ContextTypeName="AssesmentDataClassesDataContext" 
                                Select="new (SectionName, SectionId, TestSectionId, TestId, Status)" 
                                TableName="View_TestVariableNameLists">
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
