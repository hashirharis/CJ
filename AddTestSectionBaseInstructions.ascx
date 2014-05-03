<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddTestSectionBaseInstructions.ascx.cs" Inherits="AddTestSectionBaseInstructions" %>
<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<asp:Panel ID="Panel1" runat="server">
    <table style="width:400px;">
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
                Sectionwise Instructions</td>
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
                <asp:DropDownList ID="ddlOrganizations" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganizations_SelectedIndexChanged" Width="502px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqOrganizationList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (Name, OrganizationID)" TableName="Organizations" 
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
                Test</td>
            <td>
                <asp:DropDownList ID="ddlTestNameList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlTestNameList_SelectedIndexChanged" 
                    Width="502px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Test Section</td>
            <td>
                <asp:DropDownList ID="ddlTestSectionList" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" onselectedindexchanged="ddlTestSectionList_SelectedIndexChanged" 
                    Width="502px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqTestSectionList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (SectionName, TestSectionId, TestId)" TableName="TestSectionsLists">
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td>
                Test Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                    Width="502px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    <asp:ListItem Value="1">Objective Type Questions</asp:ListItem>
                    <asp:ListItem Value="2">Fill in the Blanks Questions</asp:ListItem>
                    <asp:ListItem Value="3">Rating Type Questions</asp:ListItem>
                    <asp:ListItem Value="4">Image Type Questions</asp:ListItem>
                    <asp:ListItem Value="5">Video Type Questions</asp:ListItem>
                    <asp:ListItem Value="6">Audio Type Questions</asp:ListItem>
                    <asp:ListItem Value="7">Word Type Memmory Questions</asp:ListItem>
                    <asp:ListItem Value="8">Image Type Memmory Questions</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Instructions</td>
            <td>
                <FTB:FreeTextBox ID="FreeTextBox1" runat="Server" Height="200px" Text="" 
                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                    ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="525px"></FTB:FreeTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Status</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">In Active</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <table width="200">
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                Text="Submit" />
                        </td>
                        <td>
                            <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
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
    </table>
</asp:Panel>
