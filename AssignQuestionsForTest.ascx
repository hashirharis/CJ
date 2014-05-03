<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AssignQuestionsForTest.ascx.cs" Inherits="AssignQuestionsForTest" %>
<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>
<div align="left" style="padding-top: 10px">
    <table>
        <tr>
            <td valign="top" align="left" style="padding-left: 25px">
                <table style="height: 350px;">
                    <tr>
                        <td colspan="2" 
                            valign="top">
                            <div class="titlemain">
                            Assign Evaluation Test Questions</div>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                            <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
                        </td>
                        <td height="10">
                            <asp:DropDownList ID="ddlOrganizationList" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlOrganizationList_SelectedIndexChanged" Width="450px">
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
                        <td height="10" class="label">
                            Test Name</td>
                        <td height="10">
                            <asp:DropDownList ID="ddlTestName" runat="server" AppendDataBoundItems="True" 
                                AutoPostBack="True" onselectedindexchanged="ddlTestName_SelectedIndexChanged" 
                                Width="450px">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinqDataSource ID="LinqTestList" runat="server" 
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
                        <td height="10" class="label">
                            Test Section Name</td>
                        <td height="10">
                            <asp:DropDownList ID="ddlTestSectionName" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlTestSectionName_SelectedIndexChanged" Width="450px">
                                <asp:ListItem Value="0">-- Select Test Section from the List --</asp:ListItem>
                            </asp:DropDownList>
                            <asp:LinqDataSource ID="LinqTestSectionList" runat="server" 
                                ContextTypeName="AssesmentDataClassesDataContext" 
                                Select="new (TestSectionId, SectionName, TestId)" TableName="TestSectionsLists" 
                                Where="Status == @Status">
                                <WhereParameters>
                                    <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                </WhereParameters>
                            </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                            Section Category</td>
                        <td height="10">
                            <asp:DropDownList ID="ddlSectionCategory" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlSectionCategory_SelectedIndexChanged" Width="450px">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                <asp:LinqDataSource ID="sectionCategoryDataSource" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (SectionCategoryName, SectionCategoryId)" 
                    TableName="SectionCategories" Where="Status == @Status">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                Section</td>
                        <td height="10">
                <asp:DropDownList ID="ddlSectionList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlSectionList_SelectedIndexChanged" Width="450px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                1st Level SubSection</td>
                        <td height="10">
                <asp:DropDownList ID="ddlFirstLevelList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlFirstLevelList_SelectedIndexChanged" Width="450px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                2nd Level SubSection</td>
                        <td height="10">
                <asp:DropDownList ID="ddlSecondLevelList" runat="server" 
                    AppendDataBoundItems="True" Width="450px" AutoPostBack="True" 
                                onselectedindexchanged="ddlSecondLevelList_SelectedIndexChanged">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" class="label">
                            Select Question Type:</td>
                        <td height="10">
                            <asp:DropDownList ID="ddlQuestionType" runat="server" 
                                AppendDataBoundItems="True" AutoPostBack="True" 
                                onselectedindexchanged="ddlQuestionType_SelectedIndexChanged" 
                                Width="450px">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                <asp:ListItem Value="Objective">Objective Type Question</asp:ListItem>
                                <asp:ListItem Value="FillBlanks">Fill in the Blanks Question</asp:ListItem>
                                <asp:ListItem Value="RatingType">Rating Type Question</asp:ListItem>
                                <asp:ListItem Value="ImageType">Image Type Question</asp:ListItem>
                                <asp:ListItem Value="VideoType">Video Type Question</asp:ListItem>
                                <asp:ListItem Value="AudioType">Audio Type Question</asp:ListItem>
                                <asp:ListItem Value="MemTestWords">Memmory Test (Words)</asp:ListItem>
                                <asp:ListItem Value="MemTestImages">Memmory Test (Images)</asp:ListItem>
                                <asp:ListItem Value="PhotoType">Photo Type Question</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="300" valign="top" colspan="2">
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" ScrollBars="Auto">
                                <asp:GridView ID="gvwQues" runat="server" 
    AutoGenerateColumns="False" BackColor="#E2E2E2" GridLines="None" 
                                    onpageindexchanging="gvwQues_PageIndexChanging" PageSize="50" 
                                    onrowdatabound="gvwQues_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <AlternatingItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </AlternatingItemTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="25px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="QuestionCode" ReadOnly="True" 
                                        SortExpression="QuestionCode" HeaderText="QuestionCode" >
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Question" HeaderText="  Question  " ReadOnly="True" 
                                        SortExpression="Question" />
                                        <asp:BoundField DataField="Answer" HeaderText="  Answer  " ReadOnly="True" 
                                        SortExpression="Answer" />
                                        <asp:BoundField DataField="QuestionFileName" HeaderText="File Name1" 
                                            SortExpression="QuestionFileName" />
                                        <asp:BoundField DataField="QuestionFileNameSub1" HeaderText="File Name2" 
                                            SortExpression="QuestionFileNameSub1" />
                                        <asp:BoundField DataField="QuestionId" SortExpression="QuestionId">
                                            <HeaderStyle ForeColor="Silver" Width="1px" />
                                            <ItemStyle ForeColor="#CCCCCC" Width="1px" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                                
                                <asp:LinqDataSource ID="LinqQues" runat="server" 
                                    ContextTypeName="AssesmentAppDataClassesDataContext" 
                                    Select="new (QuestionId, Question, Answer)" 
                                    TableName="QuestionCollections">
                                </asp:LinqDataSource>
                            </asp:Panel>
                            <br />
                            <div align="left" style="padding-left: 50px">
                                    <asp:Label ID="lblMessage" runat="server" ForeColor="#FF3300"></asp:Label>
                                </div>
                                <div align="left">
                            <asp:Button ID="btnsubmit" runat="server" Text="Assign Selected Questions" 
                                onclick="btnsubmit_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="10" colspan="2" align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td height="10" colspan="2">
                            <table style="width:100%;">
                                <tr>
                                    <td style="font-weight: bold">
                            Add Result Bands Under each Test Sections</td>
                                </tr>
                                <tr>
                                    <td>
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        Display Name</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Marks From</td>
                                    <td>
                                        <asp:TextBox ID="txtVariableMarksFrom" runat="server" 
                                            MaxLength="3" Width="75px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="4" rowspan="4">
                <FTB:FreeTextBox ID="txtVariableDisplayName" runat="Server" Height="100px" Text="" 
                                                Width="525px" 
                                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                                              ToolbarStyleConfiguration="NotSet" 
                    UpdateToolbar="True"></FTB:FreeTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Marks To</td>
                                    <td>
                                        <asp:TextBox ID="txtVariableMarksTo" runat="server" 
                                            MaxLength="3" Width="75px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        Bench Mark</td>
                                    <td>
                                        <asp:TextBox ID="txtVariableBenchMark" runat="server" 
                                            MaxLength="3" Width="75px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
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
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="4">
                                        <table width="200">
                                            <tr>
                                                <td>
                                        <asp:Button ID="btnAddVariableBands" runat="server" 
                                            Text="Add" Width="55px" onclick="btnAddVariableBands_Click" />
                                                </td>
                                                <td>
                                        <asp:Button ID="btnResetVariableBands" runat="server" 
                                            Text="Reset" Width="55px" onclick="btnResetVariableBands_Click" />
                                                </td>
                                                <td>
                                        <asp:Button ID="btnDeleteVariableBands" runat="server" 
                                            Text="Delete" onclick="btnDeleteVariableBands_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="6">
                                        <asp:Label ID="lblMessageSectionBand" runat="server" 
                                            ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="7">
                                        <asp:Panel ID="Panel5" runat="server" ScrollBars="Auto" Width="750px">
                                            <asp:GridView ID="gvwVariableBands" runat="server" 
                                                AutoGenerateColumns="False" 
                                                onselectedindexchanged="gvwVariableBands_SelectedIndexChanged">
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
                                                    <asp:BoundField DataField="VariableBandId" ReadOnly="True" 
                                                        SortExpression="VariableBandId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="VariableId" ReadOnly="True" 
                                                        SortExpression="VariableId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TestSectionId" SortExpression="TestSectionId">
                                                        <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:LinqDataSource ID="LinqVariableBandDetails" runat="server" 
                                                ContextTypeName="AssesmentDataClassesDataContext" 
                                                Select="new (VariableBandId, TestId, VariableId, BenchMark, MarkFrom, MarkTo, DisplayName, Description, TestSectionId)" 
                                                TableName="TestVariableResultBands">
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
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                                        </td>
                                    </tr>
                                    </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                            &nbsp;</td>
                        <td height="10">
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
    </table>
</div>
