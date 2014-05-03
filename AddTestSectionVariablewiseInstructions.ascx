<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddTestSectionVariablewiseInstructions.ascx.cs" Inherits="AddTestSectionVariablewiseInstructions" %>

<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
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
                Variablewise Instructions</td>
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
                1st Level variable List</td>
            <td>
                <asp:DropDownList ID="ddlFirstLevelVariableList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlFirstLevelVariableList_SelectedIndexChanged" 
                    Width="500px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                2nd Level variable List</td>
            <td>
                <asp:DropDownList ID="ddlSecondLevelVariableList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlSecondLevelVariableList_SelectedIndexChanged" 
                    Width="500px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
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
                    <asp:ListItem Value="4">Image Type Questions</asp:ListItem>
                    <asp:ListItem Value="5">Video Type Questions</asp:ListItem>
                    <asp:ListItem Value="6">Audio Type Questions</asp:ListItem>
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
                Image1</td>
            <td>
                <input ID="txtfilename1" runat="server" style="width: 300px" type="text" /><input 
                    ID="Button4" onclick="dispQuestionFile('txtfilename1');" 
                    style="width: 100px" type="button" value="Browse a File .." /><asp:Button 
                    ID="btnDeleteImage1" runat="server" onclick="btnDeleteImage1_Click" 
                    Text="Delete" Width="55px" />
            </td>
        </tr>
        <tr>
            <td>
                Instructions2</td>
            <td>
                <FTB:FreeTextBox ID="FreeTextBox2" runat="Server" Height="200px" Text="" 
                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                    ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="525px">
                </FTB:FreeTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Image2</td>
            <td>
                <input ID="txtfilename2" runat="server" style="width: 300px" type="text" /><input 
                    ID="Button5" onclick="dispQuestionFile('txtfilename2');" 
                    style="width: 100px" type="button" value="Browse a File .." /><asp:Button 
                    ID="btnDeleteImage2" runat="server" onclick="btnDeleteImage2_Click" 
                    Text="Delete" Width="55px" />
            </td>
        </tr>
        <tr>
            <td>
                Instructions3</td>
            <td>
                <FTB:FreeTextBox ID="FreeTextBox3" runat="Server" Height="200px" Text="" 
                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                    ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="525px">
                </FTB:FreeTextBox>
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