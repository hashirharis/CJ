<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddQuestionCountControl.ascx.cs" Inherits="AddQuestionCountControl" %>


<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left">
    <table style="width: 500px; height: 350px">
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                    Evaluation Question Count</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="lblOrganization" runat="server" Text="Organization"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOrganizationList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlOrganizationList_SelectedIndexChanged" Width="500px">
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
            <td>
                <asp:DropDownList ID="ddlTestList" runat="server" AppendDataBoundItems="True" 
                    AutoPostBack="True" onselectedindexchanged="ddlTestList_SelectedIndexChanged" 
                    Width="500px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqTestList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (TestName, TestId, OrganizationName)" TableName="TestLists" 
                    Where="Status == @Status">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td class="label">
                Test Section Name</td>
            <td>
                <asp:DropDownList ID="ddlTestSectionList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlTestSectionList_SelectedIndexChanged" Width="500px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
                <asp:LinqDataSource ID="LinqTestSectionList" runat="server" 
                    ContextTypeName="AssesmentDataClassesDataContext" 
                    Select="new (TestSectionId, TestId, SectionName)" TableName="TestSectionsLists" 
                    Where="Status == @Status">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
        <tr>
            <td>
                Variable List</td>
            <td>
                <asp:DropDownList ID="ddlSectionList" runat="server" 
                    AppendDataBoundItems="True" Width="500px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSectionList_SelectedIndexChanged">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                1st Level variable List</td>
            <td>
                <asp:DropDownList ID="ddlFirstLevelList" runat="server" 
                    AppendDataBoundItems="True" Width="500px" 
                    onselectedindexchanged="ddlFirstLevelList_SelectedIndexChanged" 
                    AutoPostBack="True">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                2nd Level Variable List</td>
            <td>
                <asp:DropDownList ID="ddlSecondLevelList" runat="server" 
                    AppendDataBoundItems="True" 
                    onselectedindexchanged="ddlSecondLevelList_SelectedIndexChanged" Width="500px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Total Objective questions count:</td>
            <td>
                <asp:TextBox ID="txtobjQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Fill In The Blanks questions count:</td>
            <td>
                <asp:TextBox ID="txtfillBlanksQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Rating Question Count</td>
            <td>
                <asp:TextBox ID="txtRatingQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Image Question Count</td>
            <td>
                <asp:TextBox ID="txtImageQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Video Question Count</td>
            <td>
                <asp:TextBox ID="txtVideoQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Audio Question Count</td>
            <td>
                <asp:TextBox ID="txtAudioQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total WordType Memmory Question Count</td>
            <td>
                <asp:TextBox ID="txtWordTypeMemQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total ImageType Memmory Question Count</td>
            <td>
                <asp:TextBox ID="txtImageTypeMemQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Total Photo Type Question Count</td>
            <td>
                <asp:TextBox ID="txtPhotoTypeQuestionCount" runat="server" MaxLength="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMesasage" runat="server" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                    Text="Submit" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                    Text="Reset" />
                &nbsp;
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    OnClientClick="return confirm('Are you sure you want to delete this entry?');" 
                    Text="Delete" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Left" Width="800px">
                    <asp:GridView ID="gvwQuestionCount" runat="server" AutoGenerateColumns="False" 
                        onselectedindexchanged="gvwQuestionCount_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                            <asp:BoundField DataField="SectionName" HeaderText="Section Name" 
                                ReadOnly="True" SortExpression="SectionName" />
                            <asp:BoundField DataField="SectionNameSub1" HeaderText="Section Name Sub1" 
                                ReadOnly="True" SortExpression="SectionNameSub1" />
                            <asp:BoundField DataField="SectionNameSub2" HeaderText="Section Name Sub2" 
                                ReadOnly="True" SortExpression="SectionNameSub2" />
                            <asp:BoundField DataField="ObjQuestionCount" HeaderText="Obj Question Count" 
                                ReadOnly="True" SortExpression="ObjQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FillBlanksQuestionCount" 
                                HeaderText="Fill Blanks Question Count" ReadOnly="True" 
                                SortExpression="FillBlanksQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RatingQuestionCount" 
                                HeaderText="Rating Question Count" ReadOnly="True" 
                                SortExpression="RatingQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ImageQuestionCount" 
                                HeaderText="Image Question Count" ReadOnly="True" 
                                SortExpression="ImageQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="VideoQuestionCount" 
                                HeaderText="Video Question Count" ReadOnly="True" 
                                SortExpression="VideoQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AudioQuestionCount" 
                                HeaderText="Audio Question Count" ReadOnly="True" 
                                SortExpression="AudioQuestionCount">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WordTypeMemQuestionCount" 
                                HeaderText="WordType MemQuestion Count" 
                                SortExpression="WordTypeMemQuestionCount" />
                            <asp:BoundField DataField="ImageTypeMemQuestionCount" 
                                HeaderText="ImageType MemQuestion Count" 
                                SortExpression="ImageTypeMemQuestionCount" />
                            <asp:BoundField DataField="PhotoTypeQuestionCount" 
                                HeaderText="Photo Type Question Count" />
                            <asp:BoundField DataField="SectionId" ReadOnly="True" 
                                SortExpression="SectionId">
                                <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OrganizationId" ReadOnly="True" 
                                SortExpression="OrganizationId">
                                <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TestId" ReadOnly="True" SortExpression="TestId">
                                <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ID" ReadOnly="True" SortExpression="ID">
                                <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TestSectionId" SortExpression="TestSectionId">
                                <ItemStyle Font-Size="1px" ForeColor="White" Width="1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div align="center">
                    <asp:LinqDataSource ID="LinqQuestionCount" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" OrderBy="ID" 
                        Select="new (ID, SectionId, SectionName, SectionNameSub1, SectionNameSub2, ObjQuestionCount, FillBlanksQuestionCount, RatingQuestionCount, ImageQuestionCount, VideoQuestionCount, AudioQuestionCount, OrganizationId, TestId, TestSectionId, WordTypeMemQuestionCount, ImageTypeMemQuestionCount, PhotoTypeQuestionCount)" 
                        TableName="QuestionCounts">
                    </asp:LinqDataSource>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="top">
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>
