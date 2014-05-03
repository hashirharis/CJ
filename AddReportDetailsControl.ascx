<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddReportDetailsControl.ascx.cs" Inherits="AddReportDetailsControl" %>
<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<div align="left">
    <asp:Panel ID="Panel1" runat="server">
        <table>
            <tr>
                <td>
                    &nbsp;</td>
                <td class="titlemain">
                    Report Details</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOrganizationList" runat="server" 
                        AppendDataBoundItems="True" AutoPostBack="True" 
                        DataSourceID="LinqOrganizationList" DataTextField="Name" 
                        DataValueField="OrganizationID" 
                        onselectedindexchanged="ddlOrganizationList_SelectedIndexChanged" Width="550px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinqDataSource ID="LinqOrganizationList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (OrganizationID, Name)" TableName="Organizations" 
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
                    Test</td>
                <td>
                    <asp:DropDownList ID="ddlTestList" runat="server" AppendDataBoundItems="True" 
                        AutoPostBack="True" onselectedindexchanged="ddlTestList_SelectedIndexChanged" 
                        Width="550px">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Executive Summary<br />
                    (Above Graph)</td>
                <td>
                    <FTB:FreeTextBox ID="txtExeSummary1" runat="Server" Height="200px" Text="" 
                        ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                        ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="550px"></FTB:FreeTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Image1</td>
                <td>
                    <input ID="txtimage1" runat="server" style="width: 210px" type="text" 
                        readonly="readonly" /><input 
                        ID="Button1" onclick="dispQuestionFile('txtimage1');" type="button" 
                        value="Browse ..." />
                    <asp:Button ID="btnDeleteImage1" runat="server" onclick="btnDeleteImage1_Click" 
                        Text="Delete Image1" />
                </td>
            </tr>
            <tr>
                <td>
                    Executive Summary<br />
                    (Below Graph)</td>
                <td>
                    <FTB:FreeTextBox ID="txtExeSummary2" runat="Server" Height="200px" Text="" 
                        ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                        ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="550px"></FTB:FreeTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Image2</td>
                <td>
                    <input ID="txtimage2" runat="server" style="width: 210px" type="text" 
                        readonly="readonly" /><input 
                        ID="Button2" onclick="dispQuestionFile('txtimage2');" type="button" 
                        value="Browse ..." />
                    <asp:Button ID="btnDeleteImage2" runat="server" onclick="btnDeleteImage2_Click" 
                        Text="Delete Image2" />
                </td>
            </tr>
            <tr>
                <td>
                    Descriptive Report</td>
                <td>
                    <FTB:FreeTextBox ID="txtDescriptiveRpt" runat="Server" Height="200px" Text="" 
                        ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                        ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="550px"></FTB:FreeTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Report Conclusion</td>
                <td>
                    <FTB:FreeTextBox ID="txtConclusion" runat="Server" Height="200px" Text="" 
                        ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                        ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="550px"></FTB:FreeTextBox>
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
                    Summary Report Type</td>
                <td>
                    <asp:DropDownList ID="ddlSummaryGraph" runat="server" 
                        AppendDataBoundItems="True">
                        <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        <asp:ListItem Value="Variablewise">Variablewise</asp:ListItem>
                        <asp:ListItem Value="TestSectionwise">Test Sectionwise</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Scoring Type</td>
                <td>
                    <asp:DropDownList ID="ddlScoringType" runat="server">
                        <asp:ListItem>Percentage</asp:ListItem>
                        <asp:ListItem>Percentile</asp:ListItem>
                    </asp:DropDownList>
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
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                    Text="Submit" Width="75px" />
                            </td>
                            <td>
                                <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                                    style="height: 26px" Text="Reset" Width="75px" />
                            </td>
                            <td>
                                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                    Text="Delete" Width="75px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</div>
