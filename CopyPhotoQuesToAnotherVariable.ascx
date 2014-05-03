<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyPhotoQuesToAnotherVariable.ascx.cs" Inherits="CopyPhotoQuesToAnotherVariable" %>
<div align="left">
    <asp:Panel ID="Panel1" runat="server">
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="titlemain" colspan="2">
                    Copy Existing Questions to Another Variable(s)</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr style="font-weight: bold">
                            <td>
                                &nbsp;</td>
                            <td width="320">
                                Select appropriate Variable to get
                                <br />
                                Photo Type Questions Under it</td>
                            <td>
                                Select a Variable (The selected questions<br />
                                &nbsp;will be added under the selected variable)</td>
                        </tr>
                        <tr>
                            <td>
                                Section</td>
                            <td>
                                <asp:DropDownList ID="ddlSectionList" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSectionList_SelectedIndexChanged" Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSectionList0" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSectionList0_SelectedIndexChanged" 
                                    Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                1st Level SubSection</td>
                            <td>
                                <asp:DropDownList ID="ddlFirstLevelList" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlFirstLevelList_SelectedIndexChanged" 
                                    Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFirstLevelList0" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlFirstLevelList0_SelectedIndexChanged" 
                                    Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                2nd Level SubSection</td>
                            <td>
                                <asp:DropDownList ID="ddlSecondLevelList" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSecondLevelList_SelectedIndexChanged" 
                                    Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSecondLevelList0" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    onselectedindexchanged="ddlSecondLevelList0_SelectedIndexChanged" 
                                    Width="300px">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
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
                                <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                                    Text="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <div style="width: 700px; overflow: scroll">
                                    <asp:GridView ID="gvwQuestionBank" runat="server" AllowPaging="True" 
                                        PageSize="100" AutoGenerateColumns="False" GridLines="None" 
                                        onpageindexchanging="gvwQuestionBank_PageIndexChanging">
                                        
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
                                            <asp:BoundField DataField="QuestionID" ReadOnly="True" 
                                        SortExpression="QuestionID" >
                                            <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Question" HeaderText="  Question  " ReadOnly="True" 
                                        SortExpression="Question" />
                                        <asp:BoundField DataField="Answer" HeaderText="  Answer  " ReadOnly="True" 
                                        SortExpression="Answer" />
                                        </Columns>
                                        
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
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
            </tr>
        </table>
    </asp:Panel>
</div>
