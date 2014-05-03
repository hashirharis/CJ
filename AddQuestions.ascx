<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddQuestions.ascx.cs" Inherits="AddQuestions" %>



<%@ Register assembly="FreeTextBox" namespace="FreeTextBoxControls" tagprefix="FTB" %>



<div align="left" style="padding-top: 10px; ">
    <table>
        <tr>
            <td align="left" valign="top">
    <table style="height: 350px;">
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                Upload&nbsp; Questions</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                Section Category</td>
            <td>
                <asp:DropDownList ID="ddlSectionCategory" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlSectionCategory_SelectedIndexChanged" 
                    Width="450px">
                    <asp:ListItem Value="0">-- Select Section Category --</asp:ListItem>
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
            <td class="label">
                Section</td>
            <td>
                <asp:DropDownList ID="ddlSectionList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlSectionList_SelectedIndexChanged" Width="450px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                1st Level SubSection</td>
            <td>
                <asp:DropDownList ID="ddlFirstLevelList" runat="server" 
                    AppendDataBoundItems="True" AutoPostBack="True" 
                    onselectedindexchanged="ddlFirstLevelList_SelectedIndexChanged" Width="450px">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                2nd Level SubSection</td>
            <td>
                <asp:DropDownList ID="ddlSecondLevelList" runat="server" 
                    AppendDataBoundItems="True" Width="450px" AutoPostBack="True" 
                    onselectedindexchanged="ddlSecondLevelList_SelectedIndexChanged">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="label">
                Category</td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                    style="height: 22px" Width="450px">
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
                <asp:Label runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="label">
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr><td colspan="2">
            <asp:Panel ID="pnlQuestions" runat="server" HorizontalAlign="Left">
                <table>
                    <tr>
                        <td width="60" >
                            QuestionCode</td>
                        <td>
                            <asp:TextBox ID="txtQuestionCode" runat="server" Width="300px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr><td width="60">
                        
                        Question</td>
                        <td>
                            <FTB:FreeTextBox ID="txtQues" runat="Server" 
                                AutoGenerateToolbarsFromString="True" AutoParseStyles="True" EnableSsl="False" 
                                Height="300px" PasteMode="Default" RenderMode="NotSet" 
                                StripAllScripting="False" TabMode="InsertSpaces" Text="" 
                                ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu|Bold,Italic,Underline,Strikethrough;Superscript,Subscript,RemoveFormat" 
                                ToolbarStyleConfiguration="NotSet" UpdateToolbar="True" Width="650px">
                            </FTB:FreeTextBox>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        <asp:Panel ID="pnlMemTypewords" runat="server" Visible="False">
                            <table>
                                <tr>
                                    <td>
                                        Enter words</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWords" runat="server" Width="300px" Height="75px" 
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 2</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord2" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 3</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord3" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 4</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord4" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 5</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord5" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 6</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord6" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 7</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord7" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 8</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord8" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 9</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord9" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 10</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord10" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 11</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord11" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 12</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord12" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 13</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord13" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 14</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord14" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 15</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord15" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 16</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord16" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 17</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord17" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 18</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord18" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 19</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord19" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Word 20</td>
                                    <td>
                                        <asp:TextBox ID="txtMemTypeWord20" runat="server" Width="300px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Display
                                        <br />
                                        Duration
                                        <br />
                                        (in seconds)</td>
                                    <td>
                                        <asp:TextBox ID="txtWordDisplayDuration" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Display Type</td>
                                    <td>
                                        <asp:DropDownList ID="ddlDisplayType_Words" runat="server">
                                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                            <asp:ListItem Value="1">Static</asp:ListItem>
                                            <asp:ListItem Value="2">Sequence</asp:ListItem>
                                            <asp:ListItem Value="3">Passage</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                            <asp:Panel ID="pnlMemTypeImages" runat="server" Visible="False">
                                <table>
                                    <tr>
                                        <td>
                                            Image</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeA" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button4" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeA');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages" runat="server" 
                                                onclick="btnDeleteMemTypeImages_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 2</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeB" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button5" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeB');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages2" runat="server" 
                                                onclick="btnDeleteMemTypeImages2_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 3</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeC" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button6" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeC');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages3" runat="server" 
                                                onclick="btnDeleteMemTypeImages3_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 4</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeD" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button7" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeD');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages4" runat="server" 
                                                onclick="btnDeleteMemTypeImages4_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 5</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeE" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button8" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeE');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages5" runat="server" 
                                                onclick="btnDeleteMemTypeImages5_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 6</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeF" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button9" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeF');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages6" runat="server" 
                                                onclick="btnDeleteMemTypeImages6_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 7</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeG" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button10" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeG');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages7" runat="server" 
                                                onclick="btnDeleteMemTypeImages7_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 8</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeH" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button11" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeH');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages8" runat="server" 
                                                onclick="btnDeleteMemTypeImages8_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 9</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeI" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button12" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeI');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages9" runat="server" 
                                                onclick="btnDeleteMemTypeImages9_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 10</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeJ" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button13" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeJ');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages10" runat="server" 
                                                onclick="btnDeleteMemTypeImages10_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 11</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeK" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button14" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeK');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages11" runat="server" 
                                                onclick="btnDeleteMemTypeImages11_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 12</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeL" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button15" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeL');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages12" runat="server" 
                                                onclick="btnDeleteMemTypeImages12_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 13</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeM" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button16" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeM');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages13" runat="server" 
                                                onclick="btnDeleteMemTypeImages13_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 14</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeN" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button17" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeN');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages14" runat="server" 
                                                onclick="btnDeleteMemTypeImages14_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 15</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeO" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button18" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeO');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages15" runat="server" 
                                                onclick="btnDeleteMemTypeImages15_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 16</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeP" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button19" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeP');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages16" runat="server" 
                                                onclick="btnDeleteMemTypeImages16_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 17</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeQ" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button20" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeQ');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages17" runat="server" 
                                                onclick="btnDeleteMemTypeImages17_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 18</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeR" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button21" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeR');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages18" runat="server" 
                                                onclick="btnDeleteMemTypeImages18_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 19</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeS" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button22" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeS');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages19" runat="server" 
                                                onclick="btnDeleteMemTypeImages19_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Image 20</td>
                                        <td>
                                            <input ID="txtfilenameMemTypeT" runat="server" style="width: 300px" 
                                                type="text" /><input ID="Button23" 
                                                onclick="dispQuestionFile('txtfilenameMemTypeT');" style="width: 100px" 
                                                type="button" value="Browse a File .." /><asp:Button 
                                                ID="btnDeleteMemTypeImages20" runat="server" 
                                                onclick="btnDeleteMemTypeImages20_Click" Text="Delete" Width="55px" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Display
                                            <br />
                                            Duration<br />
                                            Time
                                            <br />
                                            (in seconds)</td>
                                        <td style="margin-left: 40px">
                                            <asp:TextBox ID="txtImageDisplayDuration" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Display Type</td>
                                        <td style="margin-left: 40px">
                                            <asp:DropDownList ID="ddlDisplayType_Images" runat="server">
                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                <asp:ListItem Value="1">Static</asp:ListItem>
                                                <asp:ListItem Value="2">Sequence</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="pnlQuestionImage" runat="server" HorizontalAlign="Left" 
                                Visible="False">
                                <table>
                                    <tr>
                                        <td width="58">
                                            Question<br />
                                            File</td>
                                        <td>
                                            <input ID="txtFileName_main" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse_main" 
                                                onclick="dispQuestionFile('txtFileName_main');" type="button" 
                                                value="Browse ..." /></td>
                                        <td>
                                            <asp:Button ID="btnDeleteQuestionFilemain" runat="server" 
                                                onclick="btnDeleteQuestionFilemain_Click" Text="Delete" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="pnlQuestionImageSub" runat="server" Visible="False">
                                                <table>
                                                    <tr>
                                                        <td width="58">
                                                            Question<br />
                                                            File (Sub)</td>
                                                        <td>
                                                            <input ID="txtFileName_sub" runat="server" readonly="readonly" 
                                                                style="width: 350px" type="text" /><input ID="btnBrowse_sub" 
                                                                onclick="dispQuestionFile('txtFileName_sub');" type="button" 
                                                                value="Browse ..." /></td>
                                                        <td>
                                                            <asp:Button ID="btnDeleteQuestionFileSub" runat="server" 
                                                                onclick="btnDeleteQuestionFileSub_Click" Text="Delete" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <asp:Panel ID="pnlOptionEntry" runat="server" HorizontalAlign="Left">
                                <table>
                                    <tr>
                                        <td width="60">
                                            Option1</td>
                                        <td>
                                            <asp:TextBox ID="txtOption1" runat="server" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option2</td>
                                        <td>
                                            <asp:TextBox ID="txtOption2" runat="server" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option3</td>
                                        <td>
                                            <asp:TextBox ID="txtOption3" runat="server" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option4</td>
                                        <td>
                                            <asp:TextBox ID="txtOption4" runat="server" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option5</td>
                                        <td>
                                            <asp:TextBox ID="txtOption5" runat="server" Width="450px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="pnlRatingScale" runat="server" Visible="False">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td width="58">
                                                            Option6</td>
                                                        <td>
                                                            <asp:TextBox ID="txtOption6" runat="server" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Option7</td>
                                                        <td>
                                                            <asp:TextBox ID="txtOption7" runat="server" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Option8</td>
                                                        <td>
                                                            <asp:TextBox ID="txtOption8" runat="server" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Option9</td>
                                                        <td>
                                                            <asp:TextBox ID="txtOption9" runat="server" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Option10</td>
                                                        <td>
                                                            <asp:TextBox ID="txtOption10" runat="server" Width="450px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnlImageOptions" runat="server" HorizontalAlign="Left" 
                                Visible="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <asp:Label ID="lblmsgImageOption" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option1</td>
                                        <td>
                                            <input ID="txtFileName1" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse1" 
                                                onclick="dispQuestionFile('txtFileName1');" type="button" value="Browse ..." /><asp:Button 
                                                ID="btnDeleteQuestionFileOption1" runat="server" 
                                                onclick="btnDeleteQuestionFileOption1_Click" Text="Delete" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option2</td>
                                        <td>
                                            <input ID="txtFileName2" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse2" 
                                                onclick="dispQuestionFile('txtFileName2');" type="button" value="Browse ..." /><asp:Button 
                                                ID="btnDeleteQuestionFileOption2" runat="server" 
                                                onclick="btnDeleteQuestionFileOption2_Click" Text="Delete" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option3</td>
                                        <td>
                                            <input ID="txtFileName3" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse3" 
                                                onclick="dispQuestionFile('txtFileName3');" type="button" value="Browse ..." /><asp:Button 
                                                ID="btnDeleteQuestionFileOption3" runat="server" 
                                                onclick="btnDeleteQuestionFileOption3_Click" Text="Delete" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option4</td>
                                        <td>
                                            <input ID="txtFileName4" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse4" 
                                                onclick="dispQuestionFile('txtFileName4');" type="button" value="Browse ..." /><asp:Button 
                                                ID="btnDeleteQuestionFileOption4" runat="server" 
                                                onclick="btnDeleteQuestionFileOption4_Click" Text="Delete" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Option5</td>
                                        <td>
                                            <input ID="txtFileName5" runat="server" readonly="readonly" 
                                                style="width: 350px" type="text" /><input ID="btnBrowse5" 
                                                onclick="dispQuestionFile('txtFileName5');" type="button" value="Browse ..." /><asp:Button 
                                                ID="btnDeleteQuestionFileOption5" runat="server" 
                                                onclick="btnDeleteQuestionFileOption5_Click" Text="Delete" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <asp:Panel ID="pnlAnswer" runat="server">
                                <table>
                                    <tr>
                                        <td width="60">
                                            Answer</td>
                                        <td>
                                            <asp:DropDownList ID="ddlOption" runat="server" AppendDataBoundItems="True" 
                                                AutoPostBack="True" onselectedindexchanged="ddlOption_SelectedIndexChanged" 
                                                Width="100px">
                                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <%--</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">--%>&nbsp;<asp:Panel ID="pnlRatingStyle" 
                                runat="server" Visible="False">
                                <table>
                                    <tr>
                                        <td>
                                            Scoring Style</td>
                                        <td>
                                            <asp:DropDownList ID="ddlScoringStyle" runat="server" Width="100px">
                                                <asp:ListItem>Forward</asp:ListItem>
                                                <asp:ListItem>Reverse</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
            </td></tr>
        <tr><td colspan="2">
            &nbsp;</td></tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />
                <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" 
                    Text="Reset" />
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
            </td>
        </tr>
        <tr>
            <td id="quescollection" colspan="2" runat="server">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="AssesmentAppDataClassesDataContext" 
                    Select="new (QuestionID, Question, Answer, Session, Category)" 
                    TableName="QuestionCollections">
                </asp:LinqDataSource>
                <asp:Panel ID="Panel1" runat="server" Height="300px" HorizontalAlign="Left" 
                    ScrollBars="Auto" Width="650px">
                    <asp:GridView ID="gvwQues" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="gvwQues_SelectedIndexChanged" BackColor="#E2E2E2" 
                        GridLines="None" onrowdatabound="gvwQues_RowDataBound" Width="100%">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                <ControlStyle Width="30px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="QuestionCode" HeaderText="QuestionCode" 
                                SortExpression="QuestionCode" />
                            <asp:BoundField DataField="SectionName" HeaderText="Section" 
                                SortExpression="SectionName" />
                            <asp:BoundField DataField="Question" HeaderText="Question  " ReadOnly="True" 
                                SortExpression="Question">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Answer" HeaderText="Answer  " ReadOnly="True" 
                                SortExpression="Answer">
                                <ItemStyle Wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="QuestionID" ReadOnly="True" 
                                SortExpression="QuestionID" HeaderText="QuestionID">
                            </asp:BoundField>
                            <asp:BoundField DataField="QuestionFileName" HeaderText="File Name1" 
                                SortExpression="QuestionFileName" />
                            <asp:BoundField DataField="QuestionFileNameSub1" HeaderText="File Name2" 
                                SortExpression="QuestionFileNameSub1" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Visible="False" 
                    Width="650px">
                    <asp:GridView ID="gvwWordTypeMemQuestions" runat="server" 
                        AutoGenerateColumns="False" 
                        onselectedindexchanged="gvwWordTypeMemQuestions_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                            <asp:BoundField DataField="QuestionCode" HeaderText="QuestionCode" 
                                SortExpression="QuestionCode" />
                            <asp:BoundField DataField="SectionName" HeaderText="SectionName" 
                                ReadOnly="True" SortExpression="SectionName" />
                            <asp:BoundField DataField="Question" HeaderText="Question" ReadOnly="True" 
                                SortExpression="Question" />
                            <asp:BoundField DataField="Answer" HeaderText="Answer" ReadOnly="True" 
                                SortExpression="Answer" />
                            <asp:BoundField DataField="DisplayDuration" HeaderText="Duration (in sec)" 
                                ReadOnly="True" SortExpression="DisplayDuration" />
                            <asp:BoundField DataField="QuestionID" ReadOnly="True" 
                                SortExpression="QuestionID" HeaderText="QuestionID">
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqWordTypeMemQuestionList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (Question, Unit1, Unit2, Unit3, Unit4, Unit5, Unit6, Unit7, Unit8, Unit9, Unit10, Unit11, Unit12, Unit13, Unit14, Unit15, Unit16, Unit17, Unit18, Unit19, Unit20, Option1, Option2, Option3, Option4, Option5, Answer, DisplayDuration, Status, QuestionID, SectionId, SectionName, SectionNameSub1, SectionNameSub2, Category)" 
                        TableName="MemmoryTestTextQuesCollections">
                    </asp:LinqDataSource>
                </asp:Panel>
                <br />
                <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Visible="False" 
                    Width="650px">
                    <asp:GridView ID="gvwImageTypeMemQuestions" runat="server" 
                        AutoGenerateColumns="False" 
                        onselectedindexchanged="gvwImageTypeMemQuestions_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField SelectText="Edit" ShowSelectButton="True" />
                            <asp:BoundField DataField="QuestionCode" HeaderText="QuestionCode" 
                                SortExpression="QuestionCode" />
                            <asp:BoundField DataField="SectionName" HeaderText="SectionName" 
                                ReadOnly="True" SortExpression="SectionName" />
                            <asp:BoundField DataField="Question" HeaderText="Question" ReadOnly="True" 
                                SortExpression="Question" />
                            <asp:BoundField DataField="Answer" HeaderText="Answer" ReadOnly="True" 
                                SortExpression="Answer" />
                            <asp:BoundField DataField="DisplayDuration" HeaderText="Duration (in sec)" 
                                ReadOnly="True" SortExpression="DisplayDuration" />
                            <asp:BoundField DataField="QuestionID" ReadOnly="True" 
                                SortExpression="QuestionID" HeaderText="QuestionID">
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqImageTypeMemQuestionList" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (Question, Image1, Image2, Image3, Image4, Image5, Image6, Image7, Image8, Image9, Image10, Image11, Image12, Image13, Image14, Image15, Image16, Image17, Image18, Image19, Image20, OptionFile1, OptionFile2, OptionFile3, OptionFile4, OptionFile5, Option1, Option2, Option3, Option4, Option5, Answer, DisplayDuration, DisplayType, Status, QuestionID, SectionId, SectionName, SectionNameSub1, SectionNameSub2, Category)" 
                        TableName="MemmoryTestImageQuesCollections">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
    </table>
                </td>
            </tr>
        </table>
    </div>

