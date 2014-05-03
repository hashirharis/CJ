<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignationControl.ascx.cs" Inherits="DesignationControl" %>
<div align="left" style="padding-top: 10px">
    <table>
        <tr>
            <td align="left" valign="top">
    <table style="height: 350px">
        <tr>
            <td colspan="2" 
                valign="top">
                <div class="titlemain">
                Designation</div>
            </td>
        </tr>
        <tr>
            <td class="label">
                Designation:</td>
            <td>
                <asp:TextBox ID="txtPostName" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="label">
                <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
                :</td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1">Active</asp:ListItem>
                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblMessage" runat="server" ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="Submit" />
                <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
                    Text="Reset" />
                <asp:Button ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                    Text="Delete" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="275px" HorizontalAlign="Left" 
                    ScrollBars="Auto" Width="300px">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" BackColor="#E2E2E2" 
                        GridLines="None">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Edit" >
                                <ControlStyle Width="30px" />
                            </asp:CommandField>
                            <asp:BoundField DataField="PostName" HeaderText="Designation" 
                            SortExpression="PostName" />
                            <asp:BoundField DataField="Status" SortExpression="Status">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PostId" SortExpression="PostId">
                                <ItemStyle ForeColor="#E2E2E2" Width="1px" Font-Size="1px" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                        ContextTypeName="AssesmentDataClassesDataContext" 
                        Select="new (PostName, PostId, Status)" TableName="Designations">
                    </asp:LinqDataSource>
                </asp:Panel>
            </td>
        </tr>
        </table>

            </td>
        </tr>
    </table>

</div>