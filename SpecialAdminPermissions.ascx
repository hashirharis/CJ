<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SpecialAdminPermissions.ascx.cs" Inherits="SpecialAdminPermissions" %>
<div align="center">
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Left" Width="550px" 
        BorderColor="#FFCC99" BorderWidth="2px">
        <table style="width:100%;">
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="titlemain">
                        Assign Admin Permissions</div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr>
                            <td style="vertical-align: top">
                                Organization List</td>
                            <td style="vertical-align: top">
                                Admin List</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <asp:DropDownList ID="ddlOrganizations" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    DataSourceID="LinqOrganizationsList" DataTextField="Name" 
                                    DataValueField="OrganizationID" 
                                    onselectedindexchanged="ddlOrganizations_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqOrganizationsList" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (Name, OrganizationID)" TableName="Organizations" 
                                    Where="Status == @Status">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                            <td style="vertical-align: top">
                                <asp:DropDownList ID="ddlAdminList" runat="server" AppendDataBoundItems="True" 
                                    AutoPostBack="True" DataSourceID="LinqAdminList" DataTextField="UserName" 
                                    DataValueField="UserId" 
                                    onselectedindexchanged="ddlAdminList_SelectedIndexChanged">
                                    <asp:ListItem Value="0">-- select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinqDataSource ID="LinqAdminList" runat="server" 
                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                    Select="new (UserName, UserId)" TableName="UserProfiles" 
                                    Where="UserType == @UserType &amp;&amp; OrganizationID == @OrganizationID">
                                    <WhereParameters>
                                        <asp:Parameter DefaultValue="SpecialAdmin" Name="UserType" Type="String" />
                                        <asp:ControlParameter ControlID="ddlOrganizations" DefaultValue="0" 
                                            Name="OrganizationID" PropertyName="SelectedValue" Type="Int32" />
                                    </WhereParameters>
                                </asp:LinqDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="font-weight: bold">
                                            Question Types</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:CheckBoxList ID="chblQuestionTypes" runat="server" BackColor="#E2E2E2">
                                                <asp:ListItem Value="Objective">Objective Type Question</asp:ListItem>
                                                <asp:ListItem Value="FillBlanks">Fill in the Blanks Question</asp:ListItem>
                                                <asp:ListItem Value="RatingType">Rating Type Question</asp:ListItem>
                                                <asp:ListItem Value="ImageType">Image Type Question</asp:ListItem>
                                                <asp:ListItem Value="VideoType">Video Type Question</asp:ListItem>
                                                <asp:ListItem Value="AudioType">Audio Type Question</asp:ListItem>
                                                <asp:ListItem Value="MemTestWords">Memmory Test (Words)</asp:ListItem>
                                                <asp:ListItem Value="MemTestImages">Memmory Test (Images)</asp:ListItem>
                                                <asp:ListItem Value="PhotoType">Photo Type Question</asp:ListItem>
                                            </asp:CheckBoxList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMessage_type" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 200px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAssign_type" runat="server" onclick="btnAssign_type_Click" 
                                                            Text="Assign" Width="50px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDelete_type" runat="server" onclick="btnDelete_type_Click" 
                                                            Text="Delete" Width="50px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset_type" runat="server" onclick="btnReset_type_Click" 
                                                            Text="Reset" Width="50px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align: top">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="font-weight: bold">
                                            Permissions</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div>
                                                <asp:GridView ID="gvwUserPermissions" runat="server" 
                                                    AutoGenerateColumns="False" BackColor="#E2E2E2" 
                                                    DataSourceID="LinqUserPermissions" GridLines="Horizontal">
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
                                                        <asp:BoundField DataField="MenuId">
                                                            <HeaderStyle Width="1px" />
                                                            <ItemStyle Font-Size="1px" ForeColor="#E2E2E2" Width="1px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MenuName" HeaderText="Menu Name" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:LinqDataSource ID="LinqUserPermissions" runat="server" 
                                                    ContextTypeName="AssesmentDataClassesDataContext" 
                                                    Select="new (MenuId, MenuName, MenuControl)" TableName="MenuCollections" 
                                                    OrderBy="DisplayOrder" Where="Status == @Status">
                                                    <whereparameters>
                                                        <asp:Parameter DefaultValue="1" Name="Status" Type="Int32" />
                                                    </whereparameters>
                                                </asp:LinqDataSource>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="width: 300px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAssign" runat="server" onclick="btnAssign_Click" 
                                                            Text="Assign" Width="75px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                                                            Text="Delete" Width="75px" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnReset" runat="server" onclick="btnReset_Click" Text="Reset" 
                                                            Width="75px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
</div>
