u<%@ Control Language="C#" AutoEventWireup="true" CodeFile="QuestionOptionUserDetailsControl.ascx.cs" Inherits="QuestionOptionUserDetailsControl" %>
<div>
    <asp:Panel ID="Panel1" runat="server">
        <table style="width:100%;">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table>
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
                                Question,Options,User Details Report (Super Admin)</td>
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
                                    Width="550px" AutoPostBack="True" onchange="ShowProcess();"
                                    onselectedindexchanged="ddlTestList_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
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
                                <div ID="divProcess">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <table width="300">
                                    <tr>
                                        <td ID="Question Details">
                                            <asp:Button ID="btnUserDetails" runat="server" onclick="btnUserDetails_Click" 
                                                Text="User Test Details"  />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnQuesDetails" runat="server" onclick="btnQuesDetails_Click" Text="Question Details" 
                                                Width="147px" />
                                        </td>
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
                <td>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" Width="750px">
                        <asp:GridView ID="gvwReport" runat="server">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Auto" 
                        Width="750px" Visible="False">
                        <asp:GridView ID="gvwQuestions" runat="server" Visible="False">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvwUserDetails" runat="server" Visible="False">
                    </asp:GridView>
                    <asp:GridView ID="gvwQuestionIdDet" runat="server" Visible="False">
                    </asp:GridView>
                    <asp:GridView ID="gvwQuestionIdDet_memimg" runat="server" Visible="False">
                    </asp:GridView>
                    <asp:GridView ID="gvwQuestionIdDet_memword" runat="server" Visible="False">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</div>
